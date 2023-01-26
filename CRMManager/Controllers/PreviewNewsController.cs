using CRMLogic.Mall.Thiso.News;
using CRMManager.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CRMManager.Controllers
{
    public class PreviewNewsController : Controller
    {
        CMSWebNewsManagement _cmsnewsHandler;
        public PreviewNewsController(CMSWebNewsManagement cmsnewsHandler)
        {
            _cmsnewsHandler = cmsnewsHandler;
        }
        //public async Task<IActionResult> Index(string number, string langCode)
        //{

        //    PreviewNewsViewModel previewNewsViewModel = await _cmsnewsHandler.GetPreviewNewsDataByNumber(number, langCode);
        //    return View(previewNewsViewModel);
        //}
        public async Task<IActionResult> Index(int id)
        {

            PreviewNewsViewModel previewNewsViewModel = await _cmsnewsHandler.GetPreviewNewsDataByID(id);
            return View(previewNewsViewModel);
        }
    }
}
