//using AutoMapper;
//using CRMLogic.Base.BaseBO;
//using CRMLogic.Base.BusinessObject;
//using CRMLogic.Base.CRMManager;
//using CRMManager.Models;
//using Microsoft.AspNetCore.Mvc;
//using System.Collections.Generic;
//using System.Threading.Tasks;

//namespace CRMManager.Controllers
//{
//    public class PromotionGiftGroupController : Controller
//    {
//        BaseRedemptionItemsOperation _baseRedemptionItemsOperation;
       
//        IMapper _mapper;
//        public PromotionGiftGroupController(BaseRedemptionItemsOperation baseRedemptionItemsOperation,
            
//            IMapper mapper)
//        {
//            _baseRedemptionItemsOperation = baseRedemptionItemsOperation;
            
//            _mapper = mapper;
//        }
//        public async Task<IActionResult> Index()
//        {
//            PromotionGiftGroupViewModel data = new PromotionGiftGroupViewModel();

//            //List<BaseRedemptionItems> redemptionItems = await _baseRedemptionItemsOperation.GetRedemptionItems();
//            //List<RedemptionItemsVM> promotionGifts = _mapper.Map<List<RedemptionItemsVM>>(redemptionItems);

//            data.GetData();

//            //data.PromotionGroups = promotionGroup;
//            //data.RedemptionItems = promotionGifts;

//            data.PageTitle = "Quản trị nhóm quà khuyến mãi";
//            return View(data);
//        }
//    }
//}
