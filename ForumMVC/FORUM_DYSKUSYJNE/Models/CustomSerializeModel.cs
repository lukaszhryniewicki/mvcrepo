using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FORUM_DYSKUSYJNE.Models
{

    public class CustomSerializeModel
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public ICollection<string> RoleName { get; set; }
    }
}