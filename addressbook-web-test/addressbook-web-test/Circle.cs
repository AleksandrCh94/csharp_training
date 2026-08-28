using System;
using System.Collections.Generic;
using System.Text;

namespace addressbook_web_tests
{
    class Circle : Figure
    {
        private int radius; // ифна о радиусе
        
        public Circle (int radius) // конструктор
        {
            this.radius = radius;
        }

        public int Radius // свойство
        {
            get
            {
                return radius;
            }
            set
            {
                radius = value;
            }
        }
    }
}
