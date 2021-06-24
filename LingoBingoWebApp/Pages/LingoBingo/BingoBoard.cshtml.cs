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
        private readonly int _boardsize = 24;
        private readonly ILogger<BingoBoardModel> _logger;
        private string _category { get; set; }
        private string _message;
        public LingoWordsContext LingoContext { get; set; }
        public IList<LingoWord> BingoBoardWords { get; set; }
        public List<LingoWord> Lingowords { get; set; }
        public BingoBoardModel(LingoWordsContext context, ILogger<BingoBoardModel> logger)
        {
            LingoContext = context;
            _logger = logger;
            Lingowords = new List<LingoWord>();
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            _message = "BingoBoard page OnGetAsync() called.";
            _logger.LogInformation(_message);

            if (id == null)
            {
                return NotFound();
            }

            Lingowords = await LingoContext.LingoWords.Where(lw => lw.LingoCategory.Id == id).Include(x => x.LingoCategory).ToListAsync();
            _category = Lingowords[0].LingoCategory.Category;

            await CreateBingoBoard();
            return Page();
        }

        private async Task CreateBingoBoard()
        {
            _message = "BingoBoard page RandomizeList() called.";
            _logger.LogInformation(_message);

            Task<List<LingoWord>> task = new Task<List<LingoWord>>(() =>
           {
               LingoWord[] wordlist = Lingowords.ToArray();
               double[] order = new double[wordlist.Length];
               var rand = new Random();

               for (int counter = 0; counter < wordlist.Length; counter++)
               {
                   order[counter] = rand.NextDouble();
               }

               Array.Sort(order, wordlist);
               List<LingoWord> tempList = wordlist.Take(_boardsize).ToList();
               tempList.Insert(12, new LingoWord { Word = "FREE", LingoCategory = new LingoCategory { Category = _category } });
               return tempList;
           });

            task.Start();
            BingoBoardWords = await task;
        }
    }
}
