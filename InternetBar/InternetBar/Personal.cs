using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace InternetBar
{
    public partial class Personal : Form
    {
        private InternetBarDAL.User_Info _User_info;
        private double Balance;
        public Personal()
        {
            InitializeComponent();
        }

        public Personal(InternetBarDAL.User_Info _user_info, double balance)
        {
            this._User_info = _user_info;
            this.Balance = balance;
            InitializeComponent();
        }

        private void Personal_Load(object sender, EventArgs e)
        {
            this.label7.Text = this._User_info.UserName;
            this.label8.Text = this._User_info.UserID;
            this.label9.Text = this._User_info.UserAge;
            this.label10.Text = this._User_info.UserGender;
            this.label11.Text = this._User_info.UserTele;
            this.label12.Text = this.Balance.ToString().Trim();
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            tele tel = new tele(this._User_info.UserID);
            tel.ShowDialog();
            MySqlDataReader reader = InternetBarDAL.User_Info.QueryUser_Info(this._User_info.UserID);
            if (reader.HasRows)
            {
                reader.Read();
                this.label11.Text = reader.GetString(4);
            }
            reader.Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
