// SPDX-License-Identifier: MIT
// Copyright: 2018 David Lechner <david@lechnology.com>

using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using Dandy.GPG.Assuan;
using Dandy.GPG.Rt;

namespace Dandy.GPG.Example.Server
{
    class Program
    {
        static void foo(string line)
        {
            Console.WriteLine("FOO: {0}", line);
        }

        static void bar(string line)
        {
            Console.WriteLine("BAR: {0}", line);
        }

        static void commandHandler(Fd fd)
        {
            var ctx = new Context();
            if (fd == Fd.Invalid) {
                ctx.InitPipeServer(Fd.FromPosixFd(0), Fd.FromPosixFd(1));
            }
            else {
                ctx.InitSocketServer(fd, SocketServerFlags.Accepted);
            }
            ctx.RegisterCommand("FOO", foo);
            ctx.RegisterCommand("BAR", bar);
            ctx.RegisterCommand("INPUT", null);
            ctx.RegisterCommand("OUTPUT", null);

            for(;;) {
                try {
                    ctx.Accept();
                }
                catch (ErrorException ex) when (ex.Error.Code == ErrorCode.EOF) {
                    break;
                }
                try {
                    ctx.Process();
                }
                catch (Exception ex) {
                    Console.Error.WriteLine("Processing failed: {}", ex.Message);
                    continue;
                }
            }
        }

        static void Main(string[] args)
        {
            Runtime.Init();

            if (args.Length > 0) {
                // if an argument is given, create a unix socket file
                using (var socket = new Socket(AddressFamily.Unix, SocketType.Stream, ProtocolType.Unspecified)) {
                    // FIXME: we should be deleting the socket file when the program exits, not when it starts
                    if (File.Exists(args[0])) {
                        File.Delete(args[0]);
                    }
                    var endpoint = new UnixDomainSocketEndPoint(args[0]);
                    socket.Bind(endpoint);
                    socket.Listen(0);
                    commandHandler(Fd.FromSocket(socket.Accept()));
                }
            }
            else {
                // when no command line args, use stdin/stdout pipes
                commandHandler(Fd.Invalid);
            }
        }
    }
}
