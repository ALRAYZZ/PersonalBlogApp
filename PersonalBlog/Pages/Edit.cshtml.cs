using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PersonalBlog.DataAccess;
using PersonalBlog.Models;

namespace PersonalBlog.Pages
{
    public class EditModel : PageModel
    {
        private readonly JsonDataAccess _dataAccess;
		[BindProperty]
		public SingleArticleModel Article { get; set; }
		[BindProperty(SupportsGet = true)]
		public int Id { get; set; }

		public EditModel(JsonDataAccess dataAccess)
		{
			_dataAccess = dataAccess;
		}
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
		public async Task<IActionResult> OnPostUpdateAsync()
		{
			var articles = await _dataAccess.ReadAllFromJsonFileAsync<SingleArticleModel>("articles.json");
			var article = articles.FirstOrDefault(a => a.Id == Id);
			if (article == null)
			{
				return NotFound();
			}
			if (Article == null)
			{
				return BadRequest("Article data is missing");
			}
			article.Title = Article.Title;
			article.Content = Article.Content;
			article.Date = DateTime.Now;
			await _dataAccess.WriteToJsonFileAsync("articles.json", articles);
			return RedirectToPage("/Admin");
		}
	}
}
