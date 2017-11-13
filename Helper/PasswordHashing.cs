using Microsoft.AspNetCore.Identity;
using diary.Models;

namespace diary.Helper
{
      public class PasswordHashing : IPasswordHasher<User>
      {
            public PasswordHashing()
            {
            }
            public string HashPassword(User user, string password)
            {
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