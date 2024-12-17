using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PersonalBlog.DataAccess;
using PersonalBlog.Models;

namespace PersonalBlog.Pages
{
	[AllowAnonymous]
	public class ArticleModel : PageModel
    {
        private readonly JsonDataAccess _dataAccess;

		public ArticleModel(JsonDataAccess dataAccess)
		{
			_dataAccess = dataAccess;
		}
		[BindProperty(SupportsGet = true)]
		public int Id { get; set; }
		public SingleArticleModel Article { get; set; }
		public async Task OnGetAsync()
        {
			var articles = await _dataAccess.ReadAllFromJsonFileAsync<SingleArticleModel>("articles.json");
			Article = articles.FirstOrDefault(a => a.Id == Id);
		}
		public async Task<IActionResult> OnPostLogoutAsync()
		{
			await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
			return RedirectToPage("/Index");
		}
	}
}
