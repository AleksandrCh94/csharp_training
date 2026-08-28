using System;
using System.Collections.Generic;
using System.Text;

namespace addressbook_web_tests
{
    class Square : Figure
    {
        private int size; // инфа о стороне

        public Square (int size) // конструктор
        {
            this.size = size;
        }

        public int Size // свойство
        {
            get
            {
                return size;
            }
            set
            {
                size = value;
            }
        }
    }
}
