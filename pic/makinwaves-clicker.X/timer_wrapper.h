/* 
 * File:   timer_wrapper.h
 * 
 */

#ifndef TIMER_WRAPPER_H
#define	TIMER_WRAPPER_H

#include <stdint.h>
#include <stdbool.h>

void TMR3_callBack(void);

bool TIME_isTimeMonitorEnabled(void);
void TIME_enableTimeMonitor(bool newVal);

void TIME_clearCount();
uint16_t TIME_getCnt500usec();


void TIME_setPeriod(uint16_t newPeriod);
void TIME_setFreq(double newFreqInHz);
uint16_t TIME_getPeriod(void);
void TIME_setCounter(uint16_t newCnt);
uint16_t TIME_getCounter(void);
uint16_t TIME_getCurrentSampleIndex(void);
void TIME_interp();


#endif	/* TIMER_WRAPPER_H */

