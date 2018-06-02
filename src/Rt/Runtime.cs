// SPDX-License-Identifier: MIT
// Copyright: 2018 David Lechner <david@lechnology.com>

using System;
using System.Runtime.InteropServices;

namespace Dandy.GPG.Rt
{
    public static class Runtime
    {
        internal const string GPGRuntimeLibrary = "gpg-error";

        [DllImport(GPGRuntimeLibrary, CallingConvention = CallingConvention.Cdecl)]
        static extern Error gpg_err_init();

        public static void Init()
        {
            var err = gpg_err_init();
            err.Assert();
        }

        [DllImport(GPGRuntimeLibrary, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        static extern IntPtr gpgrt_check_version(string rec_version);

        public static string CheckVersion(string requiredVersion)
        {
            var ptr = gpgrt_check_version(requiredVersion);
            return Marshal.PtrToStringAnsi(ptr);
        }
    }
}
