using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Sky_framework
{
    public sealed class MenuDeroulant : Control
    {
        private const int HeightButton = 30;
        private List<Button> buttons = new List<Button>();
        private List<Button> ButtonsPage = new List<Button>();
        private Side ShowSide_ = Side.Top;
        private int SizeHeight_ = 20;
        private int SizeWidth_ = 150;
        private int BorderRadius_ = 0; // mettre à 20 pour une bordure arondi de manière résonable.
        private int nbBar = 0;
        private double TwoColumn = 1;
        private byte lang;
        private int SizeNBButton = 0;

        public Color BorderColor { get; set; } = Color.FromArgb(224, 224, 224);
        public int Border { get; set; } = 0;
        public bool View { get; private set; } = false;

        public MenuDeroulant(byte lang)
        {
            this.BackColor = Color.FromArgb(64, 64, 64);
            this.ForeColor = Color.FromArgb(224, 224, 224);
            this.Visible = false;
            this.Resize += new EventHandler(This_Resize);
            this.lang = lang;
        }

        public MenuDeroulant()
        {
            this.BackColor = Color.FromArgb(64, 64, 64);
            this.ForeColor = Color.FromArgb(224, 224, 224);
            this.Visible = false;
            this.Resize += new EventHandler(This_Resize);
            this.lang = 1;
        }

        private void This_Resize(object sender, EventArgs e)
        {
            if (this == null || this.IsDisposed == true || this.Disposing == true)
            {
                return;
            }

            IntPtr handle = Win32.CreateRoundRectRgn(0, 0, Width, Height, BorderRadius_, BorderRadius_);

            if (handle != IntPtr.Zero)
            {
                Region = Region.FromHrgn(handle);
                Win32.DeleteObject(handle);
            }

            if (Border != 0)
            {
                Sky_framework.Border.DrawRoundRectangle(new Pen(BorderColor, Border), 0, 0, Width - 1, Height - 1, BorderRadius_, this.CreateGraphics());
            }
            else
            {
                Sky_framework.Border.DrawRoundRectangle(new Pen(this.BackColor, 1), 0, 0, Width - 1, Height - 1, BorderRadius_, this.CreateGraphics());
            }
            //ControlPaint.DrawBorder(CreateGraphics(), this.ClientRectangle, BorderColor, Border, ButtonBorderStyle.Solid, BorderColor, Border, ButtonBorderStyle.Solid,
            //BorderColor, Border, ButtonBorderStyle.Solid, BorderColor, Border, ButtonBorderStyle.Solid);
        }

        public int NbButton
        {
            get
            {
                return buttons.Count();
            }
        }

        public void AddButton(string[] buttonList)
        {
            AddButton(ref buttonList);
            SizeHeight += HeightButton;
        }

        public void SetButtonClique(int index, MouseEventHandler e)
        {
            buttons[index].MouseClick += e;
        }

        public void SetButtonClique(ref int index, MouseEventHandler e)
        {
            buttons[index].MouseClick += e;
        }

        public void SetButtonClique(string Text, MouseEventHandler e)
        {
            for (int index = 0; index < buttons.Count(); index++)
            {
                if (buttons[index].Text == Text)
                {
                    buttons[index].MouseClick += e;
                    Text = null;
                    return;
                }
            }
        }

        public void SetButtonClique(ref string Text, MouseEventHandler e)
        {
            for (int index = 0; index < buttons.Count(); index++)
            {
                if (buttons[index].Text == Text)
                {
                    buttons[index].MouseClick += e;
                    return;
                }
            }
        }

        public void AddButton(ref string[] buttonList, bool TwoColumn = false)
        {
            buttons.Clear();
            this.Controls.Clear();

            if (buttonList.Count() == 0)
            {
                buttons.Add(null);
                return;
            }

            if (TwoColumn == false)
            {
                this.TwoColumn = 1;
                for (int index = 0; index < buttonList.Count(); index++)
                {
                    buttons.Add(new Button());
                    buttons[index].Text = buttonList[index];
                    buttons[index].ID = index;
                    buttons[index].Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
                    buttons[index].TextAlign = ContentAlignment.MiddleLeft;
                    buttons[index].Size = new Size(SizeWidth - Border * 2, HeightButton);

                    if (index == 0)
                    {
                        buttons[index].Location = new Point(0 + Border, 0 + Border);
                    }
                    else
                    {
                        buttons[index].Location = new Point(0 + Border, buttons[index - 1].Location.Y + HeightButton);
                    }

                    this.Controls.Add(buttons[index]);
                }
            }
            else
            {
                this.TwoColumn = 0.5;
                for (int index = 0; index < buttonList.Count(); index++)
                {
                    buttons.Add(new Button());
                    buttons[index].Text = buttonList[index];
                    buttons[index].ID = index;
                    buttons[index].Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
                    buttons[index].TextAlign = ContentAlignment.MiddleLeft;
                    buttons[index].Size = new Size((SizeWidth - Border * 2) / 2, HeightButton);

                    if (index == 0)
                    {
                        buttons[index].Location = new Point(0 + Border, 0 + Border);
                    }
                    else
                    {
                        if (index == buttonList.Count() / 2)
                        {
                            buttons[index].Location = new Point(0 + Border + this.Width / 2, 0 + Border);
                        }
                        else if (index > buttonList.Count() / 2)
                        {
                            buttons[index].Location = new Point(0 + Border + this.Width / 2, buttons[index - 1].Location.Y + HeightButton);
                        }
                        else
                        {
                            buttons[index].Location = new Point(0 + Border, buttons[index - 1].Location.Y + HeightButton);
                        }
                    }

                    this.Controls.Add(buttons[index]);
                }
            }
        }

        public void AddButton(string[] buttonList, MouseEventNameHandler e, bool TwoColumn = false)
        {
            AddButton(ref buttonList, e, TwoColumn);
        }

        public void AddButton(ref string[] buttonList, MouseEventNameHandler e, bool TwoColumn = false)
        {
            buttons.Clear();
            this.Controls.Clear();

            if (buttonList.Count() == 0)
            {
                buttons.Add(null);
                return;
            }

            if (TwoColumn == false)
            {
                this.TwoColumn = 1;
                for (int index = 0; index < buttonList.Count(); index++)
                {
                    buttons.Add(new Button());
                    buttons[index].Text = buttonList[index];
                    buttons[index].ID = index;
                    buttons[index].Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
                    buttons[index].TextAlign = ContentAlignment.MiddleLeft;
                    buttons[index].Size = new Size(SizeWidth - Border * 2, HeightButton);
                    buttons[index].MouseClickNameString += e;

                    if (index == 0)
                    {
                        buttons[index].Location = new Point(0 + Border, 0 + Border);
                    }
                    else
                    {
                        buttons[index].Location = new Point(0 + Border, buttons[index - 1].Location.Y + HeightButton);
                    }

                    this.Controls.Add(buttons[index]);
                }
            }
            else
            {
                this.TwoColumn = 0.5;
                for (int index = 0; index < buttonList.Count(); index++)
                {
                    buttons.Add(new Button());
                    buttons[index].Text = buttonList[index];
                    buttons[index].ID = index;
                    buttons[index].Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
                    buttons[index].TextAlign = ContentAlignment.MiddleLeft;
                    buttons[index].Size = new Size((SizeWidth - Border * 2) / 2, HeightButton);
                    buttons[index].MouseClickNameString += e;

                    if (index == 0)
                    {
                        buttons[index].Location = new Point(0 + Border, 0 + Border);
                    }
                    else
                    {
                        if (index == buttonList.Count() / 2)
                        {
                            buttons[index].Location = new Point(0 + Border + this.Width / 2, 0 + Border);
                        }
                        else if (index > buttonList.Count() / 2)
                        {
                            buttons[index].Location = new Point(0 + Border + this.Width / 2, buttons[index - 1].Location.Y + HeightButton);
                        }
                        else
                        {
                            buttons[index].Location = new Point(0 + Border, buttons[index - 1].Location.Y + HeightButton);
                        }
                    }

                    this.Controls.Add(buttons[index]);
                }
            }
        }

        public void AddBar(int indexButton)
        {
            Rectangle bar = new Rectangle();
            bar.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
            bar.Location = new Point(10, buttons[indexButton - 1].Location.Y + HeightButton + 5);
            bar.Size = new Size(this.Width - 20, 2);
            bar.BackColor = Color.FromArgb(((BorderColor.R - this.BackColor.R) * -1) / -2 + 20, ((BorderColor.G - this.BackColor.G) * -1) / -2 + 20, ((BorderColor.B - this.BackColor.B) * -1) / -2 + 20);
            this.Controls.Add(bar);

            for (int index = indexButton; index < buttons.Count(); index++)
            {
                buttons[index].Location = new Point(buttons[index].Location.X, buttons[index].Location.Y + 10);
            }

            nbBar++;
        }

        public void NewPage(string[] buttonList, MouseEventHandler e, bool TwoColumns = false)
        {
            NewPage(buttonList, new MouseEventHandler[1] { e }, TwoColumns);
        }

        public async void NewPage(string[] buttonList, MouseEventHandler[] e, bool TwoColumns = false)
        {
            for (int index = 0; index < ButtonsPage.Count(); index++)
            {
                this.Controls.Remove(ButtonsPage[index]);
            }

            ButtonsPage.Clear();

            if (TwoColumns == false)
            {
                for (int index = 0; index < buttonList.Count(); index++)
                {
                    ButtonsPage.Add(new Button());
                    ButtonsPage[index].Text = buttonList[index];
                    ButtonsPage[index].ID = index;
                    ButtonsPage[index].Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
                    ButtonsPage[index].TextAlign = ContentAlignment.MiddleLeft;
                    ButtonsPage[index].Size = new Size(SizeWidth - Border * 2, HeightButton);
                    if (e.Count() == 1)
                    {
                        ButtonsPage[index].MouseClick += e[0];
                    }
                    else
                    {
                        ButtonsPage[index].MouseClick += e[index];
                    }

                    if (index == 0)
                    {
                        ButtonsPage[index].Location = new Point(-(SizeWidth - Border * 2), HeightButton + Border);
                    }
                    else
                    {
                        ButtonsPage[index].Location = new Point(-(SizeWidth - Border * 2), ButtonsPage[index - 1].Location.Y + HeightButton);
                    }

                    this.Controls.Add(ButtonsPage[index]);
                }
            }
            else
            {
                for (int index = 0; index < buttonList.Count(); index++)
                {
                    ButtonsPage.Add(new Button());
                    ButtonsPage[index].Text = buttonList[index];
                    ButtonsPage[index].ID = index;
                    ButtonsPage[index].Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
                    ButtonsPage[index].TextAlign = ContentAlignment.MiddleLeft;
                    ButtonsPage[index].Size = new Size((SizeWidth - Border * 2) / 2, HeightButton);
                    if (e.Count() == 1)
                    {
                        ButtonsPage[index].MouseClick += e[0];
                    }
                    else
                    {
                        ButtonsPage[index].MouseClick += e[index];
                    }

                    if (index == 0)
                    {
                        buttons[index].Location = new Point(-(SizeWidth - Border * 2), HeightButton + Border);
                    }
                    else
                    {
                        if (index == buttonList.Count() / 2)
                        {
                            buttons[index].Location = new Point(-(SizeWidth - Border * 2) + this.Width / 2, HeightButton + Border);
                        }
                        else if (index > buttonList.Count() / 2)
                        {
                            buttons[index].Location = new Point(-(SizeWidth - Border * 2) + this.Width / 2, ButtonsPage[index - 1].Location.Y + HeightButton);
                        }
                        else
                        {
                            buttons[index].Location = new Point(-(SizeWidth - Border * 2), ButtonsPage[index - 1].Location.Y + HeightButton);
                        }
                    }

                    this.Controls.Add(ButtonsPage[index]);
                }
            }

            Button buttonRetour = new Button();
            if (lang == 0) // if french
            {
                buttonRetour.Text = "Retour"; // french
            }
            else
            {
                buttonRetour.Text = "Back"; // english
            }
            buttonRetour.ID = -1;
            buttonRetour.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            buttonRetour.TextAlign = ContentAlignment.MiddleLeft;
            buttonRetour.Size = new Size(SizeWidth - Border * 2, HeightButton);
            buttonRetour.Location = new Point(-(SizeWidth - Border * 2), 0 + Border);
            buttonRetour.MouseClick += new MouseEventHandler(MainPage);
            this.Controls.Add(buttonRetour);

            UpdateSizeNewPage(TwoColumns);

            for (int index = -(SizeWidth - Border * 2); index < 0; index += 10)
            {
                for (int index2 = 0; index2 < this.Controls.Count; index2++)
                {
                    this.Controls[index2].Location = new Point(this.Controls[index2].Location.X + 10, this.Controls[index2].Location.Y);
                }

                await Task.Delay(5);
            }
        }

        public void NewPage(string[] buttonList, MouseEventNameHandler e, bool TwoColumns = false)
        {
             NewPage(buttonList, new MouseEventNameHandler[1] { e }, TwoColumns);
        }

        public async void NewPage(string[] buttonList, MouseEventNameHandler[] e, bool TwoColumns = false)
        {
            for (int index = 0; index < ButtonsPage.Count(); index++)
            {
                this.Controls.Remove(ButtonsPage[index]);
            }

            ButtonsPage.Clear();

            if (TwoColumns == false)
            {
                for (int index = 0; index < buttonList.Count(); index++)
                {
                    ButtonsPage.Add(new Button());
                    ButtonsPage[index].Text = buttonList[index];
                    ButtonsPage[index].ID = index;
                    ButtonsPage[index].Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
                    ButtonsPage[index].TextAlign = ContentAlignment.MiddleLeft;
                    ButtonsPage[index].Size = new Size(SizeWidth - Border * 2, HeightButton);
                    if (e.Count() == 1)
                    {
                        ButtonsPage[index].MouseClickNameString += e[0];
                    }
                    else
                    {
                        ButtonsPage[index].MouseClickNameString += e[index];
                    }

                    if (index == 0)
                    {
                        ButtonsPage[index].Location = new Point(-(SizeWidth - Border * 2), HeightButton + Border);
                    }
                    else
                    {
                        ButtonsPage[index].Location = new Point(-(SizeWidth - Border * 2), ButtonsPage[index - 1].Location.Y + HeightButton);
                    }

                    this.Controls.Add(ButtonsPage[index]);
                }
            }
            else
            {
                for (int index = 0; index < buttonList.Count(); index++)
                {
                    ButtonsPage.Add(new Button());
                    ButtonsPage[index].Text = buttonList[index];
                    ButtonsPage[index].ID = index;
                    ButtonsPage[index].Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
                    ButtonsPage[index].TextAlign = ContentAlignment.MiddleLeft;
                    ButtonsPage[index].Size = new Size((SizeWidth - Border * 2) / 2, HeightButton);
                    if (e.Count() == 1)
                    {
                        ButtonsPage[index].MouseClickNameString += e[0];
                    }
                    else
                    {
                        ButtonsPage[index].MouseClickNameString += e[index];
                    }

                    if (index == 0)
                    {
                        ButtonsPage[index].Location = new Point(-(SizeWidth - Border * 2), HeightButton + Border);
                    }
                    else
                    {
                        if (index == buttonList.Count() / 2)
                        {
                            ButtonsPage[index].Location = new Point(-(SizeWidth - Border * 2) + this.Width / 2, HeightButton + Border);
                        }
                        else if (index > buttonList.Count() / 2)
                        {
                            ButtonsPage[index].Location = new Point(-(SizeWidth - Border * 2) + this.Width / 2, ButtonsPage[index - 1].Location.Y + HeightButton);
                        }
                        else
                        {
                            ButtonsPage[index].Location = new Point(-(SizeWidth - Border * 2), ButtonsPage[index - 1].Location.Y + HeightButton);
                        }
                    }

                    this.Controls.Add(ButtonsPage[index]);
                }
            }

            Button buttonRetour = new Button();
            if (lang == 0) // if french
            {
                buttonRetour.Text = "Retour"; // french
            }
            else
            {
                buttonRetour.Text = "Back"; // english
            }
            buttonRetour.ID = -1;
            buttonRetour.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            buttonRetour.TextAlign = ContentAlignment.MiddleLeft;
            buttonRetour.Size = new Size(SizeWidth - Border * 2, HeightButton);
            buttonRetour.Location = new Point(-(SizeWidth - Border * 2), 0 + Border);
            buttonRetour.MouseClick += new MouseEventHandler(MainPage);
            this.Controls.Add(buttonRetour);

            UpdateSizeNewPage(TwoColumns);

            for (int index = -(SizeWidth - Border * 2); index < 0; index += 10)
            {
                for (int index2 = 0; index2 < this.Controls.Count; index2++)
                {
                    this.Controls[index2].Location = new Point(this.Controls[index2].Location.X + 10, this.Controls[index2].Location.Y);
                }

                await Task.Delay(5);
            }
        }

        private async void UpdateSizeNewPage(bool TwoColumns)
        {
            switch (ShowSide)
            {
                case Side.Top:
                    if (TwoColumns)
                    {
                        for (int index = 0; index < HeightButton * arrondissement(ButtonsPage.Count() / 2 - buttons.Count() / 2, TwoColumn) - 15; index += 15)
                        {
                            this.Size = new Size(this.Width, this.Height + 15);
                            await Task.Delay(5);
                        }
                    }
                    else
                    {
                        for (int index = 0; index < HeightButton * arrondissement(ButtonsPage.Count() - buttons.Count(), TwoColumn); index += 15)
                        {
                            this.Size = new Size(this.Width, this.Height + 15);
                            await Task.Delay(5);
                        }
                    }
                    break;

                case Side.Bottom:
                    if (TwoColumns)
                    {
                        for (int index = 0; index < HeightButton * arrondissement(ButtonsPage.Count() / 2 - buttons.Count() / 2, TwoColumn) - 15;)
                        {
                            this.Size = new Size(this.Width, this.Height + 15);
                            this.Location = new Point(this.Location.X, this.Location.Y - 15);
                            index += 15;
                            await Task.Delay(5);
                        }
                    }
                    else
                    {
                        for (int index = 0; index < HeightButton * arrondissement(ButtonsPage.Count() - buttons.Count(), TwoColumn);)
                        {
                            this.Size = new Size(this.Width, this.Height + 15);
                            this.Location = new Point(this.Location.X, this.Location.Y - 15);
                            index += 15;
                            await Task.Delay(5);
                        }
                    }
                    break;

                case Side.Left:
                    if (TwoColumns)
                    {
                        for (int index = 0; index < SizeWidth;)
                        {
                            this.Size = new Size(this.Width + 15, (HeightButton * arrondissement(ButtonsPage.Count() / 2 - buttons.Count() / 2, TwoColumn) + Border + (10 * nbBar) - 15));
                            index += 15;
                            await Task.Delay(5);
                        }
                    }
                    else
                    {
                        for (int index = 0; index < SizeWidth;)
                        {
                            this.Size = new Size(this.Width + 15, (HeightButton * arrondissement(ButtonsPage.Count() - buttons.Count(), TwoColumn) + Border + (10 * nbBar)));
                            index += 15;
                            await Task.Delay(5);
                        }
                    }
                    break;

                case Side.Right:
                    if (TwoColumns)
                    {
                        for (int index = 0; index < SizeWidth;)
                        {
                            this.Size = new Size(this.Width + 15, (HeightButton * arrondissement(ButtonsPage.Count() / 2 - buttons.Count() / 2, TwoColumn) + Border + (10 * nbBar) - 15));
                            this.Location = new Point(this.Location.X - 15, this.Location.Y);
                            index += 15;
                            await Task.Delay(5);
                        }
                    }
                    else
                    {
                        for (int index = 0; index < SizeWidth;)
                        {
                            this.Size = new Size(this.Width + 15, (HeightButton * arrondissement(ButtonsPage.Count() - buttons.Count(), TwoColumn) + Border + (10 * nbBar)));
                            this.Location = new Point(this.Location.X - 15, this.Location.Y);
                            index += 15;
                            await Task.Delay(5);
                        }
                    }
                    break;
            }

            if (Border != 0)
            {
                Sky_framework.Border.DrawRoundRectangle(new Pen(BorderColor, Border), 0, 0, Width - 1, Height - 1, BorderRadius_, this.CreateGraphics());
            }
            else
            {
                Sky_framework.Border.DrawRoundRectangle(new Pen(this.BackColor, 1), 0, 0, Width - 1, Height - 1, BorderRadius_, this.CreateGraphics());
            }
        }

        private async void UpdateSizeMainPage(bool TwoColumns)
        {
            switch (ShowSide)
            {
                case Side.Top:
                    if (TwoColumns)
                    {
                        for (int index = (HeightButton * arrondissement(ButtonsPage.Count() / 2 - buttons.Count() / 2, TwoColumn)) - 15; index > 0;)
                        {
                            this.Size = new Size(this.Width, this.Height - 15);
                            index -= 15;
                            await Task.Delay(5);
                        }
                    }
                    else
                    {
                        for (int index = (HeightButton * arrondissement(ButtonsPage.Count() - buttons.Count(), TwoColumn)); index > 0;)
                        {
                            this.Size = new Size(this.Width, this.Height - 15);
                            index -= 15;
                            await Task.Delay(5);
                        }
                    }
                    break;

                case Side.Bottom:
                    if (TwoColumns)
                    {
                        for (int index = (HeightButton * arrondissement(ButtonsPage.Count() / 2 - buttons.Count() / 2, TwoColumn)) - 15; index > 0;)
                        {
                            this.Size = new Size(this.Width, this.Height - 15);
                            this.Location = new Point(this.Location.X, this.Location.Y + 15);
                            index -= 15;
                            await Task.Delay(5);
                        }
                    }
                    else
                    {
                        for (int index = (HeightButton * arrondissement(ButtonsPage.Count() - buttons.Count(), TwoColumn)); index > 0;)
                        {
                            this.Size = new Size(this.Width, this.Height - 15);
                            this.Location = new Point(this.Location.X, this.Location.Y + 15);
                            index -= 15;
                            await Task.Delay(5);
                        }
                    }
                    break;

                case Side.Left:
                    if (TwoColumns)
                    {
                        for (int index = this.Width; index > 0;)
                        {
                            this.Size = new Size(this.Width - 15, (HeightButton * arrondissement(ButtonsPage.Count() / 2 - buttons.Count() / 2, TwoColumn) + Border + (10 * nbBar)) - 15);
                            index -= 15;
                            await Task.Delay(5);
                        }
                    }
                    else
                    {
                        for (int index = this.Width; index > 0;)
                        {
                            this.Size = new Size(this.Width - 15, (HeightButton * arrondissement(ButtonsPage.Count() - buttons.Count(), TwoColumn) + Border + (10 * nbBar)));
                            index -= 15;
                            await Task.Delay(5);
                        }
                    }
                    break;

                case Side.Right:
                    if (TwoColumns)
                    {
                        for (int index = this.Width; index > 0;)
                        {
                            this.Size = new Size(this.Width - 15, (HeightButton * arrondissement(ButtonsPage.Count() / 2 - buttons.Count() / 2, TwoColumn) + Border + (10 * nbBar)) - 15);
                            this.Location = new Point(this.Location.X + 15, this.Location.Y);
                            index -= 15;
                            await Task.Delay(5);
                        };
                    }
                    else
                    {
                        for (int index = this.Width; index > 0;)
                        {
                            this.Size = new Size(this.Width - 15, (HeightButton * arrondissement(ButtonsPage.Count() - buttons.Count(), TwoColumn) + Border + (10 * nbBar)));
                            this.Location = new Point(this.Location.X + 15, this.Location.Y);
                            index -= 15;
                            await Task.Delay(5);
                        };
                    }
                    break;
            }

            if (Border != 0)
            {
                Sky_framework.Border.DrawRoundRectangle(new Pen(BorderColor, Border), 0, 0, Width - 1, Height - 1, BorderRadius_, this.CreateGraphics());
            }
            else
            {
                Sky_framework.Border.DrawRoundRectangle(new Pen(this.BackColor, 1), 0, 0, Width - 1, Height - 1, BorderRadius_, this.CreateGraphics());
            }
        }

        private async void MainPage(object sender, MouseEventArgs e)
        {
            if (ButtonsPage.Count() < this.Height)
            {
                UpdateSizeMainPage(true);
            }
            else
            {
                UpdateSizeMainPage(false);
            }

            for (int index = SizeWidth - Border * 2; index > 0; index -= 10)
            {
                for (int index2 = 0; index2 < this.Controls.Count; index2++)
                {
                    this.Controls[index2].Location = new Point(this.Controls[index2].Location.X - 10, this.Controls[index2].Location.Y);
                }

                await Task.Delay(5);
            }

            for (int index = 0; index < this.Controls.Count; index++)
            {
                if (this.Controls[index].Location.X < -1)
                {
                    //this.Controls[index].Dispose();
                    this.Controls.Remove(this.Controls[index]);
                }
            }
        }

        public Side ShowSide
        {
            get
            {
                return ShowSide_;
            }
            set
            {
                this.View = false;
                this.Visible = true;

                ShowSide_ = value;

                switch (value)
                {
                    case Side.Top:
                        this.Width = SizeWidth;
                        this.Height = 0;
                        break;

                    case Side.Bottom:
                        this.Width = SizeWidth;
                        this.Height = 0;
                        break;

                    case Side.Left:
                        this.Width = 0;
                        this.Height = SizeHeight;
                        break;

                    case Side.Right:
                        this.Width = 0;
                        this.Height = SizeHeight;
                        break;
                }
            }
        }

        private int SizeHeight 
        {   
            get
            {
                return SizeHeight_;
            }
            set
            {
                SizeHeight_ = value;

                for (int index = 0; index < buttons.Count(); index++)
                {
                    if (buttons[index] != null)
                    {
                        buttons[index].Width = (int)(SizeWidth * TwoColumn);
                    }
                }
            }
        }

        private int SizeWidth 
        { 
            get
            {
                return SizeWidth_;
            }
            set
            {
                SizeWidth_ = value;

                for (int index = 0; index < buttons.Count(); index++)
                {
                    if (buttons[index] != null)
                    {
                        buttons[index].Width = (int)(value * TwoColumn);
                    }
                }
            }
        }

        new public int Width
        {
            get
            {
                return base.Width;
            }
            set
            {
                base.Width = value;
                SizeWidth = value;
            }
        }

        new public int Height
        {
            get
            {
                return base.Height;
            }
            private set
            {
                base.Height = value;
                SizeHeight = value;
            }
        }

        private int arrondissement(int nb, double multiplicateur)
        {
            string nbString = (nb * multiplicateur).ToString();
            bool virgule = false;

            foreach (char i in nbString)
            {
                if (i == ',')
                {
                    virgule = true;
                }
                else
                {
                    if (virgule == true)
                    {
                        nbString += i;
                    }
                }
            }

            int result;

            if (virgule)
            {
                double r = Convert.ToSingle(nbString);
                r *= Math.Pow(10, -nbString.Length);
                r = 1 - r;
                r = (nb * multiplicateur) + r;
                result = (int)r;
            }
            else
            {
                result = (int)(nb * multiplicateur);
            }

            nbString = null;
            return result;
        }

        new public void Show()
        {
            Show(0);
        }

        public async void Show(int SizeNBButton)
        {
            if (View == true)
            {
                switch (ShowSide)
                {
                    case Side.Top:
                        this.Height = 0;
                        View = false;
                        this.Visible = false;
                        break;

                    case Side.Bottom:
                        this.Location = new Point(this.Location.X, this.Location.Y - this.Height);
                        this.Height = 0;
                        View = false;
                        this.Visible = false;
                        break;

                    case Side.Left:
                        this.Size = new Size(0, (HeightButton * arrondissement(buttons.Count(), TwoColumn) + Border + (10 * nbBar)));
                        View = false;
                        this.Visible = false;
                        break;

                    case Side.Right:
                        this.Location = new Point(this.Location.X - this.Width, this.Location.Y);
                        this.Size = new Size(0, (HeightButton * arrondissement(buttons.Count(), TwoColumn) + Border + (10 * nbBar)));
                        View = false;
                        this.Visible = false;
                        break;
                }
            }

            switch (ShowSide)
            {
                case Side.Top:
                    this.Visible = true;
                    for (int index = 0; index < HeightButton * arrondissement(buttons.Count() + SizeNBButton, TwoColumn); index += 15)
                    {
                        this.Size = new Size(this.Width, this.Height + 15);
                        await Task.Delay(5);
                    }
                    this.Height += Border * 2 + (10 * nbBar);
                    View = true;
                    break;

                case Side.Bottom:
                    this.Visible = true;
                    for (int index = 0; index < HeightButton * arrondissement(buttons.Count() + SizeNBButton, TwoColumn);)
                    {
                        this.Size = new Size(this.Width, this.Height + 15);
                        this.Location = new Point(this.Location.X, this.Location.Y - 15);
                        index += 15;
                        await Task.Delay(5);
                    }
                    this.Height += Border * 2 + (10 * nbBar);
                    this.Location = new Point(this.Location.X, this.Location.Y + Border + (10 * nbBar));
                    View = true;
                    break;

                case Side.Left:
                    this.Visible = true;
                    for (int index = 0; index < SizeWidth;)
                    {
                        this.Size = new Size(this.Width + 15, (HeightButton * arrondissement(buttons.Count() + SizeNBButton, TwoColumn) + Border + (10 * nbBar)));
                        index += 15;
                        await Task.Delay(5);
                    }
                    View = true;
                    break;

                case Side.Right:
                    this.Visible = true;
                    for (int index = 0; index < SizeWidth;)
                    {
                        this.Size = new Size(this.Width + 15, (HeightButton * arrondissement(buttons.Count() + SizeNBButton, TwoColumn) + Border + (10 * nbBar)));
                        this.Location = new Point(this.Location.X - 15, this.Location.Y);
                        index += 15;
                        await Task.Delay(5);
                    }
                    View = true;
                    break;
            }

            this.SizeNBButton = SizeNBButton;

            //ControlPaint.DrawBorder(CreateGraphics(), this.ClientRectangle, BorderColor, Border, ButtonBorderStyle.Solid, BorderColor, Border, ButtonBorderStyle.Solid,
            //BorderColor, Border, ButtonBorderStyle.Solid, BorderColor, Border, ButtonBorderStyle.Solid);
            if (Border != 0)
            {
                Sky_framework.Border.DrawRoundRectangle(new Pen(BorderColor, Border), 0, 0, Width - 1, Height - 1, BorderRadius_, this.CreateGraphics());
            }
            else
            {
                Sky_framework.Border.DrawRoundRectangle(new Pen(this.BackColor, 1), 0, 0, Width - 1, Height - 1, BorderRadius_, this.CreateGraphics());
            }
        }

        new public async void Hide()
        {
            if (View == false)
            {
                return;
            }

            switch (ShowSide)
            {
                case Side.Top:
                    for (int index = (HeightButton * arrondissement(buttons.Count() + SizeNBButton, TwoColumn)); index >= 0;)
                    {
                        this.Size = new Size(this.Width, this.Height - 15);
                        index -= 15;
                        await Task.Delay(5);
                    }
                    this.Height -= Border * 2 + (10 * nbBar);
                    View = false;
                    this.Visible = false;
                    break;

                case Side.Bottom:
                    for (int index = (HeightButton * arrondissement(buttons.Count() + SizeNBButton, TwoColumn)); index >= 0;)
                    {
                        this.Size = new Size(this.Width, this.Height - 15);
                        this.Location = new Point(this.Location.X, this.Location.Y + 15);
                        index -= 15;
                        await Task.Delay(5);
                    }
                    this.Height -= Border * 2 + (10 * nbBar);
                    this.Location = new Point(this.Location.X, this.Location.Y - Border - (10 * nbBar) - 15);
                    View = false;
                    this.Visible = false;
                    break;

                case Side.Left:
                    for (int index = this.Width; index >= 0;)
                    {
                        this.Size = new Size(this.Width - 15, (HeightButton * arrondissement(buttons.Count() + SizeNBButton, TwoColumn) + Border + (10 * nbBar)));
                        index -= 15;
                        await Task.Delay(5);
                    }
                    View = false;
                    this.Visible = false;
                    break;

                case Side.Right:
                    for (int index = this.Width; index >= 0;)
                    {
                        this.Size = new Size(this.Width - 15, (HeightButton * arrondissement(buttons.Count() + SizeNBButton, TwoColumn) + Border + (10 * nbBar)));
                        this.Location = new Point(this.Location.X + 15, this.Location.Y);
                        index -= 15;
                        await Task.Delay(5);
                    }
                    View = false;
                    this.Visible = false;
                    break;
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
                Refresh();
            }
        }
    }

    public enum Side
    {
        Top = 0,
        Bottom = 1,
        Left = 2,
        Right = 3,
    }
}
