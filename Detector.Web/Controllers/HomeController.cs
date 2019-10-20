using System.Linq;
using Detector.Database.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Detector.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly DbContext _context;

        public HomeController(DbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var data = _context.Set<IntrudeRecordEntity>().OrderByDescending(x => x.Id).Take(10);

            ViewBag.MaxId = data.Count() > 0 ? data.First().Id : 0;

            return View(data);
        }

        public IActionResult GetRecord(int maxId)
        {
            var data = _context.Set<IntrudeRecordEntity>().Where(x => x.Id > maxId);

            return Json(data);
        }

        public IActionResult InsertRecord()
        {
            return Redirect("https://detectorfunction20191015105102.azurewebsites.net/api/SyncIntrudeRecord?code=BGy3Ca1nfYcgrsH3Tb801zLT2MM/SVWQjhhg2QnVPAy/sGjaVDi22w==");
        }

        public IActionResult Error(string errorMessage)
        {
            ViewBag.ErrorMessage = errorMessage;

            return View();
        }
    }
}
