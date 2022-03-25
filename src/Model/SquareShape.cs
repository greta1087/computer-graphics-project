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
	public class SquareShape : Shape
	{
		public SquareShape(RectangleF rect) : base(rect)
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

			grfx.FillRectangle(new SolidBrush(FillColor), base.Rectangle.X, base.Rectangle.Y, base.Rectangle.Width, base.Rectangle.Height);
			grfx.DrawRectangle(new Pen(StrokeColor, StrokeWidth), base.Rectangle.X, base.Rectangle.Y, base.Rectangle.Width, base.Rectangle.Height);

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

		public SquareShape(SerializationInfo info, StreamingContext context)
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