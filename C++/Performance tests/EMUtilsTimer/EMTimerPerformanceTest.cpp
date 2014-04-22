// EMTimerTimer.cpp : Defines the entry point for the console application.
//

#include "tchar.h"
#include <iostream>
#include <sstream>
#include <Windows.h>
#include "EMTimer.h"
#include "MathUtils.h"

using namespace std;

void ShouldInitiallyReturnZero();
void ShouldGetLapTimeMs();
void ShouldGetLapTimeS();
void ShouldReset();
void ShouldStop();
void ShouldStopAndReStart();
void ShouldStopResetAndReStart();
void ShouldNotStartWhenAlreadyRunning();

// Helper methods
void sleepAndCheckLapTimeMsForSeriesOfIntervals(const vector<int>& sleepTimes, emu64 startingOffset = 0);
void sleepAndCheckTimeMs(int sleepForMs, emu64 expectedTimeMs, emu64 tolerance);
void sleepAndCheckTimeS(int sleepForMs, double expectedTimeS, double tolerance);
template <typename T> void checkTimeAndThrowOnMismatch(T elapsedTime, T expectedTime, T tolerance);

int gTolerance = 1;

int _tmain(int argc, _TCHAR* argv[])
{
	bool allTestsPass = true;

	cout << "Testing EMTimer timer functionality...\n\n";
	try {
		ShouldInitiallyReturnZero();
		ShouldGetLapTimeMs();
		ShouldGetLapTimeS();
		ShouldReset();
		ShouldStop();
		ShouldStopAndReStart();
		ShouldStopResetAndReStart();
		ShouldNotStartWhenAlreadyRunning();
	}
	catch (std::string e) {
		cout << "EMTimer timer test fail\n";
		cout << e.c_str() << endl;
		allTestsPass = false;
	}

	if (allTestsPass) {
		cout << "All tests passed\n\n";
	}
	else {
		cout << "Tests failure(s)\n\n";
	}

	system("pause");
	return 0;
}

void ShouldInitiallyReturnZero()
{
	checkTimeAndThrowOnMismatch(EMTimer::GetLapTimeMs(), static_cast<emu64>(0), static_cast<emu64>(gTolerance));
}

void ShouldGetLapTimeMs()
{
	cout << "Testing GetLapTimesMs()..." << flush;
	static const int arr[] = {50, 50, 100, 300, 500, 1000};
	vector<int> sleepTimes (arr, arr + sizeof(arr) / sizeof(arr[0]) );
	EMTimer::Start();
	sleepAndCheckLapTimeMsForSeriesOfIntervals(sleepTimes);
	sleepAndCheckLapTimeMsForSeriesOfIntervals(sleepTimes, 2000);
	EMTimer::Stop();
	EMTimer::Reset();
	cout << "Pass\n";
}

void ShouldGetLapTimeS()
{
	cout << "Testing GetLapTimeS()..." << flush;
	EMTimer::Start();
	sleepAndCheckTimeS(50, 0.05, 0.01);
	sleepAndCheckTimeS(50, 0.1, 0.01);
	sleepAndCheckTimeS(100, 0.2, 0.01);
	sleepAndCheckTimeS(300, 0.5, 0.01);
	sleepAndCheckTimeS(500, 1.0, 0.01);
	sleepAndCheckTimeS(1000, 2.0, 0.01);
	EMTimer::Stop();
	EMTimer::Reset();
	cout << "Pass\n";
}

void ShouldReset()
{
	cout << "Test Reset()..." << flush;
	static const int arr[] = {50, 50, 100, 300, 500, 1000};
	vector<int> sleepTimes (arr, arr + sizeof(arr) / sizeof(arr[0]) );
	EMTimer::Start();
	sleepAndCheckLapTimeMsForSeriesOfIntervals(sleepTimes);
	Sleep(200);
	EMTimer::Reset();
	sleepAndCheckLapTimeMsForSeriesOfIntervals(sleepTimes);
	EMTimer::Stop();
	EMTimer::Reset();
	cout << "Pass\n";
}

void ShouldStop()
{
	cout << "Test Stop()..." << flush;
	static const int arr[] = {50, 50, 100, 300, 500, 1000};
	vector<int> sleepTimes (arr, arr + sizeof(arr) / sizeof(arr[0]) );
	EMTimer::Start();
	sleepAndCheckLapTimeMsForSeriesOfIntervals(sleepTimes);
	EMTimer::Stop();
	checkTimeAndThrowOnMismatch(EMTimer::GetLapTimeMs(), static_cast<emu64>(2000), static_cast<emu64>(gTolerance));
	Sleep(200);
	checkTimeAndThrowOnMismatch(EMTimer::GetLapTimeMs(), static_cast<emu64>(2000), static_cast<emu64>(gTolerance));
	EMTimer::Stop();
	EMTimer::Reset();
	cout << "Pass\n";
}

void ShouldStopAndReStart()
{
	cout << "Test Stop and then Start..." << flush;
	EMTimer::Start();
	Sleep(200);
	EMTimer::Stop();
	checkTimeAndThrowOnMismatch(EMTimer::GetLapTimeMs(), static_cast<emu64>(200), static_cast<emu64>(gTolerance));
	Sleep(200);
	checkTimeAndThrowOnMismatch(EMTimer::GetLapTimeMs(), static_cast<emu64>(200), static_cast<emu64>(gTolerance));
	EMTimer::Start();
	Sleep(200);
	checkTimeAndThrowOnMismatch(EMTimer::GetLapTimeMs(), static_cast<emu64>(400), static_cast<emu64>(gTolerance));
	EMTimer::Stop();
	checkTimeAndThrowOnMismatch(EMTimer::GetLapTimeMs(), static_cast<emu64>(400), static_cast<emu64>(gTolerance));
	Sleep(200);
	checkTimeAndThrowOnMismatch(EMTimer::GetLapTimeMs(), static_cast<emu64>(400), static_cast<emu64>(gTolerance));
	EMTimer::Stop();
	EMTimer::Reset();
	cout << "Pass\n";
}

void ShouldStopResetAndReStart()
{
	cout << "Test Stop, Reset, and re-Start..." << flush;
	EMTimer::Start();
	Sleep(200);
	EMTimer::Stop();
	checkTimeAndThrowOnMismatch(EMTimer::GetLapTimeMs(), static_cast<emu64>(200), static_cast<emu64>(gTolerance));
	Sleep(200);
	checkTimeAndThrowOnMismatch(EMTimer::GetLapTimeMs(), static_cast<emu64>(200), static_cast<emu64>(gTolerance));
	EMTimer::Reset();
	checkTimeAndThrowOnMismatch(EMTimer::GetLapTimeMs(), static_cast<emu64>(0), static_cast<emu64>(gTolerance));
	EMTimer::Start();
	Sleep(200);
	checkTimeAndThrowOnMismatch(EMTimer::GetLapTimeMs(), static_cast<emu64>(200), static_cast<emu64>(gTolerance));
	EMTimer::Stop();
	checkTimeAndThrowOnMismatch(EMTimer::GetLapTimeMs(), static_cast<emu64>(200), static_cast<emu64>(gTolerance));
	Sleep(200);
	checkTimeAndThrowOnMismatch(EMTimer::GetLapTimeMs(), static_cast<emu64>(200), static_cast<emu64>(gTolerance));
	EMTimer::Stop();
	EMTimer::Reset();
	cout << "Pass\n";
}

void ShouldNotStartWhenAlreadyRunning()
{
	cout << "Test Start when running is ineffective..." << flush;
	EMTimer::Start();
	Sleep(200);
	checkTimeAndThrowOnMismatch(EMTimer::GetLapTimeMs(), static_cast<emu64>(200), static_cast<emu64>(gTolerance));
	EMTimer::Start();
	Sleep(200);
	checkTimeAndThrowOnMismatch(EMTimer::GetLapTimeMs(), static_cast<emu64>(400), static_cast<emu64>(gTolerance));
	EMTimer::Stop();
	EMTimer::Reset();
	cout << "Pass\n";
}

// Helper methods
void sleepAndCheckLapTimeMsForSeriesOfIntervals(const vector<int>& sleepTimes, emu64 startingOffset /* = 0 */)
{
	auto expectedTotalElapsedTime = startingOffset;
	for (int sleepTime : sleepTimes) {
		expectedTotalElapsedTime += sleepTime;
		sleepAndCheckTimeMs(sleepTime, expectedTotalElapsedTime, 10);
	}
}

void sleepAndCheckTimeMs(int sleepForMs, emu64 expectedTimeMs, emu64 tolerance)
{
	Sleep(sleepForMs);
	auto elapsedTimeMs = EMTimer::GetLapTimeMs();
	checkTimeAndThrowOnMismatch(elapsedTimeMs, expectedTimeMs, tolerance);
}

void sleepAndCheckTimeS(int sleepForMs, double expectedTimeS, double tolerance)
{
	Sleep(sleepForMs);
	auto elapsedTimeS = EMTimer::GetLapTimeS();
	checkTimeAndThrowOnMismatch(elapsedTimeS, expectedTimeS, 0.01);
}

template <typename T>
void checkTimeAndThrowOnMismatch(T elapsedTime, T expectedTime, T tolerance)
{
	if (!MathUtils::AreClose(elapsedTime, expectedTime, tolerance)) {
		stringstream ss("Unexpected time from timer. ");
		ss << "Elapsed time: " << elapsedTime;
		ss << ". Expected: " << expectedTime;
		throw ss.str();
	}
}