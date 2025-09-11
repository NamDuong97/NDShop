

using System.Drawing.Printing;
using Microsoft.AspNetCore.Mvc;
using NDShop_Ecomerce.Data;
using NDShop_Ecomerce.ViewModels;

namespace NDShop_Ecomerce.ViewComponents
{
    public class MenuLoaiViewComponent : ViewComponent
    {
        private readonly Hshop2023Context _context;
        public MenuLoaiViewComponent(Hshop2023Context context) {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            var data = _context.Loais.Select(lo => new MenuLoaiVM
            {
                MaLoai = lo.MaLoai, 
                TenLoai = lo.TenLoai, 
                SoLuong = lo.HangHoas.Count
            }).OrderBy(P => P.TenLoai);
            return View(data);  // mac dinh tim file Default.cshtml, khong thi tu dat ten
        }
    }
}
