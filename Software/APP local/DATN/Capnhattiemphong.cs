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
using static System.Net.WebRequestMethods;
using System.Net.NetworkInformation;
using System.Globalization;

namespace DATN
{
    public partial class Capnhattiemphong : Form
    {
        private Timer timer;
        DateTime realTime = DateTime.Now;
        public string textID;
        private IFirebaseClient firebaseClient;
        private int Stt = 1;
        private int SL = 0;
        string txttiemphong, zcbtp1, zcbtp2, zcbtp3, zcbtp4 ;
        int wrtp1, wrtp2, wrtp3, wrtp4;
        private int canhbao = 0;
        private int canhbaoID = 0;
        private int canhbaoDate = 0;
        private int SLT;
        private int check =0;
        string displayValue;
        public Capnhattiemphong(string a)
        {
            InitializeComponent();
            InitializeFirebaseClient();
            InitializeDataGridView();
            if(a.Length > 0)
            {
                cboID.Text = a;
            }
            laysl();
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
            //storage = new FirebaseStorage("aerobic-canto-419813.appspot.com");
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
        private void InitializeDataGridView()
        {
            dataview.Columns.Add("STT", typeof(int));
            dataview.Columns.Add("ID", typeof(string));
            dataview.Columns.Add("Vaccination date", typeof(string));
            dataview.Columns.Add("Vaccine type", typeof(string));
            dataview.Columns.Add("Note", typeof(string));



            dataView.DataSource = dataview;
            dataView.Columns["STT"].Width = 39;
            dataView.Columns["ID"].Width = 95;
            dataView.Columns["Vaccination date"].Width = 120;
            dataView.Columns["Vaccine type"].Width = 123;
            dataView.Columns["Note"].Width = 100;


        }
        private void kiemtraID()
        {
            int cb = 0;

            if (cboID.Text.Trim().Length > 0)
            {

                for (int i = 0; i < SL; i++)
                {
                    string k = cboID.Items[i].ToString();
                    if (cboID.Text != k)
                    {
                        cb = 1;
                    }
                    else
                    {
                        cb = 0;
                        break;
                    }
                }
                if (cb == 1)
                {
                    errorProvider1.SetError(cboID, "Please enter the correct ID");
                    canhbaoID = 1;
                    cboID.Text = "";
                }

            }
            else
            {

                errorProvider1.SetError(cboID, "Please enter or select an ID");
                canhbaoID = 1;
            }
        }
        private async void laysl()
        {
            
            Task.Delay(500);
            List<string> dataList = new List<string>();
            string sl = await GetDataFromFirebase("SL");
            SL = int.Parse(sl);
            string matrunggian = await GetDataFromFirebase("madau");
            if (matrunggian.Length < 10)
            {
                int k;
                k = 10 - matrunggian.Length;
                for (int i = 0; i < k; i++)
                {
                    matrunggian = "0" + matrunggian;
                }
            }
            for (int i = 0; i < SL; i++)
            {
                dataList.Add(matrunggian);
                matrunggian = await GetDataFromFirebase(matrunggian + "/zmasau");
                if (matrunggian.Length < 10)
                {

                    int l;
                    l = 10 - matrunggian.Length;

                    for (int m = 0; m < l; m++)
                    {
                        matrunggian = "0" + matrunggian;
                    }
                    //label3.Text = matrunggian;
                }
            }
            cboID.Items.AddRange(dataList.ToArray());
        }

     
        private void xulyTP()
        {

            if (dtpTP.Value.Year <= DateTime.Now.Year && dtpTP.Value.Month <= DateTime.Now.Month && dtpTP.Value.Day <= DateTime.Now.Day)
            {
                txttiemphong = dtpTP.Text;
            }
            else
            {
                canhbaoDate = 1;
                errorProvider1.SetError(dtpTP, "Invalid vaccination time");
            }

        }
        private void kiemtravaccine()
        {
            if (txtVaccine.Text.Length == 0)
            {
                // errorProvider1.SetError(txtVatnuoi, "Vui lòng nhập tên vật nuôi.");
                canhbao = 1;
            }
            else
            {
                if (Regex.IsMatch(txtVaccine.Text, @"[^\p{L}\p{N}\s]"))
                {
                    errorProvider1.SetError(txtVaccine, "Please enter the pet's name correctly.");
                    txtVaccine.Clear();
                    canhbao = 1;
                }

            }
        }

        private async void laySLT()
        {
            Task.Delay(1000);
           

        }

        private async void bntCapnhat_Click(object sender, EventArgs e)
        {
            xulyTP();
            kiemtraID();
            kiemtravaccine();
            CalculateDateAfterDays(txttiemphong, 358, out zcbtp1, canhbaoDate);
            CalculateDateAfterDays(txttiemphong, 362, out zcbtp2, canhbaoDate);
            CalculateDateAfterDays(txttiemphong, 364, out zcbtp3, canhbaoDate);
            CalculateDateAfterDays(txttiemphong, 367, out zcbtp4, canhbaoDate);
            if (canhbao == 0 && canhbaoID ==0 && canhbaoDate == 0) 
            {
                string slt = await GetDataFromFirebase(cboID.Text + "/zLST/SLT");
                //label5.Text = slt;
                SLT = int.Parse(slt);
                
                textID = cboID.Text.Trim();
                //label5.Text = zcbtp1;
                wrtp1 = CompareDates(zcbtp1, realTime.ToString("dd/MM/yyyy"));
                wrtp2 = CompareDates(zcbtp2, realTime.ToString("dd/MM/yyyy"));
                wrtp3 = CompareDates(zcbtp3, realTime.ToString("dd/MM/yyyy"));
                wrtp4 = CompareDates(zcbtp4, realTime.ToString("dd/MM/yyyy"));
                
                txttiemphong = txttiemphong.Replace("/", "-");
                string textVaccine = txtVaccine.Text.Trim();
                textVaccine = textVaccine.Replace(" ", "-");
                SLT = SLT+1;
                //label5.Text = SLT.ToString();
                string textSLT = SLT.ToString();
                //for (int i = 1; i < SLT; i++)
                //{
                //    string k = i.ToString();
                //    string a = await GetDataFromFirebase(textID + "/LST/TP" + k);
                //    string[] tach = a.Split('@');
                //    if(txttiemphong == tach[0])
                //    {
                //        check = 1;
                //        break;
                //    }
                //}
                

                    string textGhichu = txtGhichu.Text.Trim();
                    textGhichu = textGhichu.Replace(" ", "-");
                    string push = txttiemphong + "@"+ textVaccine + "@" + textGhichu;
                    FirebaseResponse responseUP = await firebaseClient.SetAsync(textID + "/zLST/TP" + textSLT, push);
                    FirebaseResponse responseSLT = await firebaseClient.SetAsync(textID + "/zLST/SLT", SLT);
                FirebaseResponse responseTP = await firebaseClient.SetAsync(textID + "/datetimetp", txttiemphong);
                FirebaseResponse responseZCBTP1 = await firebaseClient.SetAsync(textID + "/zcbtp1", wrtp1);
                FirebaseResponse responseZCBTP2 = await firebaseClient.SetAsync(textID + "/zcbtp2", wrtp2);
                FirebaseResponse responseZCBTP3 = await firebaseClient.SetAsync(textID + "/zcbtp3", wrtp3);
                FirebaseResponse responseZCBTP4 = await firebaseClient.SetAsync(textID + "/zcbtp4", wrtp4);
                dataview.Rows.Add(Stt,textID,dtpTP.Text,txtVaccine.Text,txtGhichu.Text);
                Stt++;
                canhbao = 0;
                canhbaoID = 0;
                check = 0;
                txtVaccine.Text = "";
                cboID.Text = cboID.Items[0].ToString();
                txtGhichu.Text = "";
            }
            else
            {
                MessageBox.Show("Please enter correct and complete information.");
                canhbao = 0;
                canhbaoID = 0;
                canhbaoDate = 0;
                check = 0;
                zcbtp1 = "";
                zcbtp2 = "";
                zcbtp3 = "";
                zcbtp4 = "";
                txtVaccine.Text = "";
                cboID.Text = cboID.Items[0].ToString();
                txtGhichu.Text = "";
            }
            
        }

        private void  CalculateDateAfterDays(string startDateString, int days, out string resultDate1, int canhbaodate)
        {
            DateTime startDate;
            string startDateString1;
            startDateString1 = startDateString.Trim('"');
            if (canhbaodate == 0)
            {
                // Chuyển đổi chuỗi ngày sang DateTime
                if (DateTime.TryParseExact(startDateString1, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out startDate))
                {
                    // Tính toán ngày sau số ngày đã nhập
                    DateTime resultDate = startDate.AddDays(days);
                    // Trả về kết quả dưới dạng chuỗi
                    resultDate1 = resultDate.ToString("dd/MM/yyyy");


                }
                else
                {
                    resultDate1 = startDateString1;
                }
            }
            else
            {
                resultDate1 = startDateString1;
            }
           
        }

        public static int CompareDates(string dateStringA, string dateStringB)
        {
            dateStringA = dateStringA.Trim('"');
            dateStringB = dateStringB.Trim('"');
            DateTime dateA = DateTime.ParseExact(dateStringA, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime dateB = DateTime.ParseExact(dateStringB, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            if (dateA >= dateB)
            {
                return 0; // dateStringA là thời gian sau dateStringB
            }
            else
            {
                return 1; // Hai thời gian giống nhau
            }
        }
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
    }
}
