using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace toolboxmamaneg
{
    public static class ProcessExtensions2
    {
            // Import the necessary Win32 API functions
        [DllImport("Wtsapi32.dll")]
        private static extern bool WTSQuerySessionInformation(
            IntPtr hServer,
            int sessionId,
            WTS_INFO_CLASS wtsInfoClass,
            out IntPtr ppBuffer,
            out int pBytesReturned
        );

        [DllImport("Kernel32.dll")]
        private static extern int WTSGetActiveConsoleSessionId();

        // Define the necessary Win32 API structures and constants
        private const int WTS_CURRENT_SESSION = -1;
        private const int WTS_SESSION_INFO_0 = 0;
        private const int WTSUserName = 5;
        private const int MAX_LENGTH = 256;

        [StructLayout(LayoutKind.Sequential)]
        private struct WTS_SESSION_INFO
        {
            public int SessionId;
            public string WinStationName;
            public WTS_CONNECTSTATE_CLASS State;
        }

        private enum WTS_CONNECTSTATE_CLASS
        {
            Active,
            Connected,
            ConnectQuery,
            Shadow,
            Disconnected,
            Idle,
            Listen,
            Reset,
            Down,
            Init
        }

        private enum WTS_INFO_CLASS
        {
            WTSInitialProgram,
            WTSApplicationName,
            WTSWorkingDirectory,
            WTSOEMId,
            WTSSessionId,
            WTSUserName,
            WTSWinStationName,
            WTSDomainName,
            WTSConnectState,
            WTSClientBuildNumber,
            WTSClientName,
            WTSClientDirectory,
            WTSClientProductId,
            WTSClientHardwareId,
            WTSClientAddress,
            WTSClientDisplay,
            WTSClientProtocolType,
            WTSIdleTime,
            WTSLogonTime,
            WTSIncomingBytes,
            WTSOutgoingBytes,
            WTSIncomingFrames,
            WTSOutgoingFrames,
            WTSClientInfo,
            WTSSessionInfo,
            WTSSessionInfoEx,
            WTSConfigInfo,
            WTSValidationInfo,
            WTSSessionAddressV4,
            WTSIsRemoteSession
        }

        public static int GetCurrentSessionId()
        {
            return WTSGetActiveConsoleSessionId();
        }

        public static string GetCurrentUserName()
        {
            int sessionId = GetCurrentSessionId();

            IntPtr buffer = IntPtr.Zero;
            int bytesReturned;
            string userName = "";

            bool success = WTSQuerySessionInformation(
                IntPtr.Zero,
                sessionId,
                WTS_INFO_CLASS.WTSUserName,
                out buffer,
                out bytesReturned
            );

            if (success)
            {
                userName = Marshal.PtrToStringAnsi(buffer);
            }

            return userName;
        }
    }
}