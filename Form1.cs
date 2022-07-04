using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;


namespace VeriTabaniProje
{
    public partial class Form1 : Form
    {
        DataBase db = new DataBase();
        Musteri m = new Musteri(); 
        
        public void verileriGoster(string veriler)
        {
            SqlDataAdapter dz = new SqlDataAdapter(veriler, db.baglanti);
            DataSet ds = new DataSet();
            dz.Fill(ds);
            dgvDegisiklik.DataSource = ds.Tables[0];
            db.baglanti.Close();

        }
        public Form1()
        {
            InitializeComponent();
        }
        
        private void Form1_Load(object sender, EventArgs e)
        {
            grpStandartRezervasyon.Enabled = false;
            grpYeniTarih.Enabled = false;
        }
        private void button1_Click(object sender, EventArgs e)
        {

            m.Ad=txtAd.Text;
            m.Soyad=txtSoyad.Text;
            m.TC = txtRezervasyonTc.Text;
            m.Mail=txtEmail.Text; 
            m.TelefonNo=txtTelefonNo.Text;
            //m.RezervasyonFiyat.RezervasyonTipID

            if (grpStandartRezervasyon.Enabled == true )
            {
                m.musteriBilgileriniVeritabaninaYukle();
                m.musteriIDCek();
                m.IDyiRezervasyonTablosunaEkle(Convert.ToInt32(lblFiyat.Text), dtpRezervasyonTarihi, dtpCikisTarihi);
                m.KrediTablosunaEkle(txtKrediKarti.Text, txtCvv.Text, dtpSonKullanmaTarihi);
            }
            else
            {
                m.musteriBilgileriniVeritabaninaYukle();
                m.musteriIDCek();
                m.IDyiRezervasyonTablosunaEkle(Convert.ToInt32(lblFiyat.Text), dtpRezervasyonTarihi, dtpCikisTarihi);
                
            }
            grpStandartRezervasyon.Enabled = false;


            MessageBox.Show("Rezervasyon Baþarýlý");

        }

        private void btnAra_Click(object sender, EventArgs e)
        {
            db.baglanti.Open();
            string veriler = "SELECT t1.TcNo,t1.MusteriAd,t1.MusteriSoyad,T2.RezervasyonId FROM " +
                "Musteri AS t1 INNER JOIN Rezervasyon AS t2 ON t1.MusteriID = t2.MusteriID where TcNo = '" +txtIptalTC.Text + "' ";
            SqlCommand komut = new SqlCommand(veriler, db.baglanti);
            komut.ExecuteNonQuery();
            db.baglanti.Close();
            verileriGoster(veriler);
            grpYeniTarih.Enabled = true;

        }

        private void btnRandevuIptal_Click(object sender, EventArgs e)
        {

            db.baglanti.Open();
            foreach (DataGridViewRow dgrow in dgvDegisiklik.SelectedRows)
            {
                if (dgvDegisiklik.RowCount > 0)
                {
                    string kayit = "update Rezervasyon set RezervasyonDurum=@RD where RezervasyonID=@a1";
                    SqlCommand komut = new SqlCommand(kayit, db.baglanti);
                    komut.Parameters.AddWithValue("@RD", 0);
                    komut.Parameters.AddWithValue("@a1", dgvDegisiklik.SelectedCells[3].Value);
                    komut.ExecuteNonQuery();
                    db.baglanti.Close();
                    komut.Parameters.Clear();

                }
            }

            MessageBox.Show("Randevu Ýptal Edilmiþtir");
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            RezervasyonFiyat r = new RezervasyonFiyat();
            db.baglanti.Open();
            foreach (DataGridViewRow dgrow in dgvDegisiklik.SelectedRows)
            {
                if (dgvDegisiklik.RowCount > 0)
                {
                    lblFiyat.Text = ((r.SabitFiyat * 110) / 100).ToString();
                    string kayit = "update Rezervasyon set GirisTarihi=@Gt,CikisTarihi=@Ct,Fiyat=@fiyat where RezervasyonID=@a1";
                    SqlCommand komut = new SqlCommand(kayit, db.baglanti);
                    komut.Parameters.AddWithValue("@Gt",dtpYeniGiris.Value);
                    komut.Parameters.AddWithValue("@fiyat",Convert.ToInt32(lblFiyat.Text));
                    komut.Parameters.AddWithValue("@Ct", dtpYeniCikis.Value);
                    komut.Parameters.AddWithValue("@a1", dgvDegisiklik.SelectedCells[3].Value);
                    komut.ExecuteNonQuery();
                    db.baglanti.Close();
                    komut.Parameters.Clear();

                }
                MessageBox.Show("Randevunuz Baþarýyla güncellendi");
                MessageBox.Show("Yeni Tutar: "+ lblFiyat.Text);
            }

        }

        private void dtpCikisTarihi_CloseUp(object sender, EventArgs e)
        {
            
            m.RezervasyonFiyat.Fiyat(dtpRezervasyonTarihi, dtpCikisTarihi, grpStandartRezervasyon);
            lblFiyat.Text = m.RezervasyonFiyat.Mesaj;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            CalisanGiris c = new CalisanGiris();
            c.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            frmCheckIn frmCheckIn = new frmCheckIn();
            frmCheckIn.Show();
            this.Hide();
        }
    }
}