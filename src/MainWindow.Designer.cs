
namespace MacroAutomation
{
    partial class MainWindow
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
            this.StartBtn = new System.Windows.Forms.Button();
            this.RunBtn = new System.Windows.Forms.Button();
            this.exportBtn = new System.Windows.Forms.Button();
            this.dataGrid = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // StartBtn
            // 
            this.StartBtn.Font = new System.Drawing.Font("Segoe UI", 9.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StartBtn.Location = new System.Drawing.Point(53, 38);
            this.StartBtn.Name = "StartBtn";
            this.StartBtn.Size = new System.Drawing.Size(85, 31);
            this.StartBtn.TabIndex = 0;
            this.StartBtn.Text = "Record";
            this.StartBtn.UseVisualStyleBackColor = true;
            this.StartBtn.Click += new System.EventHandler(this.button1_Click);
            // 
            // RunBtn
            // 
            this.RunBtn.Enabled = false;
            this.RunBtn.Font = new System.Drawing.Font("Segoe UI", 9.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RunBtn.Location = new System.Drawing.Point(270, 38);
            this.RunBtn.Name = "RunBtn";
            this.RunBtn.Size = new System.Drawing.Size(85, 31);
            this.RunBtn.TabIndex = 1;
            this.RunBtn.Text = "Run";
            this.RunBtn.UseVisualStyleBackColor = true;
            this.RunBtn.Click += new System.EventHandler(this.run_Click);
            // 
            // exportBtn
            // 
            this.exportBtn.Enabled = false;
            this.exportBtn.Font = new System.Drawing.Font("Segoe UI", 9.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.exportBtn.Location = new System.Drawing.Point(161, 38);
            this.exportBtn.Name = "exportBtn";
            this.exportBtn.Size = new System.Drawing.Size(85, 31);
            this.exportBtn.TabIndex = 2;
            this.exportBtn.Text = "Export";
            this.exportBtn.UseVisualStyleBackColor = true;
            this.exportBtn.Click += new System.EventHandler(this.export_Click);
            // 
            // dataGrid
            // 
            this.dataGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGrid.Location = new System.Drawing.Point(24, 119);
            this.dataGrid.MultiSelect = false;
            this.dataGrid.Name = "dataGrid";
            this.dataGrid.ReadOnly = true;
            this.dataGrid.Size = new System.Drawing.Size(447, 150);
            this.dataGrid.TabIndex = 3;
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(492, 296);
            this.Controls.Add(this.dataGrid);
            this.Controls.Add(this.exportBtn);
            this.Controls.Add(this.RunBtn);
            this.Controls.Add(this.StartBtn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainWindow";
            this.Text = "MainWindow";
            this.Load += new System.EventHandler(this.MainWindow_Load);
            this.VisibleChanged += new System.EventHandler(this.MainWindow_VisibleChanged);
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        protected System.Windows.Forms.Button StartBtn;
        protected System.Windows.Forms.Button RunBtn;
        protected System.Windows.Forms.Button exportBtn;
        protected System.Windows.Forms.DataGridView dataGrid;
    }
}