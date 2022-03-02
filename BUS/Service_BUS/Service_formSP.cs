
using _2_BUS.IService_BUS;
using _2_BUS.Models;
using DAL.IServices;
using DAL;
using DAL.Service;
using Microsoft.Extensions.DependencyInjection;


namespace _2_BUS.Service_BUS
{
    public class Service_formSP : IProduct_Service
    {
        private IGeneric_Sevice<PRODUCTS> PS;
        private IGeneric_Sevice<PRODUCTS_VARIANTS> PV;
        private IGeneric_Sevice<PRODUCTS_OPTIONS> PO;
        private IGeneric_Sevice<OPTIONS> OS;
        private IGeneric_Sevice<OPTIONS_VALUES> OV;
        private IGeneric_Sevice<VARIANTS_VALUES> Vv;
        private List<PRODUCTS> _lstProductses;
        private List<PRODUCTS_VARIANTS> _lstProductsVariantses;
        private List<PRODUCTS_OPTIONS> _lsProductsOptionses;
        private List<VARIANTS_VALUES> _lstVariantsValueses;
        private List<OPTIONS> _lsOptionses;
        private List<OPTIONS_VALUES> _lstOptionsValueses;
        private List<ProductDetail> _litSanPhamCuThes;

        public Service_formSP()
        {
            PS = new Generic_Sevice<PRODUCTS>();
            _lstProductses = new List<PRODUCTS>();

            //
            PV = new Generic_Sevice<PRODUCTS_VARIANTS>();
            _lstProductsVariantses = new List<PRODUCTS_VARIANTS>();

            //
            PO = new Generic_Sevice<PRODUCTS_OPTIONS>();
            _lsProductsOptionses = new List<PRODUCTS_OPTIONS>();

            //
            //
            Vv = new Generic_Sevice<VARIANTS_VALUES>();
            _lstVariantsValueses = new List<VARIANTS_VALUES>();

            //
            OS = new Generic_Sevice<OPTIONS>();
            _lsOptionses = new List<OPTIONS>();

            //
            OV = new Generic_Sevice<OPTIONS_VALUES>();
            _lstOptionsValueses = new List<OPTIONS_VALUES>();
            //
            loadAllList();
            //
            _litSanPhamCuThes = new List<ProductDetail>();
        }

        public List<string> ForeachOption(List<OPTIONS_VALUES> aOptionsValueses)
        {
            List<string> beu = new List<string>();
            foreach (var VARIABLE in aOptionsValueses)
            {
                beu.Add($"thuộc tính: {_lsOptionses.Where(c => c.id_Option == VARIABLE.id_Option).Select(c => c.option_Name)}: {VARIABLE.option_Values}");
            }

            return beu;
        }
        void loadAllList()
        {
            _lstProductses = PS.getList().Where(c => c.status_Delete == true).ToList();
            _lstOptionsValueses = OV.getList().Where(c => c.status_Delete == true).ToList();
            _lsOptionses = OS.getList().Where(c => c.status_Delete == true).ToList();
            _lstVariantsValueses = Vv.getList().Where(c => c.status_Delete == true).ToList();
            _lsProductsOptionses = PO.getList().Where(c => c.status_Delete == true).ToList();
            _lstProductsVariantses = PV.getList().Where(c => c.status_Delete == true).ToList();
        }

        void clearall()
        {
            _lsOptionses.Clear();
            _lsProductsOptionses.Clear();
            _lstOptionsValueses.Clear();
            _lstProductsVariantses.Clear();
            _lstProductses.Clear();
            _lstVariantsValueses.Clear();
        }


        public string addNewProduct(string name)
        {
            PRODUCTS prd = new PRODUCTS();
            prd.products_Name = name;
            prd.status_Delete = false;
            PS.Add(prd);
            return "Successful";
        }

        public List<ProductDetailTempplate> LoadDatafromDAL()
        {
            //_litSanPhamCuThes.Clear();
            //var lisdtOption = _lstVariantsValueses
            //    .Join(_lstOptionsValueses, vv => vv.id_Values, ov => ov.id_Values, (vv, ov) => new { vv, ov })
            //    .Join(_lsOptionses, i => i.ov.id_Option, o => o.id_Option, (i, o) => new { i.vv, i.ov, o })
            //    .Join(_lstProductses, i => i.vv.id_Product, p => p.id_Product, (i, p) => new { i.vv, i.ov, i.o, p })
            //    .Join(_lstProductsVariantses, i => i.vv.id_Variant, pv => pv.id_Variant,
            //        (i, pv) => new { i.vv, i.p, i.o, i.ov, pv })
            //    .ToList();
            //_lstProductsVariantses.ForEach(y =>
            //{
            //    var x = new ProductDetail();
            //    x.Product = _lstProductses.Where(x => x.id_Product == y.id_Product).FirstOrDefault();
            //    x.ProductVariant = y;
            //    lisdtOption.Where(i => i.vv.id_Variant == y.id_Variant).ToList().ForEach(z =>
            //    {
            //        x.VariantValue.Add(z.vv);
            //        x.Option.Add(z.o);
            //        x.OptionValue.Add(z.ov);
            //    });
            //    //trạng thái có phải là dữ liệu cũ có trong database hay không nếu cũ : true : false
            //    x.Status = true;
            //    _litSanPhamCuThes.Add(x);
            //});

            List<ProductDetailTempplate> _productDetailTempplates = new List<ProductDetailTempplate>();
            var lst = (from a in _lstVariantsValueses
                       group a by new
                       {
                           a.id_Product,
                           a.id_Variant
                       } into k
                       join b in _lstProductses on k.Key.id_Product equals b.id_Product
                       from c in _lstProductsVariantses.Where(c => c.id_Product == k.Key.id_Product && c.id_Variant == k.Key.id_Variant)
                       select new
                       {
                           idSanPham = k.Key.id_Product,
                           TenSP = b.products_Name,
                           skud = c.Products_Code,
                           gia = c.price,
                           _lisThuocTinh = (from vv in _lstVariantsValueses
                                            join op in _lsOptionses on vv.id_Option equals op.id_Option
                                            join ov in _lstOptionsValueses on vv.id_Values equals ov.id_Values
                                            where vv.id_Product == k.Key.id_Product && vv.id_Variant == k.Key.id_Variant
                                            select new
                                            {
                                                tenThuocTinh = op.option_Name,
                                                giaTriThuocTinh = ov.option_Values
                                            }),

                       }).ToList();

           

            foreach (var x in lst)
            {
                ProductDetailTempplate pdt = new ProductDetailTempplate();
                List<ThuocTinh> a = new List<ThuocTinh>();
                foreach (var x1 in x._lisThuocTinh)
                {
                    ThuocTinh acc = new ThuocTinh();
                    acc.Option = x1.tenThuocTinh;
                    acc.Value = x1.giaTriThuocTinh;
                    a.Add(acc);
                }

                pdt.Id = x.idSanPham;
                pdt.Name = x.TenSP;
                pdt.Skud = x.skud;
                pdt.Price = x.gia;
                pdt.ThuocTinhList = a;

                _productDetailTempplates.Add(pdt);
            }
            return _productDetailTempplates;

        }

        public List<string> ForeachOption(List<ThuocTinh> aOptionsValueses)
        {
            List<string> optionList = new List<string>();

            foreach (var x in aOptionsValueses)
            {
                optionList.Add($"{x.Option}:{x.Value}");
            }
            return optionList;
        }


        public string addProductDetail(ProductDetail productDetail)
        {
            try
            {
                PS.Add(productDetail.Product);
                PS.Save();
                _lstProductses = PS.getList();
                var idProduct = _lstProductses
                    .Where(x => x.products_Name.ToUpper() == productDetail.Product.products_Name.ToUpper())
                    .Select(o => o.id_Product)
                    .FirstOrDefault();

                productDetail.Option.ForEach(x =>
                {
                    var checkoption = _lsOptionses
                        .Where(z => z.option_Name.ToUpper() == x.option_Name.ToUpper())
                        .ToList();
                    if (checkoption.Count == 0)
                    {
                        var option = new OPTIONS();
                        option.option_Name = x.option_Name;
                        OS.Add(option);
                    }
                });
                OS.Save();
                _lsOptionses = OS.getList();
                productDetail.Option.ForEach(x =>
                {
                    var idOption = _lsOptionses
                        .Where(z => z.option_Name.ToUpper() == x.option_Name.ToUpper())
                        .Select(v => v.id_Option)
                        .FirstOrDefault();
                    var productOption = new PRODUCTS_OPTIONS();
                    productOption.id_Option = idOption;
                    productOption.id_Product = idProduct;
                    var statusAdd = PO.Add(productOption);
                });
                PO.Save();
                _lsProductsOptionses = PO.getList();
                return "Successful";
            }
            catch (System.Exception e)
            {
                return "Error addProductDetail : " + e;
            }
        }

        public string editProductDetail(ProductDetail productDetail)
        {
            try
            {
                var idProduct = productDetail.Product.id_Product;
                //-------------------------------------------------
                productDetail.Option.ForEach(x =>
                {
                    var checkoption = _lsOptionses
                        .Where(z => z.option_Name.ToUpper() == x.option_Name.ToUpper())
                        .ToList();
                    if (checkoption.Count == 0)
                    {
                        var option = new OPTIONS();
                        option.option_Name = x.option_Name;
                        OS.Add(option);
                    }
                });
                OS.Save();
                _lsOptionses = OS.getList();
                //-------------------------------------------------
                _lsProductsOptionses
                    .Where(x => x.id_Product == productDetail.Product.id_Product)
                    .ToList()
                    .ForEach(x =>
                    {
                        var variantValue = _lstVariantsValueses
                            .Where(v => v.id_Product == x.id_Product && v.id_Option == x.id_Option).ToList();
                        variantValue.ForEach(x =>
                        {
                            var statusdeleteVariantValue = Vv.Delete(x);
                        });
                        var statusDelate = PO.Delete(x);
                    });
                PO.Save();
                Vv.Save();
                _lsProductsOptionses = PO.getList();
                _lstVariantsValueses = Vv.getList();
                //-------------------------------------------------
                productDetail.Option.ForEach(x =>
                {
                    var idOption = _lsOptionses
                        .Where(v => v.option_Name.ToUpper() == x.option_Name.ToUpper())
                        .Select(x => x.id_Option)
                        .FirstOrDefault();
                    var checkConstrain = _lsProductsOptionses
                        .Where(v => v.id_Option == idOption && v.id_Product == idProduct)
                        .FirstOrDefault();
                    if (checkConstrain == null)
                    {
                        var productOption = new PRODUCTS_OPTIONS();
                        productOption.id_Option = idOption;
                        productOption.id_Product = idProduct;
                        var statusAdd = PO.Add(productOption);
                    }
                    else
                    {
                        checkConstrain.status_Delete = true;
                        PO.Edit(checkConstrain);
                        _lstVariantsValueses
                            .Where(x => x.id_Option == checkConstrain.id_Option
                                        && x.id_Product == checkConstrain.id_Product)
                            .ToList()
                            .ForEach(v =>
                            {
                                v.status_Delete = true;
                                Vv.Edit(v);
                            });
                    }
                });
                var statusSaveProductoption = PO.Save();
                var statusSaveDeletevariantValue = Vv.Save();
                _lsProductsOptionses = PO.getList();
                _lstVariantsValueses = Vv.getList();
                return "Successful";
            }
            catch (System.Exception e)
            {
                return "Error editProductDetail : " + e;
            }
        }

        public string removeProductDetail(ProductDetail productDetail)
        {
            try
            {
                var product = _lstProductses
                    .Where(x => x.products_Name.ToUpper() == productDetail.Product.products_Name.ToUpper())
                    .FirstOrDefault();
                PS.Delete(product);
                PS.Save();
                _lstProductses = PS.getList();
                //----------------------------------------------------------
                var lstProductOption = _lsProductsOptionses.Where(x => x.id_Product == product.id_Product).ToList();
                lstProductOption.ForEach(x =>
                {
                    PO.Delete(x);
                    var lstVariantValue = _lstVariantsValueses
                        .Where(v => v.id_Option == x.id_Option
                                    && v.id_Product == x.id_Product)
                        .ToList();
                    lstVariantValue.ForEach(b =>
                    {
                        var status = Vv.Delete(b);
                    });
                });
                var status = Vv.Save();
                _lstVariantsValueses = Vv.getList();
                var status2 = PO.Save();
                _lsProductsOptionses = PO.getList();
                //----------------------------------------------------------
                _lstProductsVariantses.Where(x => x.id_Product == product.id_Product)
                    .ToList()
                    .ForEach(x => { PV.Delete(x); });
                PV.Save();
                _lstProductsVariantses = PV.getList();
                //----------------------------------------------------------
                return "Successful";
            }
            catch (System.Exception e)
            {
                return "Error removeProductDetail : " + e;
            }
        }

        public string addProductVariant(ProductDetail productDetail)
        {
            try
            {
                PV.Add(productDetail.ProductVariant);
                PV.Save();
                _lstProductsVariantses = PV.getList();
                var idProductVariant = _lstProductsVariantses
                    .Where(xc => xc.Products_Code.ToUpper() == productDetail.ProductVariant.Products_Code.ToUpper())
                    .FirstOrDefault();
                //--------------------------------------------------------
                productDetail.OptionValue.ForEach(x =>
                {
                    var vValue = _lstOptionsValueses
                        .Where(m => m.option_Values.ToUpper() == x.option_Values.ToUpper()
                                    && m.id_Option == x.id_Option)
                        .ToList();
                    if (vValue.Count == 0)
                    {
                        var optionValue = new OPTIONS_VALUES();
                        optionValue.id_Option = x.id_Option;
                        optionValue.option_Values = x.option_Values;
                        var status1 = OV.Add(optionValue);
                    }

                    var status = OV.Save();
                    _lstOptionsValueses = OV.getList();
                });
                //--------------------------------------------------------
                productDetail.OptionValue.ForEach(x =>
                {
                    var value = _lstOptionsValueses
                        .Where(v => v.option_Values.ToUpper() == x.option_Values.ToUpper()
                                    && v.id_Option == x.id_Option)
                        .FirstOrDefault();
                    var varintValue = new VARIANTS_VALUES();
                    varintValue.id_Variant = idProductVariant.id_Variant;
                    varintValue.id_Product = idProductVariant.id_Product;
                    varintValue.id_Values = value.id_Values;
                    varintValue.id_Option = x.id_Option;
                    var status1 = Vv.Add(varintValue);
                });
                Vv.Save();
                _lstVariantsValueses = Vv.getList();
                return "Successful";
            }
            catch (System.Exception e)
            {
                return "Error addProductVariant : " + e;
            }
        }

        public string editProductVariant(ProductDetail productDetail)
        {
            try
            {
                var idVariantValue = productDetail.ProductVariant.id_Variant;
                var idPoduct = productDetail.ProductVariant.id_Product;
                PV.Edit(productDetail.ProductVariant);
                PV.Save();
                _lstProductsVariantses = PV.getList();
                //---------------------------------------------------
                productDetail.OptionValue.ForEach(x =>
                {
                    var vValue = _lstOptionsValueses
                        .Where(m => m.option_Values.ToUpper() == x.option_Values.ToUpper()
                                    && m.id_Option == x.id_Option)
                        .ToList();
                    if (vValue.Count == 0)
                    {
                        var optionValue = new OPTIONS_VALUES();
                        optionValue.id_Option = x.id_Option;
                        optionValue.option_Values = x.option_Values;
                        var status1 = OV.Add(optionValue);
                    }

                    var status = OV.Save();
                    _lstOptionsValueses = OV.getList();
                });
                //---------------------------------------------------
                _lstVariantsValueses
                    .Where(x => x.id_Variant == idVariantValue
                                && x.id_Product == idPoduct)
                    .ToList()
                    .ForEach(x => { Vv.Delete(x); });
                var status1 = Vv.Save();
                _lstVariantsValueses = Vv.getList();
                //---------------------------------------------------
                productDetail.OptionValue.ForEach(x =>
                {
                    var value = _lstOptionsValueses
                        .Where(v => v.option_Values.ToUpper() == x.option_Values.ToUpper()
                                    && v.id_Option == x.id_Option)
                        .FirstOrDefault();
                    var variantValue = _lstVariantsValueses.Where(
                        xc => xc.id_Option == value.id_Option
                              && xc.id_Values == value.id_Values
                              && xc.id_Variant == idVariantValue
                              && xc.id_Product == idPoduct
                    ).FirstOrDefault();
                    if (variantValue == null)
                    {
                        var vValue = new VARIANTS_VALUES();
                        vValue.id_Variant = idVariantValue;
                        vValue.id_Product = idPoduct;
                        vValue.id_Values = value.id_Values;
                        vValue.id_Option = x.id_Option;
                        var status1 = Vv.Add(vValue);
                    }
                    else
                    {
                        variantValue.status_Delete = true;
                        var status1 = Vv.Edit(variantValue);
                    }

                });
                var status = Vv.Save();
                _lstVariantsValueses = Vv.getList();
                //---------------------------------------------------
                return "Successful";
            }
            catch (System.Exception e)
            {
                return "Error editProductVariant : " + e;
            }
        }

        public string removeProductVariant(ProductDetail productDetail)
        {
            try
            {
                var idVariantValue = productDetail.ProductVariant.id_Variant;
                var idPoduct = productDetail.ProductVariant.id_Product;
                var productVariant = _lstProductsVariantses.Where(x => x.id_Variant == idVariantValue
                                                                       && x.id_Product == idPoduct).FirstOrDefault();
                //---------------------------------------------------------
                PV.Delete(productVariant);
                var status = PV.Save();
                _lstProductsVariantses = PV.getList();
                //---------------------------------------------------------
                var lstVariantValue = _lstVariantsValueses
                    .Where(x => x.id_Product == idPoduct
                                && x.id_Variant == idVariantValue)
                    .ToList();
                lstVariantValue
                    .ForEach(x => { Vv.Delete(x); });
                Vv.Save();
                _lstVariantsValueses = Vv.getList();
                //---------------------------------------------------------

                return "Successful";
            }
            catch (System.Exception e)
            {
                return "Error removeProductVariant : " + e;
            }
        }

        public string updateQuantity(ProductDetail productDetail)
        {
            try
            {
                var prv = new Generic_Sevice<PRODUCTS_VARIANTS>();
                var lst = prv.getList();
                var productVariant = lst
                    .Where(x => x.id_Product == productDetail.Product.id_Product
                                && x.id_Variant == productDetail.ProductVariant.id_Variant).FirstOrDefault();
                productVariant.quantity = productVariant.quantity - productDetail.ProductVariant.quantity;
                prv.Edit(productVariant);
                prv.Save();
                _lstProductsVariantses = prv.getList();
                return "Successful";
            }
            catch (System.Exception e)
            {
                return "Error updateQuantity : " + e;
            }
        }
    }
}
