
#include <stdlib.h>
#include <stdint.h>
#include <stdbool.h>
#include "usb_wrapper.h"
#include "trigger_wrapper.h"
#include "dac_wrapper.h"
#include "flash_wrapper.h"
#include "gpio_wrapper.h"
#include "mcc_generated_files/tmr3.h"


static uint16_t TIME_sampleCurr = 0; // *p_waveformSample;
static uint16_t TIME_samplePrev = 0; // FIXME use this in polling loop for interpolation

static uint16_t TIME_cnt500usec = 0;
static uint16_t TIME_cntSeconds = 0;
static bool TIME_timeTickMonitor = false;


bool TIME_isTimeMonitorEnabled(void)
{
    return TIME_timeTickMonitor;
}

void TIME_enableTimeMonitor(bool newVal)
{
    TIME_timeTickMonitor = newVal;
}

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
static uint16_t TIME_waveCount = 0;
static bool TIMER_enableDAC = false;

void handleStartLoop()
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

void handleEndLoop()
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

void TMR3_callBack(void)
{
    bool singleShot = true;
    bool isSignalOn = TRIG_isSignalOn();
    if (isSignalOn)
    {
        bool triggerVal      = GPIO_get(GPIO_TRIGGER_BUTTON);
        bool triggerPressed  = !triggerVal || TRIG_isUSBPressed();
        if (!triggerPressed)
        {
            DAC_turnAllDACsOff();
        }
        handleStartLoop();
        handleEndLoop();

        int16_t sample = FLASH_readSample(TIME_pageNumber, TIME_pageOffset);
        if (sample == -1)
        {
            TIME_waveCount++;
            TIME_pageOffset = 0;
            sample = FLASH_readSample(TIME_pageNumber, TIME_pageOffset);
            if (sample == -1) return;
        }
        
        if (sample < -1)
        {
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
            if (TIME_repeatCount > 0) return;
            sample = TIME_sampleCurr;
        }
        else
        {
            if (!singleShot || TIME_waveCount < 1)
            {
                TIME_pageOffset++;
            }
        }
        TIME_samplePrev = TIME_sampleCurr;
        sample = ((uint16_t)sample > 4095)? 4095 : sample;
        TIME_sampleCurr = sample;
        
        if (singleShot)
        {
            if (sample == -1)
            {
                DAC_turnAllDACsOff();
                TIMER_enableDAC = false;                 
            }
            else if (TIME_waveCount > 1)
            {
                TIMER_enableDAC = false;
            }
            else
            {
                TIMER_enableDAC = true;               
            }
           
        }
        else
        {
            if (TIME_cnt500usec >= 1000 && TIME_pageOffset == 1)
            {
                DAC_turnAllDACsOff();
                TIMER_enableDAC = false;
            }
            else if (TIME_cnt500usec < 1000)
            {
                TIMER_enableDAC = true;
            }
        }

        if (!TIMER_enableDAC)
        {
            TIME_pageOffset = 0;
            TIME_waveCount = 0;
        }
        
        if (TIMER_enableDAC && triggerPressed)
        {
            DAC_driveActiveDAC((uint16_t)TIME_sampleCurr);
        }
    }
    TIME_cnt500usec++;
    
    if (TIME_cnt500usec >= 2000)
    {
        if (!isSignalOn)
        {
            TIME_pageNumber = 0;
            TIME_pageOffset = 0;
            TIME_waveCount = 0;
        }
        TIME_cnt500usec = 0; // reset every second
        TIME_cntSeconds++; // increment every second
        if (TIME_timeTickMonitor)
        {
            uint16_t tm3TimerCount = TMR3_Counter16BitGet();
            USB_printfLinePrompt("TMR3 callback = %d seconds %d. sample = %d", TIME_cntSeconds, tm3TimerCount, TIME_samplePrev);
        }
    }
    else if (TIME_cnt500usec == 1100)
    {
        TRIG_handleTrigger();
    }
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

void TIME_interp()
{
    bool isInterplationOn = true;
    bool isSignalOn = TRIG_isSignalOn();
    if (!isInterplationOn || !isSignalOn) return;
  
    uint16_t per = TIME_getPeriod();
    uint16_t cnt = TIME_getCounter();
    if (per - cnt < 1000) return;

    uint8_t sampleTableId = 0;
    uint16_t sampleIndex = TIME_getCurrentSampleIndex();
    int16_t samples[4];
    uint8_t x = 0;
    for (x=0; x< 4; x++)
    {
        int16_t ii = sampleIndex - 1 + x;
        if (ii < 0) {ii = 0;}
        samples[x] = FLASH_readSample(sampleTableId, ii);
    }

    double slope = (double)(samples[2] - samples[1]) /
                   (double)(per);
    double rise = slope*(double)cnt;

    double interpDouble = (double)(samples[1]) + rise;
    int16_t interpValue = (uint16_t)interpDouble;
    DAC_driveActiveDAC((uint16_t)interpValue);
    USB_printfLine("i=%d. per=%d, cnt=%d, x0=%d, x=%d x1=%d slope=%0.6f rise=%0.6f, idouble=%0.6f", 
                   sampleIndex, per, cnt,  samples[1], interpValue, samples[2], slope, rise, interpDouble);
}