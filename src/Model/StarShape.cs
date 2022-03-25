using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Windows.Shapes;

namespace Draw.src.Model
{
	[Serializable]
	public class StarShape : Shape
	{
		public StarShape(RectangleF rect) : base(rect)
		{
		}

		public override bool Contains(PointF point)
		{
			if (base.Contains(point))

				return true;
			else

				return false;
		}

		public override void DrawSelf(Graphics grfx)
		{
			base.DrawSelf(grfx);

		///	grfx.DrawLine(new Pen(StrokeColor, StrokeWidth), 600, 200, 500, 400);
		///	grfx.DrawLine(new Pen(StrokeColor, StrokeWidth), 600, 200, 700, 400);
		///	grfx.DrawLine(new Pen(StrokeColor, StrokeWidth), 465, 270, 735, 270);
		///	grfx.DrawLine(new Pen(StrokeColor, StrokeWidth), 465, 272, 700, 400);
		///	grfx.DrawLine(new Pen(StrokeColor, StrokeWidth), 500, 400, 735, 272);

			grfx.DrawLine(new Pen(StrokeColor, StrokeWidth), 250, 100, 150, 300);
			grfx.DrawLine(new Pen(StrokeColor, StrokeWidth), 250, 100, 350, 300);
			grfx.DrawLine(new Pen(StrokeColor, StrokeWidth), 115, 170, 385, 170);
			grfx.DrawLine(new Pen(StrokeColor, StrokeWidth), 115, 172, 350, 300);
			grfx.DrawLine(new Pen(StrokeColor, StrokeWidth), 150, 300, 385, 172);

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

		public StarShape(SerializationInfo info, StreamingContext context)
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
