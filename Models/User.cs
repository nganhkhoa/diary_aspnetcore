using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace diary.Models
{
      public class User : IdentityUser
      {
            // Id, UserName, Email are in IdentityUser

            public string FirstName { get; set; }
            public string LastName { get; set; }
            public DateTime Brithday { get; set; }

            // navigation properties
            public virtual ICollection<Event> Events { get; set; }
            public virtual ICollection<Entry> Entries { get; set; }
      }
}