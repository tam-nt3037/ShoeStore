using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Week02.Models
{
    public class UserNode
    {
        public string Key { get; set; }
        public User User { get; set; }
        public UserNode(string key, User user )
        {
            this.Key = key;
            this.User = user;
        }
    }
}