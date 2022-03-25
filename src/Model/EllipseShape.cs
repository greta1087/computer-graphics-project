using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Draw.src.Model
{
	[Serializable]
    public class EllipseShape : Shape
	{
		public EllipseShape(RectangleF rect) : base(rect)
		{
		}

		public override bool Contains(PointF point)
		{

			if (base.Contains(point))
			{
				float a = Width / 2;
				float b = Height / 2;
				float x = Location.X + a;
				float y = Location.Y + b;

				return Math.Pow((point.X - x) / a, 2) + Math.Pow((point.Y - y) / b, 2) - 1 <= 0;
			}
			else

				return false;
		}

		public override void DrawSelf(Graphics grfx)
		{
			base.DrawSelf(grfx);

			grfx.FillEllipse(new SolidBrush(FillColor), base.Rectangle.X, base.Rectangle.Y, base.Rectangle.Width, base.Rectangle.Height);
			grfx.DrawEllipse(new Pen(StrokeColor, StrokeWidth), base.Rectangle.X, base.Rectangle.Y, base.Rectangle.Width, base.Rectangle.Height);

		}

		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("Height", Height);
			info.AddValue("Width", Width);
			info.AddValue("Location", Location);

			info.AddValue("FillColor", FillColor);
			info.AddValue("StrokeColor", StrokeColor);
			info.AddValue("StrokeWidth", StrokeWidth);
		}

		public EllipseShape(SerializationInfo info, StreamingContext context)
		{
			Height = (float)info.GetValue("Height", typeof(float));
			Width = (float)info.GetValue("Width", typeof(float));
			Location = (PointF)info.GetValue("Location", typeof(PointF));

			FillColor = (Color)info.GetValue("FillColor", typeof(Color));
			StrokeColor = (Color)info.GetValue("StrokeColor", typeof(Color));
			StrokeWidth = (int)info.GetValue("StrokeWidth", typeof(int));
		}
	}
}
