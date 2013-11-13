#include "EMTimer.h"
#include <Windows.h>
#include <string>

emu64 EMTimer::timerStart = 0;
long long EMTimer::cpuFrequency = 1;
emu64 EMTimer::timeAccumulatedBeforeStart = 0;
bool EMTimer::running = false;

void EMTimer::Start()
{
	LARGE_INTEGER freq;
	if (!QueryPerformanceFrequency(&freq)) {
		throw std::string("Failed to query performance counter");
	}
	cpuFrequency = freq.QuadPart;
	
	if (!running) {
		timerStart = currentClockTicks();
	}
	running = true;
}

void EMTimer::Stop()
{
	timeAccumulatedBeforeStart += currentClockTicks() - timerStart;
	running = false;
}

void EMTimer::Reset()
{
	timeAccumulatedBeforeStart = 0;
	timerStart = currentClockTicks();
}

emu64 EMTimer::GetLapTimeMs()
{
	if (running) {
		return ((currentClockTicks() - timerStart + timeAccumulatedBeforeStart)*1000)/cpuFrequency;
	}
	else {
		return ((timeAccumulatedBeforeStart)*1000)/cpuFrequency;
	}
}

double EMTimer::GetLapTimeS()
{
	if (running) {
		return static_cast<double>((currentClockTicks() - timerStart + timeAccumulatedBeforeStart)/static_cast<double>(cpuFrequency));
	}
	else {
		return static_cast<double>((timeAccumulatedBeforeStart)/static_cast<double>(cpuFrequency));
	}
}

inline emu64 EMTimer::currentClockTicks()
{
	LARGE_INTEGER currentTime;
	if (!QueryPerformanceCounter(&currentTime)) {
		throw std::string("Failed to query performance counter");
	}
	return currentTime.QuadPart;
}