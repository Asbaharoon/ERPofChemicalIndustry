using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace purchase
{
    public class OurT : TextBox
    {
        Size s = new Size();

        public OurT(int i)
        {
            s.Width = 107;
            Font f = new Font("Segoe ui Symbol", 12, FontStyle.Regular);
            this.Font = f;
            this.ReadOnly = true;
            this.TextAlign = HorizontalAlignment.Right;
            this.Name = "" + i;
            this.Size = s;
            //this.Text = this.Name;
        }

    }
}
