using IronPdf;
using Microsoft.AspNetCore.Hosting;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using qlsv.Models.Interfaces;
using qlsv.ViewModels.Common;
using qlsv.ViewModels.TestSchedule;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace qlsv.Models.Services
{
    public class ExportService : IExportService
    {
        private readonly IViewRenderService _viewRenderService;
        private readonly IHostingEnvironment _env;
        private readonly IWebHostEnvironment _hostEnvironment;
        public ExportService(
            IViewRenderService viewRenderService,
            IHostingEnvironment env,
            IWebHostEnvironment hostEnvironment
            )
        {
            _viewRenderService = viewRenderService;
            _env = env;
            _hostEnvironment = hostEnvironment;
        }    
        
        public async Task<ApiResult<bool>> ExportDataToPdf(string fileName, object data, string pathTemplate)
        {
            

            HtmlToPdf converter = new HtmlToPdf();

            //set converter
            converter.PrintOptions.PaperSize = PdfPrintOptions.PdfPaperSize.A4;
            //converter.PrintOptions.PaperOrientation = PdfPrintOptions.PdfPaperOrientation.Portrait;
            converter.PrintOptions.MarginLeft = 20;
            converter.PrintOptions.MarginTop = 10;
            converter.PrintOptions.MarginRight = 10;
            converter.PrintOptions.MarginBottom = 10;
            converter.PrintOptions.EnableJavaScript = true;
            converter.PrintOptions.CreatePdfFormsFromHtml = false;
            //converter.PrintOptions.CssMediaType = PdfPrintOptions.PdfCssMediaType.Screen;
            converter.PrintOptions.FitToPaperWidth = true;

            converter.PrintOptions.InputEncoding = Encoding.UTF8;
            string baseUri = _hostEnvironment.WebRootPath + @"/css/main.css";
            converter.PrintOptions.CustomCssUrl = baseUri;



            var htmlString = await _viewRenderService.RenderViewAsync(pathTemplate, data);

            IronPdf.PdfDocument doc = converter.RenderHtmlAsPdf(htmlString);

            var res = doc.SaveAs(fileName);

            if (res == null)
            {
                return new ApiErrorResult<bool>("Xuất thất bại");
            }

            return new ApiSuccessResult<bool>()
            {
                IsSuccessed =  true,
                Message = "Xuất thành công"
            };
        }

        public Task ExportDataToXlSX(string fileName, TestScheduleView<TestScheduleClassView> data)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            return Task.Run(() => {
                using (ExcelPackage pck = new ExcelPackage())
                {
                    //Create 
                    pck.Workbook.Properties.Title = "Lịch thi";
                    pck.Workbook.Properties.Subject = "Lịch thi cuối kì các lớp...";
                    pck.Workbook.Properties.Created = DateTime.Now;

                    ExcelWorksheet worksheet = pck.Workbook.Worksheets.Add("Sheet 1");
                    worksheet.Cells.Style.Font.Size = 12;
                    worksheet.Cells.Style.Font.Name = "Times New Roman"; //Default Font name for whole sheet
                    var QH1 = worksheet.Cells["A1:D1"];
                    QH1.Merge = true;
                    QH1.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    QH1.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    QH1.Value = "ĐẠI HỌC QUỐC GIA TP. HCM";
                   


                    var QH2 = worksheet.Cells["A2:D2"];
                    QH2.Merge = true;
                    QH2.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    QH2.Value = "TRƯỜNG ĐẠI HỌC KHOA HỌC TỰ NHIÊN";
                    QH2.Style.Font.Bold = true;
                    QH2.Style.VerticalAlignment = ExcelVerticalAlignment.Center;


                    var QH3 = worksheet.Cells["E1:K1"];
                    QH3.Merge = true;
                    QH3.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    QH3.Value = "CỘNG HÒA XÃ HỘI CHỦ NGHĨA VIỆT NAM";
                    QH3.Style.Font.Bold = true;
                    QH3.Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                    var QH4 = worksheet.Cells["E2:K2"];
                    QH4.Merge = true;
                    QH4.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    QH4.Value = "Độc lập - Tự do - Hạnh phúc";
                    QH4.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    QH4.Style.Font.Bold = true;
                    QH4.Style.Font.UnderLine = true;
                    

                    var title1 = worksheet.Cells["A3:I3"];
                    title1.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    title1.Merge = true;
                    title1.Value = data.Title;
                    title1.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    title1.Style.Font.Size = 16;
                    title1.Style.Font.Bold = true;
                    
                    

                    var title2 = worksheet.Cells["A4:I4"];
                    title2.Merge = true;
                    title2.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    title2.Value = data.Classes;
                    title2.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    title2.Style.Font.Bold = true;
                    title2.Style.Font.Size = 16;


                    //Header
                    //stt
                    var sheet = worksheet.Cells[$"A7:H{7 + data.ScheduleTime.Count}"];
                    var table = worksheet.Tables.Add(sheet, "Table");
                    table.Columns[0].Name = "STT";
                    table.Columns[1].Name = "Mã MH";
                    table.Columns[2].Name = "Môn học";
                    table.Columns[3].Name = "Ngày Thi";
                    table.Columns[4].Name = "Giờ thi";
                    table.Columns[5].Name = "Phòng thi";
                    table.Columns[6].Name = "Cán bộ coi thi";
                    table.Columns[7].Name = "Ghi chú";
                    //table.ShowFilter = true;
                    

                    //row exist
                    int existRow = 8;
                    foreach (var item in data.ScheduleTime)
                    {
                        string numCol = (existRow).ToString();
                        worksheet.Cells["A" + numCol].RichText.Add((existRow - 7).ToString());
                        worksheet.Cells["B" + numCol].RichText.Add(item.ClassId);
                        worksheet.Cells["C" + numCol].RichText.Add(item.ClassName);
                        worksheet.Cells["D" + numCol].RichText.Add(item.TestTime.ToShortDateString());
                        worksheet.Cells["E" + numCol].RichText.Add(item.TestHour.Substring(0, 5));
                        worksheet.Cells["F" + numCol].RichText.Add(item.RoomId.ToString());
                        worksheet.Cells["G" + numCol].RichText.Add(item.SupervisorName);
                        
                        existRow++;
                    }
                   
                    //row exist zoom it 
                    existRow += 2;
                    var note = worksheet.Cells[$"A{existRow}:D{existRow + 4}"];
                    note.Merge = true;
                    note.Style.WrapText = true;
                    note.Value = "Lưu ý: - Sinh viên phải mang thẻ SV và CMND\n" +
                        "         - Sinh viên chưa hoàn thành học phí sẽ không được dự thi.\n" +
                        "         - Sinh viên phải đeo khẩu trang khi đến trường và trong suốt thời gian thi.";
                    note.Style.Font.Bold = true;

                    existRow += 5;

                    var location = worksheet.Cells[$"F{existRow}:I{existRow}"];
                    location.Merge = true;
                    location.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    location.Value = $"Hồ Chí Minh, Ngày {DateTime.Now.Day}, Tháng {DateTime.Now.Month}, Năm {DateTime.Now.Year}";
                    location.Style.Font.Italic = true;

                    existRow += 1;

                    var position = worksheet.Cells[$"F{existRow}:I{existRow}"];
                    position.Merge = true;
                    //position.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    position.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    position.Value = $"TL. HIỆU TRƯỞNG";
                    position.Style.Font.Bold = true;

                    existRow += 3;

                    var positionName = worksheet.Cells[$"F{existRow}:I{existRow}"];
                    positionName.Merge = true;
                    positionName.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    positionName.Value = data.Signer;
                    positionName.Style.Font.Bold = true;


                    worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();
                    var file = new FileInfo(fileName);
                    pck.SaveAs(file);
                };
            });
        }
    }
}
