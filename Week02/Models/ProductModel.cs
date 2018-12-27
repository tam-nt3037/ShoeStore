using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Week02.Models
{
    public class ProductModel
    {
        
        [Required]
        [DisplayName("Product ID")]
        public int ID_sp { get; set; }
        [DisplayName("Product Name")]
        public string Ten_sp { get; set; }
        [DisplayName("Manufacture")]
        public string Nsx_sp { get; set; }
        [DisplayName("Price")]
        public int Gia_sp { get; set; }
        [DisplayName("Picture")]
        public string Hinh_sp { get; set; }
        [DisplayName("Description")]
        public string Mo_ta { get; set; }
        [DisplayName("Group Type")]
        public int ID_nhom { get; set; }
        [DisplayName("Quantity")]
        public int So_luong { get; set; }

        public List<Nhom_sp> ProductGroup { get; set; }
        public List<Nhan_hieu> Brands { get; set; }
    }
}