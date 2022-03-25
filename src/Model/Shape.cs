using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Draw
{
	[Serializable]
	/// <summary>
	/// Базовия клас на примитивите, който съдържа общите характеристики на примитивите.
	/// </summary>
	public abstract class Shape : ISerializable
	{
		#region Constructors
		
		public Shape()
		{
		}
		
		public Shape(RectangleF rect)
		{
			rectangle = rect;
		}
		
		public Shape(Shape shape)
		{
			this.Height = shape.Height;
			this.Width = shape.Width;
			this.Location = shape.Location;
			this.rectangle = shape.rectangle;
			
			this.FillColor =  shape.FillColor;
			this.StrokeColor = shape.StrokeColor;
			this.StrokeWidth = shape.StrokeWidth;
		}
		#endregion
		
		#region Properties
		
		/// <summary>
		/// Обхващащ правоъгълник на елемента.
		/// </summary>
		private RectangleF rectangle;		
		public virtual RectangleF Rectangle {
			get { return rectangle; }
			set { rectangle = value; }
		}
		
		/// <summary>
		/// Широчина на елемента.
		/// </summary>
		public virtual float Width {
			get { return Rectangle.Width; }
			set { rectangle.Width = value; }
		}
		
		/// <summary>
		/// Височина на елемента.
		/// </summary>
		public virtual float Height {
			get { return Rectangle.Height; }
			set { rectangle.Height = value; }
		}
		
		/// <summary>
		/// Горен ляв ъгъл на елемента.
		/// </summary>
		public virtual PointF Location {
			get { return Rectangle.Location; }
			set { rectangle.Location = value; }
		}
		
		/// <summary>
		/// Цвят на елемента.
		/// </summary>
		private Color fillColor;		
		public virtual Color FillColor {
			get { return fillColor; }
			set { fillColor = value; }
		}

		/// Цвят на контура.
		private Color strokeColor;
		public virtual Color StrokeColor
		{
			get { return strokeColor; }
			set { strokeColor = value; }
		}

		/// Дебелина на контура.
		private int strokeWidth;
		public virtual int StrokeWidth
		{
			get { return strokeWidth; }
			set { strokeWidth = value; }
		}

		#endregion


		/// <summary>
		/// Проверка дали точка point принадлежи на елемента.
		/// </summary>
		/// <param name="point">Точка</param>
		/// <returns>Връща true, ако точката принадлежи на елемента и
		/// false, ако не пренадлежи</returns>
		public virtual bool Contains(PointF point)
		{
			return Rectangle.Contains(point.X, point.Y);
		}
		
		/// <summary>
		/// Визуализира елемента.
		/// </summary>
		/// <param name="grfx">Къде да бъде визуализиран елемента.</param>
		public virtual void DrawSelf(Graphics grfx)
		{
			// shape.Rectangle.Inflate(shape.BorderWidth, shape.BorderWidth);
		}

		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("Height", Height);
			info.AddValue("Width", Width);
			info.AddValue("Location", Location);
			info.AddValue("rectangle", rectangle);

			info.AddValue("FillColor", FillColor);
			info.AddValue("StrokeColor", StrokeColor);
			info.AddValue("StrokeWidth", StrokeWidth);
		}

		public Shape(SerializationInfo info, StreamingContext context)
		{
			Height = (float)info.GetValue("Height", typeof(float));
			Width = (float)info.GetValue("Width", typeof(float));
			Location = (PointF)info.GetValue("Location", typeof(PointF));
			rectangle = (RectangleF)info.GetValue("rectangle", typeof(RectangleF));

			FillColor = (Color)info.GetValue("FillColor", typeof(Color));
			StrokeColor = (Color)info.GetValue("StrokeColor", typeof(Color));
			StrokeWidth = (int)info.GetValue("StrokeWidth", typeof(int));
		}
	}
}
