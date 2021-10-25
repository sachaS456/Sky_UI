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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Sky_framework
{
    public class SkyForms : Form
    {
        private ReSize resize = new ReSize();
        //private Rectangle ControlBar = new Rectangle();
        //private Label label = new Label();
        //private PictureBox PictureBox = new PictureBox();
        private bool FullSreen_ = false;
        private ButtonCircular ButtonClose = new ButtonCircular();
        private ButtonCircular ButtonMaximized = new ButtonCircular();
        private ButtonCircular ButtonMinimized = new ButtonCircular();
        private Color BorderColor_ = Color.FromArgb(64, 64, 64);
        private Color TextColor_ = Color.FromArgb(224, 224, 224);
        private bool ButtonMaximizedVisible_ = true;
        private bool Redimensionnable_ = true;
        private string TextCorrectSize = string.Empty;
        private FormClosingEventHandler closing = null;

        public sbyte Border { get; set; } = 3;
        new public FormClosingEventHandler FormClosing = null;

        public SkyForms() : base()
        {
            this.FormBorderStyle = FormBorderStyle.Sizable;
            this.MinimumSize = new Size(200, 100);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Sky Form";

            ButtonClose.Size = 20;
            ButtonClose.BackColor = Color.FromArgb(245, 90, 90);
            ButtonClose.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            ButtonClose.Location = new Point(this.Width - 36, 1);
            ButtonClose.Text = "x";
            ButtonClose.ForeColor = Color.FromArgb(224, 224, 224);
            ButtonClose.TextAlign = ContentAlignment.MiddleCenter;
            ButtonClose.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            ButtonClose.MouseClick += new MouseEventHandler(ButtonCloseClique);
            this.Controls.Add(ButtonClose);

            ButtonMaximized.Size = 20;
            ButtonMaximized.BackColor = Color.FromArgb(245, 90, 90);
            ButtonMaximized.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            ButtonMaximized.Location = new Point(this.Width - 62, 1);
            ButtonMaximized.Text = "o";
            ButtonMinimized.ForeColor = Color.FromArgb(224, 224, 224);
            ButtonMaximized.TextAlign = ContentAlignment.MiddleCenter;
            ButtonMaximized.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            ButtonMaximized.MouseClick += new MouseEventHandler(ButtonMaximizedClique);
            this.Controls.Add(ButtonMaximized);

            ButtonMinimized.Size = 20;
            ButtonMinimized.BackColor = Color.FromArgb(245, 90, 90);
            ButtonMinimized.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            ButtonMinimized.Location = new Point(this.Width - 88, 1);
            ButtonMinimized.Text = "-";
            ButtonMinimized.ForeColor = Color.FromArgb(224, 224, 224);
            ButtonMinimized.TextAlign = ContentAlignment.MiddleCenter;
            ButtonMinimized.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            ButtonMinimized.MouseClick += new MouseEventHandler(ButtonMinimizedClique);
            this.Controls.Add(ButtonMinimized);

            closing += new FormClosingEventHandler(animationCloseForm);
            base.FormClosing += closing;
            //this.MouseDown += new MouseEventHandler(This_MouseDown);
            //this.MaximumSize = Screen.FromHandle(Handle).Bounds.Size;
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
            this.UpdateStyles();
        }

        /*private void This_MouseDown(object sender, MouseEventArgs e)
        {

        }*/

        private void ButtonCloseClique(object sender, MouseEventArgs e)
        {
            this.Close();
        }

        private void ButtonMaximizedClique(object sender, MouseEventArgs e)
        {
            if (this.WindowState == FormWindowState.Maximized)
            {
                this.WindowState = FormWindowState.Normal;
            }
            else
            {
                this.WindowState = FormWindowState.Maximized;
            }
        }

        private void ButtonMinimizedClique(object sender, MouseEventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void animationCloseForm(object sender, FormClosingEventArgs e)
        {
            if (this.IsDisposed == false && this.Disposing == false && this.Opacity != 0)
            {
                this.Close(false, sender, e);
            }
        }

        new public DialogResult ShowDialog()
        {
            this.Opacity = 0;
            this.Visible = true;

            while (this.Opacity <= 0.99)
            {
                this.Opacity += 0.05;
                System.Threading.Thread.Sleep(1000 / 60);
            }

            this.Visible = false;
            return base.ShowDialog();
        }

        new public async void Show()
        {
            //base.Show();
            this.Opacity = 0;
            this.Visible = true;

            while (this.Opacity <= 0.99)
            {
                this.Opacity += 0.05;
                await Task.Delay(1000 / 60);
            }

            this.Opacity = 1;
        }

        public void Close(bool ClearMemory, object sender, FormClosingEventArgs e)
        {
            if (FormClosing != null)
            {
                FormClosing(sender, e);

                if (e.Cancel == true)
                {
                    return;
                }
            }

            while (this.Opacity >= 0.01)
            {
                this.Opacity -= 0.05;
                System.Threading.Thread.Sleep(1000/60);
            }

            if (ClearMemory == true)
            {
                base.FormClosing -= closing;
                base.Close();
            }
        }

        new public void Close()
        {           
            Close(true, new object(), new FormClosingEventArgs(CloseReason.UserClosing, false));
        }

        new public string Text
        {
            get
            {
                return base.Text;
            }
            set
            {
                base.Text = value;

                this.Refresh();
            }
        }

        public Color TextColor
        {
            get
            {
                return TextColor_;
            }
            set
            {
                TextColor_ = value;
                this.Refresh();
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

                if (this.BorderColor.R + 40 > 255)
                {
                    ButtonMaximized.BackColor = Color.FromArgb(this.BorderColor.R + (255 - this.BorderColor.R), this.BorderColor.G + 40, this.BorderColor.B + 40);
                    ButtonMinimized.BackColor = Color.FromArgb(this.BorderColor.R + (255 - this.BorderColor.R), this.BorderColor.G + 40, this.BorderColor.B + 40);
                }
                else if (this.BorderColor.G + 40 > 255)
                {
                    ButtonMaximized.BackColor = Color.FromArgb(this.BorderColor.R + 40, this.BorderColor.G + (255 - this.BorderColor.G), this.BorderColor.B + 40);
                    ButtonMinimized.BackColor = Color.FromArgb(this.BorderColor.R + 40, this.BorderColor.G + (255 - this.BorderColor.G), this.BorderColor.B + 40);
                }
                else if (this.BorderColor.B + 40 > 255)
                {
                    ButtonMaximized.BackColor = Color.FromArgb(this.BorderColor.R + 40, this.BorderColor.G + 40, this.BorderColor.B + (255 - this.BorderColor.B));
                    ButtonMinimized.BackColor = Color.FromArgb(this.BorderColor.R + 40, this.BorderColor.G + 40, this.BorderColor.B + (255 - this.BorderColor.B));
                }
                else
                {
                    ButtonMaximized.BackColor = Color.FromArgb(this.BorderColor.R + 40, this.BorderColor.G + 40, this.BorderColor.B + 40);
                    ButtonMinimized.BackColor = Color.FromArgb(this.BorderColor.R + 40, this.BorderColor.G + 40, this.BorderColor.B + 40);
                }

                this.Refresh();
            }
        }

        new public FormBorderStyle FormBorderStyle
        {
            get
            {
                return base.FormBorderStyle;
            }
            private set
            {
                base.FormBorderStyle = value;
            }
        }

        new public Icon Icon
        {
            get
            {
                return base.Icon;
            }
            set
            {
                base.Icon = value;
                this.Refresh();
            }
        }

        public bool FullScreen
        {
            get
            {
                return FullSreen_;
            }
            set
            {
                FullSreen_ = value;

                if (value == true)
                {
                    this.WindowState = FormWindowState.Normal;
                    this.FormBorderStyle = FormBorderStyle.None;
                    this.WindowState = FormWindowState.Maximized;
                    ButtonClose.Visible = false;
                    ButtonMaximized.Visible = false;
                    ButtonMinimized.Visible = false;
                }
                else
                {
                    if (Redimensionnable_)
                    {
                        this.FormBorderStyle = FormBorderStyle.Sizable;
                    }
                    else
                    {
                        this.FormBorderStyle = FormBorderStyle.FixedSingle;
                    }
                    this.WindowState = FormWindowState.Normal;
                    ButtonClose.Visible = true;
                    ButtonMaximized.Visible = ButtonMaximizedVisible_;
                    ButtonMinimized.Visible = true;
                }

                this.Refresh();
            }
        }

        new public Size Size
        {
            get
            {
                return base.Size;
            }
            set
            {
                base.Size = value;

                ButtonClose.Location = new Point(this.Width - 36, ButtonClose.Location.Y);
                ButtonMaximized.Location = new Point(this.Width - 62, ButtonMaximized.Location.Y);
                ButtonMinimized.Location = new Point(this.Width - 88, ButtonMinimized.Location.Y);
            }
        }

        new public Size ClientSize
        {
            get
            {
                return base.ClientSize;
            }
            set
            {
                base.ClientSize = value;

                ButtonClose.Location = new Point(this.Width - 36, ButtonClose.Location.Y);
                ButtonMaximized.Location = new Point(this.Width - 62, ButtonMaximized.Location.Y);
                ButtonMinimized.Location = new Point(this.Width - 88, ButtonMinimized.Location.Y);
            }
        }

        public bool ButtonMaximizedVisible
        {
            get
            {
                return ButtonMaximized.Visible;
            }
            set
            {
                ButtonMaximized.Visible = value;
                ButtonMaximizedVisible_ = value;
            }
        }

        public bool Redimensionnable 
        { 
            get
            {
                return Redimensionnable_;
            }
            set
            {
                Redimensionnable_ = value;

                if (Redimensionnable_ == true)
                {
                    this.FormBorderStyle = FormBorderStyle.Sizable;
                }
                else
                {
                    this.FormBorderStyle = FormBorderStyle.FixedSingle;
                }
            }
        }

        /*protected override void OnResizeBegin(EventArgs e)
        {
            SuspendLayout();
            base.OnResizeBegin(e);
        }
        protected override void OnResizeEnd(EventArgs e)
        {
            ResumeLayout();
            base.OnResizeEnd(e);
        }*/

        protected override void OnPaint(PaintEventArgs e)
        {
            if (FullSreen_ == false)
            {
                Sky_framework.Border.DrawRoundRectangle(new Pen(BorderColor_, Border), 0, 0, Width - 2, Height - 2, 2, e.Graphics);

                System.Drawing.Rectangle rc = new System.Drawing.Rectangle(0, 0, this.ClientSize.Width, 20);
                e.Graphics.FillRectangle(new SolidBrush(BorderColor_), rc);
                e.Graphics.DrawIcon(base.Icon, new System.Drawing.Rectangle(10, 2, 16, 16));

                if (e.Graphics.MeasureString(this.Text, new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point)).Width > this.Width - 136)
                {
                    TextCorrectSize = this.Text;
                    while (e.Graphics.MeasureString(TextCorrectSize, new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point)).Width >= this.Width - 136)
                    {
                        TextCorrectSize = TextCorrectSize.Remove(TextCorrectSize.Length - 1);
                    }

                    TextCorrectSize += "...";
                    e.Graphics.DrawString(this.TextCorrectSize, new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point), new SolidBrush(TextColor_), new Point(38, 0));
                }
                else
                {
                    e.Graphics.DrawString(this.Text, new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point), new SolidBrush(TextColor_), new Point(38, 0));
                }


                /*IntPtr handle = Win32.CreateRoundRectRgn(0, 0, Width, Height, 15, 15);

                if (handle != IntPtr.Zero)
                {
                    //Region = Region.FromHrgn(handle);
                    Win32.DeleteObject(handle);
                }*/
            }
            //else
            //{
                /*IntPtr handle = Win32.CreateRoundRectRgn(0, 0, Width + 1, Height + 1, 0, 0);

                if (handle != IntPtr.Zero)
                {
                    //Region = Region.FromHrgn(handle);
                    Win32.DeleteObject(handle);
                }*/

                //Sky_framework.Border.DrawRoundRectangle(new Pen(this.BackColor, Border), 0, 0, Width - 2, Height - 2, 5, e.Graphics);
            //}
        }

        //
        //override  WndProc  
        //
        protected override void WndProc(ref Message m)
        {
            //****************************************************************************

            if (m.Msg == 0x84 && FullSreen_ == false)
            {
                int x = (int)(m.LParam.ToInt64() & 0xFFFF);               //get x mouse position
                int y = (int)((m.LParam.ToInt64() & 0xFFFF0000) >> 16);   //get y mouse position  you can gave (x,y) it from "MouseEventArgs" too
                Point pt = PointToClient(new Point(x, y));

                if (Redimensionnable == true)
                {
                    switch (resize.getMosuePosition(pt, this))
                    {
                        case "l": m.Result = (IntPtr)10; return;  // the Mouse on Left Form
                        case "r": m.Result = (IntPtr)11; return;  // the Mouse on Right Form
                        case "a": m.Result = (IntPtr)12; return;
                        case "la": m.Result = (IntPtr)13; return;
                        case "ra": m.Result = (IntPtr)14; return;
                        case "u": m.Result = (IntPtr)15; return;
                        case "lu": m.Result = (IntPtr)16; return;
                        case "ru": m.Result = (IntPtr)17; return; // the Mouse on Right_Under Form
                        case "": m.Result = pt.Y < 0x20 /*mouse on title Bar*/ ? (IntPtr)0x2 : (IntPtr)0x1; return;
                    }
                }
                else
                {
                    switch (resize.getMosuePosition(pt, this))
                    {
                        case "": m.Result = pt.Y < 0x20 /*mouse on title Bar*/ ? (IntPtr)0x2 : (IntPtr)0x1; return;
                    }
                }
            }

            //Remove border and keep snap window
            if (m.Msg == 0x0083 && m.WParam.ToInt32() == 1 && FullSreen_ == false)
            {
                return;
            }

            /*const int WM_NCCALCSIZE = 0x83;
            if (m.Msg == WM_NCCALCSIZE && m.WParam.ToInt32() == 1)
            {
                m.Result = new IntPtr(0xF0);   // Align client area to all borders
                return;
            }*/

            base.WndProc(ref m);
        }

        /*private bool OnSizeChanged_ = true;

        protected async override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            switch (this.WindowState)
            {
                case FormWindowState.Normal:
                    if (OnSizeChanged_== false)
                    {
                        await Task.Delay(100);
                        this.Size = new Size(this.Size.Width - 6, this.Size.Height - 13);
                        OnSizeChanged_ = true;
                    }

                    break;

                case FormWindowState.Maximized:
                    OnSizeChanged_ = false;
                    break;

                case FormWindowState.Minimized:
                    OnSizeChanged_ = false;
                    break;
            }
        }*/

        /*protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.Style |= 0x20000 | 0x80000 | 0x40000; //WS_MINIMIZEBOX | WS_SYSMENU | WS_SIZEBOX;
                return cp;
            }
        }*/
    }
}
