namespace QTimerSandbox
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
            this.lblCounter = new System.Windows.Forms.Label();
            this.btnDisposeTimer = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.txtPeriod = new System.Windows.Forms.TextBox();
            this.btnModifyPeriod = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblCounter
            // 
            this.lblCounter.AutoSize = true;
            this.lblCounter.Location = new System.Drawing.Point(85, 150);
            this.lblCounter.Name = "lblCounter";
            this.lblCounter.Size = new System.Drawing.Size(13, 13);
            this.lblCounter.TabIndex = 0;
            this.lblCounter.Text = "0";
            // 
            // btnDisposeTimer
            // 
            this.btnDisposeTimer.Location = new System.Drawing.Point(33, 68);
            this.btnDisposeTimer.Name = "btnDisposeTimer";
            this.btnDisposeTimer.Size = new System.Drawing.Size(89, 23);
            this.btnDisposeTimer.TabIndex = 1;
            this.btnDisposeTimer.Text = "Dispose Timer";
            this.btnDisposeTimer.UseVisualStyleBackColor = true;
            this.btnDisposeTimer.Click += new System.EventHandler(this.btnDisposeTimer_Click);
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(33, 12);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(89, 23);
            this.btnStart.TabIndex = 2;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(32, 41);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(90, 23);
            this.btnStop.TabIndex = 3;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // txtPeriod
            // 
            this.txtPeriod.Location = new System.Drawing.Point(33, 127);
            this.txtPeriod.Name = "txtPeriod";
            this.txtPeriod.Size = new System.Drawing.Size(89, 20);
            this.txtPeriod.TabIndex = 4;
            // 
            // btnModifyPeriod
            // 
            this.btnModifyPeriod.Location = new System.Drawing.Point(33, 98);
            this.btnModifyPeriod.Name = "btnModifyPeriod";
            this.btnModifyPeriod.Size = new System.Drawing.Size(89, 23);
            this.btnModifyPeriod.TabIndex = 5;
            this.btnModifyPeriod.Text = "Modify Period";
            this.btnModifyPeriod.UseVisualStyleBackColor = true;
            this.btnModifyPeriod.Click += new System.EventHandler(this.btnModifyPeriod_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(47, 150);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Calls:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(158, 179);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnModifyPeriod);
            this.Controls.Add(this.txtPeriod);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.btnDisposeTimer);
            this.Controls.Add(this.lblCounter);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblCounter;
        private System.Windows.Forms.Button btnDisposeTimer;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.TextBox txtPeriod;
        private System.Windows.Forms.Button btnModifyPeriod;
        private System.Windows.Forms.Label label1;
    }
}

