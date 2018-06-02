// SPDX-License-Identifier: MIT
// Copyright: 2018 David Lechner <david@lechnology.com>

using System;

namespace Dandy.GPG.Rt
{
    /// <summary>
    /// Managed exception wrapper around unmanged GPG runtime errors.
    /// </summary>
    public sealed class ErrorException : Exception
    {
        public Error Error { get; }

        /// <summary>
        /// Creates a new GPG runtime error exception
        /// </summary>
        public ErrorException(Error error) : base(error.ToString())
        {
            Error = error;
        }

        /// <summary>
        /// Creates a new GPG runtime error exception
        /// </summary>
        public ErrorException(ErrorSource source, ErrorCode code) : this(new Error(source, code))
        {
        }
    }
}
