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
        int n = 0;        
        bool[,] matr_adj_copy;
        List<int> del_list_copy;
        int sought = -1; //искомая вершина
        public Form1()
        {
            InitializeComponent();
            bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
        }
        public static void draw_line(Pen pen, CCircle A, CCircle B)
        {
            Graphics g = Graphics.FromImage(bmp);
            g.DrawLine(pen, A.get_x(), A.get_y(), B.get_x(), B.get_y());
        }

        private bool[,] copy (bool[,] obj){
            bool[,] res = new bool[n, n];
            for (int i = 0; i < n; ++i)
                for (int j = 0; j < n; ++j)
                    res[i, j] = obj[i, j];
            return res;
        }
        private List<int> copy(List<int> obj)
        {
            List<int> res = new List<int>();
            for (int i = 0; i < obj.Count(); ++i)
                res.Add(obj[i]);
            return res;
        }

        public static void draw_line1(Pen pen, CCircle A, CCircle B)
        {
            Graphics g = Graphics.FromImage(bmp);
            //Pen pen1 = new Pen(Color.FromArgb(255, 0, 0, 255), 6);
            Pen pen1 = new Pen(Color.Blue, 1);
            //pen1.StartCap = LineCap.ArrowAnchor;
            pen1.CustomEndCap = new AdjustableArrowCap(5,20);

            //pen1.EndCap = LineCap.RoundAnchor;
            int delta_y = A.get_y() - B.get_y();
            int delta_x = A.get_x() - B.get_x();
            double RR = (delta_x * delta_x + delta_y * delta_y) / 1.0;
            double R = (int)( Math.Sqrt(delta_x * delta_x + delta_y * delta_y) );
            double sin = delta_y/R;
            double cos = delta_x / R;
            //g.DrawLine(pen1, A.get_x(), A.get_y(), B.get_x()- (int)(r * cos), B.get_y()-(int)(r*sin));
            g.DrawLine(pen1, A.get_x(), A.get_y(), B.get_x(), B.get_y());

        }   
        private void form_matr_adj(bool[,] matr_adj)
        {
            for (int i = 0; i < n; ++i)
                for (int j = 0; j < n; ++j)
                    if (Matrix.Rows[i].Cells[j].Value != null && (Matrix.Rows[i].Cells[j].Value).ToString() == "1")
                        matr_adj[i, j] = true;
                    else matr_adj[i, j] = false;
        }
        private bool check_vert(int versh, bool[,] matr_adj, List<int> del_list)//false - если не надо удалять 
        {
            //проверка на нахождение в списке уже удаленных
            foreach (int j in del_list)
                if (versh == j)
                    return false;
            //иначе проверяем вершину на необходимость удаления по матрице смежности
            bool fl = false; //false - нет единиц в строке/столбце - нет исх/вход ребер у versh
            //проверка, есть ли у вершины исходящее ребро
            for (int i = 0; i < n; ++i)
                if (matr_adj[versh, i] != false) 
                    fl = true;

            if (fl == false) //если нет исх. ребер
                return true; //выходим из функ, с меткой удаления вершины
            fl = false; //"обнуляем" значение fl для проверки столбца
            //проверка, есть ли у вершины входящее ребро
            for (int i = 0; i < n; ++i)
                if (matr_adj[i, versh] != false)
                    fl = true;
            if (fl == false) //если нет исх. ребер
                return true;
            return false;
        }
        private void del_vert(int versh, bool[,] matr_adj, Queue<int>och, List<int> del_list) //псевдо-удаление вершины
        {
            //псевдоудаляем вершину из матрицы смежности
            for (int i = 0; i < n; ++i)
            {
                //если у вершины есть смежные - помещаем их в очередь и псевдоудаляем ребро м/у ними
                if (matr_adj[versh, i] != false) {
                    och.Enqueue(i);
                    matr_adj[versh, i] = false; 
                }
                if (matr_adj[i, versh] != false)
                {
                    och.Enqueue(i);
                    matr_adj[i, versh] = false;
                }
            }
            //помещаем вершину в список удаленных
            del_list.Add(versh);
        }
        private bool acyclicity(bool[,] matr_adj, Queue<int> och, List<int> del_list)
        {
            //функция проверки на ацикличность: false - циклов нет
            for (int i = 0; i < n; ++i)
            {
                if (check_vert(i, matr_adj, del_list))
                {
                    del_vert(i, matr_adj, och, del_list);
                }
                //проверяем элементы из очереди
                while (och.Count() != 0)
                {
                    int j = och.Dequeue();
                    if (check_vert(j, matr_adj, del_list))
                    {
                        del_vert(j, matr_adj, och, del_list);
                    }
                }
            }
            //если все вершины в списке удаленных - циклов нет
            if (del_list.Count() == n) return false;
            else return true;
        }
        private void btn_no_cycles_Click(object sender, EventArgs e)
        { 
            //список "псевдо-удаленных" вершин 
            List<int> del_list = new List<int>();
            //очередь, в которую попадают вершины, смежные с ново-удаленной
            Queue<int> och = new Queue<int>();
            //создание матрицы смежности
            bool[,] matr_adj = new bool[n, n];
            form_matr_adj(matr_adj);
            //проверка на ацикличность - работа со сформированной матрицей смежности
            bool cycle = acyclicity(matr_adj, och, del_list);
            if (!cycle)
            {
                label1.Text = "There is no cycles in your graph";
                btn_del_vert.Visible = false;
            }
            else
            {
                label1.Text = "Your graph is cyclical ";
                //запоминаем состояние до удаление вершины
                matr_adj_copy = copy(matr_adj);
                del_list_copy = copy(del_list);
                //искомая вершина
                bool fl = false; // если не в списке удаленных
                for (int i = 0; i < n; ++i)
                {
                    fl = false;
                    foreach (int j in del_list_copy)
                        if (i == j)  //правильно ли сработает
                            fl = true; //или return;
                    if (fl == false)
                    {
                        del_vert(i, matr_adj_copy, och, del_list_copy);
                        och.Clear();
                        cycle = acyclicity(matr_adj_copy, och, del_list_copy);
                        if (!cycle) { sought = i; break; }
                        else
                        {
                            matr_adj_copy = copy(matr_adj);
                            del_list_copy = copy(del_list);
                        }
                    }
                }
                if (sought == -1)
                {
                    label1.Text += "and there is no way to escape cyclicality with deleting one vertex";
                    btn_del_vert.Visible = false;
                }
                else
                {
                    label1.Text += ("and there is a way to escape it with deleting vertex №" + (sought + 1).ToString());
                    btn_del_vert.Visible = true;
                }
            }
        }
        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            label1.Text = "";
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
                    n++;
                    //добавляем столбцы и строки
                    if (n > 1)
                    {
                        Matrix.Columns.Add((n).ToString(), (n).ToString());
                        Matrix.Columns[n-1].Width = 70;  //задаем размер новому столбцу
                        Matrix.Rows.Add("","");
                        //меняем местами последние строки
                        for (int i=0; i<n; ++i)
                        {
                            Matrix.Rows[n - 2].Cells[i].Value = Matrix.Rows[n - 1].Cells[i].Value;
                            Matrix.Rows[n - 1].Cells[i].Value = null;
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
                        //изменения в матрице смежности
                        Matrix.Rows[Globals.versh].Cells[k].Value = 1;
                        draw_line1(Globals.blueline, Globals.arr_circles.st[Globals.versh], Globals.arr_circles.st[k]);
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

        private void btn_clear_Click(object sender, EventArgs e)
        {
            Globals.arr_circles.set_count_to_zero();
            pictureBox1.Image = null;
            bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            //for (int i = 0; i < Globals.n; ++i)
            //    for (int j = 0; j < Globals.n; ++j)
            //        Matrix.Rows[i].Cells[j].Value = "";
            Matrix.Rows.Clear();
            Matrix.Columns.Clear();
            n = 0;
            Matrix.Columns.Add(0.ToString(), 1.ToString());
            Matrix.Columns[0].Width = 70;  //задаем размер новому столбцу
            //очищаем место для вывода
            label1.Text = "";
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            //pictureBox1.Image = bmp;
        }

        private void btn_aboutPr_Click(object sender, EventArgs e)
        {
            About_program frm = new About_program();
            frm.Show();
        }

        private void btn_task_Click(object sender, EventArgs e)
        {
            Task frm = new Task();
            frm.Show();
        }

        private void btn_del_vert_Click(object sender, EventArgs e)
        {
            Pen pen = new Pen(Color.Blue, 1);
            pen.CustomEndCap = new AdjustableArrowCap(5, 20);

            pictureBox1.Image = null;
            bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);

            Globals.arr_circles.del(sought);
            for (int i=0; i<n; ++i)
            {
                Matrix.Rows[sought].Cells[i].Value = null;
                Matrix.Rows[i].Cells[sought].Value = null;

            }
            for (int i = 0; i < n; ++i)
            {
                if (Globals.arr_circles.get_el(i) != null)
                    Globals.arr_circles.get_el(i).draw(i);
            }
            for (int i = 0; i < n; ++i)
                for (int j = 0; j < n; ++j)
                    if (Matrix.Rows[i].Cells[j].Value != null && (Matrix.Rows[i].Cells[j].Value).ToString() == "1")
                        draw_line1(pen, Globals.arr_circles.get_el(i), Globals.arr_circles.get_el(j));
            pictureBox1.Image = bmp;
        }
    }
    public static class Globals
    {
        public static Pen redcircleline = new Pen(Color.Red, 2);
        public static Pen bluecircleline = new Pen(Color.Blue, 2);
        public static Pen redline = new Pen(Color.Red,2);
        public static Pen blueline = new Pen(Color.Blue, 2);
        public static int versh = -1;//для отметки выделенности одной вершины
        //public static int n = 0;//количество элементов в хранилище arr_circles.st
        public static SolidBrush blueBrush = new SolidBrush(Color.Blue);
        public static SolidBrush redBrush = new SolidBrush(Color.Red);
        public static Storage arr_circles = new Storage();
        //public static Pen edge_with_arrow = new Pen(Color.Blue, 2);
        //edge_with_arrow.StartCap = LineCap.ArrowAnchor;

    }


}
