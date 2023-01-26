using AutoMapper;
using CRMLogic.Mall.Thiso.News;
using CRMManager.Models;
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
    public class NewsApprovalController : Controller
    {
        CMSWebNewsManagement _cmsnewsHandler;
        IMapper _mapper;
        MSAAuthenticationService _authenticationService;
        int pageSize = 10;

        RetailOrgChartService _retailOrgChartService;
        public NewsApprovalController(CMSWebNewsManagement cmsnewsHandler,
            RetailOrgChartService retailOrgChartService,
            MSAAuthenticationService authenticationService
            , IMapper mapper)
        {
            _authenticationService = authenticationService;
            _cmsnewsHandler = cmsnewsHandler;
            _mapper = mapper;
            _retailOrgChartService = retailOrgChartService;
        }
        [Breadcrumb("Phê duyệt tin tức")]
        public async Task<IActionResult> Index()
        {
            PromotionNewsListViewModel viewModel = new PromotionNewsListViewModel();
            CMSUser demoUser = new CMSUser()
            {
                UserID = "admin",
                BranchID = 1,
                LoginDate = System.DateTime.Now,
                SessionID = System.Guid.NewGuid()
            };
            int branchID = demoUser.BranchID;
            CMSNewsData initData = await _cmsnewsHandler.GetInitPromotionNews(branchID);
            viewModel.PromotionNewsList = initData.NewsList.OrderByDescending(o => o.CreatedOn).ToList();
            viewModel.NewsCatList = new SelectList(Enum.GetValues(typeof(EPromotionNewsCat)));
            viewModel.PageTitle = "Phê duyệt tin tức";
            viewModel.PageInfo = initData.PageInfo;

            ViewBag.OutletList = new SelectList(initData.Outlets, "ID", "OutletName");
            return View(viewModel);
        }



        [Breadcrumb("Phê duyệt")]
        public async Task<IActionResult> Approve(string number)
        {
            try
            {
                PromotionNewsViewModel promotionNewsViewModel = await _cmsnewsHandler.GetNewsDataByNumber(number);
                if (promotionNewsViewModel != null)
                {
                    List<BaseOutlet> outlets = await _retailOrgChartService.GetOutlets(1);//hard code branchID
                    ViewBag.OutletList = new SelectList(outlets, "ID", "OutletName");
                    promotionNewsViewModel.PageTitle = "Phê duyệt tin tức";
                    return View(promotionNewsViewModel);
                }

                else
                {
                    //van dung yen 1 cho
                    //pop up Bao la he thong co lỗi??
                    return View("Index");
                }
            }
            catch (Exception ex)
            {
                string errorMessage = ex.Message;
                throw;
            }

        }


        [HttpPost]
        public async Task<IActionResult> Approve(PromotionNewsViewModel viewModel)
        {
            try
            {
                if (string.IsNullOrEmpty(viewModel.ApprovalNotes) && viewModel.Approved == 0)
                {
                    //Thong bao loi bang viewbag hoac tempdata khi redirect index, khi return view hien tai thi su dung modelstate
                    ModelState.AddModelError("DisplayError", "Approve failed");
                    return RedirectToAction("Approve", viewModel.Number);
                }
                else
                {
                    bool success = await _cmsnewsHandler.ApprovedNews(viewModel);
                    if (true)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError("DisplayError", "Approve failed");
                        return RedirectToAction("Approve", viewModel.Number);
                    }
                }

            }
            catch (Exception ex)
            {
                string errorMessage = ex.Message;
                throw;
            }

        }


        public async Task<IActionResult> PagingAndFilter(int pageIndex, int selectedOutlet,
            int selectedNewsCat)
        {
            //ENewsReviewSatus _selectReviewNewsStatus = EnumHelper<ENewsReviewSatus>.ToEnum(selectedReviewStastus);
            string newsCat = ((EPromotionNewsCat)selectedNewsCat).ToString();
            string reviewStatus = ENewsReviewSatus.Pending.ToString();

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
                pageInfo, selectedOutlet, "");

            List<BaseOutlet> outlets = new List<BaseOutlet>();
            outlets = await _retailOrgChartService.GetOutlets(branchID);//hard code branchID 
            ViewBag.OutletList = new SelectList(outlets, "ID", "OutletName");
            promotionNewsListViewModel.NewsReviewStatusList = new SelectList(Enum.GetValues(typeof(ENewsReviewSatus)));
            promotionNewsListViewModel.PageInfo = pageInfo;
            promotionNewsListViewModel.SelectedNewsCat = ((int)(EPromotionNewsCat)selectedNewsCat);
            promotionNewsListViewModel.SelectedOutletID = selectedOutlet;
            promotionNewsListViewModel.SelectedReviewNewsStatus = reviewStatus;
            if (promotionNewsListViewModel != null && promotionNewsListViewModel.PromotionNewsList.Count > 0)
            {
                return View("Index", promotionNewsListViewModel);
            }
            else
            {
                return View("Index", promotionNewsListViewModel);
            }
        }

        public IActionResult Cancel()
        {
            return RedirectToAction("Index");
        }
    }
}
