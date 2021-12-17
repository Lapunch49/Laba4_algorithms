using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Laba4_algorithms
{
    public class CCircle
    {
        private int x, y, r;
        private bool highlighted = false;
        public CCircle()
        {
            x = 0;
            y = 0;
            r = 20;
        }
        public CCircle(int x, int y)
        {
            this.x = x;
            this.y = y;
            r = 20;
        }
        public int get_x()
        {
            return x;
        }

        public int get_y()
        {
            return y;
        }

        public void draw(int i)
        {
            Graphics g = Graphics.FromImage(Form1.bmp);
            g.FillEllipse(Globals.blueBrush, x - r, y - r, r * 2, r * 2);
            g.DrawString((i+1).ToString(), new Font(FontFamily.GenericSansSerif, 10,
            FontStyle.Regular), new SolidBrush(Color.White), x-r/2, y-r/2);
        }

        public void highlight(int i)
        {
            Graphics g = Graphics.FromImage(Form1.bmp);
            //g.FillEllipse(Globals.redBrush, x - r, y - r, r * 2, r * 2);
            g.DrawEllipse(Globals.redcircleline, x - r, y - r, r * 2, r * 2);
            g.DrawString((i+1).ToString(), new Font(FontFamily.GenericSansSerif, 10, 
            FontStyle.Regular), new SolidBrush(Color.White), x - r / 2, y - r / 2);
            highlighted = true;
        }

        public void non_highlight(int i)
        {
            Graphics g = Graphics.FromImage(Form1.bmp);
            g.DrawEllipse(Globals.bluecircleline, x - r, y - r, r * 2, r * 2);
            g.DrawString((i+1).ToString(), new Font(FontFamily.GenericSansSerif, 10,
            FontStyle.Regular), new SolidBrush(Color.White), x - r / 2, y - r / 2);
            highlighted = true;
        }

        public bool mouseClick_on_Object(int x_, int y_)
        {
            if ((x_ - x) * (x_ - x) + (y_ - y) * (y_ - y) <= r * r)
                return true;
            else return false;
        }

        public void change_highlight()
        {
            if (highlighted)
                highlighted = false;
            else highlighted = true;
        }
        public bool get_highlighted()
        {
            return highlighted;
        }

    };
}

