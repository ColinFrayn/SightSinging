namespace SightSinging {
  partial class MainForm {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing) {
      if (disposing && (components != null)) {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent() {
      this.menuStrip1 = new System.Windows.Forms.MenuStrip();
      this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.clefToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.sopranoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.altoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.tenorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.bassToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.difficultyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.veryEasyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.easyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.mediumToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.hardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.advancedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.expertToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.aboutToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
      this.aboutToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
      this.tbVolume = new System.Windows.Forms.TrackBar();
      this.label1 = new System.Windows.Forms.Label();
      this.btRandom = new System.Windows.Forms.Button();
      this.label2 = new System.Windows.Forms.Label();
      this.tbSpeed = new System.Windows.Forms.TrackBar();
      this.btLast = new System.Windows.Forms.Button();
      this.btNext = new System.Windows.Forms.Button();
      this.btPlay = new System.Windows.Forms.Button();
      this.btStop = new System.Windows.Forms.Button();
      this.pbScore = new System.Windows.Forms.PictureBox();
      this.btCue = new System.Windows.Forms.Button();
      this.menuStrip1.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.tbVolume)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.tbSpeed)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pbScore)).BeginInit();
      this.SuspendLayout();
      // 
      // menuStrip1
      // 
      this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.aboutToolStripMenuItem,
            this.settingsToolStripMenuItem,
            this.helpToolStripMenuItem});
      this.menuStrip1.Location = new System.Drawing.Point(0, 0);
      this.menuStrip1.Name = "menuStrip1";
      this.menuStrip1.Size = new System.Drawing.Size(788, 24);
      this.menuStrip1.TabIndex = 1;
      this.menuStrip1.Text = "menuStrip1";
      // 
      // fileToolStripMenuItem
      // 
      this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
      this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
      this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
      this.fileToolStripMenuItem.Text = "&File";
      // 
      // exitToolStripMenuItem
      // 
      this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
      this.exitToolStripMenuItem.Size = new System.Drawing.Size(92, 22);
      this.exitToolStripMenuItem.Text = "E&xit";
      this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
      // 
      // aboutToolStripMenuItem
      // 
      this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
      this.aboutToolStripMenuItem.Size = new System.Drawing.Size(12, 20);
      // 
      // settingsToolStripMenuItem
      // 
      this.settingsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.clefToolStripMenuItem,
            this.difficultyToolStripMenuItem});
      this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
      this.settingsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
      this.settingsToolStripMenuItem.Text = "&Settings";
      // 
      // clefToolStripMenuItem
      // 
      this.clefToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sopranoToolStripMenuItem,
            this.altoToolStripMenuItem,
            this.tenorToolStripMenuItem,
            this.bassToolStripMenuItem});
      this.clefToolStripMenuItem.Name = "clefToolStripMenuItem";
      this.clefToolStripMenuItem.Size = new System.Drawing.Size(126, 22);
      this.clefToolStripMenuItem.Text = "&Voice Part";
      // 
      // sopranoToolStripMenuItem
      // 
      this.sopranoToolStripMenuItem.Name = "sopranoToolStripMenuItem";
      this.sopranoToolStripMenuItem.Size = new System.Drawing.Size(118, 22);
      this.sopranoToolStripMenuItem.Text = "&Soprano";
      this.sopranoToolStripMenuItem.Click += new System.EventHandler(this.sopranoToolStripMenuItem_Click);
      // 
      // altoToolStripMenuItem
      // 
      this.altoToolStripMenuItem.Name = "altoToolStripMenuItem";
      this.altoToolStripMenuItem.Size = new System.Drawing.Size(118, 22);
      this.altoToolStripMenuItem.Text = "&Alto";
      this.altoToolStripMenuItem.Click += new System.EventHandler(this.altoToolStripMenuItem_Click);
      // 
      // tenorToolStripMenuItem
      // 
      this.tenorToolStripMenuItem.Name = "tenorToolStripMenuItem";
      this.tenorToolStripMenuItem.Size = new System.Drawing.Size(118, 22);
      this.tenorToolStripMenuItem.Text = "&Tenor";
      this.tenorToolStripMenuItem.Click += new System.EventHandler(this.tenorToolStripMenuItem_Click);
      // 
      // bassToolStripMenuItem
      // 
      this.bassToolStripMenuItem.Name = "bassToolStripMenuItem";
      this.bassToolStripMenuItem.Size = new System.Drawing.Size(118, 22);
      this.bassToolStripMenuItem.Text = "&Bass";
      this.bassToolStripMenuItem.Click += new System.EventHandler(this.bassToolStripMenuItem_Click);
      // 
      // difficultyToolStripMenuItem
      // 
      this.difficultyToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.veryEasyToolStripMenuItem,
            this.easyToolStripMenuItem,
            this.mediumToolStripMenuItem,
            this.hardToolStripMenuItem,
            this.advancedToolStripMenuItem,
            this.expertToolStripMenuItem});
      this.difficultyToolStripMenuItem.Name = "difficultyToolStripMenuItem";
      this.difficultyToolStripMenuItem.Size = new System.Drawing.Size(126, 22);
      this.difficultyToolStripMenuItem.Text = "&Difficulty";
      // 
      // veryEasyToolStripMenuItem
      // 
      this.veryEasyToolStripMenuItem.Name = "veryEasyToolStripMenuItem";
      this.veryEasyToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
      this.veryEasyToolStripMenuItem.Text = "Very Easy";
      this.veryEasyToolStripMenuItem.Click += new System.EventHandler(this.veryEasyToolStripMenuItem_Click);
      // 
      // easyToolStripMenuItem
      // 
      this.easyToolStripMenuItem.Name = "easyToolStripMenuItem";
      this.easyToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
      this.easyToolStripMenuItem.Text = "Easy";
      this.easyToolStripMenuItem.Click += new System.EventHandler(this.easyToolStripMenuItem_Click);
      // 
      // mediumToolStripMenuItem
      // 
      this.mediumToolStripMenuItem.Name = "mediumToolStripMenuItem";
      this.mediumToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
      this.mediumToolStripMenuItem.Text = "Medium";
      this.mediumToolStripMenuItem.Click += new System.EventHandler(this.mediumToolStripMenuItem_Click);
      // 
      // hardToolStripMenuItem
      // 
      this.hardToolStripMenuItem.Name = "hardToolStripMenuItem";
      this.hardToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
      this.hardToolStripMenuItem.Text = "Hard";
      this.hardToolStripMenuItem.Click += new System.EventHandler(this.hardToolStripMenuItem_Click);
      // 
      // advancedToolStripMenuItem
      // 
      this.advancedToolStripMenuItem.Name = "advancedToolStripMenuItem";
      this.advancedToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
      this.advancedToolStripMenuItem.Text = "Advanced";
      this.advancedToolStripMenuItem.Click += new System.EventHandler(this.advancedToolStripMenuItem_Click);
      // 
      // expertToolStripMenuItem
      // 
      this.expertToolStripMenuItem.Name = "expertToolStripMenuItem";
      this.expertToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
      this.expertToolStripMenuItem.Text = "Expert";
      this.expertToolStripMenuItem.Click += new System.EventHandler(this.expertToolStripMenuItem_Click);
      // 
      // helpToolStripMenuItem
      // 
      this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem2});
      this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
      this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
      this.helpToolStripMenuItem.Text = "&Help";
      // 
      // aboutToolStripMenuItem2
      // 
      this.aboutToolStripMenuItem2.Name = "aboutToolStripMenuItem2";
      this.aboutToolStripMenuItem2.Size = new System.Drawing.Size(107, 22);
      this.aboutToolStripMenuItem2.Text = "&About";
      this.aboutToolStripMenuItem2.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
      // 
      // toolStripSeparator1
      // 
      this.toolStripSeparator1.Name = "toolStripSeparator1";
      this.toolStripSeparator1.Size = new System.Drawing.Size(149, 6);
      // 
      // aboutToolStripMenuItem1
      // 
      this.aboutToolStripMenuItem1.Name = "aboutToolStripMenuItem1";
      this.aboutToolStripMenuItem1.Size = new System.Drawing.Size(32, 19);
      // 
      // tbVolume
      // 
      this.tbVolume.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.tbVolume.AutoSize = false;
      this.tbVolume.LargeChange = 10;
      this.tbVolume.Location = new System.Drawing.Point(624, 237);
      this.tbVolume.Maximum = 100;
      this.tbVolume.Minimum = 20;
      this.tbVolume.Name = "tbVolume";
      this.tbVolume.Size = new System.Drawing.Size(160, 27);
      this.tbVolume.SmallChange = 4;
      this.tbVolume.TabIndex = 3;
      this.tbVolume.TickStyle = System.Windows.Forms.TickStyle.None;
      this.tbVolume.Value = 60;
      // 
      // label1
      // 
      this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(586, 242);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(42, 13);
      this.label1.TabIndex = 5;
      this.label1.Text = "Volume";
      this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // btRandom
      // 
      this.btRandom.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.btRandom.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.btRandom.Location = new System.Drawing.Point(254, 238);
      this.btRandom.Name = "btRandom";
      this.btRandom.Size = new System.Drawing.Size(82, 26);
      this.btRandom.TabIndex = 6;
      this.btRandom.Text = "Generate";
      this.btRandom.UseVisualStyleBackColor = true;
      this.btRandom.Click += new System.EventHandler(this.btRandom_Click);
      // 
      // label2
      // 
      this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(371, 242);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(38, 13);
      this.label2.TabIndex = 8;
      this.label2.Text = "Speed";
      this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // tbSpeed
      // 
      this.tbSpeed.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.tbSpeed.AutoSize = false;
      this.tbSpeed.Location = new System.Drawing.Point(408, 238);
      this.tbSpeed.Maximum = 30;
      this.tbSpeed.Minimum = 10;
      this.tbSpeed.Name = "tbSpeed";
      this.tbSpeed.Size = new System.Drawing.Size(160, 25);
      this.tbSpeed.TabIndex = 7;
      this.tbSpeed.TickStyle = System.Windows.Forms.TickStyle.None;
      this.tbSpeed.Value = 10;
      this.tbSpeed.Scroll += new System.EventHandler(this.tbSpeed_Scroll);
      // 
      // btLast
      // 
      this.btLast.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.btLast.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.btLast.Location = new System.Drawing.Point(3, 238);
      this.btLast.Name = "btLast";
      this.btLast.Size = new System.Drawing.Size(22, 26);
      this.btLast.TabIndex = 10;
      this.btLast.Text = "<";
      this.btLast.UseVisualStyleBackColor = true;
      this.btLast.Click += new System.EventHandler(this.btLast_Click);
      // 
      // btNext
      // 
      this.btNext.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.btNext.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.btNext.Location = new System.Drawing.Point(143, 238);
      this.btNext.Name = "btNext";
      this.btNext.Size = new System.Drawing.Size(22, 26);
      this.btNext.TabIndex = 11;
      this.btNext.Text = ">";
      this.btNext.UseVisualStyleBackColor = true;
      this.btNext.Click += new System.EventHandler(this.btNext_Click);
      // 
      // btPlay
      // 
      this.btPlay.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.btPlay.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.btPlay.Location = new System.Drawing.Point(82, 238);
      this.btPlay.Name = "btPlay";
      this.btPlay.Size = new System.Drawing.Size(56, 26);
      this.btPlay.TabIndex = 12;
      this.btPlay.Text = "Play";
      this.btPlay.UseVisualStyleBackColor = true;
      this.btPlay.Click += new System.EventHandler(this.btPlay_Click);
      // 
      // btStop
      // 
      this.btStop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.btStop.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.btStop.Location = new System.Drawing.Point(31, 238);
      this.btStop.Name = "btStop";
      this.btStop.Size = new System.Drawing.Size(45, 26);
      this.btStop.TabIndex = 13;
      this.btStop.Text = "Stop";
      this.btStop.UseVisualStyleBackColor = true;
      this.btStop.Click += new System.EventHandler(this.btStop_Click);
      // 
      // pbScore
      // 
      this.pbScore.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.pbScore.Location = new System.Drawing.Point(0, 24);
      this.pbScore.Name = "pbScore";
      this.pbScore.Size = new System.Drawing.Size(788, 208);
      this.pbScore.TabIndex = 2;
      this.pbScore.TabStop = false;
      this.pbScore.Paint += new System.Windows.Forms.PaintEventHandler(this.pbScore_Paint);
      this.pbScore.Resize += new System.EventHandler(this.pbScore_Resize);
      // 
      // btCue
      // 
      this.btCue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.btCue.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.btCue.Location = new System.Drawing.Point(171, 238);
      this.btCue.Name = "btCue";
      this.btCue.Size = new System.Drawing.Size(41, 26);
      this.btCue.TabIndex = 14;
      this.btCue.Text = "Cue";
      this.btCue.UseVisualStyleBackColor = true;
      this.btCue.Click += new System.EventHandler(this.btCue_Click);
      // 
      // MainForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(788, 269);
      this.Controls.Add(this.btCue);
      this.Controls.Add(this.btStop);
      this.Controls.Add(this.btPlay);
      this.Controls.Add(this.btNext);
      this.Controls.Add(this.btLast);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.tbSpeed);
      this.Controls.Add(this.btRandom);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.tbVolume);
      this.Controls.Add(this.pbScore);
      this.Controls.Add(this.menuStrip1);
      this.MainMenuStrip = this.menuStrip1;
      this.Name = "MainForm";
      this.Text = "SightSinging Tutor";
      this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
      this.Load += new System.EventHandler(this.MainForm_Load);
      this.menuStrip1.ResumeLayout(false);
      this.menuStrip1.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.tbVolume)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.tbSpeed)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pbScore)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion
    private System.Windows.Forms.MenuStrip menuStrip1;
    private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
    private System.Windows.Forms.PictureBox pbScore;
    private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem1;
    private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem2;
    private System.Windows.Forms.TrackBar tbVolume;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Button btRandom;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.TrackBar tbSpeed;
    private System.Windows.Forms.ToolStripMenuItem clefToolStripMenuItem;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    private System.Windows.Forms.ToolStripMenuItem sopranoToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem tenorToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem bassToolStripMenuItem;
    private System.Windows.Forms.Button btLast;
    private System.Windows.Forms.Button btNext;
    private System.Windows.Forms.Button btPlay;
    private System.Windows.Forms.Button btStop;
    private System.Windows.Forms.ToolStripMenuItem difficultyToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem veryEasyToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem easyToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem mediumToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem hardToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem advancedToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem expertToolStripMenuItem;
    private System.Windows.Forms.Button btCue;
    private System.Windows.Forms.ToolStripMenuItem altoToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
  }
}

