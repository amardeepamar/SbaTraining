using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ElectionPlanner.Models.DTO.Ddl
{
    public class GetAllStateDTO
    {
        public int StateId { get; set; }
        public string StateNameEn { get; set; }
        public bool IsStateIdReadOnly { get; set; }
    }
}