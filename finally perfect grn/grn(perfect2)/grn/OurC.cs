using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace grn
{
    public class OurC : ComboBox
    {
        public OurC(int i)
        {
            Font f = new Font("Segoe ui Symbol", 12, FontStyle.Regular);
            this.Font = f;
            this.Name = "" + i;
        }

    }
}
