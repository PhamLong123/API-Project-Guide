//using AutoMapper;
//using CRMLogic.Base.BusinessObject;
//using CRMLogic.Base.Common;
//using CRMLogic.Base.CRMManager;
//using CRMManager.Models;
//using CRMSharedLib.DataType.Logic;
//using Microsoft.AspNetCore.Hosting;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using OfficeOpenXml;
//using System.Collections.Generic;
//using System.IO;
//using System.Text;
//using System.Threading.Tasks;

//namespace CRMManager.Controllers
//{
//    public class ProdItemBarCodeController : Controller
//    {
//        private IHostingEnvironment _hostingEnv;
//        BaseStockMasterBarCodeOperation _baseStockMasterOperation;
//        IMapper _mapper;
//        public ProdItemBarCodeController(IHostingEnvironment hostingEnv, IMapper mapper, BaseStockMasterBarCodeOperation baseStockMasterBarCodeOperation)
//        {
//            _hostingEnv = hostingEnv;
//            _mapper = mapper;
//            _baseStockMasterOperation = baseStockMasterBarCodeOperation;
//        }

//        public IActionResult Index()
//        {
//            return View();
//        }

//        //
//        /// <summary>
//        /// Import from ExcelMaster Data into SQL Master Data
//        /// </summary>
//        /// <returns></returns>
//        public async Task<IActionResult> ImportMasterItemBarCode(IFormFile file)
//        {
//            BODataProcessResult result = new BODataProcessResult();
//            List<ProdItemBarCodeViewModel> list = new List<ProdItemBarCodeViewModel>();
//            StringBuilder sb = new StringBuilder();
//            using (var stream = new MemoryStream())
//            {
//                await file.CopyToAsync(stream);
//                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
//                using (var package = new ExcelPackage(stream))
//                {
//                    ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
//                    var rowcount = worksheet.Dimension.Rows;

//                    #region string builder HTML
//                    sb.Append("<table class='table table-bordered'><tr>");
//                    List<string> columnNames = new List<string>();
//                    foreach (var firstRowCell in worksheet.Cells[worksheet.Dimension.Start.Row, worksheet.Dimension.Start.Column, 1, worksheet.Dimension.End.Column])
//                    {
//                        columnNames.Add(firstRowCell.Text);
//                        sb.Append("<th>" + firstRowCell.Text.ToString() + "</th>");
//                    }
//                    sb.Append("</tr>");
//                    sb.AppendLine("<tr>");
//                    var start = worksheet.Dimension.Start;
//                    var end = worksheet.Dimension.End;
//                    for (int i = start.Row + 1; i <= end.Row; i++)
//                    { // Row by row...
//                        for (int col = start.Column; col <= end.Column; col++)
//                        { // ... Cell by cell...
//                            object cellValue = worksheet.Cells[i, col].Text; // This got me the actual value I needed.
//                            sb.Append("<td>" + cellValue.ToString() + "</td>");
//                        }
//                        sb.AppendLine("</tr>");
//                    }
//                    sb.Append("</table>");


//                    #endregion

//                    for (int row = 2; row <= rowcount; row++)
//                    {
//                        list.Add(new ProdItemBarCodeViewModel
//                        {
//                            BarCode = worksheet.Cells[row, 1].Value.ToString().Trim(),
//                            Name = worksheet.Cells[row, 2].Value.ToString().Trim(),
//                            Unit = worksheet.Cells[row, 3].Value.ToString().Trim(),
//                            Description = worksheet.Cells[row, 4].Value.ToString().Trim()
//                        });
//                    }
//                }
//            }

//            #region Insert Bar Code
//            List<BaseProdItemBarCode> listBarCode = _mapper.Map<List<BaseProdItemBarCode>>(list);
//            result = await _baseStockMasterOperation.InsertBarCode(listBarCode);

//            if (!result.OK)
//            {
//                ViewBag.StringBuilder = "Error - Data import unsuccessfully ";
//            }
//            else
//            {
//                ViewBag.StringBuilder = this.Content(sb.ToString()).Content;
//            }
//            #endregion

//            return View("Index");
//        }

//        public FileResult Download()
//        {
//            var fileName = $"UploadBarCode.xlsx";
//            string path = Path.Combine(this._hostingEnv.WebRootPath, "FileTemplate/") + fileName;
//            byte[] bytes = System.IO.File.ReadAllBytes(path);
//            return File(bytes, "application/octet-stream", fileName);
//        }

//        /// <summary>
//        /// Export transaction to Excel
//        /// </summary>
//        /// <returns></returns>
//        public IActionResult ExportTransactionBarCodes()
//        {
//            return View();
//        }

//    }
//}
