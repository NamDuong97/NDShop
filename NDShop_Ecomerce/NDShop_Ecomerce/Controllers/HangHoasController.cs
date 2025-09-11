using AspNetCoreGeneratedDocument;
using Microsoft.AspNetCore.Mvc;
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
    }
}
