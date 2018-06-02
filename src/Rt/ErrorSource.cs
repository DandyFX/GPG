// SPDX-License-Identifier: MIT
// Copyright: 2018 David Lechner <david@lechnology.com>

namespace Dandy.GPG.Rt
{
    public enum ErrorSource : sbyte
    {
        Unknown = 0,
        GCrypt = 1,
        GPG = 2,
        GPGSM = 3,
        GPGAgent = 4,
        PinEntry = 5,
        SCD = 6,
        GPGME = 7,
        KeyBox = 8,
        KSBA = 9,
        Dirmngr = 10,
        GSTI = 11,
        GPA = 12,
        KLEO = 13,
        G13 = 14,
        Assuan = 15,
        TLS = 17,
        Any = 31,
        User1 = 32,
        User2 = 33,
        User3 = 34,
        User4 = 35,
    }
}
