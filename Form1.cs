using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Data.SqlClient;
using OfficeOpenXml;
using System.IO;
using OfficeOpenXml.Style;

namespace TableSchemaExport
{
    public partial class Form1 : Form
    {

        #region Members

        int _currentExcelRow = 1;
        public string ConnectionString = string.Empty;

        #endregion

        #region Constructor

        public Form1()
        {
            InitializeComponent();
        }

        #endregion

        #region Events

        private void Form1_Load(object sender, EventArgs e)
        {
            Properties.Settings setting = new Properties.Settings();
            this.Input_Connection.Text = setting.ConnectionString;
            this.Input_TableDescription.Text = setting.TableDescription;
        }

        private void Button_Connection_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(this.Input_Connection.Text))
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    conn.Open();
                    ConnectionString = this.Input_Connection.Text;
                    cmd.CommandText = "select name from sysobjects where xtype='U' order by name";

                    this.Input_CheckBoxList.Items.Clear();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            this.Input_CheckBoxList.Items.Add(dr["name"].ToString());
                        }
                    }

                    Properties.Settings setting = new Properties.Settings();
                    setting.ConnectionString = this.Input_Connection.Text;
                    setting.TableDescription = this.Input_TableDescription.Text;
                    setting.Save();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void Button_Export_Click(object sender, EventArgs e)
        {
            try
            {
                List<string> tables = new List<string>();

                for (int i = 0; i < this.Input_CheckBoxList.Items.Count; i++)
                {
                    if (this.Input_CheckBoxList.GetItemChecked(i))
                    {
                        tables.Add(this.Input_CheckBoxList.Items[i].ToString());
                    }
                }

                if (tables.Count > 0)
                {
                    using (ExcelPackage ep = new ExcelPackage())
                    {
                        for (int i = 0; i < tables.Count; i++)
                        {
                            string tableDescription = string.IsNullOrEmpty(this.Input_TableDescription.Text.Trim()) ? tables[i] : GetTableChinese(tables[i]);

                            ep.Workbook.Worksheets.Add(tables[i]); // 新增試算表  
                            ExcelWorksheet sheet = ep.Workbook.Worksheets[i + 1];  //取得剛剛加入的Sheet

                            DataTable dt = GetSchema(tables[i]);
                            DataColumnCollection fields = dt.Columns;

                            _currentExcelRow = 1;
                            sheet.Cells[_currentExcelRow, 1].Value = "資料表說明 : " + (string.Compare(tableDescription, tables[i]) == 0 ? "" : tableDescription);
                            _currentExcelRow++;
                            sheet.Cells[_currentExcelRow, 1].Value = "資料表名稱 : " + tables[i];
                            _currentExcelRow++;
                            _currentExcelRow++;

                            using (var range = sheet.Cells[1, 1, 3, 1]) GetNormalCellStyle(range, Color.Red);

                            #region 產生標題列

                            for (int fieldCount = 0; fieldCount < fields.Count; fieldCount++)
                            {
                                CurrentRowFillCellData(sheet, fieldCount + 1, fields[fieldCount].ColumnName);
                            }

                            using (var range = sheet.Cells[_currentExcelRow, 1, _currentExcelRow, fields.Count]) GetListCellStyle(range, true);

                            _currentExcelRow++;

                            #endregion

                            #region 產生資料列

                            for (int xx = 0; xx < dt.Rows.Count; xx++)
                            {
                                DataRow row = dt.Rows[xx];
                                for (int fieldCount = 0; fieldCount < fields.Count; fieldCount++)
                                {
                                    sheet.Cells[_currentExcelRow, fieldCount + 1].Value = row[fields[fieldCount].ColumnName].ToString();
                                }

                                using (var range = sheet.Cells[_currentExcelRow, 1, _currentExcelRow, fields.Count]) GetListCellStyle(range, false);

                                sheet.Cells[_currentExcelRow, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                sheet.Cells[_currentExcelRow, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                sheet.Cells[_currentExcelRow, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                sheet.Cells[_currentExcelRow, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                sheet.Cells[_currentExcelRow, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                _currentExcelRow++;
                            }

                            #endregion

                            sheet.Column(5).Width = 15;
                            sheet.Column(6).Width = 25;
                            sheet.Column(7).Width = 15;
                            sheet.Column(10).Width = 20;
                        }

                        string excelName = Path.Combine(Application.StartupPath, "TableSchema.xlsx");

                        using (FileStream fs = new FileStream(excelName, FileMode.Create))
                        {
                            ep.SaveAs(fs);
                            MessageBox.Show("檔案已產生到【" + excelName + "】");
                            System.Diagnostics.Process.Start(excelName);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("請先選擇要產出的資料表");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        #endregion

        #region Methods

        private DataTable GetSchema(string tableName)
        {
            DataTable dt = new DataTable();

            string commText = @"SELECT
	                                                    case when isnull(e.is_primary_key,0)=1 then 'V' else '' end as [主鍵],   --Primary Key 
	                                                    case when fkc.parent_object_id is not null then 'V' else '' end as [外來鍵] ,  --[Foreign Key]
	                                                    case when isnull(c.is_identity,0)=1 then 'V' else '' end as [自動遞增], 
                                                        case when b.IS_NULLABLE='YES' then 'V' else '' end as 允許空值 ,
                                                        --a.TABLE_NAME as 表格名稱,
                                                        b.COLUMN_NAME as 欄位名稱,
	                                                    isnull((
		                                                    SELECT
		                                                    value
		                                                    FROM fn_listextendedproperty (NULL, 'schema', 'dbo', 'table', a.TABLE_NAME, 'column', default)
		                                                    WHERE name='MS_Description'
		                                                    and objtype='COLUMN'
		                                                    and objname Collate Chinese_Taiwan_Stroke_CI_AS = b.COLUMN_NAME
	                                                    ),'') as 欄位說明,
                                                        b.DATA_TYPE as 資料型別,
	                                                    --case when b.CHARACTER_MAXIMUM_LENGTH=-1 then 'Max' else isnull(cast(b.CHARACTER_MAXIMUM_LENGTH as nvarchar(200)),'') end as [欄位長度],    
	                                                    case when b.CHARACTER_MAXIMUM_LENGTH='-1' then 'max' 
	                                                    else 
		                                                    case when b.DATA_TYPE='decimal' then '('+cast(b.NUMERIC_PRECISION+b.NUMERIC_SCALE as varchar(10))+','+cast(b.NUMERIC_SCALE as varchar(10))+')'
		                                                    else isnull(cast(b.CHARACTER_MAXIMUM_LENGTH as nvarchar(200)),'')
		                                                    end 
	                                                    end as [欄位長度],  
	                                                    isnull(b.COLUMN_DEFAULT,'') as 預設值,  
                                                        case when fkc.parent_object_id is not null then OBJECT_SCHEMA_NAME(fk.object_id) + '.' + OBJECT_NAME(fk.object_id)  + ' (' + fk.Name + ')' else '' end AS [外來鍵關係]
                                                    FROM INFORMATION_SCHEMA.TABLES  a
                                                    LEFT JOIN INFORMATION_SCHEMA.COLUMNS b ON ( a.TABLE_NAME=b.TABLE_NAME )
                                                    LEFT JOIN sys.columns c ON c.object_id=OBJECT_ID ('dbo.'+a.TABLE_NAME) and c.name=b.COLUMN_NAME
                                                    LEFT JOIN sys.index_columns d on c.object_id=d.object_id and c.column_id=d.column_id
                                                    LEFT JOIN sys.indexes e on e.object_id=d.object_id AND e.is_primary_key=1 AND e.index_id=d.index_id
                                                    LEFT JOIN sys.foreign_key_columns fkc ON fkc.parent_object_id = c.object_id AND fkc.parent_column_id = c.column_id 
                                                    LEFT JOIN sys.columns fk ON fk.object_id = fkc.referenced_object_id AND fk.column_id = fkc.referenced_column_id
                                                    WHERE TABLE_TYPE='BASE TABLE' and a.TABLE_NAME='" + tableName + @"'
                                                    order by a.TABLE_NAME,b.ORDINAL_POSITION ";

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            using (SqlDataAdapter sda = new SqlDataAdapter(commText, conn))
            {
                conn.Open();
                sda.Fill(dt);
            }

            return dt;
        }

        /// <summary>
        /// 取得資料表中文描述
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        private string GetTableChinese(string tableName)
        {
            string result = tableName;

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            using (SqlCommand cmd = conn.CreateCommand())
            {
                conn.Open();
                cmd.CommandText = "SELECT value FROM ::fn_listextendedproperty(default, 'user', 'dbo', 'table', default, default, default) where name='TableDescription' and objname='" + tableName + "'";

                object value = cmd.ExecuteScalar();

                if (value != null) result = value.ToString();
            }

            return result;
        }

        private void CurrentRowFillCellData(ExcelWorksheet sheet, int cell, string value)
        {
            sheet.Cells[_currentExcelRow, cell].Value = value;
        }

        /// <summary>取得Excel Cell Style(EPPlus)</summary>
        private void GetListCellStyle(ExcelRange range, bool header)
        {
            range.Style.Font.SetFromFont(new Font("微軟正黑體", 9));

            if (header)
            {
                //標題列專用
                range.Style.Font.Color.SetColor(Color.White);
                range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                range.Style.Fill.BackgroundColor.SetColor(Color.Black);
                range.Style.Font.Bold = true;
            }

            range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
            range.Style.VerticalAlignment = ExcelVerticalAlignment.Center;

            range.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            range.Style.Border.Bottom.Color.SetColor(Color.Black);
            range.Style.Border.Left.Style = ExcelBorderStyle.Thin;
            range.Style.Border.Left.Color.SetColor(Color.Black);
            range.Style.Border.Right.Style = ExcelBorderStyle.Thin;
            range.Style.Border.Right.Color.SetColor(Color.Black);
            range.Style.Border.Top.Style = ExcelBorderStyle.Thin;
            range.Style.Border.Top.Color.SetColor(Color.Black);
        }

        /// <summary>取得Excel Cell Style(EPPlus)</summary>
        private void GetNormalCellStyle(ExcelRange range, Color fontColor)
        {
            range.Style.Font.SetFromFont(new Font("微軟正黑體", 9));
            range.Style.Font.Bold = true;
            range.Style.Font.Color.SetColor(fontColor);
            range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
            range.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
        }

        #endregion

    }
}
