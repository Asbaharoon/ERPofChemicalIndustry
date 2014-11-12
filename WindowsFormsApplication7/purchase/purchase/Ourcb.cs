using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace purchase
{
    class Ourcb : CheckBox
    {
         public Ourcb(int i)
        {
            this.Name = "" + i;
            this.Text = "";
        }
    }
}
