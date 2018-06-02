// SPDX-License-Identifier: MIT
// Copyright: 2018 David Lechner <david@lechnology.com>

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Dandy.GPG.Rt;
using static Dandy.GPG.Assuan.Global;

namespace Dandy.GPG.Assuan
{
    public sealed class Context : IDisposable
    {
        static readonly bool windows = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);

        readonly List<Handler> commands = new List<Handler>();
        IntPtr handle;

        IntPtr Handle {
            get {
                if (handle == IntPtr.Zero) {
                    throw new ObjectDisposedException(null);
                }
                return handle;
            }
        }

        [DllImport(AssuanLibraryWin, EntryPoint = "assuan_new", CallingConvention = CallingConvention.Cdecl)]
        static extern Error win_assuan_new(ref IntPtr ctx);

        [DllImport(AssuanLibraryUnix, EntryPoint = "assuan_new", CallingConvention = CallingConvention.Cdecl)]
        static extern Error unix_assuan_new(ref IntPtr ctx);

        public Context()
        {
            var err = windows ? win_assuan_new(ref handle) : unix_assuan_new(ref handle);
            err.Assert();
        }

        [DllImport(AssuanLibraryWin, EntryPoint = "assuan_release", CallingConvention = CallingConvention.Cdecl)]
        static extern void win_assuan_release(IntPtr ctx);

        [DllImport(AssuanLibraryUnix, EntryPoint = "assuan_release", CallingConvention = CallingConvention.Cdecl)]
        static extern void unix_assuan_release(IntPtr ctx);

        #region IDisposable Support

        void Dispose(bool disposing)
        {
            if (handle != IntPtr.Zero) {
                if (windows) {
                    win_assuan_release(handle);
                 }
                 else {
                     unix_assuan_release(handle);
                 }
                handle = IntPtr.Zero;
            }
        }

        ~Context()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion

        [DllImport(AssuanLibraryWin, EntryPoint = "assuan_init_socket_server", CallingConvention = CallingConvention.Cdecl)]
        static extern Error win_assuan_init_socket_server(IntPtr ctx, Fd fd, SocketServerFlags flags);

        [DllImport(AssuanLibraryUnix, EntryPoint = "assuan_init_socket_server", CallingConvention = CallingConvention.Cdecl)]
        static extern Error unix_assuan_init_socket_server(IntPtr ctx, int fd, SocketServerFlags flags);

        void InitSocketServer(Fd fd, SocketServerFlags flags)
        {
            var err = windows ? win_assuan_init_socket_server(Handle, fd, flags)
                : unix_assuan_init_socket_server(Handle, fd.UnixFd, flags);
            err.Assert();
        }

        public void InitSocketServer(Socket socket, SocketServerFlags flags)
        {
            InitSocketServer(socket?.Fd ?? throw new ArgumentNullException(nameof(socket)), flags);
        }

        [DllImport(AssuanLibraryWin, EntryPoint = "assuan_init_pipe_server", CallingConvention = CallingConvention.Cdecl)]
        static extern Error win_assuan_init_pipe_server(IntPtr ctx, Fd[] filedes);

        [DllImport(AssuanLibraryUnix, EntryPoint = "assuan_init_pipe_server", CallingConvention = CallingConvention.Cdecl)]
        static extern Error unix_assuan_init_pipe_server(IntPtr ctx, int[] filedes);

        public void InitPipeServer(Fd input, Fd output)
        {
            if (windows) {
                var filedes = new Fd[] { input, output };
                var err = win_assuan_init_pipe_server(Handle, filedes);
                err.Assert();
            }
            else {
                var filedes = new int[] { input.UnixFd, output.UnixFd };
                var err = unix_assuan_init_pipe_server(Handle, filedes);
                err.Assert();
            }
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        delegate Error Handler(IntPtr ctx, IntPtr line);

        [DllImport(AssuanLibraryWin, EntryPoint = "assuan_register_command", CallingConvention = CallingConvention.Cdecl)]
        static extern Error win_assuan_register_command(IntPtr ctx, IntPtr cmd_string, Handler handler, IntPtr help_string);

        [DllImport(AssuanLibraryUnix, EntryPoint = "assuan_register_command", CallingConvention = CallingConvention.Cdecl)]
        static extern Error unix_assuan_register_command(IntPtr ctx, IntPtr cmd_string, Handler handler, IntPtr help_string);

        public void RegisterCommand(string cmd, Action<string> handler, string help = null)
        {
            var cmdPtr = Marshal.StringToHGlobalAnsi(cmd ?? throw new ArgumentNullException(nameof(cmd)));

            var handlerPtr = handler == null ? null : new Handler((c, l) =>
            {
                try {
                    handler(Marshal.PtrToStringAnsi(l));
                    return new Error(ErrorSource.Unknown, ErrorCode.NoError);
                }
                catch (ErrorException ex) {
                    return ex.Error;
                }
                catch (Exception) {
                    return new Error(ErrorSource.User1, ErrorCode.General);
                }
            });
            // keep unmanaged callback from being GC'ed
            commands.Add(handlerPtr);

            var helpPtr = Marshal.StringToHGlobalAnsi(help);

            var err = windows ?
                win_assuan_register_command(Handle, cmdPtr, handlerPtr, helpPtr) :
                unix_assuan_register_command(Handle, cmdPtr, handlerPtr, helpPtr);
            err.Assert();
        }

        [DllImport(AssuanLibraryWin, EntryPoint = "assuan_accept", CallingConvention = CallingConvention.Cdecl)]
        static extern Error win_assuan_accept(IntPtr ctx);

        [DllImport(AssuanLibraryUnix, EntryPoint = "assuan_accept", CallingConvention = CallingConvention.Cdecl)]
        static extern Error unix_assuan_accept(IntPtr ctx);

        public void Accept()
        {
            var err = windows ? win_assuan_accept(Handle) : unix_assuan_accept(Handle);

            // work around for assuan_accept() returning -1 to indicate EOF
            // instead of proper error code.
            if (err.IsMinusOne) {
                err = new Error(ErrorSource.Assuan, ErrorCode.EOF);
            }

            err.Assert();
        }

        [DllImport(AssuanLibraryWin, EntryPoint = "assuan_process", CallingConvention = CallingConvention.Cdecl)]
        static extern Error win_assuan_process(IntPtr ctx);

        [DllImport(AssuanLibraryUnix, EntryPoint = "assuan_process", CallingConvention = CallingConvention.Cdecl)]
        static extern Error unix_assuan_process(IntPtr ctx);

        public void Process()
        {
            var err = windows ? win_assuan_process(Handle) : unix_assuan_process(Handle);
            err.Assert();
        }
    }
}
