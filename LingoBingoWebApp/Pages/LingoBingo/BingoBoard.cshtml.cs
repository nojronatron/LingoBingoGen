using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LingoBingoLibrary.Collections;
using LingoBingoLibrary.DataAccess;
using LingoBingoWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace LingoBingoWebApp.Pages.LingoBingo
{
    public class BingoBoardModel : PageModel
    {
        private readonly ILogger<BingoBoardModel> _logger;
        private string _message;
        public LingoWordsContext LingoContext { get; set; }

        public IList<string> BingoBoardWords { get; set;}
        public List<LingoWord> Lingowords { get; set; }
        public LingoWordsCollection LingoWordsCollection { get; set; }
        public BingoBoardModel(LingoWordsContext context, ILogger<BingoBoardModel> logger)
        {
            LingoContext = context;
            Lingowords = new List<LingoWord>();
            _logger = logger;
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            _message = "BingoBoard page OnGetAsync() called.";
            _logger.LogInformation(_message);

            if (id == null)
            {
                return NotFound();
            }

            string category = "csharp";
            Lingowords = await LingoContext.LingoWords.Where(lw => lw.LingoCategory.Id == id).Include(x => x.LingoCategory).ToListAsync();
            await StuffLWCollection();
            await BuildBingoBoard(category);
            return Page();
        }

        private async Task BuildBingoBoard(string category)
        {
            _message = "BingoBoard page BuildBingoBoard() called.";
            _logger.LogInformation(_message);

            Task<IList<string>> task = new Task<IList<string>>(
                () =>
                {
                    return LingoWordsCollection.GetNewBingoBoard(category);
                });

            task.Start();
            BingoBoardWords = await task;
        }

        private async Task StuffLWCollection()
        {
            _message = "BingoBoard page StuffLWCollection() called.";
            _logger.LogInformation(_message);

            Task<LingoWordsCollection> task = new Task<LingoWordsCollection>(
                () =>
                {
                    return new LingoWordsCollection(Lingowords);
                });

            task.Start();
            LingoWordsCollection = await task;
        }
    }
}
