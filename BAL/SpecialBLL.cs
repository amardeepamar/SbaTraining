using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BAL
{
    public class SpecialBLL
    {
        public static IEnumerable<SelectListItem> GetRolesForDdl()
        {
            return SpecialDAO.GetRolesForDdl();
        }

        public static IEnumerable<SelectListItem> GetDistrictForDdl()
        {
            return SpecialDAO.GetDistrictForDdl();
        }
        public static IEnumerable<SelectListItem> GetDistrictForDdl(int distrid)
        {
            return SpecialDAO.GetDistrictForDdl(distrid);
        }
        public static IEnumerable<SelectListItem> GetBlockForDdl()
        {
            return SpecialDAO.GetBlockForDdl();
        }
        public static IEnumerable<SelectListItem> GetBlockForDdl(int districtid)
        {
            return SpecialDAO.GetBlockForDdl(districtid);
        }

        public static IEnumerable<SelectListItem> GetUsersForDdl()
        {
            return SpecialDAO.GetUsersForDdl();
        }
        public static IEnumerable<SelectListItem> GetFacilityForDdl()
        {
            return SpecialDAO.GetFacilityForDdl();
        }
        public static IEnumerable<SelectListItem> GetFacilityForDdl(int blockid)
        {
            return SpecialDAO.GetFacilityForDdl(blockid);
        }
        public static IEnumerable<SelectListItem> GetDesignationForDdl()
        {
            return SpecialDAO.GetDesignationForDdl();
        }

       

        public static IEnumerable<SelectListItem> GetMonthYearForDdl()
        {
            return SpecialDAO.GetMonthYearForDdl();
        }

        public static IEnumerable<SelectListItem> GetEmployeeInfoNameWithMobileForDdl()
        {
            return SpecialDAO.GetEmployeeInfoNameWithMobileForDdl();
        }
        public static IEnumerable<SelectListItem> GetEmployeeInfoNameWithMobileForDdl(int designationId)
        {
            return SpecialDAO.GetEmployeeInfoNameWithMobileForDdl(designationId);
        }

        public static IEnumerable<SelectListItem> GetEmployeeInfoNameWithMobileForDdl(int designationId, int distid)
        {
            return SpecialDAO.GetEmployeeInfoNameWithMobileForDdl(designationId,  distid);
        }
        public static IEnumerable<SelectListItem> GetBatchForDdl()
        {
            return SpecialDAO.GetBatchForDdl();
        }

    }
}
