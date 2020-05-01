using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorApp1.Data
{
    public class ResponseMessage
    {
        public int NotificationID { get; set; }
        public int RequestId { get; set; }
        public string Description { get; set; }
        public string UserName { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
