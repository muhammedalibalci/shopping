using Microsoft.AspNetCore.DataProtection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Helpers
{
    public interface IDataProtector : IDataProtectionProvider
    {
        string Encrypt(string cipherText);
        string Decrypt(string cipherText);
    }
}
