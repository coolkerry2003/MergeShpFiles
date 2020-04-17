namespace MergeShpFiles
{
    partial class Form1
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置 Managed 資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.tb_SourceFolder = new System.Windows.Forms.TextBox();
            this.lb_SouceFolder = new System.Windows.Forms.Label();
            this.btn_OpenFolder = new System.Windows.Forms.Button();
            this.btn_ExportMergeResult = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tb_SourceFolder
            // 
            this.tb_SourceFolder.Location = new System.Drawing.Point(12, 25);
            this.tb_SourceFolder.Name = "tb_SourceFolder";
            this.tb_SourceFolder.Size = new System.Drawing.Size(232, 22);
            this.tb_SourceFolder.TabIndex = 0;
            // 
            // lb_SouceFolder
            // 
            this.lb_SouceFolder.AutoSize = true;
            this.lb_SouceFolder.Location = new System.Drawing.Point(13, 7);
            this.lb_SouceFolder.Name = "lb_SouceFolder";
            this.lb_SouceFolder.Size = new System.Drawing.Size(61, 12);
            this.lb_SouceFolder.TabIndex = 1;
            this.lb_SouceFolder.Text = "SHP資料夾";
            // 
            // btn_OpenFolder
            // 
            this.btn_OpenFolder.Location = new System.Drawing.Point(250, 25);
            this.btn_OpenFolder.Name = "btn_OpenFolder";
            this.btn_OpenFolder.Size = new System.Drawing.Size(22, 23);
            this.btn_OpenFolder.TabIndex = 2;
            this.btn_OpenFolder.Text = "...";
            this.btn_OpenFolder.UseVisualStyleBackColor = true;
            this.btn_OpenFolder.Click += new System.EventHandler(this.btn_OpenFolder_Click);
            // 
            // btn_ExportMergeResult
            // 
            this.btn_ExportMergeResult.Location = new System.Drawing.Point(181, 54);
            this.btn_ExportMergeResult.Name = "btn_ExportMergeResult";
            this.btn_ExportMergeResult.Size = new System.Drawing.Size(91, 23);
            this.btn_ExportMergeResult.TabIndex = 3;
            this.btn_ExportMergeResult.Text = "匯出合併結果";
            this.btn_ExportMergeResult.UseVisualStyleBackColor = true;
            this.btn_ExportMergeResult.Click += new System.EventHandler(this.btn_ExportMergeResult_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 89);
            this.Controls.Add(this.btn_ExportMergeResult);
            this.Controls.Add(this.btn_OpenFolder);
            this.Controls.Add(this.lb_SouceFolder);
            this.Controls.Add(this.tb_SourceFolder);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Shown += new System.EventHandler(this.Form1_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tb_SourceFolder;
        private System.Windows.Forms.Label lb_SouceFolder;
        private System.Windows.Forms.Button btn_OpenFolder;
        private System.Windows.Forms.Button btn_ExportMergeResult;
    }
}

