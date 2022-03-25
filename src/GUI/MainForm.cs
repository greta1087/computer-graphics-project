using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Draw
{
	/// <summary>
	/// Върху главната форма е поставен потребителски контрол,
	/// в който се осъществява визуализацията
	/// </summary>
	public partial class MainForm : Form
	{
		/// <summary>
		/// Агрегирания диалогов процесор във формата улеснява манипулацията на модела.
		/// </summary>
		private DialogProcessor dialogProcessor = new DialogProcessor();
		public MainForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();

			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}

		/// <summary>
		/// Изход от програмата. Затваря главната форма, а с това и програмата.
		/// </summary>
		void ExitToolStripMenuItemClick(object sender, EventArgs e)
		{
			Close();
		}

		/// <summary>
		/// Събитието, което се прихваща, за да се превизуализира при изменение на модела.
		/// </summary>
		void ViewPortPaint(object sender, PaintEventArgs e)
		{
			dialogProcessor.ReDraw(sender, e);
		}

		/// <summary>
		/// Бутон, който поставя на произволно място правоъгълник със зададените размери.
		/// Променя се лентата със състоянието и се инвалидира контрола, в който визуализираме.
		/// </summary>
		void DrawRectangleSpeedButtonClick(object sender, EventArgs e)
		{
			int width;
			int height;

			if (String.IsNullOrEmpty(textBoxWidth.Text) && String.IsNullOrEmpty(textBoxHeight.Text))
			{
				width = 200;
				height = 100;
				
			} else
			{
				width = Int32.Parse(textBoxWidth.Text);
				height = Int32.Parse(textBoxHeight.Text);
			}

		    dialogProcessor.AddRandomRectangle(width, height);

			statusBar.Items[0].Text = "Последно действие: Рисуване на правоъгълник";

			viewPort.Invalidate();
		}

		/// <summary>
		/// Прихващане на координатите при натискането на бутон на мишката и проверка (в обратен ред) дали не е
		/// щракнато върху елемент. Ако е така то той се отбелязва като селектиран и започва процес на "влачене".
		/// Промяна на статуса и инвалидиране на контрола, в който визуализираме.
		/// Реализацията се диалогът с потребителя, при който се избира "най-горния" елемент от екрана.
		/// </summary>
		void ViewPortMouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if (pickUpSpeedButton.Checked)
			{
				Shape sel = dialogProcessor.ContainsPoint(e.Location);
				if (sel != null)
				{
					if (dialogProcessor.Selection.Contains(sel))

						dialogProcessor.Selection.Remove(sel);
					else
						dialogProcessor.Selection.Add(sel);
				}

				statusBar.Items[0].Text = "Последно действие: Селекция на примитив";
				dialogProcessor.IsDragging = true;
				dialogProcessor.LastLocation = e.Location;
				viewPort.Invalidate();
			}
		}

		/// <summary>
		/// Прихващане на преместването на мишката.
		/// Ако сме в режм на "влачене", то избрания елемент се транслира.
		/// </summary>
		void ViewPortMouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if (dialogProcessor.IsDragging)
			{
				if (dialogProcessor.Selection != null) statusBar.Items[0].Text = "Последно действие: Влачене";
				dialogProcessor.TranslateTo(e.Location);
				viewPort.Invalidate();
			}
		}

		/// <summary>
		/// Прихващане на отпускането на бутона на мишката.
		/// Излизаме от режим "влачене".
		/// </summary>
		void ViewPortMouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			dialogProcessor.IsDragging = false;
		}

		/// Бутон, който поставя на произволно място елипса със зададените размери.
		/// Променя се лентата със състоянието и се инвалидира контрола, в който визуализираме.
		private void DrawEllipseSpeedButtonClick(object sender, EventArgs e)
		{
			int width;
			int height;

			if (String.IsNullOrEmpty(textBoxWidth.Text) && String.IsNullOrEmpty(textBoxHeight.Text))
			{
				width = 200;
				height = 100;

			}
			else
			{
				width = Int32.Parse(textBoxWidth.Text);
				height = Int32.Parse(textBoxHeight.Text);
			}

			dialogProcessor.AddRandomEllipse(width,height);

			statusBar.Items[0].Text = "Последно действие: Рисуване на елипса";

			viewPort.Invalidate();
		}

		/// Бутон, чрез който можем да сменим цвета на селектираните обекти.
		private void ChangeColorButtonClick(object sender, EventArgs e)
		{
			foreach (Shape item in dialogProcessor.Selection)
				if (colorDialog1.ShowDialog() == DialogResult.OK)
				{
					item.FillColor = colorDialog1.Color;
					viewPort.Invalidate();
				}
		}

		/// Бутон, чрез който можем да сменим цвета на контура на селектираните обекти.
		private void ChangeStrokeColorButtonClick(object sender, EventArgs e)
		{
			foreach (Shape item in dialogProcessor.Selection)
				if (colorDialog1.ShowDialog() == DialogResult.OK)
				{
					item.StrokeColor = colorDialog1.Color;
					viewPort.Invalidate();
				}
		}

		/// Бутон, който поставя на произволно място квадрат със зададените размери.
		/// Променя се лентата със състоянието и се инвалидира контрола, в който визуализираме.
		private void DrawSquareSpeedButtonClick(object sender, EventArgs e)
		{
			int width;
			int height;

			if (String.IsNullOrEmpty(textBoxWidth.Text) && String.IsNullOrEmpty(textBoxHeight.Text))
			{
				width = 125;
				height = 125;

			}
			else
			{
				width = Int32.Parse(textBoxWidth.Text);
				height = Int32.Parse(textBoxHeight.Text);
			}

			dialogProcessor.AddRandomSquare(width, height);

			statusBar.Items[0].Text = "Последно действие: Рисуване на квадрат";

			viewPort.Invalidate();
		}

		/// Бутон, който поставя на зададено място звезда със зададените размери.
		/// Променя се лентата със състоянието и се инвалидира контрола, в който визуализираме.
		private void DrawStarSpeedButtonClick(object sender, EventArgs e)
		{
			dialogProcessor.AddStar();

			statusBar.Items[0].Text = "Последно действие: Рисуване на звезда";

			viewPort.Invalidate();
		}

		/// Бутон, който премахва фигурите, които са селектирани.
		/// Променя се лентата със състоянието и се инвалидира контрола, в който визуализираме.
		private void DeleteShapeSpeedButtonClick(object sender, EventArgs e)
		{
			if (pickUpSpeedButton.Checked)
			{
				foreach (Shape item in dialogProcessor.Selection)
					dialogProcessor.Delete(item);

					statusBar.Items[0].Text = "Последно действие: Премахване на фигура/и";

					viewPort.Invalidate();

				dialogProcessor.Selection.Clear();
			}
		}

		private void saveToolStripMenuItem_Click(object sender, EventArgs e)
		{
			dialogProcessor.Save();

			statusBar.Items[0].Text = "Последно действие: Запазване";

			viewPort.Invalidate();
		}

		private void openToolStripMenuItem_Click(object sender, EventArgs e)
		{
			dialogProcessor.OpenLastSaved();

			statusBar.Items[0].Text = "Последно действие: Отваряне на последно запазен файл";

			viewPort.Invalidate();
		}

		private void ChangeMeasuresSpeedButtonClick(object sender, EventArgs e)
		{
			int width;
			int height;

			if (String.IsNullOrEmpty(textBoxWidth.Text) && String.IsNullOrEmpty(textBoxHeight.Text))
			{
				width = 125;
				height = 125;

			}
			else
			{
				width = Int32.Parse(textBoxWidth.Text);
				height = Int32.Parse(textBoxHeight.Text);
			}

			foreach (Shape item in dialogProcessor.Selection)
			{
				item.Height = height;
				item.Width = width;
			}

			statusBar.Items[0].Text = "Последно действие: Промяна на размерите на фигура/и";

			viewPort.Invalidate();
		}
	}
}
