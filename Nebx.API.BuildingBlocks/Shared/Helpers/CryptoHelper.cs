﻿using System.Text;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace Nebx.API.BuildingBlocks.Shared.Helpers;

public static class CryptoHelper
{
    public static byte[] DerivationKey(string key, string salt, int bytesLength)
    {
        var saltedKey = KeyDerivation.Pbkdf2(
            password: key,
            salt: Encoding.UTF8.GetBytes(salt),
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: 10000,
            numBytesRequested: bytesLength
        );

        return saltedKey;
    }
}