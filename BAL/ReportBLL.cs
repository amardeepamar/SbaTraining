using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BAL
{
    public class ReportBLL
    {
        ReportDAO dao = new ReportDAO();
        public List<EmployeeInfo_EmployeeTrainingVM> GetTrainingInfo(SessionDTO session)
        {
            return dao.GetTrainingInfo(session);
        }
    }
}
