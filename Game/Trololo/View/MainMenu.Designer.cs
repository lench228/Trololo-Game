using System.Drawing;
using System.Windows.Forms;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainMenu));
            this.start = new System.Windows.Forms.Label();
            this.load = new System.Windows.Forms.Label();
            this.exit = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // start
            // 
            this.start.Location = new System.Drawing.Point(0, 0);
            this.start.Name = "start";
            this.start.Size = new System.Drawing.Size(100, 23);
            this.start.TabIndex = 0;
            this.start.Text = "Начать";
            // 
            // load
            // 
            this.load.Location = new System.Drawing.Point(0, 0);
            this.load.Name = "load";
            this.load.Size = new System.Drawing.Size(100, 23);
            this.load.TabIndex = 0;
            this.load.Text = "Загрузить";
            // 
            // exit
            // 
            this.exit.Location = new System.Drawing.Point(0, 0);
            this.exit.Name = "exit";
            this.exit.Size = new System.Drawing.Size(100, 23);
            this.exit.TabIndex = 0;
            this.exit.Text = "Выйти";
            // 
            // MainMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.Name = "MainMenu";
            this.Size = new System.Drawing.Size(1397, 980);
            this.ResumeLayout(false);

        }

        #endregion

        private Label start;
        private Label load;
        private Label exit;
    }
}
