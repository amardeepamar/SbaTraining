using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectionPlanner.Models.BAL
{
    public static class General
    {
        public static class ProcessType
        {
            public static int Login = 1;
            public static int UserAdd = 2;
            public static int UserUpdate = 3;
            public static int UserDelete = 4;
        }

        public static class TableName
        {
            public static string Login = "Login";
            public static string Users = "Users";
        }
        public static class Messages
        {
            public static int AddSuccess = 1;
            public static int EmptyArea = 2;
            public static int UpdateSuccess = 3;
            public static int ImageMissing = 4;
            public static int ExtensionError = 5;
            public static int GeneralError = 6;
        }
    }
}
