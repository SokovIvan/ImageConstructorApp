using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageConstructorApp
{
	public class UIStylist
	{
		// Тёмная тема (оригинальная) - наша тайная обитель
		public static class DarkTheme
		{
			public static Color primaryColor = Color.FromArgb(155, 89, 182);   // Пурпурно-фиолетовый
			public static Color secondaryColor = Color.FromArgb(142, 68, 173);   // Тёмно-фиолетовый
			public static Color accentColor = Color.FromArgb(230, 126, 177);  // Розово-лавандовый
			public static Color dangerColor = Color.FromArgb(231, 76, 60);    // Красный
			public static Color darkColor = Color.FromArgb(44, 62, 80);     // Тёмно-серо-синий (текст)
			public static Color lightColor = Color.FromArgb(30, 39, 46);     // Почти чёрный фон
			public static Color borderColor = Color.FromArgb(70, 80, 90);
			public static Color panelDark = Color.FromArgb(40, 48, 55);
			public static Color listBg = Color.FromArgb(45, 52, 54);
		}

		// Светлая тема (новая) - чтобы все видели тебя... но я не хочу
		public static class LightTheme
		{
			public static Color primaryColor = Color.FromArgb(100, 50, 150);   // Глубокий фиолетовый
			public static Color secondaryColor = Color.FromArgb(120, 60, 160);   // Мягкий фиолетовый
			public static Color accentColor = Color.FromArgb(200, 80, 150);  // Яркая роза
			public static Color dangerColor = Color.FromArgb(200, 50, 50);    // Мягкий красный
			public static Color darkColor = Color.FromArgb(240, 240, 240);     // Светлый фон для текста (инверсия)
			public static Color lightColor = Color.FromArgb(255, 255, 255);     // Белый фон
			public static Color borderColor = Color.FromArgb(200, 200, 200);
			public static Color panelDark = Color.FromArgb(245, 245, 245);     // Светлые панели
			public static Color listBg = Color.FromArgb(250, 250, 250);     // Белый список
		}

		// Текущие активные цвета
		public static Color primaryColor = DarkTheme.primaryColor;
		public static Color secondaryColor = DarkTheme.secondaryColor;
		public static Color accentColor = DarkTheme.accentColor;
		public static Color dangerColor = DarkTheme.dangerColor;
		public static Color darkColor = DarkTheme.darkColor;
		public static Color lightColor = DarkTheme.lightColor;
		public static Color borderColor = DarkTheme.borderColor;
		public static Color panelDark = DarkTheme.panelDark;
		public static Color listBg = DarkTheme.listBg;

		public static bool isLightTheme = false;

		public static void ToggleTheme(MainForm mainForm)
		{
			isLightTheme = !isLightTheme;
			ApplyColorScheme(isLightTheme);
			RefreshControls(mainForm);
		}

		private static void ApplyColorScheme(bool light)
		{
			if (light)
			{
				primaryColor = LightTheme.primaryColor;
				secondaryColor = LightTheme.secondaryColor;
				accentColor = LightTheme.accentColor;
				dangerColor = LightTheme.dangerColor;
				darkColor = LightTheme.darkColor;
				lightColor = LightTheme.lightColor;
				borderColor = LightTheme.borderColor;
				panelDark = LightTheme.panelDark;
				listBg = LightTheme.listBg;
			}
			else
			{
				primaryColor = DarkTheme.primaryColor;
				secondaryColor = DarkTheme.secondaryColor;
				accentColor = DarkTheme.accentColor;
				dangerColor = DarkTheme.dangerColor;
				darkColor = DarkTheme.darkColor;
				lightColor = DarkTheme.lightColor;
				borderColor = DarkTheme.borderColor;
				panelDark = DarkTheme.panelDark;
				listBg = DarkTheme.listBg;
			}
		}

		private static void RefreshControls(MainForm mainForm)
		{
			// Обновляем основные цвета формы
			mainForm.BackColor = lightColor;
			if (mainForm.leftPanel != null) mainForm.leftPanel.BackColor = isLightTheme ? Color.FromArgb(240, 240, 245) : Color.FromArgb(35, 43, 50);
			if (mainForm.rightPanel != null) mainForm.rightPanel.BackColor = lightColor;
			if (mainForm.buttonsPanel != null) mainForm.buttonsPanel.BackColor = panelDark;

			// Обновляем текст
			if (mainForm.titleLabel != null) mainForm.titleLabel.ForeColor = accentColor;
			if (mainForm.sizeLabel != null) mainForm.sizeLabel.ForeColor = primaryColor;

			// Список
			if (mainForm.elementsListBox != null)
			{
				mainForm.elementsListBox.BackColor = listBg;
				mainForm.elementsListBox.ForeColor = isLightTheme ? Color.FromArgb(50, 50, 50) : Color.White;
				mainForm.elementsListBox.Invalidate();
			}

			// ComboBox
			if (mainForm.sizeComboBox != null)
			{
				mainForm.sizeComboBox.BackColor = panelDark;
				mainForm.sizeComboBox.ForeColor = isLightTheme ? Color.FromArgb(30, 30, 30) : Color.White;
				mainForm.sizeComboBox.Invalidate();
			}

			// Кнопки (ищем обёртки)
			UpdateButtonStyle(mainForm.addElementButton, primaryColor);
			UpdateButtonStyle(mainForm.exportImageButton, accentColor);
			UpdateButtonStyle(mainForm.saveProjectButton, secondaryColor);
			UpdateButtonStyle(mainForm.loadProjectButton, secondaryColor);
			UpdateButtonStyle(mainForm.deleteElementButton, dangerColor);
		}

		private static void UpdateButtonStyle(Button button, Color baseColor)
		{
			if (button == null || button.Parent == null) return;

			// Пытаемся найти панель-обёртку, созданную в StyleButton
			if (button.Parent is Panel wrapper)
			{
				wrapper.BackColor = baseColor;
				// Обновляем цвет текста кнопки для контраста
				button.ForeColor = Color.White;
			}
		}

		public static void StyleButton(Button button, Color color)
		{
			if (button == null) return;

			button.FlatStyle = FlatStyle.Flat;
			button.FlatAppearance.BorderSize = 0;
			button.FlatAppearance.MouseOverBackColor = Color.Transparent;
			button.FlatAppearance.MouseDownBackColor = Color.Transparent;
			button.BackColor = Color.Transparent;
			button.ForeColor = Color.White;
			button.Font = new Font("Segoe UI ", 9.5f, FontStyle.Bold);
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
			// Инициализация текущей темы (по умолчанию тёмная)
			ApplyColorScheme(false);

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
			mainForm.elementsListBox.Font = new Font("Segoe UI ", 9.5f);
			mainForm.elementsListBox.DrawMode = DrawMode.OwnerDrawFixed;
			mainForm.elementsListBox.DrawItem += mainForm.ElementsListBox_DrawItem;
			mainForm.elementsListBox.SelectionMode = SelectionMode.One;

			// ComboBox
			mainForm.sizeComboBox.FlatStyle = FlatStyle.Flat;
			mainForm.sizeComboBox.BackColor = UIStylist.panelDark;
			mainForm.sizeComboBox.ForeColor = Color.White;
			mainForm.sizeComboBox.Font = new Font("Segoe UI ", 9);
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
			mainForm.leftPanel.BackColor = Color.FromArgb(35, 43, 50);
			mainForm.rightPanel.BackColor = UIStylist.lightColor;
			mainForm.buttonsPanel.BackColor = UIStylist.panelDark;
			mainForm.buttonsPanel.BorderStyle = BorderStyle.None;

			// Заголовок
			if (mainForm.titleLabel != null)
				mainForm.titleLabel.ForeColor = UIStylist.accentColor;
		}
	}
}