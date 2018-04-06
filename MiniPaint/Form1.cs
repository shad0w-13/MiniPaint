using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MiniPaint
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            StartPosition = FormStartPosition.CenterScreen;
            InitializeComponent();
            brush = new Pen(Color.Black, 1);
            LoadColors();
            g = pictureBox1.CreateGraphics();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            isBrushActive = toolStripButton1.Checked ? true : false; 
        }

        private void LoadColors()
        {
            foreach (KnownColor color in Enum.GetValues(typeof(KnownColor)))
            {
                Panel colorPanel = new Panel()
                {
                    Size = new Size(25, 25),
                    BackColor = Color.FromKnownColor(color),
                    ForeColor = Color.FromKnownColor(color),
                };
                colorPanel.Click += new EventHandler(ChangeColor_Click);
                flowLayoutPanel1.Controls.Add(colorPanel);
            }
        }

        private void ChangeColor_Click(object sender, EventArgs e)
        {
            brush.Color = ((Panel)sender).BackColor;
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (isBrushActive)
            {
                isMousePressed = true;
                x = e.X;
                y = e.Y;
            }
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (isBrushActive && isMousePressed && x != -1 && y != -1)
            {
                g.DrawLine(brush, new Point(x, y), e.Location);
                x = e.X;
                y = e.Y;
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            isMousePressed = false;
            x = -1;
            y = -1;
        }

        private bool isBrushActive = false, isMousePressed = false;
        private Pen brush;
        private Graphics g;

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            g = pictureBox1.CreateGraphics();
            groupBox1.Refresh();
        }

        int x = -1, y = -1;
    }
}
