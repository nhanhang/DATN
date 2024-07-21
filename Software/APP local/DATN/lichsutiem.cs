using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FireSharp;
using FireSharp.Config;
using FireSharp.Response;
using FireSharp.Interfaces;
using System.Xml.Linq;
using Firebase.Storage;
using System.IO;
using System.Net;
using System.Security.Policy;
using System.Net.Http;
using System.Text.RegularExpressions;

namespace DATN
{
    public partial class lichsutiem : Form
    {
        private IFirebaseClient firebaseClient;
        public string txtID;
        private int SLT = 0;
        private int Stt = 1;
        public lichsutiem(string a)
        {
            InitializeComponent();
            //label1.Text = a;
            txtID = a;
            InitializeFirebaseClient();
            InitializeDataGridView();
        
            layData();
            eventchange();
        }
        private DataTable dataview = new DataTable();
        private void InitializeFirebaseClient()
        {
            IFirebaseConfig config = new FirebaseConfig
            {
                AuthSecret = "V71B1ZNnh1L0Swmcne0OjWlFtSUF22ee24jhwQMQ",
                BasePath = "https://aerobic-canto-419813-default-rtdb.firebaseio.com/"
            };
            //IFirebaseConfig config = new FirebaseConfig
            //{
            //    AuthSecret = "\tUvfEy3TD74dpzNQEZIATUHVj2iUrdl3I0T2WduWm",
            //    BasePath = "https://tiem-phong-default-rtdb.asia-southeast1.firebasedatabase.app/"
            //};
            firebaseClient = new FireSharp.FirebaseClient(config);
            // storage = new FirebaseStorage("aerobic-canto-419813.appspot.com");
        }
        private void InitializeDataGridView()
        {
            dataview.Columns.Add("STT", typeof(int));
            dataview.Columns.Add("ID", typeof(string));
            dataview.Columns.Add("Vaccination date", typeof(string));
            dataview.Columns.Add("Vaccine type", typeof(string));
            dataview.Columns.Add("Note", typeof(string));


            dataView.DataSource = dataview;
            dataView.Columns["STT"].Width = 39;
            dataView.Columns["ID"].Width = 100;
            dataView.Columns["Vaccination date"].Width = 120;
            dataView.Columns["Vaccine type"].Width = 123;
            dataView.Columns["Note"].Width = 146;

        }
        private async Task<string> GetDataFromFirebase(string tagAddress)
        {
            try
            {
                FirebaseResponse response = await firebaseClient.GetAsync(tagAddress);
                return response.Body;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error retrieving data from Firebase:" + ex.Message);
                return null;
            }
        }
        string displayValue;
        private async void eventchange()
        {
            //await Task.Delay(500);

            firebaseClient.OnChangeGetAsync<string>("display", async (sender, args) =>
            {
                if (args != null)
                {
                    if (args != "0")
                    {
                        displayValue = args;
                        if (displayValue.Length < 10)
                        {
                            int l;
                            l = 10 - displayValue.Length;

                            for (int m = 0; m < l; m++)
                            {
                                displayValue = "0" + displayValue;
                            }
                        }
                        FirebaseResponse responseDisplay = await firebaseClient.SetAsync("display", "0");
                        //this.Hide();
                        Xem frm9 = new Xem(displayValue);
                        displayValue = "0";
                        frm9.ShowDialog();
                        frm9 = null;
                        //this.Show();

                    }
                }
            });
        }
            private async void layData()
        {
            //Task.Delay(1000);
            string slt = await GetDataFromFirebase(txtID + "/zLST/SLT");
            SLT = int.Parse(slt);
            if (SLT > 0)
            {
                for (int i = 0; i < SLT; i++)
                {
                    string k = (i+1).ToString();
                    string getdata = await GetDataFromFirebase(txtID + "/zLST/TP"+k);
                    getdata = getdata.Trim('"');
                    string[] tach = getdata.Split('@');
                    string timetp = tach[0];
                    string vaccine = tach[1];
                    string ghichu = tach[2];
                    timetp = timetp.Replace("-", "/");
                    vaccine = vaccine.Replace("-", " ");
                    ghichu = ghichu.Replace("-", " ");
                    dataview.Rows.Add(Stt, txtID, timetp, vaccine, ghichu);
                    Stt++;
                }
            }
        }
    }
}
