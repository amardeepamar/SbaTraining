using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EplannerClone.DomainModels
{
    public class PollDaySubLayer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string PollDaySubLayerNameEn { get; set; }
        public string PollDaySubLayerNameHn { get; set; }
        public int PollDayId { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime DeletedDate { get; set; }
        public DateTime AddDate { get; set; }
        public DateTime LastUpdateDate { get; set; }
        public int Starting_day { get; set; }
        public int Ending_day { get; set; }
    }
}
