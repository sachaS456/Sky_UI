using System;
using System.Windows.Forms;
using System.Drawing;

namespace Sky_framework
{
    public class Rectangle : ScrollableControl
    {
        private int BorderRadius_ = 0;
        private bool Border_ = false;
        private int BorderWidth_ = 3;
        private Color BorderColor_ = Color.FromArgb(224, 224, 224);
        private Region ThisRegion;
        
        public Rectangle()
        {
            ThisRegion = this.Region;
            this.Resize += new EventHandler(This_Resize);
            this.LocationChanged += new EventHandler(This_LocationChanged);
        }

        private void This_Resize(object sender, EventArgs e)
        {
            UpdateBorderAndRegion();
        }

        private void This_LocationChanged(object sender, EventArgs e)
        {
            UpdateBorderAndRegion();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            UpdateBorderAndRegion();
        }

        private void UpdateBorderAndRegion()
        {
            if (BorderRadius == 0)
            {
                this.Region = ThisRegion;
            }
            else
            {
                IntPtr handle = Win32.CreateRoundRectRgn(0, 0, Width, Height, BorderRadius, BorderRadius);

                if (handle != IntPtr.Zero)
                {
                    Region = Region.FromHrgn(handle);
                    Win32.DeleteObject(handle);
                }

            }

            //this.CreateGraphics().Clear(BackColor);
            
            if (Border == true)
            {
                Sky_framework.Border.DrawRoundRectangle(new Pen(BorderColor, BorderWidth), 0, 0, Width - 2, Height - 2, BorderRadius, this.CreateGraphics());
            }
        }

        public int BorderRadius
        {
            get
            {
                return BorderRadius_;
            }
            set
            {
                BorderRadius_ = value;
                UpdateBorderAndRegion();
            }
        }

        public bool Border
        {
            get
            {
                return Border_;
            }
            set
            {
                Border_ = value;
                UpdateBorderAndRegion();
            }
        }

        public int BorderWidth
        {
            get
            {
                return BorderWidth_;
            }
            set
            {
                BorderWidth_ = value;
                UpdateBorderAndRegion();
            }
        }

        public Color BorderColor
        {
            get
            {
                return BorderColor_;
            }
            set
            {
                BorderColor_ = value;
                UpdateBorderAndRegion();
            }
        }
    }
}
