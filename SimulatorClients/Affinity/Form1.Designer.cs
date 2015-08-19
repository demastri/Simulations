namespace Affinity
{
    partial class Form1
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
            this.displayGrid = new System.Windows.Forms.DataGridView();
            this.GroupCount = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.AffinityPct = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.OpeningPct = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.StepCount = new System.Windows.Forms.TextBox();
            this.StartButton = new System.Windows.Forms.Button();
            this.CurStepLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.displayGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // displayGrid
            // 
            this.displayGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.displayGrid.Location = new System.Drawing.Point(12, 68);
            this.displayGrid.Name = "displayGrid";
            this.displayGrid.Size = new System.Drawing.Size(507, 600);
            this.displayGrid.TabIndex = 0;
            // 
            // GroupCount
            // 
            this.GroupCount.Location = new System.Drawing.Point(111, 16);
            this.GroupCount.Name = "GroupCount";
            this.GroupCount.Size = new System.Drawing.Size(100, 20);
            this.GroupCount.TabIndex = 1;
            this.GroupCount.Text = "2";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Number of Groups";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Affinity %";
            // 
            // AffinityPct
            // 
            this.AffinityPct.Location = new System.Drawing.Point(111, 42);
            this.AffinityPct.Name = "AffinityPct";
            this.AffinityPct.Size = new System.Drawing.Size(100, 20);
            this.AffinityPct.TabIndex = 3;
            this.AffinityPct.Text = "0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(243, 19);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Opening %";
            // 
            // OpeningPct
            // 
            this.OpeningPct.Location = new System.Drawing.Point(342, 16);
            this.OpeningPct.Name = "OpeningPct";
            this.OpeningPct.Size = new System.Drawing.Size(100, 20);
            this.OpeningPct.TabIndex = 5;
            this.OpeningPct.Text = "15";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(243, 46);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(66, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Nbr of Steps";
            // 
            // StepCount
            // 
            this.StepCount.Location = new System.Drawing.Point(342, 43);
            this.StepCount.Name = "StepCount";
            this.StepCount.Size = new System.Drawing.Size(100, 20);
            this.StepCount.TabIndex = 7;
            this.StepCount.Text = "100";
            // 
            // StartButton
            // 
            this.StartButton.Location = new System.Drawing.Point(444, 674);
            this.StartButton.Name = "StartButton";
            this.StartButton.Size = new System.Drawing.Size(75, 23);
            this.StartButton.TabIndex = 9;
            this.StartButton.Text = "Go";
            this.StartButton.UseVisualStyleBackColor = true;
            this.StartButton.Click += new System.EventHandler(this.StartButton_Click);
            // 
            // CurStepLabel
            // 
            this.CurStepLabel.AutoSize = true;
            this.CurStepLabel.Location = new System.Drawing.Point(12, 674);
            this.CurStepLabel.Name = "CurStepLabel";
            this.CurStepLabel.Size = new System.Drawing.Size(72, 13);
            this.CurStepLabel.TabIndex = 10;
            this.CurStepLabel.Text = "Step Count: 0";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(531, 715);
            this.Controls.Add(this.CurStepLabel);
            this.Controls.Add(this.StartButton);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.StepCount);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.OpeningPct);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.AffinityPct);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.GroupCount);
            this.Controls.Add(this.displayGrid);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.displayGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView displayGrid;
        private System.Windows.Forms.TextBox GroupCount;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox AffinityPct;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox OpeningPct;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox StepCount;
        private System.Windows.Forms.Button StartButton;
        private System.Windows.Forms.Label CurStepLabel;
    }
}

