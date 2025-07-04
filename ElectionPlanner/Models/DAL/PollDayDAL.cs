using ElectionPlanner.Models.DTO;
using ElectionPlanner.Models.DTO.Ddl;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.EnterpriseServices;
using System.Linq;
using System.Web;

namespace ElectionPlanner.Models.DAL
{
    
    public class PollDayDAL
    {
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["Db"].ConnectionString;

        // Get all PollDay
        public List<PollDayDTO> GetPollDay()
        {
            List<PollDayDTO> countries = new List<PollDayDTO>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT Id, PollDayNameEn FROM tblPollDayMaster";
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    countries.Add(new PollDayDTO
                    {
                        Id = reader.GetInt32(0),
                        PollDayNameEN = reader.GetString(1)
                    });
                }
            }
            return countries;
        }

        // Add a new PollDay
        public bool AddPollDay(PollDayDTO pollDay)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO tblPollDayMaster (PollDayNameEN,AddDate,LastUpdateDate) VALUES (@PollDayNameEN,GETDATE(),GETDATE())";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@PollDayNameEN", pollDay.PollDayNameEN);
                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }

        // Update an existing PollDay
        public bool UpdatePollDay(PollDayDTO country)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "UPDATE tblPollDayMaster SET PollDayNameEN = @PollDayNameEN,LastUpdateDate=GETDATE() WHERE Id = @Id";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@PollDayNameEN", country.PollDayNameEN);
                command.Parameters.AddWithValue("@Id", country.Id);
                connection.Open();
                return command.ExecuteNonQuery() > 0;
            }
        }

        public List<RemarksDTO> GetRemarks()
        {
            List<RemarksDTO> countries = new List<RemarksDTO>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT Id, RemarkName FROM tblRemarks";
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    countries.Add(new RemarksDTO
                    {
                        Id = reader.GetInt32(0),
                        RemarkName = reader.GetString(1)
                    });
                }
            }
            return countries;
        }


        public List<GetAllDistrictDTO> GetAllDistricts()
        {
            List<GetAllDistrictDTO> districts = new List<GetAllDistrictDTO>();
            string query = @"select d.Id,d.DistrictNameEn from districtmaster d
                            inner join StateMaster s on d.StateId=s.Id
                            inner join CountryMaster c on d.CountryId=c.Id";

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        districts.Add(new GetAllDistrictDTO
                        {
                            DistId = reader.GetInt32(0),
                            DistNameEn = reader.GetString(1)
                        });
                    }
                }
            }

            return districts;
        }

        public List<GetAllAcDTO> GetAcByDistrict(int districtId)
        {
            List<GetAllAcDTO> acList = new List<GetAllAcDTO>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT AC_NO, Ac_Name_En FROM tblAc WHERE DIST_NO=@DIST_NO";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@DIST_NO", districtId);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    acList.Add(new GetAllAcDTO
                    {
                        AcId = reader.GetInt32(0),
                        AcNameEn = reader.GetString(1)
                    });
                }
            }

            return acList;
        }

        //public ActivitiesDTO GetActivityByAc(int districtId, int acId)   // 1 time
        //{
        //    ActivitiesDTO activity = null;
        //    using (SqlConnection conn = new SqlConnection(connectionString))
        //    {
        //        // Note: Make sure your column names match your database schema.
        //        string query = @"select c.tblPollDaySubLayerMasterId,b.PollDaySubLayerNameEn,c.Remark,d.RemarkName
        //                    ,c.District,e.DistrictNameEn,c.Ac,f.AC_NAME_EN,c.UserId,l.Username
        //                    from [dbo].[tblPollDayMaster] a inner join
        //                    [dbo].[tblPollDaySubLayerMaster] b on a.id=b.polldayid left join
        //                    (select tblPollDaySubLayerMasterId,Remark,District,Ac,UserId from [dbo].[tblActivities] c 
        //                    where District=@District and Ac=@Ac) c on b.Id=c.tblPollDaySubLayerMasterId
        //                    inner join tblRemarks d on c.remark=d.Id
        //                    inner join DistrictMaster e on e.Id=c.District
        //                    inner join tblAc f on f.AC_NO=c.Ac
        //                    inner join LoginMasters l on l.Id=c.UserId";
        //        SqlCommand cmd = new SqlCommand(query, conn);
        //        cmd.Parameters.AddWithValue("@District", districtId);
        //        cmd.Parameters.AddWithValue("@Ac", acId);
        //        conn.Open();
        //        SqlDataReader reader = cmd.ExecuteReader();
        //        if (reader.Read())
        //        {
        //            activity = new ActivitiesDTO
        //            {
        //                tblPollDaySubLayerMasterId = reader.GetInt32(0),
        //                PollDaySubLayerNameEn = reader.GetString(1),
        //                Remark = reader["Remark"] != DBNull.Value ? reader.GetString(1) : "",
        //                RemarkName = reader.GetString(3),
        //                District = reader.GetInt32(2),
        //                DistrictNameEn = reader.GetString(5),
        //                Ac = reader.GetInt32(3),
        //                AC_NAME_EN = reader.GetString(7),
        //                UserId = reader.GetInt32(4),
        //                Username = reader.GetString(9)

        //            };
        //        }
        //    }
        //    return activity;
        //}

        public ActivitiesDTO GetActivityDetailsByAc(int district, int ac) // 2 time
        {
            ActivitiesDTO detailsList = null; 
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                // Parameterize the query so District and Ac are dynamic.
                string query = @"select c.tblPollDaySubLayerMasterId,b.PollDaySubLayerNameEn,c.Remark,d.RemarkName
                            ,c.District,e.DistrictNameEn,c.Ac,f.AC_NAME_EN,c.UserId,l.Username
                            from [dbo].[tblPollDayMaster] a inner join
                            [dbo].[tblPollDaySubLayerMaster] b on a.id=b.polldayid left join
                            (select tblPollDaySubLayerMasterId,Remark,District,Ac,UserId from [dbo].[tblActivities] c 
                            where District=@District and Ac=@Ac) c on b.Id=c.tblPollDaySubLayerMasterId
                            inner join tblRemarks d on c.remark=d.Id
                            inner join DistrictMaster e on e.Id=c.District
                            inner join tblAc f on f.AC_NO=c.Ac
                            inner join LoginMasters l on l.Id=c.UserId";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@District", district);
                cmd.Parameters.AddWithValue("@Ac", ac);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();                
                    if (reader.Read())
                    {
                        detailsList = new ActivitiesDTO
                        {
                            tblPollDaySubLayerMasterId = reader.GetInt32(0),
                            PollDaySubLayerNameEn = reader.GetString(1),
                            Remark =  reader.GetString(2),
                            RemarkName = reader.GetString(3),
                            District = reader.GetInt32(4),
                            DistrictNameEn = reader.GetString(5),
                            Ac = reader.GetInt32(6),
                            AC_NAME_EN = reader.GetString(7),
                            UserId = reader.GetInt32(8),
                            Username = reader.GetString(9)
                        };
                    }                
            }
            return detailsList;
        }

        public List<ActivitiesDTO> GetRemarksWithActivitiesJoin()
        {
            List<ActivitiesDTO> activities = new List<ActivitiesDTO>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"select d.Id as RemarkId, d.RemarkName,c.Remark,c.Ac,c.District,c.Id from  tblActivities c inner join tblRemarks d on c.remark=d.Id where d.Id=c.Remark";
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    activities.Add(new ActivitiesDTO
                    {
                        RemarkId = reader.GetInt32(0),
                        RemarkName = reader.GetString(1),
                        Remark = reader.GetString(2),
                        Ac=reader.GetInt32(3),
                        District=reader.GetInt32(4),
                        Id=reader.GetInt32(5)
                    });
                }
            }
            return activities;
        }


        //public List<GetAllAcDTO> GetAllAc(int districtId)
        //{
        //    List<GetAllAcDTO> ac = new List<GetAllAcDTO>();
        //    string query = @"SELECT AC_NO, Ac_Name_En FROM tblAc WHERE DIST_NO=@DIST_NO";

        //    using (SqlConnection connection = new SqlConnection(connectionString))
        //    using (SqlCommand command = new SqlCommand(query, connection))
        //    {
        //        command.Parameters.AddWithValue("@DIST_NO", districtId);
        //        connection.Open();
        //        using (SqlDataReader reader = command.ExecuteReader())
        //        {
        //            while (reader.Read())
        //            {
        //                ac.Add(new GetAllAcDTO
        //                {
        //                    AcId = reader.GetInt32(0),
        //                    AcNameEn = reader.GetString(1)
        //                });
        //            }
        //        }
        //    }

        //    return ac;
        //}
    }

   
}
