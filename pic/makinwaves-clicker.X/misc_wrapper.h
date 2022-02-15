/* 
 * File:   helpers.h
 * 
 */

#ifndef MISC_WRAPPER_H
#define	MISC_WRAPPER_H


void spin(uint32_t cnt);
uint16_t getTableLen16(const int16_t *table);
bool isAllDigits(const char *s);
bool isInteger(const char *s);
bool isHex(const char *s);
int stringInArray(const char *str, const char *strArray[], int len);
char *trim(char *str);
void stringToLower(char *str);
int32_t largest(int32_t a, int32_t b);
int32_t smallest(int32_t a, int32_t b);
int32_t clamp(int32_t val, int32_t min, int32_t max);

#endif	/* MISC_WRAPPER_H */

