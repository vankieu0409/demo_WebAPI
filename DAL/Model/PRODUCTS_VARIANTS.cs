using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL
{
    public class PRODUCTS_VARIANTS
    {
        public int id_Product { get; set; } 
        public int id_Variant { get; set; } 
        public string Products_Code { get; set; }
        [Column(TypeName = "money")]
        public int import_Price { get; set; }
        [Column(TypeName = "money")]
        public int price { get; set; }
        public int quantity { get; set; }
        public bool status_Delete { get; set; }
        public PRODUCTS Products{ get; set; }
        public ICollection<VARIANTS_VALUES> VariantValues{ get; set; }

    }
}