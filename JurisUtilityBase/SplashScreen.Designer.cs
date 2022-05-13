﻿namespace JurisUtilityBase
{
    partial class SplashScreen
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SplashScreen));
            this.labelAppName = new System.Windows.Forms.Label();
            this.horizontalLine1 = new System.Windows.Forms.Label();
            this.labelStatus = new System.Windows.Forms.Label();
            this.labelHorizontalLine2 = new System.Windows.Forms.Label();
            this.labelVersion = new System.Windows.Forms.Label();
            this.labelCopyright = new System.Windows.Forms.Label();
            this.labelCompany = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // labelAppName
            // 
            this.labelAppName.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelAppName.ForeColor = System.Drawing.Color.Blue;
            this.labelAppName.Location = new System.Drawing.Point(48, 128);
            this.labelAppName.Name = "labelAppName";
            this.labelAppName.Size = new System.Drawing.Size(238, 23);
            this.labelAppName.TabIndex = 1;
            this.labelAppName.Text = "[App Name]";
            this.labelAppName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // horizontalLine1
            // 
            this.horizontalLine1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.horizontalLine1.Location = new System.Drawing.Point(11, 160);
            this.horizontalLine1.Name = "horizontalLine1";
            this.horizontalLine1.Size = new System.Drawing.Size(313, 2);
            this.horizontalLine1.TabIndex = 2;
            // 
            // labelStatus
            // 
            this.labelStatus.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelStatus.Location = new System.Drawing.Point(12, 166);
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(311, 23);
            this.labelStatus.TabIndex = 3;
            this.labelStatus.Text = "(status)";
            this.labelStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelHorizontalLine2
            // 
            this.labelHorizontalLine2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelHorizontalLine2.Location = new System.Drawing.Point(11, 211);
            this.labelHorizontalLine2.Name = "labelHorizontalLine2";
            this.labelHorizontalLine2.Size = new System.Drawing.Size(313, 2);
            this.labelHorizontalLine2.TabIndex = 4;
            // 
            // labelVersion
            // 
            this.labelVersion.AutoSize = true;
            this.labelVersion.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelVersion.Location = new System.Drawing.Point(11, 221);
            this.labelVersion.Name = "labelVersion";
            this.labelVersion.Size = new System.Drawing.Size(80, 16);
            this.labelVersion.TabIndex = 5;
            this.labelVersion.Text = "Version 2.71";
            // 
            // labelCopyright
            // 
            this.labelCopyright.AutoSize = true;
            this.labelCopyright.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCopyright.Location = new System.Drawing.Point(203, 221);
            this.labelCopyright.Name = "labelCopyright";
            this.labelCopyright.Size = new System.Drawing.Size(120, 14);
            this.labelCopyright.TabIndex = 6;
            this.labelCopyright.Text = "Copyright © 1996-2015";
            // 
            // labelCompany
            // 
            this.labelCompany.AutoSize = true;
            this.labelCompany.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCompany.Location = new System.Drawing.Point(217, 235);
            this.labelCompany.Name = "labelCompany";
            this.labelCompany.Size = new System.Drawing.Size(106, 14);
            this.labelCompany.TabIndex = 7;
            this.labelCompany.Text = "Juris by LexisNexis";
            // 
            // SplashScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(327, 327);
            this.ControlBox = false;
            this.Controls.Add(this.labelCompany);
            this.Controls.Add(this.labelCopyright);
            this.Controls.Add(this.labelVersion);
            this.Controls.Add(this.labelHorizontalLine2);
            this.Controls.Add(this.labelStatus);
            this.Controls.Add(this.horizontalLine1);
            this.Controls.Add(this.labelAppName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SplashScreen";
            this.ShowIcon = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.SplashScreen_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label labelAppName;
        private System.Windows.Forms.Label horizontalLine1;
        private System.Windows.Forms.Label labelStatus;
        private System.Windows.Forms.Label labelHorizontalLine2;
        private System.Windows.Forms.Label labelVersion;
        private System.Windows.Forms.Label labelCopyright;
        private System.Windows.Forms.Label labelCompany;
    }
}