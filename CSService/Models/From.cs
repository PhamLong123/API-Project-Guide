namespace CSService.Models
{
    public enum CSMethod
    {
        None = 0,
        SMS = 1,
        SMSTemp = 2,
        Email = 3,
        Notification = 4,
        SocialMessage = 5,
        FacebookMessage = 6,
        ZaloMessage = 7,
    }

    public enum TransType
    {
        Account = 0,
        Sale = 1,
        News = 2,
        Gift = 3,
        Lottery = 4,
        Redeem = 5,
        Promotion = 6
    }

    public class FromObject
    {
        public string Address { get; set; }
        public string Key { get; set; }
        public string UserName { get; set; }
        public string Pwd { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public string Alias { get; set; }
    }

    public class ContentParameter
    {
        public int TypeID { get; set; }
        public int CatID { get; set; }
        public string NewsID { get; set; }
        public string NewsLink { get; set; }
        public TransType TransType { get; set; }
        public string TransStatus { get; set; }
        public string NewsTag { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
    }
}
