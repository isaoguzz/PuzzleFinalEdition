using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YazLabYabBoz
{
    public partial class PuzzleImage : Form
    {
        public PuzzleImage()
        {
            InitializeComponent();
        }

        private void PuzzleImage_Load(object sender, EventArgs e)
        {
            if (Form1.imageUrl !=null)
            {
                pictureBox1.Image = System.Drawing.Image.FromFile(Form1.imageUrl);
            }
        }
    }
}
