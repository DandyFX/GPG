// SPDX-License-Identifier: MIT
// Copyright: 2018 David Lechner <david@lechnology.com>

using System;
using System.Runtime.InteropServices;

namespace Dandy.GPG.Assuan
{
    public struct SocketNonce
    {
        IntPtr length;
#if WIN
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
        byte[] nonce;
#endif
    }
}
