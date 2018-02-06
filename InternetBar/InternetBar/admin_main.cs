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
    public partial class admin_main : Form
    {
        private string admin_Id;
        public admin_main()
        {
            InitializeComponent();
        }

        public admin_main(string admin_id)
        {
            this.admin_Id = admin_id;
            InitializeComponent();
        }
        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {

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

        private void admin_main_Load(object sender, EventArgs e)
        {

        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            Add_admin add_admin = new Add_admin();
            add_admin.ShowDialog();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            Add_room add_room = new Add_room();
            add_room.ShowDialog();
        }

        private void toolStripButton3_Click_1(object sender, EventArgs e)
        {
            Add_computer add_com = new Add_computer();
            add_com.ShowDialog();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
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
                    while (reader.Read())
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

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int count = this.listView1.SelectedItems.Count;
            if (count > 0)
            {
                if (this.listView1.SelectedItems[0].ImageIndex == 1)
                {
                    MessageBox.Show("用户正在使，无法维护！");
                }
                else if (this.listView1.SelectedItems[0].ImageIndex == 2)
                {
                    string text = this.listView1.SelectedItems[0].Text;
                    string state = "0";
                    InternetBarBLL.computer.state_change(text, state);
                    this.listView1.SelectedItems[0].ImageIndex = 0;
                    MessageBox.Show("修改成功，该电脑已经可以正常使用！");
                }

                else if (this.listView1.SelectedItems[0].ImageIndex == 0)
                {
                    string text = this.listView1.SelectedItems[0].Text;
                    string state = "-1";
                    InternetBarBLL.computer.state_change(text, state);
                    this.listView1.SelectedItems[0].ImageIndex = 2;
                    MessageBox.Show("修改成功，该电脑已经无法使用！");
                }
            }
        }
    }
}
