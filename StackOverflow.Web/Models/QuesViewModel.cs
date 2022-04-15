using StackOverflow.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StackOverflow.Web.Models
{
    public class QuesViewModel
    {
        public Question Question { get; set; }
        public bool LikedAlready { get; set; }
        public int NumLikes { get; set; }
        public bool LoggedIn { get; set; }
    }
}
