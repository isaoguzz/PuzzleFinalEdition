using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Data.SqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using static System.Net.WebRequestMethods;

namespace YazLabYabBoz
{
    public partial class Form1 : Form
    {
        SqlConnection connection = new SqlConnection(@"Server=tcp:isaoguz.database.windows.net,1433;Initial Catalog=puzzleGame;Persist Security Info=False;User ID=isaoguz;Password=Yaz,Lab133;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");


        public static String imageUrl = null;
        bool wrong = false;
        double bestScore = -100.0;
        double score = 0.0;
        double Move = 0.0;

        public static System.Drawing.Image[] imageList = new System.Drawing.Image[16]; //tanım:image dizisi


        public static System.Drawing.Image mainPicture = System.Drawing.Image.FromFile("C:\\Users\\isa34\\Desktop\\img\\kopek.jpg"); //orjinal resim boyutu

        private LinkedList<System.Windows.Forms.Button> puzzleButtons;
        private LinkedList<Image> puzzleImages;
        public static int xAxis = mainPicture.Width;
        public static int yAxis = mainPicture.Height;
       
        public static System.Drawing.Image resize;
        public static double resizeRatio = 650.0 / xAxis;
        public static int newXAxis = (int)(xAxis /** resizeRatio*/);
        public static int newYAxis = (int)(yAxis /** resizeRatio*/);

        // Kutu boyutu şu an resized resme göre belirleniyor.
        public static int BoxX = newXAxis / 4;
        public static int BoxY = newYAxis / 4;
        public static List<Control> buttons = new List<Control>();
        public static List<Control> controlButtons = new List<Control>();
        public static int buttonStartListX = 178;
        public static int buttonStartListY = 21;
        public static string scoreFileAddress = "enyuksekskor.txt";
        public static string playerAddress = "players.txt";

        public static FileStream scoreFile;
        public static FileStream scoreFileTwo;


        public Form1()
        {
            InitializeComponent();

            resizedImage();
            setButtonList();
            setControlButtons();
            hideControlButtons();

            foreach (var button in buttons)
            {
                button.AllowDrop = true;
                button.DragEnter += dragButtons;
                button.DragDrop += dragAndDropButtons;
                button.MouseDown += mouseDownButtons;
            }

            setButtonPositionandDimensions();  //butonların konum ve boyutlarını ayarla
            setBestScore();  //en yüksek scoru setle
            setPlayers();    //Oyuncuları setle
            label2.Text = "";
            label4.Text = "";

        }

        public void setControlButtons()
        {
            for (int x = 17; x <= 32; x++)
            {
                var buttonName = string.Format("button{0}", x);
                var button = Controls.Find(buttonName, true).First();

                if (button != null)
                {
                    controlButtons.Add(button);
                }
            }
        }

        public void hideControlButtons()
        {
            if (!wrong)
            {
                foreach (var button in controlButtons)
                {
                    button.Visible = false;
                }
                //pictureBox1.Visible = false;
                this.Size = new Size(1500, 450);
            }
        }

        private void btnMix(object sender, EventArgs e)  //Resmi karıştır butonu
        {
            resizedImage();
            if (imageUrl == null)
            {
                MessageBox.Show("Öncelikle Resim Seçmeniz Gereklidir!!!");
                return;
            }
          

            System.Drawing.Image image = System.Drawing.Image.FromFile(imageUrl);
            if (image !=null)
            {
                 image = System.Drawing.Image.FromFile(imageUrl);
            }

            // kontrol butonları 17 - 32 numaralılar
            for (int i = 0; i < controlButtons.Count(); i++)
            {
                controlButtons[i].Width = BoxX;
                controlButtons[i].Height = BoxY;

            }
            for (int i = 0; i < 4; i++) // sutun
            {
                for (int j = 0; j < 4; j++) // satir
                {
                    controlButtons[i * 4 + j].Location = new Point(625 + j * BoxX, 21 + i * BoxY);
                }
            }
            // kontrol butonları bitiş

            //resim parçalama ve image dizisine atama
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    var index = i * 4 + j;
                    imageList[index] = new Bitmap(BoxX, BoxY);
                    var graphics = Graphics.FromImage(imageList[index]);
                    graphics.DrawImage(image, new Rectangle(0, 0, BoxX, BoxY), new Rectangle(j * BoxX, i * BoxY, BoxX, BoxY), GraphicsUnit.Pixel);
                    graphics.Dispose();
                    // kontrol butonları arkaplanını ayarla
                    controlButtons[index].BackgroundImage = imageList[index]; //uretilenSayi a idi
                    // kontrol_butonlari[index].Text = "";
                    // kontrol butonları arkaplanını ayarla bitiş
                }
            }
            Random random_nesnesi = new Random();
            var karisik_sirali_sayilar = Enumerable.Range(0, 16).OrderBy(g => Guid.NewGuid()).Take(16).ToArray();
            int karisik_sirali_dizi_indexi = 0;
            foreach (var button in buttons)
            {
                var sira = karisik_sirali_sayilar[karisik_sirali_dizi_indexi];
                button.BackgroundImage = imageList[sira];
                if (!wrong) {
                    button.Text = "";
                }
                karisik_sirali_dizi_indexi += 1;
            }
            for (int i = 0; i < 16; i++)
            {
                var sonuc = rightPlaceMethod(buttons[i]);
                if (sonuc)
                {
                    butonu_sabitle(buttons[i]);
                    puan_ekle();
                    btnKaristir.Enabled = false;
                }
                else
                {
                    // puan_kir();
                }
            }
        }

        private void mouseDownButtons(object sender, EventArgs e)
        {
            var evsahibi_buton = ((System.Windows.Forms.Button)sender);
            Console.WriteLine("Called: {0}_MouseDown", evsahibi_buton.Name);
            evsahibi_buton.DoDragDrop(evsahibi_buton, DragDropEffects.Move);
        }

        void dragButtons(object sender, DragEventArgs e)
        {
            Console.WriteLine("Called: dragButtons");
            e.Effect = DragDropEffects.Move;
        }

        void dragAndDropButtons(object sender, DragEventArgs e)
        {
            var gelen_buton = ((System.Windows.Forms.Button)e.Data.GetData(typeof(System.Windows.Forms.Button)));
            var evsahibi_buton = ((System.Windows.Forms.Button)sender);
            Console.WriteLine("Called: {0}_DragDrop", evsahibi_buton.Name);
            Console.WriteLine(gelen_buton.Name + " buttonu {0} içine sürüklendi", evsahibi_buton.Name);
            if (gelen_buton.Name == evsahibi_buton.Name) { return; }
            Point temp = gelen_buton.Location;
            gelen_buton.Location = evsahibi_buton.Location;
            evsahibi_buton.Location = temp;

            // doğru yerde mi kontrol et, sürüklenen kontrol
            var sonuc = rightPlaceMethod(gelen_buton);
            var dogru_parca_no = satir_bul(gelen_buton) * 4 + sutun_bul(gelen_buton) + 1;
            Console.WriteLine("Sürüklenen Buton {0}, Doğru Parça Buton {1}", gelen_buton.Name, dogru_parca_no);
            if (sonuc)
            {
                butonu_sabitle(gelen_buton);
                puan_ekle();
            }
            else
            {
                puan_kir();
            }


            // doğru yerde mi kontrol et, kayan kontrol
            var sonuc_2 = rightPlaceMethod(evsahibi_buton);
            var dogru_parca_no_2 = satir_bul(evsahibi_buton) * 4 + sutun_bul(evsahibi_buton) + 1;
            Console.WriteLine("Kayan Buton {0}, Doğru Parça Buton {1}", evsahibi_buton.Name, dogru_parca_no_2);
            if (sonuc_2)
            {
                butonu_sabitle(evsahibi_buton);
                puan_ekle();
            }
            else
            {
                // puan_kir();
            }
        }

        void resizedImage()
        {
            Console.WriteLine("Called: resizedImage()");
            resize = (System.Drawing.Image)(new Bitmap(newXAxis, newYAxis));
            var graphics = Graphics.FromImage(resize);
            graphics.DrawImage(mainPicture, new Rectangle(0, 0, newXAxis, newYAxis), new Rectangle(0, 0, xAxis, yAxis), GraphicsUnit.Pixel);
            graphics.Dispose();
            Console.WriteLine("wrong");
            //pictureBox1.Image = mainPicture;
        }

        void setButtonPositionandDimensions()
        {
            Console.WriteLine("Called: setButtonPositionandDimensions()");
            for (int i = 0; i < buttons.Count(); i++)
            {
                buttons[i].Width = BoxX;
                buttons[i].Height = BoxY;

            }
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    buttons[i * 4 + j].Location = new Point(buttonStartListX + j * BoxX, buttonStartListY + i * BoxY);

                }
            }
        }
        void setBestScore()
        {
            var list = System.IO.File.ReadLines(scoreFileAddress.ToString()).Select(line => double.Parse(line.Trim())).ToList();
            foreach (var item in list)
            {
                if (item > bestScore)
                {
                    bestScore = item;
                }
            }
            skor_label.Text = bestScore.ToString();
        }


        void setPlayers()
        {
            var list = System.IO.File.ReadLines(playerAddress.ToString()).Select(line => Convert.ToString(line.Trim()));
        }

        void setButtonList()
        {

            for (int x = 1; x <= 16; x++)
            {
                var buttonName = string.Format("button{0}", x);
                var button = Controls.Find(buttonName, true).First();

                if (button != null)
                {
                    buttons.Add(button);
                }
            }
        }


        bool rightPlaceMethod(Control button)
        {
            var parca_resim_indexi = satir_bul(button) * 4 + sutun_bul(button);
            System.Drawing.Image parca_resim = imageList[parca_resim_indexi];
            var sonuc = isRightPlace(button, parca_resim);
            return sonuc;
        }

        bool isRightPlace(Control buton, System.Drawing.Image image)
        {
            LinkedList<bool> sonuclar = new LinkedList<bool>();
            Bitmap bmp = new Bitmap(image);
            Bitmap bmp2 = new Bitmap(buton.BackgroundImage);
            for (int x = 0; x < image.Width; x++)
            {
                for (int y = 0; y < image.Height; y++)
                {
                    Color pixel1 = bmp.GetPixel(x, y);
                    Color pixel2 = bmp2.GetPixel(x, y);
                    if (pixel1.R == pixel2.R && pixel1.G == pixel2.G && pixel1.B == pixel2.B)
                    {
                        sonuclar.AddLast(true);
                    }
                    else
                    {
                        sonuclar.AddLast(false);
                    }
                }
            }
            var uniq_sonuclar = sonuclar.Distinct();
            if (uniq_sonuclar.First().Equals(true) && uniq_sonuclar.Count() == 1)
            {
                Console.WriteLine("Doğru yerde!");
                return true;
            }
            else
            {
                Console.WriteLine("Yanlış yerde!");
                return false;
            }
        }
   

    int satir_bul(Control button)
        {
            var satir_indisi = (button.Location.Y - buttonStartListY) / BoxY;
            return satir_indisi;
        }

        int sutun_bul(Control button)
        {
            var sutun_indisi = (button.Location.X - buttonStartListX) / BoxX;
            return sutun_indisi;
        }

        void puan_ekle()
        {
            // puan ekle 
            score = score + 5;
            Move = Move + 1;
            skorLabelUpdateAndWriteFileFinish();
        }

        void puan_kir()
        {
            // puan kır
            score = score - 10;
            Move = Move + 1;
            skorLabelUpdateAndWriteFileFinish();
        }

        void skorLabelUpdateAndWriteFileFinish()
        {
            label4.Text = score.ToString();
            if (score > bestScore)
            {
                skor_label.Text = score.ToString();
            }
            if (puzzle_bitti_mi())
            {
                // Puzzle bitti skoru dosyanın sonuna ekle
                label2.Text = "Oyun Bitti!";
                scoreFile = new FileStream(scoreFileAddress, FileMode.Append, FileAccess.Write);
                StreamWriter dosya = new StreamWriter(scoreFile);
                dosya.WriteLine(score.ToString());
                dosya.Close();


                scoreFileTwo = new FileStream(playerAddress, FileMode.Append, FileAccess.Write);
                StreamWriter dosya2 = new StreamWriter(scoreFileTwo);
                dosya2.WriteLine(Login.userName.ToString() + " || " + Move.ToString() + " || " + score.ToString());
                dosya2.Close();


                connection.Open();
                string sqlList = "Select userId from puzzleGameUsers where userName=@userName";
                SqlParameter parameter3 = new SqlParameter("userName", Login.userName);
                SqlCommand command1 = new SqlCommand(sqlList, connection);
                command1.Parameters.Add(parameter3);
                DataTable data = new DataTable();
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(command1);
                sqlDataAdapter.Fill(data);
                int userId = (int)data.Rows[0]["userId"];
                if (data.Rows.Count>0)
                {
                    string sql = "Insert into puzzleGameScores(userId,userScore,userMove) values(@userId,@userScore,@userMove)";
                    SqlCommand command = new SqlCommand(sql, connection);
                    SqlParameter parameter1 = new SqlParameter("userScore", score.ToString().Trim());
                    SqlParameter parameter2 = new SqlParameter("userMove", Move.ToString().Trim());
                    SqlParameter parameter5 = new SqlParameter("userId", userId);
                    command.Parameters.Add(parameter5);
                    command.Parameters.Add(parameter1);
                    command.Parameters.Add(parameter2);
                    command.ExecuteNonQuery();
                    connection.Close();
                    MessageBox.Show("User is saved ! ");
                }
            }
        }
        bool puzzle_bitti_mi()
        {
            bool sonuc = true;
            foreach (var button in buttons)
            {
                if (button.AllowDrop == true)
                {
                    sonuc = false;
                }
            }
            return sonuc;

        }
        void butonu_sabitle(Control gelen_buton)
        {
            // butonu sabitle ve sürüklenemez yap
            gelen_buton.AllowDrop = false;
            gelen_buton.DragEnter -= dragButtons;
            gelen_buton.DragDrop -= dragAndDropButtons;
            gelen_buton.MouseDown -= mouseDownButtons;
            Console.WriteLine("{0} Sabitlendi.", gelen_buton.Name);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            if (bestScore == -100)
            {
                skor_label.Text = "0";

            }
            //button35.Visible= false;
            label6.Text = Login.userName;


            connection.Open();
            string sqlList = "Select pu.userName, ps.userMove, ps.userScore from puzzleGameUsers as pu JOIN puzzleGameScores as ps ON pu.userId = ps.userId  ";
            SqlCommand command1 = new SqlCommand(sqlList, connection);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(command1);
            DataTable data = new DataTable();
            sqlDataAdapter.Fill(data);
            dataGridView1.DataSource = data;
            connection.Close();
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.Columns[0].Width = 100;
            dataGridView1.Columns[1].Width = 60;
            dataGridView1.Columns[2].Width = 120;
            dataGridView1.Columns[0].HeaderText = "Player Name";
            dataGridView1.Columns[1].HeaderText = "Move";
            dataGridView1.Columns[2].HeaderText = "Score";
        }


        private void button17_Click(object sender, EventArgs e)
        {

        }

        private void button12_Click(object sender, EventArgs e)
        {

        }

        private void button33_Click(object sender, EventArgs e)
        {
            OpenFileDialog file = new OpenFileDialog();
            file.Filter = "Resim Dosyaları| *.jpg;*.jpeg;*.png"; //Jpg, jpeg, png uzantılarına izin verir
            file.InitialDirectory = ".";
            if (file.ShowDialog() == DialogResult.OK)
            {
                imageUrl = file.FileName;
            }
     
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
        }

        private void button34_Click(object sender, EventArgs e)
        {
            PuzzleImage puzzleImage = new PuzzleImage();

            puzzleImage.Show();
            //pictureBox3.Image = Image.FromFile(this.imageUrl);

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {
        }

        private void button36_Click(object sender, EventArgs e)
        {
            ScoreTable scoreTable = new ScoreTable();   
            scoreTable.Show();
        }

        private void button35_Click(object sender, EventArgs e)
        {
            this.Close();
            Login login= new Login();
            this.Update();
            this.Refresh();
            login.Show();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}

