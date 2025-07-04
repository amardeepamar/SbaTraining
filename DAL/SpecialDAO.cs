using System;
using System.Collections.Generic;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using DAL;

namespace DAL
{
    public class SpecialDAO : ShsTrainingContext
    {
        public static IEnumerable<SelectListItem> GetBatchForDdl()
        {
            IEnumerable<SelectListItem> batchList = db.tblBatchMasters.Where(x => x.IsDeleted == false).OrderBy(x => x.BatchId).Select(x => new SelectListItem()
            {
                Text = x.BatchName,
                Value = SqlFunctions.StringConvert((double)x.BatchId).Trim()
            }).ToList();
            return batchList;
        }

        public static IEnumerable<SelectListItem> GetBlockForDdl()
        {
            IEnumerable<SelectListItem> BlockList = db.BlockMasters.Where(x => x.IsDeleted == false).OrderByDescending(x => x.AddDate).Select(x => new SelectListItem()
            {
                Text = x.BlockNameEn,
                Value = SqlFunctions.StringConvert((double)x.Id).Trim()
            }).ToList();
            return BlockList;
        }
        public static IEnumerable<SelectListItem> GetBlockForDdl(int districtid)
        {
            IEnumerable<SelectListItem> BlockList = db.BlockMasters.Where(x => x.IsDeleted == false && x.DistrictId == districtid).OrderByDescending(x => x.AddDate).Select(x => new SelectListItem()
            {
                Text = x.BlockNameEn,
                Value = SqlFunctions.StringConvert((double)x.Id).Trim()
            }).ToList();
            return BlockList;
        }
        public static IEnumerable<SelectListItem> GetDesignationForDdl()
        {
            IEnumerable<SelectListItem> DesignationList = db.DesignationMasters.Where(x => x.IsDeleted == false ).OrderByDescending(x => x.AddDate).Select(x => new SelectListItem()
            {
                Text = x.DesignationEn,
                Value = SqlFunctions.StringConvert((double)x.DesignationId).Trim()
            }).ToList();
            return DesignationList;
        }
       

        public static IEnumerable<SelectListItem> GetEmployeeInfoNameWithMobileForDdl()
        {
            IEnumerable<SelectListItem> EmployeeInfoList = db.Employee_info.Where(e => e.DesignationId == e.DesignationMaster.DesignationId && !db.Employee_Training.Any(t => t.Employee_infoId == e.Id) && e.IsDeleted == false).Select(e => new SelectListItem()
            {
                Text = e.Name + "-" + e.MobileNumber + "-" + e.FacilityMaster.FacilityName,
                Value = SqlFunctions.StringConvert((double)e.Id).Trim()
            }).ToList();
            return EmployeeInfoList;
        }
        public static IEnumerable<SelectListItem> GetEmployeeInfoNameWithMobileForDdl(int designationId)
        {
            IEnumerable<SelectListItem> EmployeeInfoList = db.Employee_info.Where(e => e.DesignationId == designationId && !db.Employee_Training.Any(t => t.Employee_infoId == e.Id) && e.IsDeleted == false).Select(e => new SelectListItem()
            {
                Text = e.Name + "-" + e.MobileNumber + "-" + e.FacilityMaster.FacilityName,
                Value = SqlFunctions.StringConvert((double)e.Id).Trim()
            }).ToList();
            return EmployeeInfoList;
        }
        public static IEnumerable<SelectListItem> GetEmployeeInfoNameWithMobileForDdl(int designationId,int distid)
        {
            IEnumerable<SelectListItem> EmployeeInfoList = db.Employee_info.Where(e => e.DesignationId == designationId && e.DistrictId==distid && !db.Employee_Training.Any(t => t.Employee_infoId == e.Id) && e.IsDeleted == false).Select(e => new SelectListItem()
            {
                Text = e.Name + "-" + e.MobileNumber + "-"+ e.FacilityMaster.FacilityName,
                Value = SqlFunctions.StringConvert((double)e.Id).Trim()
            }).ToList();
            return EmployeeInfoList;
        }
        public static IEnumerable<SelectListItem> GetDistrictForDdl()
        {
            IEnumerable<SelectListItem> DistrictList = db.DistrictMasters.Where(x => x.IsDeleted == false).OrderByDescending(x => x.AddDate).Select(x => new SelectListItem()
            {
                Text = x.DistrictNameEn,
                Value = SqlFunctions.StringConvert((double)x.Id).Trim()
            }).ToList();
            return DistrictList;
        }
        public static IEnumerable<SelectListItem> GetDistrictForDdl(int distid)
        {
            IEnumerable<SelectListItem> DistrictList;

            // Check if distid is -1 to select all districts
            if (distid == -1)
            {
                DistrictList = db.DistrictMasters
                    .Where(x => x.IsDeleted == false)
                    .OrderByDescending(x => x.AddDate)
                    .Select(x => new SelectListItem
                    {
                        Text = x.DistrictNameEn,
                        Value = SqlFunctions.StringConvert((double)x.Id).Trim()
                    }).ToList();                
            }
            else
            {
                // Otherwise, select the specific district
                DistrictList = db.DistrictMasters.Where(x => x.IsDeleted == false && x.Id == distid).OrderByDescending(x => x.AddDate).Select(x => new SelectListItem()
                {
                    Text = x.DistrictNameEn,
                    Value = SqlFunctions.StringConvert((double)x.Id).Trim()
                }).ToList();
                
            }

            return DistrictList;

            //IEnumerable<SelectListItem> DistrictList = db.DistrictMasters.Where(x => x.IsDeleted == false && x.Id == distid).OrderByDescending(x => x.AddDate).Select(x => new SelectListItem()
            //{
            //    Text = x.DistrictNameEn,
            //    Value = SqlFunctions.StringConvert((double)x.Id).Trim()
            //}).ToList();
            //return DistrictList;
        }

        public static IEnumerable<SelectListItem> GetFacilityForDdl()
        {
            IEnumerable<SelectListItem> FacilityList = db.FacilityMasters.Where(x => x.IsDeleted == false).OrderByDescending(x => x.AddDate).Select(x => new SelectListItem()
            {
                Text = x.FacilityName,
                Value = SqlFunctions.StringConvert((double)x.FacilityId).Trim()
            }).ToList();
            return FacilityList;
        }
        public static IEnumerable<SelectListItem> GetFacilityForDdl(int blockid)
        {
            IEnumerable<SelectListItem> FacilityList = db.FacilityMasters.Where(x => x.IsDeleted == false && x.BlockID == blockid).OrderByDescending(x => x.AddDate).Select(x => new SelectListItem()
            {
                Text = x.FacilityName,
                Value = SqlFunctions.StringConvert((double)x.FacilityId).Trim()
            }).ToList();
            return FacilityList;
        }

        public static IEnumerable<SelectListItem> GetMonthYearForDdl()
        {
            IEnumerable<SelectListItem> MonthYearList = db.MonthYearMasters.Where(x => x.IsDeleted == false).OrderByDescending(x => x.MonthYearId).Select(x => new SelectListItem()
            {
                Text = x.MonthYearName,
                Value = SqlFunctions.StringConvert((double)x.MonthYearId).Trim()
            }).ToList();
            return MonthYearList;
        }

        public static IEnumerable<SelectListItem> GetRolesForDdl()
        {
            IEnumerable<SelectListItem> RolesList = db.RoleMasters.Where(x => x.IsDeleted == false).OrderByDescending(x => x.AddDate).Select(x => new SelectListItem()
            {
                Text = x.RoleName,
                Value = SqlFunctions.StringConvert((double)x.Id).Trim()
            }).ToList();
            return RolesList;
        }

        public static IEnumerable<SelectListItem> GetUsersForDdl()
        {
            IEnumerable<SelectListItem> UsersList = db.LoginMasters.Where(x => x.IsDeleted == false).OrderByDescending(x => x.AddDate).Select(x => new SelectListItem()
            {
                Text = x.Username,
                Value = SqlFunctions.StringConvert((double)x.Id).Trim()
            }).ToList();
            return UsersList;
        }
    }
}
