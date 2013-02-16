namespace Worklog
{
    partial class dashboardDayTime
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(dashboardDayTime));
            this.panel10 = new System.Windows.Forms.Panel();
            this.panelWithBorder = new System.Windows.Forms.Panel();
            this.Year = new System.Windows.Forms.Label();
            this.DayOf = new System.Windows.Forms.Label();
            this.Day = new System.Windows.Forms.Label();
            this.Month = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.bDaysNExt = new System.Windows.Forms.Button();
            this.button14 = new System.Windows.Forms.Button();
            this.bJumpToDate = new System.Windows.Forms.Button();
            this.bDaysPrevious = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel10.SuspendLayout();
            this.panelWithBorder.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel10
            // 
            this.panel10.Controls.Add(this.panel3);
            this.panel10.Controls.Add(this.panel1);
            this.panel10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel10.Location = new System.Drawing.Point(0, 0);
            this.panel10.Name = "panel10";
            this.panel10.Size = new System.Drawing.Size(193, 216);
            this.panel10.TabIndex = 29;
            // 
            // panelWithBorder
            // 
            this.panelWithBorder.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panelWithBorder.BackColor = System.Drawing.SystemColors.Control;
            this.panelWithBorder.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelWithBorder.Controls.Add(this.Year);
            this.panelWithBorder.Controls.Add(this.DayOf);
            this.panelWithBorder.Controls.Add(this.Day);
            this.panelWithBorder.Controls.Add(this.Month);
            this.panelWithBorder.Location = new System.Drawing.Point(32, 0);
            this.panelWithBorder.Name = "panelWithBorder";
            this.panelWithBorder.Size = new System.Drawing.Size(128, 166);
            this.panelWithBorder.TabIndex = 3;
			// added this Feb 2013
			this.panelWithBorder.Dock = System.Windows.Forms.DockStyle.Fill;
            // 
            // Year
            // 
            this.Year.Dock = System.Windows.Forms.DockStyle.Top;
            this.Year.Font = new System.Drawing.Font("Georgia", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Year.ForeColor = System.Drawing.SystemColors.InfoText;
            this.Year.Location = new System.Drawing.Point(0, 66);
            this.Year.Name = "Year";
            this.Year.Size = new System.Drawing.Size(124, 18);
            this.Year.TabIndex = 3;
            this.Year.Text = "Thursday";
            this.Year.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DayOf
            // 
            this.DayOf.Dock = System.Windows.Forms.DockStyle.Top;
            this.DayOf.Font = new System.Drawing.Font("Georgia", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DayOf.ForeColor = System.Drawing.SystemColors.InfoText;
            this.DayOf.Location = new System.Drawing.Point(0, 48);
            this.DayOf.Name = "DayOf";
            this.DayOf.Size = new System.Drawing.Size(124, 18);
            this.DayOf.TabIndex = 2;
            this.DayOf.Text = "Thursday";
            this.DayOf.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Day
            // 
            this.Day.Dock = System.Windows.Forms.DockStyle.Top;
            this.Day.Font = new System.Drawing.Font("Georgia", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Day.ForeColor = System.Drawing.SystemColors.InfoText;
            this.Day.Location = new System.Drawing.Point(0, 23);
            this.Day.Name = "Day";
            this.Day.Size = new System.Drawing.Size(124, 25);
            this.Day.TabIndex = 1;
            this.Day.Text = "11";
            this.Day.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Month
            // 
            this.Month.Dock = System.Windows.Forms.DockStyle.Top;
            this.Month.Font = new System.Drawing.Font("Georgia", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Month.ForeColor = System.Drawing.SystemColors.InfoText;
            this.Month.Location = new System.Drawing.Point(0, 0);
            this.Month.Name = "Month";
            this.Month.Size = new System.Drawing.Size(124, 23);
            this.Month.TabIndex = 0;
            this.Month.Text = "October";
            this.Month.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.bDaysNExt);
            this.panel1.Controls.Add(this.button14);
            this.panel1.Controls.Add(this.bJumpToDate);
            this.panel1.Controls.Add(this.bDaysPrevious);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 189);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(193, 27);
            this.panel1.TabIndex = 27;
            // 
            // bDaysNExt
            // 
            this.bDaysNExt.Dock = System.Windows.Forms.DockStyle.Left;
            this.bDaysNExt.FlatAppearance.BorderSize = 0;
            this.bDaysNExt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.bDaysNExt.Image = CoreUtilities.FileUtils.GetImage_ForDLL("arrow_right.png");// ((System.Drawing.Image)(resources.GetObject("bDaysNExt.Image")));
            this.bDaysNExt.Location = new System.Drawing.Point(78, 0);
            this.bDaysNExt.Name = "bDaysNExt";
            this.bDaysNExt.Size = new System.Drawing.Size(26, 27);
            this.bDaysNExt.TabIndex = 14;
            this.bDaysNExt.UseVisualStyleBackColor = true;
            this.bDaysNExt.Click += new System.EventHandler(this.bDaysNExt_Click);
            // 
            // button14
            // 
            this.button14.Dock = System.Windows.Forms.DockStyle.Left;
            this.button14.FlatAppearance.BorderSize = 0;
            this.button14.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button14.Image =CoreUtilities.FileUtils.GetImage_ForDLL("date_next.png");
            this.button14.Location = new System.Drawing.Point(52, 0);
            this.button14.Name = "button14";
            this.button14.Size = new System.Drawing.Size(26, 27);
            this.button14.TabIndex = 26;
            this.button14.UseVisualStyleBackColor = true;
            this.button14.Click += new System.EventHandler(this.button14_Click);
            // 
            // bJumpToDate
            // 
            this.bJumpToDate.Dock = System.Windows.Forms.DockStyle.Left;
            this.bJumpToDate.FlatAppearance.BorderSize = 0;
            this.bJumpToDate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.bJumpToDate.Image = CoreUtilities.FileUtils.GetImage_ForDLL("date.png");//((System.Drawing.Image)(resources.GetObject("bJumpToDate.Image")));
            this.bJumpToDate.Location = new System.Drawing.Point(26, 0);
            this.bJumpToDate.Name = "bJumpToDate";
            this.bJumpToDate.Size = new System.Drawing.Size(26, 27);
            this.bJumpToDate.TabIndex = 25;
            this.bJumpToDate.UseVisualStyleBackColor = true;
            this.bJumpToDate.Click += new System.EventHandler(this.bJumpToDate_Click);
            // 
            // bDaysPrevious
            // 
            this.bDaysPrevious.Dock = System.Windows.Forms.DockStyle.Left;
            this.bDaysPrevious.FlatAppearance.BorderSize = 0;
            this.bDaysPrevious.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.bDaysPrevious.Image = CoreUtilities.FileUtils.GetImage_ForDLL("arrow_left.png");///((System.Drawing.Image)(resources.GetObject("bDaysPrevious.Image")));
            this.bDaysPrevious.Location = new System.Drawing.Point(0, 0);
            this.bDaysPrevious.Name = "bDaysPrevious";
            this.bDaysPrevious.Size = new System.Drawing.Size(26, 27);
            this.bDaysPrevious.TabIndex = 15;
            this.bDaysPrevious.UseVisualStyleBackColor = true;
            this.bDaysPrevious.Click += new System.EventHandler(this.bDaysPrevious_Click);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.panelWithBorder);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(193, 189);
            this.panel3.TabIndex = 28;
            // 
            // dashboardDayTime
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel10);
            this.Name = "dashboardDayTime";
            this.Size = new System.Drawing.Size(193, 216);
            this.panel10.ResumeLayout(false);
            this.panelWithBorder.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel10;
        private System.Windows.Forms.Button button14;
        private System.Windows.Forms.Panel panelWithBorder;
        private System.Windows.Forms.Label Year;
        private System.Windows.Forms.Label DayOf;
        private System.Windows.Forms.Label Day;
        private System.Windows.Forms.Label Month;
        private System.Windows.Forms.Button bJumpToDate;
        private System.Windows.Forms.Button bDaysPrevious;
        private System.Windows.Forms.Button bDaysNExt;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel3;
    }
}
