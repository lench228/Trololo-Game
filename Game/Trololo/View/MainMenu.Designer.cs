using System.Drawing;
using System.Windows.Forms;
using System.Drawing; 

namespace Trololo.View
{
    partial class MainMenu
    {
        /// <summary> 
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором компонентов

        /// <summary> 
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.BackgroundImage = new Bitmap(Image.FromFile($"C:\\Users\\wrwsc\\Desktop\\Trololo-Game\\Game\\Trololo\\View\\Source\\photo_2023-04-12_17-12-07.jpg"));
            this.Name = "Trololo";
            this.Size = BackgroundImage.Size;
            Button but = AddButtons("Начать игру",new Point(200,400));
            Button but2 = AddButtons("Загрузить игру", new Point(200, 500));
            Button but3 = AddButtons("Выйти из игры", new Point(200, 600));

        }

        private static Button AddButtons(string text, Point location)
        {
            var but = new Button();
            but.Text = text;
            but.Location = location;
            but.Width= 200;
            return but;
        }


        #endregion
    }
}
