#pragma once

#include "emvartypes.h"

class EMTimer
{
public:
	static void Start();
	static void Stop();
	static void Reset();
	static emu64 GetLapTimeMs();
	static double GetLapTimeS();

private:
	static emu64 currentClockTicks();

	static bool running;
	static emu64 timerStart;
	static long long cpuFrequency;
	static emu64 timeAccumulatedBeforeStart;
};

