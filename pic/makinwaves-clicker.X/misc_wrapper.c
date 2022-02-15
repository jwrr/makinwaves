
#include <stdint.h>
#include <stdbool.h>
#include <ctype.h>
#include <string.h>

void spin(uint32_t cnt)
{
    while (cnt--);
}

uint16_t getTableLen16(const int16_t *table)
{
    uint16_t maxLen = 256;
    uint16_t len = 0;
    for (; len < maxLen; len++)
    {
        if (table[len] == -1)
        {
            return len+1;
        }
    }
    return maxLen;
}

bool isAllDigits(const char *s)
{
    uint16_t i = 0;
    if (s[0] == '\0') return false;
    for (i=0; s[i] != '\0'; i++)
    {
        if (!isdigit(s[i])) return false;
    }
    return true;
}

bool isAllHex(const char *s)
{
    uint16_t i = 0;
    if (s[0] == '\0') return false;
    for (i=0; s[i] != '\0'; i++)
    {
        if (!isxdigit(s[i])) return false;
    }
    return true;
}

bool isInteger(const char *s)
{
    if (*s == '-') {
        s++;
    }
    return isAllDigits(s);
}

bool isHex(const char *s)
{
    if (*s++ != '0') return false;
    if (tolower(*s++) != 'x') return false;
    return isAllHex(s);
}

int stringInArray(const char *str, const char *strArray[], int len)
{
    int i = 0;
    for (; i < len; i++)
    {
        if (strcmp(str, strArray[i]) == 0)
        {
            return i;
        }
    }
    return -1;
}

char *trim(char *str)
{
    if (str == NULL) return NULL;
    char *trimmedStr = str;
    while (isspace(*trimmedStr)) trimmedStr++;
    uint16_t len = strlen(trimmedStr);
    if (len > 0)
    {
        char *endpos = &(trimmedStr[len-1]);
        while (isspace(*endpos))
        {
            *endpos = '\0';
            endpos--;
        }
    }
    return trimmedStr;
}

void stringToLower(char *str)
{
    while (*str != '\0')
    {
        *str = tolower(*str);
        str++;
    }
}

int32_t largest(int32_t a, int32_t b)
{
    return (a > b) ? a : b;
}

int32_t smallest(int32_t a, int32_t b)
{
    return (a < b) ? a : b;
}

int32_t clamp(int32_t val, int32_t min, int32_t max)
{
    if (val < min)
    {
        val = min;
    }
    else if (val > max)
    {
        val = max;
    }
    return val;
}
