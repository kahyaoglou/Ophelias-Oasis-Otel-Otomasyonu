using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Sql;
using System.Data.SqlClient;

namespace VeriTabaniProje
{
    public class Musteri
    {
        public int ID { get; set; }
        public int RezervasyonTipID { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public string TC { get; set; }
        public string TelefonNo { get; set; }
        public string Mail { get; set; }
        public string KrediKartNo { get; set; }
        public string CVV { get; set; }
        public string SonKullanmaTarihi { get; set; }

        public RezervasyonFiyat RezervasyonFiyat;

        public frmCheckIn frmCheckIn;

        public DataBase db;

        public Musteri()
        {
            DataBase data = new DataBase();
            this.db = data;
            RezervasyonFiyat r= new RezervasyonFiyat();
            this.RezervasyonFiyat= r;
            frmCheckIn f= new frmCheckIn();
            this.frmCheckIn = f;
        }

        public void musteriBilgileriniVeritabaninaYukle()
        {
            
            db.baglanti.Open();
            SqlCommand kayitekle = new SqlCommand("insert into Musteri (MusteriAd,MusteriSoyad,TcNo,Mail,TelefonNo) values (@p1,@p2,@p3,@p4,@p5)", db.baglanti);

            kayitekle.Parameters.AddWithValue("@p1", Ad);
            kayitekle.Parameters.AddWithValue("@p2", Soyad);
            kayitekle.Parameters.AddWithValue("@p3", TC);
            kayitekle.Parameters.AddWithValue("@p4", Mail);
            kayitekle.Parameters.AddWithValue("@p5", TelefonNo);

            kayitekle.ExecuteNonQuery();

        }

        public void musteriIDCek()
        {
            SqlCommand kmt = new SqlCommand("select MusteriID from Musteri where MusteriAd='" + Ad + "'");
            SqlDataReader dr;
            kmt.Connection = db.baglanti;
            dr = kmt.ExecuteReader();
            if (dr.Read())
            {
                ID = Convert.ToInt32(dr["MusteriID"]);
            }
            db.baglanti.Close();

        }

        public void IDyiRezervasyonTablosunaEkle(int fiyat, DateTimePicker giris, DateTimePicker cikis)
        {
            
            RezervasyonTipID=RezervasyonFiyat.RezervasyonTipID;
            db.baglanti.Open();
            SqlCommand rezervasyonEkle = new SqlCommand("insert into Rezervasyon (MusteriID,RezervasyonTipID , GirisTarihi, CikisTarihi, Fiyat) values (@q1,@q2,@q3, @q4, @q5)  ", db.baglanti);
            rezervasyonEkle.Parameters.AddWithValue("@q1", ID);
            rezervasyonEkle.Parameters.AddWithValue("@q2", RezervasyonFiyat.RezervasyonTipID);
            rezervasyonEkle.Parameters.AddWithValue("@q3", giris.Value);
            rezervasyonEkle.Parameters.AddWithValue("@q4", cikis.Value);
            rezervasyonEkle.Parameters.AddWithValue("@q5", fiyat);

            rezervasyonEkle.ExecuteNonQuery();
            db.baglanti.Close();
        }

        public void KrediTablosunaEkle(string krediNo, string cvv, DateTimePicker sonKullanma)
        {

            db.baglanti.Open();


            SqlCommand rezervasyonEkle = new SqlCommand("insert into Kredi (MusteriID, KartNo, SonKullanma, Cvc) values (@q1,@q2,@q3, @q4)  ", db.baglanti);
            rezervasyonEkle.Parameters.AddWithValue("@q1", ID);
            rezervasyonEkle.Parameters.AddWithValue("@q2", krediNo);
            rezervasyonEkle.Parameters.AddWithValue("@q3", sonKullanma.Value);
            rezervasyonEkle.Parameters.AddWithValue("@q4", cvv);

            rezervasyonEkle.ExecuteNonQuery();
            db.baglanti.Close();
        }
    }
}