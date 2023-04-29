using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Trololo.Domain;

namespace Trololo.View
{
    public partial class MainForm : Form
    {
        private Game game;
        public MainForm()
        {
            InitializeComponent();
            this.Size = new Size(1397, 980);
            game = new Game();
            game.StageChanged += OnStageChanged;
            ShowMenu();
        }

        private void OnStageChanged(GameStage stage)
        {
            switch (stage)
            {
                case GameStage.Menu:
                ShowMenu();
                break;
                case GameStage.Play:
                ShowGameControl();
                break;
                //case GameStage.Finished:
                //ShowPause();
                //break;
                //case GameStage.NotStarted:
                //default:
                //ShowDeath();
                //break;
            }

        }

        private void HideScreens()
        {
            mainMenu.Hide();
            gameControl.Hide();
        }

        private void ShowGameControl()
        {
            HideScreens();
            gameControl.Run(game);
            ActiveControl = gameControl; 
            gameControl.Show(); 
        }

        private void ShowMenu()
        {
            HideScreens();
            mainMenu.Run(game);
            mainMenu.Show();
        }



        //private void ShowPause()
        //{
        //    HideScreens();
        //    battleControl.Configure(game);
        //    battleControl.Show();
        //}

        //private void ShowDeath()
        //{
        //    HideScreens();
        //    finishedControl.Configure(game);
        //    finishedControl.Show();
        //}
    }
}
