using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EplannerClone.ViewModels
{
    public class ActivityViewModel
    {
        [Required]
        public int Id { get; set; }
        [Required(ErrorMessage = "Please select a pollday sublayer.")]
        public int PollDaySubLayer { get; set; }
        [Required]
        public int Remark { get; set; }
        [Required(ErrorMessage ="Please select district.")]
        public int District { get; set; }
        [Required(ErrorMessage ="Please select ac.")]
        public int Ac { get; set; }
        [Required]
        public int UserId { get; set; }
        public List<PollDaySubLayerViewModel> psslVM { get; set; }
        public List<DistrictsViewModel> districtsViewModels { get; set; }
        public List<AcViewModel> acViewModels { get; set; }
        public List<RemarkViewModel> remarkViewModels { get; set; }
        public List<UserViewModel> userViewModels { get; set; }

    }
}
