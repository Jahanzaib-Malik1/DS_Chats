using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication2.Models
{
    [Table("Message")]
    public partial class Messages
    {
        public int MessageId { get; set; }
        public string Message { get; set; }
        public int? SenderId { get; set; }
        public int? Chatid { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
    public partial class MessagesViewModel
    {
        public int MessageId { get; set; }
        public string Message { get; set; }
        public string SenderName { get; set; }
        public string userImage { get; set; }
        public int? SenderId { get; set; }
        public int? Chatid { get; set; }

        public DateTime? CreatedDate { get; set; }
    }
}
