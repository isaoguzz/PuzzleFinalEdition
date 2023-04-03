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
    public partial class ScoreTable : Form
    {
        public ScoreTable()
        {
            InitializeComponent();
        }

        private void ScoreTable_Load(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(@"Server=tcp:isaoguz.database.windows.net,1433;Initial Catalog=puzzleGame;Persist Security Info=False;User ID=isaoguz;Password=Yaz,Lab133;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");

            connection.Open();
            string sqlList = "Select pu.userName, ps.userMove, ps.userScore from puzzleGameUsers as pu JOIN puzzleGameScores as ps ON pu.userId = ps.userId  ";
            SqlCommand command1 = new SqlCommand(sqlList, connection);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(command1);
            DataTable data = new DataTable();
            sqlDataAdapter.Fill(data);
            dataGridView1.DataSource = data;
            connection.Close();
            dataGridView1.Columns[0].Width = 217;
            dataGridView1.Columns[1].Width = 210;
            dataGridView1.Columns[2].Width = 210;
            dataGridView1.Columns[0].HeaderText = "Player Name";
            dataGridView1.Columns[1].HeaderText = "Move";
            dataGridView1.Columns[2].HeaderText = "Score";
        }
    }
}
