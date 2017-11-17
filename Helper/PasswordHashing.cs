using Microsoft.AspNetCore.Identity;
using diary.Models;
using System;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace diary.Helper
{
    public class PasswordHashing : IPasswordHasher<User>
    {
        public PasswordHashing()
        {

        }

        public string HashPassword(User user, string password)
        {
            // this user is a registering user

            var data = System.Text.Encoding.ASCII.GetBytes(password);
            data = System.Security.Cryptography.SHA1.Create().ComputeHash(data);
            password = Convert.ToBase64String(data);

            return password;
        }
        public PasswordVerificationResult VerifyHashedPassword(User user, string userpwd, string pwd)
        {
            if (userpwd == HashPassword(user, pwd))
                return PasswordVerificationResult.Success;
            else
                return PasswordVerificationResult.Failed;
        }
    }
}