using DTO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DAL
{

    public class ErrorLogService : ShsTrainingContext
    {
        public static void LogError(Exception exception)
        {
            // Log the error to the database or a log file
            using (var db = new shsbTrainingEntities())
            {
                var errorLog = new ErrorLog
                {
                    ErrorMessage = exception.Message,
                    StackTrace = exception.StackTrace,
                    ErrorDate = DateTime.UtcNow
                };

                db.ErrorLogs.Add(errorLog);
                db.SaveChanges(); // This will "Create" the log in the database
            }
        }

        public static List<ErrorLog> GetErrorLogs()
        {
            using (var db = new shsbTrainingEntities())
            {
                return db.ErrorLogs.ToList(); // "Read" all logs
            }
        }

        public static void UpdateErrorLog(int errorLogId, string newMessage)
        {
            using (var db = new shsbTrainingEntities())
            {
                var log = db.ErrorLogs.FirstOrDefault(e => e.ErrorId == errorLogId);
                if (log != null)
                {
                    log.ErrorMessage = newMessage; // Update the log message
                    db.SaveChanges();
                }
            }
        }

        public static void DeleteErrorLog(int errorLogId)
        {
            using (var db = new shsbTrainingEntities())
            {
                var log = db.ErrorLogs.FirstOrDefault(e => e.ErrorId == errorLogId);
                if (log != null)
                {
                    db.ErrorLogs.Remove(log); // Delete the log
                    db.SaveChanges();
                }
            }
        }
    }

}
