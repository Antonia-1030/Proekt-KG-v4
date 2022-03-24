using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

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
			SaveFileDialog saveFileDialog1 = new SaveFileDialog();
			saveFileDialog1.Title = "Save an Image File";
			saveFileDialog1.Filter = "JPeg Image|*.jpg|Bitmap Image|*.bmp|PNG Image|*.png";

			if (saveFileDialog1.ShowDialog() == DialogResult.OK)
			{
				int width = Convert.ToInt32(viewPort.Width);
				int height = Convert.ToInt32(viewPort.Height);
				Bitmap bmp = new Bitmap(width, height);
				viewPort.DrawToBitmap(bmp, new Rectangle(0, 0, width, height));
				bmp.Save(saveFileDialog1.FileName, ImageFormat.Jpeg);
			}
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
			dialogProcessor.AddRandomRectangle();
			
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
			if (pickUpSpeedButton.Checked) {
				Shape sel = dialogProcessor.ContainsPoint(e.Location);
				
				if(sel != null)
                {
                    if (dialogProcessor.Selection.Contains(sel))
						dialogProcessor.Selection.Remove(sel);

                    else
                     {
							dialogProcessor.Selection.Add(sel);
                     }
                    
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
			if (dialogProcessor.IsDragging) {
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

        private void SaveButton2(object sender, EventArgs e)
        {

			SaveFileDialog saveFileDialog1 = new SaveFileDialog();
			saveFileDialog1.Filter = "JPeg Image|*.jpg|Bitmap Image|*.bmp|PNG Image|*.png";

			if (saveFileDialog1.ShowDialog() == DialogResult.OK)
			{
				int width = Convert.ToInt32(viewPort.Width);
				int height = Convert.ToInt32(viewPort.Height);
				Bitmap bmp = new Bitmap(width, height);
				viewPort.DrawToBitmap(bmp, new Rectangle(0, 0, width, height));

				bmp.Save(saveFileDialog1.FileName, ImageFormat.Jpeg);
			}

		}

		private void DrawEllipseButtonClick(object sender, EventArgs e)
        {
			dialogProcessor.AddRandomEllipse();

			statusBar.Items[0].Text = "Последно действие: Рисуване на елипса";

			viewPort.Invalidate();
		}

        private void DrawLineButtonClick(object sender, EventArgs e)
        {
			dialogProcessor.AddRandomLine();

			statusBar.Items[0].Text = "Последно действие: Рисуване на линия";

			viewPort.Invalidate();
		}

		private void ColorToolButtonClick(object sender, EventArgs e)
		{
			if (colorDialog1.ShowDialog() == DialogResult.OK)
			{
				foreach (Shape item in dialogProcessor.ShapeList)
					item.FillColor = colorDialog1.Color;

				statusBar.Items[0].Text = "Последно действие: Смяна цвят";
				viewPort.Invalidate();
			}
		}

        private void toolStripButton6_Click(object sender, EventArgs e)
        {

        }

        private void viewPort_Load(object sender, EventArgs e)
        {

        }

        private void openFromToolStripMenuItem_Click(object sender, EventArgs e)
        {
			openFileDialog1 = new OpenFileDialog();

		}

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
			SaveFileDialog saveFileDialog1 = new SaveFileDialog();
			saveFileDialog1.Filter = "JPeg Image|*.jpg|Bitmap Image|*.bmp|PNG Image|*.png";
			if (saveFileDialog1.ShowDialog() == DialogResult.OK)
			{
				int width = Convert.ToInt32(viewPort.Width);
				int height = Convert.ToInt32(viewPort.Height);
				Bitmap bmp = new Bitmap(width, height);
				viewPort.DrawToBitmap(bmp, new Rectangle(0, 0, width, height));
				bmp.Save(saveFileDialog1.FileName, ImageFormat.Jpeg);
			}
		}

        private void toolStripButton6_Click_1(object sender, EventArgs e)
        {
			if (colorDialog2.ShowDialog() == DialogResult.OK)
			{
				foreach (Shape item in dialogProcessor.ShapeList)
					item.StrokeColor = colorDialog2.Color;

				viewPort.Invalidate();
			}
		}
    }
}
