using System;
using System.Collections.Generic;
using System.Text;

namespace Logics.Services.Encryption
{
    public interface IEncryptionService
    {
        string Encrypt(string toEncrypt, bool useHashing);
        string Decrypt(string cipherString, bool useHashing);
    }
}
