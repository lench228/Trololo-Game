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

namespace Trololo.View
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent(); 
            var mainMenu = new MainMenu();
            Controls.Add(mainMenu);
        }   

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // MainForm
            // 
            this.ClientSize = new System.Drawing.Size(1280, 910);

            this.Name = "MainForm";
            this.ResumeLayout(false);
        }

    }
}
