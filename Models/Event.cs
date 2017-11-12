using System;

namespace diary.Models
{
      public enum Occurrency
      {
            never,
            once_a_day,
            once_a_week,
            once_a_month,
            once_a_year
      };

      public class Event
      {
            public int ID { get; set; }
            public string Info { get; set; }
            public DateTime StartDate { get; set; }
            public DateTime EndDate { get; set; }
            public Occurrency Occurrence { get; set; }

            // navigation properties
            public User User { get; set; }
      }
}