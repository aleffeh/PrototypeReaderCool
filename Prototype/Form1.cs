using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Media;

namespace Prototype
{

    public partial class Form1 : Form
    {
        public bool IsActive { get; set; }
        public string Txt { get; set; }
        public int Iterator { get; set; } = 0;
        public int Delay = 170;
        private List<string> sub;
        private int dot = 500;
        private int comma = 200;


        public Form1()
        {

            try
            {   // Open the text file using a stream reader.
                using (StreamReader sr = new StreamReader("text.txt", Encoding.Default))
                {
                    // Read the stream to a string, and write the string to the console.
                    var tt = sr.ReadToEnd();
                    this.Txt = tt.Replace("\n", " ");
                }
            }
            catch { }

            InitializeComponent();

            BackColor = Color.FromArgb(45, 45, 45);
            lblTxt.TextAlign = ContentAlignment.BottomCenter;
            IsActive = true;
            Start();
        }

        private async void Start()
        {
            sub = Txt.Split(' ').ToList();
            //lblTxt.Text = sub[26];

            for (int i = Iterator; i < sub.Count; i++)
            {
                if (IsActive)
                {

                    lblTxt.Text = sub[i];
                    await Task.Delay(Delay);
                    Iterator++;

                    if (sub[i].Contains(","))
                    {
                        await Task.Delay(comma);
                    }
                    if (sub[i].Contains(".") || sub[i].Contains("-") || sub[i].Contains("!") || sub[i].Contains("?") || sub[i].Contains(":"))
                    {
                        await Task.Delay(dot);
                    }
                }
                else
                {
                    lblTxt.Text = sub[Iterator];
                }
            }

        }

        private async void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                this.Close();

            if (e.KeyCode == Keys.Space)
            {
                IsActive = IsActive ? false : true;

                Start();
            }
            if (e.KeyCode == Keys.Left)
            {
                IsActive = false;
                if (Iterator > 0)
                {
                    Iterator--;
                    lblTxt.Text = sub[Iterator];
                }
            }
            if (e.KeyCode == Keys.Right)
            {
                IsActive = false;
                if (Iterator < sub.Count)
                {
                    Iterator++;
                    lblTxt.Text = sub[Iterator];
                }
            }


            if (e.KeyCode == Keys.Back)
            {
                IsActive = false;
                Iterator = 0;
                await Task.Delay(1000);
                IsActive = true;
                Start();
            }
        }
    }
}
