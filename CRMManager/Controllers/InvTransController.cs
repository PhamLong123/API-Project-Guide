//using CRMManager.Models;
//using Microsoft.AspNetCore.Mvc;
//using System;
//using AutoMapper;
//using CRMLogic.Base.BusinessObject;
//using CRMLogic.Base.CRMManager;
//using Microsoft.AspNetCore.Hosting;
//using Microsoft.AspNetCore.Http;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Threading.Tasks;
//using System.Text;
//using ClosedXML.Excel;

//namespace CRMManager.Controllers
//{
//    public class InvTransController : Controller
//    {
//        BaseInvTransOperation _baseInvTransOperation;
//        BaseInvTransDetailOperation _baseInvTransDetailOperation;
//        IMapper _mapper;
//        public InvTransController(BaseInvTransOperation baseInvTransOperation, BaseInvTransDetailOperation baseInvTransDetailOperation, IMapper mapper)
//        {
//            _baseInvTransOperation = baseInvTransOperation;
//            _baseInvTransDetailOperation = baseInvTransDetailOperation;
//            _mapper = mapper;
//        }

//        #region InvTrans
//        public async Task<IActionResult> Index()
//        {
//            ViewBag.PageTitle = "InvTrans";
//            List<BaseInvTrans> data = await _baseInvTransOperation.GetInvTrans();
//            List<InvTransViewModel> models = _mapper.Map<List<InvTransViewModel>>(data);
//            return View(models);
//        }

//        public async Task<IActionResult> Detail(Guid ID)
//        {
//            ViewBag.PageTitle = "InvTransDetail";
//            BaseInvTrans data = await _baseInvTransOperation.GetInvTransByID(ID);
//            InvTransViewModel model = _mapper.Map<InvTransViewModel>(data);
//            List<BaseInvTransDetail> datadetail = await _baseInvTransDetailOperation.GetInvTransDetail();
//            List<InvTransDetailViewModel> modeldetails = _mapper.Map<List<InvTransDetailViewModel>>(datadetail);
//            ViewBag.Detail = modeldetails;
//            return View(model);
//        }

//        public async Task<IActionResult> AddNew()
//        {
//            InvTransViewModel model = new InvTransViewModel();
//            //model.InvTransDetails.Add(new InvTransDetailViewModel() { ID = Guid.NewGuid() });
//            ViewBag.PageTitle = "InsertInvTransDetail";
//            return View(model);
//        }

//        [HttpPost]
//        public async Task<IActionResult> AddNew(InvTransViewModel model)
//        {
//            foreach(var item in model.InvTransDetails)
//            {
//                item.ID = Guid.NewGuid();
//                item.TransID = model.ID;
//                item.CreatedOn = DateTime.Now;
//                item.ModifiedOn = DateTime.Now;
//            }
//            BaseInvTrans baseInvTrans = _mapper.Map<BaseInvTrans>(model);
//            var result = await _baseInvTransOperation.InsertData(baseInvTrans);
//            return View(model);
//        }

//        [HttpPost]
//        public IActionResult ExportToCSV(List<InvTransDetailViewModel> models)
//        {
//            var builder = new StringBuilder();
//            builder.AppendLine("ID, BarCode, Quantity, TransID, Unit");
//            foreach(var item in models)
//            {
//                builder.AppendLine($"{item.ID}, {item.BarCode}, {item.Quantity}, {item.TransID}, {item.Unit}");
//            }
//            return File(Encoding.UTF8.GetBytes(builder.ToString()), "text/csv", "InvTransDetail.csv");
//        }

//        public async Task<IActionResult> ExportToExcel()
//        {
//            List<BaseInvTrans> data = await _baseInvTransOperation.GetInvTrans();
//            List<InvTransViewModel> models = _mapper.Map<List<InvTransViewModel>>(data);
//            List<BaseInvTransDetail> datadetail = await _baseInvTransDetailOperation.GetInvTransDetail();
//            List<InvTransDetailViewModel> modeldetails = _mapper.Map<List<InvTransDetailViewModel>>(datadetail);
//            using (var workbook = new XLWorkbook())
//            {
//                foreach(var item in models)
//                {
//                    var worksheet = workbook.Worksheets.Add($"{item.TCode}");
//                    var titleRow = 1;
//                    worksheet.Cell(titleRow, 5).Value = "InvTransDetail";
//                    worksheet.Cell(2, 1).Value = "DocNo";
//                    worksheet.Cell(2, 2).Value = item.DocNo;
//                    worksheet.Cell(3, 1).Value = "TCode";
//                    worksheet.Cell(3, 2).Value = item.TCode;
//                    worksheet.Cell(4, 1).Value = "Name";
//                    worksheet.Cell(4, 2).Value = item.Name;
//                    worksheet.Cell(5, 1).Value = "Number";
//                    worksheet.Cell(5, 2).Value = item.Number;
//                    worksheet.Cell(6, 1).Value = "User";
//                    worksheet.Cell(6, 2).Value = item.UserID;
//                    worksheet.Cell(7, 1).Value = "TransDate";
//                    worksheet.Cell(7, 2).Value = item.TransDate;
//                    worksheet.Cell(8, 1).Value = "WHCode";
//                    worksheet.Cell(8, 2).Value = item.WHCode;
//                    worksheet.Cell(9, 1).Value = "ShelfCode";
//                    worksheet.Cell(9, 2).Value = item.ShelfCode;
//                    var detailrowtable = 10;
//                    worksheet.Cell(detailrowtable, 1).Value = "ID";
//                    worksheet.Cell(detailrowtable, 2).Value = "BarCode";
//                    worksheet.Cell(detailrowtable, 3).Value = "Quantity";
//                    worksheet.Cell(detailrowtable, 4).Value = "Unit";
//                    foreach (var itemdetails in modeldetails)
//                    {
//                        if (itemdetails.TransID == item.ID)
//                        {
//                            detailrowtable++;
//                            worksheet.Cell(detailrowtable, 1).Value = itemdetails.ID;
//                            worksheet.Cell(detailrowtable, 2).Value = itemdetails.BarCode;
//                            worksheet.Cell(detailrowtable, 3).Value = itemdetails.Quantity;
//                            worksheet.Cell(detailrowtable, 4).Value = itemdetails.Unit;
//                        }
//                    }
//                }
//                using (var stream = new MemoryStream())
//                {
//                    workbook.SaveAs(stream);
//                    var content = stream.ToArray();
//                    return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "InvTrans" + $"{DateTime.Now}" + ".xlsx");
//                }
//            }
//        }
//        #endregion
//    }
//}
