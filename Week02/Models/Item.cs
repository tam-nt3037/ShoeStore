using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Week02.Models;
namespace Week02.Controllers
{
    [Serializable]
    public class Item
    {
        private San_pham san_pham = new San_pham();
        private int so_luong;

        public Item() { }

        public Item(San_pham san_pham, int so_luong )
        {
            this.san_pham = san_pham;
            this.so_luong = so_luong;
        }

        public San_pham San_pham { get => san_pham; set => san_pham = value; }
        public int So_luong { get => so_luong; set => so_luong = value; }
    }
}