
#include <stdint.h>
#include <stdbool.h>
#include "gpio_wrapper.h"
#include "timer_wrapper.h"
#include "mcc_generated_files/spi2.h"

// ===============================================================

void DAC_SPI2_initialize(void)
{
    // AUDEN disabled; FRMEN disabled; AUDMOD I2S; FRMSYPW One clock wide; AUDMONO stereo; FRMCNT 0; MSSEN disabled; FRMPOL disabled; IGNROV disabled; SPISGNEXT not sign-extended; FRMSYNC disabled; URDTEN disabled; IGNTUR disabled; 
    SPI2CON1H = 0x00;
    // WLENGTH 0; 
    SPI2CON2L = 0x00;
    // SPIROV disabled; FRMERR disabled; 
    SPI2STATL = 0x00;
    // SPI2BRGL 0; 
    SPI2BRGL = 0x07; // Baud Rate Generator. MCC set this to 0x00; Changed it to 7 to match display
    // SPITBFEN disabled; SPITUREN disabled; FRMERREN disabled; SRMTEN disabled; SPIRBEN disabled; BUSYEN disabled; SPITBEN disabled; SPIROVEN disabled; SPIRBFEN disabled; 
    SPI2IMSKL = 0x00;
    // RXMSK 0; TXWIEN disabled; TXMSK 0; RXWIEN disabled; 
    SPI2IMSKH = 0x00;
    // SPI2URDTL 0; 
    SPI2URDTL = 0x00;
    // SPI2URDTH 0; 
    SPI2URDTH = 0x00;
    // SPIEN enabled; DISSDO disabled; MCLKEN FOSC/2; CKP Idle:Low, Active:High; SSEN disabled; MSTEN Master; MODE16 enabled; SMP Middle; DISSCK disabled; SPIFE Frame Sync pulse precedes; CKE Idle to Active; MODE32 disabled; SPISIDL disabled; ENHBUF enabled; DISSDI disabled; 
    SPI2CON1L = 0x8421;  
    
//    // SPI2BRGL 7; 
//    SPI2BRGL = 0x07;
//    // SPIEN enabled; DISSDO disabled; MCLKEN FOSC/2; CKP Idle:Low, Active:High; SSEN disabled; MSTEN Master; MODE16 disabled; SMP Middle; DISSCK disabled; SPIFE Frame Sync pulse precedes; CKE Active to Idle; MODE32 disabled; SPISIDL disabled; ENHBUF enabled; DISSDI disabled; 
//    SPI2CON1L = 0x8121;
}

// DAC121S101
void DAC_writeSPI(bool start, uint16_t value, uint8_t dacSel)
{
    uint16_t rdata;
    uint16_t i = 0;
    if (start)
    {
        while (SPI2STATLbits.SPITBF == true);
        DAC_SPI2_initialize(); // Mode 0, 16-Bit

        if (dacSel == 1)
        {
            GPIO_set(GPIO_DAC1_CS, 1);
            for (i = 0; i < 10; i++);
            GPIO_set(GPIO_DAC1_CS, 0);
            for (i = 0; i < 10; i++);
        }
        else
        {
             GPIO_set(GPIO_DAC2_CS, 1);
            for (i = 0; i < 10; i++);
            GPIO_set(GPIO_DAC2_CS, 0);
            for (i = 0; i < 10; i++);           
        }

        uint16_t valueScaled = value / 3; // scale to 100ma max (~1300)
        SPI2BUFL = valueScaled;
        uint16_t cnt = 0;
        while (SPI2STATLbits.SPIRBE == true) cnt++;
        rdata = SPI2BUFL;     // sets SPIRBE
        SPI2_Initialize(); // Mode 1, 8-bit
    }
}

bool DAC_testInProgress = false;

void DAC_setTestInProgress(bool testInProgress)
{
    DAC_testInProgress = testInProgress;
}

bool DAC_isTestInProgress(void)
{
    return DAC_testInProgress;
}

uint8_t DAC_getActiveDAC(void)
{
    return 1;
}

uint8_t DAC_getNumDACs(void)
{
    return 1;
}

void DAC_driveActiveDAC(uint16_t v)
{
    uint8_t activeDAC = DAC_getActiveDAC();
    DAC_writeSPI(true, v, activeDAC);
}

void DAC_turnAllDACsOff()
{
    uint8_t numDACs = DAC_getNumDACs();
    uint8_t i;
    for (i = 1; i <= numDACs; i++)
    {
        DAC_writeSPI(true, 0, i);
    }
}

void DAC_setFreqForMode()
{
    TIME_setFreq(2000.0);
}