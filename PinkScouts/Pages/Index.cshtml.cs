using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PSClassLib.Repo;
using PSClassLib.Model;
using PSClassLib.Interface;
using System.Security.Claims;
using PSClassLib;
using System.Linq.Expressions;

namespace PinkScouts.Pages
{
    public class IndexModel : PageModel
    {
        private readonly UserService _userService;

        public IndexModel(IRepo<User> repo)
        {
            _userService = new UserService(repo);
        }



        [BindProperty]
        public int UserId { get; set; }

        [BindProperty]
        public string Password { get; set; }

        

        public void OnGet()
        {

        }
        public async Task OnPost()
        {
            if (ModelState.IsValid)
            {
                try
                {
                    User user = _userService.ValidateUser(UserId, Password);


                    var claims = new List<Claim> { new Claim(ClaimTypes.Name, UserId.ToString()) };
                    if (user.IsLeader)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, "leader"));
                        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
                    }
                    // User is valid, perform login actions here
                    Response.Redirect("/profile");

                }
                catch (Exception e)
                {
                    Response.Redirect("/index");
                }
                
            }
        }

    }
}
