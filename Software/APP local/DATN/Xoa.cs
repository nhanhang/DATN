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
    public partial class Xoa : Form
    {
        private IFirebaseClient firebaseClient;
        private int SL;
        public string textID="";
        private int canhbaoID;
        public Xoa()
        {
            InitializeComponent();
            InitializeFirebaseClient();
            laysl();
            if(textID.Length > 0)
            {
                cboID.Text = textID;
            }
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
        private async void bntXoa_Click(object sender, EventArgs e)
        {
            errorProvider1.Clear();
            kiemtraID();
            if(canhbaoID == 1)
            {
                MessageBox.Show("Invalid ID");
            }
            else
            {
                string matrc, masau;
                matrc = await GetDataFromFirebase(cboID.Text+"/zmatruoc");
                masau = await GetDataFromFirebase(cboID.Text + "/zmasau");
                if(matrc != "0")
                {
                    int p;
                    p = 10 - matrc.Length;

                    for (int m = 0; m < p; m++)
                    {
                        matrc = "0" + matrc;
                    }
                    if (masau != "0")
                    {
                        int l;
                        l = 10 - masau.Length;

                        for (int m = 0; m < l; m++)
                        {
                            masau = "0" + masau;
                        }
                        long numbermasau = long.Parse(masau);
                        long numbermatrc = long.Parse(matrc);
                        FirebaseResponse responseThaydoimasau = await firebaseClient.SetAsync(matrc+"/zmasau",numbermasau);
                        FirebaseResponse responseThaydoimatruoc = await firebaseClient.SetAsync(masau + "/zmatruoc", numbermatrc);
                    }
                    else
                    {
                        FirebaseResponse responseThaydoimasau = await firebaseClient.SetAsync(matrc + "/zmasau", 0);
                    }
                    
                }
                else
                {
                    int l;
                    l = 10 - masau.Length;

                    for (int m = 0; m < l; m++)
                    {
                        masau = "0" + masau;
                    }
                    long numbermasau = long.Parse(masau);
                    FirebaseResponse responseThaydoimasau = await firebaseClient.SetAsync(masau + "/zmatruoc", 0);
                    FirebaseResponse responseThaydoimadau = await firebaseClient.SetAsync("madau", numbermasau);
                }
                try
                {
                    FirebaseResponse responseXoa = await firebaseClient.DeleteAsync(cboID.Text);
                    SL--;
                    FirebaseResponse responseThaydoimasau = await firebaseClient.SetAsync("SL", SL);
                    Console.WriteLine("Successfully deleted!");
                    MessageBox.Show("Successfully deleted");
                    cboID.Items.Clear();
                    laysl();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error while deleting: {ex.Message}");
                }
            }
        }

        private void bntXem_Click(object sender, EventArgs e)
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
                    MessageBox.Show("Please enter the correct ID or select an ID");
                    cb = 0;
                }
                else
                {
                    this.Hide();
                    Xem frm9 = new Xem(cboID.Text);
                   // frm9.textID = cboID.Text;
                    frm9.ShowDialog();
                    frm9 = null;
                    this.Show();

                }
            }
            else
            {
                MessageBox.Show("You haven't selected an ID");
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}
