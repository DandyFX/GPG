// SPDX-License-Identifier: MIT
// Copyright: 2018 David Lechner <david@lechnology.com>

using System;
using System.Runtime.InteropServices;

namespace Dandy.GPG.Assuan
{
    /// <summary>
    /// Under Windows Assuan features an emulation of Unix domain sockets
    /// based on a local TCP connections. To implement access permissions
    /// based on file permissions a nonce is used which is expected by the
    /// server as the first bytes received.  On POSIX systems this is a
    /// dummy structure.
    /// </summary>
    /// <remarks>
    /// In unmanaged code, the size of this struct depends on Windows/Unix, so
    /// don't pass this directly to unmanaged code without checking the OS
    /// first. This struct can be used directly with unmanged code on Windows
    /// only.
    /// </remarks>
    public struct SocketNonce
    {
#pragma warning disable CS0169
        readonly IntPtr length;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
        readonly byte[] nonce;
#pragma warning restore CS0169
    }
}
