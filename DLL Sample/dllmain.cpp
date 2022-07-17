#include "pch.h"

void Main(HMODULE hModule)
{
    MessageBox(nullptr, TEXT("Hello, World!"), TEXT("Injected"), MB_OK);
    
    while (true)
    {
        if (GetAsyncKeyState(VK_END) & 1)
        {
            break;
        }

        Sleep(50);
    }

    FreeLibraryAndExitThread(hModule, 0);
}

BOOL APIENTRY DllMain(HMODULE hModule, DWORD  ul_reason_for_call, LPVOID lpReserved)
{
    switch (ul_reason_for_call)
    {
    case DLL_PROCESS_ATTACH:
    {
        HANDLE threadHandle = CreateThread(nullptr, 0, (LPTHREAD_START_ROUTINE)Main, hModule, 0, nullptr);
        if (threadHandle)
        {
            CloseHandle(threadHandle);
        }
    }
    case DLL_THREAD_ATTACH:
    case DLL_THREAD_DETACH:
    case DLL_PROCESS_DETACH:
        break;
    }
    return TRUE;
}
