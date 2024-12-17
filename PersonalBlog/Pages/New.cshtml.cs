using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PersonalBlog.DataAccess;
using PersonalBlog.Models;

namespace PersonalBlog.Pages
{
    public class NewModel : PageModel
    {
        private readonly JsonDataAccess _dataAccess;
		[BindProperty]
		public SingleArticleModel Article { get; set; }
		public NewModel(JsonDataAccess dataAccess)
		{
			_dataAccess = dataAccess;
		}

		public void OnGet()
        {
        }
		public async Task<IActionResult> OnPostCreateAsync()
		{
			var articles = await _dataAccess.ReadAllFromJsonFileAsync<SingleArticleModel>("articles.json");
			if (Article == null)
			{
				return BadRequest("Article data is missing");
			}
			Article.Id = articles.Any() ? articles.Max(a => a.Id) + 1 : 1;
			Article.Date = DateTime.Now;
			articles.Add(Article);
			await _dataAccess.WriteToJsonFileAsync("articles.json", articles);

			return RedirectToPage("/Admin");
		}

	}
}
