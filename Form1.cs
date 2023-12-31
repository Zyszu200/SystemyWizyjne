﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Emgu.CV;
using Emgu.CV.Structure;

namespace StrukturaObrazuSr8
{
    public partial class Form1 : Form
    {
        VideoCapture cap = null;
        Image<Bgr, byte> image1;
        Image<Bgr, byte> image2;
        Image<Bgr, byte> image_line1;
        Image<Bgr, byte> image_line2;

        public Form1()
        {
            InitializeComponent();
            image1 = new Image<Bgr, byte>(pictureBox1.Size);
            image2 = new Image<Bgr, byte>(pictureBox2.Size);
            image_line1 = new Image<Bgr, byte>(pictureBox3.Size);
            image_line2 = new Image<Bgr, byte>(pictureBox4.Size);

            try { cap = new VideoCapture(0); } catch { throw; }

            cap.SetCaptureProperty(Emgu.CV.CvEnum.CapProp.FrameWidth, pictureBox1.Width);
            cap.SetCaptureProperty(Emgu.CV.CvEnum.CapProp.FrameHeight, pictureBox1.Height);

            textBox_G.Text = "0"; textBox2_G.Text = "0";
            textBox_B.Text = "0"; textBox2_B.Text = "0";
            textBox_R.Text = "0"; textBox2_R.Text = "0";

            textBox_x.Text = "0"; textBox2_x.Text = "0";
            textBox_y.Text = "0"; textBox2_y.Text = "0";
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            textBox_x.Text = e.X.ToString();
            textBox_y.Text = e.Y.ToString();

            if(!checkBox_Hex.Checked)
            {
                textBox_R.Text = image1.Data[e.Y, e.X, 2].ToString();
                textBox_G.Text = image1.Data[e.Y, e.X, 1].ToString();
                textBox_B.Text = image1.Data[e.Y, e.X, 0].ToString();
            }
            else
            {
                textBox_R.Text = "0x" + image1.Data[e.Y, e.X, 2].ToString("X");
                textBox_G.Text = "0x" + image1.Data[e.Y, e.X, 1].ToString("X");
                textBox_B.Text = "0x" + image1.Data[e.Y, e.X, 0].ToString("X");
            }
        }

        private void pictureBox2_MouseClick(object sender, MouseEventArgs e)
        {
            textBox2_x.Text = e.X.ToString();
            textBox2_y.Text = e.Y.ToString();

            if (!checkBox2_Hex.Checked)
            {
                textBox2_R.Text = image2.Data[e.Y, e.X, 2].ToString();
                textBox2_G.Text = image2.Data[e.Y, e.X, 1].ToString();
                textBox2_B.Text = image2.Data[e.Y, e.X, 0].ToString();
            }
            else
            {
                textBox2_R.Text = "0x" + image2.Data[e.Y, e.X, 2].ToString("X");
                textBox2_G.Text = "0x" + image2.Data[e.Y, e.X, 1].ToString("X");
                textBox2_B.Text = "0x" + image2.Data[e.Y, e.X, 0].ToString("X");
            }
        }

        private void button_graphic1_Click(object sender, EventArgs e)
        {
            //image1.SetValue(new MCvScalar(0, 120, 255));
            MCvScalar circleColor = new MCvScalar(0, 120, 255);
            CvInvoke.Circle(image1, new Point(160, 120), 18, circleColor, -1);

            Point L1 = new Point(0, 0);
            Point L2 = new Point(320, 240);
            CvInvoke.Line(image1, L1, L2, new MCvScalar(200, 0x88, 17), 9);

            Rectangle rect1 = new Rectangle(5, 5, 310, 230);
            CvInvoke.Rectangle(image1, rect1, new MCvScalar(45, 0xFF, 112), 6);

            pictureBox1.Image = image1.Bitmap;
        }

        private void button_clear1_Click(object sender, EventArgs e)
        {
            image1.SetZero();
            pictureBox1.Image = image1.Bitmap;
        }

        private void button_file1_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.DefaultExt = "png";
            dlg.ShowDialog();

            //Mat img = CvInvoke.Imread(@"C:\Users\mateu\Downloads\cat.png", Emgu.CV.CvEnum.ImreadModes.AnyColor);
            Mat img = CvInvoke.Imread(dlg.FileName, Emgu.CV.CvEnum.ImreadModes.AnyColor);
            CvInvoke.Resize(img, img, pictureBox1.Size);
            image1 = img.ToImage<Bgr, byte>();
            pictureBox1.Image = image1.Bitmap;
        }

        private void button_cam1_Click(object sender, EventArgs e)
        {
            Mat img = new Mat();
            cap.Read(img);
            CvInvoke.Resize(img, img, pictureBox1.Size);
            image1 = img.ToImage<Bgr, byte>();
            pictureBox1.Image = image1.Bitmap;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            image2.SetZero();
            pictureBox2.Image = image2.Bitmap;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Mat img = new Mat();
            cap.Read(img);
            CvInvoke.Resize(img, img, pictureBox2.Size);
            image2 = img.ToImage<Bgr, byte>();
            pictureBox2.Image = image2.Bitmap;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.DefaultExt = "png";
            dlg.ShowDialog();

            // img = CvInvoke.Imread(@"C:\Users\mateu\Downloads\cat.png", Emgu.CV.CvEnum.ImreadModes.AnyColor);
            Mat img = CvInvoke.Imread(dlg.FileName, Emgu.CV.CvEnum.ImreadModes.AnyColor);
            CvInvoke.Resize(img, img, pictureBox2.Size);
            image2 = img.ToImage<Bgr, byte>();
            pictureBox2.Image = image2.Bitmap;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //image1.SetValue(new MCvScalar(0, 120, 255));
            MCvScalar circleColor = new MCvScalar(255, 67, 38);
            CvInvoke.Circle(image2, new Point(160, 120), 18, circleColor, -1);

            Point L1 = new Point(0, 0);
            Point L2 = new Point(320, 240);
            CvInvoke.Line(image2, L1, L2, new MCvScalar(200, 0x88, 17), 9);

            Rectangle rect1 = new Rectangle(5, 5, 310, 230);
            CvInvoke.Rectangle(image2, rect1, new MCvScalar(45, 0xFF, 112), 6);

            pictureBox2.Image = image2.Bitmap;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            copySelective(image1, image2, checkBox_Color_B.Checked, checkBox_Color_G.Checked, checkBox_Color_R.Checked);
            pictureBox2.Image = image2.Bitmap;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            copyMono(image1, image2);
            pictureBox2.Image = image2.Bitmap;
        }

        private void button_copy2to1_Click(object sender, EventArgs e)
        {
            copySelective(image2, image1, checkBox_Color_B.Checked, checkBox_Color_G.Checked, checkBox_Color_R.Checked);
            pictureBox1.Image = image1.Bitmap;
        }

        private void button_graph1_Click(object sender, EventArgs e)
        {
            int lineY = Convert.ToInt32(textBox_y.Text);
            // pictureBox3 to okienko z linią pod PictureBox1
            // pictureBox1_line to u nas PicureBox3
            double scaleY = (pictureBox3.Height - 1) / 255.0;

            image_line1.SetZero();
            byte[,,] srcData = image1.Data;
            byte[,,] dstData = image_line1.Data;
            int lineHeight = pictureBox3.Height - 1;
            for (int x = 0; x < pictureBox3.Width; x++)
            {
                for(int ch = 0; ch < 3; ch++)
                {
                    int Yb = (int)(lineHeight - srcData[lineY, x, ch] * scaleY);
                    dstData[Yb, x, ch] = 255;
                }
            }
            pictureBox3.Image = image_line1.Bitmap;
        }

        private void button_graph2_Click(object sender, EventArgs e)
        {
            int lineY = Convert.ToInt32(textBox2_y.Text);
            // pictureBox3 to okienko z linią pod PictureBox1
            // pictureBox1_line to u nas PicureBox3
            double scaleY = (pictureBox4.Height - 1) / 255.0;

            image_line2.SetZero();
            byte[,,] srcData = image2.Data;
            byte[,,] dstData = image_line2.Data;
            int lineHeight = pictureBox3.Height - 1;
            for (int x = 0; x < pictureBox4.Width; x++)
            {
                for (int ch = 0; ch < 3; ch++)
                {
                    int Yb = (int)(lineHeight - srcData[lineY, x, ch] * scaleY);
                    dstData[Yb, x, ch] = 255;
                }
            }
            pictureBox4.Image = image_line2.Bitmap;
        }

        private void checkBox_Hex_CheckedChanged(object sender, EventArgs e)
        {
            if(!checkBox_Hex.Checked)
            {
                textBox_B.Text = Convert.ToInt32(textBox_B.Text, 16).ToString();
                textBox_G.Text = Convert.ToInt32(textBox_G.Text, 16).ToString();
                textBox_R.Text = Convert.ToInt32(textBox_R.Text, 16).ToString();

                return;
            }

            textBox_B.Text = "0x" + Convert.ToInt32(textBox_B.Text).ToString("X");
            textBox_G.Text = "0x" + Convert.ToInt32(textBox_G.Text).ToString("X");
            textBox_R.Text = "0x" + Convert.ToInt32(textBox_R.Text).ToString("X");

        }

        private void checkBox2_Hex_CheckedChanged(object sender, EventArgs e)
        {
            if (!checkBox2_Hex.Checked)
            {
                textBox2_B.Text = Convert.ToInt32(textBox2_B.Text, 16).ToString();
                textBox2_G.Text = Convert.ToInt32(textBox2_G.Text, 16).ToString();
                textBox2_R.Text = Convert.ToInt32(textBox2_R.Text, 16).ToString();

                return;
            }

            textBox2_B.Text = "0x" + Convert.ToInt32(textBox2_B.Text).ToString("X");
            textBox2_G.Text = "0x" + Convert.ToInt32(textBox2_G.Text).ToString("X");
            textBox2_R.Text = "0x" + Convert.ToInt32(textBox2_R.Text).ToString("X");
        }
    }
}
