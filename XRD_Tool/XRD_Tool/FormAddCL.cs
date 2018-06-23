using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XRD_Tool
{
    public partial class FormAddCL : Form
    {
        IniEdit iniE1 = new IniEdit();
        IniEdit iniE = new IniEdit();
        int mode;//0:新增   1：删除  2：修改
        string name;
        public FormAddCL(int _flag)
        {
            InitializeComponent();
            iniE.IniEditConfig("数据字典.ini");
            mode = _flag;
        }
        public FormAddCL(int _flag,string _name)
        {
            InitializeComponent();
            iniE.IniEditConfig("数据字典.ini");
            iniE1.IniEditConfig("参数设置.ini");
            mode = _flag;
            name = _name;
        }

        /// <summary>
        /// 校验数据有效性
        /// </summary>
        /// <returns></returns>
        public bool CheckValue()
        {
            if (textBoxV.Text == "")
                return false;
            if (textBoxE.Text == "")
                return false;
            if (textBoxName.Text == "")
                return false;
            string[] strname = iniE.GetSections();
            if (strname.Length>0)
            {
                foreach (string str in strname)
                {
                    if (str==textBoxName.Text)
                    {
                        MessageBox.Show("该项已经存在，请重新确认");
                        return false;
                    }

                }
            }

            return true;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                switch (mode)
                {
                    case 0:
                        if (CheckValue())
                        {
                            iniE.INIWriteItems(textBoxName.Text, "E=" + textBoxE.Text + "\0V=" + textBoxV.Text);
                            this.Close();
                        }
                        break;
                    case 1:
                        iniE.DeleteSection(textBoxName.Text);
                        this.Close();
                        break;
                    case 2:
                        iniE.SaveValueWithKey(name, "E", textBoxE.Text);
                        iniE.SaveValueWithKey(name, "V", textBoxV.Text);
                        iniE1.SaveValueWithKey(name, "E", textBoxE.Text);
                        iniE1.SaveValueWithKey(name, "V", textBoxV.Text);
                        this.Close();
                        break;
                    default:
                        break;
                }
                
                
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// 保存数据字典
        /// </summary>
        public void SaveZD()
        {
 
        }
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void FormAddCL_Load(object sender, EventArgs e)
        {
            try
            {
                switch (mode)
                {
                    case 0:
                        this.Text = "新增";
                        break;
                    case 1:
                        this.Text = "删除";
                        textBoxName.Text = name;
                        textBoxE.Text = iniE.GetVlueWithKey(name, "E");
                        textBoxV.Text = iniE.GetVlueWithKey(name, "v");
                        textBoxName.ReadOnly = true;
                        textBoxE.ReadOnly = true;
                        textBoxV.ReadOnly = true;
                        break;
                    case 2:
                        this.Text = "修改";
                        textBoxName.Text = name;
                        textBoxName.ReadOnly = true;
                        textBoxE.Text = iniE.GetVlueWithKey(name, "E");
                        textBoxV.Text = iniE.GetVlueWithKey(name, "v");
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
