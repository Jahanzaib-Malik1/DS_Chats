using System;
using System.Collections.Generic;

namespace WebApplication2.Models
{
    public partial class Chat
    {
        public int ChatId { get; set; }
        public string ChatName { get; set; }
        public int? ChatType { get; set; }
    }
}
