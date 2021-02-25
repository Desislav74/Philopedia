namespace Philopedia.Services.Data.Categories
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using Philopedia.Data.Common.Repositories;
    using Philopedia.Data.Models;
    using Philopedia.Web.ViewModels.Categories;

    public class CategoriesService : ICategoriesService
    {
        private readonly string[] allowedExtensions = new[] { "jpg", "png", "gif" };
        private readonly IDeletableEntityRepository<Category> categoriesRepository;

        public CategoriesService(IDeletableEntityRepository<Category> categoriesRepository)
        {
            this.categoriesRepository = categoriesRepository;
        }

        public async Task CreateAsync(CategoryInputModel input, string imagePath)
        {
            var category = new Category()
            {
                Name = input.Name,
                Title = input.Title,
                Description = input.Description,
            };
            Directory.CreateDirectory($"{imagePath}/categories/");
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
                category.Images.Add(dbImage);
                var physicalPath = $"{imagePath}/categories/{dbImage.Id}.{extension}";
                await using Stream fileStream = new FileStream(physicalPath, FileMode.Create);
                await image.CopyToAsync(fileStream);
            }

            await this.categoriesRepository.AddAsync(category);
            await this.categoriesRepository.SaveChangesAsync();
        }
    }
}
