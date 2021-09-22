using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Notespace.Web.Data;
using Notespace.Web.Models;


namespace Notespace.Web.Controllers
{
    public class ReportsController : Controller
    {
        private readonly ApplicationIdentityContext _context;
        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _signInManager;

        public ReportsController(ApplicationIdentityContext context,
               UserManager<ApplicationUser> userMgr,
               SignInManager<ApplicationUser> signInMgr)
        {
            _userManager = userMgr;
            _signInManager = signInMgr;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Results(string username, string title, string type, DateTime lm_after, DateTime lm_before, string sort, string reportTitle = "New Report")
        {
            ViewBag.Title = reportTitle;
            List<ReportResults> results = new List<ReportResults>();
            var notes = _context.Notes.Include(n => n.Notebook).Include(n => n.User);
            var notebooks = _context.Notebooks.Include(n => n.User);

            if (type == "notes" || type == "both")
            {
                var filtered = notes.Where(n => n.LastModified > lm_after && n.LastModified < lm_before)
                                    .Where(n => n.NotebookID == null);

                if (!String.IsNullOrEmpty(username) && !String.IsNullOrWhiteSpace(username))
                {
                    filtered = filtered.Where(n => n.User.UserName.Contains(username));
                }

                if (!String.IsNullOrEmpty(title) && !String.IsNullOrWhiteSpace(title))
                {
                    filtered = filtered.Where(n => n.Title.Contains(title));
                }

                foreach (Note note in filtered)
                {
                    ReportResults rr = new ReportResults();
                    rr.Username = note.User.UserName;
                    rr.Title = note.Title;
                    rr.LastModified = note.LastModified;
                    rr.Type = "Note";
                    results.Add(rr);
                }
            }

            if (type == "notebooks" || type == "both")
            {
                var filtered = notebooks.Where(n => n.LastModified > lm_after && n.LastModified < lm_before);

                if (!String.IsNullOrEmpty(username) && !String.IsNullOrWhiteSpace(username))
                {
                    filtered = filtered.Where(n => n.User.UserName.Contains(username));
                }

                if (!String.IsNullOrEmpty(title) && !String.IsNullOrWhiteSpace(title))
                {
                    filtered = filtered.Where(n => n.Title.Contains(title));
                }

                foreach (Notebook note in filtered)
                {
                    ReportResults rr = new ReportResults();
                    rr.Username = note.User.UserName;
                    rr.Title = note.Title;
                    rr.LastModified = note.LastModified;
                    rr.Type = "Notebook";
                    results.Add(rr);
                }
            }

            switch(sort)
            {
                case "username":
                    results = results.OrderBy(r => r.Username).ToList();
                    break;
                case "title":
                    results = results.OrderBy(r => r.Title).ToList();
                    break;
                case "modified":
                    results = results.OrderBy(r => r.LastModified).ToList();
                    break;
                case "type":
                    results = results.OrderBy(r => r.Type).ToList();
                    break;
            }

            return View(results);
        }
    }
}
