// SPDX-License-Identifier: MIT
// Copyright: 2018 David Lechner <david@lechnology.com>

using System;
using System.Runtime.InteropServices;

using static Dandy.GPG.Rt.Runtime;

namespace Dandy.GPG.Rt
{
    public struct Error
    {
        const int codeMask = 65536 - 1;
        const int sourceMask = 128 - 1;
        const int sourceShift = 24;

        readonly uint error;

        public ErrorCode Code => (ErrorCode)(error & codeMask);

        [DllImport(RuntimeLibrary, CallingConvention = CallingConvention.Cdecl)]
        static extern IntPtr gpg_strerror(uint err);

        public string CodeDescription {
            get {
                var ptr = gpg_strerror(error);
                return Marshal.PtrToStringAnsi(ptr);
            }
        }

        public ErrorSource Source => (ErrorSource)((error >> sourceShift) & sourceMask);

        [DllImport(RuntimeLibrary, CallingConvention = CallingConvention.Cdecl)]
        static extern IntPtr gpg_strsource(uint err);

        public string SourceDescription {
            get {
                var ptr = gpg_strsource(error);
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

        [DllImport(RuntimeLibrary, EntryPoint = "gpg_err_code_from_errno", CallingConvention = CallingConvention.Cdecl)]
        public static extern ErrorCode CodeFromErrno(int err);

        [DllImport(RuntimeLibrary, EntryPoint = "gpg_err_code_to_errno", CallingConvention = CallingConvention.Cdecl)]
        public static extern int CodeToErrno(ErrorCode code);
    }
}
