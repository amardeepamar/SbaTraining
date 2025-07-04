using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EplannerClone.DomainModels
{
    public class District
    {
        [Key]
        public int Id { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DistCode { get; set; }
        public string DistrictNameEn { get; set; }
        public string DistrictNameHn { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedDate { get; set; }
        public DateTime AddDate { get; set; }
        public DateTime LastUpdateDate { get; set; }
    }
}
