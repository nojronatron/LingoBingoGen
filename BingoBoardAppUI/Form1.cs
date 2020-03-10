using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using LingoBingoGen;

namespace BingoBoardAppUI
{
    public partial class Form1 : Form
    {
        // create the cancellation token source
        static CancellationTokenSource tokenSource = new CancellationTokenSource();
        // create the cancellation token
        static CancellationToken token = tokenSource.Token;
        //internal static async Task<BingoBoard> GenerateBoard()
        //{
        //    //BingoBoard
        //}

        public Form1()
        {
            InitializeComponent();
        }

        private void btnQuit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label9_Click(object sender, EventArgs e)
        {
            //  remove me
        }

        private void label7_Click(object sender, EventArgs e)
        {
            //  remove me
        }

        private void labelBox1_Click(object sender, EventArgs e)
        {
            if (labelBox1.Image == null)
            {
                labelBox1.Image = BingoBoardAppUI.Properties.Resources.Turquoise_Dauber;
            }
        }

        private void labelBox2_Click(object sender, EventArgs e)
        {
            if (labelBox2.Image == null)
            {
                labelBox2.Image = BingoBoardAppUI.Properties.Resources.Turquoise_Dauber;
            }
        }

        private void labelBox3_Click(object sender, EventArgs e)
        {
            if (labelBox3.Image == null)
            {
                labelBox3.Image = BingoBoardAppUI.Properties.Resources.Turquoise_Dauber;
            }
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            //  TODO: Print page
        }
    }
}
