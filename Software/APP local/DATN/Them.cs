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
using System.Globalization;

namespace DATN
    {
        public partial class Them : Form
        {
            private IFirebaseClient firebaseClient;
            private string tagname, zcbtp1, zcbtp2, zcbtp3, zcbtp4;
            private int SL=0;
        private int SL1=0;
            private FirebaseStorage storage;
             DateTime realTime = DateTime.Now;
        //DateTime datetime = 
        List<string> dataList = new List<string>();

        public Them(List<string> a)
            {
           
                InitializeComponent();
                InitializeFirebaseClient();
            string[] totnghiep = { "Sterilized", "Intact" };
            string[] toa = { "R1", "R2", "R3", "R4", "R5", "R6" };
            string[] dinhvi = { "Not Registered", "Registered" };
            string[] tinhbiet = { "Male", "Female" };
            string[] xuatxu = { "Domestic", "Foreign" };
            cboDCT.Items.AddRange(toa);
            cboTN.Items.AddRange(totnghiep);
            cboDinhvi.Items.AddRange(dinhvi);
            cboTinhbiet.Items.AddRange(tinhbiet);
            cboXX.Items.AddRange(xuatxu);
            cboTN.Text = cboTN.Items[0].ToString();
            cboDCT.Text = cboDCT.Items[0].ToString();
            cboDinhvi.Text = cboDinhvi.Items[0].ToString();
            cboTinhbiet.Text = cboTinhbiet.Items[0].ToString();
            cboXX.Text = cboXX.Items[0].ToString();
            //laysl();
            eventchange();


        }
        private string txtnamsinh = "";
        private string txttiemphong = "";
        private string txtmadau = "";
        private string hinhanh = "";
        private int canhbao = 0;
        int wrtp1, wrtp2, wrtp3, wrtp4;

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
                    MessageBox.Show("Error retrieving data from Firebase:" + ex.Message);
                    return null;
                }
            }
        private void ResetFull()
        { 
            dataList.Clear();
            laysl();
            txtID.Text = string.Empty;
            txtHinhanh.Text = string.Empty;
            txtChieucao.Text = string.Empty;
            txtCannang.Text = string.Empty;
            txtDCP.Text = string.Empty;
            txtGiong.Text = string.Empty;   
            txtMausac.Text = string.Empty;
            SL = 0;
            txtVaccine.Text = string.Empty;
            txtGhichu.Text  = string.Empty;
            txtTenchu.Text = string.Empty;
            dtpNS.Value = realTime;
            dtpTP.Value = realTime;
            cboTN.Text = cboTN.Items[0].ToString();
            cboDCT.Text = cboDCT.Items[0].ToString();
            cboDinhvi.Text = cboDinhvi.Items[0].ToString();
            cboTinhbiet.Text = cboTinhbiet.Items[0].ToString();
            cboXX.Text = cboXX.Items[0].ToString();
            txtTen.Text = string.Empty;
            txtSDT.Text = string.Empty;
            txtnamsinh = "";
            txttiemphong = "";
            txtmadau = "";
            hinhanh = "";
            canhbao = 0;
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

        //private async void DownloadAndDisplayImage(string imageName)
        //{
        //    try
        //    {
        //        // Lấy URL tải xuống của hình ảnh từ Firebase Storage
        //        var imageUrl = await storage.Child("images").Child(imageName).GetDownloadUrlAsync();

        //        // Tải hình ảnh từ URL
        //        using (var webClient = new WebClient())
        //        {
        //            byte[] imageData = await webClient.DownloadDataTaskAsync(imageUrl);

        //            // Hiển thị hình ảnh trên PictureBox
        //            pictureBox1.Image = Image.FromStream(new System.IO.MemoryStream(imageData));
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show($"Lỗi khi tải xuống và hiển thị hình ảnh: {ex.Message}");
        //    }
        //}
        private void xulyNS()
        {
            //int ki = CompareDates(DateTime.Now.Date.ToString("dd/MM/yyyy"), dtpNS.Text);

            //label21.Text = ki.ToString();
            DateTime dateTP = dtpTP.Value;
            DateTime dateNS = dtpNS.Value;
            int ki = 0;
            if (dateNS >= DateTime.Now)
            {
                ki = 1;
            }
            else
            {
                ki = 0;
            }
            if (ki == 0)
            {
                txtnamsinh = dtpNS.Value.ToString("dd/MM/yyyy");
            }
            else
            {
                errorProvider1.SetError(dtpNS, "Invalid birth year");
                canhbao = 1;
            }
        }

        private void xulyTP()
        {
            int ku = 0;
            DateTime dateTP = dtpTP.Value;
            DateTime dateNS = dtpNS.Value;
            string wrTP;
            if (DateTime.Now < dateTP)
            {
                ku = 0;
            }
            else
            {
                ku = 1;
            }
            CalculateDateAfterDays(dtpTP.Text, 365, out wrTP, 0);
            
            //DateTime wrTPDate = DateTime.ParseExact(wrTP, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            string timenow;

            DateTime dateWrTP = DateTime.ParseExact(wrTP, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            //timenow = realTime.Day.ToString()+realTime.Month.ToString()+realTime.Year.ToString();
            //CalculateDateAfterDays(realTime.ToString(), 0, out timenow, 0);
            int ki = 0;
            if(DateTime.Now > dateWrTP)
            {
                ki = 1;
            }
            else
            {
                ki = 0;
            }
            
            //label21.Text = ki.ToString();
            
            int ky = 0;
            if(dateTP> dateNS)
            {
                ky = 0;
            }
            else
            {
                ky = 1;
            }
            if (ky ==  0&&ki==0 && ku == 1)
            {
                txttiemphong = dtpTP.Value.ToString("dd/MM/yyyy");
            }
            else
            {
                canhbao = 1;
                errorProvider1.SetError(dtpTP, "Invalid vaccination time");
            }
        }
        private void kiemtraID()
        {
            //string input1 = txtID.Text;
            int ktID;
            ktID = CompareWithDataList(txtID.Text, dataList);
            //label21.Text = ktID.ToString();
            long number;
            if (ktID == 1)
            {
                canhbao = 1;
                errorProvider1.SetError(txtID, "ID already exists");
                txtID.Clear();
            }
            else
            { 
                if (txtID.Text.Length == 10)
                {
                    //long number;
                    if (!long.TryParse(txtID.Text, out number))
                    {
                        canhbao = 1;
                        errorProvider1.SetError(txtID, "Invalid ID" + txtID.Text.Length);
                        txtID.Clear();
                    }


                }
                else
                {
                    txtID.Clear();
                    errorProvider1.SetError(txtID, "Invalid ID");
                    canhbao = 1;
                }
            }
        }
        private void kiemtrachieucao()
        {

            if (txtChieucao.Text.Length > 0)
            {
                int number;
                if (!int.TryParse(txtChieucao.Text, out number))
                {
                    canhbao = 1;
                    errorProvider1.SetError(txtChieucao, "Invalid height");
                    txtChieucao.Clear();
                }
               
            }
            else
            {
                canhbao = 1;
                txtChieucao.Clear();
                errorProvider1.SetError(txtChieucao, "Please enter the height.");
            }
        }
        private void kiemtracannang()
        {

            if (txtCannang.Text.Length > 0)
            {
                int number;
                if (!int.TryParse(txtCannang.Text, out number))
                {
                    canhbao = 1;
                    errorProvider1.SetError(txtCannang, "Invalid weight");
                    txtCannang.Clear();
                }

            }
            else
            {
                canhbao = 1;
                txtCannang.Clear();
                errorProvider1.SetError(txtCannang, "Please enter the weight.");
            }
        }
        private void kiemtrasodienthoai()
        {

            if (txtSDT.Text.Length > 7 && txtSDT.Text.Length < 12)
            {
                long number;
                if (!long.TryParse(txtSDT.Text, out number))
                {
                    canhbao = 1;
                    errorProvider1.SetError(txtSDT, "Invalid phone number");
                    txtSDT.Clear();
                }

            }
            else
            {
                canhbao = 1;
                txtSDT.Clear();
                errorProvider1.SetError(txtSDT, "Invalid phone number.");
            }
        }
        private void kiemtramausac()
        {
            if (txtMausac.Text.Length > 0)
            {
                if (System.Text.RegularExpressions.Regex.IsMatch(txtMausac.Text, @"\d"))
                {
                    canhbao = 1;
                    errorProvider1.SetError(txtMausac, "Invalid color.");
                    txtMausac.Clear();

                }
            }
            else
            {
                errorProvider1.SetError(txtMausac, "Please enter the color of the pet.");
                canhbao = 1;
            }
        }
        private void kiemtratenchu()
        {
            if (txtTenchu.Text.Length > 0)
            {
                if (System.Text.RegularExpressions.Regex.IsMatch(txtTenchu.Text, @"\d"))
                {
                    errorProvider1.SetError(txtTenchu, "The information you entered is not correct.");
                    txtTenchu.Clear();
                    canhbao = 1;
                }
            }
            else
            {
                errorProvider1.SetError(txtTenchu, "Please enter the pet owner's name.");
                canhbao = 1;
            }
        }
        private void kiemtraten()
        {
            if (txtTen.Text.Length == 0)
            {
                errorProvider1.SetError(txtTen, "Please enter the pet's name.");
                canhbao = 1;
            }
            else
            {
                if (Regex.IsMatch(txtTen.Text, @"[^a-zA-Z0-9]"))
                {
                    errorProvider1.SetError(txtTen, "Please enter the pet's name correctly.");
                    txtTen.Clear();
                    canhbao = 1;
                }
                
            }
        }
        private void kiemtragiong()
        {
            if (txtGiong.Text.Length == 0)
            {
                errorProvider1.SetError(txtGiong, "Please enter the breed of the pet.");
                canhbao = 1;
            }
            else
            {
                if (System.Text.RegularExpressions.Regex.IsMatch(txtGiong.Text, @"\d"))
                {
                    errorProvider1.SetError(txtGiong, "The information you entered is not correct.");
                    txtGiong.Clear();
                    canhbao = 1;
                }
            }
        }
        private void kiemtradcp()
        {
            if (txtDCP.Text.Length == 0)
            {
                errorProvider1.SetError(txtDCP, "Please enter the room address");
                canhbao = 1;
            }
            else
            {
                if (Regex.IsMatch(txtDCP.Text, @"[^a-zA-Z0-9]"))
                {
                    errorProvider1.SetError(txtDCP, "Please enter the correct room address.");
                    txtDCP.Clear();
                    canhbao = 1;
                }
            }
        }
        private void kiemtrahinhanh()
        {
            if(txtHinhanh.Text.Length == 0)
            {
                errorProvider1.SetError(bntChonanh, "Please select an image.");
                canhbao = 1;
            }
        }
        private void kiemtraVaccine()
        {
            if(txtVaccine.Text.Length == 0)
            {
                errorProvider1.SetError(bntChonanh, "Please enter the vaccine name.");
                canhbao = 1;
            }
        }
        
        private async void laysl()
        {
            
            string sl = await GetDataFromFirebase("SL");
            SL1 = int.Parse(sl);
            string matrunggian = await GetDataFromFirebase("madau");
            matrunggian = matrunggian.Trim();
            matrunggian = matrunggian.Trim('"');
            
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
            for (int i = 0; i < SL1; i++)
            {
                dataList.Add(matrunggian);
                matrunggian = await GetDataFromFirebase(matrunggian + "/zmasau");
                matrunggian = matrunggian.Trim();
                matrunggian = matrunggian.Trim('"');
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
        private async void bntThem_Click(object sender, EventArgs e)
        {
                
                
                errorProvider1.Clear();
                canhbao = 0;
                xulyNS();
                xulyTP();
                CalculateDateAfterDays(txttiemphong, 358, out zcbtp1, canhbao);
                CalculateDateAfterDays(txttiemphong, 362, out zcbtp2, canhbao);
                CalculateDateAfterDays(txttiemphong, 364, out zcbtp3, canhbao);
                CalculateDateAfterDays(txttiemphong, 367, out zcbtp4, canhbao);
                kiemtracannang();
                kiemtrachieucao();
                kiemtraID();
                kiemtradcp();
                kiemtragiong();
                kiemtramausac();
                kiemtraten();
                kiemtrahinhanh();
                kiemtraVaccine();
                //int numberID =int.Parse(txtID.Text) ;
                
                kiemtrasodienthoai();
                
                kiemtratenchu();
                
                
                

                // Tạo một đối tượng chứa thông tin sinh viên
                // Tạo tên tag từ mã sinh viên


                if (canhbao == 1)
                {
                    MessageBox.Show("Enter complete and accurate information");
                }
                else
                {
                wrtp1 = CompareDates(zcbtp1, realTime.ToString("dd/MM/yyyy"));
                wrtp2 = CompareDates(zcbtp2, realTime.ToString("dd/MM/yyyy"));
                wrtp3 = CompareDates(zcbtp3, realTime.ToString("dd/MM/yyyy"));
                wrtp4 = CompareDates(zcbtp4, realTime.ToString("dd/MM/yyyy"));
                int numberCannang = int.Parse(txtCannang.Text);
                int numberChieucao = int.Parse(txtChieucao.Text);
                int numberDinhvi;
                long numberSDT = long.Parse(txtSDT.Text);
                string txttenchu = txtTenchu.Text;
                string txtmausac = txtMausac.Text;
                string txtgiong = txtGiong.Text;
                txttenchu = txttenchu.Replace(" ", "-");
                txtmausac = txtmausac.Replace(" ", "-");
                txtgiong = txtgiong.Replace(" ", "-");
                txtnamsinh = txtnamsinh.Replace("/", "-");
                txttiemphong = txttiemphong.Replace("/", "-");
                int cb = 0;
                    string path = txtID.Text; // Thay đổi đường dẫn nếu cần
                    long numberID = long.Parse(txtID.Text);
                    long numbermasau=0;
                    //FirebaseResponse responsemadau = await firebaseClient.SetAsync("madau", txtID.Text);

                    // Thêm dữ liệu vào Firebase
                    // string path = "ID"; // Đường dẫn tới tag "ID"
                    
                    string f = await GetDataFromFirebase("SL");
                    SL=int.Parse(f);
                    if (SL > 0)
                    {

                        //MessageBox.Show($"{txtID.Text}" + "-" + $"{SL}");
                        string giatri;
                        txtmadau = await GetDataFromFirebase("madau");
                        
                        
                        if (txtmadau.Length < 10)
                        {
                            int k;
                            k = 10 - txtmadau.Length;
                            for (int i = 0; i < k; i++)
                            {
                                txtmadau = "0" + txtmadau;
                            }
                        }
                        string matrunggian = txtmadau;
                        for (int i = 0; i < SL; i++)
                        {
                            
                            if (txtID.Text == matrunggian)
                            {
                                
                                cb = 1;
                            }
                            else
                            {
                                giatri = await GetDataFromFirebase(matrunggian + "/zmasau");
                                matrunggian = giatri;
                                if (matrunggian.Length < 6)
                                {
                                    int k;
                                    k = 10 - txtmadau.Length;
                                    for (int m = 0; m < k; m++)
                                    {
                                        txtmadau = "0" + txtmadau;
                                    }
                                }
                            }

                        }
                        if (cb == 0)
                        {
                            FirebaseResponse responseSL = await firebaseClient.SetAsync("SL", 1);
                            FirebaseResponse responsethaydoimatruoc = await firebaseClient.SetAsync(txtmadau + "/zmatruoc", numberID);
                            numbermasau = long.Parse(txtmadau);
                            FirebaseResponse responsemadau = await firebaseClient.SetAsync("madau", numberID);
                        }


                    }
                    else { FirebaseResponse responsemadau = await firebaseClient.SetAsync("madau", numberID);
                        numbermasau = 0;
                    }
                    
                    if (cb == 0)
                    {
                        
                        string imagepath = hinhanh;
                        if (File.Exists(imagepath))
                        {
                            // đọc dữ liệu từ tệp hình ảnh
                            byte[] imagebytes = File.ReadAllBytes(imagepath);

                            // tạo tên tệp hình ảnh dựa trên tên của tệp
                            string imagename = Path.GetFileName(imagepath);

                            // tải dữ liệu lên firebase storage
                            await UploadImage(imagebytes, imagename);
                        }
                        else
                        {
                            MessageBox.Show("Please select a valid image file.");
                        }
                        if(cboDinhvi.Text == "Not Registered")
                        {
                            numberDinhvi = 0;
                        }
                        else
                        {
                            numberDinhvi = 1;
                        }
                        string txtXX = cboXX.Text;
                        txtXX = txtXX.Trim();
                        txtXX = txtXX.Replace(" ", "-");
                        string txtTN = cboTN.Text;
                        txtTN = txtTN.Trim();
                        txtTN = txtTN.Replace(" ", "-");
                        // Tạo một đối tượng chứa dữ liệu bạn muốn thêm
                        Data data = new Data
                        {
                            ten = txtTen.Text,
                            znamsinh = txtnamsinh,
                            datetimetp = txttiemphong,
                            tttiem = 0,
                            giong = txtgiong,
                            zdinhvi = numberDinhvi,
                            tinhbiet = cboTinhbiet.Text,
                            mausac = txtmausac,
                            cannang = numberCannang,
                            chieucao = numberChieucao,
                            zloaitotnghiep = txtTN,
                            tenchu = txttenchu,
                            sodienthoai = numberSDT,
                            diachiphong = txtDCP.Text,
                            diachitoa = cboDCT.Text,
                            vitriX = 0,
                            vitriY = 0,
                            zmatruoc = 0,
                            zmasau = numbermasau,
                            ztenhinhanh = txtHinhanh.Text,
                            zcbtp1 = wrtp1,
                            zcbtp2 = wrtp2,
                            zcbtp3 = wrtp3,
                            zcbtp4 = wrtp4,
                            xuatxu = txtXX
                        };

                        // Sử dụng phương thức SetAsync để thêm dữ liệu vào Firebase mà không ghi đè lên dữ liệu hiện có
                        FirebaseResponse response = firebaseClient.Set(path, data);
                        FirebaseResponse responseLST = await firebaseClient.SetAsync(path + "/zLST/SLT",1);
                        string textVaccine = txtVaccine.Text;
                        textVaccine = textVaccine.Trim();
                        textVaccine = textVaccine.Replace(" ", "-");
                        string textGhichu = txtGhichu.Text;
                        textGhichu = textGhichu.Trim();
                        textGhichu = textGhichu.Replace(" ", "-");
                        string tp = txttiemphong + "@" + textVaccine + "@" + textGhichu; 
                        FirebaseResponse responseTP = await firebaseClient.SetAsync(path + "/zLST/TP1", tp);
                        //if (response.StatusCode==HttpStatusCode.OK)
                        //{
                        //    // Thông báo khi gửi dữ liệu thành công
                        //    MessageBox.Show("Dữ liệu đã được lưu vào Firebase thành công!");
                        //}
                        //else
                        //{
                        //    // Thông báo khi gửi dữ liệu không thành công
                        //    MessageBox.Show("Gửi dữ liệu lên Firebase không thành công. Mã lỗi: " + response.StatusCode);
                        //}
                        //FirebaseResponse responsenamsinh = await firebaseClient.SetAsync(path + "/namsinh", data.namsinh);
                        //FirebaseResponse responsetimetp = await firebaseClient.SetAsync(path + "/timetp", data.timetp);
                        //FirebaseResponse responsetrangthaitiem = await firebaseClient.SetAsync(path + "/trangthaitiem", data.trangthaitiem);
                        //FirebaseResponse responsegiong = await firebaseClient.SetAsync(path + "/giong", data.giong);
                        //FirebaseResponse responsedinhvi = await firebaseClient.SetAsync(path + "/dinhvi", data.dinhvi);
                        //FirebaseResponse responsetinhbiet = await firebaseClient.SetAsync(path + "/tinhbiet", data.tinhbiet);
                        //FirebaseResponse responsemausac = await firebaseClient.SetAsync(path + "/mausac", data.mausac);

                        //FirebaseResponse responsecannang = await firebaseClient.SetAsync(path + "/cannang", data.cannang);
                        //FirebaseResponse responsechieucao = await firebaseClient.SetAsync(path + "/chieucao", data.chieucao);
                        //FirebaseResponse responseloaitotnghiep = await firebaseClient.SetAsync(path + "/loaitotnghiep", data.loaitotnghiep);
                        //FirebaseResponse responsetenchu = await firebaseClient.SetAsync(path + "/tenchu", data.tenchu);
                        //FirebaseResponse responsesodienthoai = await firebaseClient.SetAsync(path + "/sodienthoai", data.sodienthoai);
                        //FirebaseResponse responsediachiphong = await firebaseClient.SetAsync(path + "/diachiphong", data.diachiphong);
                        //FirebaseResponse responsediachitoa = await firebaseClient.SetAsync(path + "/diachitoa", data.diachitoa);
                        //FirebaseResponse responsevitriX = await firebaseClient.SetAsync(path + "/vitriX", data.vitriX);
                        //FirebaseResponse responsevitriY = await firebaseClient.SetAsync(path + "/vitriY", data.vitriY);
                        //FirebaseResponse responsemasau = await firebaseClient.SetAsync(path + "/masau", data.masau);
                        //FirebaseResponse responsematruoc = await firebaseClient.SetAsync(path + "/matruoc", data.matruoc);
                        //FirebaseResponse responsehinhanh = await firebaseClient.SetAsync(path + "/tenhinhanh", data.tenhinhanh);
                        //FirebaseResponse responsecbtp1 = await firebaseClient.SetAsync(path + "/cbtp1", data.cbtp1);
                        //FirebaseResponse responsecbtp2 = await firebaseClient.SetAsync(path + "/cbtp2", data.cbtp2);
                        //FirebaseResponse responsecbtp3 = await firebaseClient.SetAsync(path + "/cbtp3", data.cbtp3);
                        //FirebaseResponse responsecbtp4 = await firebaseClient.SetAsync(path + "/cbtp4", data.cbtp4);


                        SL++;
                        FirebaseResponse responseSL = await firebaseClient.SetAsync("SL", SL);
                        MessageBox.Show("Data has been successfully saved to Firebase!");
                        ResetFull();

                    }
                    else
                    {
                        
                        errorProvider1.SetError(txtID, "The ID already exists, please enter a different ID.");
                    }

                } 
                
                // Đường dẫn đến nơi bạn muốn lưu dữ liệu

           
        }

        private void bntChonanh_Click_1(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.jpg, *.jpeg, *.png) | *.jpg; *.jpeg; *.png";
            openFileDialog.Multiselect = false;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Lấy đường dẫn của tệp hình ảnh được chọn và hiển thị nó trong TextBox
                hinhanh = openFileDialog.FileName;
                txtHinhanh.Text = Path.GetFileName(openFileDialog.FileName);
            }
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
        private bool IsValidNumericString(string input)
        {
            // Kiểm tra chuỗi có đúng 10 ký tự và tất cả đều là chữ số
            if (Regex.IsMatch(input, @"^\d{10}$"))
            {
                try
                {
                    // Kiểm tra xem chuỗi có phải là số hợp lệ trong phạm vi của long
                    long number = long.Parse(input);
                    return true;
                }
                catch (FormatException)
                {
                    MessageBox.Show("The string is not a valid number.");
                    return false;
                }
                catch (OverflowException)
                {
                    MessageBox.Show("The value is too large or too small for an Int64 data type.");
                    return false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error has occurred:" + ex.Message);
                    return false;
                }
            }
            return false;
        }


        public  int CompareWithDataList(string valueToCompare, List<string> dataList)
        {
            valueToCompare = valueToCompare.Trim();
            for(int i = 0; i < SL1; i++)
            {
                if(valueToCompare == dataList[i])
                {
                    //label21.Text = "ok";
                    return 1;
                }
            }
            //if (dataList.Contains(valueToCompare))
            //{
            //    return 1; // Trùng lặp
            //}
            return 0; // Không trùng lặp
        }






        //private void bntLay_Click_1(object sender, EventArgs e)
        //{
        //    string imageName = "anh-meo.jpg"; // Thay thế bằng tên hình ảnh bạn muốn hiển thị
        //    DownloadAndDisplayImage(imageName);
        //}
    }
    }
