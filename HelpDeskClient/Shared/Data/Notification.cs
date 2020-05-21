using Dapper.Contrib.Extensions;
using System;

namespace HelpDesk.Shared.Data
{
    [Table("Notification")]
    public partial class Notification
    {
        [Key]
        public long NotificationId { get; set; }
        public long RequestId { get; set; }
        public string Description { get; set; }
        public bool Read { get; set; }
        public long EditedBy { get; set; }
        public DateTimeOffset EditedOn { get; set; }
    }
}
