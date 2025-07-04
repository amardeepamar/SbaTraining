using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EplannerClone.DomainModels
{
    public class Activity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int PollDaySubLayer { get; set; }
        public int Remark { get; set; }
        public int District { get; set; }
        public int Ac { get; set; }
        public DateTime Action_Date { get; set; }
        public int UserId { get; set; }
    }
}
