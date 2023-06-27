using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;

public static class ADSHelper
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    private struct WIN32_STREAM_ID
    {
        public uint dwStreamId;
        public uint dwStreamAttributes;
        public LARGE_INTEGER Size;
        public uint dwStreamNameSize;
    }

    [StructLayout(LayoutKind.Explicit, Size = 8)]
    private struct LARGE_INTEGER
    {
        [FieldOffset(0)] public int LowPart;
        [FieldOffset(4)] public int HighPart;
    }

    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern IntPtr CreateFile(string lpFileName, uint dwDesiredAccess, FileShare dwShareMode, IntPtr lpSecurityAttributes, FileMode dwCreationDisposition, uint dwFlagsAndAttributes, IntPtr hTemplateFile);

    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern bool BackupRead(IntPtr hFile, IntPtr lpBuffer, uint nNumberOfBytesToRead, out uint lpNumberOfBytesRead, bool bAbort, bool bProcessSecurity, ref IntPtr lpContext);

    public static IEnumerable<string> GetAlternateStreams(string filePath)
    {
        const uint GENERIC_READ = 0x80000000;
        const uint FILE_FLAG_BACKUP_SEMANTICS = 0x02000000;

        var hFile = CreateFile(filePath, GENERIC_READ, FileShare.Read, IntPtr.Zero, FileMode.Open, FILE_FLAG_BACKUP_SEMANTICS, IntPtr.Zero);

        if (hFile.ToInt64() == -1)
            yield break;

        IntPtr context = IntPtr.Zero;
        WIN32_STREAM_ID streamId;
        uint dwStreamHeaderSize = (uint)Marshal.SizeOf(typeof(WIN32_STREAM_ID));
        IntPtr buffer = Marshal.AllocHGlobal((int)dwStreamHeaderSize);
        uint bytesRead = 0;

        while (BackupRead(hFile, buffer, dwStreamHeaderSize, out bytesRead, false, true, ref context))
        {
            if (bytesRead == 0)
                break;

            streamId = (WIN32_STREAM_ID)Marshal.PtrToStructure(buffer, typeof(WIN32_STREAM_ID));

            if (streamId.dwStreamNameSize > 0)
            {
                IntPtr nameBuffer = Marshal.AllocHGlobal((int)streamId.dwStreamNameSize);
                if (BackupRead(hFile, nameBuffer, streamId.dwStreamNameSize, out bytesRead, false, true, ref context))
                {
                    string streamName = Marshal.PtrToStringUni(nameBuffer, (int)bytesRead / sizeof(char));
                    if (!string.IsNullOrEmpty(streamName))
                        yield return streamName;
                }
                Marshal.FreeHGlobal(nameBuffer);
            }

            long streamSize = streamId.Size.LowPart + ((long)streamId.Size.HighPart << 32);

            if (streamSize > 0)
            {
                IntPtr tempBuffer = IntPtr.Zero;
                uint tempSize = (uint)Math.Min(streamSize, int.MaxValue);
                BackupRead(hFile, tempBuffer, tempSize, out bytesRead, true, true, ref context);
            }
        }

        Marshal.FreeHGlobal(buffer);
        BackupRead(hFile, IntPtr.Zero, 0, out bytesRead, true, true, ref context);
    }
}
