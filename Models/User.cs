using System;
using System.Collections.Generic;

namespace diary.Models
{
      public class User
      {
            public int ID { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Email { get; set; }
            public DateTime Brithday { get; set; }
            public string Username { get; set; }
            public string Password { get; set; }

            // navigation properties
            public ICollection<Event> Events { get; set; }
            public ICollection<Entry> Entries { get; set; }
      }
}