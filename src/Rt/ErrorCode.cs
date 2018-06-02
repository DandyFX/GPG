// SPDX-License-Identifier: MIT
// Copyright: 2018 David Lechner <david@lechnology.com>

namespace Dandy.GPG.Rt
{
    static class System
    {
        public const ushort Error = 1 << 15;
    }

    public enum ErrorCode : ushort
    {
        NoError = 0,
        General = 1,
        UnknownPacket = 2,
        UnknownVersion = 3,
        PubkeyAlgo = 4,
        DigestAlgo = 5,
        BadPubkey = 6,
        BadSeckey = 7,
        BadSignature = 8,
        NoPubkey = 9,
        Checksum = 10,
        BadPassphrase = 11,
        CipherAlgo = 12,
        KeyringOpen = 13,
        InvalidPacket = 14,
        InvalidArmor = 15,
        NoUserId = 16,
        NoSeckey = 17,
        WrongSeckey = 18,
        BadKey = 19,
        ComprAlgo = 20,
        NoPrime = 21,
        NoEncodingMethod = 22,
        NoEncryptionScheme = 23,
        NoSignatureScheme = 24,
        InvalidAttr = 25,
        NoValue = 26,
        NotFound = 27,
        ValueNotFound = 28,
        Syntax = 29,
        BadMPI = 30,
        InvalidPassphrase = 31,
        SigClass = 32,
        ResourceLimit = 33,
        InvalidKeyring = 34,
        TrustDB = 35,
        BadCert = 36,
        InvalidUserId = 37,
        Unexpected = 38,
        TimeConflict = 39,
        Keyserver = 40,
        WrongPubkeyAlgo = 41,
        TributeToDA = 42,
        WeakKey = 43,
        InvalidKeylen = 44,
        InvalidArg = 45,
        BadUri = 46,
        InvalidUri = 47,
        Network = 48,
        UnknownHost = 49,
        SelftestFailed = 50,
        NotEncrypted = 51,
        NotProcessed = 52,
        UnusablePubkey = 53,
        UnusableSeckey = 54,
        InvalidValue = 55,
        BadCertChain = 56,
        MissingCert = 57,
        NoData = 58,
        Bug = 59,
        NotSupported = 60,
        InvalidOp = 61,
        Timeout = 62,
        Internal = 63,
        EOFGCrypt = 64,
        InvalidObj = 65,
        TooShort = 66,
        TooLarge = 67,
        NoObj = 68,
        NotImplemented = 69,
        Conflict = 70,
        InvalidCipherMode = 71,
        InvalidFlag = 72,
        InvalidHandle = 73,
        Truncated = 74,
        IncompleteLine = 75,
        InvalidResponse = 76,
        NoAgent = 77,
        Agent = 78,
        InvalidData = 79,
        AssuanServerFault = 80,
        Assuan = 81,
        InvalidSessionKey = 82,
        InvalidSExpression = 83,
        UnsupportedAlgorithm = 84,
        NoPinEntry = 85,
        PinEntry = 86,
        BadPin = 87,
        InvalidName = 88,
        BadData = 89,
        InvalidParameter = 90,
        WrongCard = 91,
        NoDirmngr = 92,
        Dirmngr = 93,
        CertRevoked = 94,
        NoCrlKnown = 95,
        CrlTooOld = 96,
        LineTooLong = 97,
        NotTrusted = 98,
        Canceled = 99,
        BadCaCert = 100,
        CertExpired = 101,
        CertTooYoung = 102,
        UnsupportedCert = 103,
        UnknownSExpression = 104,
        UnsupportedProtection = 105,
        CorruptedProtection = 106,
        AmbiguousName = 107,
        Card = 108,
        CardReset = 109,
        CardRemoved = 110,
        InvalidCard = 111,
        CardNotPresent = 112,
        NoPKCS15App = 113,
        NotConfirmed = 114,
        Configuration = 115,
        NoPolicyMatch = 116,
        InvalidIndex = 117,
        InvalidId = 118,
        NoSCDaemon = 119,
        SCDaemon = 120,
        UnsupportedProtocol = 121,
        BadPinMethod = 122,
        CardNotInitialized = 123,
        UnsupportedOperation = 124,
        WrongKeyUsage = 125,
        NothingFound = 126,
        WrongBlobType = 127,
        MissingValue = 128,
        Hardware = 129,
        PinBlocked = 130,
        UseConditions = 131,
        PinNotSynced = 132,
        InvalidCrl = 133,
        BadBER = 134,
        InvalidBER = 135,
        ElementNotFound = 136,
        IdentifierNotFound = 137,
        InvalidTag = 138,
        InvalidLength = 139,
        InvalidKeyinfo = 140,
        UnexpectedTag = 141,
        NotDEREncoded = 142,
        NoCmsObj = 143,
        InvalidCmsObj = 144,
        UnknownCmsObj = 145,
        UnsupportedCmsObj = 146,
        UnsupportedEncoding = 147,
        UnsupportedCmsVersion = 148,
        UnknownAlgorithm = 149,
        InvalidEngine = 150,
        PubkeyNotTrusted = 151,
        DecryptFailed = 152,
        KeyExpired = 153,
        SigExpired = 154,
        EncodingProblem = 155,
        InvalidState = 156,
        DupValue = 157,
        MissingAction = 158,
        ModuleNotFound = 159,
        InvalidOidString = 160,
        InvalidTime = 161,
        InvalidCrlObj = 162,
        UnsupportedCrlVersion = 163,
        InvalidCertObj = 164,
        UnknownName = 165,
        LocaleProblem = 166,
        NotLocked = 167,
        ProtocolViolation = 168,
        InvalidMac = 169,
        InvalidRequest = 170,
        UnknownExtn = 171,
        UnknownCritExtn = 172,
        Locked = 173,
        UnknownOption = 174,
        UnknownCommand = 175,
        NotOperational = 176,
        NoPassphrase = 177,
        NoPin = 178,
        NotEnabled = 179,
        NoEngine = 180,
        MissingKey = 181,
        TooMany = 182,
        LimitReached = 183,
        NotInitialized = 184,
        MissingIssuerCert = 185,
        NoKeyserver = 186,
        InvalidCurve = 187,
        UnknownCurve = 188,
        DupKey = 189,
        Ambiguous = 190,
        NoCryptCtx = 191,
        WrongCryptCtx = 192,
        BadCryptCtx = 193,
        CryptCtxConflict = 194,
        BrokenPubkey = 195,
        BrokenSeckey = 196,
        MacAlgo = 197,
        FullyCanceled = 198,
        Unfinished = 199,
        BufferTooShort = 200,
        SExpressionInvalidLenSpec = 201,
        SExpressionStringTooLong = 202,
        SExpressionUnmatchedParen = 203,
        SExpressionNotCanonical = 204,
        SExpressionBadCharacter = 205,
        SExpressionBadQuotation = 206,
        SExpressionZeroPrefix = 207,
        SExpressionNestedDH = 208,
        SExpressionUnmatchedDH = 209,
        SExpressionUnexpectedPunc = 210,
        SExpressionBadHexChar = 211,
        SExpressionOddHexNumbers = 212,
        SExpressionBadOctChar = 213,
        SubkeySExpressionOrRev = 217,
        DBCorrupted = 218,
        ServerFailed = 219,
        NoName = 220,
        NoKey = 221,
        LegacyKey = 222,
        RequestTooShort = 223,
        RequestTooLong = 224,
        ObjTermState = 225,
        NoCertChain = 226,
        CertTooLarge = 227,
        InvalidRecord = 228,
        BadMac = 229,
        UnexpectedMsg = 230,
        ComprFailed = 231,
        WouldWrap = 232,
        FatalAlert = 233,
        NoCipher = 234,
        MissingClientCert = 235,
        CloseNotify = 236,
        TicketExpired = 237,
        BadTicket = 238,
        UnknownIdentity = 239,
        BadHsCert = 240,
        BadHsCertReq = 241,
        BadHsCertVer = 242,
        BadHsChangeCipher = 243,
        BadHsClientHello = 244,
        BadHsServerHello = 245,
        BadHsServerHelloDone = 246,
        BadHsFinished = 247,
        BadHsServerKex = 248,
        BadHsClientKex = 249,
        BogusString = 250,
        Forbidden = 251,
        KeyDisabled = 252,
        KeyOnCard = 253,
        InvalidLockObj = 254,
        True = 255,
        False = 256,
        AssuanGeneral = 257,
        AssuanAcceptFailed = 258,
        AssuanConnectFailed = 259,
        AssuanInvalidResponse = 260,
        AssuanInvalidValue = 261,
        AssuanIncompleteLine = 262,
        AssuanLineTooLong = 263,
        AssuanNestedCommands = 264,
        AssuanNoDataCb = 265,
        AssuanNoInquireCb = 266,
        AssuanNotAServer = 267,
        AssuanNotAClient = 268,
        AssuanServerStart = 269,
        AssuanReadError = 270,
        AssuanWriteError = 271,
        AssuanTooMuchData = 273,
        AssuanUnexpectedCmd = 274,
        AssuanUnknownCmd = 275,
        AssuanSyntax = 276,
        AssuanCanceled = 277,
        AssuanNoInput = 278,
        AssuanNoOutput = 279,
        AssuanParameter = 280,
        AssuanUnknownInquire = 281,
        EngineTooOld = 300,
        WindowTooSmall = 301,
        WindowTooLarge = 302,
        MissingEnvvar = 303,
        UserIdExists = 304,
        NameExists = 305,
        DupName = 306,
        TooYoung = 307,
        TooOld = 308,
        UnknownFlag = 309,
        InvalidOrder = 310,
        AlreadyFetched = 311,
        TryLater = 312,
        WrongName = 313,
        SystemBug = 666,
        DNSUnknown = 711,
        DNSSection = 712,
        DNSAddress = 713,
        DNSNoQuery = 714,
        DNSNoAnswer = 715,
        DNSClosed = 716,
        DNSVerify = 717,
        DNSTimeout = 718,
        LDAPGeneral = 721,
        LDAPAttrGeneral = 722,
        LDAPNameGeneral = 723,
        LDAPSecurityGeneral = 724,
        LDAPServiceGeneral = 725,
        LDAPUpdateGeneral = 726,
        LDAPEGeneral = 727,
        LDAPXGeneral = 728,
        LDAPOtherGeneral = 729,
        LDAPXConnecting = 750,
        LDAPReferralLimit = 751,
        LDAPClientLoop = 752,
        LDAPNoResults = 754,
        LDAPControlNotFound = 755,
        LDAPNotSupported = 756,
        LDAPConnect = 757,
        LDAPNoMemory = 758,
        LDAPParam = 759,
        LDAPUserCancelled = 760,
        LDAPFilter = 761,
        LDAPAuthUnknown = 762,
        LDAPTimeout = 763,
        LDAPDecoding = 764,
        LDAPEncoding = 765,
        LDAPLocal = 766,
        LDAPServerDown = 767,
        LDAPSuccess = 768,
        LDAPOperations = 769,
        LDAPProtocol = 770,
        LDAPTimelimit = 771,
        LDAPSizelimit = 772,
        LDAPCompareFalse = 773,
        LDAPCompareTrue = 774,
        LDAPUnsupportedAuth = 775,
        LDAPStrongAuthRqrd = 776,
        LDAPPartialResults = 777,
        LDAPReferral = 778,
        LDAPAdminlimit = 779,
        LDAPUnavailCritExtn = 780,
        LDAPConfidentRqrd = 781,
        LDAPSaslBindInprog = 782,
        LDAPNoSuchAttribute = 784,
        LDAPUndefinedType = 785,
        LDAPBadMatching = 786,
        LDAPConstViolation = 787,
        LDAPTypeValueExists = 788,
        LDAPInvalidSyntax = 789,
        LDAPNoSuchObj = 800,
        LDAPAliasProblem = 801,
        LDAPInvalidDnSyntax = 802,
        LDAPIsLeaf = 803,
        LDAPAliasDERef = 804,
        LDAPXProxyAuthFail = 815,
        LDAPBadAuth = 816,
        LDAPInvalidCredentials = 817,
        LDAPInsufficientAcc = 818,
        LDAPBusy = 819,
        LDAPUnavailable = 820,
        LDAPUnwillToPerform = 821,
        LDAPLoopDetect = 822,
        LDAPNamingViolation = 832,
        LDAPObjClsViolation = 833,
        LDAPNotAllowNonleaf = 834,
        LDAPNotAllowOnRdn = 835,
        LDAPAlreadyExists = 836,
        LDAPNoObjClassMods = 837,
        LDAPResultsTooLarge = 838,
        LDAPAffectsMultDsas = 839,
        LDAPVlv = 844,
        LDAPOther = 848,
        LDAPCupResourceLimit = 881,
        LDAPCupSecViolation = 882,
        LDAPCupInvalidData = 883,
        LDAPCupUnsupScheme = 884,
        LDAPCupReload = 885,
        LDAPCancelled = 886,
        LDAPNoSuchOperation = 887,
        LDAPTooLate = 888,
        LDAPCannotCancel = 889,
        LDAPAssertionFailed = 890,
        LDAPProxAuthDenied = 891,
        User1 = 1024,
        User2 = 1025,
        User3 = 1026,
        User4 = 1027,
        User5 = 1028,
        User6 = 1029,
        User7 = 1030,
        User8 = 1031,
        User9 = 1032,
        User10 = 1033,
        User11 = 1034,
        User12 = 1035,
        User13 = 1036,
        User14 = 1037,
        User15 = 1038,
        User16 = 1039,
        MissingErrno = 16381,
        UnknownErrno = 16382,
        EOF = 16383,

        /* The following error codes are used to map system errors.  */
        E2BIG = System.Error | 0,
        EACCES = System.Error | 1,
        EADDRINUSE = System.Error | 2,
        EADDRNOTAVAIL = System.Error | 3,
        EADV = System.Error | 4,
        EAFNOSUPPORT = System.Error | 5,
        EAGAIN = System.Error | 6,
        EALREADY = System.Error | 7,
        EAUTH = System.Error | 8,
        EBACKGROUND = System.Error | 9,
        EBADE = System.Error | 10,
        EBADF = System.Error | 11,
        EBADFD = System.Error | 12,
        EBADMSG = System.Error | 13,
        EBADR = System.Error | 14,
        EBADRPC = System.Error | 15,
        EBADRQC = System.Error | 16,
        EBADSLT = System.Error | 17,
        EBFONT = System.Error | 18,
        EBUSY = System.Error | 19,
        ECANCELED = System.Error | 20,
        ECHILD = System.Error | 21,
        ECHRNG = System.Error | 22,
        ECOMM = System.Error | 23,
        ECONNABORTED = System.Error | 24,
        ECONNREFUSED = System.Error | 25,
        ECONNRESET = System.Error | 26,
        ED = System.Error | 27,
        EDEADLK = System.Error | 28,
        EDEADLOCK = System.Error | 29,
        EDESTADDRREQ = System.Error | 30,
        EDIED = System.Error | 31,
        EDOM = System.Error | 32,
        EDOTDOT = System.Error | 33,
        EDQUOT = System.Error | 34,
        EEXIST = System.Error | 35,
        EFAULT = System.Error | 36,
        EFBIG = System.Error | 37,
        EFTYPE = System.Error | 38,
        EGRATUITOUS = System.Error | 39,
        EGREGIOUS = System.Error | 40,
        EHOSTDOWN = System.Error | 41,
        EHOSTUNREACH = System.Error | 42,
        EIDRM = System.Error | 43,
        EIEIO = System.Error | 44,
        EILSEQ = System.Error | 45,
        EINPROGRESS = System.Error | 46,
        EINTR = System.Error | 47,
        EINVAL = System.Error | 48,
        EIO = System.Error | 49,
        EISCONN = System.Error | 50,
        EISDIR = System.Error | 51,
        EISNAM = System.Error | 52,
        EL2HLT = System.Error | 53,
        EL2NSYNC = System.Error | 54,
        EL3HLT = System.Error | 55,
        EL3RST = System.Error | 56,
        ELIBACC = System.Error | 57,
        ELIBBAD = System.Error | 58,
        ELIBEXEC = System.Error | 59,
        ELIBMAX = System.Error | 60,
        ELIBSCN = System.Error | 61,
        ELNRNG = System.Error | 62,
        ELOOP = System.Error | 63,
        EMEDIUMTYPE = System.Error | 64,
        EMFILE = System.Error | 65,
        EMLINK = System.Error | 66,
        EMSGSIZE = System.Error | 67,
        EMULTIHOP = System.Error | 68,
        ENAMETOOLONG = System.Error | 69,
        ENAVAIL = System.Error | 70,
        ENEEDAUTH = System.Error | 71,
        ENETDOWN = System.Error | 72,
        ENETRESET = System.Error | 73,
        ENETUNREACH = System.Error | 74,
        ENFILE = System.Error | 75,
        ENOANO = System.Error | 76,
        ENOBUFS = System.Error | 77,
        ENOCSI = System.Error | 78,
        ENODATA = System.Error | 79,
        ENODEV = System.Error | 80,
        ENOENT = System.Error | 81,
        ENOEXEC = System.Error | 82,
        ENOLCK = System.Error | 83,
        ENOLINK = System.Error | 84,
        ENOMEDIUM = System.Error | 85,
        ENOMEM = System.Error | 86,
        ENOMSG = System.Error | 87,
        ENONET = System.Error | 88,
        ENOPKG = System.Error | 89,
        ENOPROTOOPT = System.Error | 90,
        ENOSPC = System.Error | 91,
        ENOSR = System.Error | 92,
        ENOSTR = System.Error | 93,
        ENOSYS = System.Error | 94,
        ENOTBLK = System.Error | 95,
        ENOTCONN = System.Error | 96,
        ENOTDIR = System.Error | 97,
        ENOTEMPTY = System.Error | 98,
        ENOTNAM = System.Error | 99,
        ENOTSOCK = System.Error | 100,
        ENOTSUP = System.Error | 101,
        ENOTTY = System.Error | 102,
        ENOTUNIQ = System.Error | 103,
        ENXIO = System.Error | 104,
        EOPNOTSUPP = System.Error | 105,
        EOVERFLOW = System.Error | 106,
        EPERM = System.Error | 107,
        EPFNOSUPPORT = System.Error | 108,
        EPIPE = System.Error | 109,
        EPROCLIM = System.Error | 110,
        EPROCUNAVAIL = System.Error | 111,
        EPROGMISMATCH = System.Error | 112,
        EPROGUNAVAIL = System.Error | 113,
        EPROTO = System.Error | 114,
        EPROTONOSUPPORT = System.Error | 115,
        EPROTOTYPE = System.Error | 116,
        ERANGE = System.Error | 117,
        EREMCHG = System.Error | 118,
        EREMOTE = System.Error | 119,
        EREMOTEIO = System.Error | 120,
        ERESTART = System.Error | 121,
        EROFS = System.Error | 122,
        ERPCMISMATCH = System.Error | 123,
        ESHUTDOWN = System.Error | 124,
        ESOCKTNOSUPPORT = System.Error | 125,
        ESPIPE = System.Error | 126,
        ESRCH = System.Error | 127,
        ESRMNT = System.Error | 128,
        ESTALE = System.Error | 129,
        ESTRPIPE = System.Error | 130,
        ETIME = System.Error | 131,
        ETIMEDOUT = System.Error | 132,
        ETOOMANYREFS = System.Error | 133,
        ETXTBSY = System.Error | 134,
        EUCLEAN = System.Error | 135,
        EUNATCH = System.Error | 136,
        EUSERS = System.Error | 137,
        EWOULDBLOCK = System.Error | 138,
        EXDEV = System.Error | 139,
        EXFULL = System.Error | 140,
    }
}
