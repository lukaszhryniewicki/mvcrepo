using System.Collections.Generic;

namespace FORUM_DYSKUSYJNE.Web.ViewModels
{

    public class CustomSerializeModel
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public ICollection<string> RoleName { get; set; }
    }
}