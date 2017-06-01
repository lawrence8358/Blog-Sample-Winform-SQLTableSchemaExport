namespace TableSchemaExport
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.Button_Connection = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.Button_Export = new System.Windows.Forms.Button();
            this.Input_CheckBoxList = new System.Windows.Forms.CheckedListBox();
            this.Input_TableDescription = new TableSchemaExport.MyTextBox();
            this.Input_Connection = new TableSchemaExport.MyTextBox();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tableLayoutPanel1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(468, 92);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "資料庫連線";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.Input_TableDescription, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.Button_Connection, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.Input_Connection, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 23);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(462, 66);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // Button_Connection
            // 
            this.Button_Connection.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Button_Connection.Location = new System.Drawing.Point(349, 3);
            this.Button_Connection.Name = "Button_Connection";
            this.Button_Connection.Size = new System.Drawing.Size(110, 27);
            this.Button_Connection.TabIndex = 0;
            this.Button_Connection.Text = "連接資料庫";
            this.Button_Connection.UseVisualStyleBackColor = true;
            this.Button_Connection.Click += new System.EventHandler(this.Button_Connection_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tableLayoutPanel2);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 92);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(468, 491);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "資料表清單";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.Button_Export, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.Input_CheckBoxList, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 23);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.Size = new System.Drawing.Size(462, 465);
            this.tableLayoutPanel2.TabIndex = 3;
            // 
            // Button_Export
            // 
            this.Button_Export.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Button_Export.Location = new System.Drawing.Point(3, 439);
            this.Button_Export.Name = "Button_Export";
            this.Button_Export.Size = new System.Drawing.Size(456, 23);
            this.Button_Export.TabIndex = 2;
            this.Button_Export.Text = "產出結構表";
            this.Button_Export.UseVisualStyleBackColor = true;
            this.Button_Export.Click += new System.EventHandler(this.Button_Export_Click);
            // 
            // Input_CheckBoxList
            // 
            this.Input_CheckBoxList.CheckOnClick = true;
            this.Input_CheckBoxList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Input_CheckBoxList.FormattingEnabled = true;
            this.Input_CheckBoxList.Location = new System.Drawing.Point(3, 3);
            this.Input_CheckBoxList.Name = "Input_CheckBoxList";
            this.Input_CheckBoxList.Size = new System.Drawing.Size(456, 430);
            this.Input_CheckBoxList.TabIndex = 0;
            // 
            // Input_TableDescription
            // 
            this.Input_TableDescription.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Input_TableDescription.Location = new System.Drawing.Point(3, 36);
            this.Input_TableDescription.Name = "Input_TableDescription";
            this.Input_TableDescription.Size = new System.Drawing.Size(340, 27);
            this.Input_TableDescription.TabIndex = 2;
            this.Input_TableDescription.TipColor = System.Drawing.SystemColors.Highlight;
            this.Input_TableDescription.TipFont = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Input_TableDescription.TipText = "資料表擴充屬性(顯示匯出中文備註)";
            // 
            // Input_Connection
            // 
            this.Input_Connection.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Input_Connection.Location = new System.Drawing.Point(3, 3);
            this.Input_Connection.Name = "Input_Connection";
            this.Input_Connection.Size = new System.Drawing.Size(340, 27);
            this.Input_Connection.TabIndex = 1;
            this.Input_Connection.TipColor = System.Drawing.SystemColors.Highlight;
            this.Input_Connection.TipFont = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Input_Connection.TipText = "請輸入資料庫連線字串";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(468, 583);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "資料庫匯出工具";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button Button_Connection;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckedListBox Input_CheckBoxList; 
        private System.Windows.Forms.Button Button_Export;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private MyTextBox Input_Connection;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private MyTextBox Input_TableDescription;
    }
}

