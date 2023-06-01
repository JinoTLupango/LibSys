using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Libsys
{
    public partial class Form1 : Form
    {
        private OleDbConnection con;
        public Form1()
        {
            InitializeComponent();
            con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=\"D:\\JinoRepos\\Jino Project\\Libsys\\Libsys\\libsys.accdb\"");
            con.Open();
            grid1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
        }
        private void loadDatagrid()
        {
            OleDbDataAdapter adapter = new OleDbDataAdapter("SELECT * FROM Book ORDER BY Accession_Number ASC", con);
            DataTable book = new DataTable();
            adapter.Fill(book);
            grid1.DataSource = book;
            grid1.ReadOnly = true;
        }

        private void grid1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.grid1.Rows[e.RowIndex];
                txtno.Text = row.Cells["Accession_Number"].Value.ToString();
                txttitle.Text = row.Cells["Title"].Value.ToString();
                txtauthor.Text = row.Cells["Author"].Value.ToString();
            }
        }

        private void txno_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            OleDbCommand cmd = new OleDbCommand("INSERT INTO Book (Accession_Number, Title, Author) VALUES (@Accession_Number, @Title, @Author)", con);
            cmd.Parameters.AddWithValue("@Accession_Number", txtno.Text);
            cmd.Parameters.AddWithValue("@Title", txttitle.Text);
            cmd.Parameters.AddWithValue("@Author", txtauthor.Text);
            cmd.ExecuteNonQuery();
            loadDatagrid();

            MessageBox.Show("Successfully Added.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to edit this?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                OleDbCommand cmd = new OleDbCommand("UPDATE Book SET Title = @Title, Author = @Author WHERE Accession_Number = @Accession_Number", con);
                cmd.Parameters.AddWithValue("@Title", txttitle.Text);
                cmd.Parameters.AddWithValue("@Author", txtauthor.Text);
                cmd.Parameters.AddWithValue("@Accession_Number", txtno.Text);
                cmd.ExecuteNonQuery();
                loadDatagrid();

                MessageBox.Show("Successfully Updated.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            OleDbDataAdapter adapter = new OleDbDataAdapter("SELECT * FROM Book WHERE Accession_Number LIKE '%" + txtSearch.Text + "%' OR Author LIKE '%" + txtSearch.Text + "%' ORDER BY Accession_Number ASC", con);
            DataTable book = new DataTable();
            adapter.Fill(book);
            grid1.DataSource = book;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to delete this record?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                OleDbCommand cmd = new OleDbCommand("DELETE FROM Book WHERE Accession_Number = @Accession_Number", con);
                cmd.Parameters.AddWithValue("@Accession_Number", txtno.Text);
                cmd.ExecuteNonQuery();
                loadDatagrid();

                MessageBox.Show("Successfully Deleted.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

                txtno.Clear();
                txttitle.Clear();
                txtauthor.Clear();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }
    }
}

