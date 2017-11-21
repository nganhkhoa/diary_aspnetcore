﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using diary.Models.ScheduleViewModels;

namespace diary.Models
{
      public class ModelCollection
      {
            public UploadDatetimeModel udModel { get; set; }
            public IEnumerable<Entry> entryList { get; set; }

            public string UserName { get; set; }
      }
}
