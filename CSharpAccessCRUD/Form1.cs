using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace CSharpAccessCRUD
{
    public partial class Form1 : Form
    {
        OleDbConnection conn = new OleDbConnection();
        string dbProvider = "PROVIDER=Microsoft.Jet.OLEDB.4.0;";
        string dbSource = "Data Source=D:\\AccessDB\\db_cscrud.mdb;";
        OleDbDataAdapter adapter = new OleDbDataAdapter();
        DataSet ds = new DataSet();
        string currentid = "";

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            conn.ConnectionString = dbProvider + dbSource;
            GetRecords();
        }

        public void GetRecords()
        {
            ds = new DataSet();
            adapter = new OleDbDataAdapter("select * from [tbl_names]", conn);
            adapter.Fill(ds, "tbl_names");

            dataGridView1.DataSource = ds;
            dataGridView1.DataMember = "tbl_names";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ds = new DataSet();
            adapter = new OleDbDataAdapter("insert into [tbl_names] ([firstname], [lastname]) VALUES " +
             "('" + textBox1.Text + "','" + textBox2.Text + "')", conn);
            adapter.Fill(ds, "tbl_names");
            textBox1.Clear();
            textBox2.Clear();
            GetRecords();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int i = dataGridView1.CurrentRow.Index;
            currentid = dataGridView1[0, i].Value.ToString();

            ds = new DataSet();
            adapter = new OleDbDataAdapter("delete from [tbl_names] where id = " + currentid, conn);
            adapter.Fill(ds, "tbl_names");
            GetRecords();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int i = dataGridView1.CurrentRow.Index;
            currentid = dataGridView1[0, i].Value.ToString();
            textBox1.Text = dataGridView1[1, i].Value.ToString();
            textBox2.Text = dataGridView1[2, i].Value.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ds = new DataSet();
            adapter = new OleDbDataAdapter("update [tbl_names] set [firstname] = '" + textBox1.Text + 
                "', [lastname] = '" + textBox2.Text + 
                "' where id = " + currentid, conn);
            adapter.Fill(ds, "tbl_names");
            textBox1.Clear();
            textBox2.Clear();
            GetRecords();
        }
    }
}
