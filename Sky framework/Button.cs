using System;
using System.Windows.Forms;
using System.Drawing;
using System.Threading.Tasks;

namespace Sky_framework
{
    public delegate void MouseEventNameHandler(string Text, int ID, MouseEventArgs e);

    public class Button : Control
    {
        private ContentAlignment TextAling_ = ContentAlignment.MiddleCenter;
        private Color Color;
        private bool Border_ = false;
        private bool MouseTouchButton = false;
        private int borderRadius_ = 0;
        private Image Image_ = null;
        private ContentAlignment ImageAlign_ = ContentAlignment.MiddleCenter;

        public Color BorderColor { get; set; } = Color.FromArgb(224, 224, 224);
        public MouseEventNameHandler MouseClickNameString = null;
        public int ID { get; set; } = 0;

        public Button()
        {
            this.BackColor = Color.FromArgb(64, 64, 64);
            WriteText();

            this.Resize += new EventHandler(Button_Resize);
            this.MouseEnter += new EventHandler(Button_MouseEnter);
            this.MouseLeave += new EventHandler(Button_MouseLeave);
            this.MouseDown += new MouseEventHandler(Button_MouseDown);
            this.MouseUp += new MouseEventHandler(Button_MouseUp);
            this.MouseClick += new MouseEventHandler(Button_MouseClick);

            Color = this.BackColor;
        }

        private void Button_MouseClick(object sender, MouseEventArgs e)
        {
            if (MouseClickNameString != null)
            {
                MouseClickNameString(this.Text, ID, e);
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            WriteText(); // Peut causer une haute utilisation de la mémoire et du processeur
        }

        private void Button_Resize(object sender, EventArgs e)
        {
            IntPtr handle = Win32.CreateRoundRectRgn(0, 0, Width, Height, borderRadius_, borderRadius_);

            if (handle != IntPtr.Zero)
            {
                Region = Region.FromHrgn(handle);
                Win32.DeleteObject(handle);
            }

            if (Border == true)
            {
                ControlPaint.DrawBorder(CreateGraphics(), new System.Drawing.Rectangle(0, 0, this.Width, this.Height), BorderColor, ButtonBorderStyle.Solid);
            }

            WriteText();
        }

        public int borderRadius
        {
            get
            {
                return borderRadius_;
            }
            set
            {
                borderRadius_ = value;

                IntPtr handle = Win32.CreateRoundRectRgn(0, 0, Width, Height, borderRadius_, borderRadius_);

                if (handle != IntPtr.Zero)
                {
                    Region = Region.FromHrgn(handle);
                    Win32.DeleteObject(handle);
                }
            }
        }

        private void WriteText()
        {
            if (this.Text == string.Empty || this.Text == null)
            {
                return;
            }

            SizeF sizeText = this.CreateGraphics().MeasureString(Text, new Font("Segoe UI", 9.75F, FontStyle.Bold));
            this.CreateGraphics().Clear(base.BackColor);

            if (Image_ != null)
            {
                DrawImage();
            }

            switch (TextAling_)
            {
                case ContentAlignment.MiddleLeft:
                    this.CreateGraphics().DrawString(Text, new Font("Segoe UI", 9.75F, FontStyle.Bold), new SolidBrush(Color.FromArgb(224, 224, 224)), new Point(5, this.Height / 2 - (int)sizeText.Height / 2));
                    break;

                case ContentAlignment.MiddleCenter:
                    this.CreateGraphics().DrawString(Text, new Font("Segoe UI", 9.75F, FontStyle.Bold), new SolidBrush(Color.FromArgb(224, 224, 224)), new Point(this.Width / 2 - (int)sizeText.Width / 2, this.Height / 2 - (int)sizeText.Height / 2));
                    break;

                case ContentAlignment.MiddleRight:
                    this.CreateGraphics().DrawString(Text, new Font("Segoe UI", 9.75F, FontStyle.Bold), new SolidBrush(Color.FromArgb(224, 224, 224)), new Point(this.Width - (int)sizeText.Width, this.Height / 2 - (int)sizeText.Height / 2));
                    break;
            }

            sizeText = SizeF.Empty;
        }

        private async void Button_MouseEnter(object sender, EventArgs e)
        {
            MouseTouchButton = true;
            if (this.BackColor.R + 40 > 255 && this.BackColor.G + 40 > 255 && this.BackColor.B + 40 > 255)
            {
                for (int index = 0; index <= 20; index += 2)
                {
                    if (Color.R + index >= 255 && Color.G + index >= 255 && Color.B + index >= 255)
                    {
                        base.BackColor = Color.FromArgb(255, 255, 255);
                    }
                    else if (Color.R + index >= 255)
                    {
                        if (Color.G + index >= 255)
                        {
                            base.BackColor = Color.FromArgb(255, 255, Color.B + index);
                        }
                        else if (Color.B + index >= 255)
                        {
                            base.BackColor = Color.FromArgb(255, Color.G + index, 255);
                        }
                        else
                        {
                            base.BackColor = Color.FromArgb(255, Color.G + index, Color.B + index);
                        }
                    }
                    else if (Color.G + index >= 255)
                    {
                        if (Color.B + index >= 255)
                        {
                            base.BackColor = Color.FromArgb(Color.R + index, 255, 255);
                        }
                        else
                        {
                            base.BackColor = Color.FromArgb(Color.R + index, 255, Color.B + index);
                        }
                    }
                    else if (Color.B + index >= 255)
                    {
                        base.BackColor = Color.FromArgb(Color.R + index, Color.G + index, 255);
                    }
                    else
                    {
                        base.BackColor = Color.FromArgb(Color.R + index, Color.G + index, Color.B + index);
                    }

                    await Task.Delay(3);
                }

                base.BackColor = Color.FromArgb(255, 255, 255);
                return;
            }

            if (this.BackColor.R + 40 > 255) // R
            {
                if (Color.G + 40 > 255)
                {
                    for (int index = 0; index <= 20; index += 2)
                    {
                        if (Color.R + index >= 255 && Color.G + index >= 255)
                        {
                            base.BackColor = Color.FromArgb(255, 255, Color.B + index);
                        }
                        else if (Color.R + index >= 255)
                        {
                            base.BackColor = Color.FromArgb(255, Color.G + index, Color.B + index);
                        }
                        else if (Color.G + index >= 255)
                        {
                            base.BackColor = Color.FromArgb(Color.R + index, 255, Color.B + index);
                        }
                        else
                        {
                            base.BackColor = Color.FromArgb(Color.R + index, Color.G + index, Color.B + index);
                        }

                        await Task.Delay(3);
                    }

                    base.BackColor = Color.FromArgb(255, 255, Color.B + 40);
                }
                else if (Color.B + 40 > 255)
                {
                    for (int index = 0; index <= 20; index += 2)
                    {
                        if (Color.R + index >= 255 && Color.B + index >= 255)
                        {
                            base.BackColor = Color.FromArgb(255, Color.G + index, 255);
                        }
                        else if (Color.R + index >= 255)
                        {
                            base.BackColor = Color.FromArgb(255, Color.G + index, Color.B + index);
                        }
                        else if (Color.B + index >= 255)
                        {
                            base.BackColor = Color.FromArgb(Color.R + index, Color.G + index, 255);
                        }
                        else
                        {
                            base.BackColor = Color.FromArgb(Color.R + index, Color.G + index, Color.B + index);
                        }

                        await Task.Delay(3);
                    }

                    base.BackColor = Color.FromArgb(255, Color.G + 40, 255);
                }
                else
                {
                    for (int index = 0; index <= 20; index += 2)
                    {
                        if (Color.R + index >= 255)
                        {
                            base.BackColor = Color.FromArgb(255, Color.G + index, Color.B + index);
                        }
                        else
                        {
                            base.BackColor = Color.FromArgb(Color.R + index, Color.G + index, Color.B + index);
                        }

                        await Task.Delay(3);
                    }

                    base.BackColor = Color.FromArgb(255, Color.G + 40, Color.B + 40);
                }
            }
            else if (this.BackColor.G + 40 > 255) // G
            {
                if (Color.B + 40 > 255)
                {
                    for (int index = 0; index <= 20; index += 2)
                    {
                        if (Color.G + index >= 255 && Color.B + index >= 255)
                        {
                            base.BackColor = Color.FromArgb(Color.R + index, 255, 255);
                        }
                        else if (Color.G + index >= 255)
                        {
                            base.BackColor = Color.FromArgb(Color.R + index, 255, Color.B + index);
                        }
                        else if (Color.B + index >= 255)
                        {
                            base.BackColor = Color.FromArgb(Color.R + index, Color.G + index, 255);
                        }
                        else
                        {
                            base.BackColor = Color.FromArgb(Color.R + index, Color.G + index, Color.B + index);
                        }

                        await Task.Delay(3);
                    }

                    base.BackColor = Color.FromArgb(Color.R + 40, 255, 255);
                }
                else
                {
                    for (int index = 0; index <= 20; index += 2)
                    {
                        if (Color.G + index >= 255)
                        {
                            base.BackColor = Color.FromArgb(Color.R + index, 255, Color.B + index);
                        }
                        else
                        {
                            base.BackColor = Color.FromArgb(Color.R + index, Color.G + index, Color.B + index);
                        }

                        await Task.Delay(3);
                    }

                    base.BackColor = Color.FromArgb(Color.R + 40, 255, Color.B + 40);
                }
            }
            else if (this.BackColor.B + 40 > 255) // B
            {
                for (int index = 0; index <= 20; index += 2)
                {
                    if (Color.B + index >= 255)
                    {
                        base.BackColor = Color.FromArgb(Color.R + index, Color.G + index, 255);
                    }
                    else
                    {
                        base.BackColor = Color.FromArgb(Color.R + index, Color.G + index, Color.B + index);
                    }

                    await Task.Delay(3);
                }

                base.BackColor = Color.FromArgb(Color.R + 40, Color.G + 40, 255);
            }
            else
            {
                for (int index = 0; index <= 20; index += 2)
                {
                    base.BackColor = Color.FromArgb(Color.R + index, Color.G + index, Color.B + index);

                    await Task.Delay(3);
                }

                base.BackColor = Color.FromArgb(Color.R + 40, Color.G + 40, Color.B + 40);
            }
        }

        private async void Button_MouseLeave(object sender, EventArgs e)
        {
            MouseTouchButton = false;

            while (base.BackColor.R < Color.R + 40 && base.BackColor.G < Color.G + 40 && base.BackColor.B < Color.B + 40)
            {
                await Task.Delay(100);
            }

            if (base.BackColor.R - 40 != Color.R && base.BackColor.G - 40 != Color.G && base.BackColor.B - 40 != Color.B)
            {
                for (int index = 20; index >= 0; index -= 2)
                {
                    if (255 - Color.R <= index && 255 - Color.G <= index && 255 - Color.B <= index)
                    {
                        base.BackColor = Color.FromArgb(base.BackColor.R, base.BackColor.G, base.BackColor.B);
                    }
                    else if (255 - Color.R <= index)
                    {
                        if (255 - Color.G <= index)
                        {
                            base.BackColor = Color.FromArgb(base.BackColor.R, base.BackColor.G, Color.B + index);
                        }
                        else if (255 - Color.B <= index)
                        {
                            base.BackColor = Color.FromArgb(base.BackColor.R, Color.G + index, base.BackColor.B);
                        }
                        else
                        {
                            base.BackColor = Color.FromArgb(base.BackColor.R, Color.G + index, Color.B + index);
                        }
                    }
                    else if (255 - Color.G <= index)
                    {
                        if (255 - Color.B <= index)
                        {
                            base.BackColor = Color.FromArgb(Color.R + index, base.BackColor.G, base.BackColor.B);
                        }
                        else
                        {
                            base.BackColor = Color.FromArgb(Color.R + index, base.BackColor.G, Color.B + index);
                        }
                    }
                    else if (255 - Color.B <= index)
                    {
                        base.BackColor = Color.FromArgb(Color.R + index, Color.G + index, base.BackColor.B);
                    }
                    else
                    {
                        base.BackColor = Color.FromArgb(Color.R + index, Color.G + index, Color.B + index);
                    }

                    await Task.Delay(10);

                    if (MouseTouchButton == true)
                    {
                        return;
                    }
                }

                base.BackColor = Color;
                return;
            }

            if (base.BackColor.R - 40 != Color.R) // R
            {
                if (base.BackColor.G - 40 != Color.G)
                {
                    for (int index = 20; index >= 0; index -= 2)
                    {
                        if (255 - Color.R <= index && 255 - Color.G <= index)
                        {
                            base.BackColor = Color.FromArgb(base.BackColor.R, base.BackColor.G, Color.B + index);
                        }
                        else if (255 - Color.R <= index)
                        {
                            base.BackColor = Color.FromArgb(base.BackColor.R, Color.G + index, Color.B + index);
                        }
                        else if (255 - Color.G <= index)
                        {
                            base.BackColor = Color.FromArgb(Color.R + index, base.BackColor.G, Color.B + index);
                        }
                        else
                        {
                            base.BackColor = Color.FromArgb(Color.R + index, Color.G + index, Color.B + index);
                        }

                        await Task.Delay(10);

                        if (MouseTouchButton == true)
                        {
                            return;
                        }
                    }
                }
                else if (base.BackColor.B - 40 != Color.B)
                {
                    for (int index = 20; index >= 0; index -= 2)
                    {
                        if (255 - Color.R <= index && 255 - Color.B <= index)
                        {
                            base.BackColor = Color.FromArgb(base.BackColor.R, Color.G + index, base.BackColor.B);
                        }
                        else if (255 - Color.R <= index)
                        {
                            base.BackColor = Color.FromArgb(base.BackColor.R, Color.G + index, Color.B + index);
                        }
                        else if (255 - Color.B <= index)
                        {
                            base.BackColor = Color.FromArgb(Color.R + index, Color.G + index, base.BackColor.B);
                        }
                        else
                        {
                            base.BackColor = Color.FromArgb(Color.R + index, Color.G + index, Color.B + index);
                        }

                        await Task.Delay(10);

                        if (MouseTouchButton == true)
                        {
                            return;
                        }
                    }
                }
                else
                {
                    for (int index = 20; index >= 0; index -= 2)
                    {
                        if (255 - Color.R <= index)
                        {
                            base.BackColor = Color.FromArgb(base.BackColor.R, Color.G + index, Color.B + index);
                        }
                        else
                        {
                            base.BackColor = Color.FromArgb(Color.R + index, Color.G + index, Color.B + index);
                        }

                        await Task.Delay(10);

                        if (MouseTouchButton == true)
                        {
                            return;
                        }
                    }
                }
            }
            else if (base.BackColor.G - 40 != Color.G) // G
            {
                if (base.BackColor.B - 40 != Color.B)
                {
                    for (int index = 20; index >= 0; index -= 2)
                    {
                        if (255 - Color.G <= index && 255 - Color.B <= index)
                        {
                            base.BackColor = Color.FromArgb(Color.R + index, base.BackColor.G, base.BackColor.B);
                        }
                        else if (255 - Color.G <= index)
                        {
                            base.BackColor = Color.FromArgb(Color.R + index, base.BackColor.G, Color.B + index);
                        }
                        else if (255 - Color.B <= index)
                        {
                            base.BackColor = Color.FromArgb(Color.R + index, Color.G + index, base.BackColor.B);
                        }
                        else
                        {
                            base.BackColor = Color.FromArgb(Color.R + index, Color.G + index, Color.B + index);
                        }

                        await Task.Delay(10);

                        if (MouseTouchButton == true)
                        {
                            return;
                        }
                    }
                }
                else
                {
                    for (int index = 20; index >= 0; index -= 2)
                    {
                        if (255 - Color.G <= index)
                        {
                            base.BackColor = Color.FromArgb(Color.G + index, base.BackColor.G, Color.B + index);
                        }
                        else
                        {
                            base.BackColor = Color.FromArgb(Color.R + index, Color.G + index, Color.B + index);
                        }

                        await Task.Delay(10);

                        if (MouseTouchButton == true)
                        {
                            return;
                        }
                    }
                }
            }
            else if (base.BackColor.B - 40 != Color.B) // B
            {
                for (int index = 20; index >= 0; index -= 2)
                {
                    if (255 - Color.R <= index)
                    {
                        base.BackColor = Color.FromArgb(base.BackColor.R, Color.G + index, Color.B + index);
                    }
                    else
                    {
                        base.BackColor = Color.FromArgb(Color.R + index, Color.G + index, Color.B + index);
                    }

                    await Task.Delay(10);

                    if (MouseTouchButton == true)
                    {
                        return;
                    }
                }
            }
            else
            {
                for (int index = 20; index >= 0; index -= 2)
                {
                    base.BackColor = Color.FromArgb(Color.R + index, Color.G + index, Color.B + index);

                    await Task.Delay(10);

                    if (MouseTouchButton == true)
                    {
                        return;
                    }
                }
            }

            base.BackColor = Color;
        }

        private void Button_MouseDown(object sender, EventArgs e)
        {
            if (Color.R + 100 > 255)
            {
                if (Color.G + 100 > 255 && Color.B + 100 > 255)
                {
                    base.BackColor = Color.FromArgb(255, 255, 255);
                }
                else if (Color.G + 100 > 255)
                {
                    base.BackColor = Color.FromArgb(255, 255, Color.B + 100);
                }
                else if (Color.B + 100 > 255)
                {
                    base.BackColor = Color.FromArgb(255, Color.G + 100, 255);
                }
                else
                {
                    base.BackColor = Color.FromArgb(255, Color.G + 100, Color.B + 100);
                }
            }
            else if (Color.G + 100 > 255)
            {
                if (Color.B + 100 > 255)
                {
                    base.BackColor = Color.FromArgb(Color.R + 100, 255, 255);
                }
                else
                {
                    base.BackColor = Color.FromArgb(Color.R + 100, 255, Color.B + 100);
                }
            }
            else if (Color.B + 100 > 255)
            {
                base.BackColor = Color.FromArgb(Color.R + 100, Color.G + 100, 255);
            }
            else
            {
                base.BackColor = Color.FromArgb(Color.R + 100, Color.G + 100, Color.B + 100);
            }
        }

        private void Button_MouseUp(object sender, MouseEventArgs e)
        {
            base.BackColor = Color;

            if (MouseTouchButton == true)
            {
                Button_MouseEnter(sender, e);
            }
            else
            {
                Button_MouseLeave(sender, e);
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

                if (value == true)
                {
                    ControlPaint.DrawBorder(CreateGraphics(), new System.Drawing.Rectangle(0, 0, this.Width, this.Height), BorderColor, ButtonBorderStyle.Solid);
                }
                else
                {
                    CreateGraphics().Clear(base.BackColor);
                    this.BackgroundImage = BackgroundImage;
                }
            }
        }

        new public Color BackColor
        {
            get
            {
                return Color;
            }
            set
            {
                base.BackColor = value;
                Color = value;

                if (Border == true)
                {
                    ControlPaint.DrawBorder(CreateGraphics(), new System.Drawing.Rectangle(0, 0, this.Width, this.Height), BorderColor, ButtonBorderStyle.Solid);
                }
            }
        }

        /*public void SetFont(Font font)
        {
            label.Font = font;
            TextAlignement();
        }

        public void SetFont(ref Font font)
        {
            label.Font = font;
            TextAlignement();
        }*/

        public ContentAlignment TextAlign
        {
            get
            {
                return TextAling_;
            }
            set
            {
                TextAling_ = value;
                WriteText();
            }
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
                WriteText();
            }
        }

        public ContentAlignment ImageAlign
        {
            get
            {
                return ImageAlign_;
            }
            set
            {
                ImageAlign_ = value;
                DrawImage();
            }
        }

        public Image Image
        {
            get
            {
                return Image_;
            }
            set
            {
                Image_ = value;
                DrawImage();
            }
        }

        private void DrawImage()
        {
            if (Image_ == null)
            {
                this.CreateGraphics().Clear(base.BackColor);
                WriteText();
                return;
            }

            switch (ImageAlign_)
            {
                case ContentAlignment.MiddleLeft:
                    this.CreateGraphics().DrawImage(Image_, new Point(5, this.Height / 2 - Image_.Height / 2));
                    break;

                case ContentAlignment.MiddleCenter:
                    this.CreateGraphics().DrawImage(Image_, new Point(this.Width / 2 - Image_.Width / 2, this.Height / 2 - Image_.Height / 2));
                    break;

                case ContentAlignment.MiddleRight:
                    this.CreateGraphics().DrawImage(Image_, new Point(this.Width - Image_.Width, this.Height / 2 - Image_.Height / 2));
                    break;
            }
        }

        /*public MouseEventHandler MouseDownControl
        {
            set
            {
                this.MouseDown += value;
            }
        }

        public MouseEventHandler MouseUpControl
        {
            set
            {
                this.MouseUp += value;
            }
        }

        public MouseEventHandler MouseMoveControl
        {
            set
            {
                this.MouseMove += value;
            }
        }

        public MouseEventHandler MouseClickControl
        {
            set
            {
                this.MouseClick += value;
            }
        }*/

        /*public bool BlurEffect
        {
            get
            {
                return BlurEffect_;
            }
            set
            {
                BlurEffect_ = value;

                this.BackgroundImage = Effect.GaussianBlurControl(this).Result;
            }
        }*/
    }
}
