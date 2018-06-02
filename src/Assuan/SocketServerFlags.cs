// SPDX-License-Identifier: MIT
// Copyright: 2018 David Lechner <david@lechnology.com>

using System;

namespace Dandy.GPG.Assuan
{
    /// <summary>
    /// Flags for <see cref="Context.InitSocketServer"/>.
    /// </summary>
    [Flags]
    public enum SocketServerFlags
    {
        /// <summary>
        /// If set, sendmsg and recvmesg are used for input and output, which
        /// enables the use of descriptor passing.
        /// </summary>
        FdPassing = 1,

        /// <summary>
        /// If set, fd refers to an already accepted socket.  That is, Libassuan
        /// won’t call accept for it. It is suggested to set this bit as it
        /// allows better control of the connection state.
        /// </summary>
        Accepted = 2,
    }
}
