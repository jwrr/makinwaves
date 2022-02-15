/* 
 * File:   trigger_wrapper.h
 * 
 */

#ifndef TRIGGER_WRAPPER_H
#define	TRIGGER_WRAPPER_H

#include <stdbool.h>

void TRIG_handleTrigger(void);
bool TRIG_isSignalOn(void);
void TRIG_setSignalOnDelayed(bool val);
void TRIG_updateSignalOnDelayed();
bool TRIG_isSignalOnDelayed(void);
bool TRIG_isFirstCycle(void);

bool TRIG_isUSBPressed(void);
void TRIG_setUSBPressed(bool newVal);

#endif	/* TRIGGER_WRAPPER_H */

