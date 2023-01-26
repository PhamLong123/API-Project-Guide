//using AutoMapper;
//using CRMLogic.Base.BaseBO;
//using CRMLogic.Base.BusinessObject;
//using CRMLogic.Base.CRMManager;
//using CRMManager.Models;
//using Microsoft.AspNetCore.Mvc;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using static CRMManager.Models.MKTCampaignBillViewModel;

//namespace CRMManager.Controllers
//{
//    public class MKTCampaignBillController : Controller
//    {
//        BaseRedemptionItemsOperation _baseRedemptionItemsOperation;
        
//        IMapper _mapper;
//        public MKTCampaignBillController(BaseRedemptionItemsOperation baseRedemptionItemsOperation,
            
//            IMapper mapper)
//        {
//            _baseRedemptionItemsOperation = baseRedemptionItemsOperation;
          
//            _mapper = mapper;
//        }
//        public async Task<IActionResult> Index()
//        {
//            MKTCampaignBillViewModel data = new MKTCampaignBillViewModel();
//            data.PageTitle = "Chương trình khuyến mãi theo bill";

//            List<IGeneralDataSource> itemTypes = IGeneralDataSource.GetDataSource<ERedeemItemType>();
//            List<PromotionGroupViewModel> promotionGroup = ((ERedeemItemType[])Enum.GetValues(typeof(ERedeemItemType))).Select(c => new PromotionGroupViewModel() { ID = (int)c, Name = c.ToString() }).ToList();

//            //List<BaseRedemptionItems> redemptionItems = await _baseRedemptionItemsOperation.GetRedemptionItems();
//            //List<RedemptionItemsVM> promotionGifts = _mapper.Map<List<RedemptionItemsVM>>(redemptionItems);

//            data.PromotionGroups = promotionGroup;
//            //data.PromotionGifts = promotionGifts;

//            return View(data);
//        }

//        private int List<T>(List<BaseRedemptionItems> redemptionItems)
//        {
//            throw new NotImplementedException();
//        }
//    }
//}
