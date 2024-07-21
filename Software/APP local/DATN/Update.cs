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

namespace DATN
{
    public partial class Update : Form
    {
        private Timer timer;
        DateTime realTime = DateTime.Now;
        
        private IFirebaseClient firebaseClient;
        private FirebaseStorage storage;
        private int SL;
        string[] totnghiep = { "Sterilized", "Intact" };
        string[] toa = { "R1", "R2", "R3", "R4", "R5", "R6" };
        string[] dinhvi = { "Not Registered","Registered"  };
        string[] tinhbiet = { "Male", "Female" };
        string[] xuatxu = { "Domestic", "Foreign" };
        private int canhbaoID = 0;
        private int canhbaoVat= 0;
        private int canhbaoChu = 0;
        private string txtnamsinh = "";
        private string txttiemphong = "";
        private string txtmadau = "";
        private string hinhanh = "";
        private int demVat = 0;
        private int demchu = 0;
        private int Stt = 1;
        public Update(string a)
        {
            
            InitializeComponent();
            
            string[] vatnuoi = { "Name", "Year of birth", "Species", "Location", "Gender", "Color", "Weight", "Height", "Origin", "Sterilization", "Image" };
            string[] chu = { "Pet owner's name", "Phone number", "Room address", "Building address" };
            cboChu.Items.AddRange(chu);
            cboVatnuoi.Items.AddRange(vatnuoi);
            cboVatnuoi.Text = cboVatnuoi.Items[0].ToString();
            cboChu.Text = cboChu.Items[0].ToString();
            if (a.Length > 0)
            {
                cboID.Text = a;
            }
            InitializeFirebaseClient();
            InitializeDataGridView();
            
            laysl();

            eventchange();

            //cboTT.Items.AddRange(dinhvi);
            //cboDCT.Items.AddRange(toa);

            //timer = new Timer();
            //timer.Interval = 1000; // Thời gian tính bằng mili giây
            //timer.Tick += chuyen;
            //timer.Start();
        }
        private DataTable dataview = new DataTable();
        public string textID = "";
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
        private async Task<string> GetDataFromFirebase(string tagAddress)
        {
            try
            {
                FirebaseResponse response = await firebaseClient.GetAsync(tagAddress);
                return response.Body;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lấy dữ liệu từ Firebase: " + ex.Message);
                return null;
            }
        }
        private void InitializeDataGridView()
        {
            dataview.Columns.Add("STT", typeof(int));
            dataview.Columns.Add("ID", typeof(string));
            dataview.Columns.Add("Type", typeof(string));
            dataview.Columns.Add("New information", typeof(string));
            dataview.Columns.Add("Old information", typeof(string));
            
            

            dataView1.DataSource = dataview;
            dataView1.Columns["STT"].Width = 39;
            dataView1.Columns["ID"].Width = 82;
            dataView1.Columns["Type"].Width = 115;
            dataView1.Columns["New information"].Width = 140;
            dataView1.Columns["Old information"].Width = 140;


        }
        
        
        private async Task UploadImage(byte[] imageData, string imageName)
        {
            try
            {
                // Chuyển đổi mảng byte thành một MemoryStream
                using (MemoryStream stream = new MemoryStream(imageData))
                { // Tải hình ảnh lên Firebase Storage
                    await storage.Child(imageName).PutAsync(stream);

                    // Đợi cho quá trình tải hoàn tất


                    MessageBox.Show("Image uploaded successfully!");
                };



            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error uploading image: {ex.Message}");
            }
        }
        private async void laysl()
        {
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
                Task.Delay(500);
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
        private void chuyen()
        {
            int cb1 = 0;
            int cb2 = 0;
            
            for (int i = 0; i < 11 ; i++)
            {
                string k = cboVatnuoi.Items[i].ToString();
                
                    if (cboVatnuoi.Text != k)
                    {
                        cb1 = 1;
                    }
                    else
                    {
                        cb1 = 0;
                        break;
                    }
                
            }
            for (int i = 0; i < 4; i++)
            {
                string k = cboChu.Items[i].ToString();

                if (cboChu.Text != k)
                {
                    cb2 = 1;
                   
                }
                else
                {
                    cb2 = 0;
                    break;
                }

            }
            if (cb1 == 1 || cb2 == 1)
            {
                if (cb1 == 1)
                {
                    errorProvider1.SetError(cboVatnuoi, "Please enter the correct attribute");
                    cboVatnuoi.Text = cboVatnuoi.Items[0].ToString();
                    canhbaoVat = 1;
                }
                else if (cb2 == 1)
                {
                    errorProvider1.SetError(cboChu, "Please enter the correct attribute");
                    cboChu.Text = cboChu.Items[0].ToString();
                    canhbaoChu = 1;
                }
                

            }
            else
            {
                if (cboVatnuoi.Text == "Year of birth")
                {
                    txtVatnuoi.Visible = false;
                    dtpNT.Visible = true;
                    bntChonanh.Visible = false;
                    cboTT.Visible = false;
                    
                }
                else if (cboVatnuoi.Text == "Location" || cboVatnuoi.Text == "Gender" || cboVatnuoi.Text == "Sterilization" || cboVatnuoi.Text == "Origin")
                {
                    txtVatnuoi.Visible = false;
                    dtpNT.Visible = false;
                    bntChonanh.Visible = false;
                    cboTT.Visible = true;
                    if(cboVatnuoi.Text == "Location")
                    {
                        cboTT.Items.AddRange(dinhvi);
                    }
                    else if (cboVatnuoi.Text == "Gender")
                    {
                        cboTT.Items.AddRange(tinhbiet);
                    }
                    else if(cboVatnuoi.Text == "Origin")
                    {
                        cboTT.Items.AddRange(xuatxu);
                    }
                    else
                    {
                        cboTT.Items.AddRange(totnghiep);
                    }
                    cboTT.Text = cboTT.Items[0].ToString();
                }
                else if (cboVatnuoi.Text == "Image")
                {
                    txtVatnuoi.Visible = true;
                    txtVatnuoi.ReadOnly = true;
                    dtpNT.Visible = false;
                    bntChonanh.Visible = true;
                    cboTT.Visible = false;
                }
                else
                {
                    txtVatnuoi.Visible = true;
                    txtVatnuoi.ReadOnly = false;
                    dtpNT.Visible = false;
                    bntChonanh.Visible = false;
                    cboTT.Visible = false;
                }
                if (cboChu.Text == "Building address")
                {
                    txtChu.Visible = false;
                    cboDCT.Visible = true;
                    cboDCT.Items.AddRange(toa);
                    cboDCT.Text = cboDCT.Items[0].ToString();
                }
                else
                {
                    txtChu.Visible = true;
                    cboDCT.Visible = false;
                }
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
        private void xulyNS()
        {

            if (dtpNT.Value.Year <= DateTime.Now.Year && dtpNT.Value.Month <= DateTime.Now.Month && dtpNT.Value.Day <= DateTime.Now.Day)
            {
                txtnamsinh = dtpNT.Text;
            }
            else
            {
                errorProvider1.SetError(dtpNT, "Invalid birth year");
                canhbaoVat = 1;
            }

        }
        private void xulyTP()
        {

            if (dtpNT.Value.Year <= DateTime.Now.Year && dtpNT.Value.Month <= DateTime.Now.Month && dtpNT.Value.Day <= DateTime.Now.Day)
            {
                txttiemphong = dtpNT.Text;
            }
            else
            {
                canhbaoVat = 1;
                errorProvider1.SetError(dtpNT, "Invalid vaccination time");
            }

        }
        private void kiemtraID()
        {
            int cb=0;

            if (cboID.Text.Trim().Length > 0)
            {

               for(int i = 0; i < SL; i++)
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
        private void kiemtrachieucao()
        {

            if (txtVatnuoi.Text.Length > 0)
            {
                int number;
                if (!int.TryParse(txtVatnuoi.Text, out number))
                {
                    canhbaoVat = 1;
                    errorProvider1.SetError(txtVatnuoi, "Invalid height");
                    txtVatnuoi.Clear();
                }

            }
            else
            {
                canhbaoVat = 1;
                //errorProvider1.SetError(txtVatnuoi, "Vui lòng nhập chiều cao ");
            }
        }
        private void kiemtracannang()
        {

            if (txtVatnuoi.Text.Length > 0)
            {
                int number;
                if (!int.TryParse(txtVatnuoi.Text, out number))
                {
                    canhbaoVat = 1;
                   errorProvider1.SetError(txtVatnuoi, "Invalid weight");
                    txtVatnuoi.Clear();
                }

            }
            else
            {
                canhbaoVat = 1;
               // errorProvider1.SetError(txtVatnuoi, "Cân nặng chưa hợp lệ ");
            }
        }
        private void kiemtrasodienthoai()
        {
            if (txtChu.Text.Length > 0)
            {
                if (txtChu.Text.Length > 7 && txtChu.Text.Length < 12)
                {
                    long number;
                    if (!long.TryParse(txtChu.Text, out number))
                    {
                        canhbaoChu = 1;
                        errorProvider1.SetError(txtChu, "Invalid phone number");
                        txtChu.Clear();
                    }

                }
                else
                {
                    canhbaoChu = 1;
                    errorProvider1.SetError(txtChu, "Invalid phone number");
                    txtChu.Clear();
                }
            }
            else
            {
                canhbaoChu = 1;
                //errorProvider1.SetError(txtChu, "Số điện thoại chưa hợp lệ ");
            }
        }
        private void kiemtramausac()
        {
            if (txtVatnuoi.Text.Length > 0)
            {
                if (System.Text.RegularExpressions.Regex.IsMatch(txtVatnuoi.Text, @"\d"))
                {
                    canhbaoVat = 1;
                    errorProvider1.SetError(txtVatnuoi, "Invalid color");
                    txtVatnuoi.Clear();

                }
            }
            else
            {
               // errorProvider1.SetError(txtVatnuoi, "Vui lòng nhập màu của vật nuôi.");
                canhbaoVat = 1;
            }
        }
        private void kiemtratenchu()
        {
            if (txtChu.Text.Length > 0)
            {
                if (System.Text.RegularExpressions.Regex.IsMatch(txtChu.Text, @"\d"))
                {
                    errorProvider1.SetError(txtChu, "You entered the pet owner's name incorrectly.");
                    txtChu.Clear();
                    canhbaoChu = 1;
                }
            }
            else
            {
                //errorProvider1.SetError(txtChu, "Vui lòng nhập tên chủ sở hữu.");
                canhbaoChu = 1;
            }
        }
        private void kiemtraten()
        {
            if (txtVatnuoi.Text.Length == 0)
            {
               // errorProvider1.SetError(txtVatnuoi, "Vui lòng nhập tên vật nuôi.");
                canhbaoVat = 1;
            }
            else
            {
                if (Regex.IsMatch(txtVatnuoi.Text, @"[^a-zA-Z0-9]"))
                {
                    errorProvider1.SetError(txtVatnuoi, "Please enter the pet's name correctly");
                    txtVatnuoi.Clear();
                    canhbaoVat = 1;
                }

            }
        }
        private void kiemtragiong()
        {
            if (txtVatnuoi.Text.Length == 0)
            {
                //errorProvider1.SetError(txtVatnuoi, "Vui lòng nhập giống vật nuôi.");
                canhbaoVat = 1;
            }
            else
            {
                if (System.Text.RegularExpressions.Regex.IsMatch(txtVatnuoi.Text, @"\d"))
                {
                    errorProvider1.SetError(txtVatnuoi, "The information you entered is not correct");
                    txtVatnuoi.Clear();
                    canhbaoVat = 1;
                }
            }
        }
        private void kiemtradcp()
        {
            if (txtChu.Text.Length == 0)
            {
               // errorProvider1.SetError(txtChu, "Vui lòng dịa chỉ phòng.");
                canhbaoChu = 1;
            }
            else
            {
                if (Regex.IsMatch(txtChu.Text, @"[^a-zA-Z0-9]"))
                {
                    errorProvider1.SetError(txtChu, "Please enter the room address correctly");
                    txtChu.Clear();
                    canhbaoChu = 1;
                }
            }
        }
        private void kiemtrahinhanh()
        {
            if (txtVatnuoi.Text.Length == 0)
            {
                //errorProvider1.SetError(bntChonanh, "Vui lòng chọn hình ảnh.");
                canhbaoVat = 1;
            }
        }
        private void cboVatnuoi_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboTT.Items.Clear();

            if (demVat > 0)
            {
                
                chuyen();
                if (cboVatnuoi.Text == "Location" || cboVatnuoi.Text == "Gender" || cboVatnuoi.Text == "Sterilization" || cboVatnuoi.Text == "Origin")
                {
                    
                    cboTT.Text = cboTT.Items[0].ToString();
                }
                else if (cboVatnuoi.Text == "Year of birth")
                {
                    chuyen();
                    dtpNT.Value = realTime;
                }
                else
                {
                    txtVatnuoi.Clear();
                }
                
                
            }
            
            demVat++;

        }
        private void cboChu_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboDCT.Items.Clear();
            
            if (demchu > 0)
            {
                chuyen();
                if (cboChu.Text == "Building address")
                {
                    cboDCT.Text = cboDCT.Items[0].ToString();
                }
                else
                {
                    txtChu.Clear();
                }
                
            }
            //dtpNT.Value = realTime;
            
            demchu++;

        }
        private async void bntUpdate_Click(object sender, EventArgs e)
        {
            errorProvider1.Clear();
            kiemtraID();
         
            if (cboVatnuoi.Text == cboVatnuoi.Items[0].ToString())
            {
                kiemtraten();

            }
            else if (cboVatnuoi.Text == cboVatnuoi.Items[1].ToString())
            {
                xulyNS();
            }
            //else if (cboVatnuoi.Text == cboVatnuoi.Items[2].ToString())
            //{
            //    xulyTP();
            //}
            else if (cboVatnuoi.Text == cboVatnuoi.Items[2].ToString())
            {
                kiemtragiong();
            }
            else if (cboVatnuoi.Text == cboVatnuoi.Items[3].ToString())
            {
                //dinhvi
            }
            else if (cboVatnuoi.Text == cboVatnuoi.Items[5].ToString())
            {
                kiemtramausac();
            }
            else if (cboVatnuoi.Text == cboVatnuoi.Items[6].ToString())
            {
                kiemtracannang();
            }
            else if (cboVatnuoi.Text == cboVatnuoi.Items[7].ToString())
            {
                kiemtrachieucao();
            }
            else if (cboVatnuoi.Text == cboVatnuoi.Items[9].ToString())
            {
                //loaitotnghiep
            }
            else if (cboVatnuoi.Text == cboVatnuoi.Items[10].ToString())
            {
                kiemtrahinhanh();
            }
            else if (cboVatnuoi.Text == cboVatnuoi.Items[4].ToString())
            {
                //tinhbiet
            }
            else;
            
            if(cboChu.Text == cboChu.Items[0].ToString())
            {
                kiemtratenchu();
            }
            else if (cboChu.Text == cboChu.Items[2].ToString())
            {
                kiemtradcp();
            }
            else if (cboChu.Text == cboChu.Items[1].ToString())
            {
                kiemtrasodienthoai();
            }
            else
            {
                //diachitoa
            }

            
            if (canhbaoID ==0 )
            {
                if (canhbaoVat == 0)
                {
                    string ndtrc="";
                    string ndsau="";
                    int ha=0;
                    if (cboVatnuoi.Text == cboVatnuoi.Items[0].ToString())
                    {
                        string m = txtVatnuoi.Text;
                        ndtrc = await GetDataFromFirebase(cboID.Text + "/ten");
                        ndtrc = ndtrc.Trim('"');
                        ndsau = txtVatnuoi.Text;
                        m = m.Replace(" ", "-");
                        FirebaseResponse responseTen = await firebaseClient.SetAsync(cboID.Text + "/ten", txtVatnuoi.Text);
                        
                    }
                    else if (cboVatnuoi.Text == cboVatnuoi.Items[1].ToString())
                    {
                        txtnamsinh = txtnamsinh.Replace("/", "-");
                        ndtrc = await GetDataFromFirebase(cboID.Text + "/znamsinh");
                        ndtrc = ndtrc.Trim('"');
                        ndtrc = ndtrc.Replace("-", "/");
                        ndsau = txtnamsinh;
                        FirebaseResponse responseNamsinh = await firebaseClient.SetAsync(cboID.Text + "/znamsinh", txtnamsinh);
                        
                    }
                    //else if (cboVatnuoi.Text == cboVatnuoi.Items[2].ToString())
                    //{
                    //    txttiemphong = txttiemphong.Replace("/", "-");
                    //    ndtrc = await GetDataFromFirebase(cboID.Text + "/timetp");
                    //    ndtrc = ndtrc.Trim('"');
                    //    ndtrc = ndtrc.Replace("-", "/");
                    //    ndsau = txttiemphong;
                    //    FirebaseResponse responseTimetp = await firebaseClient.SetAsync(cboID.Text + "/timetp", txttiemphong);
                        
                        
                    //}
                    else if (cboVatnuoi.Text == cboVatnuoi.Items[2].ToString())
                    {
                        string giong = txtVatnuoi.Text;
                        giong = giong.Replace(" ", "-");
                        ndtrc = await GetDataFromFirebase(cboID.Text + "/giong");
                        ndtrc = ndtrc.Trim('"');
                        ndtrc = ndtrc.Replace("-", " ");
                        ndsau = giong;
                        FirebaseResponse responseGiong = await firebaseClient.SetAsync(cboID.Text + "/giong", giong);

                        
                    }
                    else if (cboVatnuoi.Text == cboVatnuoi.Items[3].ToString())
                    {

                        int numberdinhvi = 1;
                        if (cboTT.Text == dinhvi[0])
                        {
                            numberdinhvi = 0;
                        }
                        else if (cboTT.Text == dinhvi[1])
                        {
                            numberdinhvi =1;
                        }
                        ndsau = cboTT.Text;
                        ndtrc = await GetDataFromFirebase(cboID.Text + "/zdinhvi");
                        if (ndtrc == "1")
                        {
                            ndtrc = "Registered";
                        }
                        else if (ndtrc == "0")
                        {
                            ndtrc = "Not registered";
                        }
                        else;
                        FirebaseResponse responseDinhvi = await firebaseClient.SetAsync(cboID.Text + "/zdinhvi", numberdinhvi);


                        //dinhvi
                    }
                    else if (cboVatnuoi.Text == cboVatnuoi.Items[5].ToString())
                    {
                        string mausac = txtVatnuoi.Text;
                        mausac = mausac.Replace(" ", "-");
                        ndsau = txtVatnuoi.Text;
                        ndtrc = await GetDataFromFirebase(cboID.Text + "/mausac");
                        ndtrc = ndtrc.Trim('"');
                        ndtrc = ndtrc.Replace("-", " ");
                        FirebaseResponse responseMausac = await firebaseClient.SetAsync(cboID.Text + "/mausac", mausac);
                    }
                    else if (cboVatnuoi.Text == cboVatnuoi.Items[6].ToString())
                    {
                        int cannang = int.Parse(txtVatnuoi.Text);
                        ndsau = txtVatnuoi.Text;
                        ndtrc = await GetDataFromFirebase(cboID.Text + "/cannang");
                        FirebaseResponse responseCannang = await firebaseClient.SetAsync(cboID.Text + "/cannang", cannang);
                    }
                    else if (cboVatnuoi.Text == cboVatnuoi.Items[7].ToString())
                    {
                        int chieucao = int.Parse(txtVatnuoi.Text);
                        ndsau = txtVatnuoi.Text;
                        ndtrc = await GetDataFromFirebase(cboID.Text + "/chieucao");
                        FirebaseResponse responseChieucao = await firebaseClient.SetAsync(cboID.Text + "/chieucao", chieucao);
                    }
                    else if (cboVatnuoi.Text == cboVatnuoi.Items[8].ToString()) {
                        ndsau = cboTT.Text;
                        ndtrc = await GetDataFromFirebase(cboID.Text + "/xuatxu");
                        ndtrc = ndtrc.Trim('"');
                        FirebaseResponse responseXuatxu = await firebaseClient.SetAsync(cboID.Text + "/xuatxu", cboTT.Text);
                    }
                    else if (cboVatnuoi.Text == cboVatnuoi.Items[9].ToString())
                    {
                        ndsau = cboTT.Text;
                        ndtrc = await GetDataFromFirebase(cboID.Text + "/zloaitotnghiep");
                        ndtrc = ndtrc.Trim('"');
                        FirebaseResponse responseTotnghiep = await firebaseClient.SetAsync(cboID.Text + "/zloaitotnghiep", cboTT.Text);
                        //loaitotnghiep
                    }
                    
                    else if (cboVatnuoi.Text == cboVatnuoi.Items[10].ToString())
                    {
                        string imagepath = hinhanh;

                        if (System.IO.File.Exists(imagepath))
                        {
                            ha = 0;
                            ndsau = txtVatnuoi.Text;
                            ndtrc = await GetDataFromFirebase(cboID.Text + "/ztenhinhanh");
                            ndtrc = ndtrc.Trim('"');
                            FirebaseResponse responsehinhanh = await firebaseClient.SetAsync(cboID.Text + "/ztenhinhanh", txtVatnuoi.Text);
                            // đọc dữ liệu từ tệp hình ảnh
                            byte[] imagebytes = System.IO.File.ReadAllBytes(imagepath);

                            // tạo tên tệp hình ảnh dựa trên tên của tệp
                            string imagename = Path.GetFileName(imagepath);

                            // tải dữ liệu lên firebase storage
                            await UploadImage(imagebytes, imagename);
                        }
                        else
                        {
                            ha = 1;
                            MessageBox.Show("Please select a valid image file.");
                        }
                        
                    }

                    else if (cboVatnuoi.Text == cboVatnuoi.Items[4].ToString())
                    {
                        ndsau = cboTT.Text;
                        ndtrc = await GetDataFromFirebase(cboID.Text + "/tinhbiet");
                        ndtrc = ndtrc.Trim('"');
                        FirebaseResponse responseTotnghiep = await firebaseClient.SetAsync(cboID.Text + "/tinhbiet", cboTT.Text);
                        //tinhbiet
                    }
                    else;
                    if (ha == 0)
                    {
                        

                        dataview.Rows.Add(Stt, cboID.Text, cboVatnuoi.Text, ndsau, ndtrc);
                        dataView1.DataSource = dataview;
                        Stt++;
                    }

                }
                if (canhbaoChu == 0)
                {
                    string ndtrc = "";
                    string ndsau = "";
                    if (cboChu.Text == cboChu.Items[0].ToString())
                    {
                        string tenchu = txtChu.Text;
                        tenchu = tenchu.Replace(" ", "-");
                        ndsau = txtChu.Text;
                        ndtrc = await GetDataFromFirebase(cboID.Text + "/tenchu");
                        ndtrc = ndtrc.Trim('"');
                        ndtrc = ndtrc.Replace("-", " ");
                        
                        FirebaseResponse responseTenchu = await firebaseClient.SetAsync(cboID.Text + "/tenchu", txtChu.Text);
                    }
                    else if (cboChu.Text == cboChu.Items[2].ToString())
                    {
                        ndsau = txtChu.Text;
                        ndtrc = await GetDataFromFirebase(cboID.Text + "/diachiphong");
                        ndtrc = ndtrc.Trim('"');
                        FirebaseResponse responseDiachiphong = await firebaseClient.SetAsync(cboID.Text + "/diachiphong", txtChu.Text);
                    }
                    else if (cboChu.Text == cboChu.Items[1].ToString())
                    {

                        long numberSDT = long.Parse(txtChu.Text);
                        ndsau = txtChu.Text;
                        ndtrc = await GetDataFromFirebase(cboID.Text + "/sodienthoai");
                        if (ndtrc.Length < 10)
                        {
                            int j = 10 - ndtrc.Length;
                            for (int i = 0; i < j; i++)
                            {
                                ndtrc = '0' + ndtrc;
                            }
                        }
                        FirebaseResponse responseSodienthoai = await firebaseClient.SetAsync(cboID.Text + "/sodienthoai",numberSDT);
                    }
                    else
                    {
                        ndsau = cboDCT.Text;
                        ndtrc = await GetDataFromFirebase(cboID.Text + "/diachitoa");
                        ndtrc = ndtrc.Trim('"');
                        FirebaseResponse responseSodienthoai = await firebaseClient.SetAsync(cboID.Text + "/diachitoa", cboDCT.Text);
                        //diachitoa
                    }
                    

                    dataview.Rows.Add(Stt, cboID.Text, cboChu.Text, ndsau, ndtrc);
                    dataView1.DataSource = dataview;
                    Stt++;
                }
                if(canhbaoVat == 1 && canhbaoChu == 1)
                {
                    MessageBox.Show("Please enter the content you want to update.");
                    canhbaoChu = 0;
                    canhbaoVat = 0;
                }
                else
                {
                    MessageBox.Show("Data updated successfully.");
                    canhbaoChu = 0;
                    canhbaoVat = 0;
                    cboID.Text = "";
                    txtChu.Clear();
                    txtVatnuoi.Clear();
                    cboVatnuoi.Text = cboVatnuoi.Items[0].ToString();
                    cboChu.Text = cboChu.Items[0].ToString();
                }
            }
            else
            {
                MessageBox.Show("Please enter the correct information.");
                canhbaoChu = 0;
                canhbaoVat = 0;
                canhbaoID = 0;
            }
        }

        private void bntChonanh_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.jpg, *.jpeg, *.png) | *.jpg; *.jpeg; *.png";
            openFileDialog.Multiselect = false;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Lấy đường dẫn của tệp hình ảnh được chọn và hiển thị nó trong TextBox
                hinhanh = openFileDialog.FileName;
                txtVatnuoi.Text = Path.GetFileName(openFileDialog.FileName);
            }
        }

        private void bntTiemphong_Click(object sender, EventArgs e)
        {

            if (cboID.Text.Trim().Length > 0)
            {
                int cb = 0;
                for (int i = 0; i < SL; i++)
                {
                    AB: 
                    if (cboID.Items.Count > 0)
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
                    else
                    {
                        MessageBox.Show("Please wait for the system to get the ID.");
                        goto AB;
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
                    Capnhattiemphong frm7 = new Capnhattiemphong(cboID.Text);
                    //frm7.textID = cboID.Text;
                    frm7.ShowDialog();
                    frm7 = null;
                    this.Show();

                }
            }
            else
            {
                this.Hide();
                Capnhattiemphong frm7 = new Capnhattiemphong(cboID.Text);
                //frm7.textID = cboID.Text;
                frm7.ShowDialog();
                frm7 = null;
                this.Show();
            }
            
            
        }

       
    }
}
