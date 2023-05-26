using System.Runtime.CompilerServices;

namespace Trololo.View
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.mainMenu = new Trololo.View.MainMenu();
            this.gameControl = new Trololo.View.GameControl();
            this.looseControl = new Trololo.View.LooseControl(); 
            this.pauseControl = new Trololo.View.PauseControl();

            this.ClientSize = new System.Drawing.Size(1300, 980);
            this.Controls.Add(this.gameControl);
            this.Controls.Add(this.mainMenu);
            this.Controls.Add(this.looseControl);
            this.Controls.Add(this.pauseControl); 
            KeyPreview = true;
        }

        #endregion

        private MainMenu mainMenu;
        private GameControl gameControl;
        private LooseControl looseControl;
        private PauseControl pauseControl; 
    }
}