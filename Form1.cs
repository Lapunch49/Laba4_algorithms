using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace Laba4_algorithms
{
    public partial class Form1 : Form
    {
        public static Bitmap bmp;
        public bool ctrlPress = false;
        public Form1()
        {
            InitializeComponent();
            bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
        }
        public static void draw_line( Pen pen, CCircle A, CCircle B)
        {
            Graphics g = Graphics.FromImage(bmp);
            g.DrawLine(pen, A.get_x(), A.get_y(), B.get_x(), B.get_y());
        }

        public void Wait(double seconds)
        {
            int ticks = System.Environment.TickCount + (int)Math.Round(seconds * 1000.0);
            while (System.Environment.TickCount < ticks)
            {
                Application.DoEvents();
            }
        }
        private void Worker(object ignored)
        {
            //Run first query
            Thread.Sleep(1000);
            //Run second query etc.
        }
        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            int k = -1;
            //проверяем попал ли курсор по какому-либо кругу
            for (int i = 0; i < Globals.arr_circles.get_count(); ++i)
                if (Globals.arr_circles.st[i].mouseClick_on_Object(e.X, e.Y))
                    {
                        k = i;  //курсор попал по кругу с индексом k
                    }
            if (e.Button == MouseButtons.Right)
            {
                if (k == -1)  //курсор не попал по кругу
                {
                    //создаем новый круг и рисуем его
                    Globals.arr_circles.add(new CCircle(e.X, e.Y));
                    Globals.arr_circles.st[Globals.arr_circles.get_count()-1].draw(Globals.arr_circles.get_count() - 1);
                    Globals.n++;
                    //добавляем столбцы и строки
                    if (Globals.n > 1)
                    {
                        Matrix.Columns.Add((Globals.n).ToString(), (Globals.n).ToString());
                        Matrix.Columns[Globals.n-1].Width = 70;  //задаем размер новому столбцу
                        Matrix.Rows.Add("", "");
                    }
                    //заполняем заголовки строк
                    for (int i = 0; (i <= (Matrix.Rows.Count - 1)); i++)
                    {
                        Matrix.Rows[i].HeaderCell.Value = string.Format((i + 1).ToString(), "0");
                    }
                }
            }
            else if (e.Button == MouseButtons.Left)//была нажата левая кнопка мыши
            {
                if (Globals.versh == -1)//пока не запомнили ни одну из вершин 
                {
                    if (k > -1)
                    {
                        Globals.versh = k;
                        Globals.arr_circles.st[k].highlight(k);
                    }
                }
                else
                {
                    if (k > -1)
                    {
                        draw_line(Globals.redline, Globals.arr_circles.st[Globals.versh], Globals.arr_circles.st[k]);
                        Globals.arr_circles.st[k].highlight(k);
                        //изменения в матрице смежности
                        Matrix.Rows[k].Cells[Globals.versh].Value = 1;
                        Matrix.Rows[Globals.versh].Cells[k].Value = 1;
                        //через небольшой промежуток времени
                        //Thread.Sleep(3000);
                        //ThreadPool.QueueUserWorkItem(Worker);
                        //Worker(pictureBox1);
                        //Wait(0.5);
                        draw_line(Globals.blueline, Globals.arr_circles.st[Globals.versh], Globals.arr_circles.st[k]);
                        Globals.arr_circles.st[Globals.versh].draw(Globals.versh);
                        Globals.arr_circles.st[Globals.versh].non_highlight(Globals.versh);
                        Globals.arr_circles.st[k].draw(k);
                        Globals.arr_circles.st[k].non_highlight(k);
                        Globals.versh = -1;
                    }
                }
            }
            pictureBox1.Image = bmp;
        }
        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            //for (int i=0; i < Globals.n; ++i)
            //{

            //}
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Globals.arr_circles.set_count_to_zero();
            pictureBox1.Image = null;
            bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            //for (int i = 0; i < Globals.n; ++i)
            //    for (int j = 0; j < Globals.n; ++j)
            //        Matrix.Rows[i].Cells[j].Value = "";
            Matrix.Rows.Clear();
            Matrix.Columns.Clear();
            Globals.n = 0;
            Matrix.Columns.Add(0.ToString(), 1.ToString());
            Matrix.Columns[0].Width = 70;  //задаем размер новому столбцу
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            //pictureBox1.Image = bmp;
        }
    }
    //public static class Globals
    //{
    //    public static Pen circle = new Pen(Color.Blue, 20);
    //    public static Pen line = new Pen(Color.Blue);
    //    public static Point[] arr_points = new Point[50];
    //    public static int n = 0;
    //    public static int versh = -1;
    //}
    public static class Globals
    {
        public static Pen redcircleline = new Pen(Color.Red, 2);
        public static Pen bluecircleline = new Pen(Color.Blue, 2);
        public static Pen redline = new Pen(Color.Red,2);
        public static Pen blueline = new Pen(Color.Blue, 2);
        public static int versh = -1;//для отметки выделенности одной вершины
        public static int n = 0;//количество элементов в хранилище arr_circles.st
        public static SolidBrush blueBrush = new SolidBrush(Color.Blue);
        public static SolidBrush redBrush = new SolidBrush(Color.Red);
        public static Storage arr_circles = new Storage();
    }


}
