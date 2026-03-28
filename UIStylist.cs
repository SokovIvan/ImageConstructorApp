using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageConstructorApp
{

	public class UIStylist
	{
		public static Color primaryColor = Color.FromArgb(155, 89, 182);   // Пурпурно-фиолетовый (романтика)
		public static Color secondaryColor = Color.FromArgb(142, 68, 173);   // Тёмно-фиолетовый (глубина)
		public static Color accentColor = Color.FromArgb(230, 126, 177);  // Розово-лавандовый (сладость)
		public static Color dangerColor = Color.FromArgb(231, 76, 60);    // Красный (страсть... и предупреждение)
		public static Color darkColor = Color.FromArgb(44, 62, 80);     // Очень тёмно-серо-синий (фон текста)
		public static Color lightColor = Color.FromArgb(30, 39, 46);     // Почти чёрный фон (ночной экран)
		public static Color borderColor = Color.FromArgb(70, 80, 90);     // Мягкая граница
		public static Color panelDark = Color.FromArgb(40, 48, 55);     // Панели (тёплый тёмный)
		public static Color listBg = Color.FromArgb(45, 52, 54);     // Фон списка
		public static void StyleButton(Button button, Color color)
		{
			if (button == null) return;

			button.FlatStyle = FlatStyle.Flat;
			button.FlatAppearance.BorderSize = 0;
			button.FlatAppearance.MouseOverBackColor = Color.Transparent;
			button.FlatAppearance.MouseDownBackColor = Color.Transparent;
			button.BackColor = Color.Transparent;
			button.ForeColor = Color.White;
			button.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
			button.Cursor = Cursors.Hand;
			button.Padding = new Padding(8, 4, 8, 4);
			button.FlatAppearance.BorderColor = Color.Red;

			// Создаём панель-обёртку для красивого фона
			var wrapper = new Panel
			{
				Size = new Size(button.Width + 16, button.Height + 8),
				Location = new Point(button.Location.X - 8, button.Location.Y - 4),
				BackColor = color,
				Parent = button.Parent
			};
			using (var path = new GraphicsPath())
			{
				var radius = 15;
				var rect = new Rectangle(0, 0, wrapper.Width, wrapper.Height);
				path.AddArc(rect.X, rect.Y, radius, radius, 180, 90);
				path.AddArc(rect.Right - radius, rect.Y, radius, radius, 270, 90);
				path.AddArc(rect.Right - radius, rect.Bottom - radius, radius, radius, 0, 90);
				path.AddArc(rect.X, rect.Bottom - radius, radius, radius, 90, 90);
				path.CloseFigure();
				wrapper.Region = new Region(path);
			}
			button.Parent = wrapper;
			button.Dock = DockStyle.Fill;
			button.BringToFront();

			// Эффекты наведения на обёртку
			wrapper.MouseEnter += (s, e) => wrapper.BackColor = ControlPaint.Light(color, 0.2f);
			wrapper.MouseLeave += (s, e) => wrapper.BackColor = color;
		}
		public static void ApplyModernStyling(MainForm mainForm)
		{
			mainForm.BackColor = UIStylist.lightColor;

			// Стилизация кнопок
			UIStylist.StyleButton(mainForm.addElementButton, UIStylist.primaryColor);
			UIStylist.StyleButton(mainForm.exportImageButton, UIStylist.accentColor);
			UIStylist.StyleButton(mainForm.saveProjectButton, UIStylist.secondaryColor);
			UIStylist.StyleButton(mainForm.loadProjectButton, UIStylist.secondaryColor);
			UIStylist.StyleButton(mainForm.deleteElementButton, UIStylist.dangerColor);

			// Список элементов
			mainForm.elementsListBox.BackColor = UIStylist.listBg;
			mainForm.elementsListBox.ForeColor = Color.White;
			mainForm.elementsListBox.BorderStyle = BorderStyle.None;
			mainForm.elementsListBox.Font = new Font("Segoe UI", 9.5f);
			mainForm.elementsListBox.DrawMode = DrawMode.OwnerDrawFixed;
			mainForm.elementsListBox.DrawItem += mainForm.ElementsListBox_DrawItem;
			mainForm.elementsListBox.SelectionMode = SelectionMode.One;

			// ComboBox
			mainForm.sizeComboBox.FlatStyle = FlatStyle.Flat;
			mainForm.sizeComboBox.BackColor = UIStylist.panelDark;
			mainForm.sizeComboBox.ForeColor = Color.White;
			mainForm.sizeComboBox.Font = new Font("Segoe UI", 9);
			mainForm.sizeComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
			mainForm.sizeComboBox.DrawMode = DrawMode.OwnerDrawFixed;
			mainForm.sizeComboBox.DrawItem += (s, e) =>
			{
				e.DrawBackground();
				using (var brush = new SolidBrush(e.State.HasFlag(DrawItemState.Selected) ? UIStylist.accentColor : Color.White))
				{
					e.Graphics.DrawString(mainForm.sizeComboBox.Items[e.Index].ToString(), e.Font, brush, e.Bounds);
				}
				e.DrawFocusRectangle();
			};

			// Панели
			mainForm.leftPanel.BackColor = Color.FromArgb(35, 43, 50); // чуть светлее фона
			mainForm.rightPanel.BackColor = UIStylist.lightColor;
			mainForm.buttonsPanel.BackColor = UIStylist.panelDark;
			mainForm.buttonsPanel.BorderStyle = BorderStyle.None;

			// Заголовок
			if (mainForm.titleLabel != null)
				mainForm.titleLabel.ForeColor = UIStylist.accentColor;
		}
	}
}
