using System.Collections.Generic;
using _2_BUS.Models;
using DAL;
namespace _2_BUS.IService_BUS
{
    public interface IProduct_Service
    {
        public List<ProductDetail> LoadDatafromDAL();
       
        public string addNewProduct(string name);

        public string editProductDetail(ProductDetail productDetail);
        public string addProductVariant(ProductDetail productDetail);
        public string updateQuantity(ProductDetail productDetail);
        public string removeProductVariant(ProductDetail productDetail);
        public string editProductVariant(ProductDetail productDetail);
        public string removeProductDetail(ProductDetail productDetail);
        public string addProductDetail(ProductDetail productDetail);


    }
}