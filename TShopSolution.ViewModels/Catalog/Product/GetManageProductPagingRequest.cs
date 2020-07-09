using System;
using System.Collections.Generic;
using System.Text;
using TShopSolution.ViewModels.Common;

namespace TShopSolution.ViewModels.Catalog.Product
{
    public class GetManageProductPagingRequest : PagingRequestBase
    {
        public string KeyWord { get; set; }

        public List<int> CategoryIds { get; set; }
    }
}
