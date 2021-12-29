/*--------------------------------------------------------------------------------------------------------------------
 Copyright (C) 2021 Himber Sacha

 This program is free software: you can redistribute it and/or modify
 it under the +terms of the GNU General Public License as published by
 the Free Software Foundation, either version 2 of the License, or
 any later version.

 This program is distributed in the hope that it will be useful,
 but WITHOUT ANY WARRANTY; without even the implied warranty of
 MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 GNU General Public License for more details.

 You should have received a copy of the GNU General Public License
 along with this program.  If not, see https://www.gnu.org/licenses/gpl-2.0.html. 

--------------------------------------------------------------------------------------------------------------------*/

using System;
using System.Windows.Forms;
using System.Drawing;

namespace Sky_UI
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
                Sky_UI.Border.DrawRoundRectangle(new Pen(BorderColor, BorderWidth), 0, 0, Width - 2, Height - 2, BorderRadius, this.CreateGraphics());
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
