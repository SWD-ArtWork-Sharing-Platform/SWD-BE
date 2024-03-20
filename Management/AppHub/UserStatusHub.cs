using Management.Data;
using Management.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Management.AppHub
{
    public class UserStatusHub :Hub
    {
        public static IEnumerable<ApplicationUser> CurrentUser { get; set; }

        private readonly UserManager<ApplicationUser> _userManager;

        public UserStatusHub(UserManager<ApplicationUser> _userManage)
        {
            _userManager = _userManage;
        }

        public override Task OnConnectedAsync()
        {
            CurrentUser = _userManager.Users.ToList();
            Clients.Caller.SendAsync("updateTotalUser", CurrentUser).GetAwaiter().GetResult();
            return base.OnConnectedAsync();
        }
        

    }
}
