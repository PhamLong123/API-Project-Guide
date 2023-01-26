//using Microsoft.AspNetCore.Mvc;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using CRMManager.Models;

//namespace CRMManager.Controllers
//{
//    public class CardListController : Controller
//    {
//        public CardListController()
//        {

//        }

//        public IActionResult Index(CardListViewModel cardListViewModel)
//        {
//            ViewBag.PageTitle = "QUẢN LÝ THẺ CỨNG";
//            List<CardListViewModel> cardListViewModels = new List<CardListViewModel>();
//            ViewBag.CardList = cardListViewModels;
//            return View(cardListViewModel);
//        }

//        public IActionResult Edit(int ID)
//        {
//            CardListViewModel cardListViewModel = new CardListViewModel();
//            return RedirectToAction("Index", cardListViewModel);
//        }

//        [HttpPost]
//        public IActionResult Edit(CardListViewModel cardListViewModel)
//        {
//            return RedirectToAction("Index", cardListViewModel);
//        }

//        [HttpPost]
//        public IActionResult AddNew(CardListViewModel cardListViewModel)
//        {
//            return RedirectToAction("Index", cardListViewModel);
//        }
//    }
//}
