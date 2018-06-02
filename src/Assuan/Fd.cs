// SPDX-License-Identifier: MIT
// Copyright: 2018 David Lechner <david@lechnology.com>

using System;
using System.Net.Sockets;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Dandy.GPG.Assuan
{
    /// <summary>
    /// File descriptor
    /// </summary>
    /// <remarks>
    /// In unmanaged code, the size of this struct depends on Windows/Unix, so
    /// don't pass this directly to unmanaged code without checking the OS
    /// first. This struct can be used directly with unmanged code on Windows,
    /// but <see cref="UnixFd"/> should be used instead on other OSes.
    /// </remarks>
    public partial struct Fd : IEquatable<Fd>
    {
        static readonly bool windows = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);

        IntPtr fd;

        /// <summary>
        /// Invalid file descriptor
        /// </summary>
        public static Fd Invalid => new Fd { fd = new IntPtr(-1) };

        public int UnixFd => windows ? throw new NotSupportedException() : fd.ToInt32();

        [DllImport("msvcrt.dll", SetLastError = true)]
        static extern IntPtr _get_osfhandle(int fd);

        /// <summary>
        /// Converts a POSIX file descriptor to an Assuan file descriptor.
        /// </summary>
        public static Fd FromPosixFd(int fd)
        {
            if (windows) {
                if (fd < 0) {
                    return Invalid;
                }
                return new Fd { fd = _get_osfhandle(fd) };
            }
            return new Fd { fd = (IntPtr)fd };
        }

        public static Fd FromSocket(Socket socket)
        {
            // Socket.Handle property is not available in netstandard1.6
            // TODO: Fix this when we upgrade to netstandard2.x
            var handle = (IntPtr)socket.GetType().GetRuntimeProperty("Handle").GetValue(socket);
            return new Fd { fd = handle };
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if (obj is Fd other) {
                return Equals(other);
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
