using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace Assignment4Group59

{
    internal interface IDrawable
    {
        void Draw(Graphics g);
    }

    internal interface IWritable
    {
        void Write(TextWriter writer);
    }

    internal abstract class Primitive
    {
        protected Color color;
        protected bool filled;
        protected Rectangle boundingRectangle;

        public Primitive(Color color, bool filled, Rectangle rectangle)
        {
            this.color = color;
            this.filled = filled;
            this.boundingRectangle = rectangle;
        }
    }

    internal class DrawableRectangle : Primitive, IDrawable, IWritable
    {
        internal DrawableRectangle(Color color, bool filled, Rectangle rectangle) : base(color, filled, rectangle)
        {
        }

        public void Draw(Graphics g)
        {
            if (filled)
            {
                SolidBrush brush = new SolidBrush(color);
                g.FillRectangle(brush, boundingRectangle);
            }
            else
            {
                Pen pen = new Pen(color);
                g.DrawRectangle(pen, boundingRectangle);
            }
        }

        public void Write(TextWriter writer)
        {
            writer.WriteLine($"Color: {this.color} Rectangle: {this.boundingRectangle} Filled: {this.filled}");
        }

        public double Area()
        {
            return this.boundingRectangle.Width * this.boundingRectangle.Height;
        }
    }

    internal class DrawableEllipse : Primitive, IDrawable
    {
        internal DrawableEllipse(Color color, bool filled, Rectangle rectangle) : base(color, filled, rectangle)
        {
        }

        public void Draw(Graphics g)
        {
            if (filled)
            {
                SolidBrush brush = new SolidBrush(color);
                g.FillEllipse(brush, boundingRectangle);
            }
            else
            {
                Pen pen = new Pen(color);
                g.DrawEllipse(pen, boundingRectangle);
            }
        }

        public double Area()
        {
            return this.boundingRectangle.Width / 2 * this.boundingRectangle.Height / 2 * Math.PI;
        }
    }

    internal class DrawableLine : IDrawable, IWritable
    {
        protected Color color;
        protected Point lineStart;
        protected Point lineEnd;

        internal DrawableLine(Color color, Point start, Point end)
        {
            this.color = color;
            this.lineStart = start;
            this.lineEnd = end;
        }

        public void Draw(Graphics g)
        {
            Pen pen = new Pen(color);
            g.DrawLine(pen, this.lineStart, this.lineEnd);
        }

        public void Write(TextWriter writer)
        {
            writer.WriteLine($"Color: {this.color} Line Start: {this.lineStart} Line End: {this.lineEnd}");
        }
    }

    internal class DrawableBezier : IDrawable, IWritable
    {
        protected Color color;
        protected Point curveStart;
        protected Point controlFirst;
        protected Point controlSecond;
        protected Point curveEnd;

        internal DrawableBezier(Color color, Point start, Point first, Point second, Point end)
        {
            this.color = color;
            this.curveStart = start;
            this.controlFirst = first;
            this.controlSecond = second;
            this.curveEnd = end;
        }

        public void Draw(Graphics g)
        {
            Pen pen = new Pen(color);
            g.DrawBezier(pen, curveStart, controlFirst, controlSecond, curveEnd);
        }

        public void Write(TextWriter writer)
        {
            writer.WriteLine($"Color: {color} Curve Start: {curveStart} Control First: {controlFirst} Control Second: {controlSecond} Curve End: {curveEnd}");
        }
    }

    internal class DrawableArc : Primitive, IDrawable, IWritable
    {
        protected float start;
        protected float end;

        internal DrawableArc(Color color, bool filled, Rectangle rectangle, float start, float end) : base(color, filled, rectangle)
        {
            this.start = start;
            this.end = end;
        }

        public void Draw(Graphics g)
        {
            Pen pen = new Pen(color);
            g.DrawArc(pen, boundingRectangle, start, end);
        }

        public void Write(TextWriter writer)
        {
            writer.WriteLine($"Color: {color} Filled: {filled} Rectangle: {boundingRectangle} Start: {start} End: {end}");
        }
    }
}