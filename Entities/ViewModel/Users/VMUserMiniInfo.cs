﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.ViewModel.Users
{
    public class VMUserMiniInfo
    {
        public string UserName { get; set; }
        public string NickName { get; set; }
        public string SecurityStamp { get; set; }
        public int Id { get; set; }
    }
}
