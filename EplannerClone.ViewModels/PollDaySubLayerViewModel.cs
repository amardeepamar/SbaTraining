using EplannerClone.DomainModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EplannerClone.ViewModels
{
    public class PollDaySubLayerViewModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string PollDaySubLayerNameEn { get; set; }
        [Required]
        public int Starting_day { get; set; }
        [Required]
        public int Ending_day { get; set; }

        [ForeignKey("Id")]
        public int PollDayId { get; set; }
        public List<PollDay> PdaysList { get; set; }
    }
}

