using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageConstructorApp
{
	internal class ElementsManager
	{
		private static Image CreateIconImage(Color color, string text)
		{
			var bmp = new Bitmap(16, 16);
			using (var g = Graphics.FromImage(bmp))
			{
				g.SmoothingMode = SmoothingMode.AntiAlias;

				// Фон иконки
				using (var brush = new SolidBrush(color))
				{
					g.FillEllipse(brush, 0, 0, 15, 15);
				}

				// Текст иконки
				using (var font = new Font("Arial", 6, FontStyle.Bold))
				using (var brush = new SolidBrush(Color.White))
				{
					var size = g.MeasureString(text, font);
					g.DrawString(text, font, brush,
								(16 - size.Width) / 2,
								(16 - size.Height) / 2);
				}
			}
			return bmp;
		}
		public static Image GetElementIcon(BaseElement element)
		{
			// Возвращаем иконки для разных типов элементов
			if (element is BackgroundElement) return ElementsManager.CreateIconImage(UIStylist.primaryColor, "BG");
			if (element is TextElement) return ElementsManager.CreateIconImage(UIStylist.accentColor, "T");
			if (element is ImageElement) return ElementsManager.CreateIconImage(UIStylist.secondaryColor, "IMG");
			if (element is AwardElement) return ElementsManager.CreateIconImage(Color.Orange, "A");
			if (element is EventElement) return ElementsManager.CreateIconImage(Color.Purple, "E");
			if (element is BPElement) return ElementsManager.CreateIconImage(Color.Teal, "BP");
			if (element is ImageRowElement) return ElementsManager.CreateIconImage(Color.Green, "R");

			return null;
		}
		public static GroupBox CreateGroupBox(string text, int y)
		{
			var box = new GroupBox
			{
				Text = "",
				Location = new Point(0, y),
				Size = new Size(320, 70),
				BackColor = Color.Transparent
			};

			// Добавляем красивую надпись поверх
			var label = new Label
			{
				Text = "✦ " + text,
				Font = new Font("Segoe UI", 9.5f, FontStyle.Bold),
				ForeColor = UIStylist.primaryColor,
				Location = new Point(10, -5),
				AutoSize = true
			};
			box.Controls.Add(label);

			return box;
		}
		public static Button CreateButton(string text, Point location)
		{
			return new Button
			{
				Text = text,
				Location = location,
				Size = new Size(135, 30),
				Font = new Font("Segoe UI", 9),
				TextAlign = ContentAlignment.MiddleLeft,
				Cursor = Cursors.Hand
			};
		}
	}
}
