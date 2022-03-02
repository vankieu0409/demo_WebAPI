using System.Runtime.CompilerServices;
using _2_BUS.IService_BUS;
using _2_BUS.Models;
using _2_BUS.Service_BUS;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;
using NuGet.Protocol;

namespace demo_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private IProduct_Service _productService = new Service_formSP();
        [HttpGet(Name = "GetProduct/{id}")]
        public ProductDetailTempplate GetProductDetail(int id)
        {
            List<ProductDetailTempplate> products = new List<ProductDetailTempplate>();
            products = _productService.LoadDatafromDAL();
            ProductDetailTempplate qq = products.FirstOrDefault(c => c.Id == id);


            return qq;


        }
        //[HttpGet(Name = "GetProducttest/{id}")]
        //public string GetProductDetailTest(int id)
        //{
        //    List<ProductDetailTempplate> products = new List<ProductDetailTempplate>();
        //    products = _productService.LoadDatafromDAL();
        //    ProductDetailTempplate qq = products.FirstOrDefault(c => c.Id == id);

        //    string kku= $"Tên:{qq.Name}; Sku: {qq.Skud} {_productService.ForeachOption(qq.ThuocTinhList)}";
        //    return kku;


        //}
    }
}
