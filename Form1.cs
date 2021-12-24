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
        private static int versh = -1;//для отметки выделенности одной вершины
        private static int n = 0;//количество элементов в хранилище arr_circles.st
        private static Storage arr_circles = new Storage();

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

        private void btn_EC_Click(object sender, EventArgs e) //BFS
        {
            //определяем количество вершин и дуг
            int num_of_edg = 0;
            int num_of_vert = n;
            for (int i = 0; i < n; ++i)
            {
                if (Matrix.Rows[i].Cells[i].Value != null)
                    Matrix.Rows[i].Cells[i].Value = null;
                for (int j = 0; j < n; ++j)
                {
                    if ((Matrix.Rows[i].Cells[j].Value) != null) num_of_edg++;
                }
            }
            //формирование списка смежности
            int[,] adj_list = new int[num_of_edg, 2];//список смежности
            int ii = 0;
            for (int i = 0; i < n; ++i)
                for (int j = 0; j < n; ++j)
                    if (Matrix.Rows[i].Cells[j].Value != null) {
                        adj_list[ii, 0] = Convert.ToInt32(Matrix.Rows[i].HeaderCell.Value);
                        adj_list[ii++, 1] = Convert.ToInt32(Matrix.Columns[j].HeaderText);
                    }
            //массив
            //очередь и список
            Queue <int> Och = new Queue <int>();
            List<int> Lst = new List<int>();
            //определяем начальную вершину
            int st_vert = 1; // По умолчанию
            //1) Проверка - Поиск в ширину
            bool fl = false; //есть ли вершина в списке Lst
            int tmp;
            Och.Enqueue(st_vert); //заносим первый элемент в очередь
            while (Och.Count!=0)
            {
                tmp = Och.Dequeue();
                fl = false;
                foreach (int j in Lst)
                {
                    if (j == tmp)
                        fl = true;
                }
                if (!fl)
                { 
                    Lst.Add(tmp);//добавили в лист
                    //в списке смежности ищем ребра, начинающиеся с tmp
                    for (int i = 0; i < num_of_edg; ++i)
                    {
                        if (adj_list[i, 0] == tmp)
                        {
                            fl = false;
                            foreach (int j in Lst)
                            {
                                if (j == adj_list[i, 1])
                                    fl = true;
                            }
                            if (!fl)
                                Och.Enqueue(adj_list[i, 1]);
                        }
                    }
                }   
            }
            if (Lst.Count() != num_of_vert)
            {
                label.Text += "The graph does not contain an Eulerian cycle, since the graph is disconnected";
            }
            else //2) проверка четности степеней всех вершин
            {
                //label.Text = "";
                int cnt = 0;
                fl = false;
                for (int i = 0; i < num_of_vert; ++i)
                {
                    cnt = 0; //обнуляем счетчик
                    for (int j = 0; j < num_of_edg; ++j)
                    {
                        if (adj_list[j, 0] == i)
                            cnt++; //считаем все ребра вершины
                    }
                    if (cnt % 2 == 1) fl = true;
                }
                if (fl)
                {
                    label.Text = "The graph does not contain an Eulerian cycle, since not all vertices have an even degree";
                }
                else
                { //3) Строим Эйлеров цикл
                    Lst.Clear();
                    Stack<int> stec = new Stack<int>();
                    Stack<int> res = new Stack<int>();
                    int vert_st = 1;
                    int vert_end = -1;
                    stec.Push(st_vert);
                    while (stec.Count != 0)
                    {
                        vert_st = stec.Peek();
                        fl = false;
                        for (int i = num_of_edg - 1; i >= 0; --i)
                            if (adj_list[i, 0] == vert_st)
                            {
                                vert_end = adj_list[i, 1];
                                fl = true;
                            }
                        if (fl)
                        {
                            stec.Push(vert_end); //заносим в стек
                            //удаляем ребра
                            for (int i = 0; i < num_of_edg; ++i)
                            {
                                if (adj_list[i, 0] == vert_st && adj_list[i, 1] == vert_end)
                                {
                                    adj_list[i, 0] = 0;
                                    adj_list[i, 1] = 0;
                                }
                                if (adj_list[i, 0] == vert_end && adj_list[i, 1] == vert_st)
                                {
                                    adj_list[i, 0] = 0;
                                    adj_list[i, 1] = 0;
                                }
                            }
                            vert_st = vert_end;
                        }
                        else
                        {
                            vert_st = stec.Pop();
                            res.Push(vert_st);
                        }
                    }
                    label.Text = "";
                    int iii = 1;
                    while (res.Count != 0)
                    {
                        label.Text += (res.Pop()).ToString();
                        if (iii++ != (num_of_edg/2+1))
                            label.Text += "-";
                    }
                }
            }
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
                        Matrix.Rows.Add(null, null);
                        for(int i=0; i < n; ++i)
                            for (int j=0; j<n; ++j)
                            {
                                if (Matrix.Rows[i].Cells[j].Value != Matrix.Rows[j].Cells[i].Value)
                                {
                                    Matrix.Rows[j].Cells[i].Value = Matrix.Rows[i].Cells[j].Value;
                                }
                            }
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
                        //изменения в матрице смежности
                        Matrix.Rows[k].Cells[versh].Value = 1;
                        Matrix.Rows[versh].Cells[k].Value = 1;
                        //рисуем дугу и убираем выделение вершины
                        draw_line(Globals.blueline, arr_circles.st[versh], arr_circles.st[k]);
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

        private void btn_clear_Click(object sender, EventArgs e)
        {
            arr_circles.set_count_to_zero();
            pictureBox1.Image = null;
            bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            //удаляем все колонки и столбцы
            Matrix.Rows.Clear();
            Matrix.Columns.Clear();
            n = 0;
            Matrix.Columns.Add(0.ToString(), 1.ToString());
            Matrix.Columns[0].Width = 70;  //задаем размер новому столбцу
            //очищаем поле ввода и вывода
            label.Text = "";
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
        public static SolidBrush blueBrush = new SolidBrush(Color.Blue);
        public static SolidBrush redBrush = new SolidBrush(Color.Red);
    }


}
