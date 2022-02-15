/* 
 * File:   flash_wrapper.h
 * 
 */

#ifndef FLASH_WRAPPER_H
#define	FLASH_WRAPPER_H

uint32_t FLASH_getPageSize();
uint32_t FLASH_getNumPages();
uint8_t FLASH_erasePage(uint8_t PageNumber);
uint8_t FLASH_writePage(int32_t writeData[], uint32_t dataSize, uint8_t pageNumber);
int32_t FLASH_readFromPage(uint8_t pageNumber, uint16_t pageOffset);
int16_t FLASH_readSample(uint8_t pageNumber, uint16_t pageOffset);
bool FLASH_isValid(int8_t pageNumber, uint16_t pageOffset);
bool FLASH_isStartLoop(int8_t pageNumber, uint16_t pageOffset);
bool FLASH_isEndLoop(int8_t pageNumber, uint16_t pageOffset);
int32_t FLASH_read(uint32_t flashOffset);
uint8_t FLASH_verify(const int32_t dataTable[], uint16_t len, uint8_t pageNumber);
uint8_t FLASH_test();

void FLASH_memcpy16to32(int32_t dest32[], int16_t src16[], uint16_t len, int32_t upperWordVal);

void FLASH_clearLoadBuffer();
uint16_t FLASH_appendLoadBuffer(uint32_t value);
uint16_t FLASH_appendStartLoop(uint16_t loopCnt);
uint16_t FLASH_appendEndLoop(void);

uint16_t FLASH_getLoaddBufferLen();
uint32_t FLASH_getLoadBuffer(uint16_t i);
uint8_t FLASH_writeLoadBuffer(uint8_t pageNumber);

#endif	/* FLASH_WRAPPER_H */

