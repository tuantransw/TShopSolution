using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TShopSolution.Application.Catalog.Products;
using TShopSolution.ViewModels.Catalog.Product;

namespace TShopSolution.BackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IPublicProductService _publicProductService;
        private readonly IManageProductService _manageProductService;

        public ProductController(IPublicProductService publicProductService,IManageProductService manageProductService)
        {
            _publicProductService = publicProductService;
            _manageProductService = manageProductService;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var product = await _publicProductService.GetAll();
            return Ok(product);
        }

        [HttpGet("public-paging")]
        public async Task<IActionResult> GetAllProductById([FromQuery]GetPublicProductPagingRequest request)
        {
            var product = await _publicProductService.GetAllProductByCategoryId(request);
            return Ok(product);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody]ProductCreateRequest request)
        {
            var result = await _manageProductService.Create(request);
            if (result == 0)
                return BadRequest();

            return Ok(); 
        }
    }
}
