using System;

namespace diary.Models

{
      public enum Mood
      {
            inlove,
            joyful,
            happy,
            peaceful,
            normal,
            a_bit_blue,
            sad,
            depress,
            gloomy
      };
      public class Entry
      {
            public int ID { get; set; }
            public string Title { get; set; }
            public string Content { get; set; }
            public DateTime Date { get; set; }
            public Mood? Mood { get; set; }

            // navigation properties
            public virtual User User { get; set; }
      }
}