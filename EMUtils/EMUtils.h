#pragma once

#include "MathUtils.h"

class EMUtils
{
public:
	static void StartTimer();
	static emu64 GetTimeMs();
	static double GetTimeS();

private:
	static emu64 cpuTimerStart;
	static long long cpuFrequency;
};

