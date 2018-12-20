using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Week02.Models
{
    public class AdminViewModel
    {
    }
    public class SaveEditModel
    {
        [Required]
        public int ID_sp { get; set; }
        [Required]
        public string Ten_sp { get; set; }
        [Required]
        public int Gia_sp { get; set; }
        [Required]
        public string Hinh_sp { get; set; }
        [Required]
        public string Mo_ta { get; set; }
        [Required]
        public int ID_nhom { get; set; }
        [Required]
        public int So_luong { get; set; }
        [Required]
        public string Nsx_sp { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual Nhom_sp Nhom_sp { get; set; }
    }
}