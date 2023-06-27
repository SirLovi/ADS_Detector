# ADS_Detector
Scanning tool that detects ADS (Alternate Data Stream).

- [x] Detect ADS in NTFS
- [ ] Implement detection service in Windows
- [x] Real time detection
- [x] Notification pop-up
- [x] Extended configuration (Folder restriction, drives, ...)
- [ ] User Interface

```
echo test > file.txt:stream
more < file.txt:stream
```