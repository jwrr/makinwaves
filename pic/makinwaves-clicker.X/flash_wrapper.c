/*
    (c) 2020 Microchip Technology Inc. and its subsidiaries. You may use this
    software and any derivatives exclusively with Microchip products.

    THIS SOFTWARE IS SUPPLIED BY MICROCHIP "AS IS". NO WARRANTIES, WHETHER
    EXPRESS, IMPLIED OR STATUTORY, APPLY TO THIS SOFTWARE, INCLUDING ANY IMPLIED
    WARRANTIES OF NON-INFRINGEMENT, MERCHANTABILITY, AND FITNESS FOR A
    PARTICULAR PURPOSE, OR ITS INTERACTION WITH MICROCHIP PRODUCTS, COMBINATION
    WITH ANY OTHER PRODUCTS, OR USE IN ANY APPLICATION.

    IN NO EVENT WILL MICROCHIP BE LIABLE FOR ANY INDIRECT, SPECIAL, PUNITIVE,
    INCIDENTAL OR CONSEQUENTIAL LOSS, DAMAGE, COST OR EXPENSE OF ANY KIND
    WHATSOEVER RELATED TO THE SOFTWARE, HOWEVER CAUSED, EVEN IF MICROCHIP HAS
    BEEN ADVISED OF THE POSSIBILITY OR THE DAMAGES ARE FORESEEABLE. TO THE
    FULLEST EXTENT ALLOWED BY LAW, MICROCHIP'S TOTAL LIABILITY ON ALL CLAIMS IN
    ANY WAY RELATED TO THIS SOFTWARE WILL NOT EXCEED THE AMOUNT OF FEES, IF ANY,
    THAT YOU HAVE PAID DIRECTLY TO MICROCHIP FOR THIS SOFTWARE.

    MICROCHIP PROVIDES THIS SOFTWARE CONDITIONALLY UPON YOUR ACCEPTANCE OF THESE
    TERMS.
*/

#include "mcc_generated_files/memory/flash.h"
#include "flash_wrapper.h"
#include "usb_wrapper.h"
#include "factory_defaults.h"

static const uint8_t FLASH_flashNumPages = 10;

// Allocate and reserve pages of flash.  The compiler/linker will reserve this for data and not place any code here.
static __prog__  uint8_t flashPage0[FLASH_ERASE_PAGE_SIZE_IN_PC_UNITS] __attribute__((space(prog),aligned(FLASH_ERASE_PAGE_SIZE_IN_PC_UNITS)));

static uint32_t FLASH_getPageAddress(uint32_t flashPageNumber)
{
    switch (flashPageNumber)
    {
    case 0:  return FLASH_GetErasePageAddress((uint32_t)&flashPage0[0]);
    }
    return 0;
}

uint32_t FLASH_getNumPages()
{
    return FLASH_flashNumPages;
}

uint8_t FLASH_erasePage(uint8_t PageNumber)
{
    uint32_t flashPageBaseAddress = FLASH_getPageAddress(PageNumber);
    bool success = FLASH_ErasePage(flashPageBaseAddress);
    return success ? 0 : 1;
}

uint8_t FLASH_writePage(int32_t writeData[], uint32_t dataSize, uint8_t pageNumber)
{
    uint8_t  successCode = 0;
    uint8_t  errorCode = successCode;
    uint32_t flashPageBaseAddress = FLASH_getPageAddress(pageNumber);
    
    // Get flash page aligned address of flash reserved above for this test.
    if (flashPageBaseAddress == 0)
    {
        errorCode = 1; // invalid NULL address
        return errorCode;
    }

    FLASH_Unlock(FLASH_UNLOCK_KEY);
    
    bool success = FLASH_ErasePage(flashPageBaseAddress);
    if (!success)
    {
        errorCode = 2; // failed to erase page
        return errorCode;
    }
  
    dataSize = (dataSize < FLASH_PAGESIZE) ? dataSize : FLASH_PAGESIZE;
    uint32_t iii = 0;
//    for (flashOffset= 0U; flashOffset< FLASH_ERASE_PAGE_SIZE_IN_PC_UNITS; flashOffset += 4U)
    uint32_t flashOffset;
    for (flashOffset= 0U; flashOffset < 2*dataSize; flashOffset += 4U)
    {
        uint32_t d0 = writeData[iii++];
        uint32_t d1 = writeData[iii++];
        uint32_t flashAddress = flashPageBaseAddress+flashOffset;
        success = FLASH_WriteDoubleWord24(flashAddress, d0, d1);
        if (!success)
        {
            errorCode = 3; // failed writing to flash
            return errorCode;
        }   
    }
    errorCode = successCode;
    return errorCode;
}

int32_t FLASH_readFromPage(uint8_t pageNumber, uint16_t pageOffset)
{
    pageOffset *= 2;
    if (pageOffset > FLASH_PAGESIZE) return -1;
    const uint32_t flashPageBaseAddress = FLASH_getPageAddress(pageNumber);
    if (flashPageBaseAddress == 0) return -1;
    const uint32_t flashAddress = flashPageBaseAddress + pageOffset;
    int32_t readData = FLASH_ReadWord24(flashAddress);
    return readData;
}

bool FLASH_isValid(int8_t pageNumber, uint16_t pageOffset)
{
    // value must be between 0x10000 and 0x17fff)
    uint32_t value = FLASH_readFromPage(pageNumber, pageOffset);
    uint32_t minFlashValue = FLASH_VALID_BIT;
    uint32_t maxFlashValue = minFlashValue + 0xffff;
    bool valid = (minFlashValue <= value) && (value <= maxFlashValue);
    return valid;
}

int16_t FLASH_readSample(uint8_t pageNumber, uint16_t pageOffset)
{
    bool isPageProgrammed = FLASH_isValid(pageNumber, 0);
    int16_t val16 = -1;
    if (isPageProgrammed == true)
    {
        int32_t val32 = FLASH_readFromPage(pageNumber, pageOffset);
        val16 = (int16_t)(val32 & 0xffff);
    }
    else
    {
        val16 = WAVE_getFactoryDefault(pageNumber, pageOffset);
    }
    return val16;
}

int32_t FLASH_read32(uint8_t pageNumber, uint16_t pageOffset)
{
    bool isPageProgrammed = FLASH_isValid(pageNumber, 0);
    int16_t val32 = -1;
    if (isPageProgrammed == true)
    {
        val32 = FLASH_readFromPage(pageNumber, pageOffset);
    }
    else
    {
        val32 = (int32_t)WAVE_getFactoryDefault(pageNumber, pageOffset);
    }
    return val32;
}

bool FLASH_isStartLoop(int8_t pageNumber, uint16_t pageOffset)
{
    int32_t val32 = FLASH_read32(pageNumber, pageOffset);
    bool isStartLoop = (val32 & 0xF000) == FLASH_CMD_FOR;
    return isStartLoop;
}

bool FLASH_isEndLoop(int8_t pageNumber, uint16_t pageOffset)
{
    int32_t val32 = FLASH_read32(pageNumber, pageOffset);
    bool isEndLoop = (val32 & 0xF000) == FLASH_CMD_ENDFOR;
    return isEndLoop;
}

int32_t FLASH_read(uint32_t flashOffset)
{
    const uint8_t pageNumber = flashOffset / FLASH_PAGESIZE;
    const uint16_t pageOffset = 2 * (flashOffset % FLASH_PAGESIZE);
    int32_t readData = FLASH_readFromPage(pageNumber, pageOffset);
    return readData;
}

uint8_t FLASH_verify(const int32_t dataTable[], uint16_t len, uint8_t pageNumber)
{
    int32_t flashRdata;
    uint16_t failCnt = 0;
    uint16_t i = 0;
    uint16_t pageBase = pageNumber * FLASH_PAGESIZE;
    for (; i < len; i++)
    {
        int raddr = pageBase + i;
        flashRdata = FLASH_read(raddr);
        bool fail = (flashRdata != dataTable[i]);
        if (fail)
        {
            failCnt++;
            if (failCnt < 10)
            {
                int16_t upper = flashRdata >> 16;
                int16_t lower = flashRdata & 0xffff;
                USB_printfLine("Fail: addr = %d, actual = %04x_%04x expected = %04x", raddr, upper, lower, (int)dataTable[i]);
            }
            else if (failCnt == 11)
            {
                USB_printLine("Fail again: fail message disabled...");
            }
        }
    }
    return (failCnt > 0) ? 1 : 0;
}


uint8_t FLASH_test()
{
    int32_t testData[FLASH_PAGESIZE];


    int pageNumber = 0;
    uint8_t err = 0;
    for (; pageNumber<9; pageNumber++)
    {
        int i;
        for (i=0; i < FLASH_PAGESIZE; i++)
        {
            testData[i] = (pageNumber*FLASH_PAGESIZE + i) | FLASH_VALID_BIT;
        }
        err = FLASH_writePage(testData, FLASH_PAGESIZE, pageNumber);
        if (err)
        {
            USB_printfLinePrompt("FLASH WRITE ERROR #%d", err);
            return 1;
        }
    }
    USB_printLine("FLASH WRITE done");
    
    for (pageNumber=0; pageNumber<9; pageNumber++)
    {
        int i;
        for (i=0; i < FLASH_PAGESIZE; i++)
        {
            testData[i] = (pageNumber*FLASH_PAGESIZE + i) | FLASH_VALID_BIT;
        }
        err = FLASH_verify(testData, FLASH_PAGESIZE, pageNumber);
        if (err)
        {
            USB_printLinePrompt("FLASH READ Test failed");
            return 2;
        }
    }
    USB_printLine("FLASH READ done");

    for (pageNumber=0; pageNumber<9; pageNumber++)
    {
        err = FLASH_erasePage(pageNumber);
        if (err)
        {
            USB_printLinePrompt("FLASH ERASE failed");
            return 3;
        }
        int i;
        for (i=0; i < FLASH_PAGESIZE; i++)
        {
            testData[i] = 0x00ffffff;
        }
        err = FLASH_verify(testData, FLASH_PAGESIZE, pageNumber);
        if (err)
        {
            USB_printLinePrompt("FLASH ERASE failed");
            return 4;
        }
    }        

    USB_printLinePrompt("FLASH WRITE/READ/ERASE Test passed");
    return 0;
}    
    
void FLASH_memcpy16to32(int32_t dest32[], int16_t src16[], uint16_t len, int32_t upperWordVal)
{
    int i = 0;
    for (; i < len; i++)
    {
        dest32[i] = ((int32_t)src16[i]) | (upperWordVal << 16);
    }
}


#define  FLASH_loadBufferSize 256
static int32_t FLASH_loadBuffer[FLASH_loadBufferSize];
uint16_t FLASH_indexLoadBuffer = 0;
void FLASH_clearLoadBuffer()
{
    FLASH_indexLoadBuffer = 0;
}

uint16_t FLASH_appendLoadBuffer(uint32_t value)
{
    FLASH_loadBuffer[FLASH_indexLoadBuffer++] = (value | FLASH_VALID_BIT);
    return FLASH_indexLoadBuffer;
}

uint16_t FLASH_appendStartLoop(uint16_t loopCnt)
{
    FLASH_loadBuffer[FLASH_indexLoadBuffer++] = (loopCnt | FLASH_CMD_FOR);
    return FLASH_indexLoadBuffer;
}

uint16_t FLASH_appendEndLoop(void)
{
    FLASH_loadBuffer[FLASH_indexLoadBuffer++] = FLASH_CMD_ENDFOR;
    return FLASH_indexLoadBuffer;
}

uint16_t FLASH_getLoaddBufferLen()
{
    return FLASH_indexLoadBuffer;
}

uint32_t FLASH_getLoadBuffer(uint16_t i)
{
    return FLASH_loadBuffer[FLASH_indexLoadBuffer];
}

uint8_t FLASH_writeLoadBuffer(uint8_t pageNumber)
{    
    uint8_t err = FLASH_writePage(FLASH_loadBuffer, (uint32_t)FLASH_indexLoadBuffer, pageNumber);
    FLASH_indexLoadBuffer = 0;
    return err;
}


