
#include <stdlib.h>
#include <stdint.h>
#include <stdbool.h>
#include "usb_wrapper.h"
#include "dac_wrapper.h"
#include "flash_wrapper.h"
#include "gpio_wrapper.h"
#include "timer_wrapper.h"
#include "mcc_generated_files/tmr3.h"


static uint16_t TIME_samplePrev = 0;
static uint16_t TIME_cnt500usec = 0;
static uint16_t TIME_cntSeconds = 0;

void TIME_clearCount()
{
    TIME_cnt500usec = 0;
    TIME_cntSeconds = 0;
}

uint16_t TIME_getCnt500usec()
{
    return TIME_cnt500usec;
}

static uint8_t  TIME_pageNumber = 0;
static uint16_t TIME_pageOffset = 0;

uint16_t TIME_getCurrentSampleIndex(void)
{
    return TIME_pageOffset;
}

#define TIME_loopStackDepth 8
static uint8_t TIME_loopStackPtr = 0;
static uint16_t Time_loopCount[TIME_loopStackDepth] = {0};
static uint16_t TIME_loopOffset[TIME_loopStackDepth] = {0};
static uint16_t TIME_repeatCount = 0;
static bool TIMER_enableDAC = false;

static void handleCmdFor()
{
    while (FLASH_isStartLoop(TIME_pageNumber, TIME_pageOffset))
    {
        if (TIME_loopStackPtr < TIME_loopStackDepth)
        {
            uint16_t cnt = FLASH_readSample(TIME_pageNumber, TIME_pageOffset);
            cnt &= 0x0fff;
            Time_loopCount[TIME_loopStackPtr] = cnt;
            TIME_pageOffset++;
            TIME_loopOffset[TIME_loopStackPtr] = TIME_pageOffset;
            TIME_loopStackPtr++;
        }
    }
}

static void handleCmdEndFor()
{
    while (FLASH_isEndLoop(TIME_pageNumber, TIME_pageOffset))
    {
        if (TIME_loopStackPtr == 0)
        {
            break;
        }
        uint8_t stackEntry = TIME_loopStackPtr-1;
        if (Time_loopCount[stackEntry] <= 1)
        {
            TIME_pageOffset++;
            TIME_loopStackPtr--;
        }
        else
        {
            Time_loopCount[stackEntry] -= 1;
            TIME_pageOffset = TIME_loopOffset[stackEntry];
        }
    }        
}

static bool handleCmdRep(int16_t sample)
{
    if (sample >= -1)
    {
        TIME_pageOffset++;
        return false;
    }
        
    if (TIME_repeatCount == 0)
    {
        TIME_repeatCount = abs(sample);
    }
    else
    {
        TIME_repeatCount--;
        if (TIME_repeatCount == 0)
        {
            TIME_pageOffset++;
        }
    }
    return true;
}

void TMR3_callBack(void)
{
    bool triggerPressed  = TIME_isUSBPressed();
    if (!triggerPressed)
    {
        if (TIMER_enableDAC)
        {
            DAC_turnAllDACsOff();
        }
        TIMER_enableDAC = false;
        TIME_pageOffset = 0;
        return;
    }

    TIMER_enableDAC = true;
    handleCmdFor();
    handleCmdEndFor();

    int16_t sample = FLASH_readSample(TIME_pageNumber, TIME_pageOffset);
    if (sample == -1)
    {
        TIME_pageOffset = 0;
        return;
    }

    if (handleCmdRep(sample)) return;

    sample = ((uint16_t)sample > 4095)? 4095 : sample;
    DAC_driveActiveDAC((uint16_t)sample);
    TIME_cnt500usec++;
    TIME_samplePrev = sample;
}

void TIME_setPeriod(uint16_t newPeriod)
{
    TMR3_Period16BitSet(newPeriod);
}

void TIME_setFreq(double newFreqInHz)
{
    double clkFreqInMHz = 16;
    double clkFreqInHz = clkFreqInMHz * 1000000;
    double newPeriod = (clkFreqInHz / newFreqInHz) - 1;
    uint16_t maxInt = 65535;
    if (newPeriod > (double)maxInt)
    {
        newPeriod = (double)maxInt;
    }
    uint16_t newPeriodInt = (uint16_t)newPeriod;
    TIME_setPeriod(newPeriodInt);
}

uint16_t TIME_getPeriod(void)
{
    return TMR3_Period16BitGet();
}

void TIME_setCounter(uint16_t newCnt)
{
    TMR3_Counter16BitSet(newCnt);
}

uint16_t TIME_getCounter(void)
{
    return TMR3_Counter16BitGet();
}

static bool USB_triggerPressed = false;

bool TIME_isUSBPressed(void)
{
    return USB_triggerPressed;
}

void TIME_setUSBPressed(bool newVal)
{
    USB_triggerPressed = newVal;
}
