/* 
 * File:   dac_wrapper.h
 *
 */

#ifndef DAC_WRAPPER_H
#define	DAC_WRAPPER_H

//bool DAC_write(bool start, uint16_t value);

void DAC_SPI2_initialize(void);
void DAC_writeSPI(bool start, uint16_t value, uint8_t dacSel);

void DAC_setTestInProgress(bool testInProgress);
bool DAC_isTestInProgress(void);
uint8_t DAC_getActiveDAC(void);
uint8_t DAC_getNumDACs(void);
void DAC_driveActiveDAC(uint16_t v);
void DAC_turnAllDACsOff();
void DAC_setFreqForMode();


#endif	/* DAC_WRAPPER_H */

