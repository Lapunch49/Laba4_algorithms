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
using System.Drawing.Drawing2D;

namespace Laba4_algorithms
{
    public partial class Form1 : Form
    {
        public static Bitmap bmp;
        public bool ctrlPress = false;
        const int r = 20;
        private static Pen redcircleline = new Pen(Color.Red, 2);
        private static Pen bluecircleline = new Pen(Color.Blue, 2);
        private static Pen redline = new Pen(Color.Red, 2);
        private static Pen blueline = new Pen(Color.Blue, 2);
        private static int versh = -1;//для отметки выделенности одной вершины
        private static int n = 0;//количество элементов в хранилище arr_circles.st
        private static SolidBrush blueBrush = new SolidBrush(Color.Blue);
        private static SolidBrush redBrush = new SolidBrush(Color.Red);
        private static Storage arr_circles = new Storage();
        //public static Pen edge_with_arrow = new Pen(Color.Blue, 2);
        //edge_with_arrow.StartCap = LineCap.ArrowAnchor;

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

        private void button2_Click(object sender, EventArgs e)
        {
            int num_of_edg = 0;
            int num_of_vert = n;
            for (int i = 0; i < n; ++i)
                for (int j = 0; j < n; ++j)
                    if (Matrix.Rows[i].Cells[j].Value != null) num_of_edg++;

            int[,] g = new int[num_of_edg, 2];
            int ii = 0;
            for (int i = 0; i < n; ++i)
                for (int j = 0; j < n; ++j)
                    if (Matrix.Rows[i].Cells[j].Value != null) {
                        g[ii, 0] = Convert.ToInt32(Matrix.Rows[i].HeaderCell.Value);
                        g[ii, 1] = Convert.ToInt32(Matrix.Columns[j].HeaderText); 
                    }


            int[,] p = new int[num_of_edg, 2];


            int[] color = {0, 0, 0, 0, 0}; // цвета вершин

            for (int i = 0; i < num_of_vert; i++)
                if (color[i] == 0) ;
                    //dfs(i,color, num_of_edg,g,p);

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
            for (int i = 0; i < arr_circles.get_count(); ++i)
                if (arr_circles.st[i].mouseClick_on_Object(e.X, e.Y))
                    {
                        k = i;  //курсор попал по кругу с индексом k
                    }
            if (e.Button == MouseButtons.Right)
            {
                if (k == -1)  //курсор не попал по кругу
                {
                    //создаем новый круг и рисуем его
                    arr_circles.add(new CCircle(e.X, e.Y));
                    arr_circles.st[arr_circles.get_count()-1].draw(arr_circles.get_count() - 1);
                    n++;
                    //добавляем столбцы и строки
                    if (n > 1)
                    {
                        Matrix.Columns.Add((n).ToString(), (n).ToString());
                        Matrix.Columns[n-1].Width = 70;  //задаем размер новому столбцу
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
                if (versh == -1)//пока не запомнили ни одну из вершин 
                {
                    if (k > -1)
                    {
                        versh = k;
                        arr_circles.st[k].highlight(k);
                    }
                }
                else
                {
                    if (k > -1)
                    {
                        //draw_line(redline, arr_circles.st[versh], arr_circles.st[k]);
                        //arr_circles.st[k].highlight(k);
                        //изменения в матрице смежности
                        Matrix.Rows[k].Cells[versh].Value = 1;
                        Matrix.Rows[versh].Cells[k].Value = 1;
                        //через небольшой промежуток времени
                        //Thread.Sleep(3000);
                        //ThreadPool.QueueUserWorkItem(Worker);
                        //Worker(pictureBox1);
                        //Wait(0.5);
                        draw_line(blueline, arr_circles.st[versh], arr_circles.st[k]);
                        arr_circles.st[versh].draw(versh);
                        arr_circles.st[versh].non_highlight(versh);
                        arr_circles.st[k].draw(k);
                        arr_circles.st[k].non_highlight(k);
                        versh = -1;
                    }
                }
            }
            pictureBox1.Image = bmp;
        }
        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            //for (int i=0; i < n; ++i)
            //{

            //}
        }

        private void button1_Click(object sender, EventArgs e)
        {
            arr_circles.set_count_to_zero();
            pictureBox1.Image = null;
            bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            //for (int i = 0; i < n; ++i)
            //    for (int j = 0; j < n; ++j)
            //        Matrix.Rows[i].Cells[j].Value = "";
            Matrix.Rows.Clear();
            Matrix.Columns.Clear();
            n = 0;
            Matrix.Columns.Add(0.ToString(), 1.ToString());
            Matrix.Columns[0].Width = 70;  //задаем размер новому столбцу
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            //pictureBox1.Image = bmp;
        }

   
    }
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
        //public static Pen edge_with_arrow = new Pen(Color.Blue, 2);
        //edge_with_arrow.StartCap = LineCap.ArrowAnchor;

    }


}
