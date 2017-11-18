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


        public string HashPassword(string password)
        {
            byte[] salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            password = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));

            return password;
        }


        public string HashPassword(User user, string password)
        {
            //// this user is a registering user

            byte[] salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            password = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));

            // has password implementation
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