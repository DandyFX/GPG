// SPDX-License-Identifier: MIT
// Copyright: 2018 David Lechner <david@lechnology.com>

using System;
using System.Runtime.InteropServices;

namespace Dandy.GPG.Rt
{
    public static class Runtime
    {
        static readonly bool windows = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);

        internal const string RuntimeLibraryWin = "libgpg-error-0";
        internal const string RuntimeLibraryUnix = "gpg-error";

        [DllImport(RuntimeLibraryWin, EntryPoint = "gpg_err_init", CallingConvention = CallingConvention.Cdecl)]
        static extern Error win_gpg_err_init();

        [DllImport(RuntimeLibraryUnix, EntryPoint = "gpg_err_init", CallingConvention = CallingConvention.Cdecl)]
        static extern Error unix_gpg_err_init();

        public static void Init()
        {
            var err = windows ? win_gpg_err_init() : unix_gpg_err_init();
            err.Assert();
        }

        [DllImport(RuntimeLibraryWin, EntryPoint = "gpgrt_check_version", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        static extern IntPtr win_gpgrt_check_version(string rec_version);

        [DllImport(RuntimeLibraryUnix, EntryPoint = "gpgrt_check_version", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        static extern IntPtr unix_gpgrt_check_version(string rec_version);

        public static string CheckVersion(string requiredVersion)
        {
            var ptr = windows ? win_gpgrt_check_version(requiredVersion) : unix_gpgrt_check_version(requiredVersion);
            return Marshal.PtrToStringAnsi(ptr);
        }
    }
}
