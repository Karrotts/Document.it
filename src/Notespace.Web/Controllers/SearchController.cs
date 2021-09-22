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
    public class SearchController : Controller
    {
        private readonly ApplicationIdentityContext _context;
        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _signInManager;

        public SearchController(ApplicationIdentityContext context,
               UserManager<ApplicationUser> userMgr,
               SignInManager<ApplicationUser> signInMgr)
        {
            _userManager = userMgr;
            _signInManager = signInMgr;
            _context = context;
        }

        [Authorize]
        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public ActionResult Results(string search)
        {
            if (!String.IsNullOrEmpty(search))
            {
                var notes = _context.Notes.Include(n => n.Notebook).Include(n => n.User);
                var notebooks = _context.Notebooks.Include(n => n.User);

                var noteResults = notes.Where(n => n.Title.Contains(search) && n.Notebook == null);
                var notebookResults = notebooks.Where(n => n.Title.Contains(search));

                List<SearchResults> results = new List<SearchResults>();

                foreach (Note note in noteResults)
                {
                    SearchResults sr = new SearchResults();
                    sr.Id = note.NoteID;
                    sr.Title = note.Title;
                    sr.Type = "Notes";
                    sr.LastModified = note.LastModified;
                    sr.Creator = note.User.UserName;
                    results.Add(sr);
                }

                foreach (Notebook notebook in notebookResults)
                {
                    SearchResults sr = new SearchResults();
                    sr.Id = notebook.NotebookID;
                    sr.Title = notebook.Title;
                    sr.Type = "Notebooks";
                    sr.LastModified = notebook.LastModified;
                    sr.Creator = notebook.User.UserName;
                    results.Add(sr);
                }

                return View(results);
            }

            return View(new List<SearchResults>());
        }
    }
}
