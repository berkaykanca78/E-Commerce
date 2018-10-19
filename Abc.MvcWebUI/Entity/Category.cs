using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Abc.MvcWebUI.Entity
{
    public class Category
    {
        public int Id { get; set; }

        //Data Annotations
        [DisplayName("Kategori Adı")]
        [StringLength(maximumLength:20,ErrorMessage ="En Fazla 20 Karakter Girebilirsiniz.")]
        public string Name { get; set; }

        [DisplayName("Açıklama")]
        public string Description { get; set; }

        //1-N Relationship
        public List<Product> Products { get; set; }
    }
}