//using AutoMapper;
//using CRMLogic.Base.CRMManager;
//using CRMManager.Models;
//using Microsoft.AspNetCore.Hosting;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using SmartBreadcrumbs.Attributes;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Threading.Tasks;

//namespace CRMManager.Controllers
//{
//    public class RedemptionController : Controller
//    {

//        private readonly IWebHostEnvironment _webhost;
//        //BasePromotionGroupOperation _basePromotionGroupOperation;
//        //BasePromotionGroupDetailOperation _basePromotionGroupDetailOperation;
//        BaseRedemptionItemsOperation _baseRedemptionItemsOperation;
//        BaseRedemptionGroupOperation _baseRedemptionGroupOperation;
//        BaseMemberClassOperation _baseMemberClassOperation;
//        RedeemItemMallsViewModel _riViewmodel;
//        RedemptionGroupsViewModel _rgViewmodel;
//        IMapper _mapper;

//        public RedemptionController(IWebHostEnvironment webHost, /*BasePromotionGroupOperation basePromotionGroupOperation,*/
//            /*BasePromotionGroupDetailOperation basePromotionGroupDetailOperation,*/ BaseRedemptionItemsOperation baseRedemptionItemsOperation,
//            BaseRedemptionGroupOperation baseRedemptionGroupOperation,
//            BaseMemberClassOperation baseMemberClassOperation,
//            IMapper mapper)
//        {
//            _webhost = webHost;
//            //_basePromotionGroupOperation = basePromotionGroupOperation;
//            //_basePromotionGroupDetailOperation = basePromotionGroupDetailOperation;
//            _baseRedemptionItemsOperation = baseRedemptionItemsOperation;
//            _baseRedemptionGroupOperation = baseRedemptionGroupOperation;
//            _baseMemberClassOperation = baseMemberClassOperation;
//            _mapper = mapper;
//            _riViewmodel = new RedeemItemMallsViewModel(_baseRedemptionItemsOperation, baseRedemptionGroupOperation, baseMemberClassOperation, _mapper);
//            _rgViewmodel = new RedemptionGroupsViewModel(_baseRedemptionGroupOperation, _mapper);
//        }
//        #region Group
//        [Breadcrumb("Nhóm quà")]
//        public async Task<IActionResult> Index()
//        {
//            await _rgViewmodel.GetData();
//            return View(_rgViewmodel);
//        }

//        [Breadcrumb("Nhóm quà")]
//        public async Task<IActionResult> Group()
//        {
//            await _rgViewmodel.GetData();
//            return View(_rgViewmodel);
//        }

//        [Breadcrumb("Thêm nhóm quà")]
//        public IActionResult AddNewGroup()
//        {
//            RedemptionGroupsViewModel model = new RedemptionGroupsViewModel();
//            return View(model);
//        }
//        [HttpPost]
//        public async Task<IActionResult> AddNewGroup(RedemptionGroupsViewModel model)
//        {
//            await _rgViewmodel.InsertData(model.RedemptionGroup);
//            return RedirectToAction("Group");
//        }

//        [Breadcrumb("Sửa nhóm quà")]
//        public async Task<IActionResult> EditGroup(int ID)
//        {
//            await _rgViewmodel.GetDataByID(ID);
//            return View(_rgViewmodel);
//        }
//        [HttpPost]
//        public async Task<IActionResult> EditGroup(RedemptionGroupsViewModel model)
//        {
//            await _rgViewmodel.UpdateData(model.RedemptionGroup);
//            return View(_rgViewmodel);
//        }

//        [HttpPost]
//        public IActionResult DeleteGroup()
//        {
//            return RedirectToAction("Group");
//        }

//        [HttpPost]
//        public IActionResult UploadimageGroup(IFormFile imgfile)
//        {
//            var saveimg = Path.Combine(_webhost.WebRootPath, "Dat'sImages", "Redemption", "Group", imgfile.FileName);
//            string imgext = Path.GetExtension(imgfile.FileName);
//            if (imgext == ".jpg" || imgext == ".png")
//            {
//                using (var uploadimg = new FileStream(saveimg, FileMode.Create))
//                {
//                    imgfile.CopyTo(uploadimg);
//                    ViewData["Message"] = "Ảnh" + imgfile.FileName + "đã được lưu";
//                }
//            }

//            return RedirectToAction("Group");
//        }
//        #endregion

//        #region Attribute
//        public IActionResult Attribute()
//        {
//            ViewBag.PageTitle = "THUỘC TÍNH QUÀ";
//            RedemptionAttributesViewModel model = new RedemptionAttributesViewModel();
//            return View(model);
//        }
//        public IActionResult AddNewAttribute(RedemptionAttributeViewModel model)
//        {
//            ViewBag.PageTitle = "THÊM THUỘC TÍNH QUÀ";
//            return View(model);
//        }
//        [HttpPost]
//        public IActionResult AddNewAttribute()
//        {
//            return RedirectToAction("Attribute");
//        }

//        public IActionResult EditAttribute(int ID)
//        {
//            ViewBag.PageTitle = "SỬA THÔNG TIN THUỘC TÍNH QUÀ";
//            var model = new RedemptionAttributesViewModel().RedemptionAttributes.Where(x => x.ID == ID).FirstOrDefault();
//            return View(model);
//        }
//        [HttpPost]
//        public IActionResult EditAttribute()
//        {
//            return RedirectToAction("Attribute");
//        }

//        [HttpPost]
//        public IActionResult DeleteAttribute()
//        {
//            return RedirectToAction("Attribute");
//        }

//        [HttpPost]
//        public IActionResult UploadimageAttribute(IFormFile imgfile)
//        {
//            var saveimg = Path.Combine(_webhost.WebRootPath, "Dat'sImages", "Redemption", "Attribute", imgfile.FileName);
//            string imgext = Path.GetExtension(imgfile.FileName);
//            if (imgext == ".jpg" || imgext == ".png")
//            {
//                using (var uploadimg = new FileStream(saveimg, FileMode.Create))
//                {
//                    imgfile.CopyTo(uploadimg);
//                    ViewData["Message"] = "Ảnh" + imgfile.FileName + "đã được lưu";
//                }
//            }

//            return RedirectToAction("Attribute");
//        }
//        #endregion

//        #region GroupFef
//        public IActionResult GroupRef()
//        {
//            ViewBag.PageTitle = "NHÓM QUÀ ĐẠI DIỆN";
//            RedemptionGroupRefsViewModel model = new RedemptionGroupRefsViewModel();
//            return View(model);
//        }
//        public IActionResult AddNewGroupRef(RedemptionGroupRefViewModel model)
//        {
//            ViewBag.PageTitle = "THÊM NHÓM QUÀ ĐẠI DIỆN";
//            return View(model);
//        }
//        [HttpPost]
//        public IActionResult AddNewGroupRef()
//        {
//            return RedirectToAction("GroupRef");
//        }

//        public IActionResult EditGroupRef(int ID)
//        {
//            ViewBag.PageTitle = "SỬA THÔNG TIN NHÓM QUÀ ĐẠI DIỆN";
//            var model = new RedemptionGroupRefsViewModel().RedemptionGroupRefs.Where(x => x.ID == ID).FirstOrDefault();
//            return View(model);
//        }
//        [HttpPost]
//        public IActionResult EditGroupRef()
//        {
//            return RedirectToAction("GroupRef");
//        }

//        [HttpPost]
//        public IActionResult DeleteGroupRef()
//        {
//            return RedirectToAction("GroupRef");
//        }
//        [HttpPost]
//        public IActionResult UploadiconGroupRef(IFormFile imgfile)
//        {
//            var saveimg = Path.Combine(_webhost.WebRootPath, "Dat'sImages", "Redemption", "GroupRef", imgfile.FileName);
//            string imgext = Path.GetExtension(imgfile.FileName);
//            if (imgext == ".jpg" || imgext == ".png")
//            {
//                using (var uploadimg = new FileStream(saveimg, FileMode.Create))
//                {
//                    imgfile.CopyTo(uploadimg);
//                    ViewData["Message"] = "Ảnh" + imgfile.FileName + "đã được lưu";
//                }
//            }

//            return RedirectToAction("GroupRef");
//        }
//        [HttpPost]
//        public IActionResult UploadimageGroupRef(IFormFile imgfile)
//        {
//            var saveimg = Path.Combine(_webhost.WebRootPath, "Dat'sImages", "Redemption", "GroupRef", imgfile.FileName);
//            string imgext = Path.GetExtension(imgfile.FileName);
//            if (imgext == ".jpg" || imgext == ".png")
//            {
//                using (var uploadimg = new FileStream(saveimg, FileMode.Create))
//                {
//                    imgfile.CopyTo(uploadimg);
//                    ViewData["Message"] = "Ảnh" + imgfile.FileName + "đã được lưu";
//                }
//            }

//            return RedirectToAction("GroupRef");
//        }
//        #endregion

//        #region RedeemItemMall
//        public async Task<IActionResult> RedeemItemMall()
//        {
//            await _riViewmodel.GetData();
//            return View(_riViewmodel);
//        }
//        public IActionResult AddNewRedeemItemMall()
//        {
//            RedeemItemMallsViewModel model = new RedeemItemMallsViewModel();
//            return View(model);
//        }
//        [HttpPost]
//        public async Task<IActionResult> AddNewRedeemItemMall(RedeemItemMallsViewModel model)
//        {
//            await _riViewmodel.InsertData(model.RedeemItemMall);
//            return RedirectToAction("RedeemItemMall");
//        }

//        public async Task<IActionResult> EditRedeemItemMall(int ID)
//        {
//            await _riViewmodel.GetDataByID(ID);
//            return View(_riViewmodel);
//        }
//        [HttpPost]
//        public async Task<IActionResult> EditRedeemItemMall(RedeemItemMallsViewModel model)
//        {
//            await _riViewmodel.UpdateData(model.RedeemItemMall);
//            return View(_riViewmodel);
//        }

//        [HttpPost]
//        public IActionResult DeleteRedeemItemMall()
//        {
//            return RedirectToAction("RedeemItemMall");
//        }
//        [HttpPost]
//        public IActionResult UploadiconRedeemItemMall(IFormFile imgfile)
//        {
//            var saveimg = Path.Combine(_webhost.WebRootPath, "Dat'sImages", "Redemption", "RedeemItemMall", imgfile.FileName);
//            string imgext = Path.GetExtension(imgfile.FileName);
//            if (imgext == ".jpg" || imgext == ".png")
//            {
//                using (var uploadimg = new FileStream(saveimg, FileMode.Create))
//                {
//                    imgfile.CopyTo(uploadimg);
//                    ViewData["Message"] = "Ảnh" + imgfile.FileName + "đã được lưu";
//                }
//            }

//            return RedirectToAction("RedeemItemMall");
//        }
//        [HttpPost]
//        public IActionResult UploadimageRedeemItemMall(IFormFile imgfile)
//        {
//            var saveimg = Path.Combine(_webhost.WebRootPath, "Dat'sImages", "Redemption", "RedeemItemMall", imgfile.FileName);
//            string imgext = Path.GetExtension(imgfile.FileName);
//            if (imgext == ".jpg" || imgext == ".png")
//            {
//                using (var uploadimg = new FileStream(saveimg, FileMode.Create))
//                {
//                    imgfile.CopyTo(uploadimg);
//                    ViewData["Message"] = "Ảnh" + imgfile.FileName + "đã được lưu";
//                }
//            }

//            return RedirectToAction("RedeemItemMall");
//        }
//        #endregion

//        #region RedeemItemAddjust
//        public IActionResult RedeemItemAddjust()
//        {
//            ViewBag.PageTitle = "ĐIỀU CHỈNH QUÀ";
//            RedeemItemAddjustsViewModel model = new RedeemItemAddjustsViewModel();
//            return View(model);
//        }
//        public IActionResult AddNewRedeemItemAddjust(RedeemItemAddjustViewModel model)
//        {
//            ViewBag.PageTitle = "THÊM ĐIỀU CHỈNH QUÀ";
//            return View(model);
//        }
//        [HttpPost]
//        public IActionResult AddNewRedeemItemAddjust()
//        {
//            return RedirectToAction("RedeemItemAdjust");
//        }
//        [HttpPost]
//        public IActionResult DeleteRedeemItemAddjust()
//        {
//            return RedirectToAction("RedeemItemAdjust");
//        }
//        #endregion

//        #region RedemptionPromotion
//        public IActionResult RedemptionPromotion()
//        {
//            ViewBag.PageTitle = "NHÓM KHUYẾN MÃI/QUÀ TẶNG";
//            RedemptionPromotionsViewModel model = new RedemptionPromotionsViewModel();
//            return View(model);
//        }
//        public IActionResult AddNewRedemptionPromotion(RedemptionPromotionViewModel model)
//        {
//            ViewBag.PageTitle = "THÊM NHÓM KHUYẾN MÃI/QUÀ TẶNG";
//            return View(model);
//        }
//        [HttpPost]
//        public IActionResult AddNewRedemptionPromotion()
//        {
//            return RedirectToAction("RedemptionPromotion");
//        }

//        public IActionResult EditRedemptionPromotion(int ID)
//        {
//            ViewBag.PageTitle = "SỬA THÔNG TIN NHÓM KHUYẾN MÃI/QUÀ TẶNG";
//            var model = new RedemptionPromotionsViewModel().RedemptionPromotions.Where(x => x.ID == ID).FirstOrDefault();
//            return View(model);
//        }
//        [HttpPost]
//        public IActionResult EditRedemptionPromotion()
//        {
//            return RedirectToAction("RedemptionPromotion");
//        }

//        [HttpPost]
//        public IActionResult DeleteRedemptionPromotion()
//        {
//            return RedirectToAction("RedemptionPromotion");
//        }
//        #endregion

//        #region PromotionGroupRedesign
//        public async Task<IActionResult> PromotionGroup(RedemptionPromotionViewModel model)
//        {
//            model = new RedemptionPromotionViewModel();
//            ViewBag.PageTitle = "NHÓM KHUYẾN MÃI/QUÀ TẶNG";
//            //List<BasePromotionGroup> basePromotionGroups = await _basePromotionGroupOperation.GetPromotionGroup();
//            //List<RedemptionPromotionViewModel> redemptionPromotionViewModels = _mapper.Map<List<RedemptionPromotionViewModel>>(basePromotionGroups);
//            List<RedemptionPromotionViewModel> redemptionPromotionViewModels = new List<RedemptionPromotionViewModel>();
//            ViewBag.PromotionGroup = redemptionPromotionViewModels;

//            return View(model);
//        }

//        public async Task<IActionResult> EditPromotionGroup(int ID)
//        {
//            RedemptionPromotionViewModel model = new RedemptionPromotionViewModel();
//            //BasePromotionGroup basePromotionGroup = await _basePromotionGroupOperation.GetSinglePromotionGroup(ID);
//            //RedemptionPromotionViewModel promotionViewModel = _mapper.Map<RedemptionPromotionViewModel>(basePromotionGroup);
//            //return RedirectToAction("PromotionGroup", promotionViewModel);
//            return View();
//        }

//        [HttpPost]
//        public async Task<IActionResult> EditPromotionGroup(RedemptionPromotionViewModel promotionViewModel)
//        {
//            //BasePromotionGroup basePromotionGroup = _mapper.Map<BasePromotionGroup>(promotionViewModel);
//            //var result = await _basePromotionGroupOperation.SaveData(basePromotionGroup);
//            //return RedirectToAction("PromotionGroup", promotionViewModel);
//            return View();
//        }

//        [HttpPost]
//        public async Task<IActionResult> AddNewPromotionGroup(RedemptionPromotionViewModel promotionViewModel)
//        {
//            //BasePromotionGroup basePromotionGroup = _mapper.Map<BasePromotionGroup>(promotionViewModel);
//            //var result = await _basePromotionGroupOperation.InsertData(basePromotionGroup);
//            //return RedirectToAction("PromotionGroup", promotionViewModel);
//            return View();
//        }
//        #endregion

//        #region PromotionGroupDetail
//        public async Task<IActionResult> PromotionGroupDetail(int ID)
//        {
//            ViewBag.PageTitle = "CHI TIẾT NHÓM KHUYẾN MÃI/QUÀ TẶNG";
//            //List<BasePromotionGroupDetail> basePromotionGroupDetails = await _basePromotionGroupDetailOperation.GetPromotionGroupDetailByGroupID(ID);
//            //List<PromotionGroupDetailViewModel> promotionGroupDetailViewModels = _mapper.Map<List<PromotionGroupDetailViewModel>>(basePromotionGroupDetails);
//            //List<BaseRedeemItemMall> baseRedeemItemMalls = await _baseRedeemItemMallOperation.GetRedeemItemMall();
//            //List<RedeemItemMallViewModel> redeemItemMallViewModels = _mapper.Map<List<RedeemItemMallViewModel>>(baseRedeemItemMalls);
//            //ViewBag.RedeemItemMall = redeemItemMallViewModels;
//            //ViewBag.GroupID = ID;
//            //return View(promotionGroupDetailViewModels);
//            return View();
//        }

//        public async Task<IActionResult> AddGiftToPromotionGroupDetail(int ID)
//        {
//            ViewBag.PageTitle = "THÊM QUÀ TẶNG";
//            //PromotionGroupDetail
//            //List<BasePromotionGroupDetail> basePromotionGroupDetails = await _basePromotionGroupDetailOperation.GetPromotionGroupDetailByGroupID(ID);
//            //List<PromotionGroupDetailViewModel> promotionGroupDetailViewModels = _mapper.Map<List<PromotionGroupDetailViewModel>>(basePromotionGroupDetails);
//            List<PromotionGroupDetailViewModel> promotionGroupDetailViewModels = new List<PromotionGroupDetailViewModel>();

//            //RedeemItemMall
//            //List<BaseRedeemItemMall> baseRedeemItemMalls = await _baseRedeemItemMallOperation.GetRedeemItemMall();
//            //List<RedeemItemMallViewModel> redeemItemMallViewModels = _mapper.Map<List<RedeemItemMallViewModel>>(baseRedeemItemMalls);

//            //PromotionGroup
//            //List<BasePromotionGroup> basePromotionGroups = await _basePromotionGroupOperation.GetPromotionGroup();
//            //List<RedemptionPromotionViewModel> redemptionPromotionViewModels = _mapper.Map<List<RedemptionPromotionViewModel>>(basePromotionGroups);


//            //ViewBag.PromotionGroup = redemptionPromotionViewModels;
//            //ViewBag.RedeemItemMall = redeemItemMallViewModels;
//            //ViewBag.GroupID = ID;
//            //return View(promotionGroupDetailViewModels);
//            return View();
//        }

//        [HttpPost]
//        public async Task<IActionResult> AddGiftToPromotionGroupDetail(List<PromotionGroupDetailViewModel> promotionGroupDetailViewModels)
//        {
//            //foreach(var item in promotionGroupDetailViewModels)
//            //{
//            //    if(item.IsActive == true)
//            //    {
//            //        PromotionGroupDetailViewModel promotionGroupDetailViewModel = item;
//            //        BasePromotionGroupDetail basePromotionGroupDetail = _mapper.Map<BasePromotionGroupDetail>(promotionGroupDetailViewModel);
//            //        var result = await _basePromotionGroupDetailOperation.InsertGift(basePromotionGroupDetail);
//            //    }
//            //}
//            return RedirectToAction("PromotionGroupDetail");
//        }
//        #endregion
//    }
//}
