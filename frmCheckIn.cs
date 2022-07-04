using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Sql;
using System.Data.SqlClient;

namespace VeriTabaniProje
{
    public partial class frmCheckIn : Form
    {
        DataBase db = new DataBase();
        public DateTime BugununTarihi { get; set; }
        public DateTime RezervasyonTarihi { get; set; }
        public int OdaNo { get; set; }
        public int MusteriID { get; set; }


        public frmCheckIn()
        {
           InitializeComponent();
        }
        public void ChekinTarihi()
        {        
            foreach (DataGridViewRow dgrow in dataGridView1.SelectedRows)
            {
                if (dataGridView1.RowCount > 0)
                {
                    BugununTarihi=DateTime.Now;
                    RezervasyonTarihi =Convert.ToDateTime(dataGridView1.SelectedCells[4].Value);

                    TimeSpan KalanGun= BugununTarihi - RezervasyonTarihi;

                    if (Math.Abs(KalanGun.Days)==1)
                    {
                        groupBox1.Enabled = true;
                    }
                    else
                    {
                        MessageBox.Show("Check-in yapmaya kalan gün sayınız: "+ Math.Abs(KalanGun.Days));
                    }
                }
            }
        }

        DataGridView x;
        public void verileriGoster(string veriler)
        {
            SqlDataAdapter dz = new SqlDataAdapter(veriler, db.baglanti);
            DataSet ds = new DataSet();
            dz.Fill(ds);
            x.DataSource = ds.Tables[0];
            db.baglanti.Close();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            db.baglanti.Open();
            string veriler = "SELECT t1.MusteriID, t1.TcNo,t1.MusteriAd,t1.MusteriSoyad,T2.GirisTarihi,t2.CikisTarihi FROM Musteri AS t1 INNER JOIN Rezervasyon AS t2 ON t1.MusteriID = t2.MusteriID where Mail ='"+ textBox1.Text +"' ";
            SqlCommand komut = new SqlCommand(veriler, db.baglanti);
            komut.ExecuteNonQuery();
            x = dataGridView1;
            verileriGoster(veriler); 
        }
        private void frmCheckIn_Load(object sender, EventArgs e)
        {
            groupBox1.Enabled= false;
            db.baglanti.Open();
            string veriler = "SELECT OdaNo from Oda where OdaDurum= ' " + 0 + " ' ";
            SqlCommand komut = new SqlCommand(veriler, db.baglanti);
            komut.ExecuteNonQuery();
            x= dataGridView2;
            verileriGoster(veriler);
            db.baglanti.Close();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            db.baglanti.Open();
            foreach (DataGridViewRow dgrow in dataGridView2.SelectedRows)
            {
                if (dataGridView2.RowCount > 0)
                {
                    OdaNo =Convert.ToInt32(dataGridView2.SelectedCells[0].Value);
                    string kayit = "update Oda set OdaDurum=@OD where OdaNo=@a1";
                    SqlCommand komut = new SqlCommand(kayit, db.baglanti);
                    komut.Parameters.AddWithValue("@OD", 1);
                    komut.Parameters.AddWithValue("@a1", OdaNo);
                    komut.ExecuteNonQuery();
                    db.baglanti.Close();
                    komut.Parameters.Clear();
                }
            }
            MessageBox.Show("Oda rezerve edilmiştir");
        }
        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            ChekinTarihi();
        } 
    }
}
