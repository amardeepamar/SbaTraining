using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectionPlanner.Models.DAL
{
    public class EpContext
    {
        public static EpEntities db = new EpEntities();
    }
    
}
