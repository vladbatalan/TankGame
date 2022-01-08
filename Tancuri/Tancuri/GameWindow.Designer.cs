namespace Tancuri
{
    partial class GameWindow
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
            this.components = new System.ComponentModel.Container();
            this.labelDebug = new System.Windows.Forms.Label();
            this.timerFrame = new System.Windows.Forms.Timer(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.labelPalyer1Ammo = new System.Windows.Forms.Label();
            this.labelPlayer1Health = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.labelPlayer2Ammo = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.labelPlayer2Health = new System.Windows.Forms.Label();
            this.labelDebug2 = new System.Windows.Forms.Label();
            this.buttonRestart = new System.Windows.Forms.Label();
            this.timerReload = new System.Windows.Forms.Timer(this.components);
            this.canvas = new Tancuri.MyCanvas();
            this.labelWinMessage = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.canvas.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelDebug
            // 
            this.labelDebug.AutoSize = true;
            this.labelDebug.Location = new System.Drawing.Point(190, 13);
            this.labelDebug.Name = "labelDebug";
            this.labelDebug.Size = new System.Drawing.Size(0, 13);
            this.labelDebug.TabIndex = 1;
            // 
            // timerFrame
            // 
            this.timerFrame.Interval = 33;
            this.timerFrame.Tick += new System.EventHandler(this.timerFrame_Tick);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.labelPalyer1Ammo);
            this.groupBox1.Controls.Add(this.labelPlayer1Health);
            this.groupBox1.ForeColor = System.Drawing.Color.Blue;
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(263, 53);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Player1";
            // 
            // labelPalyer1Ammo
            // 
            this.labelPalyer1Ammo.AutoSize = true;
            this.labelPalyer1Ammo.Location = new System.Drawing.Point(134, 20);
            this.labelPalyer1Ammo.Name = "labelPalyer1Ammo";
            this.labelPalyer1Ammo.Size = new System.Drawing.Size(64, 26);
            this.labelPalyer1Ammo.TabIndex = 1;
            this.labelPalyer1Ammo.Text = "Ammunition:\r\n10";
            // 
            // labelPlayer1Health
            // 
            this.labelPlayer1Health.AutoSize = true;
            this.labelPlayer1Health.Location = new System.Drawing.Point(7, 20);
            this.labelPlayer1Health.Name = "labelPlayer1Health";
            this.labelPlayer1Health.Size = new System.Drawing.Size(41, 26);
            this.labelPlayer1Health.TabIndex = 0;
            this.labelPlayer1Health.Text = "Health:\r\n100";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.labelPlayer2Ammo);
            this.groupBox2.Controls.Add(this.groupBox3);
            this.groupBox2.Controls.Add(this.labelPlayer2Health);
            this.groupBox2.ForeColor = System.Drawing.Color.Red;
            this.groupBox2.Location = new System.Drawing.Point(285, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(275, 53);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Player2";
            // 
            // labelPlayer2Ammo
            // 
            this.labelPlayer2Ammo.AutoSize = true;
            this.labelPlayer2Ammo.Location = new System.Drawing.Point(133, 20);
            this.labelPlayer2Ammo.Name = "labelPlayer2Ammo";
            this.labelPlayer2Ammo.Size = new System.Drawing.Size(64, 26);
            this.labelPlayer2Ammo.TabIndex = 3;
            this.labelPlayer2Ammo.Text = "Ammunition:\r\n10";
            // 
            // groupBox3
            // 
            this.groupBox3.Location = new System.Drawing.Point(273, 0);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(275, 46);
            this.groupBox3.TabIndex = 6;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "groupBox3";
            // 
            // labelPlayer2Health
            // 
            this.labelPlayer2Health.AutoSize = true;
            this.labelPlayer2Health.Location = new System.Drawing.Point(6, 20);
            this.labelPlayer2Health.Name = "labelPlayer2Health";
            this.labelPlayer2Health.Size = new System.Drawing.Size(41, 26);
            this.labelPlayer2Health.TabIndex = 2;
            this.labelPlayer2Health.Text = "Health:\r\n100";
            // 
            // labelDebug2
            // 
            this.labelDebug2.AutoSize = true;
            this.labelDebug2.Location = new System.Drawing.Point(364, 574);
            this.labelDebug2.Name = "labelDebug2";
            this.labelDebug2.Size = new System.Drawing.Size(39, 13);
            this.labelDebug2.TabIndex = 7;
            this.labelDebug2.Text = "Debug";
            // 
            // buttonRestart
            // 
            this.buttonRestart.AutoSize = true;
            this.buttonRestart.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.buttonRestart.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.buttonRestart.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonRestart.Location = new System.Drawing.Point(228, 585);
            this.buttonRestart.Name = "buttonRestart";
            this.buttonRestart.Size = new System.Drawing.Size(105, 22);
            this.buttonRestart.TabIndex = 8;
            this.buttonRestart.Text = "Restart Level";
            this.buttonRestart.Click += new System.EventHandler(this.buttonRestart_Click);
            // 
            // timerReload
            // 
            this.timerReload.Interval = 3000;
            this.timerReload.Tick += new System.EventHandler(this.timerReload_Tick);
            // 
            // canvas
            // 
            this.canvas.Controls.Add(this.labelWinMessage);
            this.canvas.Location = new System.Drawing.Point(32, 71);
            this.canvas.Name = "canvas";
            this.canvas.Size = new System.Drawing.Size(500, 500);
            this.canvas.TabIndex = 2;
            this.canvas.Paint += new System.Windows.Forms.PaintEventHandler(this.canvas_Paint);
            // 
            // labelWinMessage
            // 
            this.labelWinMessage.AutoSize = true;
            this.labelWinMessage.Font = new System.Drawing.Font("Script MT Bold", 48F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelWinMessage.ForeColor = System.Drawing.Color.Red;
            this.labelWinMessage.Location = new System.Drawing.Point(56, 20);
            this.labelWinMessage.Name = "labelWinMessage";
            this.labelWinMessage.Size = new System.Drawing.Size(394, 77);
            this.labelWinMessage.TabIndex = 0;
            this.labelWinMessage.Text = "Player 1 Win!";
            this.labelWinMessage.Visible = false;
            // 
            // GameWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(572, 615);
            this.Controls.Add(this.buttonRestart);
            this.Controls.Add(this.labelDebug2);
            this.Controls.Add(this.labelDebug);
            this.Controls.Add(this.canvas);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "GameWindow";
            this.Text = "Tanks";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.GameWindow_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.GameWindow_KeyUp);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.canvas.ResumeLayout(false);
            this.canvas.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label labelDebug;
        private System.Windows.Forms.Timer timerFrame;
        private MyCanvas canvas;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label labelPalyer1Ammo;
        private System.Windows.Forms.Label labelPlayer1Health;
        private System.Windows.Forms.Label labelPlayer2Ammo;
        private System.Windows.Forms.Label labelPlayer2Health;
        public System.Windows.Forms.Label labelWinMessage;
        private System.Windows.Forms.Label labelDebug2;
        private System.Windows.Forms.Label buttonRestart;
        private System.Windows.Forms.Timer timerReload;
    }
}

