using AutoMapper;
using CRMLogic.Mall.Thiso.News;
using CRMManager.Models;
using CRMSharedLib.DataType.Logic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MSA.FW.BaseApp.DataObjects;
using MSAServices.Authentication;
using MSAServices.ContentService;
using MSAServices.RetailService;
using SmartBreadcrumbs.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRMManager.Controllers
{
    public class PromotionNewsController : Controller
    {
        CMSWebNewsManagement _cmsnewsHandler;
        IMapper _mapper;
        MSAAuthenticationService _authenticationService;
        int pageSize = 10;
        RetailOrgChartService _retailOrgChartService;
        public PromotionNewsController(CMSWebNewsManagement cmsnewsHandler,
            RetailOrgChartService retailOrgChartService,
            MSAAuthenticationService authenticationService
            , IMapper mapper)
        {
            _authenticationService = authenticationService;
            _cmsnewsHandler = cmsnewsHandler;
            _mapper = mapper;
            _retailOrgChartService = retailOrgChartService;
        }
        [Breadcrumb("Quản lý nội dung khuyến mãi")]
        public async Task<IActionResult> Index()
        {
            PromotionNewsListViewModel viewModel = new PromotionNewsListViewModel();

            var branchIDSession = HttpContext.Session.GetString("branchID");
            int branchID = 1;
            if (Convert.ToInt32(branchIDSession) != -1)
            {
                branchID = Convert.ToInt32(branchIDSession);
            }
            CMSNewsData initData = await _cmsnewsHandler.GetInitPromotionNews(branchID);
            viewModel.PromotionNewsList = initData.NewsList.OrderByDescending(o => o.CreatedOn).ToList();
            viewModel.NewsReviewStatusList = new SelectList(Enum.GetValues(typeof(ENewsReviewSatus)));
            viewModel.NewsCatList = new SelectList(Enum.GetValues(typeof(EPromotionNewsCat)));
            viewModel.PageTitle = "Quản lý nội dung khuyến mãi";
            viewModel.PageInfo = initData.PageInfo;

            ViewBag.OutletList = new SelectList(initData.Outlets, "ID", "OutletName");
            return View(viewModel);
        }

        public async Task<IActionResult> PagingAndFilter(int pageIndex, int selectedOutlet,
            int selectedNewsStatus, int selectedNewsCat, string keywords)
        {
            int selectedReviewStastus = 0;
            //ENewsReviewSatus _selectReviewNewsStatus = EnumHelper<ENewsReviewSatus>.ToEnum(selectedReviewStastus);
            ENewsReviewSatus _selectReviewNewsStatus = ((ENewsReviewSatus)selectedReviewStastus);

            string reviewStatus = _selectReviewNewsStatus.ToString();
            string newsCat = ((EPromotionNewsCat)selectedNewsCat).ToString();
            int newsStatus = selectedNewsStatus;

            var branchIDSession = HttpContext.Session.GetString("branchID");
            int branchID = 1;
            if (Convert.ToInt32(branchIDSession) != -1)
            {
                branchID = Convert.ToInt32(branchIDSession);
            }


            PageInfo pageInfo = new PageInfo(pageSize);
            pageInfo.IsInit = true;
            pageInfo.PageIndex = pageIndex;


            PromotionNewsListViewModel promotionNewsListViewModel = await _cmsnewsHandler.
                GetPagingPromotionNews(reviewStatus, newsCat,
                pageInfo, selectedOutlet, "", keywords);

            List<BaseOutlet> outlets = new List<BaseOutlet>();
            outlets = await _retailOrgChartService.GetOutlets(branchID);//hard code branchID 
            ViewBag.OutletList = new SelectList(outlets, "ID", "OutletName");
            promotionNewsListViewModel.NewsReviewStatusList = new SelectList(Enum.GetValues(typeof(ENewsReviewSatus)));
            promotionNewsListViewModel.PageInfo = pageInfo;
            promotionNewsListViewModel.SelectedNewsCat = ((int)(EPromotionNewsCat)selectedNewsCat);
            promotionNewsListViewModel.SelectedOutletID = selectedOutlet;
            //promotionNewsListViewModel.SelectedReviewNewsStatus = ((int)(EPromotionNewsCat)_selectReviewNewsStatus);
            //promotionNewsListViewModel.SelectedReviewNewsStatus = _selectReviewNewsStatus.ToString();
            promotionNewsListViewModel.SelectedNewsStatus = newsStatus;
            promotionNewsListViewModel.SearchKeyword = keywords;
            if (promotionNewsListViewModel != null && promotionNewsListViewModel.PromotionNewsList.Count > 0)
            {
                return View("Index", promotionNewsListViewModel);
            }
            else
            {
                return View("Index", promotionNewsListViewModel);
            }
        }

        [Breadcrumb("Chi tiết")]
        public async Task<IActionResult> Details(string number)
        {

            //PromotionNewsViewModel viewModel = new PromotionNewsViewModel();
            PromotionNewsViewModel viewModel = await _cmsnewsHandler.GetNewsDataByNumber(number);
            if (viewModel != null)
            {
                viewModel.DataState = EDataState.Idle;

                List<BaseOutlet> outlets = await _retailOrgChartService.GetOutlets(1);//hard code branchID
                ViewBag.OutletList = new SelectList(outlets, "ID", "OutletName");
                viewModel.PageTitle = "Chi tiết tin khuyến mãi";
                return View("Details", viewModel);
            }

            else
            {
                //van dung yen 1 cho
                //pop up Bao la he thong co lỗi??
                return View("Index");
            }
        }

        [Breadcrumb("Thêm mới")]
        [HttpGet]
        public async Task<IActionResult> Create(int SelectedNewsCat)
        {

            PromotionNewsViewModel data = new PromotionNewsViewModel();
            //data = model;
            data.DataState = EDataState.Create;

            data.FromDate = DateTime.Now;
            data.ToDate = DateTime.Now.AddDays(7);
            data.SelectedNewsCat = SelectedNewsCat;
            string number = DateTime.Now.Ticks.ToString();
            string user = "admin";
            DateTime dataDate = DateTime.Now;
            data.ID = -1;// ID DB tu kiem soat
            data.CreatedOn = dataDate;
            data.ModifiedOn = dataDate;
            data.ModifiedBy = user;
            data.CreatedBy = user;
            data.Number = number;
            data.Approved = 0;
            string newsCateTitle = "Khuyến mãi";
            if (SelectedNewsCat == 1)
            {
                newsCateTitle = "Sự kiện";
            }
            data.PageTitle = "Soạn tin tức " + newsCateTitle;
            CMSUser demoUser = new CMSUser()
            {
                UserID = "admin",
                BranchID = 1,
                LoginDate = System.DateTime.Now,
                SessionID = System.Guid.NewGuid()
            };
            int branchID = demoUser.BranchID;
            List<BaseOutlet> outlets = await _retailOrgChartService.GetOutlets(branchID);
            ViewBag.OutletList = new SelectList(outlets, "ID", "OutletName");
            //HttpContext.Session.SetString
            return View("CreateNews", data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveCreate(PromotionNewsViewModel viewModel)
        {
            int branchID = 1;
            List<BaseOutlet> outlets = await _retailOrgChartService.GetOutlets(branchID);
            ViewBag.OutletList = new SelectList(outlets, "ID", "OutletName");

            if (viewModel.IsValidNewsContent())
            {
                ModelState.AddModelError("DisplayError", "Please enter required fields in news content");
                return View("CreateNews", viewModel);
            }
            else
            {

                viewModel.Name = viewModel.VNNews.Subject;
                viewModel.VNNews.Name = viewModel.Name;
                viewModel.VNNews.CreatedBy = "admin";
                viewModel.VNNews.CreatedOn = DateTime.Now;
                viewModel.VNNews.ModifiedBy = "admin";
                viewModel.VNNews.ModifiedOn = DateTime.Now;
                viewModel.VNNews.LangCode = ELangCode.VN.ToString();

                viewModel.ENNews.Name = viewModel.Name;
                viewModel.ENNews.CreatedBy = "admin";
                viewModel.ENNews.CreatedOn = DateTime.Now;
                viewModel.ENNews.ModifiedBy = "admin";
                viewModel.ENNews.ModifiedOn = DateTime.Now;
                viewModel.ENNews.LangCode = ELangCode.EN.ToString();


                bool success = await _cmsnewsHandler.Save(viewModel);
                if (success)
                {
                    viewModel.DataState = EDataState.Idle;
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("DisplayError", "Insert failed");
                    return View("CreateNews", viewModel);
                }

            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Preview(PromotionNewsViewModel model)
        {
            PromotionNewsViewModel data = new PromotionNewsViewModel();
            data = model;
            //TempData["ImageLayout1"] = model.ImageLayout400_400;

            return View("Preview", data);
        }

        [HttpGet]
        [Breadcrumb("Chỉnh sửa")]
        public async Task<IActionResult> Edit(string number)
        {
            PromotionNewsViewModel viewModel = await _cmsnewsHandler.GetNewsDataByNumber(number);
            if (viewModel != null)
            {

                List<BaseOutlet> outlets = await _retailOrgChartService.GetOutlets(1);//hard code branchID
                ViewBag.OutletList = new SelectList(outlets, "ID", "OutletName");

                viewModel.PageTitle = "Chi tiết tin tức";
                viewModel.DataState = EDataState.Edit;
                string errorMessage = TempData["ErrorMessage"] as string;
                string resultMessage = TempData["OKMessage"] as string;
                if (!string.IsNullOrEmpty(errorMessage))
                {
                    ModelState.AddModelError("DisplayError", errorMessage);
                }
                if (!string.IsNullOrEmpty(resultMessage))
                {
                    ViewBag.ResultMessage = resultMessage;
                }
                return View(viewModel);
            }
            else
            {
                //bao loi=> tra ve trang truoc do
                return View();

            }
        }


        [HttpPost]
        public async Task<IActionResult> Edit(PromotionNewsViewModel viewModel)
        {
            string resultMessage = "";
            if (viewModel.IsValidNewsContent())
            {
                resultMessage = "Please enter required fields in news content";
                TempData["ErrorMessage"] = resultMessage;
                return RedirectToAction("Edit", "PromotionNews", new { number = viewModel.Number });
            }

            viewModel.Name = viewModel.VNNews.Subject;

            viewModel.VNNews.ModifiedBy = "admin";
            viewModel.VNNews.ModifiedOn = DateTime.Now;
            viewModel.VNNews.LangCode = ELangCode.VN.ToString();

            viewModel.ENNews.ModifiedBy = "admin";
            viewModel.ENNews.ModifiedOn = DateTime.Now;
            viewModel.ENNews.LangCode = ELangCode.EN.ToString();

            bool success = await _cmsnewsHandler.Save(viewModel);
            if (success)
            {
                resultMessage = "Update news success";
                TempData["OKMessage"] = resultMessage;
                return RedirectToAction("Edit", "PromotionNews", new { number = viewModel.Number });

            }
            else
            {
                resultMessage = "Update news failed";
                TempData["OKMessage"] = resultMessage;
                return RedirectToAction("Edit", "PromotionNews", new { number = viewModel.Number });
            }

        }

        public IActionResult Cancel()
        {
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Delete(string number)
        {
            PromotionNewsViewModel viewModel = await _cmsnewsHandler.GetNewsDataByNumber(number);
            if (viewModel != null)
            {
                //viewModel.DataState = EDataState.Edit;
                //viewModel.IsDeleted = true;
                //bool success = await _cmsnewsHandler.Save(viewModel);

            }
            else
            {
                //bao loi=> tra ve trang truoc do
                return View();

            }
            return RedirectToAction("Index");
        }

    }
}
