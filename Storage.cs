using System;
using System.Collections.Generic;
using System.Text;

namespace Laba4_algorithms
{
    public class Storage
    {
        private int n, k;
        public CCircle[] st;
        public Storage()
        {
            st = new CCircle[2];
            n = 2;
            k = 0;
        }
        public Storage(int size)
        {
            st = new CCircle[size];
            n = size;
            k = 0;
        }

        public void add(CCircle new_el)
        {
            if (k < n)
            {
                st[k] = new_el;
                k = k + 1;
            }
            else
            {
                n = n * 2;
                CCircle[] st_ = new CCircle[n];
                for (int i = 0; i < k; ++i)
                    st_[i] = st[i];
                st_[k] = new_el;
                k = k + 1;
                st = st_;
            }
        }

        public void set_count_to_zero()
        {
            k = 0;
        }
        public CCircle get_el(int ind)
        {
            return st[ind];
        } 
        public void del(int ind)
        {
            st[ind] = new CCircle();
            //st[ind] = null;
            //for (int i = ind; i < k - 1; ++i)
            //    st[i] = st[i + 1];
            //k = k - 1;
        }
        public int get_count()
        {
            return k;
        }
    };
}
