using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EplannerClone.DomainModels
{
    public class PollDay
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string PollDayNameEn { get; set; }
        public string PollDayNameHn { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime DeletedDate { get; set; }
        public DateTime AddDate { get; set; }
        public DateTime LastUpdateDate { get; set; }
    }
}
