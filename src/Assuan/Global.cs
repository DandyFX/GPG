// SPDX-License-Identifier: MIT
// Copyright: 2018 David Lechner <david@lechnology.com>

namespace Dandy.GPG.Assuan
{
    static class Global
    {
        /// <summary>
        /// Name of the Libassuan dll on Windows
        /// </summary>
        internal const string AssuanLibraryWin = "libassuan-0";

        /// <summary>
        /// Name of the Libassuan shared lib on *nix
        /// </summary>
        internal const string AssuanLibraryUnix = "assuan";
    }
}
