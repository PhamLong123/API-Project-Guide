using CRMManager.Helper.Logic.News;
using CRMManager.Models;
using CRMSharedLib.DataType.Logic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MSAServices.ContentService;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRMManager.Controllers
{
    public class ApplicationContentController : Controller
    {

        Dictionary<string, string> CRMAboutImagesConfig = new Dictionary<string, string>();
        Dictionary<string, string> BannerImagesConfig = new Dictionary<string, string>();
        Dictionary<string, string> UtilitiesImagesConfig = new Dictionary<string, string>();
        Dictionary<string, string> CRMWebClientImagesConfig = new Dictionary<string, string>();
        string typeNumber = "CLMApp";

        CMSWebAppContent _cMSWebAppContent;

        public ApplicationContentController(CMSWebAppContent cMSWebAppContent)
        {
            _cMSWebAppContent = cMSWebAppContent;
        }

        public async Task<IActionResult> Index(int selectedCatID = -1)
        {
            CRMAboutImagesConfig.Add("Image1", "1440_300");
            BannerImagesConfig.Add("Image1", "1440_700");
            UtilitiesImagesConfig.Add("ImageSlide1", "500_240");
            UtilitiesImagesConfig.Add("ImageSlide2", "500_240");
            CRMWebClientImagesConfig.Add("Image1", "1440_300");

            ApplicationContentListViewModel viewModel = new ApplicationContentListViewModel();
            if (selectedCatID == -1)
            {
                WebAppContentData webAppContentData = await _cMSWebAppContent.GetInitAppContent();
                viewModel.AppContentList = webAppContentData.Contents;
            }

            else
            {
                string catNumber = ((EAppContentCatNumber)selectedCatID).ToString().ToUpper();
                viewModel.AppContentList = await _cMSWebAppContent.GetAppContent(catNumber);
            }


            //viewModel.AppContentList = webAppContentData.Contents;
            viewModel.PageTitle = "Quản lý nội dung ứng dụng";
            viewModel.SelectCatList = new SelectList(Enum.GetValues(typeof(EAppContentCatNumber)));
            viewModel.SelectedCatID = 1;
            return View(viewModel);
        }
        public IActionResult Create(int SelectedNewsCat)
        {

            ApplicationContentViewModel viewModel = new ApplicationContentViewModel();
            viewModel.PageTitle = "Thêm nội dung";
            viewModel.SelectedCatID = SelectedNewsCat;
            viewModel.DataState = EDataState.Create;
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ApplicationContentViewModel viewModel)
        {
            try
            {
                if (viewModel.IsValidNewsContent())
                {
                    ModelState.AddModelError("DisplayError", "Please enter required fields in news content");
                    return View("Create", viewModel);
                }
                else
                {
                    viewModel.Number = DateTime.Now.Ticks.ToString();
                    viewModel.Name = viewModel.Subject;
                    viewModel.CreatedBy = "admin";
                    viewModel.CreatedOn = DateTime.Now;
                    viewModel.ModifiedBy = "admin";
                    viewModel.ModifiedOn = DateTime.Now;
                    bool success = await _cMSWebAppContent.Save(viewModel);
                    if (success)
                    {
                        viewModel.DataState = EDataState.Idle;
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        return View("Create", viewModel);
                    }

                }


            }
            catch (Exception ex)
            {
                string errorMessage = ex.Message;
                throw;
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            ApplicationContentViewModel applicationContentViewModel = await _cMSWebAppContent.GetAppContent(id);
            if (applicationContentViewModel != null)
            {
                applicationContentViewModel.DataState = EDataState.Edit;
                return View(applicationContentViewModel);
            }
            else
            {
                return RedirectToAction("Index");
            }

        }

        [HttpPost]
        public async Task<IActionResult> Edit(ApplicationContentViewModel viewModel)
        {

            return View();
        }
    }
}
