using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Trololo.View
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            var control = new ControlForm();
            control.BackColor = Color.Black;
            Controls.Add(control);

        }
    }
}
