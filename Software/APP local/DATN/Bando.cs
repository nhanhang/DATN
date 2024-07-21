using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GMap.NET.WindowsForms;
using GMap.NET.MapProviders;
using GMap.NET;
using GMap.NET.WindowsForms.Markers;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using Firebase.Storage;
using FireSharp.Extensions;
using Newtonsoft.Json;

namespace DATN
{
    public partial class Bando : Form
    {
        private GMapOverlay markersOverlay;
        private IFirebaseClient firebaseClient;

        private Class1 Alldata1;
        private int dem;
        private double lat=0, lng=0;
        private string textID;
        //public string tagLatitude;
        //public string tagLongitude;
        private System.Windows.Forms.Timer delayTimer;
        private string timetp ="", ten="",tenchu="",sdt = "";
        //string note;
        public Bando(string a)
        {
            
            
            InitializeComponent();
            this.textID = a;
            // this.txtID = txtID;
            InitializeFirebaseClient();
            InitializeMap();
            //delayTimer = new System.Windows.Forms.Timer();
            //delayTimer.Tick += new EventHandler(delayTimer_Tick);
            //delayTimer.Interval = 1000;
            //laytoado();
           // dem = 0;
            //delayTimer.Start();
           // laynote();
           
            LoadDataFromFirebase();
            
            dem = 0;

            //firebaseClient.OnChangeGetAsync<double>(tagLatitude, async (sender, args) =>
            //{

            //    lat = ((double)args);
            //    UpdateMapOnUIThread();
            //});
            //firebaseClient.OnChangeGetAsync<double>(tagLongitude, async (sender, args) =>
            //{
            //    lng = ((double)args);
            //    UpdateMapOnUIThread();
            //});
            eventchange();

        }
        
        private void InitializeMap()
        {
            // Thiết lập GMapControl
            gMapControl1.MapProvider = GMapProviders.GoogleMap;
            GMaps.Instance.Mode = AccessMode.ServerOnly;
            gMapControl1.Position = new PointLatLng(21.0285, 105.8542);
            gMapControl1.MinZoom = 0;
            gMapControl1.MaxZoom = 24;
            gMapControl1.Zoom = 12;
            gMapControl1.Dock = DockStyle.Fill;
            gMapControl1.ShowCenter = false;
            markersOverlay = new GMapOverlay("markers");
            gMapControl1.Overlays.Add(markersOverlay);
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
            //    AuthSecret = "UvfEy3TD74dpzNQEZIATUHVj2iUrdl3I0T2WduWm",
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
                
                    // Xử lý dữ liệu ở đây
                    return response.Body;
                
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error retrieving data from Firebase:" + ex.Message);
                return null;
            }
        }

        //private async void laynote()
        //{
        //    try
        //    {
        //        await Task.Delay(1000);
        //        //delayTimer.Start();
        //        timetp = await GetDataFromFirebase(textID + "/timetp");
        //        timetp = timetp.Trim('"');
        //        //delayTimer.Start();
        //        await Task.Delay(1000);
        //        ten = await GetDataFromFirebase(textID + "/ten");
        //        ten = ten.Trim('"');
        //        //delayTimer.Start();
        //        await Task.Delay(1000);
        //        tenchu = await GetDataFromFirebase(textID + "/tenchu");
        //        tenchu = tenchu.Trim('"');
        //        tenchu = tenchu.Replace("-", " ");
        //        button1.Text = tenchu;
        //       // await Task.Delay(1000);
        //        sdt = await GetDataFromFirebase(textID + "/sodienthoai");
        //        sdt = "0" + sdt;
        //        //note = $"Tên vật nuôi: {ten}\n Tên chủ: {tenchu}\n Ngày tiêm phòng: {timetp}\nLatitude: {lat}\nLongitude: {lng}";
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show($"Lỗi khi lấy dữ liệu :  {ex.Message}");
        //    }
        //}
        string displayValue;
        private async void eventchange()
        {
            //await Task.Delay(500);
            firebaseClient.OnChangeGetAsync<double>(textID+"/vitriX", async (sender, args) =>
            {
                lat = ((double)args);
                UpdateMapOnUIThread();
            });
            firebaseClient.OnChangeGetAsync<double>(textID + "/vitriY", async (sender, args) =>
            {
                lng = ((double)args);
                UpdateMapOnUIThread();
            });
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
                       // this.Show();


                    }
                }
            });
        }

        private async void LoadDataFromFirebase()
        {
            string Alldata = await GetDataFromFirebase(textID);
            ParseJson(Alldata);
            if (dem == 0)
            {
                //string Alldata = await GetDataFromFirebase(textID);
                //ParseJson(Alldata);
                // await Task.Delay(1000);
                //delayTimer.Start();
                timetp = Alldata1.datetimetp.Replace("-","/");
                //timetp = timetp.Trim('"');
                //timetp = timetp.Replace("-", " ");
                //delayTimer.Start();
                //await Task.Delay(1000);
                ten = Alldata1.ten;
                //ten = ten.Trim('"');
                //delayTimer.Start();
                //await Task.Delay(1000);
                tenchu =Alldata1.tenchu.Replace("-"," ");
                //tenchu = tenchu.Trim('"');
                //tenchu = tenchu.Replace("-", " ");
                //button1.Text = tenchu;
                // await Task.Delay(1000);
                sdt = "0" + Alldata1.sodienthoai.ToString();
                //sdt = "0" + sdt;
                dem = 1;
            }
            
            //await Task.Delay(1000);
            //string latitude = 
            //string longitude = await GetDataFromFirebase(tagLongitude);
            lat = Alldata1.vitriX;
            lng = Alldata1.vitriY;
            double newLat = Alldata1.vitriX;
            double newLng = Alldata1.vitriY;
            //button1.Text = lat.ToString() + "-" + lng.ToString();
            //laytoado();
            //delayTimer.Start();
            //textBox1.Text = latitude;
            //button1.Text = lat.ToString () +  "-" + lng.ToString();
            //cũ
            if (lat!=0 && lng !=0 ) { 
            
             string note = $"Pet name : {ten}\nPet owner's name: {tenchu}\nVaccination date: {timetp}\nPhone number: {sdt}\nLatitude: {lat}\nLongitude: {lng}";
            

            // Xóa tất cả các marker cũ trong overlay
            foreach (var marker in markersOverlay.Markers.ToList())
            {
                if (marker.Position.Lat != lat || marker.Position.Lng != lng)
                {
                    markersOverlay.Markers.Remove(marker);
                }
            }

            // Kiểm tra xem marker mới đã tồn tại trong overlay chưa
            bool isNewMarkerExist = markersOverlay.Markers.Any(m => m.Position.Lat == lat && m.Position.Lng == lng);

            if (!isNewMarkerExist)
            {
                // Tạo marker mới
                GMapMarker newMarker = new GMarkerGoogle(new PointLatLng(lat, lng), GMarkerGoogleType.red);
                newMarker.ToolTipText = note;

                // Hiển thị ghi chú luôn
                newMarker.ToolTipMode = MarkerTooltipMode.Always;
                //newMarker.ToolTipText = note; // Ghi chú tọa độ lên marker
                markersOverlay.Markers.Add(newMarker);
                
            }

        

            // Phóng to và di chuyển bản đồ sao cho marker nằm ở giữa

            gMapControl1.ZoomAndCenterMarkers("markers");
            gMapControl1.Zoom = 19;
  
            }
            //cũ
            //mới
            if (newLat != 0 && newLng != 0)
            {
                string note = $"Pet name : {ten}\nPet owner's name: {tenchu}\nVaccination date: {timetp}\nPhone number: {sdt}\nLatitude: {newLat}\nLongitude: {newLng}";

                GMapMarker marker = markersOverlay.Markers.FirstOrDefault(m => m.ToolTipText.Contains("Pet owner's name: " + tenchu));

                if (marker != null)
                {
                    // Marker đã tồn tại, cập nhật vị trí
                    MoveMarker(marker, newLat, newLng);
                }
                else
                {
                    // Marker chưa tồn tại, tạo mới
                    GMapMarker newMarker = new GMarkerGoogle(new PointLatLng(newLat, newLng), GMarkerGoogleType.red);
                    newMarker.ToolTipText = note;
                    newMarker.ToolTipMode = MarkerTooltipMode.Always;
                    markersOverlay.Markers.Add(newMarker);
                }

                lat = newLat;
                lng = newLng;

                // Di chuyển bản đồ đến vị trí mới và giữ marker ở giữa
                gMapControl1.Position = new PointLatLng(newLat, newLng);
                gMapControl1.Refresh();
            }
            //moi
        }
        private void UpdateMapOnUIThread()
        {
            if (InvokeRequired)
            {
                // Nếu không ở trong luồng UI chính, gọi lại phương thức này trên luồng UI chính
                BeginInvoke(new Action(() =>
                {
                    // Cập nhật bản đồ tại đây
                    LoadDataFromFirebase();
                }));
            }
            else
            {
                // Nếu đã ở trong luồng UI chính, cập nhật bản đồ trực tiếp
                LoadDataFromFirebase();
            }
        }
        private void ParseJson(string json)
        {
            Alldata1 = JsonConvert.DeserializeObject<Class1>(json);

            // Gán giá trị cho các biến


            // Hiển thị các giá trị trong MessageBox (tùy chọn)
            // MessageBox.Show($"Tên: {Alldata.Ten}\nGiống: {Alldata.Giong}\nMàu sắc: {ananimalData.Mausac}\nSố điện thoại: {ananimalData.ZLST.SLT}");
        }
        //private void delayTimer_Tick(object sender, EventArgs e)
        //{
        //    //MessageBox.Show("Timer ticked!");
        //    // Xử lý công việc sau khi delay
        //    delayTimer.Stop(); // Dừng Timer sau khi hoàn thành công việc
        //}

        private void MoveMarker(GMapMarker marker, double lat, double lng)
        {
            marker.Position = new PointLatLng(lat, lng);
        }
    }
}
