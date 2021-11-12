﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using xNet;
using Newtonsoft.Json;
using System.Net;
using System.Collections.Specialized;
using System.Text.RegularExpressions;

namespace DesktopApp_CreateCode_Winform
{
    public partial class FrmIndex : Form
    {
        

        #region Properties
        HttpRequest httpRequest;
        string url = "";
        ItemCustomFile.AllFile itemCustomFile = new ItemCustomFile.AllFile();
        List<ItemCustomList.DataCode> listCode = new List<ItemCustomList.DataCode>();
        bool checkSort = false;
        #endregion
        public FrmIndex()
        {
            InitializeComponent();
            httpRequest = new HttpRequest();
            url = itemCustomFile.readFile();
            loadList();
            btnNhapUrl.Visible = false;
            btnUpdateNhom.Visible = false;
            
        }

        private bool check { set; get; }

        private void loadList()
        {
            listView.Controls.Clear();
            listCode.Clear();
            string link = url + "/list";
            string getUrl = GET(link);
            string json = getUrl;
            string[] arrStr = json.Split('}');
            if (arrStr.Length > 0)
            {
                for (int i = 0; i < arrStr.Length - 1; i++)
                {
                    String jsonData = convertString(arrStr[i]);
                    ItemCustomList.PanelCustom panel = new ItemCustomList.PanelCustom(jsonData);
                    ItemCustomList.DataCode dataCode = new ItemCustomList.DataCode(jsonData);
                    listView.Controls.Add(panel.create());
                    listCode.Add(dataCode);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

            loadList();
        }
        private String convertString(String arrStr)
        {
            String str = "";
            str = arrStr.Substring(1, arrStr.Length - 1) + "}";
            return str;

        }

        #region Methods
        /// Lấy mã HTML từ address truyền vào
        public string GET(string address)
        {


            WebRequest request = WebRequest.Create(address);
            request.Credentials = CredentialCache.DefaultNetworkCredentials;
            try
            {
                WebResponse response = request.GetResponse();

                if (((HttpWebResponse)response).StatusDescription == "OK")
                {
                    using (Stream data = response.GetResponseStream())
                    {
                        StreamReader reader = new StreamReader(data);
                        string res = reader.ReadToEnd();
                        return res;
                    }
                }
                else
                {
                    return "";
                }
            } catch (Exception)
            {
                MessageBox.Show("Kiểm tra lại");
                return "";
            }
            
        }
        #endregion
        private void button1_Click(object sender, EventArgs e)
        {
            btnNhapUrl.Visible = true;
            btnUpdateNhom.Visible = true;
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnTaoMoi_Click(object sender, EventArgs e)
        {
            frmAdd frmAdd = new frmAdd(false);
            frmAdd.ShowDialog();
        }


        private void btnNhapUrl_Click(object sender, EventArgs e)
        {
            frmSetting frmSetting = new frmSetting();
            btnNhapUrl.Visible = false;
            btnUpdateNhom.Visible = false;
            frmSetting.Show();
        }

        private void btnUpdateNhom_Click(object sender, EventArgs e)
        {
            frmUpdateTeamcs frmUpdateTeamcs = new frmUpdateTeamcs();
            btnNhapUrl.Visible = false;
            btnUpdateNhom.Visible = false;
            frmUpdateTeamcs.Show();
        }

        public bool checkDigit(string s)
        {
            for (int i = 0; i < s.Length; i++)
            {
                if (char.IsDigit(s[i]) == true)
                    return true;
            }
            return false;
        }
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            listView.Controls.Clear();
            string link = url + "/list";
            string getUrl = GET(link);
            string json = getUrl;
            string[] arrStr = json.Split('}');


            for (int i = 0; i < arrStr.Length - 1; i++)
            {

                String jsonData = convertString(arrStr[i]);
                ItemCustomList.PanelCustom panel = new ItemCustomList.PanelCustom(jsonData);
                ItemCustomList.DataCode dataCode = new ItemCustomList.DataCode(jsonData);
                
                if(dataCode.id.Contains(txtSearch.Text) || dataCode.size.Contains(txtSearch.Text))
                {
                    listView.Controls.Add(panel.create());
                }
            }

        }
        
        void Sort()
        {
            listView.Controls.Clear();
            // Mảng data có name là listCode
            // sap xep
            if (!checkSort)
            {
                for (int i = 0; i < listCode.Count; i++)
                {
                    for (int j = i + 1; j < listCode.Count; j++)
                    {
                        if (System.Convert.ToInt32(listCode[i].id[0]) > System.Convert.ToInt32(listCode[j].id[0]))
                        {
                            ItemCustomList.DataCode x = listCode[i];
                            listCode[i] = listCode[j];
                            listCode[j] = x;
                        }
                    }

                }
            } else
            {
                for (int i = 0; i < listCode.Count; i++)
                {
                    for (int j = i + 1; j < listCode.Count; j++)
                    {
                        if (System.Convert.ToInt32(listCode[i].id[0]) < System.Convert.ToInt32(listCode[j].id[0]))
                        {
                            ItemCustomList.DataCode x = listCode[i];
                            listCode[i] = listCode[j];
                            listCode[j] = x;
                        }
                    }

                }
            }
            checkSort = !checkSort;
            // Add tung phan tu vao listview
            for (int i = 0; i < listCode.Count; i++)
            {
                Console.WriteLine(listCode[i].id);
                ItemCustomList.PanelCustom panel = new ItemCustomList.PanelCustom(listCode[i]);
                listView.Controls.Add(panel.create());
            }
        }

        private void btnSort_Click(object sender, EventArgs e)
        {
            Sort();
        }
    }
}
