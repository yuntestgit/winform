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
         // TODO: �b���[�J�o�����O����k�C
         public:
             double fun1(double Balance, double InterestRate, string str)
             {

                 return Balance*InterestRate/100;
             }
     };
}