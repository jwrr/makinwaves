#include <stdlib.h>
#include <stdint.h>
#include <stdbool.h>
#include <string.h>
#include "dac_wrapper.h"
#include "flash_wrapper.h"
#include "help_wrapper.h"
#include "misc_wrapper.h"
#include "timer_wrapper.h"
#include "usb_wrapper.h"
#include "version.h"

static bool CMD_loadInProgress = false;

void CMD_interpreter(char *rxLine)
{
    char cmdStr[USB_LINELEN] = "";
    if (rxLine[0] != '\0')
    {
        strncpy(cmdStr, rxLine, sizeof(cmdStr)-1);
        cmdStr[sizeof(cmdStr)-1] = '\0';
    }
    // Process command line
    if (USB_getEchoCommand() == true)
    {
        USB_printEOL();
        USB_printfLine("cmd = '%s'", cmdStr);
    }
    
    char *trimmedCmdStr = trim(cmdStr);
    stringToLower(trimmedCmdStr);
    if (strncmp(trimmedCmdStr,"dac ",4) == 0)
    {
        uint16_t v = (uint16_t)strtol(&(trimmedCmdStr[4]), NULL, 10);
        bool testInProgress = (v > 0);
        DAC_setTestInProgress(testInProgress);
        uint8_t mode = 0;
        uint8_t dacSel = (mode < 3) ? 1 : 2;
        DAC_turnAllDACsOff();
        DAC_driveActiveDAC(v);
        USB_printfLine("Driving DAC%d to %d (dscSel based on mode", dacSel, v);
    }
    else if (strcmp(trimmedCmdStr,"f") == 0      ||
             strcmp(trimmedCmdStr, "play") == 0)
    {
        TIME_setUSBPressed( !TIME_isUSBPressed() );
        if (TIME_isUSBPressed())
        {
            USB_printLine("Play pressed");
        }
        else
        {
            USB_printLine("Play released");
        }
    }
    else if (strcmp(trimmedCmdStr,"e") == 0 ||
             strcmp(trimmedCmdStr, "erase") == 0)
    {
        FLASH_erasePage(0);
        USB_printfLine("Erasing Waveform from flash");
    }
    else if (strcmp(trimmedCmdStr,"h") == 0 ||
             strcmp(trimmedCmdStr, "help") == 0)
    {
        USB_printLine(HELP_getHelpMenu());
    }
    else if (strcmp(trimmedCmdStr,"version") == 0)
    {
        USB_printfLine("PN: %s, FW: %s, Build: %s, Date: %s", VERSION_PN, VERSION_FW, VERSION_BUILD, VERSION_DATE);
    }
    else if (strcmp(trimmedCmdStr,"w") == 0 ||
             strcmp(trimmedCmdStr, "write") == 0)
    { // write Wave
        FLASH_clearLoadBuffer();
        CMD_loadInProgress = true;
        USB_printLine("Loading waveform into flash");
        USB_printLine("Enter one value per line. Enter 'end' to end");
    }
    else if (CMD_loadInProgress)
    {
        if (strcmp(trimmedCmdStr, "-1") == 0 || 
            strcmp(trimmedCmdStr, "endwave") == 0)
        {
            CMD_loadInProgress = false;
            FLASH_appendLoadBuffer(-1);
            uint8_t pageNumber = 0;
            uint16_t len = FLASH_getLoaddBufferLen();
            FLASH_writeLoadBuffer(pageNumber);
            len = FLASH_getLoaddBufferLen();
            USB_printLine("Wave is in flash");
            USB_printWaveSize();
        }
        else if (isInteger(trimmedCmdStr))
        {
            int16_t loadValue = (int16_t)strtol(trimmedCmdStr, NULL, 10);
            FLASH_appendLoadBuffer((uint32_t)loadValue);
        }
        else if (isHex(trimmedCmdStr))
        {
            int16_t loadValue = (int16_t)strtol(trimmedCmdStr, NULL, 0);
            FLASH_appendLoadBuffer((uint32_t)loadValue);
        }
        else if (strncmp(trimmedCmdStr, "rep", 3) == 0)
        {
            uint16_t len = strlen(trimmedCmdStr);
            if (len > 3+1)
            {
                int16_t repCnt = (int16_t)strtol(trimmedCmdStr+3+1, NULL, 10);
                int16_t repCntNegative = -1 * repCnt;
                FLASH_appendLoadBuffer((uint32_t)repCntNegative);
                USB_printfLine("REP %d", repCnt);
            }
        }
        else if (strcmp(trimmedCmdStr, "forever") == 0)
        {
            FLASH_appendLoadBuffer(FLASH_CMD_FOR);
            USB_printLine("FOREVER");
        }
        else if (strncmp(trimmedCmdStr, "for", 3) == 0)
        {
            uint16_t len = strlen(trimmedCmdStr);
            if (len > 3+1)
            {
                int32_t loopCnt = (int32_t)strtol(trimmedCmdStr+3+1, NULL, 10);
                loopCnt = clamp(loopCnt, 0, 4095);
                FLASH_appendLoadBuffer(FLASH_CMD_FOR + loopCnt);
                USB_printfLine("FOR %d", (int16_t)loopCnt);
            }
        }
        else if (strcmp(trimmedCmdStr, "endfor") == 0)
        {
            FLASH_appendEndLoop();
            USB_printLine("ENDFOR");
        }
    } // CMD_loadInProgress
    else if (strncmp(trimmedCmdStr, "freq", 4) == 0)
    {
        uint16_t len = strlen(trimmedCmdStr);
        if (len > 4+1)
        {
            int16_t freq = (int16_t)strtol(trimmedCmdStr+5, NULL, 10);
            TIME_setFreq(freq);
            USB_printfLine("Sample frequency changed to %d", freq);
        }            
    }
    else if (strcmp(trimmedCmdStr, "r") == 0  ||
             strcmp(trimmedCmdStr, "read") == 0) // read wave
    {
        USB_printWave();
    }
    else
    {
        USB_printfLine("Undefined command '%s'",  trimmedCmdStr); 
    }
    USB_printPrompt();
}

#define CMD_rxLineLen  256
static char CMD_rxLine[CMD_rxLineLen];
void CMD_processCommand()
{
    bool eoln = USB_readLine(CMD_rxLine, CMD_rxLineLen, USB_getEchoChar());
    if (eoln) {
        CMD_interpreter(CMD_rxLine);
        CMD_rxLine[0] = '\0';
    }
}
