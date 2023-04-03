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

namespace YazLabYabBoz
{
    public partial class Login : Form
    {
  

        SqlConnection connection = new SqlConnection(@"Server=tcp:isaoguz.database.windows.net,1433;Initial Catalog=puzzleGame;Persist Security Info=False;User ID=isaoguz;Password=Yaz,Lab133;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");

        public static string userName;
        public static string password;
   
        public Login()
        {
            InitializeComponent();
            userName = textBox1.Text.Trim();
            password = textBox3.Text.Trim();
        }

        private void button2_Click(object sender, EventArgs e)
        {
         
        }

        private void button1_Click(object sender, EventArgs e)
        {
            userName= textBox1.Text.Trim(); 
            password= textBox3.Text.Trim();
            try
            {
                connection.Open();
                string sql = "Select * from puzzleGameUsers where userName=@userName AND userPassword=@userPassword";
                SqlParameter parameter1 = new SqlParameter("userName", userName);
                SqlParameter parameter2 = new SqlParameter("userPassword", password);
                SqlCommand command = new SqlCommand(sql,connection);
                command.Parameters.Add(parameter1);
                command.Parameters.Add(parameter2);
                DataTable data = new DataTable();
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(command);
                sqlDataAdapter.Fill(data);

                if (data.Rows.Count>0)
                {
                    Form1 form1 = new Form1();
                    form1.Show();
                    connection.Close();
                    this.Hide();
                }
               
            }
            catch (Exception)
            {

                MessageBox.Show("Hatalı Kullanıcı Adı veya Şifre...");
            }
         
        }

        private void button3_Click(object sender, EventArgs e)
        {
            connection.Open();
            string sql = "Insert into puzzleGameUsers(userName,userPassword) values(@userName,@userPassword)";
            SqlCommand command = new SqlCommand(sql, connection);
            SqlParameter parameter1 = new SqlParameter("userName", textBox2.Text.Trim());
            SqlParameter parameter2 = new SqlParameter("userPassword", textBox4.Text.Trim());
            command.Parameters.Add(parameter1);
            command.Parameters.Add(parameter2);
            command.ExecuteNonQuery();
            MessageBox.Show("User is saved ! ");
            connection.Close(); 
        }

        private void Login_Load(object sender, EventArgs e)
        {
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            userName = textBox1.Text.Trim();
            password = textBox3.Text.Trim();
            try
            {
                connection.Open();
                string sql = "Select * from puzzleGameUsers where userName=@userName AND userPassword=@userPassword";
                SqlParameter parameter1 = new SqlParameter("userName", userName);
                SqlParameter parameter2 = new SqlParameter("userPassword", password);
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.Add(parameter1);
                command.Parameters.Add(parameter2);
                DataTable data = new DataTable();
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(command);
                sqlDataAdapter.Fill(data);

                if (data.Rows.Count > 0)
                {
                    Form1 form1 = new Form1();
                    form1.Show();
                    connection.Close();
                    this.Hide();
                }

            }
            catch (Exception)
            {

                MessageBox.Show("Hatalı Kullanıcı Adı veya Şifre...");
            }
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            connection.Open();
            string sql = "Insert into puzzleGameUsers(userName,userPassword) values(@userName,@userPassword)";
            SqlCommand command = new SqlCommand(sql, connection);
            SqlParameter parameter1 = new SqlParameter("userName", textBox2.Text.Trim());
            SqlParameter parameter2 = new SqlParameter("userPassword", textBox4.Text.Trim());
            command.Parameters.Add(parameter1);
            command.Parameters.Add(parameter2);
            command.ExecuteNonQuery();
            MessageBox.Show("User is saved ! ");
            connection.Close();
        }
    }
}
