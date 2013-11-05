#pragma once

#include "MathUtils.h"

class EMUtils
{
public:
	static void StartTimer();
	static emu64 GetLapTimeMs();
	static double GetLapTimeS();

private:
	static emu64 cpuTimerStart;
	static long long cpuFrequency;
};

