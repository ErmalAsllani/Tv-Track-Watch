using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TV_Track.ViewModels
{
    public class AccountViewModel
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
