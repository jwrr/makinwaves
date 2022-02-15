

#include <stdint.h>
#include "factory_defaults.h"


const int16_t WAVE_factoryDefault[NUM_WAVEFORMS][WAVEFORM_SIZE] = {
    {
        0, 4100, 0, 4099, 0, 6, 27, 60, 107, 166, 238, 320, 414, 517, 629, 749, 876, 1009, 
        1146, 1286, 1428, 1571, 1713, 1853, 1990, 2123, 2249, 2370, 2482, 
        2585, 2679, 2761, 2833, 2892, 2939, 2972, 2993, 3000, 2993, 2972, 
        2939, 2892, 2833, 2761, 2679, 2585, 2482, 2370, 2249, 2123, 1990, 
        1853, 1713, 1571, 1428, 1286, 1146, 1009, 876, 750, 629, 517, 414, 
        320, 238, 166, 107, 60, 27, 6, -40, 8192, -100, 8192, 0, -1
    }
};


int16_t WAVE_getFactoryDefault(uint8_t pageNumber, uint16_t pageOffset)
{
    return WAVE_factoryDefault[pageNumber][pageOffset];
}

