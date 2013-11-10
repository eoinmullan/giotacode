#include "EMUtils.h"
#include <Windows.h>
#include <string>

emu64 EMUtils::cpuTimerStart;
long long EMUtils::cpuFrequency;

void EMUtils::StartTimer()
{
	LARGE_INTEGER li;
	if (!QueryPerformanceFrequency(&li)) {
		throw std::string("Failed to query performance counter");
	}
	cpuFrequency = li.QuadPart;

	if (!QueryPerformanceCounter(&li)) {
		throw std::string("Failed to query performance counter");
	}
	cpuTimerStart = li.QuadPart;
}

emu64 EMUtils::GetLapTimeMs()
{
	LARGE_INTEGER li;
	if (!QueryPerformanceCounter(&li)) {
		throw std::string("Failed to query performance counter");
	}
	return ((li.QuadPart - cpuTimerStart)*1000)/cpuFrequency;
}

double EMUtils::GetLapTimeS()
{
	LARGE_INTEGER li;
	if (!QueryPerformanceCounter(&li)) {
		throw std::string("Failed to query performance counter");
	}
	return static_cast<double>((li.QuadPart - cpuTimerStart)/static_cast<double>(cpuFrequency));
}

void EMUtils::Sleep(int timeMs)
{
	::Sleep(timeMs);
}