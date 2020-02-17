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

namespace PansiyonKayıt
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-72PGNVV\\SQLEXPRESS;Initial Catalog=dbPansiyon;User ID=sa;password=1234;");

        private void Form1_Load(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("Select * from bosoda",baglanti);
            SqlDataReader oda = komut.ExecuteReader();

            while (oda.Read())
            {
                comboBox1.Items.Add(oda[0].ToString());
            }
            baglanti.Close();
        }

        private void verileriGoster()
        {
            listView1.Items.Clear();
            baglanti.Open();
            SqlCommand komut = new SqlCommand("Select * from musteriler", baglanti);
            SqlDataReader oku = komut.ExecuteReader();

            while (oku.Read())
            {
                ListViewItem ekle = new ListViewItem();
                ekle.Text = oku["id"].ToString();
                ekle.SubItems.Add(oku["Ad"].ToString());
                ekle.SubItems.Add(oku["Soyad"].ToString());
                ekle.SubItems.Add(oku["OdaNo"].ToString());
                ekle.SubItems.Add(oku["GTarih"].ToString());
                ekle.SubItems.Add(oku["Telefon"].ToString());
                ekle.SubItems.Add(oku["Hesap"].ToString());
                ekle.SubItems.Add(oku["CTarih"].ToString());

                listView1.Items.Add(ekle);
            }
            baglanti.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            verileriGoster();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("insert into musteriler (id,Ad,Soyad,OdaNo,GTarih,Telefon,Hesap,CTarih) values ('" + textBox1.Text.ToString() + "','" + textBox2.Text.ToString() + "','" + textBox3.Text.ToString() + "','" + comboBox1.Text.ToString() + "','" + dateTimePicker1.Text.ToString() + "','" + textBox5.Text.ToString() + "','" + textBox6.Text.ToString() + "','" + dateTimePicker2.Text.ToString() + "')", baglanti);
            komut.ExecuteNonQuery();

            komut.CommandText=("insert into doluoda (Dolu) values ('" + comboBox1.Text + "')");
            komut.ExecuteNonQuery();

            komut.CommandText = ("delete from bosoda where Bos='" + comboBox1.Text + "'");
            komut.ExecuteNonQuery();

            baglanti.Close();
            verileriGoster();

            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox5.Clear();
            textBox6.Clear();
        }

        int id=0;

        private void button3_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("delete from musteriler where id=(" + id + ")", baglanti);
            komut.ExecuteNonQuery();

            komut.CommandText = ("insert into bosoda(Bos) values ('" + comboBox1.Text + "')");
            komut.ExecuteNonQuery();

            komut.CommandText = ("delete from doluoda where Dolu='"+comboBox1.Text+"'");
            komut.ExecuteNonQuery();

            baglanti.Close();
            verileriGoster();
        }

        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            id = int.Parse(listView1.SelectedItems[0].SubItems[0].Text);
            textBox1.Text = listView1.SelectedItems[0].SubItems[0].Text;
            textBox2.Text = listView1.SelectedItems[0].SubItems[1].Text;
            textBox3.Text = listView1.SelectedItems[0].SubItems[2].Text;
            comboBox1.Text = listView1.SelectedItems[0].SubItems[3].Text;
            dateTimePicker1.Text = listView1.SelectedItems[0].SubItems[4].Text;
            textBox5.Text = listView1.SelectedItems[0].SubItems[5].Text;
            textBox6.Text = listView1.SelectedItems[0].SubItems[6].Text;
            dateTimePicker2.Text = listView1.SelectedItems[0].SubItems[7].Text;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("update musteriler set id='" + textBox1.Text.ToString() + "',Ad='" + textBox2.Text.ToString() + "',Soyad='" + textBox3.Text.ToString() + "',OdaNo='" + comboBox1.Text.ToString() + "',GTarih='" + dateTimePicker1.Text.ToString() + "',Telefon='" + textBox5.Text.ToString() + "',Hesap='" + textBox6.Text.ToString() + "',CTarih='" + dateTimePicker2.Text.ToString() + "'where id="+id+"", baglanti);
            komut.ExecuteNonQuery();
            baglanti.Close();
            verileriGoster();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select * from musteriler where ad like '%" + textBox7.Text + "%'", baglanti);
            SqlDataReader oku = komut.ExecuteReader();

            while (oku.Read())
            {
                ListViewItem ekle = new ListViewItem();
                ekle.Text = oku["id"].ToString();
                ekle.SubItems.Add(oku["Ad"].ToString());
                ekle.SubItems.Add(oku["Soyad"].ToString());
                ekle.SubItems.Add(oku["OdaNo"].ToString());
                ekle.SubItems.Add(oku["GTarih"].ToString());
                ekle.SubItems.Add(oku["Telefon"].ToString());
                ekle.SubItems.Add(oku["Hesap"].ToString());
                ekle.SubItems.Add(oku["CTarih"].ToString());

                listView1.Items.Add(ekle);
            }
            baglanti.Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            textBox4.Text = textBox4.Text.Substring(1) + textBox4.Text.Substring(0, 1);
        }
    }
}
