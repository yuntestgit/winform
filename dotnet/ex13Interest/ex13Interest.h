// ex13Interest.h

#pragma once

#include "stdafx.h"

#include <iostream>
#include <cctype>
#include <cstdio>
#include <string>
#include <sstream>
#include <string.h>
#define f 50

using namespace std;

using namespace System;

namespace ex13Interest {
	
    public ref class lib
     {
         // TODO: 在此加入這個類別的方法。
         public:
             double fun1(double Balance, double InterestRate, string str)
             {

                 return Balance*InterestRate/100;
             }
     };
}