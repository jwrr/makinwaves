
#include <stdio.h>
#include <stdint.h>
#include <stdbool.h>
#include "gpio_wrapper.h"
#include "dac_wrapper.h"

static uint8_t  G_triggerCountDown = 0;
static uint8_t  G_triggerCountDownMax = 5;

static bool TRIG_signalOnFirstCycle = false;
static bool TRIG_signalOn           = false;
static bool TRIG_signalOnDelayed    = false;
static bool G_triggerPressedPrev    = false;
static bool USB_triggerPressed      = false;
static bool USB_triggerPressedPrev  = false;

bool TRIG_isSignalOn(void)
{
    return TRIG_signalOn;
}

void TRIG_updateSignalOnDelayed()
{
    TRIG_signalOnDelayed = TRIG_signalOn;
}

bool TRIG_isSignalOnDelayed()
{
    return TRIG_signalOnDelayed;
}

bool TRIG_isFirstCycle(void)
{
    return TRIG_signalOnFirstCycle;
}

bool TRIG_isUSBPressed(void)
{
    return USB_triggerPressed;
}

void TRIG_setUSBPressed(bool newVal)
{
    USB_triggerPressed = newVal;
}

void TRIG_handleTrigger(void)
{
    bool triggerVal      = GPIO_get(GPIO_TRIGGER_BUTTON);
    bool triggerPressed  = !triggerVal;
    bool triggerEdge     = !G_triggerPressedPrev && triggerPressed;
    G_triggerPressedPrev = triggerPressed;
    bool USB_triggerEdge = !USB_triggerPressedPrev && USB_triggerPressed;
    USB_triggerPressedPrev = USB_triggerPressed;
    TRIG_signalOnFirstCycle = false;
 
    bool singleShot = true;
    if (singleShot)
    {
        G_triggerCountDown = 0;
    }
    

    if (G_triggerCountDown == 0){
        if (triggerEdge || USB_triggerEdge)
        {
            DAC_setFreqForMode();
            TRIG_signalOnFirstCycle = true;
            TRIG_signalOn = true;
            TRIG_signalOnDelayed = false;
            G_triggerCountDown = G_triggerCountDownMax + 1;
        }
        else if (triggerPressed || USB_triggerPressed)
        {
            TRIG_signalOn = false;
            TRIG_signalOnDelayed = false;

        }
        else
        {
            TRIG_signalOn = false;
            TRIG_signalOnDelayed = false;
        }
    }
    else if ((triggerPressed || USB_triggerPressed))
    {
        G_triggerCountDown--;
    }
    else
    {
        DAC_turnAllDACsOff();
        TRIG_signalOn = false;
        G_triggerCountDown = 0;
        TRIG_signalOn = false;
        TRIG_signalOnDelayed = false;
    }
}
