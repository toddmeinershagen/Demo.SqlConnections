namespace Demo.SqlConnections.WinForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.CounterTree = new System.Windows.Forms.TreeView();
            this.ExecuteButton = new System.Windows.Forms.Button();
            this.CommandList = new System.Windows.Forms.ListBox();
            this.SourceTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // CounterTree
            // 
            this.CounterTree.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CounterTree.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.CounterTree.Location = new System.Drawing.Point(12, 495);
            this.CounterTree.Name = "CounterTree";
            this.CounterTree.Size = new System.Drawing.Size(429, 281);
            this.CounterTree.TabIndex = 0;
            // 
            // ExecuteButton
            // 
            this.ExecuteButton.Location = new System.Drawing.Point(366, 448);
            this.ExecuteButton.Name = "ExecuteButton";
            this.ExecuteButton.Size = new System.Drawing.Size(75, 23);
            this.ExecuteButton.TabIndex = 1;
            this.ExecuteButton.Text = "Execute";
            this.ExecuteButton.UseVisualStyleBackColor = true;
            this.ExecuteButton.Click += new System.EventHandler(this.ExecuteButton_Click);
            // 
            // CommandList
            // 
            this.CommandList.DisplayMember = "Description";
            this.CommandList.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CommandList.FormattingEnabled = true;
            this.CommandList.ItemHeight = 16;
            this.CommandList.Location = new System.Drawing.Point(13, 12);
            this.CommandList.Name = "CommandList";
            this.CommandList.ScrollAlwaysVisible = true;
            this.CommandList.Size = new System.Drawing.Size(428, 420);
            this.CommandList.TabIndex = 3;
            this.CommandList.ValueMember = "Description";
            this.CommandList.SelectedIndexChanged += new System.EventHandler(this.CommandList_SelectedIndexChanged);
            // 
            // SourceTextBox
            // 
            this.SourceTextBox.BackColor = System.Drawing.Color.LemonChiffon;
            this.SourceTextBox.Font = new System.Drawing.Font("Consolas", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SourceTextBox.ForeColor = System.Drawing.SystemColors.Desktop;
            this.SourceTextBox.Location = new System.Drawing.Point(447, 13);
            this.SourceTextBox.Multiline = true;
            this.SourceTextBox.Name = "SourceTextBox";
            this.SourceTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.SourceTextBox.Size = new System.Drawing.Size(839, 763);
            this.SourceTextBox.TabIndex = 4;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1298, 788);
            this.Controls.Add(this.SourceTextBox);
            this.Controls.Add(this.CommandList);
            this.Controls.Add(this.ExecuteButton);
            this.Controls.Add(this.CounterTree);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "SQL Connection Handling Scenarios";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView CounterTree;
        private System.Windows.Forms.Button ExecuteButton;
        private System.Windows.Forms.ListBox CommandList;
        private System.Windows.Forms.TextBox SourceTextBox;
    }
}

