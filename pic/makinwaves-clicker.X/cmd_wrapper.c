#include <stdlib.h>
#include <stdint.h>
#include <stdbool.h>
#include <string.h>
#include "dac_wrapper.h"
#include "flash_wrapper.h"
#include "help_wrapper.h"
#include "misc_wrapper.h"
#include "timer_wrapper.h"
#include "trigger_wrapper.h"
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
        TRIG_setUSBPressed( !TRIG_isUSBPressed() );
        if (TRIG_isUSBPressed())
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
            strcmp(trimmedCmdStr, "end") == 0)
        {
            CMD_loadInProgress = false;
            FLASH_appendLoadBuffer(-1);
            uint8_t pageNumber = 0;
            uint16_t len = FLASH_getLoaddBufferLen();
            USB_printfLine("Load Buffer final len = %d", len); 
            FLASH_writeLoadBuffer(pageNumber);
            len = FLASH_getLoaddBufferLen();
            USB_printfLine("Waveform uploaded to flash memory (len=%d)", len);
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
        else if (strncmp(trimmedCmdStr, "delay_ms", 8) == 0)
        {
            uint16_t len = strlen(trimmedCmdStr);
            if (len > 8+1)
            {
                int16_t delayInMsec = (int16_t)strtol(trimmedCmdStr+9, NULL, 10);
                int16_t delayIn500usec = 2 * delayInMsec;
                int16_t delayNegative = -1 * delayIn500usec;
                FLASH_appendLoadBuffer((uint32_t)delayNegative);
                USB_printfLine("Value %d written to Flash", delayNegative);
            }
        }
        else if (strncmp(trimmedCmdStr, "loop", 4) == 0)
        {
            uint16_t len = strlen(trimmedCmdStr);
            if (len > 4+1)
            {
                int16_t loopCnt = (int16_t)strtol(trimmedCmdStr+5, NULL, 10);
                loopCnt = clamp(loopCnt, 0, 4095);
                FLASH_appendStartLoop((uint32_t)loopCnt);
                USB_printfLine("Start loop of %d written to flash", loopCnt);
            }
        }
        else if (strcmp(trimmedCmdStr, "endloop") == 0)
        {
            FLASH_appendEndLoop();
            USB_printLine("End loop written to flash");
        }
    } // CMD_loadInProgress
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
void CMD_handleUSB()
{
    bool eoln = USB_readLine(CMD_rxLine, CMD_rxLineLen, USB_getEchoChar());
    if (eoln) {
        CMD_interpreter(CMD_rxLine);
        CMD_rxLine[0] = '\0';
    }
}
