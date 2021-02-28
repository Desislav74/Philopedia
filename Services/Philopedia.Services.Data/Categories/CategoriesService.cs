namespace Philopedia.Services.Data.Categories
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using Philopedia.Data.Common.Repositories;
    using Philopedia.Data.Models;
    using Philopedia.Services.Mapping;
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

        public IEnumerable<T> GetAllWhitePaging<T>(int page, int itemsPerPage = 6)
        {
            var categories = this.categoriesRepository.All()
                .OrderByDescending(x => x.Id)
                .Skip((page - 1) * itemsPerPage).Take(itemsPerPage)
                .To<T>().ToList();
            return categories;
        }


        public IEnumerable<T> GetAll<T>(int? count = null)
        {
            IQueryable<Category> query =
                this.categoriesRepository.All().OrderBy(x => x.Name);
            if (count.HasValue)
            {
                query = query.Take(count.Value);
            }

            return query.To<T>().ToList();
        }
    }
}
