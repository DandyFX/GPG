// SPDX-License-Identifier: MIT
// Copyright: 2018 David Lechner <david@lechnology.com>

using System;
using System.Net.Sockets;
using System.Reflection;
using System.Runtime.InteropServices;

using static Dandy.GPG.Assuan.Global;

namespace Dandy.GPG.Assuan
{
    /// <summary>
    /// File descriptor
    /// </summary>
    public partial struct Fd : IEquatable<Fd>
    {
#if WIN
        IntPtr fd;
#else
        int fd;
#endif

        /// <summary>
        /// Invalid file descriptor
        /// </summary>
        public static Fd Invalid => new Fd {
#if WIN
            fd = new IntPtr(-1)
#else
            fd = -1
#endif
        };

        [DllImport("msvcrt.dll", SetLastError=true)]
        static extern IntPtr _get_osfhandle(int fd);

        /// <summary>
        /// Converts a POSIX file descriptor to an Assuan file descriptor.
        /// </summary>
        public static Fd FromPosixFd(int fd)
        {
#if WIN
            if (fd < 0) {
                return Invalid;
            }
            return new Fd { fd = _get_osfhandle(fd) };
#else
            return new Fd { fd = fd };
#endif
        }

        public static Fd FromSocket(Socket socket)
        {
            // Socket.Handle property is not available in netstandard1.6
            // TODO: Fix this when we upgrade to netstandard2.x
            var handle = (IntPtr)socket.GetType().GetRuntimeProperty("Handle").GetValue(socket);
#if WIN
            return new Fd { fd = handle };
#else
            return new Fd { fd = handle.ToInt32() };
#endif
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if (obj is Fd other) {
                return this.Equals(other);
            }
            return false;
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            return fd.GetHashCode();
        }

        /// <inheritdoc />
        public bool Equals(Fd other)
        {
            return fd == other.fd;
        }

        /// <summary>
        /// Tests if two file descriptors are equal
        /// </summary>
        public static bool operator ==(Fd a, Fd b)
        {
            return a.Equals(b);
        }

        /// <summary>
        /// Tests if two file descriptors are not equal
        /// </summary>
        public static bool operator !=(Fd a, Fd b)
        {
            return !a.Equals(b);
        }
    }
}
