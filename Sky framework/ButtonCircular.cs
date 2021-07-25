using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace Sky_framework
{
    public sealed class ButtonCircular : Button
    {
        public ButtonCircular() : base()
        {
            Size = 50;

            this.Resize += new EventHandler(This_Resize);
        }

        new public int Size
        {
            get
            {
                return base.Size.Width;
            }
            set
            {
                base.Size = new Size(value, value);
            }
        }

        private void This_Resize(object sender, EventArgs e)
        {
            IntPtr handle = Win32.CreateRoundRectRgn(0, 0, Width, Height, this.Size, this.Size);

            if (handle != IntPtr.Zero)
            {
                Region = Region.FromHrgn(handle);
                Win32.DeleteObject(handle);
            }
        }
    }
}
