using Bloggie.Web.Data.Repository.Interface;
using Bloggie.Web.Models.Domain;
using Bloggie.Web.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Bloggie.Web.Controllers
{
	[Authorize(Roles = "Admin")]
	public class AdminBlogPostsController : Controller
	{
        private readonly ITagRepository tagRepository;
        private readonly IBlogPostRepository blogPostRepository;

        public AdminBlogPostsController(ITagRepository tagRepository, IBlogPostRepository blogPostRepository)
        {
            this.tagRepository = tagRepository;
            this.blogPostRepository = blogPostRepository;
        }


        [HttpGet]
		public async Task<IActionResult> Add()
		{
			// get tags from repository
			var tags = await tagRepository.GetAllAsync();

			var model = new AddBlogPostRequest
			{
				Tags = tags.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() })
			};

			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> Add(AddBlogPostRequest addBlogPostRequest)
		{
			// Map view model to domain model
			var blogPost = new BlogPost
			{
				Heading = addBlogPostRequest.Heading,
				PageTitle = addBlogPostRequest.PageTitle,
				Content = addBlogPostRequest.Content,
				ShortDescription = addBlogPostRequest.ShortDescription,
				FeaturedImageUrl = addBlogPostRequest.FeaturedImageUrl,
				UrlHandle = addBlogPostRequest.UrlHandle,
				PublishedDate = addBlogPostRequest.PublishedDate,
				Author = addBlogPostRequest.Author,
				Visible = addBlogPostRequest.Visible,
			};

			// Map Tags from selected tags
			var selectedTags = new List<Tag>();
			foreach(var selectedTadId in addBlogPostRequest.SelectedTags)
			{
				var selectedTadIdAsGuid = Guid.Parse(selectedTadId);
				var existingTag = await tagRepository.GetAsync(selectedTadIdAsGuid);

				if(existingTag != null)
				{
					selectedTags.Add(existingTag);
				}
			}
			
			// Mapping tags back to domain model
			blogPost.Tags = selectedTags;

			await blogPostRepository.AddAsync(blogPost);

			return RedirectToAction("List");
		}

		[HttpGet]
		public async Task<IActionResult> List()
		{
			// Call the repository
			var blogPosts = await blogPostRepository.GetAllAsync();

			return View(blogPosts);
		}

		[HttpGet]
		public async Task<IActionResult> Edit(Guid id)
		{
			// Retrieve the result from the repository
			var blogPost = await blogPostRepository.GetAsync(id);
			var tagsDomainModel = await tagRepository.GetAllAsync();

			if(blogPost != null)
			{
				// map the domain model into the view model
				var model = new EditBlogPostRequest
				{
					Id = blogPost.Id,
					Heading = blogPost.Heading,
					PageTitle = blogPost.PageTitle,
					Content = blogPost.Content,
					Author = blogPost.Author,
					FeaturedImageUrl = blogPost.FeaturedImageUrl,
					UrlHandle = blogPost.UrlHandle,
					ShortDescription = blogPost.ShortDescription,
					PublishedDate = blogPost.PublishedDate,
					Visible = blogPost.Visible,
					Tags = tagsDomainModel.Select(x => new SelectListItem
					{
						Text = x.Name,
						Value = x.Id.ToString()
					}),
				};

				return View(model);
			}

			

			// pass date to view
			return View(null);
		}

		[HttpPost]
		public async Task<IActionResult> Edit(EditBlogPostRequest editBlogPostRequest)
		{
			// map view model back to domain model
			var blogPostDomainModel = new BlogPost
			{
				Id = editBlogPostRequest.Id,
				Heading = editBlogPostRequest.Heading,
				PageTitle = editBlogPostRequest.PageTitle,
				Content = editBlogPostRequest.Content,
				Author = editBlogPostRequest.Author,
				ShortDescription = editBlogPostRequest.ShortDescription,
				FeaturedImageUrl = editBlogPostRequest.FeaturedImageUrl,
				PublishedDate = editBlogPostRequest.PublishedDate,
				UrlHandle = editBlogPostRequest.UrlHandle,
				Visible = editBlogPostRequest.Visible,
			};

			// Map tags into domain model

			var SelectedTags = new List<Tag>();
			foreach(var selectedTag in editBlogPostRequest.SelectedTags)
			{
				if(Guid.TryParse(selectedTag, out var tag))
				{
					var foundTag = await tagRepository.GetAsync(tag);

					if(foundTag != null)
					{
						SelectedTags.Add(foundTag);
					}
				}
			}

			blogPostDomainModel.Tags = SelectedTags;

			// Submit information  to repository
			var updatedBlog = await blogPostRepository.UpdateAsync(blogPostDomainModel);

			if (updatedBlog != null)
			{
				// show success notification
				return RedirectToAction("List");
			}

            //show error notification
            return RedirectToAction("Edit");
        }

		[HttpPost]
		public async Task<IActionResult> Delete(EditBlogPostRequest editBlogPostRequest)
		{
			//Talk to repository to delete this Blog Post and tags
			var deletedBlogPost =  await blogPostRepository.DeleteAsync(editBlogPostRequest.Id);

			if (deletedBlogPost != null)
			{
				// Show success notification
				return RedirectToAction("List");
			}

			// show error notification
			return RedirectToAction("Edit", new {id = editBlogPostRequest.Id});
		}
	}
}
