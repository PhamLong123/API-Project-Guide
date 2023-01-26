using System.Collections.Generic;

namespace CSService.Models
{
    public class Config
    {
        public string SourceNumber { get; set; }
        public string Key { get; set; }
        public string UserName { get; set; }
        public string Pwd { get; set; }
        public string Provider { get; set; }
        public string Language { get; set; }
    }

    public class MessageInfo : Config
    {
        public int ID { get; set; }
        public int TemplateID { get; set; }
        public string ListName { get; set; }
        public CSMethod SendMethod { get; set; }
        public FromObject From { get; set; }
        public List<ToObject> Tos { get; set; }
        public ContentParameter ContentParameter { get; set; }
        public bool IsTemplateRequest { get; set; }
    }
}
