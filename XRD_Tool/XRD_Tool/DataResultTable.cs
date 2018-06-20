using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Windows.Forms;
using System.Drawing;
using System.Collections;

namespace XRD_Tool
{

    public class DataResultTable
    {
        private System.Windows.Forms.DataGridView myDataGridView;
        public DataSet myDataSet = new DataSet("DataResult");
        public DataTable myDataTable = new DataTable("DataResultTable");

        // 列名
        //public string[] myColumnNames = { "Beta", "Psi", "Sin^psi", "DSpacing", "2Theta", "Strain*E3", "FWHM", "Breadth", "Intensity" };
        public string[] myColumnNames = { "Psi角度", "Sin^psi", "D值", "峰位角度", "半峰宽", "强度", "应变*E3", "%(d-d0)/d0"};


        // 构造函数
        public DataResultTable(System.Windows.Forms.DataGridView dgv)
        {
            this.myDataGridView = dgv;

            this.InitDataResultTable();
        }

        private void InitDataResultTable()
        {
            DataColumnCollection columns = myDataTable.Columns;
            DataColumn column = null;

            for (int i = 0; i < myColumnNames.Length; i++)
            {
                column = columns.Add(myColumnNames[i], typeof(double));
                //column.DataType = System.Type.GetType("System.Double");
                //this.dataGridViewCalcResult.Columns[i].DefaultCellStyle.Format = "0.00";
            }
            myDataSet.Tables.Add(myDataTable);

            this.myDataGridView.DataSource = myDataSet.Tables[0];
            //this.myDataGridView.Columns[0].DefaultCellStyle.Format = "N2";  // "Beta"  
            //this.myDataGridView.Columns[1].DefaultCellStyle.Format = "N2";  // "Psi"
            //this.myDataGridView.Columns[2].DefaultCellStyle.Format = "N4";  // "Sin^psi"
            //this.myDataGridView.Columns[3].DefaultCellStyle.Format = "N6";  // "DSpacing"
            //this.myDataGridView.Columns[4].DefaultCellStyle.Format = "N3";  // "2Theta"
            //this.myDataGridView.Columns[5].DefaultCellStyle.Format = "N3";  // "Strain*E3"
            //this.myDataGridView.Columns[6].DefaultCellStyle.Format = "N3";  // "FWHM"
            //this.myDataGridView.Columns[7].DefaultCellStyle.Format = "N2";  // "Breadth"
            //this.myDataGridView.Columns[8].DefaultCellStyle.Format = "N2";  // "Intensity"

 
            this.myDataGridView.Columns[0].DefaultCellStyle.Format = "N2";  // "Psi"
            this.myDataGridView.Columns[1].DefaultCellStyle.Format = "N4";  // "Sin^psi"
            this.myDataGridView.Columns[2].DefaultCellStyle.Format = "N6";  // "DSpacing"
            this.myDataGridView.Columns[3].DefaultCellStyle.Format = "N3";  // "2Theta"
            this.myDataGridView.Columns[4].DefaultCellStyle.Format = "N3";  // "FWHM"
            this.myDataGridView.Columns[5].DefaultCellStyle.Format = "N2";  // "Intensity"
            this.myDataGridView.Columns[6].DefaultCellStyle.Format = "N3";  // "Strain*E3"
            this.myDataGridView.Columns[7].DefaultCellStyle.Format = "N3";  // "%(d-d0)/d0"
        }

        public bool UpdateDataResultTable(double[] data)
        {
            DataRow row = myDataTable.NewRow();

            for (int i = 0; ((i < data.Length) && (i < myColumnNames.Length)); i++)
            {
                row[i] = data[i];
            }

            myDataTable.Rows.Add(row);

            // "%(d-d0)/d0"
            double d0 = (double)(myDataTable.Rows[0][2]);
            double d = data[2];
            row[myColumnNames.Length - 1] = (double)(((d - d0) / d0) * 100);

            return true;
        }

        public bool ClearDataResultTable()
        {
            myDataTable.Rows.Clear();

            return true;
        }








    }
}
