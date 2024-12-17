using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PersonalBlog.DataAccess;
using PersonalBlog.Models;
using System.Security.Claims;

namespace PersonalBlog.Pages
{
    [AllowAnonymous]
	public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly JsonDataAccess _dataAccess;


		public List<SingleArticleModel> Articles { get; set; }
        public IndexModel(ILogger<IndexModel> logger, JsonDataAccess jsonDataAccess)
        {
            _logger = logger;
            _dataAccess = jsonDataAccess;
        }

        public async Task OnGetAsync()
        {
			//Call for the read method from the database
            Articles = await _dataAccess.ReadAllFromJsonFileAsync<SingleArticleModel>("articles.json");
		}
        public async Task<IActionResult> OnPostLoginAsync(string username, string password)
        {
			if (username == "admin" && password =="admin")
			{
				var claims = new[] { new Claim(ClaimTypes.Name, username) };
                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);
				await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                return RedirectToPage("/Admin");
			}

            ModelState.AddModelError(string.Empty, "Invalid Username or Password");
			return Page();
		}
        public async Task<IActionResult> OnPostLogoutAsync()
		{
			await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
			return RedirectToPage("/Index");
		}
	}
}
