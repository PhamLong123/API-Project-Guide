using System.Collections.Generic;

namespace CSService.Models
{
    public class ToObject
    {
        public string To { get; set; }
        public string Name { get; set; }
        public string ProjectID { get; set; }
        public string[] CC { get; set; }
        public string[] BCC { get; set; }
        public string[] Attachments { get; set; }
        public string Device { get; set; }
        public string StatusCode { get; set; }
        public bool ByConversion { get; set; }
        public bool ByMobile { get; set; }
        public Dictionary<string, object> Parameters { get; set; }
    }
}
