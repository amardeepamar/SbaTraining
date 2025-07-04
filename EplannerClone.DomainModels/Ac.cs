using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EplannerClone.DomainModels
{
    public class Ac
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AC_NO { get; set; }
        public int DIST_NO { get; set; }
        public string AC_NAME_HN { get; set; }
        public string AC_NAME_EN { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime AddDate { get; set; }
    }
}
