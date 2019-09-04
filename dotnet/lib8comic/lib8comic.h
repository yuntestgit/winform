// lib8comic.h

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

namespace lib8comic {

	public ref class lib
	{
		public:
		double CalInterest(double Balance, double InterestRate, string str)
		{
			return Balance*InterestRate/100;
		}

		
	};
}
