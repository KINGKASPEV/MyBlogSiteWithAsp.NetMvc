using Bloggie.Web.Data.Repository.Interface;
using Bloggie.Web.Models.Domain;
using Bloggie.Web.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Bloggie.Web.Controllers
{
	public class BlogsController : Controller
	{
		private readonly IBlogPostRepository blogPostRepository;
		private readonly IBlogPostLikeRepository blogPostLikeRepository;
		private readonly SignInManager<IdentityUser> signInManager;
		private readonly UserManager<IdentityUser> userManager;
		private readonly IBlogPostCommentRepository blogPostCommentRepository;

		public BlogsController(IBlogPostRepository blogPostRepository,
			IBlogPostLikeRepository blogPostLikeRepository,
			SignInManager<IdentityUser> signInManager,
			UserManager<IdentityUser> userManager,
			IBlogPostCommentRepository blogPostCommentRepository)
		{
			this.blogPostRepository = blogPostRepository;
			this.blogPostLikeRepository = blogPostLikeRepository;
			this.signInManager = signInManager;
			this.userManager = userManager;
			this.blogPostCommentRepository = blogPostCommentRepository;
		}


		[HttpGet]
		public async Task<IActionResult> Index(string urlHandle)
		{
			var liked = false;
			var blogPost = await blogPostRepository.GetByUrlHandleAsync(urlHandle);

			var blogDetailsViewModel = new BlogDetailsViewModel();

			if (blogPost != null)
			{
				var totalLikes = await blogPostLikeRepository.GetTotalLikes(blogPost.Id);

				if (signInManager.IsSignedIn(User))
				{
					// Get like for this user
					var likesForBlog = await blogPostLikeRepository.GetLikesForBlog(blogPost.Id);

					var userId = userManager.GetUserId(User);

					if (userId != null)
					{
						var likeFromUser = likesForBlog.FirstOrDefault(x => x.UserId == Guid.Parse(userId));
						liked = likeFromUser != null;
					}
				}

				//Get comments for blog post
				var blogCommentsDomainModel = await blogPostCommentRepository
					.GetCommentsByBlogIdAsync(blogPost.Id);

				var blogCommentForView = new List<BlogComment>();

				foreach (var blogComment in blogCommentsDomainModel)
				{
					blogCommentForView.Add(new BlogComment
					{
						Description = blogComment.Descripption,
						DateAdded = blogComment.DateAdded,
						Username = (await userManager.FindByIdAsync(blogComment.UserId
						.ToString())).UserName
					}) ; 
				}

				blogDetailsViewModel = new BlogDetailsViewModel
				{
					Id = blogPost.Id,
					Content = blogPost.Content,
					PageTitle = blogPost.PageTitle,
					Author = blogPost.Author,
					FeaturedImageUrl = blogPost.FeaturedImageUrl,
					Heading = blogPost.Heading,
					PublishedDate = blogPost.PublishedDate,
					ShortDescription = blogPost.ShortDescription,
					UrlHandle = blogPost.UrlHandle,
					Visible = blogPost.Visible,
					Tags = blogPost.Tags,
					TotalLikes = totalLikes,
					Liked = liked,
					Comments = blogCommentForView
				};
			}

			return View(blogDetailsViewModel);
		}

		[HttpPost]
		public async Task<IActionResult> Index(BlogDetailsViewModel blogDetailsViewModel)
		{
			if (signInManager.IsSignedIn(User))
			{
				var domainModel = new BlogPostComment
				{
					BlogPostId = blogDetailsViewModel.Id,
					Descripption = blogDetailsViewModel.CommentDescription,
					UserId = Guid.Parse(userManager.GetUserId(User)),
					DateAdded = DateTime.Now
				};

				await blogPostCommentRepository.AddAsync(domainModel);
				return RedirectToAction("Index", "Blogs", new { urlHandle = 
					blogDetailsViewModel.UrlHandle});
			}
			return View();
		}
	}
}
