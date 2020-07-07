using System.Collections.Generic;
using System.Threading.Tasks;
using TShopSolution.ViewModels.Catalog.Product;
using TShopSolution.ViewModels.Common;

namespace TShopSolution.Application.Catalog.Products
{
    public interface IPublicProductService
    {
        Task<PageResult<ProductViewModel>> GetAllProductByCategoryId(GetPublicProductPagingRequest request);
        Task<List<ProductViewModel>> GetAll();
    }
}
