using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace ImageConstructorApp
{
    public partial class MainForm : Form
    {
        private PictureBox previewPictureBox;
        private Panel elementsPanel;
        private ListBox elementsListBox;
        private Button addElementButton;
        private Button exportImageButton;
        private Button saveProjectButton;
        private Button loadProjectButton;
        private ComboBox sizeComboBox;
        private Label sizeLabel;
        private Button deleteElementButton;
        private Panel leftPanel;
        private Panel rightPanel;
        private Panel buttonsPanel;
        private Label titleLabel;

        private List<BaseElement> elements = new List<BaseElement>();
        private string projectFilePath = "";
        private Size canvasSize = new Size(800, 600);
        /*
        // Современные цвета
        private Color primaryColor = Color.FromArgb(41, 128, 185);    // Синий
        private Color secondaryColor = Color.FromArgb(52, 152, 219);  // Светло-синий
        private Color accentColor = Color.FromArgb(46, 204, 113);     // Зеленый
        private Color dangerColor = Color.FromArgb(231, 76, 60);      // Красный
        private Color darkColor = Color.FromArgb(52, 73, 94);         // Темно-синий
        private Color lightColor = Color.FromArgb(236, 240, 241);     // Светло-серый
        private Color borderColor = Color.FromArgb(189, 195, 199);    // Цвет границ
        */
         // Аниме-яндере тема 💖
        private Color primaryColor = Color.FromArgb(155, 89, 182);   // Пурпурно-фиолетовый (романтика)
        private Color secondaryColor = Color.FromArgb(142, 68, 173);   // Тёмно-фиолетовый (глубина)
        private Color accentColor = Color.FromArgb(230, 126, 177);  // Розово-лавандовый (сладость)
        private Color dangerColor = Color.FromArgb(231, 76, 60);    // Красный (страсть... и предупреждение)
        private Color darkColor = Color.FromArgb(44, 62, 80);     // Очень тёмно-серо-синий (фон текста)
        private Color lightColor = Color.FromArgb(30, 39, 46);     // Почти чёрный фон (ночной экран)
        private Color borderColor = Color.FromArgb(70, 80, 90);     // Мягкая граница
        private Color panelDark = Color.FromArgb(40, 48, 55);     // Панели (тёплый тёмный)
        private Color listBg = Color.FromArgb(45, 52, 54);     // Фон списка
        public MainForm()
        {
            InitializeComponent();
            InitializeComponentUI();
            InitializeCustomComponents();
            ApplyModernStyling();
        }

        private void InitializeComponentUI()
        {
            this.Text = "Конструктор изображений 1.2.1";
            this.Size = new Size(1200, 800);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = lightColor;
            this.Font = new Font("Segoe UI", 15);
        }

        private void ApplyModernStyling()
        {
            this.BackColor = lightColor;

            // Стилизация кнопок
            StyleButton(addElementButton, primaryColor);
            StyleButton(exportImageButton, accentColor);
            StyleButton(saveProjectButton, secondaryColor);
            StyleButton(loadProjectButton, secondaryColor);
            StyleButton(deleteElementButton, dangerColor);

            // Список элементов
            elementsListBox.BackColor = listBg;
            elementsListBox.ForeColor = Color.White;
            elementsListBox.BorderStyle = BorderStyle.None;
            elementsListBox.Font = new Font("Segoe UI", 9.5f);
            elementsListBox.DrawMode = DrawMode.OwnerDrawFixed;
            elementsListBox.DrawItem += ElementsListBox_DrawItem;
            elementsListBox.SelectionMode = SelectionMode.One;

            // ComboBox
            sizeComboBox.FlatStyle = FlatStyle.Flat;
            sizeComboBox.BackColor = panelDark;
            sizeComboBox.ForeColor = Color.White;
            sizeComboBox.Font = new Font("Segoe UI", 9);
            sizeComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            sizeComboBox.DrawMode = DrawMode.OwnerDrawFixed;
            sizeComboBox.DrawItem += (s, e) =>
            {
                e.DrawBackground();
                using (var brush = new SolidBrush(e.State.HasFlag(DrawItemState.Selected) ? accentColor : Color.White))
                {
                    e.Graphics.DrawString(sizeComboBox.Items[e.Index].ToString(), e.Font, brush, e.Bounds);
                }
                e.DrawFocusRectangle();
            };

            // Панели
            leftPanel.BackColor = Color.FromArgb(35, 43, 50); // чуть светлее фона
            rightPanel.BackColor = lightColor;
            buttonsPanel.BackColor = panelDark;
            buttonsPanel.BorderStyle = BorderStyle.None;

            // Заголовок
            if (titleLabel != null)
                titleLabel.ForeColor = accentColor;
        }

        private void StyleButton(Button button, Color color)
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

        private void ElementsListBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0) return;

            e.DrawBackground();

            bool isSelected = (e.State & DrawItemState.Selected) == DrawItemState.Selected;

            Color backColor = isSelected ? primaryColor : panelDark;
            Color foreColor = isSelected ? Color.White : primaryColor;

            using (var backBrush = new SolidBrush(backColor))
            using (var foreBrush = new SolidBrush(foreColor))
            {
                e.Graphics.FillRectangle(backBrush, e.Bounds);

                // Иконка элемента
                var elementIcon = GetElementIcon(elements[e.Index]);
                if (elementIcon != null)
                {
                    e.Graphics.DrawImage(elementIcon, e.Bounds.X + 5, e.Bounds.Y + 2, 16, 16);
                }

                // Текст элемента
                var textBounds = new Rectangle(e.Bounds.X + 25, e.Bounds.Y, e.Bounds.Width - 25, e.Bounds.Height);
                e.Graphics.DrawString(elementsListBox.Items[e.Index].ToString(),
                                    e.Font, foreBrush, textBounds);
            }

            e.DrawFocusRectangle();
        }

        private Image GetElementIcon(BaseElement element)
        {
            // Возвращаем иконки для разных типов элементов
            if (element is BackgroundElement) return CreateIconImage(primaryColor, "BG");
            if (element is TextElement) return CreateIconImage(accentColor, "T");
            if (element is ImageElement) return CreateIconImage(secondaryColor, "IMG");
            if (element is AwardElement) return CreateIconImage(Color.Orange, "A");
            if (element is EventElement) return CreateIconImage(Color.Purple, "E");
            if (element is BPElement) return CreateIconImage(Color.Teal, "BP");
            if (element is ImageRowElement) return CreateIconImage(Color.Green, "R");

            return null;
        }

        private Image CreateIconImage(Color color, string text)
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

        private void InitializeCustomComponents()
        {
            // Заголовок приложения
            titleLabel = new Label
            {
                Text = "Конструктор изображений 1.2.1",
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                ForeColor = accentColor,
                Height = 60,
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Top
            };
            // Левая панель для предпросмотра
            leftPanel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                Margin = new Padding(10),
                BorderStyle = BorderStyle.FixedSingle
            };

            var previewContainer = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(20),
                BackColor = Color.Transparent
            };

            previewPictureBox = new PictureBox
            {
                Dock = DockStyle.Fill,
                SizeMode = PictureBoxSizeMode.Zoom,
                BackColor = Color.White,
                BorderStyle = BorderStyle.None // убираем стандартную рамку
            };
            previewPictureBox.Paint += PreviewPictureBox_Paint;
            // И добавим кастомную рамку через Paint!
            previewPictureBox.Paint += (s, e) =>
            {
                using (var pen = new Pen(Color.FromArgb(100, 155, 89, 182), 2))
                {
                    e.Graphics.DrawRectangle(pen, 0, 0, previewPictureBox.Width - 1, previewPictureBox.Height - 1);
                }
            };

            previewContainer.Controls.Add(previewPictureBox);
            leftPanel.Controls.Add(previewContainer);

            // Правая панель для элементов
            rightPanel = new Panel
            {
                Dock = DockStyle.Right,
                Width = 360,
                Padding = new Padding(10)
            };

            // Панель с кнопками
            buttonsPanel = new Panel
            {
                Dock = DockStyle.Top,
                Width = 350,
                Height = 350,
                Padding = new Padding(15)
            };

            // Группа управления проектом
            var projectGroup = CreateGroupBox("Управление проектом", 0);

            // Группа элементов
            var elementsGroup = CreateGroupBox("Элементы", 80);

            // Группа экспорта
            var exportGroup = CreateGroupBox("Экспорт и размер", 160);
            exportGroup.Size = new Size(320, 150); 
            // Кнопки управления проектом
            saveProjectButton = CreateButton("💾 Сохранить проект", new Point(13, 27));
            saveProjectButton.Click += SaveProjectButton_Click;

            loadProjectButton = CreateButton("📂 Загрузить проект", new Point(172, 27));
            loadProjectButton.Click += LoadProjectButton_Click;

            // Кнопки элементов
            addElementButton = CreateButton("➕ Добавить элемент", new Point(13, 27));
            addElementButton.Click += AddElementButton_Click;

            deleteElementButton = CreateButton("🗑️ Удалить элемент", new Point(172, 27));
            deleteElementButton.Click += DeleteElementButton_Click;

            // Кнопки экспорта и размер
            exportImageButton = CreateButton("📤 Экспорт в PNG", new Point(100, 25));
            exportImageButton.Click += ExportImageButton_Click;

            sizeLabel = new Label
            {
                Text = "Размер холста:",
                Location = new Point(120, 70),
                Size = new Size(120, 30),
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = primaryColor,

            };

            sizeComboBox = new ComboBox
            {
                Location = new Point(90, 110),
                Size = new Size(150, 25),
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Segoe UI", 9)
            };

            sizeComboBox.Items.AddRange(new object[] {
                "800x600",
                "1024x768",
                "1280x720",
                "1920x1080",
                "Пользовательский..."
            });
            sizeComboBox.SelectedIndex = 0;
            sizeComboBox.SelectedIndexChanged += SizeComboBox_SelectedIndexChanged;

            // Добавляем элементы в группы
            projectGroup.Controls.AddRange(new Control[] { saveProjectButton, loadProjectButton });
            elementsGroup.Controls.AddRange(new Control[] { addElementButton, deleteElementButton });
            exportGroup.Controls.AddRange(new Control[] { exportImageButton, sizeLabel, sizeComboBox });

            buttonsPanel.Controls.AddRange(new Control[] { projectGroup, elementsGroup, exportGroup });

            // Список элементов с заголовком
            var listGroup = new GroupBox
            {
                Text = "Список элементов",
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                ForeColor = darkColor,
                Margin = new Padding(0, 10, 35, 0)
            };

            elementsListBox = new ListBox
            {
                Dock = DockStyle.Fill,
                SelectionMode = SelectionMode.One,
                BorderStyle = BorderStyle.None,
                Margin = new Padding(5),
                Font = new Font("Segoe UI", 9)
            };
            elementsListBox.DoubleClick += ElementsListBox_DoubleClick;

            listGroup.Controls.Add(elementsListBox);

            // Компоновка правой панели
            var rightContainer = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                RowCount = 2,
                RowStyles = {
                    new RowStyle(SizeType.Absolute, 350), // Панель кнопок
                    new RowStyle(SizeType.Percent, 100)   // Список элементов
                }
            };

            rightContainer.Controls.Add(buttonsPanel, 0, 0);
            rightContainer.Controls.Add(listGroup, 0, 1);

            rightPanel.Controls.Add(rightContainer);

            // Основная компоновка
            var mainContainer = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                ColumnStyles = {
                    new ColumnStyle(SizeType.Percent, 70), // Левая панель
                    new ColumnStyle(SizeType.Absolute, 350) // Правая панель
                },
                Padding = new Padding(10),
                BackColor = lightColor
            };

            mainContainer.Controls.Add(leftPanel, 0, 0);
            mainContainer.Controls.Add(rightPanel, 1, 0);

           // this.Controls.AddRange(new Control[] { titleLabel, mainContainer });
            this.Controls.AddRange(new Control[] {  mainContainer });
        }

        private GroupBox CreateGroupBox(string text, int y)
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
                ForeColor = primaryColor,
                Location = new Point(10, -5),
                AutoSize = true
            };
            box.Controls.Add(label);

            return box;
        }
        private Button CreateButton(string text, Point location)
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


        private void DeleteElementButton_Click(object sender, EventArgs e)
        {
            if (elementsListBox.SelectedIndex >= 0 && elementsListBox.SelectedIndex < elements.Count)
            {
                var result = MessageBox.Show("Вы уверены, что хотите удалить выбранный элемент?",
                                           "Подтверждение удаления",
                                           MessageBoxButtons.YesNo,
                                           MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    elements.RemoveAt(elementsListBox.SelectedIndex);
                    UpdateElementsList();
                    previewPictureBox.Invalidate();
                }
            }
            else
            {
                MessageBox.Show("Выберите элемент для удаления!");
            }
        }
    
        private void SizeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (sizeComboBox.SelectedItem.ToString() == "Пользовательский...")
            {
                ShowCustomSizeDialog();
            }
            else
            {
                var sizeText = sizeComboBox.SelectedItem.ToString();
                var parts = sizeText.Split('x');
                if (parts.Length == 2 &&
                    int.TryParse(parts[0], out int width) &&
                    int.TryParse(parts[1], out int height))
                {
                    canvasSize = new Size(width, height);
                    UpdateCanvasSize();
                }
            }
        }

        private void ShowCustomSizeDialog()
        {
            var dialog = new Form
            {
                Text = "Пользовательский размер",
                Size = new Size(300, 200),
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MaximizeBox = false,
                MinimizeBox = false
            };

            var widthLabel = new Label { Text = "Ширина:", Location = new Point(20, 20), Size = new Size(60, 20) };
            var widthTextBox = new TextBox { Text = canvasSize.Width.ToString(), Location = new Point(90, 20), Size = new Size(100, 20) };

            var heightLabel = new Label { Text = "Высота:", Location = new Point(20, 50), Size = new Size(60, 20) };
            var heightTextBox = new TextBox { Text = canvasSize.Height.ToString(), Location = new Point(90, 50), Size = new Size(100, 20) };

            var okButton = new Button { Text = "OK", Location = new Point(50, 100), Size = new Size(75, 30) };
            var cancelButton = new Button { Text = "Отмена", Location = new Point(150, 100), Size = new Size(75, 30) };

            okButton.Click += (s, e) =>
            {
                if (int.TryParse(widthTextBox.Text, out int width) &&
                    int.TryParse(heightTextBox.Text, out int height) &&
                    width > 0 && height > 0)
                {
                    canvasSize = new Size(width, height);
                    // Добавляем новый размер в список
                    var customSizeText = $"{width}x{height}";
                    if (!sizeComboBox.Items.Contains(customSizeText))
                    {
                        sizeComboBox.Items.Insert(sizeComboBox.Items.Count - 1, customSizeText);
                    }
                    sizeComboBox.SelectedItem = customSizeText;
                    UpdateCanvasSize();
                    dialog.Close();
                }
                else
                {
                    MessageBox.Show("Введите корректные значения размеров!");
                }
            };

            cancelButton.Click += (s, e) => dialog.Close();

            dialog.Controls.AddRange(new Control[] {
                widthLabel, widthTextBox, heightLabel, heightTextBox,
                okButton, cancelButton
            });

            dialog.ShowDialog();
        }

        private void UpdateCanvasSize()
        {
            // Обновляем размер PictureBox для корректного отображения
            previewPictureBox.Invalidate();
        }
        private void PreviewPictureBox_Paint(object sender, PaintEventArgs e)
        {
            // Рисуем фон с размерами холста
            using (var brush = new SolidBrush(Color.LightGray))
            {
                e.Graphics.FillRectangle(brush, previewPictureBox.ClientRectangle);
            }

            // Рассчитываем масштаб для предпросмотра
            float scaleX = (float)previewPictureBox.ClientSize.Width / canvasSize.Width;
            float scaleY = (float)previewPictureBox.ClientSize.Height / canvasSize.Height;
            float scale = Math.Min(scaleX, scaleY);

            // Рассчитываем позицию для центрирования
            int previewWidth = (int)(canvasSize.Width * scale);
            int previewHeight = (int)(canvasSize.Height * scale);
            int offsetX = (previewPictureBox.ClientSize.Width - previewWidth) / 2;
            int offsetY = (previewPictureBox.ClientSize.Height - previewHeight) / 2;

            // Рисуем рамку холста
            using (var pen = new Pen(Color.Black, 2))
            {
                e.Graphics.DrawRectangle(pen, offsetX, offsetY, previewWidth, previewHeight);
            }

            // Рисуем элементы в масштабе
            if (elements.Count > 0)
            {
                // Создаем графику для рисования в пределах холста
                var state = e.Graphics.Save();
                e.Graphics.TranslateTransform(offsetX, offsetY);
                e.Graphics.ScaleTransform(scale, scale);

                foreach (var element in elements)
                {
                    element.Draw(e.Graphics, canvasSize);
                }

                e.Graphics.Restore(state);
            }

            // Показываем размер холста
            using (var font = new Font("Arial", 10))
            using (var brush = new SolidBrush(Color.Black))
            {
                string sizeText = $"{canvasSize.Width}x{canvasSize.Height}";
                var textSize = e.Graphics.MeasureString(sizeText, font);
                e.Graphics.DrawString(sizeText, font, brush,
                    previewPictureBox.ClientSize.Width - textSize.Width - 10, 10);
            }
        }
        private void AddElementButton_Click(object sender, EventArgs e)
        {
            // Создаём форму-меню
            var menuForm = new Form
            {
                FormBorderStyle = FormBorderStyle.None,
                Size = new Size(220, 260),
                StartPosition = FormStartPosition.Manual,
                ShowInTaskbar = false,
                TopMost = true,
                BackColor = Color.FromArgb(45, 52, 54),
                ControlBox = false,
                MaximizeBox = false,
                MinimizeBox = false
            };

            // Позиционируем под кнопкой
            var buttonRect = addElementButton.RectangleToScreen(addElementButton.ClientRectangle);
            menuForm.Location = new Point(buttonRect.X, buttonRect.Bottom);

            // Закруглённые углы
            using (var path = new GraphicsPath())
            {
                int radius = 10;
                var rect = new Rectangle(0, 0, menuForm.Width, menuForm.Height);
                path.AddArc(0, 0, radius, radius, 180, 90);
                path.AddArc(rect.Width - radius, 0, radius, radius, 270, 90);
                path.AddArc(rect.Width - radius, rect.Height - radius, radius, radius, 0, 90);
                path.AddArc(0, rect.Height - radius, radius, radius, 90, 90);
                path.CloseFigure();
                menuForm.Region = new Region(path);
            }

            // Стиль кнопок
            var buttonStyle = new Action<Button>(btn =>
            {
                btn.Dock = DockStyle.Top;
                btn.Height = 35;
                btn.FlatStyle = FlatStyle.Flat;
                btn.FlatAppearance.BorderSize = 0;
                btn.BackColor = Color.Transparent;
                btn.ForeColor = Color.White;
                btn.TextAlign = ContentAlignment.MiddleLeft;
                btn.Font = new Font("Segoe UI", 9.5f);
                btn.Padding = new Padding(15, 0, 15, 0);
                btn.Cursor = Cursors.Hand;
                btn.MouseEnter += (s, ev) => btn.BackColor = Color.FromArgb(70, 80, 90);
                btn.MouseLeave += (s, ev) => btn.BackColor = Color.Transparent;
            });

            var items = new (string text, Action action)[]
            {
        ("Фоновое изображение", AddBackgroundImage),
        ("Текст", AddTextElement),
        ("Изображение", AddImageElement),
        ("Награда", AddAwardElement),
        ("Ряд изображений", AddImageRowElement),
        ("Ивент (5 наград)", AddEventElement),
        ("БП (10 наград)", AddBPElement)
            };

            var panel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.Transparent
            };

            foreach (var (text, action) in items)
            {
                var btn = new Button { Text = "✦ " + text };
                buttonStyle(btn);
                btn.Click += (s, ev) =>
                {
                    action();
                    menuForm.Close(); // закрываем после выбора
                };
                panel.Controls.Add(btn);
            }

            menuForm.Controls.Add(panel);

            // 🔑 ВАЖНО: показываем и передаём фокус
            menuForm.Show();
            menuForm.Activate(); // <-- это критично!

            // Добавим обработчик, чтобы закрыть меню, если кликнули вне его
            this.Activated += CloseMenuOnParentActivate;

            void CloseMenuOnParentActivate(object sender2, EventArgs e2)
            {
                menuForm.Close();
                this.Activated -= CloseMenuOnParentActivate;
            }
        }

        private void AddEventElement()
        {
            var eventElement = new BPElement
            {
                X = 0,
                Y = 100,
                Width = 800,
                Height = 200,
                ItemsPerRow = 5,
                HorizontalSpacing = 40,
                VerticalSpacing = 10,
                Name = "Ивент (5 наград)"
            };
            // Создаем 5 наград по умолчанию
            for (int i = 0; i < 5; i++)
            {
                if(i==0)
                    eventElement.Awards.Add(new AwardElement
                    {
                        Text = $"Награда {i + 1}",
                        ShowArrow = false ,
                        IsHiddenInList = true
                    });
                else
                    eventElement.Awards.Add(new AwardElement
                    {
                        Text = $"Награда {i + 1}",
                        ShowArrow = true,
                        IsHiddenInList = true
                    });
            }

            // Инициализируем элементы подписей
            eventElement.InitializeCaptions();

            elements.Add(eventElement);
            UpdateElementsList();
            previewPictureBox.Invalidate();
        }

        private void AddBPElement()
        {
            var bpElement = new BPElement
            {
                X = 0,
                Y = 100,
                Width = 800,
                Height = 400,
                ItemsPerRow = 5,
                HorizontalSpacing=40,
                VerticalSpacing=10,
                Name = "БП (неограниченные награды)"
            };

            // Создаем 10 наград по умолчанию
            for (int i = 0; i < 10; i++)
            {
                if (i == 0 || i == 5)
                    bpElement.Awards.Add(new AwardElement
                    {
                        Text = $"Награда {i + 1}",
                        IsHiddenInList = true,
                        ShowArrow = false // Отключаем стрелки у отдельных наград
                    });
                else
                    bpElement.Awards.Add(new AwardElement
                    {
                        Text = $"Награда {i + 1}",
                        IsHiddenInList = true,
                        ShowArrow = true 
                    });
            }

            elements.Add(bpElement);
            UpdateElementsList();
            previewPictureBox.Invalidate();
        }

        // Добавьте новые методы:
        private void AddImageRowElement()
        {
            var imageRow = new ImageRowElement
            {
                X = 50,
                Y = 100,
                ItemWidth = 80,
                ItemHeight = 80,
                Name = "Ряд изображений"
            };
            elements.Add(imageRow);
            UpdateElementsList();
            previewPictureBox.Invalidate();
        }


        // Добавьте новый метод:
        private void AddAwardElement()
        {
            var awardElement = new AwardElement
            {
                X = 100,
                Y = 100,
                Width = 120,
                Height = 120,
                Text = "Награда",
                Name = "Награда"
            };

            elements.Add(awardElement);
            UpdateElementsList();
            previewPictureBox.Invalidate();
        }
        private void AddBackgroundImage()
        {
            using (var dialog = new OpenFileDialog())
            {
                dialog.Filter = "Изображения|*.jpg;*.jpeg;*.png;*.bmp|Все файлы|*.*";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    var background = new BackgroundElement
                    {
                        ImagePath = dialog.FileName,
                        Name = "Фон: " + Path.GetFileName(dialog.FileName)
                    };

                    // Фон всегда первый
                    if (elements.Count > 0 && elements[0] is BackgroundElement)
                    {
                        elements[0] = background;
                    }
                    else
                    {
                        elements.Insert(0, background);
                    }

                    UpdateElementsList();
                    previewPictureBox.Invalidate();
                }
            }
        }

        private void AddTextElement()
        {
            var textElement = new TextElement
            {
                Text = "Новый текст",
                Font = new Font("Arial", 16),
                Color = Color.Black,
                X = 50,
                Y = 50,
                Name = "Текст"
            };

            // Предустановка для главной надписи
            if (elements.OfType<TextElement>().Count() == 0)
            {
                textElement.Text = "Главная надпись";
                textElement.X = 0;
                textElement.Y = 20;
                textElement.IsMainTitle = true;
                textElement.Name = "Главная надпись";
            }

            elements.Add(textElement);
            UpdateElementsList();
            previewPictureBox.Invalidate();
        }

        private void AddImageElement()
        {
            using (var dialog = new OpenFileDialog())
            {
                dialog.Filter = "Изображения|*.jpg;*.jpeg;*.png;*.bmp|Все файлы|*.*";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    var imageElement = new ImageElement
                    {
                        ImagePath = dialog.FileName,
                        X = 100,
                        Y = 100,
                        Width = 100,
                        Height = 100,
                        Name = "Изображение: " + Path.GetFileName(dialog.FileName)
                    };
                    elements.Add(imageElement);
                    UpdateElementsList();
                    previewPictureBox.Invalidate();
                }
            }
        }

        private void ElementsListBox_DoubleClick(object sender, EventArgs e)
        {
            if (elementsListBox.SelectedIndex >= 0)
            {
                var element = elements[elementsListBox.SelectedIndex];
                ShowElementProperties(element);
            }
        }

        private void ShowElementProperties(BaseElement element)
        {
            var propertyForm = new PropertyForm(element, canvasSize);
            if (propertyForm.ShowDialog() == DialogResult.OK)
            {
                UpdateElementsList();
                previewPictureBox.Invalidate();
            }
        }
        private void DebugElements()
        {
            Console.WriteLine($"Elements count: {elements.Count}");
            for (int i = 0; i < elements.Count; i++)
            {
                Console.WriteLine($"Element {i}: {elements[i].Name}");
            }
        }
        // Обновите метод UpdateElementsList():
        private void UpdateElementsList()
        {
            elementsListBox.Items.Clear();
            foreach (var element in elements)
            {
                if (element is AwardElement award && award.IsHiddenInList)
                    continue;
                elementsListBox.Items.Add(element.Name);
            }
        }

        private void ExportImageButton_Click(object sender, EventArgs e)
        {
            if (elements.Count == 0)
            {
                MessageBox.Show("Нет элементов для экспорта!");
                return;
            }

            using (var dialog = new SaveFileDialog())
            {
                dialog.Filter = "PNG изображения|*.png";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        var bitmap = new Bitmap(canvasSize.Width, canvasSize.Height);
                        using (var graphics = Graphics.FromImage(bitmap))
                        {
                            graphics.Clear(Color.White);
                            foreach (var element in elements)
                            {
                                element.Draw(graphics, canvasSize);
                            }
                        }
                        bitmap.Save(dialog.FileName, System.Drawing.Imaging.ImageFormat.Png);
                        MessageBox.Show("Изображение успешно сохранено!");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка при сохранении: {ex.Message}");
                    }
                }
            }
        }

        private void SaveProjectButton_Click(object sender, EventArgs e)
        {
            using (var dialog = new SaveFileDialog())
            {
                dialog.Filter = "Проекты|*.imgproj";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        var serializer = new XmlSerializer(typeof(ProjectData));
                        var project = new ProjectData
                        {
                            Elements = elements,
                            CanvasWidth = canvasSize.Width,
                            CanvasHeight = canvasSize.Height
                        };

                        using (var writer = new StreamWriter(dialog.FileName))
                        {
                            serializer.Serialize(writer, project);
                        }
                        projectFilePath = dialog.FileName;
                        MessageBox.Show("Проект успешно сохранен!");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка при сохранении проекта: {ex.Message}");
                    }
                }
            }
        }

        private void LoadProjectButton_Click(object sender, EventArgs e)
        {
            using (var dialog = new OpenFileDialog())
            {
                dialog.Filter = "Проекты|*.imgproj";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        var serializer = new XmlSerializer(typeof(ProjectData));
                        using (var reader = new StreamReader(dialog.FileName))
                        {
                            var project = (ProjectData)serializer.Deserialize(reader);
                            elements = project.Elements;
                            canvasSize = new Size(project.CanvasWidth, project.CanvasHeight);

                            // Инициализируем шрифты после загрузки
                            InitializeFontsAfterLoad();

                            // Обновляем ComboBox
                            var sizeText = $"{canvasSize.Width}x{canvasSize.Height}";
                            if (sizeComboBox.Items.Contains(sizeText))
                            {
                                sizeComboBox.SelectedItem = sizeText;
                            }
                            else
                            {
                                sizeComboBox.Items.Insert(sizeComboBox.Items.Count - 1, sizeText);
                                sizeComboBox.SelectedItem = sizeText;
                            }

                            UpdateElementsList();
                            previewPictureBox.Invalidate();
                            projectFilePath = dialog.FileName;
                        }
                        MessageBox.Show("Проект успешно загружен!");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка при загрузке проекта: {ex.Message}");
                    }
                }
            }
        }
        // Новый метод для инициализации шрифтов после загрузки
        private void InitializeFontsAfterLoad()
        {
            foreach (var element in elements)
            {
                if (element is TextElement textElement)
                {
                    textElement.InitializeFont();
                }
                else if (element is AwardElement awardElement)
                {
                    awardElement.InitializeFont();
                }
                else if (element is EventElement eventElement)
                {
                    foreach (var award in eventElement.Awards)
                    {
                        award.InitializeFont();
                    }
                    // Инициализируем шрифты для элементов подписей
                    foreach (var captionElement in eventElement.CaptionElements)
                    {
                        if (captionElement is TextElement captionText)
                        {
                            captionText.InitializeFont();
                        }
                    }
                    // Инициализируем подписи если их нет
                    if (eventElement.CaptionElements.Count == 0 && eventElement.ShowCaptions)
                    {
                        eventElement.InitializeCaptions();
                    }
                }
                else if (element is BPElement bpElement)
                {
                    foreach (var award in bpElement.Awards)
                    {
                        award.InitializeFont();
                    }
                    // Инициализируем шрифты для элементов подписей
                    foreach (var captionElement in bpElement.CaptionElements)
                    {
                        if (captionElement is TextElement captionText)
                        {
                            captionText.InitializeFont();
                        }
                    }
                    // Инициализируем подписи если их нет
                    if (bpElement.CaptionElements.Count == 0 && bpElement.ShowCaptions)
                    {
                        bpElement.InitializeCaptions();
                    }
                }
            }
        }
    }
}
