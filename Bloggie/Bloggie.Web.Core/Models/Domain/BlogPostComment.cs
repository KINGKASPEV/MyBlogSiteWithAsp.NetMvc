namespace Bloggie.Web.Models.Domain
{
	public class BlogPostComment
	{
        public Guid Id { get; set; }
        public string Descripption { get; set; }
		public Guid BlogPostId { get; set; }
		public Guid UserId { get; set; }
        public DateTime DateAdded { get; set; }
    }
}
