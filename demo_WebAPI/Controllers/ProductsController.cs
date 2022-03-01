using _2_BUS.IService_BUS;
using _2_BUS.Models;
using _2_BUS.Service_BUS;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace demo_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private IProduct_Service _productService = new Service_formSP();
        [HttpGet(Name = "GetProduct/{id}")]
        public string GetProductDetail(int id)
        {
            List<ProductDetail> products = new List<ProductDetail>();
            products= _productService.LoadDatafromDAL();
            string a=products.Find(c => c.Product.id_Product == id).Product.products_Name;
            return a;

        }
    }
}
