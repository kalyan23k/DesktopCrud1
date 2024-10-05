using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DesktopCrud1
{
    public partial class DesktopCrud : Form
    {
        public DesktopCrud()
        {
            InitializeComponent();
            txtTeacherId.TextChanged += txtTeacherId_TextChanged;
        }
        private void txtTeacherId_TextChanged(object sender, EventArgs e)
        {
            bool isNotEmpty = !string.IsNullOrWhiteSpace(txtTeacherId.Text);
            btnUpdate.Enabled = isNotEmpty;
            btnDelete.Enabled = isNotEmpty;
        }

        SqlConnection con;
        SqlCommand cmd;

        private void DesktopCrud_Load(object sender, EventArgs e)
        {
            con = new SqlConnection("Data Source=DESKTOP-QSEHQ5T\\SQLEXPRESS02;Integrated Security=True;Initial Catalog=SAMPLEDATA");
            cmd = new SqlCommand();
            cmd.Connection = con;
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            string Query = $"insert into teacher values('{txtTeacherId.Text.ToString()}','{txtName.Text}','{txtAddress.Text}','{txtSalary.Text.ToString()}')";
            cmd.CommandText = Query;
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                string Query = "update teacher set name='" + txtName.Text + "',Address='" + txtAddress.Text + "',salary='" + txtSalary.Text.ToString() + "' where TeacherId='" + txtTeacherId.Text.ToString() + "' ";
                cmd.CommandText = Query;
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {
                throw ex;

            }


        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                string Query = $"delete teacher where TeacherId ='{txtTeacherId.Text.ToString()}'";
                cmd.CommandText = Query;
                cmd.ExecuteNonQuery();
                con.Close();

            }
            catch (Exception ex)
            {
                throw ex;

            }
        }

        private void btnShowall_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                SqlCommand cmd=con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                string Query = $"select * from teacher";
                cmd.CommandText = Query;
                cmd.ExecuteNonQuery();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                DGV.DataSource = dt;
            }
            catch (Exception ex) {
                throw ex;
            }
        }
    }
}   
