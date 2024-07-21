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
using Newtonsoft.Json;

namespace DATN
{
    public partial class Xem : Form
    {
        private IFirebaseClient firebaseClient;
        private string tagname;
        private int SL = 0;
        private FirebaseStorage storage;
        DateTime realTime = DateTime.Now;
        private  string textID;
        private Class1 Alldata1;
        public Xem(string textID)
        {
            InitializeComponent();
            InitializeFirebaseClient();
            this.textID = textID;
            int labelX = (groupBox2.Width - label18.Width) / 2;
            label18.Location = new Point(labelX, label18.Location.Y);
             labelX = (groupBox2.Width - label19.Width) / 2;
            label19.Location = new Point(labelX, label19.Location.Y);
             labelX = (groupBox2.Width - label32.Width) / 2;
            label32.Location = new Point(labelX, label32.Location.Y);
            //uphinhanh();
            taidulieu();
            eventchange();
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
            storage = new FirebaseStorage("aerobic-canto-419813.appspot.com");
        }
        private Dictionary<string, string> cache = new Dictionary<string, string>();
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
                AB: 
                FirebaseResponse response = await firebaseClient.GetAsync(tagAddress);
                if (response.Body != null)
                {
                    // Xử lý dữ liệu ở đây
                    return response.Body;
                }
                else
                {
                    // Xử lý khi dữ liệu trả về là null
                    goto AB;
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error retrieving data from Firebase:" + ex.Message);
                return null;
            }
        }
        private async void uphinhanh(string a)
        {
            //string hinhanh = await GetDataFromFirebase(textID + "/ztenhinhanh");
            //a = a.Trim('"');
         
            DownloadAndDisplayImage(a);
        }
        private async void DownloadAndDisplayImage(string imageName)
        {
            try
            {
                // Lấy URL tải xuống của hình ảnh từ Firebase Storage
                var imageUrl = await storage.Child(imageName).GetDownloadUrlAsync();

                // Tải hình ảnh từ URL
                using (var webClient = new WebClient())
                {
                    byte[] imageData = await webClient.DownloadDataTaskAsync(imageUrl);

                    // Hiển thị hình ảnh trên PictureBox
                    pictureBox1.Image = Image.FromStream(new System.IO.MemoryStream(imageData));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error downloading and displaying image: {ex.Message}");
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
        private async void taidulieu()
        {
            
            string Alldatapath = await GetDataFromFirebase(textID);
            ParseJson(Alldatapath);
            uphinhanh(Alldata1.ztenhinhanh);

            //string txtten = await GetDataFromFirebase(textID + "/ten");
            //txtten = txtten.Trim('"');
            //string txttenchu;
            //txttenchu = txttenchu.Trim('"');
            //txttenchu = txttenchu.Replace("-", " ");
            //string txtnamsinh = await GetDataFromFirebase(textID + "/znamsinh");
            //txtnamsinh = txtnamsinh.Trim('"');
            //txtnamsinh = txtnamsinh.Replace("-", "/");
            //string timetp = await GetDataFromFirebase(textID + "/datetimetp");
            //timetp = timetp.Trim('"');
            //timetp = timetp.Replace("-", "/");
            //string txtcannang = await GetDataFromFirebase(textID + "/cannang");
            //txtcannang = txtcannang.Trim('"');
            //string txtchieucao = await GetDataFromFirebase(textID + "/chieucao");
            //txtchieucao = txtchieucao.Trim('"');
            //await Task.Delay(500);
            //string txtdcp = await GetDataFromFirebase(textID + "/diachiphong");
            //txtdcp = txtdcp.Trim('"');
            //string txtdiachitoa = await GetDataFromFirebase(textID + "/diachitoa");
            //txtdiachitoa = txtdiachitoa.Trim('"');
            //string txttotnghiep = await GetDataFromFirebase(textID + "/zloaitotnghiep");
            //txttotnghiep = txttotnghiep.Trim('"');
            //txttotnghiep = txttotnghiep.Replace("-", " ");
            //string txtgiong = await GetDataFromFirebase(textID + "/giong");
            //txtgiong = txtgiong.Trim('"');
            //txtgiong = txtgiong.Replace("-", " ");
            //string txtmausac = await GetDataFromFirebase(textID + "/mausac");
            //txtmausac = txtmausac.Trim('"');
            //string txtsdt = await GetDataFromFirebase(textID + "/sodienthoai");
            //txtsdt = "0" + txtsdt;
            //string txttinbiet = await GetDataFromFirebase(textID + "/tinhbiet");
            //txttinbiet = txttinbiet.Trim('"');
            //string txtxx = await GetDataFromFirebase(textID + "/xuatxu");
            //txtxx = txtxx.Trim('"');
            //txtxx = txtxx.Replace("-", " ");
            string txtdinhvi;
            if (Alldata1.zdinhvi == 0)
            {
                txtdinhvi = "Not registered";
            }
            else
            {
                txtdinhvi = "Registered";
            }

            txtTenchu.Text = Alldata1.tenchu.Replace("-"," ");
            int labelX = (groupBox2.Width - txtTenchu.Width) / 2;
           // int labelY = (groupBox1.Height - label1.Height) / 2;

            // Cập nhật vị trí của Label
            txtTenchu.Location = new Point(labelX, txtTenchu.Location.Y);
            txtTen.Text = Alldata1.ten;
            txtNamsinh.Text = Alldata1.znamsinh.Replace("-","/");
            txtHuanluyen.Text = Alldata1.zloaitotnghiep;
            txtChieucao.Text = Alldata1.chieucao.ToString();
            txtCannang.Text = Alldata1.cannang.ToString();
            txtMausac.Text = Alldata1.mausac.Replace("-"," ");
            txtXuaxu.Text = Alldata1.xuatxu;
            txtTiemphong.Text = Alldata1.datetimetp.Replace("-","/");
            txtTinhbiet.Text = Alldata1.tinhbiet;
            txtID.Text = textID;
            txtGiong.Text = Alldata1.giong;
            txtSDT.Text = "0"+Alldata1.sodienthoai.ToString();
            txtDinhvi.Text = txtdinhvi;
            txtDC.Text = Alldata1.diachiphong + "-" + Alldata1.diachitoa + "-" + "Time City Apartment";
            labelX = (groupBox2.Width -txtSDT.Width) / 2;
            txtSDT.Location = new Point(labelX, txtSDT.Location.Y);
            labelX = (groupBox2.Width - txtDC.Width) / 2;
            txtDC.Location = new Point(labelX, txtDC.Location.Y);

        }

        private void bntVitri_Click(object sender, EventArgs e)
        {
            if (Alldata1.zdinhvi == 0)
            {
                MessageBox.Show("ID has not registered for location, please register to track location");
            }
            else
            {
                this.Hide();
                Bando frm6 = new Bando(textID);
                //frm6.textID = textID;
                //frm6.tagLatitude = textID+ "/vitriX";
                //frm6.tagLongitude = textID + "/vitriY";
                frm6.ShowDialog();
                frm6 = null;
                this.Show();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            lichsutiem frm8 = new lichsutiem(textID);
            //frm8.txtID = textID;
            frm8.ShowDialog();
            frm8 = null; 
            this.Show();
        }
        private void ParseJson(string json)
        {
            Alldata1 = JsonConvert.DeserializeObject<Class1>(json);

            // Gán giá trị cho các biến


            // Hiển thị các giá trị trong MessageBox (tùy chọn)
            // MessageBox.Show($"Tên: {Alldata.Ten}\nGiống: {Alldata.Giong}\nMàu sắc: {ananimalData.Mausac}\nSố điện thoại: {ananimalData.ZLST.SLT}");
        }

        
    }
}
