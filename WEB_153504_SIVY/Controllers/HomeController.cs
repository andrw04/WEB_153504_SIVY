using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WEB_153504_SIVY.Controllers
{
    public class ListDemo
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.List = new List<string>()
            {
                "Элемент 1 списка",
                "Элемент 2 списка",
                "Элемент 4 списка",
                "Элемент 5 списка",
                "Элемент 6 списка",
                "Элемент 7 списка",
            };

            ViewData["header1"] = "Лабораторная работа №2";

            var items = new List<ListDemo>
            {
                new ListDemo { Id = 1, Name = "Item 1"},
                new ListDemo { Id = 2, Name = "Item 2"},
                new ListDemo { Id = 3, Name = "Item 3"},
            };
            var selectItems = new SelectList(items, "Id", "Name"); 

            return View(selectItems);
        }
    }
}
