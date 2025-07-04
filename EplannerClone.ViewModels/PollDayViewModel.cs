using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EplannerClone.ViewModels
{
    public class PollDayViewModel
    {
        [Required]
        public int PollDayId { get; set; }
        [Required]
        public string PollDayNameEn { get; set; }
        [Required]
        public string PollDayNameHn { get; set; }
    }
}
