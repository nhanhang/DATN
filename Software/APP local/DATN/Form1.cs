using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using System.Windows.Forms;
using FireSharp.Config;
using FireSharp.Response;
using FireSharp.Interfaces;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Text.RegularExpressions;
using System.Web.Caching;
using System.Globalization;
using static System.Net.Mime.MediaTypeNames;
using Newtonsoft.Json;
using System.Timers;

namespace DATN
{
    public partial class Form1 : Form
    {
        private IFirebaseClient firebaseClient;
        DateTime realTime = DateTime.Now;
        string formattedTime;
        string zzcbtp1, zzcbtp2, zzcbtp3, zzcbtp4;
        int zcbtp1, zcbtp2, zcbtp3, zcbtp4 ;
        int wrtp1=0, wrtp2=0, wrtp3 = 0, wrtp4 = 0;
        string cbtp1, cbtp2, cbtp3, cbtp4;
        private int SL;
        private System.Timers.Timer timer;
        private Class1 Alldata;
        string displayValue;
        int dayStart;
        int canhbaoDate = 0;
        List<string> dataList = new List<string>();
        public Form1()
        {
            InitializeComponent();
            InitializeFirebaseClient();
           
            laysl();
            
            realTime = DateTime.Now;
            dayStart = realTime.Day;
            timer = new System.Timers.Timer();
            timer.Interval = 5 * 60 * 1000; // 5 phút
            timer.Elapsed += Timer_Elapsed;
            
            eventchange();

        }
        private DataTable dataview = new DataTable();
        private async void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            realTime = DateTime.Now;
            if(realTime.Day != dayStart)
            {
                cbtp1 = "";
                cbtp2 = "";
                cbtp3 = "";
                cbtp4 = "";
                wrtp1 = 0;
                wrtp2 = 0;
                wrtp3 = 0;
                wrtp4 = 0;
                for (int i = 0; i < SL; i++)
                {
                    string k = cboID.Items[i].ToString();
                    string Alldatapath = await GetDataFromFirebase(k);
                    ParseJson(Alldatapath);
                    CalculateDateAfterDays(Alldata.datetimetp.Replace("-","/"), 358, out zzcbtp1, canhbaoDate);
                    CalculateDateAfterDays(Alldata.datetimetp.Replace("-", "/"), 362, out zzcbtp2, canhbaoDate);
                    CalculateDateAfterDays(Alldata.datetimetp.Replace("-", "/"), 364, out zzcbtp3, canhbaoDate);
                    CalculateDateAfterDays(Alldata.datetimetp.Replace("-", "/"), 367, out zzcbtp4, canhbaoDate);
                    //string tenchu = Alldata.tenchu.Replace("-", " ");
                    //tenchu = tenchu.Trim('"');
                    //tenchu = tenchu.Replace("-", " ");
                    // string sdt = "0" + Alldata.sodienthoai.ToString();
                    //sdt = "0" + sdt;
                    //string dcphong = await GetDataFromFirebase(k + "/diachiphong");
                    //dcphong = dcphong.Trim('"');
                    //string dctoa = await GetDataFromFirebase(k + "/diachitoa");
                    //dctoa = dctoa.Trim('"');
                    //string timetp = Alldata.datetimetp.Replace("-", "/");
                    zcbtp1 = CompareDates(zzcbtp1, realTime.ToString("dd/MM/yyyy"));
                    zcbtp2 = CompareDates(zzcbtp2, realTime.ToString("dd/MM/yyyy"));
                    zcbtp3 = CompareDates(zzcbtp3, realTime.ToString("dd/MM/yyyy"));
                    zcbtp4 = CompareDates(zzcbtp4, realTime.ToString("dd/MM/yyyy"));
                   
                  
                    if (zcbtp1 == 1 || zcbtp2 == 1 || zcbtp3 == 1 || zcbtp4 == 1)
                    {
                        FirebaseResponse responseTrangthaitiem = await firebaseClient.SetAsync(k + "/tttiem", 1);
                        if (zcbtp1 == 1)
                        {
                            FirebaseResponse responseZCBTP1 = await firebaseClient.SetAsync(k+"/zcbtp1", zcbtp1);
                            if (wrtp1 == 0)
                            {
                                cbtp1 = cbtp1 + k + ":0" + Alldata.sodienthoai.ToString();
                                wrtp1 = 1;
                            }
                            else
                            {
                                cbtp1 = cbtp1 + "-" + k + ":0" + Alldata.sodienthoai.ToString();
                            }
                        }
                        if (zcbtp2 == 1)
                        {
                            FirebaseResponse responseZCBTP2 = await firebaseClient.SetAsync(k + "/zcbtp2", zcbtp2);
                            if (wrtp2 == 0)
                            {
                                cbtp2 = cbtp2 + k + ":0" + Alldata.sodienthoai.ToString();
                                wrtp2 = 1;
                            }
                            else
                            {
                                cbtp2 = cbtp2 + "-" + k + ":0" + Alldata.sodienthoai.ToString();
                            }
                        }
                        if (zcbtp3 == 1)
                        {
                            FirebaseResponse responseZCBTP3 = await firebaseClient.SetAsync(k + "/zcbtp3", zcbtp3);
                            if (wrtp3 == 0)
                            {
                                cbtp3 = cbtp3 + k + ":0" + Alldata.sodienthoai.ToString();
                                wrtp3 = 1;
                            }
                            else
                            {
                                cbtp3 = cbtp3 + "-" + k + ":0" + Alldata.sodienthoai.ToString();
                            }
                        }
                        if (zcbtp4 == 1)
                        {
                            FirebaseResponse responseZCBTP4 = await firebaseClient.SetAsync(k + "/zcbtp4", zcbtp4);
                            if (wrtp4 == 0)
                            {
                                cbtp4 = cbtp4 + k + ":0" + Alldata.sodienthoai.ToString();
                                wrtp4 = 1;
                            }
                            else
                            {
                                cbtp4 = cbtp4 + "-" + k + ":0" + Alldata.sodienthoai.ToString();
                            }
                        }
                    }
                    else
                    {
                        FirebaseResponse responseTrangthaitiem = await firebaseClient.SetAsync(k + "/tttiem", 0);
                    }
                    //dataview.Rows.Add(k, Alldata.ten, tenchu, sdt, timetp, Alldata.diachiphong + "-" + Alldata.diachitoa);


                }
                if (cbtp1 == "")
                {
                    FirebaseResponse responseZCBTP1 = await firebaseClient.SetAsync("canhbao/cbtp1", "0");
                }
                else
                {
                    FirebaseResponse responseZCBTP1 = await firebaseClient.SetAsync("canhbao/cbtp1", cbtp1);
                }
                if (cbtp2 == "")
                {
                    FirebaseResponse responseZCBTP2 = await firebaseClient.SetAsync("canhbao/cbtp2", "0");
                }
                else
                {
                    FirebaseResponse responseZCBTP2 = await firebaseClient.SetAsync("canhbao/cbtp2", cbtp2);
                }
                if (cbtp3 == "")
                {
                    FirebaseResponse responseZCBTP3 = await firebaseClient.SetAsync("canhbao/cbtp3", "0");
                }
                else
                {
                    FirebaseResponse responseZCBTP3 = await firebaseClient.SetAsync("canhbao/cbtp3", cbtp3);
                }
                if (cbtp4 == "")
                {
                    FirebaseResponse responseZCBTP4 = await firebaseClient.SetAsync("canhbao/cbtp4", "0");
                }
                else
                {
                    FirebaseResponse responseZCBTP4 = await firebaseClient.SetAsync("canhbao/cbtp4", cbtp4);
                }
                //dataView.DataSource = dataview;

            }
            // Hàm này sẽ được gọi mỗi khi timer kết thúc đếm ngược 5 phút
            // Thực hiện công việc của bạn ở đây
            //MessageBox.Show("Đã qua 5 phút!");
        }
        private async void startcanhbao()
        {
            realTime = DateTime.Now;
            

                cbtp1 = "";
                cbtp2 = "";
                cbtp3 = "";
                cbtp4 = "";
                wrtp1 = 0;
                wrtp2 = 0;
                wrtp3 = 0;
                wrtp4 = 0;
                for (int i = 0; i < SL; i++)
                {
                    string k = cboID.Items[i].ToString();
                    string Alldatapath = await GetDataFromFirebase(k);
                    ParseJson(Alldatapath);
                string dienthoai = Alldata.sodienthoai.ToString();
                    CalculateDateAfterDays(Alldata.datetimetp.Replace("-", "/"), 358, out zzcbtp1, canhbaoDate);
                    CalculateDateAfterDays(Alldata.datetimetp.Replace("-", "/"), 362, out zzcbtp2, canhbaoDate);
                    CalculateDateAfterDays(Alldata.datetimetp.Replace("-", "/"), 364, out zzcbtp3, canhbaoDate);
                    CalculateDateAfterDays(Alldata.datetimetp.Replace("-", "/"), 366, out zzcbtp4, canhbaoDate);
                    //string tenchu = Alldata.tenchu.Replace("-", " ");
                    //tenchu = tenchu.Trim('"');
                    //tenchu = tenchu.Replace("-", " ");
                    // string sdt = "0" + Alldata.sodienthoai.ToString();
                    //sdt = "0" + sdt;
                    //string dcphong = await GetDataFromFirebase(k + "/diachiphong");
                    //dcphong = dcphong.Trim('"');
                    //string dctoa = await GetDataFromFirebase(k + "/diachitoa");
                    //dctoa = dctoa.Trim('"');
                    //string timetp = Alldata.datetimetp.Replace("-", "/");
                    zcbtp1 = CompareDates(zzcbtp1, realTime.ToString("dd/MM/yyyy"));
                    zcbtp2 = CompareDates(zzcbtp2, realTime.ToString("dd/MM/yyyy"));
                    zcbtp3 = CompareDates(zzcbtp3, realTime.ToString("dd/MM/yyyy"));
                    zcbtp4 = CompareDates(zzcbtp4, realTime.ToString("dd/MM/yyyy"));
                int zgcbtp1 = zcbtp1;
                int zgcbtp2 = zcbtp2;
                int zgcbtp3 = zcbtp3;
                int zgcbtp4 = zcbtp4;
                
               
                    if (zcbtp1 == 1 || zcbtp2 == 1 || zcbtp3 == 1 || zcbtp4 == 1)
                    {
            
                    
                   
                    if (zgcbtp1 == 1)
                        {
                        
                        FirebaseResponse responseZCBTP1 = await firebaseClient.SetAsync(k + "/zcbtp1", zgcbtp1);
                       
                        }
                        if (zgcbtp2 == 1)
                        {
                        FirebaseResponse responseZCBTP2 = await firebaseClient.SetAsync(k + "/zcbtp2", zgcbtp2);
                      
                        }
                        if (zgcbtp3 == 1)
                        {
                        FirebaseResponse responseZCBTP3 = await firebaseClient.SetAsync(k + "/zcbtp3", zgcbtp3);
                     
                        }
                        if (zgcbtp4 == 1)
                        {
                        FirebaseResponse responseZCBTP4 = await firebaseClient.SetAsync(k + "/zcbtp4", zgcbtp4);
                       
                        }
                    FirebaseResponse responseTrangthaitiem = await firebaseClient.SetAsync(k + "/tttiem", 1);
                }
                //dataview.Rows.Add(k, Alldata.ten, tenchu, sdt, timetp, Alldata.diachiphong + "-" + Alldata.diachitoa);
                

            }
             
                //dataView.DataSource = dataview;

            //}
            // Hàm này sẽ được gọi mỗi khi timer kết thúc đếm ngược 5 phút
            // Thực hiện công việc của bạn ở đây
            //MessageBox.Show("Đã qua 5 phút!");
        }
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
        }
        
        private async void laysl()
        {
            InitializeDataGridView();
            
            
            string sl = await GetDataFromFirebase("SL");
           SL=int.Parse(sl);
            string matrunggian= await GetDataFromFirebase("madau");
            if (matrunggian.Length < 10)
            {
                
                int k;
                k = 10 - matrunggian.Length;
                
                for (int i = 0; i < k; i++)
                {
                    matrunggian = "0" + matrunggian;
                }
                //label3.Text = matrunggian;
            }
            for(int i = 0; i < SL; i++)
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
            startcanhbao();
            //Task.Delay(2000);
            LoadDataToDataGridView();
           

        }
        //private Dictionary<string, string> cache = new Dictionary<string, string>();

        private async Task<string> GetDataFromFirebase(string tagAddress)
        {
            //if (cache.ContainsKey(tagAddress))
            //{
            //    return cache[tagAddress];
            //}
            //else
            //{
            //    try
            //    {
            //        FirebaseResponse response = await firebaseClient.GetAsync(tagAddress);
            //        string data = response.Body;
            //        cache[tagAddress] = data; // Lưu kết quả vào cache
            //        return data;
            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show("Lỗi khi lấy dữ liệu từ Firebase: " + ex.Message);
            //        return null;
            //    }
            //}
            try
            {
                FirebaseResponse response = await firebaseClient.GetAsync(tagAddress);
                return response.Body;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error getting data from Firebase: " + ex.Message);
                return null;
            }
        }
        private void InitializeDataGridView()
        {
            dataview.Columns.Add("ID", typeof(string));
            dataview.Columns.Add("Pet name", typeof(string));
            dataview.Columns.Add("Pet owner's name", typeof(string));
            dataview.Columns.Add("Phone number", typeof(string));
            dataview.Columns.Add("Vaccination date", typeof(string));
            dataview.Columns.Add("Address", typeof(string));
            
            dataView.DataSource = dataview;
            dataView.Columns["ID"].Width = 100;
            dataView.Columns["Pet name"].Width = 125;
            dataView.Columns["Pet owner's name"].Width = 150;
            dataView.Columns["Phone number"].Width = 150;
            dataView.Columns["Vaccination date"].Width = 130;
            dataView.Columns["Address"].Width = 70;
        }
        private async void LoadDataToDataGridView()
        {
            

            for (int i = 0; i < SL; i++)
            {
                
                string k = cboID.Items[i].ToString();
                string Alldatapath = await GetDataFromFirebase(k);
                ParseJson(Alldatapath);
                //string ten = await GetDataFromFirebase(k + "/ten");
                //ten = ten.Trim('"');
                string tenvat = Alldata.ten.Replace("-", " ");
                string tenchu = Alldata.tenchu.Replace("-", " ");
                //tenchu = tenchu.Trim('"');
                //tenchu = tenchu.Replace("-", " ");
                string sdt = "0"+ Alldata.sodienthoai.ToString();
                //sdt = "0" + sdt;
                string dcphong = Alldata.diachiphong.Trim('"');
                //dcphong = dcphong.Trim('"');
                string dctoa = Alldata.diachitoa.Trim('"');
                //dctoa = dctoa.Trim('"');
                string timetp = Alldata.datetimetp.Replace("-","/");

                //timetp = timetp.Trim('"');
                //timetp = timetp.Replace("-", "/");
                //zcbtp1 = await GetDataFromFirebase(k + "/zcbtp1");
                //zcbtp2 = await GetDataFromFirebase(k + "/zcbtp2");
                //zcbtp3 = await GetDataFromFirebase(k + "/zcbtp3");
                //zcbtp4 = await GetDataFromFirebase(k + "/zcbtp4");
                zcbtp1=Alldata.zcbtp1;
                zcbtp2 = Alldata.zcbtp2;
                zcbtp3 = Alldata.zcbtp3;
                zcbtp4 = Alldata.zcbtp4;
                if (zcbtp1 == 1 || zcbtp2 == 1 || zcbtp3 == 1 || zcbtp4 == 1)
                {
                    FirebaseResponse responseTrangthaitiem = await firebaseClient.SetAsync(k + "/tttiem", 1);
                    if (zcbtp1 == 1)
                    {

                        if (wrtp1 == 0)
                        {
                            cbtp1 = cbtp1 + k + ":0" + Alldata.sodienthoai.ToString();
                            wrtp1 = 1;
                        }
                        else
                        {
                            cbtp1 = cbtp1 + "-" + k + ":0" + Alldata.sodienthoai.ToString();
                        }
                    }
                    if (zcbtp2 == 1)
                    {
                        if (wrtp2 == 0)
                        {
                            cbtp2 = cbtp2 + k + ":0" + Alldata.sodienthoai.ToString();
                            wrtp2 = 1;
                        }
                        else
                        {
                            cbtp2 = cbtp2 + "-" + k + ":0" + Alldata.sodienthoai.ToString();
                        }
                    }
                    if (zcbtp3 == 1)
                    {
                        if (wrtp3 == 0)
                        {
                            cbtp3 = cbtp3 + k + ":0" + Alldata.sodienthoai.ToString();
                            wrtp3 = 1;
                        }
                        else
                        {
                            cbtp3 = cbtp3 + "-" + k + ":0" + Alldata.sodienthoai.ToString();
                        }
                    }
                    if (zcbtp4 == 1)
                    {
                        if (wrtp4 == 0)
                        {
                            cbtp4 = cbtp4 + k + ":0" + Alldata.sodienthoai.ToString();
                            wrtp4 = 1;
                        }
                        else
                        {
                            cbtp4 = cbtp4 + "-" + k + ":0" + Alldata.sodienthoai.ToString();
                        }
                    }
                }
                else
                {
                    FirebaseResponse responseTrangthaitiem = await firebaseClient.SetAsync(k + "/tttiem", 0);
                }
              
                //label2.Text = dctoa;
                dataview.Rows.Add(k,tenvat, tenchu,sdt, timetp, dcphong + "-"+ dctoa);
                tenvat = "";
               
            }
            dataView.DataSource = dataview;
            if (cbtp1 == "")
            {
                FirebaseResponse responseZCBTP1 = await firebaseClient.SetAsync("canhbao/cbtp1", "0");
            }
            else
            {
                FirebaseResponse responseZCBTP1 = await firebaseClient.SetAsync("canhbao/cbtp1", cbtp1);
            }
            if (cbtp2 == "")
            {
                FirebaseResponse responseZCBTP2 = await firebaseClient.SetAsync("canhbao/cbtp2", "0");
            }
            else
            {
                FirebaseResponse responseZCBTP2 = await firebaseClient.SetAsync("canhbao/cbtp2", cbtp2);
            }
            if (cbtp3 == "")
            {
                FirebaseResponse responseZCBTP3 = await firebaseClient.SetAsync("canhbao/cbtp3", "0");
            }
            else
            {
                FirebaseResponse responseZCBTP3 = await firebaseClient.SetAsync("canhbao/cbtp3", cbtp3);
            }
            if (cbtp4 == "")
            {
                FirebaseResponse responseZCBTP4 = await firebaseClient.SetAsync("canhbao/cbtp4", "0");
            }
            else
            {
                FirebaseResponse responseZCBTP4 = await firebaseClient.SetAsync("canhbao/cbtp4", cbtp4);
            }

            //FirebaseResponse responseZCBTP2 = await firebaseClient.SetAsync("canhbao/cbtp2", cbtp2);
            //FirebaseResponse responseZCBTP3 = await firebaseClient.SetAsync("canhbao/cbtp3", cbtp3);
            //FirebaseResponse responseZCBTP4 = await firebaseClient.SetAsync("canhbao/cbtp4", cbtp4);
        }

        private void bntT_Click(object sender, EventArgs e)
        {
            this.Hide();
            Them frm3 = new Them(dataList);
            frm3.ShowDialog();
            frm3 = null;
            this.Show();

        }

        private void btnChitiet_Click(object sender, EventArgs e)
        {
            if (cboID.Text == "")
            {
                MessageBox.Show("Please select or enter an ID.");
            }
            else
            {
                int cb = 0;
                for(int i = 0; i < SL; i++)
                {
                   string k = cboID.Items[i].ToString();
                   // MessageBox.Show($"{cboID.Items[i].ToString()}");
                    if(cboID.Text != k)
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
                    MessageBox.Show("Please enter the correct ID or select an ID.");
                    cb = 0;
                }
                else
                {
                    this.Hide();
                    Xem frm2 = new Xem(cboID.Text);
                    //frm2.textID = cboID.Text;
                    frm2.ShowDialog();
                    frm2 = null;
                    this.Show();
                }

            }
        }

        private void bntCS_Click(object sender, EventArgs e)
        {

            if (cboID.Text.Trim().Length > 0)
            {
                int cb = 0;
                for (int i = 0; i < SL; i++)
                {
                    string k = cboID.Items[i].ToString();
                   // MessageBox.Show($"{cboID.Items[i].ToString()}");
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
                    MessageBox.Show("Please enter the correct ID or select an ID.");
                    cb = 0;
                }
                else
                {
                    this.Hide();
                    Update frm4 = new Update(cboID.Text);
                    //frm4.textID = ;
                    //label2.Text = cboID.Text;
                    frm4.ShowDialog();
                    frm4 = null;
                    this.Show();
                }
            }
            else
            {
                this.Hide();
                Update frm4 = new Update(cboID.Text);
                //frm4.textID = cboID.Text;
                
                frm4.ShowDialog();
                frm4 = null;
                this.Show();
            }
        }

        private void bntX_Click(object sender, EventArgs e)
        {
            if (cboID.Text.Trim().Length > 0)
            {
                int cb = 0;
                for (int i = 0; i < SL; i++)
                {
                    string k = cboID.Items[i].ToString();
                    //MessageBox.Show($"{cboID.Items[i].ToString()}");
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
                    MessageBox.Show("Please enter the correct ID or select an ID.");
                    cb = 0;
                }
                else
                {
                    this.Hide();
                    Xoa frm5 = new Xoa();
                    frm5.textID = cboID.Text;
                    frm5.ShowDialog();
                    frm5 = null;
                    this.Show();
                }
            }
            else
            {
                this.Hide();
                Xoa frm5 = new Xoa();
                frm5.textID = cboID.Text;
                frm5.ShowDialog();
                frm5 = null;
                this.Show();
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
        private async void button1_Click(object sender, EventArgs e)
        {
            cboID.Text = "";
            txtTimkiem.Text = "";
            dataView.DataSource = null;
            dataView.Columns.Clear();
            dataView.Rows.Clear();
            dataview.Clear();
            dataView.DataSource = dataview;
            //LoadDataToDataGridView();
            dataView.Columns["ID"].Width = 100;
            dataView.Columns["Pet name"].Width = 125;
            dataView.Columns["Pet owner's name"].Width = 150;
            dataView.Columns["Phone number"].Width = 150;
            dataView.Columns["Vaccination date"].Width = 130;
            dataView.Columns["Address"].Width = 70;
            cboID.DataSource = null;
            cboID.Items.Clear();
            cbtp1 = "";
            cbtp2 = "";
            cbtp3 = "";
            cbtp4 = "";
            wrtp1 = 0;
            wrtp2 = 0;
            wrtp3 = 0;
                wrtp4 = 0;
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
                //label3.Text = matrunggian;
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
            for (int i = 0; i < SL; i++)
            {
                string k = cboID.Items[i].ToString();
                string Alldatapath = await GetDataFromFirebase(k);
                ParseJson(Alldatapath);
                string tenchu = Alldata.tenchu.Replace("-", " ");
                //tenchu = tenchu.Trim('"');
                //tenchu = tenchu.Replace("-", " ");
                string sdt = "0" + Alldata.sodienthoai.ToString();
                //sdt = "0" + sdt;
                //string dcphong = await GetDataFromFirebase(k + "/diachiphong");
                //dcphong = dcphong.Trim('"');
                //string dctoa = await GetDataFromFirebase(k + "/diachitoa");
                //dctoa = dctoa.Trim('"');
                string timetp = Alldata.datetimetp.Replace("-", "/");
                zcbtp1 = Alldata.zcbtp1;
                zcbtp2 = Alldata.zcbtp2;
                zcbtp3 = Alldata.zcbtp3;
                zcbtp4 = Alldata.zcbtp4;

                if (zcbtp1 == 1 || zcbtp2 == 1 || zcbtp3 == 1 || zcbtp4 == 1)
                {
                    FirebaseResponse responseTrangthaitiem = await firebaseClient.SetAsync(k + "/tttiem", 1);
                    if (zcbtp1 == 1)
                    {

                        if (wrtp1 == 0)
                        {
                            cbtp1 = cbtp1 + k + ":0" + Alldata.sodienthoai.ToString();
                            wrtp1 = 1;
                        }
                        else
                        {
                            cbtp1 = cbtp1 + "-" + k + ":0" + Alldata.sodienthoai.ToString();
                        }
                    }
                    if (zcbtp2 == 1)
                    {
                        if (wrtp2 == 0)
                        {
                            cbtp2 = cbtp2 + k + ":0" + Alldata.sodienthoai.ToString();
                            wrtp2 = 1;
                        }
                        else
                        {
                            cbtp2 = cbtp2 + "-" + k + ":0" + Alldata.sodienthoai.ToString();
                        }
                    }
                    if (zcbtp3 == 1)
                    {
                        if (wrtp3 == 0)
                        {
                            cbtp3 = cbtp3 + k + ":0" + Alldata.sodienthoai.ToString();
                            wrtp3 = 1;
                        }
                        else
                        {
                            cbtp3 = cbtp3 + "-" + k + ":0" + Alldata.sodienthoai.ToString();
                        }
                    }
                    if (zcbtp4 == 1)
                    {
                        if (wrtp4 == 0)
                        {
                            cbtp4 = cbtp4 + k + ":0" + Alldata.sodienthoai.ToString();
                            wrtp4 = 1;
                        }
                        else
                        {
                            cbtp4 = cbtp4 + "-" + k + ":0" + Alldata.sodienthoai.ToString();
                        }
                    }
                }
                else
                {
                    FirebaseResponse responseTrangthaitiem = await firebaseClient.SetAsync(k + "/tttiem", 0);
                }
               
                dataview.Rows.Add(k, Alldata.ten, tenchu, sdt, timetp, Alldata.diachiphong + "-" + Alldata.diachitoa);


            }

            if (cbtp1 == "")
            {
                FirebaseResponse responseZCBTP1 = await firebaseClient.SetAsync("canhbao/cbtp1", "0");
            }
            else
            {
                FirebaseResponse responseZCBTP1 = await firebaseClient.SetAsync("canhbao/cbtp1", cbtp1);
            }
            if (cbtp2 == "")
            {
                FirebaseResponse responseZCBTP2 = await firebaseClient.SetAsync("canhbao/cbtp2", "0");
            }
            else
            {
                FirebaseResponse responseZCBTP2 = await firebaseClient.SetAsync("canhbao/cbtp2", cbtp2);
            }
            if (cbtp3 == "")
            {
                FirebaseResponse responseZCBTP3 = await firebaseClient.SetAsync("canhbao/cbtp3", "0");
            }
            else
            {
                FirebaseResponse responseZCBTP3 = await firebaseClient.SetAsync("canhbao/cbtp3", cbtp3);
            }
            if (cbtp4 == "")
            {
                FirebaseResponse responseZCBTP4 = await firebaseClient.SetAsync("canhbao/cbtp4", "0");
            }
            else
            {
                FirebaseResponse responseZCBTP4 = await firebaseClient.SetAsync("canhbao/cbtp4", cbtp4);
            }
            dataView.DataSource = dataview;
  

        }
        DataTable originalDataTable = null;
        private void bntTimkiem_Click(object sender, EventArgs e)
        {
            
            string keyword = txtTimkiem.Text.Trim().ToLower();
            if (originalDataTable == null)
            {
                
                    originalDataTable = ((DataTable)dataView.DataSource).Copy(); // Copy dữ liệu để giữ nguyên ban đầu
                
            }
            DataTable filteredDataTable = originalDataTable.Clone();
            foreach (DataRow originalRow in originalDataTable.Rows)
            {
                bool containsKeyword = false; // Biến này sẽ đánh dấu xem dòng có chứa từ khóa hay không

                foreach (object item in originalRow.ItemArray)
                {
                    if (item != null && item.ToString().ToLower().Contains(keyword))
                    {
                        // Nếu tìm thấy từ khóa trong bất kỳ cột nào của dòng, đặt biến containsKeyword thành true
                        containsKeyword = true;
                        break; // Đã tìm thấy từ khóa, không cần duyệt qua các cột khác của dòng
                    }
                }

                // Nếu dòng chứa từ khóa, thêm dòng này vào bảng lọc
                if (containsKeyword)
                {
                    filteredDataTable.ImportRow(originalRow);
                }
            }
            dataView.DataSource = filteredDataTable;

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

        private void ParseJson(string json)
        {
            Alldata = JsonConvert.DeserializeObject<Class1>(json);
            Task.Delay(500);
            // Gán giá trị cho các biến


            // Hiển thị các giá trị trong MessageBox (tùy chọn)
           // MessageBox.Show($"Tên: {Alldata.Ten}\nGiống: {Alldata.Giong}\nMàu sắc: {ananimalData.Mausac}\nSố điện thoại: {ananimalData.ZLST.SLT}");
        }
        private void CalculateDateAfterDays(string startDateString, int days, out string resultDate1, int canhbaodate)
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

    }

   
}
