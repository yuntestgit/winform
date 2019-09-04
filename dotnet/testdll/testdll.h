// testdll.h

#pragma once  
  
#ifdef TEST_EXPORTS  
#define TEST_API __declspec(dllexport)  
#else  
#define TEST_API __declspec(dllimport)  
#endif  
  
class TEST_API testdll
{  
public:  
    int testfun(int a, int b);
};  