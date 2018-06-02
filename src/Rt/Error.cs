// SPDX-License-Identifier: MIT
// Copyright: 2018 David Lechner <david@lechnology.com>

using System;
using System.Runtime.InteropServices;

using static Dandy.GPG.Rt.Runtime;

namespace Dandy.GPG.Rt
{
    public struct Error
    {
        static readonly bool windows = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);

        const int codeMask = 65536 - 1;
        const int sourceMask = 128 - 1;
        const int sourceShift = 24;

        readonly uint error;

        public ErrorCode Code => (ErrorCode)(error & codeMask);

        [DllImport(RuntimeLibraryWin, EntryPoint = "gpg_strerror", CallingConvention = CallingConvention.Cdecl)]
        static extern IntPtr win_gpg_strerror(uint err);

        [DllImport(RuntimeLibraryUnix, EntryPoint = "gpg_strerror", CallingConvention = CallingConvention.Cdecl)]
        static extern IntPtr unix_gpg_strerror(uint err);

        public string CodeDescription {
            get {
                var ptr = windows ? win_gpg_strerror(error) : unix_gpg_strerror(error);
                return Marshal.PtrToStringAnsi(ptr);
            }
        }

        public ErrorSource Source => (ErrorSource)((error >> sourceShift) & sourceMask);

        [DllImport(RuntimeLibraryWin, EntryPoint = "gpg_strsource", CallingConvention = CallingConvention.Cdecl)]
        static extern IntPtr win_gpg_strsource(uint err);

        [DllImport(RuntimeLibraryUnix, EntryPoint = "gpg_strsource", CallingConvention = CallingConvention.Cdecl)]
        static extern IntPtr unix_gpg_strsource(uint err);

        public string SourceDescription {
            get {
                var ptr = windows ? win_gpg_strsource(error) : unix_gpg_strsource(error);
                return Marshal.PtrToStringAnsi(ptr);
            }
        }

        /// <summary>
        /// Indicates that the underlying value is -1 (not a valid value).
        /// This is used internally to work around functions that return
        /// -1 instead of a valid error and should not normally be needed.
        /// </summary>
        public bool IsMinusOne => error == unchecked((uint)-1);

        public Error(ErrorSource source, ErrorCode code)
        {
            if (code == ErrorCode.NoError) {
                error = (uint)ErrorCode.NoError;
            }
            else {
                error = ((uint)source & sourceMask) << sourceShift | ((uint)code & codeMask);
            }
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return $"{error} {CodeDescription} <{SourceDescription}>";
        }

        /// <summary>
        /// Throws a <see cref="ErrorException" /> if error is not
        /// <see cref="ErrorCode.NoError" />
        /// </summary>
        public void Assert()
        {
            if (Code != ErrorCode.NoError) {
                throw new ErrorException(this);
            }
        }

        [DllImport(RuntimeLibraryWin, EntryPoint = "gpg_err_code_from_errno", CallingConvention = CallingConvention.Cdecl)]
        static extern ErrorCode win_gpg_err_code_from_errno(int err);

        [DllImport(RuntimeLibraryUnix, EntryPoint = "gpg_err_code_from_errno", CallingConvention = CallingConvention.Cdecl)]
        static extern ErrorCode unix_gpg_err_code_from_errno(int err);

        public static ErrorCode CodeFromErrno(int err)
        {
            return windows ? win_gpg_err_code_from_errno(err) : unix_gpg_err_code_from_errno(err);
        }

        [DllImport(RuntimeLibraryWin, EntryPoint = "gpg_err_code_to_errno", CallingConvention = CallingConvention.Cdecl)]
        static extern int win_gpg_err_code_to_errno(ErrorCode code);

        [DllImport(RuntimeLibraryUnix, EntryPoint = "gpg_err_code_to_errno", CallingConvention = CallingConvention.Cdecl)]
        static extern int unix_gpg_err_code_to_errno(ErrorCode code);

        public static int CodeToErrno(ErrorCode code)
        {
            return windows ? win_gpg_err_code_to_errno(code) : unix_gpg_err_code_to_errno(code);
        }
    }
}
