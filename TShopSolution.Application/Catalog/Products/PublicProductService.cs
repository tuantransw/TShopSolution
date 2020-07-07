using System.Threading.Tasks;
using TShopSolution.Data.EF;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TShopSolution.ViewModels.Catalog.Product;
using TShopSolution.ViewModels.Common;
using System.Collections.Generic;

namespace TShopSolution.Application.Catalog.Products
{
    public class PublicProductService : IPublicProductService
    {
        private readonly TShopDbContext _context;
        public PublicProductService(TShopDbContext context)
        {
            _context = context;
        }

        public async Task<List<ProductViewModel>> GetAll()
        {
            // B1: Select join
            var query = from p in _context.Products
                        join pt in _context.ProductTranslations on p.Id equals pt.ProductId
                        join pic in _context.ProductInCategories on p.Id equals pic.ProductId
                        join c in _context.Categories on pic.CategoryId equals c.Id
                        select new { p, pt, pic };
            // B2: Filter
            //if (!string.IsNullOrEmpty(request.KeyWord))
            //{
            //    query = query.Where(x => x.pt.Name.Contains(request.KeyWord));
            //}
            //if (request.CategoryId.HasValue && request.CategoryId.Value > 0)
            //{
            //    query = query.Where(p => p.pic.CategoryId == request.CategoryId);
            //}

            //// B3: Paging
            //int totalRow = await query.CountAsync();
            var data = await query.Select(x => new ProductViewModel()
                {
                    Id = x.pt.Id,
                    Name = x.pt.Name,
                    DateCreated = x.p.DateCreated,
                    Description = x.pt.Description,
                    Details = x.pt.Details,
                    LanguageId = x.pt.LanguageId,
                    OriginalPrice = x.p.OriginalPrice,
                    Price = x.p.Price,
                    SeoAlias = x.pt.SeoAlias,
                    SeoDescription = x.pt.SeoDescription,
                    SeoTitle = x.pt.SeoTitle,
                    Stock = x.p.Stock,
                    ViewCount = x.p.ViewCount,

                }).ToListAsync();


            // B4: Select and projection
            //var pageResult = new PageResult<ProductViewModel>()
            //{
            //    TotalRecord = totalRow,
            //    Items = data
            //};
            return data;
        }

        public async Task<PageResult<ProductViewModel>> GetAllProductByCategoryId(GetPublicProductPagingRequest request)
        {
            // B1: Select join
            var query = from p in _context.Products
                        join pt in _context.ProductTranslations on p.Id equals pt.ProductId
                        join pic in _context.ProductInCategories on p.Id equals pic.ProductId
                        join c in _context.Categories on pic.CategoryId equals c.Id
                        select new { p, pt, pic };
            // B2: Filter
            //if (!string.IsNullOrEmpty(request.KeyWord))
            //{
            //    query = query.Where(x => x.pt.Name.Contains(request.KeyWord));
            //}
            if (request.CategoryId.HasValue && request.CategoryId.Value > 0)
            {
                query = query.Where(p => p.pic.CategoryId == request.CategoryId);
            }

            // B3: Paging
            int totalRow = await query.CountAsync();
            var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new ProductViewModel()
                {
                    Id = x.pt.Id,
                    Name = x.pt.Name,
                    DateCreated = x.p.DateCreated,
                    Description = x.pt.Description,
                    Details = x.pt.Details,
                    LanguageId = x.pt.LanguageId,
                    OriginalPrice = x.p.OriginalPrice,
                    Price = x.p.Price,
                    SeoAlias = x.pt.SeoAlias,
                    SeoDescription = x.pt.SeoDescription,
                    SeoTitle = x.pt.SeoTitle,
                    Stock = x.p.Stock,
                    ViewCount = x.p.ViewCount,

                }).ToListAsync();

            // B4: Select and projection
            var pageResult = new PageResult<ProductViewModel>()
            {
                TotalRecord = totalRow,
                Items = data
            };
            return pageResult;
        }
    }
}
