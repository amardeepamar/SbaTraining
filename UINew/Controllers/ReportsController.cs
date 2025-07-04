using DAL;
using DTO;
using OfficeOpenXml;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Font = iTextSharp.text.Font;
using System;
using BAL;
using UINew.Models;

namespace UINew.Controllers
{
    public class ReportsController : BaseController
    {
        Dall dal = new Dall();
        

        [HttpGet]
        public ActionResult SbaTrainedOrUntrained()
        {
            SessionDTO session = (SessionDTO)Session["UserInfo"];
            return View(dal.GetCustomerList(session.DistId));
        }

        #region District PDF And Excel 

        public ActionResult DistrictPDF(string searchDistrict)
        {
            var employeeData = GetDistrictEmployeeData(searchDistrict);
            MemoryStream workStream = new MemoryStream();
            Document document = new Document(PageSize.A4, 25, 25, 30, 30);
            PdfWriter.GetInstance(document, workStream).CloseStream = false;
            document.Open();
            Font paragraphFont = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 14, BaseColor.BLUE);
            Paragraph p1 = new Paragraph("State Health Society,Bihar", paragraphFont);
            Paragraph p2 = new Paragraph("SBA Training Employee Information Data", paragraphFont);
            Paragraph p3 = new Paragraph("  ");
            p1.Alignment = Element.ALIGN_CENTER;
            p1.SpacingAfter = 10f;
            p2.Alignment = Element.ALIGN_CENTER;
            p2.SpacingAfter = 10f;
            p3.SpacingAfter = 20f;
            document.Add(p1);
            document.Add(p2);

            // Create table with 8 columns
            PdfPTable table = new PdfPTable(9);
            table.WidthPercentage = 100;
            // Create a base cell style (simulates a "class" for table cells)
            PdfPCell baseCell = new PdfPCell();
            baseCell.HorizontalAlignment = Element.ALIGN_CENTER;
            baseCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            baseCell.BorderColor = BaseColor.GRAY;
            baseCell.BackgroundColor = BaseColor.LIGHT_GRAY;
            // Create a font style (similar to a "class" for text)
            Font boldFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12, BaseColor.BLACK);
            // Add a cell using this font style
            PdfPCell sn = new PdfPCell(new Phrase("S. No.", boldFont));
            sn.HorizontalAlignment = baseCell.HorizontalAlignment;
            sn.VerticalAlignment = baseCell.VerticalAlignment;
            sn.BorderColor = baseCell.BorderColor;
            sn.BackgroundColor = baseCell.BackgroundColor;
            table.AddCell(sn);
            PdfPCell empName = new PdfPCell(new Phrase("Employee Name", boldFont));
            empName.HorizontalAlignment = baseCell.HorizontalAlignment;
            empName.VerticalAlignment = baseCell.VerticalAlignment;
            empName.BorderColor = baseCell.BorderColor;
            empName.BackgroundColor = baseCell.BackgroundColor;
            table.AddCell(empName);
            PdfPCell mob = new PdfPCell(new Phrase("Mobile Number", boldFont));
            mob.HorizontalAlignment = baseCell.HorizontalAlignment;
            mob.VerticalAlignment = baseCell.VerticalAlignment;
            mob.BorderColor = baseCell.BorderColor;
            mob.BackgroundColor = baseCell.BackgroundColor;
            table.AddCell(mob);
            PdfPCell dist = new PdfPCell(new Phrase("District", boldFont));
            dist.HorizontalAlignment = baseCell.HorizontalAlignment;
            dist.VerticalAlignment = baseCell.VerticalAlignment;
            dist.BorderColor = baseCell.BorderColor;
            dist.BackgroundColor = baseCell.BackgroundColor;
            table.AddCell(dist);
            PdfPCell Block = new PdfPCell(new Phrase("Block", boldFont));
            Block.HorizontalAlignment = baseCell.HorizontalAlignment;
            Block.VerticalAlignment = baseCell.VerticalAlignment;
            Block.BorderColor = baseCell.BorderColor;
            Block.BackgroundColor = baseCell.BackgroundColor;
            table.AddCell(Block);
            PdfPCell Facility = new PdfPCell(new Phrase("Facility", boldFont));
            Facility.HorizontalAlignment = baseCell.HorizontalAlignment;
            Facility.VerticalAlignment = baseCell.VerticalAlignment;
            Facility.BorderColor = baseCell.BorderColor;
            Facility.BackgroundColor = baseCell.BackgroundColor;
            table.AddCell(Facility);
            PdfPCell status = new PdfPCell(new Phrase("Training Status", boldFont));
            status.HorizontalAlignment = baseCell.HorizontalAlignment;
            status.VerticalAlignment = baseCell.VerticalAlignment;
            status.BorderColor = baseCell.BorderColor;
            status.BackgroundColor = baseCell.BackgroundColor;
            table.AddCell(status);
            PdfPCell year = new PdfPCell(new Phrase("Training Completion Year", boldFont));
            year.HorizontalAlignment = baseCell.HorizontalAlignment;
            year.VerticalAlignment = baseCell.VerticalAlignment;
            year.BorderColor = baseCell.BorderColor;
            year.BackgroundColor = baseCell.BackgroundColor;
            table.AddCell(year);
            PdfPCell date = new PdfPCell(new Phrase("Created Date", boldFont));
            date.HorizontalAlignment = baseCell.HorizontalAlignment;
            date.VerticalAlignment = baseCell.VerticalAlignment;
            date.BorderColor = baseCell.BorderColor;
            date.BackgroundColor = baseCell.BackgroundColor;
            table.AddCell(date);
            // Add table rows (employee data with serial number)
            int serialNo = 1;  // Initialize serial number
            // Add table rows (employee data)
            foreach (var item in employeeData)
            {
                table.AddCell(serialNo++.ToString());  // Serial number
                table.AddCell(item.Name);
                table.AddCell(item.MobileNumber);
                table.AddCell(item.DistrictMaster.DistrictNameEn);
                table.AddCell(item.BlockMaster.BlockNameEn);
                table.AddCell(item.FacilityMaster.FacilityName);
                // Assuming item.Istrained is a boolean value (true/false)
                string isTrainedDisplay = (item.Istrained ?? false) ? "Yes" : "No";
                //string isTrainedDisplay = item.Istrained.HasValue? (item.Istrained.Value ? "Yes" : "No"): "N/A"; // Handle null case with "N/A" or any other value
                // Add it to the iTextSharp table
                table.AddCell(isTrainedDisplay);

                table.AddCell(item.TrainingCompletionYear?.ToString("MM/yyyy") ?? "N/A");
                table.AddCell(item.AddDate.ToString("dd/MM/yyyy"));
            }

            document.Add(table);
            document.Close();
            byte[] byteInfo = workStream.ToArray();
            workStream.Write(byteInfo, 0, byteInfo.Length);
            workStream.Position = 0;
            return File(workStream, "application/pdf", "DistrictByEmployeeData.pdf");
        }
        //public ActionResult DistrictExcel(string searchDistrict)
        //{
        //    SessionDTO session = (SessionDTO)Session["UserInfo"];
        //    List<Employee_info> employeeData = GetDistrictEmployeeData(searchDistrict);

        //    using (ExcelPackage package = new ExcelPackage())
        //    {

        //        ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("State Health Society,Bihar || SBA Training District by Employee Information Data");
        //        // Add additional line break for spacing
        //        worksheet.Cells[2, 1].Value = "";  // Empty row for spacing

        //        // Add header
        //        worksheet.Cells[3, 1].Value = "S. No.";
        //        worksheet.Cells[3, 2].Value = "Employee Name";
        //        worksheet.Cells[3, 3].Value = "Mobile Number";
        //        worksheet.Cells[3, 4].Value = "District";
        //        worksheet.Cells[3, 5].Value = "Block";
        //        worksheet.Cells[3, 6].Value = "Facility";
        //        worksheet.Cells[3, 7].Value = "Training Completion Year";
        //        worksheet.Cells[3, 8].Value = "Created Date";

        //        // Add data
        //        int row = 4;  // Start data from row 4 (below the header and title)
        //        int serialNo = 1;  // Initialize serial number
        //        foreach (var item in employeeData)
        //        {
        //            worksheet.Cells[row, 1].Value = serialNo++;  // Serial number
        //            worksheet.Cells[row, 2].Value = item.Name;
        //            worksheet.Cells[row, 3].Value = item.MobileNumber;
        //            worksheet.Cells[row, 4].Value = item.DistrictMaster.DistrictNameEn;
        //            worksheet.Cells[row, 5].Value = item.BlockMaster.BlockNameEn;
        //            worksheet.Cells[row, 6].Value = item.FacilityMaster.FacilityName;
        //            worksheet.Cells[row, 7].Value = item.TrainingCompletionYear?.ToString("MM/yyyy") ?? "N/A";
        //            worksheet.Cells[row, 8].Value = item.AddDate.ToString("dd/MM/yyyy");
        //            row++;
        //        }
        //        // Add extra title (e.g., "Report Generated for District XYZ")
        //        worksheet.Cells[1, 1].Value = "Report Generated for District: " + session.Username;
        //        worksheet.Cells[1, 1, 1, 8].Merge = true;  // Merge cells for the extra title
        //        worksheet.Cells[1, 1, 1, 8].Style.Font.Size = 16;  // Increase font size for the title
        //        worksheet.Cells[1, 1, 1, 8].Style.Font.Bold = true;  // Make the title bold
        //        worksheet.Cells[1, 1, 1, 8].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;  // Center align the title

        //        // Add header formatting
        //        using (var headerRange = worksheet.Cells[3, 1, 3, 8])
        //        {
        //            headerRange.Style.Font.Bold = true;
        //            headerRange.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
        //            headerRange.Style.Fill.BackgroundColor.SetColor(Color.LightGray);
        //        }

        //        var stream = new MemoryStream();
        //        package.SaveAs(stream);
        //        stream.Position = 0;
        //        return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "DistrictByEmployeeData.xlsx");
        //    }
        //}
        private List<Employee_info> GetDistrictEmployeeData(string searchDistrict)
        {
            SessionDTO session = (SessionDTO)Session["UserInfo"];
            if (session.Role == "1" || session.Role == "2")
            {
                var ei = db.Employee_info.Where(x => x.IsDeleted == false).AsQueryable();
                if (!string.IsNullOrEmpty(searchDistrict))
                {
                    ei = ei.Where(p => p.DistrictMaster.DistrictNameEn == searchDistrict).OrderBy(p => p.DistrictMaster.DistrictNameEn).Skip(1);
                }
                ViewBag.SelectedDistrict = searchDistrict;
                return ei.ToList();
            }
            else
            {
                var ei = db.Employee_info.Where(x => x.IsDeleted == false && x.DistrictMaster.Id == session.DistId).OrderBy(x => x.DistrictMaster.DistrictNameEn).Skip(1).AsQueryable();
                if (!string.IsNullOrEmpty(searchDistrict))
                {
                    ei = ei.Where(p => p.DistrictMaster.DistrictNameEn == searchDistrict).OrderBy(x => x.DistrictMaster.DistrictNameEn).Skip(1);
                }
                ViewBag.SelectedDistrict = searchDistrict;
                return ei.ToList();
            }
        }
        #endregion

        private shsbTrainingEntities db = new shsbTrainingEntities();
        // Make sure to install EPPlus for Excel generation

        public ActionResult GetDistrictwiseEmployeeInfo()
        {
            SessionDTO session = (SessionDTO)Session["UserInfo"];
            if (session.Role == "1" || session.Role == "2")
            {
                var districts = db.Employee_info
                                  .Where(x => x.IsDeleted == false)
                                  .OrderBy(x => x.DistrictMaster.DistrictNameEn)
                                  .Skip(1)
                                  .Select(p => p.DistrictMaster.DistrictNameEn)
                                  .Distinct()
                                  .ToList();
                ViewBag.Districts = new SelectList(districts);
            }
            else
            {
                var districts = db.Employee_info
                                  .Where(x => x.DistrictMaster.Id == session.DistId && x.IsDeleted == false)
                                  .OrderBy(x => x.DistrictMaster.DistrictNameEn)
                                  .Skip(1)
                                  .Select(p => p.DistrictMaster.DistrictNameEn)
                                  .Distinct()
                                  .ToList();
                ViewBag.Districts = new SelectList(districts);
            }
            return View();
        }

        [HttpPost]
        public ActionResult GetDistrictwiseEmployeeInfo(string searchDistrict)
        {
            SessionDTO session = (SessionDTO)Session["UserInfo"];
            IQueryable<Employee_info> products;

            if (session.Role == "1" || session.Role == "2")
            {
                products = db.Employee_info
                             .Where(x => x.IsDeleted == false)
                             .AsQueryable();
            }
            else
            {
                products = db.Employee_info
                             .Where(x => x.IsDeleted == false && x.DistrictMaster.Id == session.DistId)
                             .AsQueryable();
            }

            if (!string.IsNullOrEmpty(searchDistrict))
            {
                products = products.Where(p => p.DistrictMaster.DistrictNameEn.ToString() == searchDistrict)
                                   .OrderBy(x => x.DistrictMaster.DistrictNameEn)
                                   .Skip(1);
            }

            return PartialView("~/Views/Reports/Partial/_GetDistrictwiseEmployeeInformation.cshtml", products.ToList());
        }

        [HttpPost]
        public ActionResult ExportToExcel(string searchDistrict)
        {
            SessionDTO session = (SessionDTO)Session["UserInfo"];
            IQueryable<Employee_info> emp_info;

            if (session.Role == "1" || session.Role == "2")
            {
                emp_info = db.Employee_info
                             .Where(x => x.IsDeleted == false)
                             .AsQueryable();
            }
            else
            {
                emp_info = db.Employee_info
                             .Where(x => x.IsDeleted == false && x.DistrictMaster.Id == session.DistId)
                             .AsQueryable();
            }

            if (!string.IsNullOrEmpty(searchDistrict))
            {
                emp_info = emp_info.Where(p => p.DistrictMaster.DistrictNameEn.ToString() == searchDistrict)
                                   .OrderBy(x => x.DistrictMaster.DistrictNameEn)
                                   .Skip(1);
            }

            var employeeList = emp_info.ToList();

            // Create an Excel file
            using (ExcelPackage package = new ExcelPackage())
            {

                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("State Health Society,Bihar || SBA Training District by Employee Information Data");
                // Add additional line break for spacing
                worksheet.Cells[2, 1].Value = "";  // Empty row for spacing

                // Add header
                worksheet.Cells[3, 1].Value = "S. No.";
                worksheet.Cells[3, 2].Value = "Employee Name";
                worksheet.Cells[3, 3].Value = "Mobile Number";
                worksheet.Cells[3, 4].Value = "District";
                worksheet.Cells[3, 5].Value = "Block";
                worksheet.Cells[3, 6].Value = "Facility";
                worksheet.Cells[3, 7].Value = "Trained";
                worksheet.Cells[3, 8].Value = "Training Completion Year";
                worksheet.Cells[3, 9].Value = "Created Date";

                // Add data
                int row = 4;  // Start data from row 4 (below the header and title)
                int serialNo = 1;  // Initialize serial number
                foreach (var item in employeeList)
                {
                    worksheet.Cells[row, 1].Value = serialNo++;  // Serial number
                    worksheet.Cells[row, 2].Value = item.Name;
                    worksheet.Cells[row, 3].Value = item.MobileNumber;
                    worksheet.Cells[row, 4].Value = item.DistrictMaster.DistrictNameEn;
                    worksheet.Cells[row, 5].Value = item.BlockMaster.BlockNameEn;
                    worksheet.Cells[row, 6].Value = item.FacilityMaster.FacilityName;
                    worksheet.Cells[row, 7].Value = item.Istrained;
                    worksheet.Cells[row, 8].Value = item.TrainingCompletionYear?.ToString("MM/yyyy") ?? "N/A";
                    worksheet.Cells[row, 9].Value = item.AddDate.ToString("dd/MM/yyyy");
                    row++;
                }
                // Add extra title (e.g., "Report Generated for District XYZ")
                worksheet.Cells[1, 1].Value = "Report Generated for District: " + session.Username;
                worksheet.Cells[1, 1, 1, 8].Merge = true;  // Merge cells for the extra title
                worksheet.Cells[1, 1, 1, 8].Style.Font.Size = 16;  // Increase font size for the title
                worksheet.Cells[1, 1, 1, 8].Style.Font.Bold = true;  // Make the title bold
                worksheet.Cells[1, 1, 1, 8].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;  // Center align the title

                // Add header formatting
                using (var headerRange = worksheet.Cells[3, 1, 3, 8])
                {
                    headerRange.Style.Font.Bold = true;
                    headerRange.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    headerRange.Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                }

                var stream = new MemoryStream();
                package.SaveAs(stream);
                stream.Position = 0;
                return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "DistrictByEmployeeData.xlsx");
            }
        }

        ReportBLL bll = new ReportBLL();
        [LoginControl]
        public ActionResult GetTrainingInfo()
        {
            SessionDTO session = (SessionDTO)Session["UserInfo"];
            List<EmployeeInfo_EmployeeTrainingVM> model = new List<EmployeeInfo_EmployeeTrainingVM>();
            model = bll.GetTrainingInfo(session);
            return View(model);
        }



        [HttpGet]
        public ActionResult GetDistrictBlockEmployeeInfo()
        {
            SessionDTO session = (SessionDTO)Session["UserInfo"];
            if (session.Role == "1" || session.Role == "2")
            {
                var districts = db.Employee_info.Where(x => x.IsDeleted == false).Select(p => p.DistrictMaster.DistrictNameEn).Distinct().ToList();
                var blocks = db.Employee_info.Where(x => x.IsDeleted == false).Select(p => p.BlockMaster.BlockNameEn).Distinct().ToList();
                ViewBag.Districts = new SelectList(districts);
                ViewBag.Blocks = new SelectList(blocks);
            }
            else
            {
                var districts = db.Employee_info.Where(x => x.DistrictMaster.Id == session.DistId && x.IsDeleted == false).Select(p => p.DistrictMaster.DistrictNameEn).Distinct().ToList();
                var blocks = db.Employee_info.Select(p => p.BlockMaster.BlockNameEn).Distinct().ToList();
                ViewBag.Districts = new SelectList(districts);
                ViewBag.Blocks = new SelectList(blocks);
            }
            return View();
        }

        [HttpPost]
        public ActionResult GetDistrictBlockEmployeeInfo(string searchDistrict, string searchBlock)
        {
            SessionDTO session = (SessionDTO)Session["UserInfo"];
            if (session.Role == "1" || session.Role == "2")
            {
                // Fetch products based on selected district and block
                var ei = db.Employee_info.Where(x => x.IsDeleted == false).AsQueryable();

                if (!string.IsNullOrEmpty(searchDistrict))
                {
                    ei = ei.Where(p => p.DistrictMaster.DistrictNameEn == searchDistrict);
                }

                if (!string.IsNullOrEmpty(searchBlock))
                {
                    ei = ei.Where(p => p.BlockMaster.BlockNameEn == searchBlock);
                }

                return PartialView("~/Views/Reports/Partial/_GetDistrictBlockEmployee_Info.cshtml", ei.ToList());
            }
            else
            {
                // Fetch products based on selected district and block
                var ei = db.Employee_info.Where(x => x.IsDeleted == false && x.DistrictMaster.Id == session.DistId).OrderBy(x => x.DistrictMaster.DistrictNameEn).Skip(1).AsQueryable();

                if (!string.IsNullOrEmpty(searchDistrict))
                {
                    ei = ei.Where(p => p.DistrictMaster.DistrictNameEn == searchDistrict).OrderBy(x => x.DistrictMaster.DistrictNameEn).Skip(1);
                }

                if (!string.IsNullOrEmpty(searchBlock))
                {
                    ei = ei.Where(p => p.BlockMaster.BlockNameEn == searchBlock).OrderBy(x => x.BlockMaster.BlockNameEn).Skip(1);
                }

                return PartialView("~/Views/Reports/Partial/_GetDistrictBlockEmployee_Info.cshtml", ei.ToList());
            }
        }
        #region District Block PDF and Excel 
        public ActionResult ExportToExcel(string searchDistrict, string searchBlock)
        {
            var employeeData = GetFilteredEmployeeData(searchDistrict, searchBlock);

            using (ExcelPackage package = new ExcelPackage())
            {

                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("State Health Society,Bihar || SBA Training Employee Information Data");

                //ExcelWorksheet worksheet2 = package.Workbook.Worksheets.Add("SBA Training Employee Information Data");
                //ExcelWorksheet worksheet3 = package.Workbook.Worksheets.Add("  ");


                // Add header
                worksheet.Cells[1, 1].Value = "S. No.";
                worksheet.Cells[1, 2].Value = "Employee Name";
                worksheet.Cells[1, 3].Value = "Mobile Number";
                worksheet.Cells[1, 4].Value = "District";
                worksheet.Cells[1, 5].Value = "Block";
                worksheet.Cells[1, 6].Value = "Facility";
                worksheet.Cells[1, 7].Value = "Training Completion Year";
                worksheet.Cells[1, 8].Value = "Created Date";

                // Add data
                int row = 2;
                int serialNo = 1;  // Initialize serial number
                foreach (var item in employeeData)
                {
                    worksheet.Cells[row, 1].Value = serialNo++;  // Serial number
                    worksheet.Cells[row, 2].Value = item.Name;
                    worksheet.Cells[row, 3].Value = item.MobileNumber;
                    worksheet.Cells[row, 4].Value = item.DistrictMaster.DistrictNameEn;
                    worksheet.Cells[row, 5].Value = item.BlockMaster.BlockNameEn;
                    worksheet.Cells[row, 6].Value = item.FacilityMaster.FacilityName;
                    worksheet.Cells[row, 7].Value = item.TrainingCompletionYear?.ToString("MM/yyyy") ?? "N/A";
                    worksheet.Cells[row, 8].Value = item.AddDate.ToString("dd/MM/yyyy");
                    row++;
                }

                var stream = new MemoryStream();
                package.SaveAs(stream);
                stream.Position = 0;
                return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "EmployeeData.xlsx");
            }
        }
        public ActionResult ExportToPDF(string searchDistrict, string searchBlock)
        {
            var employeeData = GetFilteredEmployeeData(searchDistrict, searchBlock);

            MemoryStream workStream = new MemoryStream();
            Document document = new Document(PageSize.A4, 25, 25, 30, 30);
            PdfWriter.GetInstance(document, workStream).CloseStream = false;
            document.Open();

            // Add title
            // Define a "class-like" paragraph style

            Font paragraphFont = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 14, BaseColor.BLUE);
            Paragraph p1 = new Paragraph("State Health Society,Bihar", paragraphFont);
            Paragraph p2 = new Paragraph("SBA Training Employee Information Data", paragraphFont);
            Paragraph p3 = new Paragraph("  ");
            p1.Alignment = Element.ALIGN_CENTER;
            p1.SpacingAfter = 10f;
            p2.Alignment = Element.ALIGN_CENTER;
            p2.SpacingAfter = 10f;
            p3.SpacingAfter = 20f;
            document.Add(p1);
            document.Add(p2);

            // Create table with 8 columns
            PdfPTable table = new PdfPTable(9);
            table.WidthPercentage = 100;

            // Create a base cell style (simulates a "class" for table cells)
            PdfPCell baseCell = new PdfPCell();
            baseCell.HorizontalAlignment = Element.ALIGN_CENTER;
            baseCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            baseCell.BorderColor = BaseColor.GRAY;
            baseCell.BackgroundColor = BaseColor.LIGHT_GRAY;

            // Create a font style (similar to a "class" for text)
            Font boldFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12, BaseColor.BLACK);

            // Add a cell using this font style
            PdfPCell sn = new PdfPCell(new Phrase("S. No.", boldFont));
            sn.HorizontalAlignment = baseCell.HorizontalAlignment;
            sn.VerticalAlignment = baseCell.VerticalAlignment;
            sn.BorderColor = baseCell.BorderColor;
            sn.BackgroundColor = baseCell.BackgroundColor;
            table.AddCell(sn);
            PdfPCell empName = new PdfPCell(new Phrase("Employee Name", boldFont));
            empName.HorizontalAlignment = baseCell.HorizontalAlignment;
            empName.VerticalAlignment = baseCell.VerticalAlignment;
            empName.BorderColor = baseCell.BorderColor;
            empName.BackgroundColor = baseCell.BackgroundColor;
            table.AddCell(empName);
            PdfPCell mob = new PdfPCell(new Phrase("Mobile Number", boldFont));
            mob.HorizontalAlignment = baseCell.HorizontalAlignment;
            mob.VerticalAlignment = baseCell.VerticalAlignment;
            mob.BorderColor = baseCell.BorderColor;
            mob.BackgroundColor = baseCell.BackgroundColor;
            table.AddCell(mob);
            PdfPCell dist = new PdfPCell(new Phrase("District", boldFont));
            dist.HorizontalAlignment = baseCell.HorizontalAlignment;
            dist.VerticalAlignment = baseCell.VerticalAlignment;
            dist.BorderColor = baseCell.BorderColor;
            dist.BackgroundColor = baseCell.BackgroundColor;
            table.AddCell(dist);
            PdfPCell Block = new PdfPCell(new Phrase("Block", boldFont));
            Block.HorizontalAlignment = baseCell.HorizontalAlignment;
            Block.VerticalAlignment = baseCell.VerticalAlignment;
            Block.BorderColor = baseCell.BorderColor;
            Block.BackgroundColor = baseCell.BackgroundColor;
            table.AddCell(Block);
            PdfPCell Facility = new PdfPCell(new Phrase("Facility", boldFont));
            Facility.HorizontalAlignment = baseCell.HorizontalAlignment;
            Facility.VerticalAlignment = baseCell.VerticalAlignment;
            Facility.BorderColor = baseCell.BorderColor;
            Facility.BackgroundColor = baseCell.BackgroundColor;
            table.AddCell(Facility);
            PdfPCell status = new PdfPCell(new Phrase("Training Status", boldFont));
            status.HorizontalAlignment = baseCell.HorizontalAlignment;
            status.VerticalAlignment = baseCell.VerticalAlignment;
            status.BorderColor = baseCell.BorderColor;
            status.BackgroundColor = baseCell.BackgroundColor;
            table.AddCell(status);
            PdfPCell year = new PdfPCell(new Phrase("Training Completion Year", boldFont));
            year.HorizontalAlignment = baseCell.HorizontalAlignment;
            year.VerticalAlignment = baseCell.VerticalAlignment;
            year.BorderColor = baseCell.BorderColor;
            year.BackgroundColor = baseCell.BackgroundColor;
            table.AddCell(year);
            PdfPCell date = new PdfPCell(new Phrase("Created Date", boldFont));
            date.HorizontalAlignment = baseCell.HorizontalAlignment;
            date.VerticalAlignment = baseCell.VerticalAlignment;
            date.BorderColor = baseCell.BorderColor;
            date.BackgroundColor = baseCell.BackgroundColor;
            table.AddCell(date);
            // Add table rows (employee data with serial number)
            int serialNo = 1;  // Initialize serial number
            // Add table rows (employee data)
            foreach (var item in employeeData)
            {
                table.AddCell(serialNo++.ToString());  // Serial number
                table.AddCell(item.Name);
                table.AddCell(item.MobileNumber);
                table.AddCell(item.DistrictMaster.DistrictNameEn);
                table.AddCell(item.BlockMaster.BlockNameEn);
                table.AddCell(item.FacilityMaster.FacilityName);
                // Assuming item.Istrained is a boolean value (true/false)
                string isTrainedDisplay = (item.Istrained ?? false) ? "Yes" : "No";
                //string isTrainedDisplay = item.Istrained.HasValue? (item.Istrained.Value ? "Yes" : "No"): "N/A"; // Handle null case with "N/A" or any other value
                // Add it to the iTextSharp table
                table.AddCell(isTrainedDisplay);

                table.AddCell(item.TrainingCompletionYear?.ToString("MM/yyyy") ?? "N/A");
                table.AddCell(item.AddDate.ToString("dd/MM/yyyy"));
            }

            document.Add(table);
            document.Close();

            byte[] byteInfo = workStream.ToArray();
            workStream.Write(byteInfo, 0, byteInfo.Length);
            workStream.Position = 0;
            return File(workStream, "application/pdf", "EmployeeData.pdf");
        }
        private List<Employee_info> GetFilteredEmployeeData(string searchDistrict, string searchBlock)
        {
            SessionDTO session = (SessionDTO)Session["UserInfo"];
            if (session.Role == "1" || session.Role == "2")
            {
                var ei = db.Employee_info.Where(x => x.IsDeleted == false).AsQueryable();

                if (!string.IsNullOrEmpty(searchDistrict))
                {
                    ei = ei.Where(p => p.DistrictMaster.DistrictNameEn == searchDistrict);
                }

                if (!string.IsNullOrEmpty(searchBlock))
                {
                    ei = ei.Where(p => p.BlockMaster.BlockNameEn == searchBlock);
                }

                ViewBag.SelectedDistrict = searchDistrict;
                ViewBag.SelectedBlock = searchBlock;

                return ei.ToList();
            }
            else
            {
                var ei = db.Employee_info.Where(x => x.IsDeleted == false && x.DistrictMaster.Id == session.DistId).AsQueryable();

                if (!string.IsNullOrEmpty(searchDistrict))
                {
                    ei = ei.Where(p => p.DistrictMaster.DistrictNameEn == searchDistrict);
                }

                if (!string.IsNullOrEmpty(searchBlock))
                {
                    ei = ei.Where(p => p.BlockMaster.BlockNameEn == searchBlock);
                }

                ViewBag.SelectedDistrict = searchDistrict;
                ViewBag.SelectedBlock = searchBlock;

                return ei.ToList();
            }

        }
        #endregion

        [HttpGet]
        public ActionResult YearAndBatchBySBATraining()
        {
            SessionDTO session = (SessionDTO)Session["UserInfo"];
            if (session.Role == "1" || session.Role == "2")
            {
                // Fetch distinct districts and blocks for the dropdown lists
                var monthyearmasters = db.Employee_Training.Where(x => x.IsDeleted == false).Select(p => p.MonthYearMaster.MonthYearName).Distinct().ToList();
                var batchmasters = db.Employee_Training.Where(x => x.IsDeleted == false).Select(p => p.tblBatchMaster.BatchName).Distinct().ToList();

                ViewBag.monthyearmasters = new SelectList(monthyearmasters);
                ViewBag.batchmasters = new SelectList(batchmasters);
            }

            else
            {
                var monthyearmasters = db.Employee_Training.Where(x => x.Employee_info.UserId == session.UserID && x.IsDeleted == false).Select(p => p.MonthYearMaster.MonthYearName).Distinct().ToList();
                var batchmasters = db.Employee_Training.Select(p => p.tblBatchMaster.BatchName).Distinct().ToList();

                ViewBag.monthyearmasters = new SelectList(monthyearmasters);
                ViewBag.batchmasters = new SelectList(batchmasters);
            }
            //var monthyearmasters = db.Employee_Training.Select(p => p.MonthYearMaster.MonthYearName).Distinct().ToList();
            //var batchmasters = db.Employee_Training.Select(p => p.tblBatchMaster.BatchName).Distinct().ToList();

            //ViewBag.monthyearmasters = new SelectList(monthyearmasters);
            //ViewBag.batchmasters = new SelectList(batchmasters);

            // Return the main view initially without products
            return View();
        }

        [HttpPost]
        public ActionResult YearAndBatchBySBATraining(string searchMonthyearmasters, string searchBatchmasters)
        {
            SessionDTO session = (SessionDTO)Session["UserInfo"];
            if (session.Role == "1" || session.Role == "2")
            {
                // Fetch products based on selected district and block
                var et = db.Employee_Training.Where(x => x.IsDeleted == false).AsQueryable();

                if (!string.IsNullOrEmpty(searchMonthyearmasters))
                {
                    et = et.Where(p => p.MonthYearMaster.MonthYearName == searchMonthyearmasters);
                }

                if (!string.IsNullOrEmpty(searchBatchmasters))
                {
                    et = et.Where(p => p.tblBatchMaster.BatchName == searchBatchmasters);
                }

                return PartialView("~/Views/Reports/Partial/_GetYearAndBatchBySBATraining.cshtml", et.ToList());
            }
            else
            {
                // Fetch products based on selected district and block
                var et = db.Employee_Training.Where(x => x.IsDeleted == false && x.Employee_info.UserId == session.UserID).AsQueryable();

                if (!string.IsNullOrEmpty(searchMonthyearmasters))
                {
                    et = et.Where(p => p.MonthYearMaster.MonthYearName == searchMonthyearmasters);
                }

                if (!string.IsNullOrEmpty(searchBatchmasters))
                {
                    et = et.Where(p => p.tblBatchMaster.BatchName == searchBatchmasters);
                }

                return PartialView("~/Views/Reports/Partial/_GetYearAndBatchBySBATraining.cshtml", et.ToList());
            }
        }










    }
}
