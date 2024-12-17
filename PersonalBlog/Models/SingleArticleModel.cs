namespace PersonalBlog.Models
{
	public class SingleArticleModel
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public string Content { get; set; }
		public DateTime Date { get; set; } = DateTime.Now;
	}
}
