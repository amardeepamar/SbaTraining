using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL
{
    public class LogBLL
    {
        public static void AddLog(int Processtype, string TableName, int ProcessID,SessionDTO session)
        {
            LogDAO.AddLog(Processtype, TableName, ProcessID,session);
        }
        LogDAO dao = new LogDAO();
        public List<LogDTO> GetLogs()
        {
            return dao.GetLogs();
        }
    }
}
