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
    public partial class main : Form
    {
        private string User_id;
        public main()
        {
            InitializeComponent();
        }
        public main(string user_id)
        {
            InitializeComponent();
            this.User_id = user_id;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            treeLoding();
        }

        private void treeLoding()
        {
            this.treeView1.Nodes.Clear();
            TreeNode front = new TreeNode("全部");
            try
            {
                MySqlDataReader reader = InternetBarBLL.computer.select_roompos();
                if (reader.HasRows)
                {
                    while(reader.Read())
                    {
                        TreeNode node = new TreeNode();
                        node.Text = reader[1].ToString().Trim();
                        node.Tag = reader[0].ToString().Trim();
                        front.Nodes.Add(node);
                    }
                }
                this.treeView1.Nodes.Add(front);
                reader.Close();
                //this.listView1.Items.Clear();
                //reader = InternetBarBLL.computer

            }
            catch (MySqlException ex)
            {
                MessageBox.Show("数据库异常" + ex.Message);
            }
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            this.listView1.Items.Clear();
            MySqlDataReader reader;
            if (this.treeView1.SelectedNode.Parent != null)
            {
                reader = InternetBarBLL.computer.select_computer(this.treeView1.SelectedNode.Tag.ToString().Trim());
                listloding(reader);
            }
        }

        private void listloding(MySqlDataReader reader)
        {
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    ListViewItem item = new ListViewItem();
                    if ("0".Equals(reader[2].ToString().Trim()))
                    {
                        item.ImageIndex = 0;
                        item.Text = reader[0].ToString().Trim();
                    }
                   else if ("1".Equals(reader[2].ToString().Trim()))
                    {
                        item.ImageIndex = 1;
                        item.Text = reader[0].ToString().Trim();
                    }
                    else if ("-1".Equals(reader[2].ToString().Trim()))
                    {
                        item.ImageIndex = 2;
                        item.Text = reader[0].ToString().Trim();
                    }
                    this.listView1.Items.Add(item);
                }
            }
            reader.Close();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int count = this.listView1.SelectedItems.Count;
            if (count > 0)
            {
                if (this.listView1.SelectedItems[0].ImageIndex == 0)
                {
                    string text = this.listView1.SelectedItems[0].Text;
                    bool k = InternetBarBLL.computer.use_computer(text, this.User_id);
                    if(k)
                        this.listView1.SelectedItems[0].ImageIndex = 1;
                }
                else if (this.listView1.SelectedItems[0].ImageIndex == 1)
                {
                    MessageBox.Show("该电脑已经被使用！请选用其它空闲电脑");
                }
                else if (this.listView1.SelectedItems[0].ImageIndex == 2)
                {
                    MessageBox.Show("该电脑正在维护，不能使用！请选用其它空闲电脑");
                }
            }
        }

        private void main_Load(object sender, EventArgs e)
        {

        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            Account account = new Account(this.User_id);
            account.ShowDialog();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            InternetBarBLL.user.CheckOut(this.User_id);
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            InternetBarDAL.User_Info _user_info = new InternetBarDAL.User_Info();
            MySqlDataReader reader = InternetBarDAL.User_Info.QueryUser_Info(this.User_id);
            if (reader.HasRows)
            {
                reader.Read();
                _user_info.UserID = reader.GetString(0);
                _user_info.UserName = reader.GetString(1);
                _user_info.UserAge = reader.GetString(2);
                _user_info.UserGender = reader.GetString(3);
                _user_info.UserTele = reader.GetString(4);
            }
            reader.Close();
            double balance = 0;
            reader = InternetBarDAL.User_Info.balance(this.User_id);
            if (reader.HasRows)
            {
                reader.Read();
                balance = reader.GetDouble(0);

            }
            reader.Close();
            Personal personal = new Personal(_user_info, balance);
            personal.ShowDialog();
        }
    }
}
