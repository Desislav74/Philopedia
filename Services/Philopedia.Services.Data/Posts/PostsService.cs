using Microsoft.AspNetCore.Http;

namespace Philopedia.Services.Data.Posts
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using Philopedia.Data.Common.Repositories;
    using Philopedia.Data.Models;
    using Philopedia.Web.ViewModels.Posts;

    public class PostsService : IPostsService
    {
        private readonly string[] allowedExtensions = new[] { "jpg", "png", "gif" };
        private readonly IDeletableEntityRepository<Post> postsRepository;

        public PostsService(IDeletableEntityRepository<Post> postsRepository)
        {
            this.postsRepository = postsRepository;
        }

        public async Task<int> CreateAsync(PostCreateInputModel input, string userId, string imagePath)
        {
            var post = new Post
            {
                CategoryId = input.CategoryId,
                Content = input.Content,
                Title = input.Title,
                UserId = userId,
            };
            Directory.CreateDirectory($"{imagePath}/posts/");
            foreach (var image in input.Images)
            {
                var extension = Path.GetExtension(image.FileName).TrimStart('.');
                if (!this.allowedExtensions.Any(x => extension.EndsWith(x)))
                {
                    throw new Exception($"Invalid image extension {extension}");
                }

                var dbImage = new Image
                {
                    Extension = extension,
                };
                post.Images.Add(dbImage);
                var physicalPath = $"{imagePath}/posts/{dbImage.Id}.{extension}";
                await using Stream fileStream = new FileStream(physicalPath, FileMode.Create);
                await image.CopyToAsync(fileStream);
            }
            await this.postsRepository.AddAsync(post);
            await this.postsRepository.SaveChangesAsync();
            return post.Id;
        }
    }
}
