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

namespace Sky_framework
{
    public sealed class ProgressBar : Control
    {
        private Rectangle Rectangle = new Rectangle();
        private Region ThisRegion;
        private Region RectangleRegion;
        private int Value = 0;
        private int ValuePourcentage = 0;
        private int BorderRadius_ = 0;
        private const int bordure = -2;
        private MouseEventHandler MouseClick_ = null;
        private MouseEventHandler MouseMove_ = null;
        private MouseEventHandler MouseUp_ = null;
        private MouseEventHandler MouseDown_ = null;

        public ProgressBar()
        {
            Rectangle.BackColor = Color.Orange;
            Rectangle.Size = new Size(this.Width + bordure, this.Height + bordure);
            Rectangle.Location = new Point(bordure / -2, bordure / -2);
            Rectangle.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;
            Rectangle.Resize += new EventHandler(This_Resize);
            this.Controls.Add(Rectangle);

            this.BackColor = Color.FromArgb(64, 64, 64);
            this.Resize += new EventHandler(This_Resize);
            ThisRegion = this.Region;
            RectangleRegion = Rectangle.Region;
        }

        private void This_Resize(object sender, EventArgs e)
        {
            BorderUpddate();
        }

        private void BorderUpddate()
        {
            if (BorderRadius == 0)
            {
                this.Region = ThisRegion;
                Rectangle.Region = RectangleRegion;
            }
            else
            {
                IntPtr handle = Win32.CreateRoundRectRgn(0, 0, Width, Height, BorderRadius, BorderRadius);

                if (handle != IntPtr.Zero)
                {
                    Region = Region.FromHrgn(handle);
                    Win32.DeleteObject(handle);
                }

                IntPtr handle2 = Win32.CreateRoundRectRgn(0, 0, Rectangle.Width, Rectangle.Height, BorderRadius, BorderRadius);

                if (handle2 != IntPtr.Zero)
                {
                    Rectangle.Region = Region.FromHrgn(handle2);
                    Win32.DeleteObject(handle2);
                }
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
                BorderUpddate();
            }
        }

        public Color Color
        {
            get
            {
                return Rectangle.BackColor;
            }
            set
            {
                Rectangle.BackColor = value;
            }
        }

        public int ValuePourcentages
        {
            get
            {
                return ValuePourcentage;
            }
            set
            {
                Value = (int)((double)value / 100 * (this.Width - bordure));
                Rectangle.Width = Value;
                ValuePourcentage = value;
                this.Resize += new EventHandler(this.ProgressBar_Resize);
            }
        }

        public void SetValuePixels(int value)
        {
            if (value <= this.Width - bordure)
            {
                Value = value;
                Rectangle.Width = value;
                ValuePourcentage = (int)((double)value * 100 / this.Width);
            }
            else
            {
                Value = this.Width - bordure;
                Rectangle.Width = this.Width - bordure;
            }

            this.Resize -= new EventHandler(this.ProgressBar_Resize);
        }

        public int GetValuePixels()
        {
            return Value;
        }

        new public MouseEventHandler MouseDown
        {
            get
            {
                return MouseDown_;
            }
            set
            {
                Rectangle.MouseDown += value;
                base.MouseDown += value;
                MouseDown_ += value;
            }
        }

        new public MouseEventHandler MouseUp
        {
            get
            {
                return MouseUp_;
            }
            set
            {
                Rectangle.MouseUp += value;
                base.MouseUp += value;
                MouseUp_ += value;
            }
        }

        new public MouseEventHandler MouseMove
        {
            get
            {
                return MouseMove_;
            }
            set
            {
                Rectangle.MouseMove += value;
                base.MouseMove += value;
                MouseMove_ += value;
            }
        }

        new public MouseEventHandler MouseClick
        {
            get
            {
                return MouseClick_;
            }
            set
            {
                Rectangle.MouseClick += value;
                base.MouseClick += value;
                MouseClick_ += value;
            }
        }

        private void ProgressBar_Resize(object sender, EventArgs e)
        {
            Rectangle.Width = Value / 100 * this.Width + bordure;
        }
    }
}
