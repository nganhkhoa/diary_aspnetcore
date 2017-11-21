using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace diary.Models.ScheduleViewModels
{
    
   public class UploadDatetimeModel
    {
        [Required]
        public string strUpload { get; set; }
        [Required]
        public DateTime Datetime { get; set; }
      
    }


}
