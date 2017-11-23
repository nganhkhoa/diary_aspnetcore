using System;
using diary.Models;

namespace diary.Models.ScheduleViewModels
{
      public class NewEventModel
      {
            public string Info { get; set; }
            public DateTime StartDate { get; set; }
            public DateTime EndDate { get; set; }
            public DateTime StartTime { get; set; }
            public DateTime EndTime { get; set; }
            public Occurrency Occurrence { get; set; }

      }
}