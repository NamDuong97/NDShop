using System.Net.Cache;
using AspNetCoreGeneratedDocument;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NDShop_Ecomerce.Data;
using NDShop_Ecomerce.ViewModels;

namespace NDShop_Ecomerce.Controllers
{
    public class HangHoasController : Controller
    {
        private readonly Hshop2023Context _context;
        public HangHoasController(Hshop2023Context context)
        {
            _context = context;
        }
        public IActionResult Index(int? loai)
        {
            var hanghoas = _context.HangHoas.AsQueryable();
            if (loai.HasValue)
            {
                hanghoas = hanghoas.Where(p => p.MaLoai == loai);
            }
            var result = hanghoas.Select(i => new HangHoaVM
            {
                MaHangHoa = i.MaHh,
                TenHH = i.TenHh,
                Hinh = i.Hinh ?? "",
                DonGia = i.DonGia ?? 0,
                MoTanNgan = i.MoTaDonVi ?? "",
                TenLoai = i.MaLoaiNavigation.TenLoai,
            }).ToList();
            return View(result);
        }

        public IActionResult Search(string query)
        {
            var hanghoas = _context.HangHoas.AsQueryable();
            if (query != null)
            {
                hanghoas = hanghoas.Where(p => p.TenHh.Contains(query));
            }
            var result = hanghoas.Select(i => new HangHoaVM
            {
                MaHangHoa = i.MaHh,
                TenHH = i.TenHh,
                Hinh = i.Hinh ?? "",
                DonGia = i.DonGia ?? 0,
                MoTanNgan = i.MoTaDonVi ?? "",
                TenLoai = i.MaLoaiNavigation.TenLoai,
            }).ToList();
            return View(result);
        }

        public IActionResult Detail(int id)
        {
            var hanghoa = _context.HangHoas.Include(P => P.MaLoaiNavigation).FirstOrDefault(p => p.MaHh == id);
            if (hanghoa == null)
            {
                TempData["Message"] = $"Khong thay san pham co ma id: {id}";
                return Redirect("/404");
            }
            ChiTietHangHoa result =  new ChiTietHangHoa
            {
                MaHangHoa = hanghoa.MaHh,
                TenHH = hanghoa.TenHh,
                Hinh = hanghoa.Hinh ?? "",
                DonGia = hanghoa.DonGia ?? 0,
                MoTanNgan = hanghoa.MoTaDonVi ?? "",
                TenLoai = hanghoa.MaLoaiNavigation.TenLoai,
                ChiTiet = hanghoa.MoTa ?? "",
                SoLuongTon = 10,
                DiemDanhGia = 5,
            };
            return View(result);
        }
    }
}
