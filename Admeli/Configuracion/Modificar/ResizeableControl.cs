using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace Admeli.Configuracion.Modificar
{
    class ResizeableControl
    {

        /* ****** TO USE: ********************************************************** *
         * Ex. "ResizeableControl RC = new ResizeableControl(button1);"
         * ************************************************************************* */


        private Control mControl;
        private bool mMouseDown = false;
        public EdgeEnum mEdge = EdgeEnum.None;
        private int mWidth = 8;
        private bool mOutlineDrawn = false;
        int mouseX;
        int mouseY;

        public enum EdgeEnum
        {
            None,
            Right,
            Left,
            Top,
            Bottom,
            Moving,
            BottomRight,
            BottomLeft,
            TopLeft,
            TopRight
        };




        public ResizeableControl(Control Control)
        {
            mControl = Control;

            mControl.MouseDown += new System.Windows.Forms.MouseEventHandler(mControl_MouseDown);
            mControl.MouseUp += new System.Windows.Forms.MouseEventHandler(mControl_MouseUp);
            mControl.MouseMove += new System.Windows.Forms.MouseEventHandler(mControl_MouseMove);
            mControl.MouseLeave += new System.EventHandler(mControl_MouseLeave);
        }
        public ResizeableControl(Control Control,int estado)
        {
            mControl = Control;

            mControl.MouseDown -= new System.Windows.Forms.MouseEventHandler(mControl_MouseDown);
            mControl.MouseUp -= new System.Windows.Forms.MouseEventHandler(mControl_MouseUp);
            mControl.MouseMove -= new System.Windows.Forms.MouseEventHandler(mControl_MouseMove);
            mControl.MouseLeave -= new System.EventHandler(mControl_MouseLeave);
        }



        public void mControl_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            //this.pPantallaVentas.Refresh();
            mouseX = e.X;
            mouseY = e.Y;
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                mMouseDown = true;
            }
        }

        public void mControl_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            mMouseDown = false;
        }



        private Control SelControl;
        public void mControl_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            Control c = ((Control)sender);
            Graphics g = c.CreateGraphics();
            switch (mEdge)
            {
                case EdgeEnum.Moving:
                    mOutlineDrawn = true;
                    break;
                case EdgeEnum.BottomRight:
                    DrawOrangeSquares(c, g);
                    g.FillRectangle(Brushes.Black, c.Width - mWidth, c.Height - mWidth, mWidth, mWidth);
                    mOutlineDrawn = true;
                    break;
                case EdgeEnum.BottomLeft:
                    DrawOrangeSquares(c, g);
                    g.FillRectangle(Brushes.Fuchsia, 0, c.Height - mWidth, mWidth, mWidth);
                    mOutlineDrawn = true;
                    break;
                case EdgeEnum.TopRight:
                    DrawOrangeSquares(c, g);
                    g.FillRectangle(Brushes.Fuchsia, c.Width - mWidth, 0, mWidth, mWidth); //top right
                    mOutlineDrawn = true;
                    break;
                case EdgeEnum.TopLeft:
                    DrawOrangeSquares(c, g);
                    g.FillRectangle(Brushes.Fuchsia, 0, 0, mWidth, mWidth); //top left
                    mOutlineDrawn = true;
                    break;
                case EdgeEnum.Left:
                    DrawOrangeSquares(c, g);
                    g.FillRectangle(Brushes.Fuchsia, 0, c.Height / 2 - 4, mWidth, mWidth); //left
                    mOutlineDrawn = true;
                    break;
                case EdgeEnum.Right:
                    DrawOrangeSquares(c, g);
                    g.FillRectangle(Brushes.Fuchsia, c.Width - mWidth, c.Height / 2 - 4, mWidth, mWidth); //right
                    mOutlineDrawn = true;
                    break;
                case EdgeEnum.Top:
                    DrawOrangeSquares(c, g);
                    g.FillRectangle(Brushes.Fuchsia, c.Width / 2 - 4, 0, mWidth, mWidth); //top
                    mOutlineDrawn = true;
                    break;
                case EdgeEnum.Bottom:
                    DrawOrangeSquares(c, g);
                    g.FillRectangle(Brushes.Fuchsia, c.Width / 2 - 4, c.Height - mWidth, mWidth, mWidth); //bottom
                    mOutlineDrawn = true;
                    break;
                case EdgeEnum.None:
                    if (mOutlineDrawn)
                    {
                        c.Refresh();
                        mOutlineDrawn = false;
                    }
                    break;
            }

            if (mMouseDown & mEdge != EdgeEnum.None)
            {
                //MessageBox.Show(Convert.ToString(c));
                c.SuspendLayout();
                switch (mEdge)
                {
                    case EdgeEnum.TopRight:
                        c.SetBounds(c.Left, c.Top, c.Width - (c.Width - e.X), c.Height);
                        c.SetBounds(c.Left, c.Top + e.Y, c.Width, c.Height - e.Y);
                        break;
                    case EdgeEnum.TopLeft:
                        c.SetBounds(c.Left + e.X, c.Top, c.Width - e.X, c.Height);
                        c.SetBounds(c.Left, c.Top + e.Y, c.Width, c.Height - e.Y);
                        break;
                    case EdgeEnum.BottomRight:
                        c.SetBounds(c.Left, c.Top, c.Width - (c.Width - e.X), c.Height);
                        c.SetBounds(c.Left, c.Top, c.Width, c.Height - (c.Height - e.Y));
                        break;
                    case EdgeEnum.BottomLeft:
                        c.SetBounds(c.Left + e.X, c.Top, c.Width - e.X, c.Height);
                        c.SetBounds(c.Left, c.Top, c.Width, c.Height - (c.Height - e.Y));
                        break;
                    case EdgeEnum.Left:
                        c.SetBounds(c.Left + e.X, c.Top, c.Width - e.X, c.Height);
                        break;
                    case EdgeEnum.Right:
                        c.SetBounds(c.Left, c.Top, c.Width - (c.Width - e.X), c.Height);
                        break;
                    case EdgeEnum.Top:
                        c.SetBounds(c.Left, c.Top + e.Y, c.Width, c.Height - e.Y);
                        break;
                    case EdgeEnum.Bottom:
                        c.SetBounds(c.Left, c.Top, c.Width, c.Height - (c.Height - e.Y));
                        break;
                    case EdgeEnum.Moving:
                        c.SetBounds(c.Left + e.X - mouseX, c.Top + e.Y - mouseY, c.Width, c.Height);
                        break;
                }
                c.ResumeLayout();
            }
            else
            {

                if (e.X > c.Width - (mWidth) & e.Y > c.Height - (mWidth)) //Bottom right corner
                {
                    c.Cursor = Cursors.SizeNWSE;
                    mEdge = EdgeEnum.BottomRight;
                }
                else if (e.X <= mWidth & e.Y > c.Height - (mWidth)) //Bottom Left corner
                {
                    c.Cursor = Cursors.SizeNESW;
                    mEdge = EdgeEnum.BottomLeft;
                }
                else if (e.X > c.Width - (mWidth) & e.Y <= mWidth) //Top Right corner
                {
                    c.Cursor = Cursors.SizeNESW;
                    mEdge = EdgeEnum.TopRight;
                }
                else if (e.X <= mWidth & e.Y <= mWidth) //Top Left corner
                {
                    c.Cursor = Cursors.SizeNWSE;
                    mEdge = EdgeEnum.TopLeft;
                }
                else if (e.X <= mWidth & e.Y >= c.Height / 2 - 4 & e.Y <= c.Height / 2 + 4) //left edge
                {
                    c.Cursor = Cursors.SizeWE;
                    mEdge = EdgeEnum.Left;
                }
                else if (e.X >= c.Width - mWidth & e.Y >= c.Height / 2 - 4 & e.Y <= c.Height / 2 + 4) //right edge
                {
                    c.Cursor = Cursors.SizeWE;
                    mEdge = EdgeEnum.Right;
                }
                else if (e.X >= c.Width / 2 - 4 & e.X <= c.Width / 2 + 4 & e.Y <= mWidth) //top edge
                {
                    c.Cursor = Cursors.SizeNS;
                    mEdge = EdgeEnum.Top;
                }
                else if (e.X >= c.Width / 2 - 4 & e.X <= c.Width / 2 + 4 & e.Y >= c.Height - mWidth) //bottom edge
                {
                    c.Cursor = Cursors.SizeNS;
                    mEdge = EdgeEnum.Bottom;
                }
                else if (e.X <= c.Width & e.Y <= c.Height) //move
                {
                    c.Cursor = Cursors.SizeAll;
                    mEdge = EdgeEnum.Moving;
                    DrawOrangeSquares(c, g);
                }
                else                        //no edge
                {
                    c.Cursor = Cursors.Default;
                    mEdge = EdgeEnum.None;
                }


            }


        }

        public void mControl_MouseLeave(object sender, System.EventArgs e)
        {

            Control c = ((Control)sender);
            mEdge = EdgeEnum.None;
            c.Refresh();
            c.Cursor = Cursors.Default;

        }

        internal void DrawOrangeSquares(Control c, Graphics g)
        {

            g.FillRectangle(Brushes.White, c.Width - mWidth - 1, c.Height - mWidth - 1, mWidth + 1, mWidth + 1);
            g.FillRectangle(Brushes.OrangeRed, c.Width - mWidth, c.Height - mWidth, mWidth, mWidth); //bottom right

            g.FillRectangle(Brushes.White, 0, c.Height - mWidth - 1, mWidth + 1, mWidth + 1);
            g.FillRectangle(Brushes.OrangeRed, 0, c.Height - mWidth, mWidth, mWidth); //bottom left

            g.FillRectangle(Brushes.White, 0, c.Height / 2 - 5, mWidth + 1, mWidth + 2);
            g.FillRectangle(Brushes.OrangeRed, 0, c.Height / 2 - 4, mWidth, mWidth); //left

            g.FillRectangle(Brushes.White, c.Width - mWidth - 1, c.Height / 2 - 5, mWidth + 1, mWidth + 2);
            g.FillRectangle(Brushes.OrangeRed, c.Width - mWidth, c.Height / 2 - 4, mWidth, mWidth); //right

            g.FillRectangle(Brushes.White, c.Width / 2 - 5, 0, mWidth + 2, mWidth + 1);
            g.FillRectangle(Brushes.OrangeRed, c.Width / 2 - 4, 0, mWidth, mWidth); //top

            g.FillRectangle(Brushes.White, c.Width / 2 - 5, c.Height - mWidth - 1, mWidth + 2, mWidth + 1);
            g.FillRectangle(Brushes.OrangeRed, c.Width / 2 - 4, c.Height - mWidth, mWidth, mWidth); //bottom

            g.FillRectangle(Brushes.White, c.Width - mWidth - 1, 0, mWidth + 1, mWidth + 1);
            g.FillRectangle(Brushes.OrangeRed, c.Width - mWidth, 0, mWidth, mWidth); //top right

            g.FillRectangle(Brushes.White, 0, 0, mWidth + 1, mWidth + 1);
            g.FillRectangle(Brushes.OrangeRed, 0, 0, mWidth, mWidth); //top left
        }


    }
}