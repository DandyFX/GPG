// SPDX-License-Identifier: MIT
// Copyright: 2018 David Lechner <david@lechnology.com>

using System;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using Dandy.GPG.Rt;
using static Dandy.GPG.Assuan.Global;

namespace Dandy.GPG.Assuan
{
    public sealed class Socket : IDisposable
    {
        static readonly bool windows = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);

        Fd fd = Fd.Invalid;

        public Fd Fd {
            get {
                if (fd == Fd.Invalid) {
                    throw new ObjectDisposedException(null);
                }
                return fd;
            }
        }


        [DllImport(AssuanLibraryWin, EntryPoint = "assuan_sock_init", CallingConvention = CallingConvention.Cdecl)]
        static extern Error win_assuan_sock_init();

        [DllImport(AssuanLibraryUnix, EntryPoint = "assuan_sock_init", CallingConvention = CallingConvention.Cdecl)]
        static extern Error unix_assuan_sock_init();

        static Socket()
        {
            var err = windows ? win_assuan_sock_init() : unix_assuan_sock_init();
            err.Assert();
        }

        [DllImport(AssuanLibraryWin, EntryPoint = "assuan_sock_new", SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        static extern Fd win_assuan_sock_new(int domain, int type, int protocol);

        [DllImport(AssuanLibraryUnix, EntryPoint = "assuan_sock_new", SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        static extern int unix_assuan_sock_new(int domain, int type, int protocol);

        public Socket(AddressFamily addressFamily, SocketType socketType, ProtocolType protocolType)
        {
            if (windows) {
                fd = win_assuan_sock_new((int)addressFamily, (int)socketType, (int)protocolType);
                if (fd == Fd.Invalid) {
                    var err = Marshal.GetLastWin32Error();
                    throw new ErrorException(ErrorSource.User1, Error.CodeFromErrno(err));
                }
            }
            else {
                var ret = unix_assuan_sock_new((int)addressFamily, (int)socketType, (int)protocolType);
                if (ret == -1) {
                    var err = Marshal.GetLastWin32Error();
                    throw new ErrorException(ErrorSource.User1, Error.CodeFromErrno(err));
                }
                fd = Fd.FromPosixFd(ret);
            }
        }

        Socket(Fd fd)
        {
            this.fd = fd;
        }

        [DllImport(AssuanLibraryWin, EntryPoint = "assuan_sock_close", CallingConvention = CallingConvention.Cdecl)]
        static extern int win_assuan_sock_close(Fd fd);

        [DllImport(AssuanLibraryUnix, EntryPoint = "assuan_sock_close", CallingConvention = CallingConvention.Cdecl)]
        static extern int unix_assuan_sock_close(int fd);

        #region IDisposable Support

        void Dispose(bool disposing)
        {
            if (fd != Fd.Invalid) {
                if (windows) {
                    win_assuan_sock_close(fd);
                }
                else {
                    unix_assuan_sock_close(fd.UnixFd);
                }
            }
        }

        ~Socket() {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion

        [DllImport(AssuanLibraryWin, EntryPoint = "assuan_sock_bind", SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        static extern int win_assuan_sock_bind(Fd fd, IntPtr addr, int addrlen);

        [DllImport(AssuanLibraryUnix, EntryPoint = "assuan_sock_bind", SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        static extern int unix_assuan_sock_bind(int fd, IntPtr addr, int addrlen);

        public void Bind(SocketAddress address)
        {
            var addrPtr = Marshal.AllocHGlobal(address?.Size ?? throw new ArgumentNullException(nameof(address)));
            try {
                for (var i = 0; i < address.Size; i++) {
                    Marshal.WriteByte(addrPtr, i, address[i]);
                }

                var ret = windows ? win_assuan_sock_bind(Fd, addrPtr, address.Size) : unix_assuan_sock_bind(Fd.UnixFd, addrPtr, address.Size);
                if (ret == -1) {
                    var err = Marshal.GetLastWin32Error();
                    throw new ErrorException(ErrorSource.User1, Error.CodeFromErrno(err));
                }
            }
            finally {
                Marshal.FreeHGlobal(addrPtr);
            }
        }

        [DllImport(AssuanLibraryWin, EntryPoint = "assuan_sock_set_sockaddr_un", SetLastError = true, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        static extern int win_assuan_sock_set_socaddr_un(string fname, IntPtr addr, out bool r_redirected);

        [DllImport(AssuanLibraryUnix, EntryPoint = "assuan_sock_set_sockaddr_un", SetLastError = true, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        static extern int unix_assuan_sock_set_socaddr_un(string fname, IntPtr addr, out bool r_redirected);

        public static SocketAddress CreateSocketAddress(string path, out bool redirected)
        {
            if (path == null) {
                throw new ArgumentNullException(nameof(path));
            }

            var max_size = 128;

            var addrPtr = Marshal.AllocHGlobal(max_size);
            try {
                var ret = windows ? win_assuan_sock_set_socaddr_un(path, addrPtr, out redirected) : unix_assuan_sock_set_socaddr_un(path, addrPtr, out redirected);
                if (ret == -1) {
                    var err = Marshal.GetLastWin32Error();
                    throw new ErrorException(ErrorSource.User1, Error.CodeFromErrno(err));
                }

                // Find the actual size (header + string length)
                var size = windows ? 8 : 2;
                while (Marshal.ReadByte(addrPtr, size) != 0) {
                    size++;
                }

                var address = new SocketAddress(AddressFamily.Unix, size);

                for (int i = 0; i < size; i++) {
                    address[i] = Marshal.ReadByte(addrPtr, i);
                }

                return address;
            }
            finally {
                Marshal.FreeHGlobal(addrPtr);
            }
        }

        [DllImport("Ws2_32", EntryPoint = "listen", SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        static extern int win_listen(Fd s, int backlog);

        [DllImport("libc", EntryPoint = "listen", SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        static extern int unix_listen(int sockfd, int backlog);

        public void Listen(int backlog)
        {
            var ret = windows ? win_listen(Fd, backlog) : unix_listen(Fd.UnixFd, backlog);
            if (ret == -1) {
                var err = Marshal.GetLastWin32Error();
                throw new ErrorException(ErrorSource.User1, Error.CodeFromErrno(err));
            }
        }

        [DllImport("Ws2_32", EntryPoint = "accept", SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        static extern Fd win_accept(Fd s, IntPtr addr, IntPtr addrlen);

        [DllImport("libc", EntryPoint = "accept", SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        static extern int unix_accept(int sockfd, IntPtr addr, IntPtr addrlen);

        public Socket Accept()
        {
            if (windows) {
                var ret = win_accept(Fd, IntPtr.Zero, IntPtr.Zero);
                if (ret == Fd.Invalid) {
                    var err = Marshal.GetLastWin32Error();
                    throw new ErrorException(ErrorSource.User1, Error.CodeFromErrno(err));
                }
                return new Socket(ret);
            }
            else {
                var ret = unix_accept(Fd.UnixFd, IntPtr.Zero, IntPtr.Zero);
                if (ret == -1) {
                    var err = Marshal.GetLastWin32Error();
                    throw new ErrorException(ErrorSource.User1, Error.CodeFromErrno(err));
                }
                return new Socket(Fd.FromPosixFd(ret));
            }
        }
    }
}
