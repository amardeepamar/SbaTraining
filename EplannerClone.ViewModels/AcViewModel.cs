using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EplannerClone.ViewModels
{
    public class AcViewModel
    {
        [Required(ErrorMessage ="Ac no is required.")]
        public int AC_NO { get; set; }
        [Required(ErrorMessage ="District no is required.")]
        public int DIST_NO { get; set; }
        [Required(ErrorMessage ="Ac Name required.")]
        public string AC_NAME_EN { get; set; }
    }
}
