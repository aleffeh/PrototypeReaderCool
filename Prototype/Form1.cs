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

namespace Prototype
{

    public partial class Form1 : Form
    {
        public bool IsActive { get; set; }
        public string Txt { get; set; }
        public int Iterator { get; set; } = 0;
        public int Delay = 200;
        private List<string> sub;

        public Form1()
        {

            InitializeComponent();
            Txt = "História é a ciência que estuda o ser humano e sua ação no tempo e no espaço concomitantemente à análise de processos e eventos ocorridos no passado. O termo \"História\" também pode significar toda a informação do passado arquivada em todas as línguas por todo o mundo, por intermédio de registos históricos";
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
                if (Iterator > 0 )
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

            if (e.KeyCode == Keys.Up)
            {
                Delay += 20;
            }
            if (e.KeyCode == Keys.Up)
            {
                Delay -= 20;
            }
            if (e.KeyCode == Keys.Return)
            {
                Delay = 200;
            }
            if (e.KeyCode == Keys.Back)
            {
                IsActive = false;
                Iterator = 0;
                await Task.Delay(1000);
            }
        }
    }
}
