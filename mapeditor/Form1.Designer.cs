﻿namespace mapeditor
{
    partial class Form1
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.button_waku = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button_imgRead = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.button_mapExport = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button_waku
            // 
            this.button_waku.Location = new System.Drawing.Point(35, 118);
            this.button_waku.Name = "button_waku";
            this.button_waku.Size = new System.Drawing.Size(97, 30);
            this.button_waku.TabIndex = 0;
            this.button_waku.Text = "枠作製";
            this.button_waku.UseVisualStyleBackColor = true;
            this.button_waku.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(35, 57);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(73, 19);
            this.textBox1.TabIndex = 1;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(35, 82);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(71, 19);
            this.textBox2.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 57);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "縦";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 85);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "横";
            // 
            // button_imgRead
            // 
            this.button_imgRead.Location = new System.Drawing.Point(35, 263);
            this.button_imgRead.Name = "button_imgRead";
            this.button_imgRead.Size = new System.Drawing.Size(97, 30);
            this.button_imgRead.TabIndex = 5;
            this.button_imgRead.Text = "画像読み取り";
            this.button_imgRead.UseVisualStyleBackColor = true;
            this.button_imgRead.Click += new System.EventHandler(this.button_imgRead_Click);
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 12;
            this.listBox1.Location = new System.Drawing.Point(36, 322);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(95, 244);
            this.listBox1.TabIndex = 6;
            // 
            // button_mapExport
            // 
            this.button_mapExport.Location = new System.Drawing.Point(36, 194);
            this.button_mapExport.Name = "button_mapExport";
            this.button_mapExport.Size = new System.Drawing.Size(97, 30);
            this.button_mapExport.TabIndex = 7;
            this.button_mapExport.Text = "map出力";
            this.button_mapExport.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1197, 615);
            this.Controls.Add(this.button_mapExport);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.button_imgRead);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button_waku);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button_waku;
        private System.Windows.Forms.Button button_imgRead;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button button_mapExport;
    }
}
