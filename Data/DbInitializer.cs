using diary.Models;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using diary.Helper;

namespace diary.Data
{
      public static class DbInitializer
      {
            public static void Initialize(diaryContext context)
            {
                  if (context.Users.Any())
                  {
                        return;
                  }

                  var passwordhash = new PasswordHashing();

                  var users = new User[]
                  {
                        new User
                        {
                              Id = "1",
                              FirstName = "Khoa",
                              LastName = "Nguyen Anh",
                              Email = "khoa@oop.bk16.com",
                              Brithday = DateTime.Parse("1/1/1998"),
                              UserName = "khoa",
                              NormalizedUserName = "KHOA",
                              PasswordHash = passwordhash.HashPassword(null, "12345678"),
                              SecurityStamp = "randomestring"
                        },
                        new User
                        {
                              Id = "2",
                              FirstName = "Khoi",
                              LastName = "Tran Dang",
                              Email = "khoi@oop.bk16.com",
                              Brithday = DateTime.Parse("2/2/1998"),
                              UserName = "khoi",
                              NormalizedUserName = "KHOI",
                              PasswordHash = passwordhash.HashPassword(null, "12345678"),
                              SecurityStamp = "randomestring"
                        },
                        new User
                        {
                              Id = "3",
                              FirstName = "Duy",
                              LastName = "Ung Van",
                              Email = "duy@oop.bk16.com",
                              Brithday = DateTime.Parse("3/3/1998"),
                              UserName = "duy",
                              NormalizedUserName = "DUY",
                              PasswordHash = passwordhash.HashPassword(null, "12345678"),
                              SecurityStamp = "randomestring"
                        },
                        new User
                        {
                              Id = "4",
                              FirstName = "Chi",
                              LastName = "Le Vinh",
                              Email = "chi@oop.bk16.com",
                              Brithday = DateTime.Parse("4/4/1998"),
                              UserName = "chi",
                              NormalizedUserName = "CHI",
                              PasswordHash = passwordhash.HashPassword(null, "12345678"),
                              SecurityStamp = "randomestring"
                        }
                  };

                  var events = new Event[]
                  {
                        new Event
                        {
                              ID = 1,
                              User = users[0],
                              Info = "Birthday",
                              StartDate = DateTime.Parse("1/1/2017"),
                              EndDate = DateTime.Parse("2/1/2017"),
                              Occurrence = Occurrency.once_a_year
                        },
                        new Event
                        {
                              ID = 2,
                              User = users[1],
                              Info = "Birthday",
                              StartDate = DateTime.Parse("2/2/2017"),
                              EndDate = DateTime.Parse("3/2/2017"),
                              Occurrence = Occurrency.once_a_year
                        },
                        new Event
                        {
                              ID = 3,
                              User = users[2],
                              Info = "Birthday",
                              StartDate = DateTime.Parse("3/3/2017"),
                              EndDate = DateTime.Parse("4/3/2017"),
                              Occurrence = Occurrency.once_a_year
                        },
                        new Event
                        {
                              ID = 4,
                              User = users[3],
                              Info = "Birthday",
                              StartDate = DateTime.Parse("4/4/2017"),
                              EndDate = DateTime.Parse("5/4/2017"),
                              Occurrence = Occurrency.once_a_year
                        }
                  };


                  var entries = new Entry[]
                  {
                        new Entry
                        {
                              ID = 1,
                              User = users[0],
                              Title = "Cong tac xa hoi",
                              Content = "Di cong tac xa hoi",
                              Date = DateTime.Parse("11/11/2017"),
                              Mood = Mood.happy
                        },
                        new Entry
                        {
                              ID = 2,
                              User = users[1],
                              Title = "OOP",
                              Content = "Hoc oop",
                              Date = DateTime.Parse("11/11/2017"),
                              Mood = Mood.normal
                        },
                        new Entry
                        {
                              ID = 3,
                              User = users[2],
                              Title = "Late night",
                              Content = "On fackbook khuya",
                              Date = DateTime.Parse("11/11/2017"),
                              Mood = Mood.normal
                        },
                        new Entry
                        {
                              ID = 4,
                              User = users[3],
                              Title = "????",
                              Content = "Mat tieu",
                              Date = DateTime.Parse("11/11/2017"),
                              Mood = Mood.normal
                        }
                  };

                  /*
                  foreach (User u in users)
                  {
                        u.Entries = new Collection<Entry>();
                        u.Events = new Collection<Event>();
                  }

                  for (int i = 0; i < users.Length; ++i)
                  {
                        users[i].Entries.Add(entries[i]);
                        users[i].Events.Add(events[i]);
                  }
                  */

                  // add user to database
                  foreach (User u in users)
                        context.Add(u);
                  context.SaveChanges();

                  foreach (Event e in events)
                        context.Add(e);
                  context.SaveChanges();

                  foreach (Entry e in entries)
                        context.Add(e);
                  context.SaveChanges();
            }
      }
}