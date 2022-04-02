/*--------------------------------------------------------------------------------------------------------------------
 Copyright (C) 2022 Himber Sacha

 This program is free software: you can redistribute it and/or modify
 it under the +terms of the GNU General Public License as published by
 the Free Software Foundation, either version 3 of the License, or
 any later version.

 This program is distributed in the hope that it will be useful,
 but WITHOUT ANY WARRANTY; without even the implied warranty of
 MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 GNU General Public License for more details.

 You should have received a copy of the GNU General Public License
 along with this program.  If not, see https://www.gnu.org/licenses/gpl-3.0.html. 

--------------------------------------------------------------------------------------------------------------------*/

using System;
using System.Windows.Forms;
using System.Drawing;

namespace Sky_UI
{
    public sealed class ProgressBar : Control
    {
        private Rectangle Rectangle = new Rectangle();
        private Region ThisRegion;
        private Region RectangleRegion;
        private int Value = 0;
        private int ValuePourcentage = 0;
        private int BorderRadius_ = 0;
        private const int borderW = 2;
        private MouseEventHandler MouseClick_ = null;
        private MouseEventHandler MouseMove_ = null;
        private MouseEventHandler MouseUp_ = null;
        private MouseEventHandler MouseDown_ = null;

        public ProgressBar()
        {
            Rectangle.BackColor = Color.Orange;
            Rectangle.Size = new Size(this.Width - borderW * 2, this.Height - borderW * 2);
            Rectangle.Location = new Point(borderW, borderW);
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
            BorderUpdate();
        }

        private void BorderUpdate()
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

                if (value - 1 >= 0)
                {
                    Rectangle.BorderRadius = value - 1;
                }
                else
                {
                    Rectangle.BorderRadius = 0;
                }

                BorderUpdate();
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
                Value = (int)((float)value / 100 * (this.Width - borderW * 2));
                Rectangle.Width = Value;
                ValuePourcentage = value;
                this.Resize += new EventHandler(this.ProgressBar_Resize);
            }
        }

        public void SetValuePixels(int value)
        {
            if (value <= this.Width - borderW * 2)
            {
                Value = value;
                Rectangle.Width = value;
                ValuePourcentage = (int)((float)value * 100 / this.Width - borderW * 2);
            }
            else
            {
                Value = this.Width + borderW;
                Rectangle.Width = this.Width - borderW * 2;
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
            Rectangle.Width = (int)((float)Value / 100 * this.Width - borderW * 2);
        }
    }
}
