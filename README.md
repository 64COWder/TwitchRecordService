# TwitchRecordService

Records streams to specified location (TwitchRecordService.exe.config)

Checks if streamer is online every 30 seconds. If so, start saving video.

Build in release mode. If you have Windows 8/10, add to services with something like
  New-Service -Name Test -BinaryPathName "C:\Users\j\Downloads\Release\TwitchRecordService.exe" 
