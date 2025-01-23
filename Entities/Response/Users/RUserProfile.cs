using Entities.Enum.Users;
using Entities.ViewModel.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Response.Users
{
    public class RUserProfile : VMUserMiniInfo
    {
        public int FriendshipPending { get; set; }
        public List<RUserBoxScenario> Scenarios { get; set; }
    }
}
