using ElectionPlanner.Models.DTO;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

namespace ElectionPlanner.Models.DAL
{
    public class PollDaySubLayerDAL
    {
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["Db"].ConnectionString;

        // Get all PollDay
        public List<PollDaySubLayerDTO> GetPollDaySubLayer()
        {
            List<PollDaySubLayerDTO> countries = new List<PollDaySubLayerDTO>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"SELECT pds.Id, pds.PollDaySubLayerNameEn,pd.Id,pd.PollDayNameEn,
                    -isnull(pds.starting_day,0) as starting_day, -isnull(pds.ending_day,0) as ending_day, isnull((-pds.starting_day+ ending_day)+1,0) as duration
                                FROM tblPollDaySubLayerMaster as pds 
                                inner join tblPollDayMaster as pd on pds.PollDayId = pd.Id order by pds.Id";



                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    countries.Add(new PollDaySubLayerDTO
                    {
                        Id = reader.GetInt32(0),
                        PollDaySubLayerNameEN = reader.GetString(1),
                        PollDayId = reader.GetInt32(2),
                        PollDayNameEN = reader.GetString(3),
                        StartingDay = reader.GetInt32(4),
                        EndingDay = reader.GetInt32(5),
                        Duration = reader.GetInt32(6)
                    });
                }
            }
            return countries;
        }

        // Add a new PollDay
        public bool AddPollDaySubLayer(PollDaySubLayerDTO pollDaySubLayer)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                int duration = (int)(pollDaySubLayer.EndingDay - pollDaySubLayer.StartingDay) + 1;
                string query = @"INSERT INTO tblPollDaySubLayerMaster (PollDaySubLayerNameEN,PollDayId,starting_day,ending_day,duration,AddDate,LastUpdateDate)
                            VALUES (@PollDaySubLayerNameEN,@PollDayId,@starting_day,@ending_day, @duration,GETDATE(),GETDATE())";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@PollDaySubLayerNameEN", pollDaySubLayer.PollDaySubLayerNameEN);

                command.Parameters.AddWithValue("@starting_day", pollDaySubLayer.StartingDay);
                command.Parameters.AddWithValue("@ending_day", pollDaySubLayer.EndingDay);
                command.Parameters.AddWithValue("@duration", duration);

                command.Parameters.AddWithValue("@PollDayId", pollDaySubLayer.PollDayId);
                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }

        // Update an existing PollDaySubLayer
        public bool UpdatePollDaySubLayer(PollDaySubLayerDTO SubLayer)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "UPDATE tblPollDaySubLayerMaster SET PollDaySubLayerNameEN = @PollDaySubLayerNameEN,PollDayId=@PollDayId,starting_day=@starting_day,ending_day=@ending_day,duration=@duration,LastUpdateDate=GETDATE() WHERE Id = @Id";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@PollDaySubLayerNameEN", SubLayer.PollDaySubLayerNameEN);
                command.Parameters.AddWithValue("@PollDayId", SubLayer.PollDayId);
                command.Parameters.AddWithValue("@starting_day", SubLayer.StartingDay);
                command.Parameters.AddWithValue("@ending_day", SubLayer.EndingDay);
                command.Parameters.AddWithValue("@duration", SubLayer.Duration);
                command.Parameters.AddWithValue("@Id", SubLayer.Id);
                connection.Open();
                return command.ExecuteNonQuery() > 0;
            }
        }

        public List<ActivitiesDTO> GetActivities()
        {
            List<ActivitiesDTO> pollDays = new List<ActivitiesDTO>();
            string query = @"SELECT t2.id, t1.PollDayNameEn, t2.PollDaySubLayerNameEn, 
                        -t2.starting_day as starting_day, -t2.ending_day as ending_day, (-t2.starting_day+ending_day)+1 as duration,  
                        DATEADD(DAY, t2.starting_day, CAST('28 Oct 2025' AS DATE)) AS 'TentativeStartingDate', 
                        DATEADD(DAY, t2.ending_day, CAST('28 Oct 2025' AS DATE)) AS 'TentativeEndingDate'
                        , '' AS Remark 
                        FROM tblPollDayMaster t1 
                        INNER JOIN tblPollDaySubLayerMaster t2 ON t1.Id = t2.PolldayId where t2.id<5";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    pollDays.Add(new ActivitiesDTO
                    {
                        Id = Convert.ToInt32(reader["id"]),
                        PollDayNameEn = reader["PollDayNameEn"].ToString(),
                        PollDaySubLayerNameEn = reader["PollDaySubLayerNameEn"].ToString(),
                        StartingDay = Convert.ToInt32(reader["starting_day"]),
                        EndingDay = Convert.ToInt32(reader["ending_day"]),
                        Duration = Convert.ToInt32(reader["duration"]),
                        TentativeStartingDate = Convert.ToDateTime(reader["TentativeStartingDate"]),
                        TentativeEndingDate = Convert.ToDateTime(reader["TentativeEndingDate"]),
                        Remark = reader["Remark"].ToString()
                    });
                }
            }
            return pollDays;
        }


        public bool SaveActivity(ActivitiesDTO activity)
        {

            string strQry = @"select * from tblActivities where tblPollDaySubLayerMasterId=@tblPollDaySubLayerMasterId and District=@District and Ac=@Ac";
            
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(strQry, connection);
                command.Parameters.AddWithValue("@tblPollDaySubLayerMasterId", activity.Id);
                command.Parameters.AddWithValue("@District", activity.District);
                command.Parameters.AddWithValue("@Ac", activity.Ac);
                SqlDataAdapter da = new SqlDataAdapter(command);
                DataTable dt = new DataTable();
                da.Fill(dt);




                if (dt.Rows.Count == 0)
                {                    
                    string query = @"INSERT INTO tblActivities (tblPollDaySubLayerMasterId, Remark,District ,Ac,UserId)  VALUES (@tblPollDaySubLayerMasterId, @Remark,@District ,@Ac,@UserId)";
                    command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@tblPollDaySubLayerMasterId", activity.Id);
                    command.Parameters.AddWithValue("@Remark", activity.Remark);
                    command.Parameters.AddWithValue("@District", activity.District);
                    command.Parameters.AddWithValue("@Ac", activity.Ac);
                    command.Parameters.AddWithValue("@UserId", 1);
                    connection.Open();
                    return command.ExecuteNonQuery() > 0;
                }
                else
                {
                    string query = @"Update tblActivities set Remark = @Remark  where tblPollDaySubLayerMasterId=@tblPollDaySubLayerMasterId and District=@District and Ac=@Ac";
                    command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@tblPollDaySubLayerMasterId", activity.Id);
                    command.Parameters.AddWithValue("@Remark", activity.Remark);
                    command.Parameters.AddWithValue("@District", activity.District);
                    command.Parameters.AddWithValue("@Ac", activity.Ac);
                    command.Parameters.AddWithValue("@UserId",1);
                    connection.Open();
                    return command.ExecuteNonQuery() > 0;

                }
            }
        }

        public ActivitiesDTO GetSavedRemark(int activityId)
        {
            ActivitiesDTO remark = null;

            string query = "SELECT Remark FROM tblActivities WHERE Id = @Id";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", activityId);
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    remark = new ActivitiesDTO
                    {
                        Remark = reader.GetString(0)
                    };
                }
            }

            return remark;
        }

    }
}