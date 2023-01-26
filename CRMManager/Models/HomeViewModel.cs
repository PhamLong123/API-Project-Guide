namespace Demo.Models
{
    public class HomeViewModel
    {
        public string PageTitle { get; set; }
        public string MemberOverView { get; set; }
        public string MemberTypeView { get; set; }
        public string TransactionOverView { get; set; }
        public string TransactionTypeView { get; set; }
        public string GiftOverView { get; set; }
        public string GiftTypeView { get; set; }

        public HomeViewModel() {
            PageTitle = "Demo mẫu báo cáo";
            MemberOverView = "Thống kê khách hàng";
            MemberTypeView = "Phân loại khách hàng";
            TransactionOverView = "Thống kê giao dịch";
            TransactionTypeView = "Phân loại giao dịch";
            GiftOverView = "Thống kê tặng quà";
            GiftTypeView = "Phân loại quà";
        }
    }
}
