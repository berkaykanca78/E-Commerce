using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Abc.MvcWebUI.Entity
{
    public class Product
    {
        public int Id { get; set; }

        [DisplayName("Ürün Adı")]
        public string Name { get; set; }
        [DisplayName("Ürün Açıklaması")]
        public string Description { get; set; }
        [DisplayName("Fiyatı")]
        public double Price { get; set; }
        [DisplayName("Stok Adeti")]
        public int Stock { get; set; }
        [DisplayName("Resim")]
        public string Image { get; set; }
        [DisplayName("Anasayfa")]
        public bool IsHome { get; set; }
        [DisplayName("Onay")]
        public bool IsApproved { get; set; }

        //Foreign Key
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}