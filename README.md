# CheetoInjector

## Description

💉 Injects unmanaged DLL into target process.

Awesome article about other injection techniques https://github.com/odzhan/injection

## How to use

`[path to injector executable]` `[process name]` `[path to dll file]`

Simply make `inject.bat` file in same directory with `Injector.exe` with one of next following contents

```batch
@echo off
::Injector.exe "ac_client.exe" "D:/dev/CheetoInjector/DLL Sample.dll"
Injector.exe "ac_client.exe" "./DLL Sample.dll"
::Injector.exe "ac_client.exe" "DLL Sample.dll"
@pause

```
## Credits
- [CasualGamer - How To Make A DLL Injector C++ (YouTube)](https://www.youtube.com/watch?v=44-TOfLGBzk)
- [Dan Sporici - C# Inject a Dll into a Process (Article)](https://codingvision.net/c-inject-a-dll-into-a-process-w-createremotethread)
- [Zer0Mem0ry - Dll Injection Explained (YouTube)](https://www.youtube.com/watch?v=IBwoVUR1gt8)
- [Zer0Mem0ry - Dll Injection (YouTube)](https://www.youtube.com/watch?v=g_Xx90wyk0c)
