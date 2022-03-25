using Draw.src.Model;
using System;
using System.Drawing;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Draw
{
	/// <summary>
	/// Класът, който ще бъде използван при управляване на диалога.
	/// </summary>
	public class DialogProcessor : DisplayProcessor
	{
		#region Constructor
		
		public DialogProcessor()
		{
		}
		
		#endregion
		
		#region Properties
		
		/// <summary>
		/// Избран елемент.
		/// </summary>
		private List<Shape> selection = new List<Shape>();
		public List<Shape> Selection {
			get { return selection; }
			set { selection = value; }
		}
		
		/// <summary>
		/// Дали в момента диалога е в състояние на "влачене" на избрания елемент.
		/// </summary>
		private bool isDragging;
		public bool IsDragging {
			get { return isDragging; }
			set { isDragging = value; }
		}
		
		/// <summary>
		/// Последна позиция на мишката при "влачене".
		/// Използва се за определяне на вектора на транслация.
		/// </summary>
		private PointF lastLocation;
		public PointF LastLocation {
			get { return lastLocation; }
			set { lastLocation = value; }
		}
		
		#endregion
		
		/// <summary>
		/// Добавя примитив - правоъгълник на произволно място върху клиентската област.
		/// </summary>
		public void AddRandomRectangle(int width, int height)
		{
			Random rnd = new Random();
			int x = rnd.Next(100,1000);
			int y = rnd.Next(100,600);

			RectangleShape rect = new RectangleShape(new Rectangle(x,y,width,height));
			rect.FillColor = Color.White;                       
			rect.StrokeColor = Color.Magenta;
			rect.StrokeWidth = 2;

			ShapeList.Add(rect);
		}

		/// Добавя примитив - елипса на произволно място върху клиентската област.
		public void AddRandomEllipse(int width, int height)
		{
			Random rnd = new Random();
			int x = rnd.Next(100, 1000);
			int y = rnd.Next(100, 600);

			EllipseShape ellipse = new EllipseShape(new Rectangle(x, y, width, height));
			ellipse.FillColor = Color.White;
			ellipse.StrokeColor = Color.Purple;
			ellipse.StrokeWidth = 5;

			ShapeList.Add(ellipse);
		}

		/// Добавя примитив - квадрат на произволно място върху клиентската област.
		public void AddRandomSquare(int width, int height)
		{
			Random rnd = new Random();
			int x = rnd.Next(100, 1000);
			int y = rnd.Next(100, 600);

			SquareShape square = new SquareShape(new Rectangle(x, y, width, height));
			square.FillColor = Color.White;
			square.StrokeColor = Color.BlueViolet;
			square.StrokeWidth = 7;

			ShapeList.Add(square);
		}

		public void AddStar()
		{
			StarShape star = new StarShape(new Rectangle(0, 0, 0, 0));
			star.StrokeColor = Color.Plum;
			star.StrokeWidth = 5;

			ShapeList.Add(star);
		}

		public void Delete(Shape sel) 
		{
			ShapeList.Remove(sel);

		}

		/// <summary>
		/// Проверява дали дадена точка е в елемента.
		/// Обхожда в ред обратен на визуализацията с цел намиране на
		/// "най-горния" елемент т.е. този който виждаме под мишката.
		/// </summary>
		/// <param name="point">Указана точка</param>
		/// <returns>Елемента на изображението, на който принадлежи дадената точка.</returns>
		public Shape ContainsPoint(PointF point)
		{
			for(int i = ShapeList.Count - 1; i >= 0; i--){
				if (ShapeList[i].Contains(point)){
				///	ShapeList[i].FillColor = Color.LightGray;
						
					return ShapeList[i];
				}	
			}
			return null;
		}
		
		/// <summary>
		/// Транслация на избраният елемент на вектор определен от <paramref name="p>p</paramref>
		/// </summary>
		/// <param name="p">Вектор на транслация.</param>
		public void TranslateTo(PointF p)
		{
			foreach (Shape item in Selection)
				item.Location = new PointF(item.Location.X + p.X - lastLocation.X, item.Location.Y + p.Y - lastLocation.Y);
				
			lastLocation = p;
		}

		public void Save()
		{
			Stream stream = File.Open("ShapeData.dat", FileMode.Create);

			BinaryFormatter bf = new BinaryFormatter();

			bf.Serialize(stream, ShapeList);

			stream.Close();

		}

		public void OpenLastSaved()
		{
			Stream stream = File.Open("ShapeData.dat", FileMode.Open);

			BinaryFormatter bf = new BinaryFormatter();

			ShapeList = (List<Shape>)bf.Deserialize(stream);

			stream.Close();
		}
	}
}
