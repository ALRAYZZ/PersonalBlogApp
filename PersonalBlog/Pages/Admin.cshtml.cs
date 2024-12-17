using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PersonalBlog.DataAccess;
using PersonalBlog.Models;

namespace PersonalBlog.Pages
{
    public class AdminModel : PageModel
    {
		private readonly JsonDataAccess _dataAccess;
		public List<SingleArticleModel> Articles { get; set; }
		public AdminModel(JsonDataAccess dataAccess)
		{
			_dataAccess = dataAccess;
		}

		public async Task OnGetAsync()
        {
			Articles = await _dataAccess.ReadAllFromJsonFileAsync<SingleArticleModel>("articles.json");
		}
		public async Task<IActionResult> OnPostLogoutAsync()
		{
			await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
			return RedirectToPage("/Index");
		}
		public async Task<IActionResult> OnPostDeleteAsync(int id)
		{
			var articles = await _dataAccess.ReadAllFromJsonFileAsync<SingleArticleModel>("articles.json");
			var article = articles.FirstOrDefault(a => a.Id == id);

			if (article != null)
			{
				articles.Remove(article);
				await _dataAccess.WriteToJsonFileAsync("articles.json", articles);
			}
			return RedirectToPage("/Admin");
		}
	}
}
