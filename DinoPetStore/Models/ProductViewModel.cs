using DinoPetStore.EF;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DinoPetStore.Models
{
    public class ProductViewModel
    {
        DinoStoreDbContext data = new DinoStoreDbContext();
        public int MASP { get; set; }
        public string TENSP { get; set; }
        public decimal? DONGIABAN { get; set; }
        public string HINHANH { get; set; }
        public int MATH { get; set; }
        public int MALOAI { get; set; }
        public String TENTH { get; set; }
        public String TENLOAI { get; set; }
        public int SOLUONG { get; set; }
        public string MOTA { get; set; }
        public String TENMAUSAC { get; set; }
        public int TENKICHTHUOC { get; set; }
        public string HINH1 { get; set; }
        public string LOGO { get; set; }
        public string THANHTOANON { get; set; }


        public int GIAMGIA { get; set; }

    }
}