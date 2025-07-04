using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace EplannerClone.DomainModels
{
    [Table("Users")]
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string ContactNo { get; set; }
        public int RoleId { get; set; } // Store the selected Role
        public bool IsDeleted { get; set; } = false;
        public DateTime DeletedDate { get; set; }
        public DateTime AddDate { get; set; } = DateTime.Now;
        public int LastUpdateUserId { get; set; }
        public DateTime LastUpdateDate { get; set; } = DateTime.Now;

    }
}
