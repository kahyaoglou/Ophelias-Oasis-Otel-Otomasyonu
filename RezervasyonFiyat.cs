using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Sql;
using System.Data.SqlClient;

namespace VeriTabaniProje
{
    public class RezervasyonFiyat
    {
        public int SabitFiyat = 250;
        public double OnodemeliFiyat { get; set; }
        public double AtmısGunOncedenFiyat { get; set; }
        public int RezervasyonTipID { get; set; }
        public string Mesaj { get; set; }
        public DateTime GirisTarihi { get; set; }
        public DateTime CikisTarihi { get; set; }

        public string Fiyat(DateTimePicker giris, DateTimePicker cikis, GroupBox groupBox)
        {
            GirisTarihi = giris.Value;
            CikisTarihi = cikis.Value;

            TimeSpan Gun = CikisTarihi - GirisTarihi;

            if (Math.Abs(Gun.Days) >= 90 && Math.Abs(Gun.Days) <= 365)
            {
                OnodemeliFiyat = (SabitFiyat * 75) / 100;
                Mesaj = OnodemeliFiyat.ToString();
                groupBox.Enabled = true;
                RezervasyonTipID = 1;
            }
            else if (Math.Abs(Gun.Days) < 90 && Math.Abs(Gun.Days) >= 60)
            {
                AtmısGunOncedenFiyat = (SabitFiyat * 85) / 100;
                Mesaj = AtmısGunOncedenFiyat.ToString();
                groupBox.Enabled = false;
                RezervasyonTipID = 2;
            }
            else if (Math.Abs(Gun.Days) < 60)
            {
                Mesaj = SabitFiyat.ToString();
                groupBox.Enabled = true;
                RezervasyonTipID = 3;
            }
            else
            {
                MessageBox.Show("HATA!!!");
            }

            return Mesaj;
        }
    }
}