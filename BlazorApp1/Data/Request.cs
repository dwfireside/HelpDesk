using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorApp1.Data
{
    [Table("Request")]
    public class Request
    {
        [Key]
        public long RequestId { get; set; }
        public long EventId { get; set; }
        public long RoomId { get; set; }
        public long IssueId { get; set; }
        public string Description { get; set; }
        public bool HelpNeeded { get; set; }
        public bool RaiseTemperature { get; set; }
        public bool LowerTemperature { get; set; }
        public bool ResolvedIssue { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
        public long CreatedBy { get; set; }
        public long EditedBy { get; set; }
        public DateTimeOffset EditedOn { get; set; }
    }


    public class RequestEx
    {
        public int RequestID { get; set; }
        public string EventName { get; set; }
        public string Description { get; set; }
        public DateTime CreatedOn { get; set; }
        public int NotificationCount { get; set; }
        public bool  IsResolved { get; set; }
        public DateTime EditedOn { get; set; }
    }


}
