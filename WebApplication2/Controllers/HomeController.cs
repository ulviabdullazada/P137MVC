using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using WebApplication2.Helpers;
using WebApplication2.Models;
using WebApplication2.Services.Implements;
using WebApplication2.Services.Interfaces;

namespace WebApplication2.Controllers
{
    public class HomeController:Controller
    {
        //public string Index(string name, string surname)
        //{
        //    return name + ' ' + surname;
        //}
        //public int About(int id)
        //{
        //    return id;
        //}
        //public IActionResult Index(int? id)
        //{
        //    JsonResult jr = new JsonResult(new { Name = "Adil", Surname = "Tehranli" });
        //    ContentResult cr = new ContentResult();
        //    cr.Content = "Salam";
        //    ViewResult vr = new ViewResult();
        //    if (id > 0)
        //    {
        //        return vr;
        //    }
        //    else if(id==0)
        //    {
        //        return cr;
        //    }
        //    return jr;
        //    //return cr;
        //    //return new() { "asd":"asd" };
        //}
        //public IActionResult Index()
        //{
        //    //ViewBag.Title = "Adil";
        //    //ViewData["Title"] = "Samid";
        //    string salam = "Said";
        //    return View((object)salam);
        //    //return View("./Views/Home/Index.cshtml");
        //}
        //public IActionResult Details()
        //{
        //    ViewData["Title"] = "Samid";
        //    TempData["Word"] = "Samid";
        //    return RedirectToAction(nameof(Index));
        //}


        public async Task<IActionResult> Index()
        {
            List<Music> musics = new List<Music>();
            DataTable dt = await SqlHelper.SelectAsync("Select * from Musics");
            foreach (DataRow item in dt.Rows)
            {
                musics.Add(new()
                {
                    Id = (int)item[0],
                    Name = (string)item[1],
                    Duration = Convert.ToInt32(item[2])
                });
            }
            return View(musics);
        }
        [HttpPost]
        public async Task<IActionResult> Music(string name, int duration)
        {
            IMusicService service = new MusicService();
            await service.AddAsync(new Music
            {
                Duration = duration,
                Name = name
            });
            return RedirectToAction(nameof(MusicGetAll));
        }
        public async Task<IActionResult> MusicGetAll()
        {
            IMusicService service = new MusicService();
            return Json(await service.GetAllAsync());
        }
        public async Task<IActionResult> MusicGetById(int id)
        {
            IMusicService service = new MusicService();
            return Json(await service.GetByIdAsync(id));
        }
        public async Task<IActionResult> MusicDelete(int id)
        {
            IMusicService service = new MusicService();
            if (await service.Delete(id) > 0)
            {
                return Ok();
            }
            return NotFound();
        }
    }
}
