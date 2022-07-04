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
    public partial class CalisanGiris : Form
    {
        public CalisanGiris()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataBase db = new DataBase();

            SqlCommand giris = new SqlCommand("SELECT * FROM Personel where Mail='" + txtMail.Text + "' AND Password='" + txtPass.Text + "'");
            db.baglanti.Open();
            giris.Connection = db.baglanti;
            SqlDataReader kontrol = giris.ExecuteReader();

            if (kontrol.Read())
            {

                switch (kontrol["Gorev"])
                {
                    case true:
                        Yonetici yonetici = new Yonetici();
                        yonetici.Show();
                        this.Hide();
                        break;
                    case false:
                        Personel personel = new Personel();
                        personel.Show();
                        this.Hide();
                        break;
                 default:
                 break;
                }
            }
            else
            {
                MessageBox.Show("Kullanıcı adı veya şifre hatalı tekrar giriniz");

            }
            db.baglanti.Close();
        }
    }
}
