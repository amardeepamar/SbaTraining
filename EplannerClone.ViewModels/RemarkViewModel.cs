using System.ComponentModel.DataAnnotations;

namespace EplannerClone.ViewModels
{
    public class RemarkViewModel
    {
        [Required]
        public int RemarkId { get; set; }

        [Required]
        public string RemarkName { get; set; }
    }
}
