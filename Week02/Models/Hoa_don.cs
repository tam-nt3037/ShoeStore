//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Week02.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Hoa_don
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Hoa_don()
        {
            this.CT_HoaDon = new HashSet<CT_HoaDon>();
        }
    
        public int Ma_hd { get; set; }
        public int ID_kh { get; set; }
        public Nullable<System.DateTime> Ngay_lap { get; set; }
        public Nullable<int> Tong_tien { get; set; }
        public string Trang_thai { get; set; }
        public Nullable<int> Tien_ship { get; set; }
        public string Trangthai_xulyhd { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CT_HoaDon> CT_HoaDon { get; set; }
        public virtual Khach_hang Khach_hang { get; set; }
    }
}
