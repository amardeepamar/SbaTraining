using DTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace DAL
{
    public class Dall
    {
        public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Db"].ConnectionString);
        public List<ReportVM> GetCustomerList(int districtid)
        {
            List<ReportVM> list = new List<ReportVM>();
            SqlCommand cmd = new SqlCommand("SP_Report_TotalREgistration", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@DistID", districtid);
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                list.Add(new ReportVM
                {
                    SN = Convert.ToInt32(dr[0]),
                    DistrictNameEn = Convert.ToString(dr[1]),
                    TotalTrainedStaffNurse = Convert.ToString(dr[2]),
                    TotalUnTrainedStaffNurse = Convert.ToString(dr[3]),
                    TotalTrainedANM = Convert.ToString(dr[4]),
                    TotalUnTrainedANM = Convert.ToString(dr[5])
                });
            }
            return list;
        }
        
    }
}
