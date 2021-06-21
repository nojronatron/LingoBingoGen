using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using LingoBingoLibrary.DataAccess;
using LingoBingoLibrary.Collections;
using Microsoft.Extensions.Logging;

namespace LingoBingoWebApp.Pages.LingoBingo
{
    public class WelcomeModel : PageModel
    {
        private readonly ILogger<WelcomeModel> _logger;
        private string _message;
        private LingoWordsCollection _lingoWordsCollection;
        private readonly LingoWordsContext _context;
        public IList<string> Categories { get; set; }
        public IList<LingoCategory> Lingocategories { get; set; }
        public WelcomeModel(LingoWordsContext context, ILogger<WelcomeModel> logger)
        {
            _context = context;
            _logger = logger;
            Lingocategories = new List<LingoCategory>();
        }

        public async Task OnGetAsync()
        {
            _message = "Welcome page OnGetAsync() called.";
            _logger.LogInformation(_message);

            await Task.Run(() =>
            {
                if (_context.LingoCategories.Any())
                {
                    Lingocategories = _context.LingoCategories
                                              .ToList();
                }
            });
        }
    }
}
