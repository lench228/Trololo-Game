using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using Trololo.Domain; 

namespace Trololo.View
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent(); 
            var game = new Game();
            var mainMenu = new MainMenu();
            Controls.Add(mainMenu);
            Controls.Add(new GameControl()); 
        }   

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // MainForm
            // 
            this.ClientSize = new System.Drawing.Size(1397, 980);

            this.Name = "MainForm";
            this.ResumeLayout(false);
        }

    }
}
