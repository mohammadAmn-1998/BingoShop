using Microsoft.AspNetCore.Mvc;
using Shared.Application.Models;
using Users.Application.Services.Interfaces;

namespace BingoShop.WebApplication.Areas.Admin.Controllers.Users
{
    [Area("Admin")]
	[Route("/Admin/Users")]
    public class UsersController : Controller
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        public IActionResult Index(int pageId = 1 , int take = 2 , string q = "")
        {
            return View(_userService.GetFilteredUserForAdmin(new FilterParams()
            {
				PageId = pageId,
				Take = take,
				Title = q,

            }));
        }
    }
}
