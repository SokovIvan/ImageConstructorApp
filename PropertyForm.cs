using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace ImageConstructorApp
{
    public partial class PropertyForm : Form
    {
        private BaseElement element;
        private Size canvasSize;
        private TextBox nameTextBox;
        private NumericUpDown xNumericUpDown;
        private NumericUpDown yNumericUpDown;
        private NumericUpDown widthNumericUpDown;
        private NumericUpDown heightNumericUpDown;
        private Button okButton;
        private Button cancelButton;
        private Panel mainPanel;
        private Panel scrollPanel;

        public PropertyForm(BaseElement element, Size canvasSize)
        {
            this.element = element;
            this.canvasSize = canvasSize;
            InitializeComponent();
            InitializeComponentUI();
            LoadElementProperties();
        }

        private void InitializeComponentUI()
        {
            this.Text = "Свойства элемента";
            this.Size = new Size(600, 800);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            // Панель для кнопок OK/Cancel
            var buttonPanel = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 50,
                BackColor = SystemColors.Control
            };

            okButton = new Button { Text = "OK", Location = new Point(150, 10), Size = new Size(75, 30) };
            okButton.Click += OkButton_Click;

            cancelButton = new Button { Text = "Отмена", Location = new Point(250, 10), Size = new Size(75, 30) };
            cancelButton.Click += CancelButton_Click;

            buttonPanel.Controls.AddRange(new Control[] { okButton, cancelButton });

            // Основная панель с прокруткой
            mainPanel = new Panel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true
            };

            // Панель для содержимого
            scrollPanel = new Panel
            {
                Size = new Size(450, 800), // Увеличенный размер
                Location = new Point(0, 0)
            };

            mainPanel.Controls.Add(scrollPanel);
            this.Controls.Add(mainPanel);
            this.Controls.Add(buttonPanel);

            // Добавляем базовые элементы управления
            AddBasicControls();
            AddSpecificControls();
        }

        private void AddBasicControls()
        {
            var nameLabel = new Label { Text = "Название:", Location = new Point(20, 20), Size = new Size(100, 20) };
            nameTextBox = new TextBox { Location = new Point(130, 20), Size = new Size(200, 20) };

            var yLabel = new Label { Text = "Y:", Location = new Point(200, 60), Size = new Size(30, 20) };
            yNumericUpDown = new NumericUpDown { Location = new Point(230, 60), Size = new Size(100, 20), Minimum = 0, Maximum = 10000 };

            var xLabel = new Label { Text = "X:", Location = new Point(20, 60), Size = new Size(30, 20) };
            xNumericUpDown = new NumericUpDown { Location = new Point(50, 60), Size = new Size(100, 20), Minimum = 0, Maximum = 10000 };

            var widthLabel = new Label { Text = "Ширина:", Location = new Point(20, 100), Size = new Size(60, 20) };
            widthNumericUpDown = new NumericUpDown { Location = new Point(80, 100), Size = new Size(100, 20), Minimum = 1, Maximum = 10000 };

            var heightLabel = new Label { Text = "Высота:", Location = new Point(200, 100), Size = new Size(60, 20) };
            heightNumericUpDown = new NumericUpDown { Location = new Point(260, 100), Size = new Size(100, 20), Minimum = 1, Maximum = 10000 };

            scrollPanel.Controls.AddRange(new Control[] {
                nameLabel, nameTextBox,
                xLabel, xNumericUpDown,
                yLabel, yNumericUpDown,
                widthLabel, widthNumericUpDown,
                heightLabel, heightNumericUpDown
            });
        }
        private string GetImagePathByType(AwardImageType type)
        {
            var imagePathTochange = "";
            switch (type)
            {
                case AwardImageType.Points:
                    imagePathTochange = "Images/Awards/Points.png";
                    break;
				case AwardImageType.Experience:
                    imagePathTochange = "Images/Awards/Experince.png";
                    break;
                case AwardImageType.Spheres:
                    imagePathTochange = "Images/Awards/Spheres.png";
                    break;
                case AwardImageType.SSRTicket:
                    imagePathTochange = "Images/Awards/SSRTicket.png";
                    break;
                case AwardImageType.BossMedals:
                    imagePathTochange = "Images/Awards/BossMedals.png";
                    break;
                case AwardImageType.Ideas:
                    imagePathTochange = "Images/Awards/Idea.png";
                    break;
                case AwardImageType.Boxes:
                    imagePathTochange = "Images/Awards/Boxes.png";
                    break;
                case AwardImageType.Backpacks:
                    imagePathTochange = "Images/Awards/Backpack.png";
                    break;
                case AwardImageType.Bags:
                    imagePathTochange = "Images/Awards/Bags.png";
                    break;
                case AwardImageType.EpicBoxes:
                    imagePathTochange = "Images/Awards/EpicBoxes.png";
                    break;
                case AwardImageType.LegendBoxes:
                    imagePathTochange = "Images/Awards/LegendBoxes.png";
                    break;
                case AwardImageType.Glazgo:
                    imagePathTochange = "Images/Awards/Glazgo.png";
                    break;
                case AwardImageType.Burai:
                    imagePathTochange = "Images/Awards/Burai.png";
                    break;
                case AwardImageType.NightPolice:
                    imagePathTochange = "Images/Awards/KnightPolice.png";
                    break;
                case AwardImageType.Sazerlend:
                    imagePathTochange = "Images/Awards/Sazerland.png";
                    break;
                case AwardImageType.Gloster:
                    imagePathTochange = "Images/Awards/Gloster.png";
                    break;
                case AwardImageType.Gekka:
                    imagePathTochange = "Images/Awards/Gekka.png";
                    break;
                case AwardImageType.Akazuki:
                    imagePathTochange = "Images/Awards/Akazuki.png";
                    break;
                case AwardImageType.Vincent:
                    imagePathTochange = "Images/Awards/Vincent.png";
                    break;
                case AwardImageType.Diamonds:
                    imagePathTochange = "Images/Awards/diamond.png";
                    break;
                case AwardImageType.Gold:
                    imagePathTochange = "Images/Awards/gold.png";
                    break;
                case AwardImageType.Nightmares:
                    imagePathTochange = "Images/Awards/Nightmares.png";
                    break;
                case AwardImageType.Lancelot:
                    imagePathTochange = "Images/Awards/Lancelot.png";
                    break;
                case AwardImageType.Raid:
                    imagePathTochange = "Images/Awards/Raid.png";
                    break;
                case AwardImageType.Sakuradite:
                    imagePathTochange = "Images/Awards/Sakuradite.png";
                    break;
                case AwardImageType.SealGold:
                    imagePathTochange = "Images/Awards/SealGold.png";
                    break;
                case AwardImageType.SealLove:
                    imagePathTochange = "Images/Awards/SealLove.png";
                    break;
                case AwardImageType.SealSpace:
                    imagePathTochange = "Images/Awards/SealSpace.png";
                    break;
                case AwardImageType.SealSpiritual:
                    imagePathTochange = "Images/Awards/SealSpiritual.png";
                    break;
                case AwardImageType.SealTerrible:
                    imagePathTochange = "Images/Awards/SealTerrible.png";
                    break;
                case AwardImageType.SoulFragments:
                    imagePathTochange = "Images/Awards/SoulFragments.png";
                    break;
				case AwardImageType.Books:
					imagePathTochange = "Images/Awards/Books.png";
					break;
				default: return "";
            }
            return imagePathTochange;
        }
        private void AddSpecificControls()
        {
            int currentY = 140;

            if (element is TextElement textElement)
            {
                var textLabel = new Label { Text = "Текст:", Location = new Point(20, currentY), Size = new Size(100, 20) };
                var textTextBox = new TextBox { Text = textElement.Text, Location = new Point(130, currentY), Size = new Size(200, 20) };
                textTextBox.TextChanged += (s, e) => textElement.Text = textTextBox.Text;
                scrollPanel.Controls.Add(textLabel);
                scrollPanel.Controls.Add(textTextBox);
                currentY += 40;

                var fontLabel = new Label { Text = "Шрифт:", Location = new Point(20, currentY), Size = new Size(100, 20) };
                var fontButton = new Button { Text = "Выбрать шрифт", Location = new Point(130, currentY), Size = new Size(150, 30) };
                fontButton.Click += (s, e) =>
                {
                    using (var fontDialog = new FontDialog { Font = textElement.Font })
                    {
                        if (fontDialog.ShowDialog() == DialogResult.OK)
                        {
                            textElement.Font = fontDialog.Font;
                            textElement.FontName = fontDialog.Font.Name;
                            textElement.FontSize = fontDialog.Font.Size;
                            textElement.FontStyle = fontDialog.Font.Style;
                        }
                    }
                };
                scrollPanel.Controls.Add(fontLabel);
                scrollPanel.Controls.Add(fontButton);
                currentY += 40;
                // Прозрачность текста
                var opacityLabel = new Label { Text = "Прозрачность:", Location = new Point(20, currentY), Size = new Size(100, 20) };
                var opacityTrackBar = new TrackBar
                {
                    Location = new Point(130, currentY),
                    Size = new Size(150, 40),
                    Minimum = 0,
                    Maximum = 100,
                    Value = textElement.Opacity
                };
                var opacityValueLabel = new Label { Text = $"{textElement.Opacity}%", Location = new Point(290, currentY), Size = new Size(40, 20) };

                opacityTrackBar.ValueChanged += (s, e) =>
                {
                    textElement.Opacity = opacityTrackBar.Value;
                    opacityValueLabel.Text = $"{opacityTrackBar.Value}%";
                };

                scrollPanel.Controls.Add(opacityLabel);
                scrollPanel.Controls.Add(opacityTrackBar);
                scrollPanel.Controls.Add(opacityValueLabel);
                currentY += 50;
                var colorLabel = new Label { Text = "Цвет:", Location = new Point(20, currentY), Size = new Size(100, 20) };
                var colorButton = new Button { Text = "Выбрать цвет", Location = new Point(130, currentY), Size = new Size(150, 30) };
                colorButton.BackColor = textElement.Color;
                colorButton.Click += (s, e) =>
                {
                    using (var colorDialog = new ColorDialog { Color = textElement.Color })
                    {
                        if (colorDialog.ShowDialog() == DialogResult.OK)
                        {
                            textElement.Color = colorDialog.Color;
                            colorButton.BackColor = colorDialog.Color;
                        }
                    }
                };
                scrollPanel.Controls.Add(colorLabel);
                scrollPanel.Controls.Add(colorButton);
                currentY += 40;

                var OutlineCheckBox = new CheckBox
                {
                    Text = "Outline",
                    Location = new Point(20, currentY),
                    Size = new Size(250, 30),
                    Checked = textElement.HasOutline
                };
                OutlineCheckBox.CheckedChanged += (s, e) => textElement.HasOutline = OutlineCheckBox.Checked;
                scrollPanel.Controls.Add(OutlineCheckBox);
                currentY += 40;

                var mainTitleCheckBox = new CheckBox
                {
                    Text = "Главная надпись (по центру)",
                    Location = new Point(20, currentY),
                    Size = new Size(250, 30),
                    Checked = textElement.IsMainTitle
                };
                mainTitleCheckBox.CheckedChanged += (s, e) => textElement.IsMainTitle = mainTitleCheckBox.Checked;
                scrollPanel.Controls.Add(mainTitleCheckBox);
            }
            else if (element is ImageElement imageElement)
            {
                // Тип изображения
                var imageTypeLabel = new Label { Text = "Тип изображения:", Location = new Point(20, currentY), Size = new Size(100, 20) };
                var imageTypeComboBox = new ComboBox { Location = new Point(130, currentY), Size = new Size(150, 20), DropDownStyle = ComboBoxStyle.DropDownList };
                imageTypeComboBox.Items.AddRange(Enum.GetNames(typeof(AwardImageType)));
                // Устанавливаем выбранный элемент безопасно
                var selectedType = imageElement.ImageType.ToString();
                if (imageTypeComboBox.Items.Contains(selectedType))
                {
                    imageTypeComboBox.SelectedItem = selectedType;
                }
                else if (imageTypeComboBox.Items.Count > 0)
                {
                    imageTypeComboBox.SelectedIndex = 0;
                }
                imageTypeComboBox.SelectedIndexChanged += (s, e) =>
                {
                    var combo = s as ComboBox;
                    if (combo.SelectedItem != null)
                    {
                        imageElement.ImageType = (AwardImageType)Enum.Parse(typeof(AwardImageType), combo.SelectedItem.ToString());
                        if (imageElement.ImageType != AwardImageType.Custom)
                        {
                            switch (imageElement.ImageType)
                            {
                                case AwardImageType.Points:
                                    imageElement.ImagePath = "Images/Awards/Points.png";
                                    break;
                                case AwardImageType.Experience:
                                    imageElement.ImagePath = "Images/Awards/Experince.png";
                                    break;
                                case AwardImageType.Spheres:
                                    imageElement.ImagePath = "Images/Awards/Spheres.png";
                                    break;
                                case AwardImageType.SSRTicket:
                                    imageElement.ImagePath = "Images/Awards/SSRTicket.png";
                                    break;
                                case AwardImageType.BossMedals:
                                    imageElement.ImagePath = "Images/Awards/BossMedals.png";
                                    break;
                                case AwardImageType.Ideas:
                                    imageElement.ImagePath = "Images/Awards/Idea.png";
                                    break;
                                case AwardImageType.Boxes:
                                    imageElement.ImagePath = "Images/Awards/Boxes.png";
                                    break;
                                case AwardImageType.Backpacks:
                                    imageElement.ImagePath = "Images/Awards/Backpack.png";
                                    break;
                                case AwardImageType.Bags:
                                    imageElement.ImagePath = "Images/Awards/Bags.png";
                                    break;
                                case AwardImageType.EpicBoxes:
                                    imageElement.ImagePath = "Images/Awards/EpicBoxes.png";
                                    break;
                                case AwardImageType.LegendBoxes:
                                    imageElement.ImagePath = "Images/Awards/LegendBoxes.png";
                                    break;
                                case AwardImageType.Glazgo:
                                    imageElement.ImagePath = "Images/Awards/Glazgo.png";
                                    break;
                                case AwardImageType.Burai:
                                    imageElement.ImagePath = "Images/Awards/Burai.png";
                                    break;
                                case AwardImageType.NightPolice:
                                    imageElement.ImagePath = "Images/Awards/KnightPolice.png";
                                    break;
                                case AwardImageType.Sazerlend:
                                    imageElement.ImagePath = "Images/Awards/Sazerland.png";
                                    break;
                                case AwardImageType.Gloster:
                                    imageElement.ImagePath = "Images/Awards/Gloster.png";
                                    break;
                                case AwardImageType.Gekka:
                                    imageElement.ImagePath = "Images/Awards/Gekka.png";
                                    break;
                                case AwardImageType.Akazuki:
                                    imageElement.ImagePath = "Images/Awards/Akazuki.png";
                                    break;
                                case AwardImageType.Vincent:
                                    imageElement.ImagePath = "Images/Awards/Vincent.png";
                                    break;
                                case AwardImageType.Diamonds:
                                    imageElement.ImagePath = "Images/Awards/diamond.png";
                                    break;
                                case AwardImageType.Gold:
                                    imageElement.ImagePath = "Images/Awards/gold.png";
                                    break;
                                case AwardImageType.Nightmares:
                                    imageElement.ImagePath = "Images/Awards/Nightmares.png";
                                    break;
                                case AwardImageType.Lancelot:
                                    imageElement.ImagePath = "Images/Awards/Lancelot.png";
                                    break;
                                case AwardImageType.Raid:
                                    imageElement.ImagePath = "Images/Awards/Raid.png";
                                    break;
                                case AwardImageType.Sakuradite:
                                    imageElement.ImagePath = "Images/Awards/Sakuradite.png";
                                    break;
                                case AwardImageType.SealGold:
                                    imageElement.ImagePath = "Images/Awards/SealGold.png";
                                    break;
                                case AwardImageType.SealLove:
                                    imageElement.ImagePath = "Images/Awards/SealLove.png";
                                    break;
                                case AwardImageType.SealSpace:
                                    imageElement.ImagePath = "Images/Awards/SealSpace.png";
                                    break;
                                case AwardImageType.SealSpiritual:
                                    imageElement.ImagePath = "Images/Awards/SealSpiritual.png";
                                    break;
                                case AwardImageType.SealTerrible:
                                    imageElement.ImagePath = "Images/Awards/SealTerrible.png";
                                    break;
                                case AwardImageType.SoulFragments:
                                    imageElement.ImagePath = "Images/Awards/SoulFragments.png";
                                    break;
								case AwardImageType.Books:
									imageElement.ImagePath = "Images/Awards/Books.png";
									break;
							}

                        }
                    }
                };
                scrollPanel.Controls.Add(imageTypeLabel);
                scrollPanel.Controls.Add(imageTypeComboBox);
                currentY += 40;

                // Кнопка загрузки изображения
                var loadImageLabel = new Label { Text = "Изображение:", Location = new Point(20, currentY), Size = new Size(100, 20) };
                var loadImageButton = new Button { Text = "Загрузить", Location = new Point(130, currentY), Size = new Size(100, 30) };
                loadImageButton.Click += (s, e) =>
                {
                    using (var dialog = new OpenFileDialog())
                    {
                        dialog.Filter = "Изображения|*.jpg;*.jpeg;*.png;*.bmp|Все файлы|*.*";
                        if (dialog.ShowDialog() == DialogResult.OK)
                        {
                            imageElement.ImagePath = dialog.FileName;
                            imageElement.ImageType = AwardImageType.Custom;
                            // Обновляем ComboBox
                            if (imageTypeComboBox.Items.Contains(AwardImageType.Custom.ToString()))
                            {
                                imageTypeComboBox.SelectedItem = AwardImageType.Custom.ToString();
                            }
                        }
                    }
                };
                scrollPanel.Controls.Add(loadImageLabel);
                scrollPanel.Controls.Add(loadImageButton);
                currentY += 40;
                var styleLabel = new Label { Text = "Стиль:", Location = new Point(20, currentY), Size = new Size(100, 20) };
                var styleComboBox = new ComboBox { Location = new Point(130, currentY), Size = new Size(150, 20), DropDownStyle = ComboBoxStyle.DropDownList };
                styleComboBox.Items.AddRange(Enum.GetNames(typeof(ImageStyle)));
                styleComboBox.SelectedItem = imageElement.Style.ToString();
                styleComboBox.SelectedIndexChanged += (s, e) =>
                {
                    imageElement.Style = (ImageStyle)Enum.Parse(typeof(ImageStyle), styleComboBox.SelectedItem.ToString());
                };
                scrollPanel.Controls.Add(styleLabel);
                scrollPanel.Controls.Add(styleComboBox);
                currentY += 40;

                var brightnessLabel = new Label { Text = "Яркость:", Location = new Point(20, currentY), Size = new Size(100, 20) };
                var brightnessTrackBar = new TrackBar
                {
                    Location = new Point(130, currentY),
                    Size = new Size(150, 40),
                    Minimum = -100,
                    Maximum = 100,
                    Value = (int)(imageElement.Brightness * 100)
                };
                brightnessTrackBar.ValueChanged += (s, e) => imageElement.Brightness = brightnessTrackBar.Value / 100f;
                scrollPanel.Controls.Add(brightnessLabel);
                scrollPanel.Controls.Add(brightnessTrackBar);
                currentY += 40;

                var contrastLabel = new Label { Text = "Контраст:", Location = new Point(20, currentY), Size = new Size(100, 20) };
                var contrastTrackBar = new TrackBar
                {
                    Location = new Point(130, currentY),
                    Size = new Size(150, 40),
                    Minimum = -100,
                    Maximum = 100,
                    Value = (int)(imageElement.Contrast * 100)
                };
                contrastTrackBar.ValueChanged += (s, e) => imageElement.Contrast = contrastTrackBar.Value / 100f;
                scrollPanel.Controls.Add(contrastLabel);
                scrollPanel.Controls.Add(contrastTrackBar);
                currentY += 50;

                // Тип масштабирования изображения
                var scaleTypeLabel = new Label { Text = "Тип масштабирования:", Location = new Point(20, currentY), Size = new Size(250, 20) };
                currentY += 30;
                var scaleTypeComboBox = new ComboBox
                {
                    Location = new Point(100, currentY),
                    Size = new Size(150, 20),
                    DropDownStyle = ComboBoxStyle.DropDownList
                };
                scaleTypeComboBox.Items.AddRange(Enum.GetNames(typeof(ImageScaleType)));
                scaleTypeComboBox.SelectedItem = imageElement.ImageScaleType.ToString();
                scaleTypeComboBox.SelectedIndexChanged += (s, e) =>
                {
                    imageElement.ImageScaleType = (ImageScaleType)Enum.Parse(typeof(ImageScaleType), scaleTypeComboBox.SelectedItem.ToString());
                };
                scrollPanel.Controls.Add(scaleTypeLabel);
                scrollPanel.Controls.Add(scaleTypeComboBox);
                currentY += 40;

                // Опции рамки
                var borderGroupLabel = new Label
                {
                    Text = "Настройки рамки:",
                    Location = new Point(20, currentY),
                    Size = new Size(150, 20),
                    Font = new Font("Arial", 9, FontStyle.Bold)
                };
                scrollPanel.Controls.Add(borderGroupLabel);
                currentY += 25;

                // Показывать рамку
                var showBorderCheckBox = new CheckBox
                {
                    Text = "Показывать рамку",
                    Location = new Point(20, currentY),
                    Size = new Size(150, 20),
                    Checked = imageElement.ShowBorder
                };
                showBorderCheckBox.CheckedChanged += (s, e) => imageElement.ShowBorder = showBorderCheckBox.Checked;
                scrollPanel.Controls.Add(showBorderCheckBox);
                currentY += 30;

                // Ширина рамки
                var borderWidthLabel = new Label { Text = "Ширина рамки:", Location = new Point(20, currentY), Size = new Size(100, 20) };
                var borderWidthNumeric = new NumericUpDown
                {
                    Location = new Point(130, currentY),
                    Size = new Size(60, 20),
                    Minimum = 1,
                    Maximum = 20,
                    Value = imageElement.BorderWidth
                };
                borderWidthNumeric.ValueChanged += (s, e) => imageElement.BorderWidth = (int)borderWidthNumeric.Value;
                scrollPanel.Controls.Add(borderWidthLabel);
                scrollPanel.Controls.Add(borderWidthNumeric);
                currentY += 30;

                // Цвет рамки
                var borderColorLabel = new Label { Text = "Цвет рамки:", Location = new Point(20, currentY), Size = new Size(200, 20) };
                currentY += 30;
                var borderColorButton = new Button { Text = "Выбрать цвет", Location = new Point(130, currentY), Size = new Size(150, 30) };
                borderColorButton.BackColor = imageElement.BorderColor;
                borderColorButton.Click += (s, e) =>
                {
                    using (var colorDialog = new ColorDialog { Color = imageElement.BorderColor })
                    {
                        if (colorDialog.ShowDialog() == DialogResult.OK)
                        {
                            imageElement.BorderColor = colorDialog.Color;
                            borderColorButton.BackColor = colorDialog.Color;
                        }
                    }
                };
                scrollPanel.Controls.Add(borderColorLabel);
                scrollPanel.Controls.Add(borderColorButton);
            }
            else if (element is BackgroundElement bgElement)
            {
                var styleLabel = new Label { Text = "Стиль:", Location = new Point(20, currentY), Size = new Size(100, 20) };
                var styleComboBox = new ComboBox { Location = new Point(130, currentY), Size = new Size(150, 20), DropDownStyle = ComboBoxStyle.DropDownList };
                styleComboBox.Items.AddRange(Enum.GetNames(typeof(BackgroundStyle)));
                styleComboBox.SelectedItem = bgElement.Style.ToString();
                styleComboBox.SelectedIndexChanged += (s, e) =>
                {
                    bgElement.Style = (BackgroundStyle)Enum.Parse(typeof(BackgroundStyle), styleComboBox.SelectedItem.ToString());
                };
                scrollPanel.Controls.Add(styleLabel);
                scrollPanel.Controls.Add(styleComboBox);
                currentY += 40;

                var brightnessLabel = new Label { Text = "Яркость:", Location = new Point(20, currentY), Size = new Size(100, 20) };
                var brightnessTrackBar = new TrackBar
                {
                    Location = new Point(130, currentY),
                    Size = new Size(150, 40),
                    Minimum = -100,
                    Maximum = 100,
                    Value = (int)(bgElement.Brightness * 100)
                };
                brightnessTrackBar.ValueChanged += (s, e) => bgElement.Brightness = brightnessTrackBar.Value / 100f;
                scrollPanel.Controls.Add(brightnessLabel);
                scrollPanel.Controls.Add(brightnessTrackBar);
                currentY += 40;

                var contrastLabel = new Label { Text = "Контраст:", Location = new Point(20, currentY), Size = new Size(100, 20) };
                var contrastTrackBar = new TrackBar
                {
                    Location = new Point(130, currentY),
                    Size = new Size(150, 40),
                    Minimum = -100,
                    Maximum = 100,
                    Value = (int)(bgElement.Contrast * 100)
                };
                contrastTrackBar.ValueChanged += (s, e) => bgElement.Contrast = contrastTrackBar.Value / 100f;
                scrollPanel.Controls.Add(contrastLabel);
                scrollPanel.Controls.Add(contrastTrackBar);
            }
            else if (element is ImageRowElement imageRow)
            {
                // Отображение добавленных изображений
                var imagesLabel = new Label { Text = "Добавленные изображения:", Location = new Point(20, currentY), Size = new Size(200, 20), Font = new Font("Arial", 9, FontStyle.Bold) };
                scrollPanel.Controls.Add(imagesLabel);
                currentY += 25;

                // Создаем панель для отображения миниатюр
                var imagesPanel = new Panel
                {
                    Location = new Point(20, currentY),
                    Size = new Size(400, 100),
                    BackColor = Color.LightGray,
                    BorderStyle = BorderStyle.FixedSingle
                };
                scrollPanel.Controls.Add(imagesPanel);

                // Отображаем миниатюры
                UpdateImageThumbnails(imagesPanel, imageRow.ImagePaths);
                currentY += 120;

                var itemsLabel = new Label { Text = "Изображения:", Location = new Point(20, currentY), Size = new Size(100, 20) };
                var addImageButton = new Button { Text = "Добавить изображения", Location = new Point(130, currentY), Size = new Size(150, 30) };
                addImageButton.Click += (s, e) =>
                {
                    AddImageButtonToRow(imageRow, imagesPanel);
                };
                scrollPanel.Controls.Add(itemsLabel);
                scrollPanel.Controls.Add(addImageButton);
                currentY += 40;

                // Добавьте кнопку для добавления одного изображения:
                var addSingleButton = new Button { Text = "Добавить одно", Location = new Point(290, currentY - 40), Size = new Size(100, 30) };
                addSingleButton.Click += (s, e) =>
                {
                    using (var dialog = new OpenFileDialog())
                    {
                        dialog.Filter = "Изображения|*.jpg;*.jpeg;*.png;*.bmp|Все файлы|*.*";
                        if (dialog.ShowDialog() == DialogResult.OK)
                        {
                            imageRow.ImagePaths.Add(dialog.FileName);
                            UpdateImageThumbnails(imagesPanel, imageRow.ImagePaths);
                        }
                    }
                };
                scrollPanel.Controls.Add(addSingleButton);

                // Добавьте кнопку для очистки:
                var clearButton = new Button { Text = "Очистить", Location = new Point(290, currentY - 10), Size = new Size(100, 30) };
                clearButton.Click += (s, e) =>
                {
                    imageRow.ImagePaths.Clear();
                    UpdateImageThumbnails(imagesPanel, imageRow.ImagePaths);
                };
                scrollPanel.Controls.Add(clearButton);
                currentY += 30;
                // Опции рамки для ImageRow
                var borderGroupLabel = new Label
                {
                    Text = "Настройки рамки:",
                    Location = new Point(20, currentY),
                    Size = new Size(150, 20),
                    Font = new Font("Arial", 9, FontStyle.Bold)
                };
                scrollPanel.Controls.Add(borderGroupLabel);
                currentY += 25;

                // Показывать рамку
                var showBorderCheckBox = new CheckBox
                {
                    Text = "Показывать рамку",
                    Location = new Point(20, currentY),
                    Size = new Size(150, 20),
                    Checked = imageRow.ShowBorder
                };
                showBorderCheckBox.CheckedChanged += (s, e) => imageRow.ShowBorder = showBorderCheckBox.Checked;
                scrollPanel.Controls.Add(showBorderCheckBox);
                currentY += 30;

                // Ширина рамки
                var borderWidthLabel = new Label { Text = "Ширина рамки:", Location = new Point(20, currentY), Size = new Size(100, 20) };
                var borderWidthNumeric = new NumericUpDown
                {
                    Location = new Point(130, currentY),
                    Size = new Size(60, 20),
                    Minimum = 1,
                    Maximum = 20,
                    Value = imageRow.BorderWidth
                };
                borderWidthNumeric.ValueChanged += (s, e) => imageRow.BorderWidth = (int)borderWidthNumeric.Value;
                scrollPanel.Controls.Add(borderWidthLabel);
                scrollPanel.Controls.Add(borderWidthNumeric);
                currentY += 30;

                // Цвет рамки
                var borderColorLabel = new Label { Text = "Цвет рамки:", Location = new Point(20, currentY), Size = new Size(200, 20) };
                currentY += 30;
                var borderColorButton = new Button { Text = "Выбрать цвет", Location = new Point(130, currentY), Size = new Size(150, 30) };
                borderColorButton.BackColor = imageRow.BorderColor;
                borderColorButton.Click += (s, e) =>
                {
                    using (var colorDialog = new ColorDialog { Color = imageRow.BorderColor })
                    {
                        if (colorDialog.ShowDialog() == DialogResult.OK)
                        {
                            imageRow.BorderColor = colorDialog.Color;
                            borderColorButton.BackColor = colorDialog.Color;
                        }
                    }
                };
                scrollPanel.Controls.Add(borderColorLabel);
                scrollPanel.Controls.Add(borderColorButton);
                currentY += 40;


                // Тип масштабирования изображений
                var scaleTypeLabel = new Label { Text = "Тип масштабирования:", Location = new Point(20, currentY), Size = new Size(150, 20) };
                var scaleTypeComboBox = new ComboBox
                {
                    Location = new Point(180, currentY),
                    Size = new Size(150, 20),
                    DropDownStyle = ComboBoxStyle.DropDownList
                };
                scaleTypeComboBox.Items.AddRange(Enum.GetNames(typeof(ImageScaleType)));
                scaleTypeComboBox.SelectedItem = imageRow.ImageScaleType.ToString();
                scaleTypeComboBox.SelectedIndexChanged += (s, e) =>
                {
                    imageRow.ImageScaleType = (ImageScaleType)Enum.Parse(typeof(ImageScaleType), scaleTypeComboBox.SelectedItem.ToString());
                };
                scrollPanel.Controls.Add(scaleTypeLabel);
                scrollPanel.Controls.Add(scaleTypeComboBox);
                currentY += 30;

                // Максимальная ширина строки
                var maxWidthLabel = new Label { Text = "Макс. ширина строки:", Location = new Point(20, currentY), Size = new Size(150, 20) };
                var maxWidthNumeric = new NumericUpDown
                {
                    Location = new Point(180, currentY),
                    Size = new Size(100, 20),
                    Minimum = imageRow.ItemWidth,
                    Maximum = 10000,
                    Value = imageRow.MaxRowWidth
                };
                maxWidthNumeric.ValueChanged += (s, e) => imageRow.MaxRowWidth = (int)maxWidthNumeric.Value;
                scrollPanel.Controls.Add(maxWidthLabel);
                scrollPanel.Controls.Add(maxWidthNumeric);
                currentY += 30;

                // Перенос изображений
                var wrapCheckBox = new CheckBox
                {
                    Text = "Переносить изображения",
                    Location = new Point(20, currentY),
                    Size = new Size(200, 20),
                    Checked = imageRow.WrapImages
                };
                wrapCheckBox.CheckedChanged += (s, e) => imageRow.WrapImages = wrapCheckBox.Checked;
                scrollPanel.Controls.Add(wrapCheckBox);
                currentY += 30;
                // Исправленные NumericUpDown с правильными пределами
                var itemWidthLabel = new Label { Text = "Ширина элемента:", Location = new Point(20, currentY), Size = new Size(100, 20) };
                var itemWidthNumeric = new NumericUpDown
                {
                    Location = new Point(130, currentY),
                    Size = new Size(100, 20),
                    Minimum = 20,
                    Maximum = 500,
                    Value = Math.Min(Math.Max(imageRow.ItemWidth, 20), 500), // Обеспечиваем корректное начальное значение
                    DecimalPlaces = 0
                };
                itemWidthNumeric.ValueChanged += (s, e) =>
                {
                    imageRow.ItemWidth = (int)itemWidthNumeric.Value;
                    // Обновляем миниатюры при изменении размера
                    UpdateImageThumbnails(imagesPanel, imageRow.ImagePaths);
                };
                scrollPanel.Controls.Add(itemWidthLabel);
                scrollPanel.Controls.Add(itemWidthNumeric);
                currentY += 30;

                var itemHeightLabel = new Label { Text = "Высота элемента:", Location = new Point(20, currentY), Size = new Size(100, 20) };
                var itemHeightNumeric = new NumericUpDown
                {
                    Location = new Point(130, currentY),
                    Size = new Size(100, 20),
                    Minimum = 20,
                    Maximum = 500,
                    Value = Math.Min(Math.Max(imageRow.ItemHeight, 20), 500), // Обеспечиваем корректное начальное значение
                    DecimalPlaces = 0
                };
                itemHeightNumeric.ValueChanged += (s, e) =>
                {
                    imageRow.ItemHeight = (int)itemHeightNumeric.Value;
                    // Обновляем миниатюры при изменении размера
                    UpdateImageThumbnails(imagesPanel, imageRow.ImagePaths);
                };
                scrollPanel.Controls.Add(itemHeightLabel);
                scrollPanel.Controls.Add(itemHeightNumeric);
                currentY += 30;

                var spacingLabel = new Label { Text = "Отступ:", Location = new Point(20, currentY), Size = new Size(100, 20) };
                var spacingNumeric = new NumericUpDown
                {
                    Location = new Point(130, currentY),
                    Size = new Size(100, 20),
                    Value = Math.Min(Math.Max(imageRow.Spacing, 0), 100), // Обеспечиваем корректное начальное значение
                    Minimum = 0,
                    Maximum = 100,
                    DecimalPlaces = 0
                };
                spacingNumeric.ValueChanged += (s, e) => imageRow.Spacing = (int)spacingNumeric.Value;
                scrollPanel.Controls.Add(spacingLabel);
                scrollPanel.Controls.Add(spacingNumeric);

            }
            else if (element is EventElement eventElement)
            {
                // Добавляем общие свойства для EventElement
                var commonPropertiesLabel = new Label
                {
                    Text = "Общие свойства наград:",
                    Location = new Point(20, currentY),
                    Size = new Size(200, 20),
                    Font = new Font("Arial", 9, FontStyle.Bold)
                };
                scrollPanel.Controls.Add(commonPropertiesLabel);
                currentY += 25;

                // Общий шрифт для всех наград
                var commonFontLabel = new Label { Text = "Общий шрифт:", Location = new Point(20, currentY), Size = new Size(100, 20) };
                var commonFontButton = new Button { Text = "Выбрать шрифт", Location = new Point(130, currentY), Size = new Size(150, 30) };
                commonFontButton.Click += (s, e) =>
                {
                    if (eventElement.Awards.Count > 0)
                    {
                        using (var fontDialog = new FontDialog { Font = eventElement.Awards[0].Font })
                        {
                            if (fontDialog.ShowDialog() == DialogResult.OK)
                            {
                                foreach (var award in eventElement.Awards)
                                {
                                    award.Font = fontDialog.Font;
                                    award.FontName = fontDialog.Font.Name;
                                    award.FontSize = fontDialog.Font.Size;
                                    award.FontStyle = fontDialog.Font.Style;
                                }
                            }
                        }
                    }
                };
                scrollPanel.Controls.Add(commonFontLabel);
                scrollPanel.Controls.Add(commonFontButton);
                currentY += 40;
                // Общий шрифт для всех наград
                var commonFontCaptionsLabel = new Label { Text = "Подписи:", Location = new Point(20, currentY), Size = new Size(100, 20) };
                var commonFontCaptionsButton = new Button { Text = "Выбрать шрифт", Location = new Point(130, currentY), Size = new Size(150, 30) };
                commonFontCaptionsButton.Click += (s, e) =>
                {
                    if (eventElement.Awards.Count > 0)
                    {
                        using (var fontDialog = new FontDialog { Font = (eventElement.CaptionElements[0] as TextElement).Font})
                        {
                            if (fontDialog.ShowDialog() == DialogResult.OK)
                            {
                                foreach (var caption in eventElement.CaptionElements)
                                {
                                    if (caption is TextElement) {
                                        var capT = caption as TextElement;
                                        capT.Font = fontDialog.Font;
                                        capT.FontName = fontDialog.Font.Name;
                                        capT.FontSize = fontDialog.Font.Size;
                                        capT.FontStyle = fontDialog.Font.Style;
                                    }

                                }
                            }
                        }
                    }
                };
                scrollPanel.Controls.Add(commonFontCaptionsLabel);
                scrollPanel.Controls.Add(commonFontCaptionsButton);
                currentY += 40;
                // Общий цвет текста для всех наград
                var commonTextColorLabel = new Label { Text = "Общий цвет текста:", Location = new Point(20, currentY), Size = new Size(200, 20) };
                currentY += 30;
                var commonTextColorButton = new Button { Text = "Выбрать цвет", Location = new Point(130, currentY), Size = new Size(150, 30) };
                if (eventElement.Awards.Count > 0)
                {
                    commonTextColorButton.BackColor = eventElement.Awards[0].TextColor;
                }
                commonTextColorButton.Click += (s, e) =>
                {
                    if (eventElement.Awards.Count > 0)
                    {
                        using (var colorDialog = new ColorDialog { Color = eventElement.Awards[0].TextColor })
                        {
                            if (colorDialog.ShowDialog() == DialogResult.OK)
                            {
                                foreach (var award in eventElement.Awards)
                                {
                                    award.TextColor = colorDialog.Color;
                                }
                                commonTextColorButton.BackColor = colorDialog.Color;
                            }
                        }
                    }
                };
                scrollPanel.Controls.Add(commonTextColorLabel);
                scrollPanel.Controls.Add(commonTextColorButton);
                currentY += 40;

                // Общая прозрачность текста для всех наград
                var commonTextOpacityLabel = new Label { Text = "Общая прозрачность текста:", Location = new Point(20, currentY), Size = new Size(140, 40) };
                currentY += 10;
                var commonTextOpacityTrackBar = new TrackBar
                {
                    Location = new Point(170, currentY),
                    Size = new Size(150, 40),
                    Minimum = 0,
                    Maximum = 100,
                    Value = eventElement.Awards.Count > 0 ? eventElement.Awards[0].TextOpacity : 100
                };
                var commonTextOpacityValueLabel = new Label
                {
                    Text = $"{(eventElement.Awards.Count > 0 ? eventElement.Awards[0].TextOpacity : 100)}%",
                    Location = new Point(330, currentY),
                    Size = new Size(40, 20)
                };

                commonTextOpacityTrackBar.ValueChanged += (s, e) =>
                {
                    int value = commonTextOpacityTrackBar.Value;
                    foreach (var award in eventElement.Awards)
                    {
                        award.TextOpacity = value;
                    }
                    commonTextOpacityValueLabel.Text = $"{value}%";
                };

                scrollPanel.Controls.Add(commonTextOpacityLabel);
                scrollPanel.Controls.Add(commonTextOpacityTrackBar);
                scrollPanel.Controls.Add(commonTextOpacityValueLabel);
                currentY += 60;

                // Общий цвет фона для всех наград
                var commonBgColorLabel = new Label { Text = "Общий цвет фона:", Location = new Point(20, currentY), Size = new Size(200, 20) };
                currentY += 30;
                var commonBgColorButton = new Button { Text = "Выбрать цвет", Location = new Point(130, currentY), Size = new Size(150, 30) };
                if (eventElement.Awards.Count > 0)
                {
                    commonBgColorButton.BackColor = eventElement.Awards[0].BackgroundColor;
                }
                commonBgColorButton.Click += (s, e) =>
                {
                    if (eventElement.Awards.Count > 0)
                    {
                        using (var colorDialog = new ColorDialog { Color = eventElement.Awards[0].BackgroundColor })
                        {
                            if (colorDialog.ShowDialog() == DialogResult.OK)
                            {
                                foreach (var award in eventElement.Awards)
                                {
                                    award.BackgroundColor = colorDialog.Color;
                                }
                                commonBgColorButton.BackColor = colorDialog.Color;
                            }
                        }
                    }
                };
                scrollPanel.Controls.Add(commonBgColorLabel);
                scrollPanel.Controls.Add(commonBgColorButton);
                currentY += 40;

                // Общий цвет рамки для всех наград
                var commonBorderColorLabel = new Label { Text = "Общий цвет рамки:", Location = new Point(20, currentY), Size = new Size(200, 20) };
                currentY += 30;
                var commonBorderColorButton = new Button { Text = "Выбрать цвет", Location = new Point(130, currentY), Size = new Size(150, 30) };
                if (eventElement.Awards.Count > 0)
                {
                    commonBorderColorButton.BackColor = eventElement.Awards[0].BorderColor;
                }
                commonBorderColorButton.Click += (s, e) =>
                {
                    if (eventElement.Awards.Count > 0)
                    {
                        using (var colorDialog = new ColorDialog { Color = eventElement.Awards[0].BorderColor })
                        {
                            if (colorDialog.ShowDialog() == DialogResult.OK)
                            {
                                foreach (var award in eventElement.Awards)
                                {
                                    award.BorderColor = colorDialog.Color;
                                }
                                commonBorderColorButton.BackColor = colorDialog.Color;
                            }
                        }
                    }
                };
                scrollPanel.Controls.Add(commonBorderColorLabel);
                scrollPanel.Controls.Add(commonBorderColorButton);
                currentY += 40;
                // Общий цвет стрелки для всех наград
                var commonArrowColorLabel = new Label { Text = "Стрелка:", Location = new Point(20, currentY), Size = new Size(150, 20) };
                currentY += 30;
                var commonArrowColorButton = new Button { Text = "Выбрать цвет", Location = new Point(130, currentY), Size = new Size(150, 30) };
                if (eventElement.Awards.Count > 0)
                {
                    commonArrowColorButton.BackColor = eventElement.Awards[0].ArrowColor;
                }
                commonArrowColorButton.Click += (s, e) =>
                {
                    if (eventElement.Awards.Count > 0)
                    {
                        using (var colorDialog = new ColorDialog { Color = eventElement.Awards[0].ArrowColor })
                        {
                            if (colorDialog.ShowDialog() == DialogResult.OK)
                            {
                                foreach (var award in eventElement.Awards)
                                {
                                    award.ArrowColor = colorDialog.Color;
                                }
                                commonArrowColorButton.BackColor = colorDialog.Color;
                            }
                        }
                    }
                };
                scrollPanel.Controls.Add(commonArrowColorLabel);
                scrollPanel.Controls.Add(commonArrowColorButton);
                currentY += 40;
                // Общая ширина рамки для всех наград
                var commonBorderWidthLabel = new Label { Text = "Общая ширина рамки:", Location = new Point(20, currentY), Size = new Size(120, 20) };
                var commonBorderWidthNumeric = new NumericUpDown
                {
                    Location = new Point(150, currentY),
                    Size = new Size(60, 20),
                    Minimum = 0,
                    Maximum = 20,
                    Value = eventElement.Awards.Count > 0 ? eventElement.Awards[0].BorderWidth : 3
                };
                commonBorderWidthNumeric.ValueChanged += (s, e) =>
                {
                    foreach (var award in eventElement.Awards)
                    {
                        award.BorderWidth = (int)commonBorderWidthNumeric.Value;
                    }
                };
                scrollPanel.Controls.Add(commonBorderWidthLabel);
                scrollPanel.Controls.Add(commonBorderWidthNumeric);
                currentY += 30;
                // Реализация редактирования EventElement
                var awardsLabel = new Label { Text = "Награды:", Location = new Point(20, currentY), Font = new Font("Arial", 9, FontStyle.Bold) };
                scrollPanel.Controls.Add(awardsLabel);
                currentY += 25;

                for (int i = 0; i < eventElement.Awards.Count; i++)
                {
                    var award = eventElement.Awards[i];
                    var awardLabel = new Label { Text = $"Награда {i + 1}:", Location = new Point(20, currentY) };
                    var editButton = new Button { Text = "Редактировать", Location = new Point(120, currentY), Size = new Size(100, 25), Tag = award };
                    editButton.Click += (s, e) => ShowAwardProperties((AwardElement)((Button)s).Tag);

                    scrollPanel.Controls.Add(awardLabel);
                    scrollPanel.Controls.Add(editButton);
                    currentY += 30;
                }
                // После общих свойств добавляем настройки отступов
                var spacingGroupLabel = new Label
                {
                    Text = "Настройки отступов:",
                    Location = new Point(20, currentY),
                    Size = new Size(200, 20),
                    Font = new Font("Arial", 9, FontStyle.Bold)
                };
                scrollPanel.Controls.Add(spacingGroupLabel);
                currentY += 25;

                // Горизонтальный отступ между наградами
                var horizontalSpacingLabel = new Label
                {
                    Text = "Горизонтальный отступ:",
                    Location = new Point(20, currentY),
                    Size = new Size(150, 20)
                };
                var horizontalSpacingNumeric = new NumericUpDown
                {
                    Location = new Point(180, currentY),
                    Size = new Size(60, 20),
                    Minimum = 0,
                    Maximum = 100,
                    Value = eventElement.HorizontalSpacing
                };
                horizontalSpacingNumeric.ValueChanged += (s, e) =>
                {
                    eventElement.HorizontalSpacing = (int)horizontalSpacingNumeric.Value;
                };
                scrollPanel.Controls.Add(horizontalSpacingLabel);
                scrollPanel.Controls.Add(horizontalSpacingNumeric);
                currentY += 30;

                // Вертикальный отступ
                var verticalSpacingLabel = new Label
                {
                    Text = "Вертикальный отступ:",
                    Location = new Point(20, currentY),
                    Size = new Size(150, 20)
                };
                var verticalSpacingNumeric = new NumericUpDown
                {
                    Location = new Point(180, currentY),
                    Size = new Size(60, 20),
                    Minimum = 0,
                    Maximum = 100,
                    Value = eventElement.VerticalSpacing
                };
                verticalSpacingNumeric.ValueChanged += (s, e) =>
                {
                    eventElement.VerticalSpacing = (int)verticalSpacingNumeric.Value;
                };
                scrollPanel.Controls.Add(verticalSpacingLabel);
                scrollPanel.Controls.Add(verticalSpacingNumeric);
                currentY += 40;
                // Добавляем чекбокс для показа подписей
                var showCaptionsCheckBox = new CheckBox
                {
                    Text = "Показывать подписи",
                    Location = new Point(20, currentY),
                    Size = new Size(150, 20),
                    Checked = eventElement.ShowCaptions
                };
                showCaptionsCheckBox.CheckedChanged += (s, e) =>
                {
                    eventElement.ShowCaptions = showCaptionsCheckBox.Checked;
                };
                scrollPanel.Controls.Add(showCaptionsCheckBox);
                currentY += 30;
                // Добавляем настройку отступа между элементами подписей
                var spacingLabel = new Label
                {
                    Text = "Отступ между элементами:",
                    Location = new Point(20, currentY),
                    Size = new Size(150, 20)
                };
                var spacingNumeric = new NumericUpDown
                {
                    Location = new Point(180, currentY),
                    Size = new Size(60, 20),
                    Minimum = 1,
                    Maximum = 200,
                    Value = eventElement.CaptionSpacing
                };
                spacingNumeric.ValueChanged += (s, e) => eventElement.CaptionSpacing = (int)spacingNumeric.Value;
                scrollPanel.Controls.Add(spacingLabel);
                scrollPanel.Controls.Add(spacingNumeric);
                currentY += 30;
                // Добавляем редактирование элементов подписей
                if (eventElement.ShowCaptions)
                {
                    var captionsLabel = new Label
                    {
                        Text = "Элементы подписей:",
                        Location = new Point(20, currentY),
                        Font = new Font("Arial", 9, FontStyle.Bold)
                    };
                    scrollPanel.Controls.Add(captionsLabel);
                    currentY += 25;

                    for (int i = 0; i < eventElement.CaptionElements.Count; i++)
                    {
                        var captionElement = eventElement.CaptionElements[i];
                        var elementLabel = new Label
                        {
                            Text = $"{captionElement.Name}:",
                            Location = new Point(20, currentY)
                        };
                        var editButton = new Button
                        {
                            Text = "Редактировать",
                            Location = new Point(150, currentY),
                            Size = new Size(100, 25),
                            Tag = captionElement
                        };
                        editButton.Click += (s, e) => ShowElementProperties((BaseElement)((Button)s).Tag);

                        scrollPanel.Controls.Add(elementLabel);
                        scrollPanel.Controls.Add(editButton);
                        currentY += 30;
                    }
                }
            }
            else if (element is BPElement bpElement)
            {
                // Добавляем общие свойства для BPElement
                var commonPropertiesLabel = new Label
                {
                    Text = "Общие свойства наград:",
                    Location = new Point(20, currentY),
                    Size = new Size(200, 30),
                    Font = new Font("Arial", 9, FontStyle.Bold)
                };
                scrollPanel.Controls.Add(commonPropertiesLabel);
                currentY += 35;

                // Количество наград в ряду
                var itemsPerRowLabel = new Label { Text = "Наград в ряду:", Location = new Point(20, currentY), Size = new Size(200, 20) };
                var itemsPerRowNumeric = new NumericUpDown
                {
                    Location = new Point(230, currentY),
                    Size = new Size(60, 20),
                    Minimum = 1,
                    Maximum = 20,
                    Value = bpElement.ItemsPerRow
                };
                itemsPerRowNumeric.ValueChanged += (s, e) => bpElement.ItemsPerRow = (int)itemsPerRowNumeric.Value;
                scrollPanel.Controls.Add(itemsPerRowLabel);
                scrollPanel.Controls.Add(itemsPerRowNumeric);
                currentY += 30;

                // Общий шрифт для всех наград
                var commonFontLabel = new Label { Text = "Общий шрифт:", Location = new Point(20, currentY), Size = new Size(100, 20) };
                var commonFontButton = new Button { Text = "Выбрать шрифт", Location = new Point(130, currentY), Size = new Size(150, 30) };
                commonFontButton.Click += (s, e) =>
                {
                    if (bpElement.Awards.Count > 0)
                    {
                        using (var fontDialog = new FontDialog { Font = bpElement.Awards[0].Font })
                        {
                            if (fontDialog.ShowDialog() == DialogResult.OK)
                            {
                                foreach (var award in bpElement.Awards)
                                {
                                    award.Font = fontDialog.Font;
                                    award.FontName = fontDialog.Font.Name;
                                    award.FontSize = fontDialog.Font.Size;
                                    award.FontStyle = fontDialog.Font.Style;
                                }
                            }
                        }
                    }
                };
                scrollPanel.Controls.Add(commonFontLabel);
                scrollPanel.Controls.Add(commonFontButton);
                currentY += 40;

                // Общий шрифт для подписей
                var commonFontCaptionsLabel = new Label { Text = "Подписи:", Location = new Point(20, currentY), Size = new Size(100, 20) };
                var commonFontCaptionsButton = new Button { Text = "Выбрать шрифт", Location = new Point(130, currentY), Size = new Size(150, 30) };
                commonFontCaptionsButton.Click += (s, e) =>
                {
                    if (bpElement.Awards.Count > 0 && bpElement.CaptionElements.Count > 0)
                    {
                        var firstCaption = bpElement.CaptionElements[0] as TextElement;
                        if (firstCaption != null)
                        {
                            using (var fontDialog = new FontDialog { Font = firstCaption.Font })
                            {
                                if (fontDialog.ShowDialog() == DialogResult.OK)
                                {
                                    foreach (var caption in bpElement.CaptionElements)
                                    {
                                        if (caption is TextElement capT)
                                        {
                                            capT.Font = fontDialog.Font;
                                            capT.FontName = fontDialog.Font.Name;
                                            capT.FontSize = fontDialog.Font.Size;
                                            capT.FontStyle = fontDialog.Font.Style;
                                        }
                                    }
                                }
                            }
                        }
                    }
                };
                scrollPanel.Controls.Add(commonFontCaptionsLabel);
                scrollPanel.Controls.Add(commonFontCaptionsButton);
                currentY += 40;
                // Общий цвет текста для всех наград
                var captionsTextColorLabel = new Label { Text = "Цвет подписей:", Location = new Point(20, currentY), Size = new Size(200, 20) };
                currentY += 30;
                var captionsTextColorButton = new Button { Text = "Выбрать цвет", Location = new Point(130, currentY), Size = new Size(200, 30) };

                if (bpElement.CaptionElements.Count > 0)
                {
                    captionsTextColorButton.BackColor = ((TextElement)(bpElement.CaptionElements[0])).Color;
                }
                captionsTextColorButton.Click += (s, e) =>
                {
                    if (bpElement.CaptionElements.Count > 0)
                    {
                        using (var colorDialog = new ColorDialog { Color = ((TextElement)(bpElement.CaptionElements[0])).Color })
                        {
                            if (colorDialog.ShowDialog() == DialogResult.OK)
                            {
                                foreach (var caption in bpElement.CaptionElements)
                                {
                                    if(caption is TextElement textElement)
                                        textElement.Color = colorDialog.Color;
                                }
                                captionsTextColorButton.BackColor = colorDialog.Color;
                            }
                        }
                    }
                };
                scrollPanel.Controls.Add(captionsTextColorLabel);
                scrollPanel.Controls.Add(captionsTextColorButton);
                currentY += 40;
                // Общий цвет текста для всех наград
                var commonTextColorLabel = new Label { Text = "Общий цвет текста:", Location = new Point(20, currentY), Size = new Size(200, 20) };
                currentY += 30;
                var commonTextColorButton = new Button { Text = "Выбрать цвет", Location = new Point(130, currentY), Size = new Size(200, 30) };
                if (bpElement.Awards.Count > 0)
                {
                    commonTextColorButton.BackColor = bpElement.Awards[0].TextColor;
                }
                commonTextColorButton.Click += (s, e) =>
                {
                    if (bpElement.Awards.Count > 0)
                    {
                        using (var colorDialog = new ColorDialog { Color = bpElement.Awards[0].TextColor })
                        {
                            if (colorDialog.ShowDialog() == DialogResult.OK)
                            {
                                foreach (var award in bpElement.Awards)
                                {
                                    award.TextColor = colorDialog.Color;
                                }
                                commonTextColorButton.BackColor = colorDialog.Color;
                            }
                        }
                    }
                };
                scrollPanel.Controls.Add(commonTextColorLabel);
                scrollPanel.Controls.Add(commonTextColorButton);
                currentY += 40;

                // Общая прозрачность текста для всех наград
                var commonTextOpacityLabel = new Label { Text = "Общая прозрачность текста:", Location = new Point(20, currentY), Size = new Size(140, 40) };
                currentY += 20;
                var commonTextOpacityTrackBar = new TrackBar
                {
                    Location = new Point(170, currentY),
                    Size = new Size(150, 40),
                    Minimum = 0,
                    Maximum = 100,
                    Value = bpElement.Awards.Count > 0 ? bpElement.Awards[0].TextOpacity : 100
                };
                var commonTextOpacityValueLabel = new Label
                {
                    Text = $"{(bpElement.Awards.Count > 0 ? bpElement.Awards[0].TextOpacity : 100)}%",
                    Location = new Point(330, currentY),
                    Size = new Size(40, 20)
                };

                commonTextOpacityTrackBar.ValueChanged += (s, e) =>
                {
                    int value = commonTextOpacityTrackBar.Value;
                    foreach (var award in bpElement.Awards)
                    {
                        award.TextOpacity = value;
                    }
                    commonTextOpacityValueLabel.Text = $"{value}%";
                };

                scrollPanel.Controls.Add(commonTextOpacityLabel);
                scrollPanel.Controls.Add(commonTextOpacityTrackBar);
                scrollPanel.Controls.Add(commonTextOpacityValueLabel);
                currentY += 60;

                // Общий цвет фона для всех наград
                var commonBgColorLabel = new Label { Text = "Общий цвет фона:", Location = new Point(20, currentY), Size = new Size(200, 20) };
                currentY += 30;
                var commonBgColorButton = new Button { Text = "Выбрать цвет", Location = new Point(130, currentY), Size = new Size(200, 30) };
                if (bpElement.Awards.Count > 0)
                {
                    commonBgColorButton.BackColor = bpElement.Awards[0].BackgroundColor;
                }
                commonBgColorButton.Click += (s, e) =>
                {
                    if (bpElement.Awards.Count > 0)
                    {
                        using (var colorDialog = new ColorDialog { Color = bpElement.Awards[0].BackgroundColor })
                        {
                            if (colorDialog.ShowDialog() == DialogResult.OK)
                            {
                                foreach (var award in bpElement.Awards)
                                {
                                    award.BackgroundColor = colorDialog.Color;
                                }
                                commonBgColorButton.BackColor = colorDialog.Color;
                            }
                        }
                    }
                };
                scrollPanel.Controls.Add(commonBgColorLabel);
                scrollPanel.Controls.Add(commonBgColorButton);
                currentY += 40;

                // Общий цвет рамки для всех наград
                var commonBorderColorLabel = new Label { Text = "Общий цвет рамки:", Location = new Point(20, currentY), Size = new Size(200, 20) };
                currentY += 30;
                var commonBorderColorButton = new Button { Text = "Выбрать цвет", Location = new Point(130, currentY), Size = new Size(200, 30) };
                if (bpElement.Awards.Count > 0)
                {
                    commonBorderColorButton.BackColor = bpElement.Awards[0].BorderColor;
                }
                commonBorderColorButton.Click += (s, e) =>
                {
                    if (bpElement.Awards.Count > 0)
                    {
                        using (var colorDialog = new ColorDialog { Color = bpElement.Awards[0].BorderColor })
                        {
                            if (colorDialog.ShowDialog() == DialogResult.OK)
                            {
                                foreach (var award in bpElement.Awards)
                                {
                                    award.BorderColor = colorDialog.Color;
                                }
                                commonBorderColorButton.BackColor = colorDialog.Color;
                            }
                        }
                    }
                };
                scrollPanel.Controls.Add(commonBorderColorLabel);
                scrollPanel.Controls.Add(commonBorderColorButton);
                currentY += 40;

                // Общий цвет стрелки для всех наград
                var commonArrowColorLabel = new Label { Text = "Стрелка:", Location = new Point(20, currentY), Size = new Size(100, 20) };
                var commonArrowColorButton = new Button { Text = "Выбрать цвет", Location = new Point(130, currentY), Size = new Size(150, 30) };
                if (bpElement.Awards.Count > 0)
                {
                    commonArrowColorButton.BackColor = bpElement.Awards[0].ArrowColor;
                }
                commonArrowColorButton.Click += (s, e) =>
                {
                    if (bpElement.Awards.Count > 0)
                    {
                        using (var colorDialog = new ColorDialog { Color = bpElement.Awards[0].ArrowColor })
                        {
                            if (colorDialog.ShowDialog() == DialogResult.OK)
                            {
                                foreach (var award in bpElement.Awards)
                                {
                                    award.ArrowColor = colorDialog.Color;
                                }
                                commonArrowColorButton.BackColor = colorDialog.Color;
                            }
                        }
                    }
                };
                scrollPanel.Controls.Add(commonArrowColorLabel);
                scrollPanel.Controls.Add(commonArrowColorButton);
                currentY += 40;

                // Общая ширина рамки для всех наград
                var commonBorderWidthLabel = new Label { Text = "Общая ширина рамки:", Location = new Point(20, currentY), Size = new Size(120, 20) };
                var commonBorderWidthNumeric = new NumericUpDown
                {
                    Location = new Point(150, currentY),
                    Size = new Size(60, 20),
                    Minimum = 0,
                    Maximum = 20,
                    Value = bpElement.Awards.Count > 0 ? bpElement.Awards[0].BorderWidth : 3
                };
                commonBorderWidthNumeric.ValueChanged += (s, e) =>
                {
                    foreach (var award in bpElement.Awards)
                    {
                        award.BorderWidth = (int)commonBorderWidthNumeric.Value;
                    }
                };
                scrollPanel.Controls.Add(commonBorderWidthLabel);
                scrollPanel.Controls.Add(commonBorderWidthNumeric);
                currentY += 30;

                // Настройки отступов
                var spacingGroupLabel = new Label
                {
                    Text = "Настройки отступов:",
                    Location = new Point(20, currentY),
                    Size = new Size(200, 20),
                    Font = new Font("Arial", 9, FontStyle.Bold)
                };
                scrollPanel.Controls.Add(spacingGroupLabel);
                currentY += 25;

                // Горизонтальный отступ
                var horizontalSpacingLabel = new Label { Text = "Горизонтальный отступ:", Location = new Point(20, currentY), Size = new Size(150, 20) };
                var horizontalSpacingNumeric = new NumericUpDown
                {
                    Location = new Point(180, currentY),
                    Size = new Size(60, 20),
                    Minimum = 0,
                    Maximum = 100,
                    Value = bpElement.HorizontalSpacing
                };
                horizontalSpacingNumeric.ValueChanged += (s, e) => bpElement.HorizontalSpacing = (int)horizontalSpacingNumeric.Value;
                scrollPanel.Controls.Add(horizontalSpacingLabel);
                scrollPanel.Controls.Add(horizontalSpacingNumeric);
                currentY += 30;

                // Вертикальный отступ
                var verticalSpacingLabel = new Label { Text = "Вертикальный отступ:", Location = new Point(20, currentY), Size = new Size(150, 20) };
                var verticalSpacingNumeric = new NumericUpDown
                {
                    Location = new Point(180, currentY),
                    Size = new Size(60, 20),
                    Minimum = 0,
                    Maximum = 100,
                    Value = bpElement.VerticalSpacing
                };
                verticalSpacingNumeric.ValueChanged += (s, e) => bpElement.VerticalSpacing = (int)verticalSpacingNumeric.Value;
                scrollPanel.Controls.Add(verticalSpacingLabel);
                scrollPanel.Controls.Add(verticalSpacingNumeric);
                currentY += 40;

                // Показывать подписи
                var showCaptionsCheckBox = new CheckBox
                {
                    Text = "Показывать подписи",
                    Location = new Point(20, currentY),
                    Size = new Size(250, 30),
                    Checked = bpElement.ShowCaptions
                };
                showCaptionsCheckBox.CheckedChanged += (s, e) =>
                {
                    bpElement.ShowCaptions = showCaptionsCheckBox.Checked;
                    if (bpElement.ShowCaptions && bpElement.CaptionElements.Count == 0)
                    {
                        bpElement.InitializeCaptions();
                    }
                };
                scrollPanel.Controls.Add(showCaptionsCheckBox);
                currentY += 35;
                // Показывать вторую строку подписей
                var showSecondLineCheckBox = new CheckBox
                {
                    Text = "Показывать вторую строку подписей",
                    Location = new Point(20, currentY),
                    Size = new Size(250, 30),
                    Checked = bpElement.ShowSecondCaptionLine
                };
                showSecondLineCheckBox.CheckedChanged += (s, e) =>
                {
                    bpElement.ShowSecondCaptionLine = showSecondLineCheckBox.Checked;
                    // Переинициализируем подписи при изменении опции
                    if (bpElement.ShowCaptions)
                    {
                        bpElement.InitializeCaptions();
                    }
                };
                scrollPanel.Controls.Add(showSecondLineCheckBox);
                currentY += 35;
                var firstIconChk = new CheckBox
                {
                    Text = "Иконка в первой строке",
                    Location = new Point(20, currentY),
                    Size = new Size(250, 30),
                    Checked = bpElement.ShowFirstLineIcon
                };
                firstIconChk.CheckedChanged += (_, __) => bpElement.ShowFirstLineIcon = firstIconChk.Checked;
                scrollPanel.Controls.Add(firstIconChk);
                currentY += 30;

                var secondIconChk = new CheckBox
                {
                    Text = "Иконка во второй строке",
                    Location = new Point(20, currentY),
                    Size = new Size(250, 30),
                    Checked = bpElement.ShowSecondLineIcon
                };
                secondIconChk.CheckedChanged += (_, __) => bpElement.ShowSecondLineIcon = secondIconChk.Checked;
                scrollPanel.Controls.Add(secondIconChk);
                currentY += 30;
                // Отступ между элементами подписей
                var spacingLabel = new Label { Text = "Отступ между элементами:", Location = new Point(20, currentY), Size = new Size(200, 20) };
                var spacingNumeric = new NumericUpDown
                {
                    Location = new Point(220, currentY),
                    Size = new Size(60, 20),
                    Minimum = 1,
                    Maximum = 200,
                    Value = bpElement.CaptionSpacing
                };
                spacingNumeric.ValueChanged += (s, e) => bpElement.CaptionSpacing = (int)spacingNumeric.Value;
                scrollPanel.Controls.Add(spacingLabel);
                scrollPanel.Controls.Add(spacingNumeric);
                currentY += 30;
                // Общая ширина рамки для всех наград
                var captionLineSpacing = new Label { Text = "Отступ между линиями:", Location = new Point(20, currentY), Size = new Size(200, 20) };
                var captionLineSpacingNemeric = new NumericUpDown
                {
                    Location = new Point(220, currentY),
                    Size = new Size(60, 20),
                    Minimum = 0,
                    Maximum = 20,
                    Value = bpElement.CaptionLineSpacing
                };
                captionLineSpacingNemeric.ValueChanged += (s, e) =>
                {
                    bpElement.CaptionLineSpacing = (int)captionLineSpacingNemeric.Value;
                };
                scrollPanel.Controls.Add(captionLineSpacing);
                scrollPanel.Controls.Add(captionLineSpacingNemeric);
                currentY += 30;


                // После настройки отступов добавляем высоту подписей
                var captionHeightLabel = new Label { Text = "Высота подписей:", Location = new Point(20, currentY), Size = new Size(150, 20) };
                var captionHeightNumeric = new NumericUpDown
                {
                    Location = new Point(180, currentY),
                    Size = new Size(60, 20),
                    Minimum = 20,
                    Maximum = 200,
                    Value = bpElement.CaptionHeight
                };
                captionHeightNumeric.ValueChanged += (s, e) => bpElement.CaptionHeight = (int)captionHeightNumeric.Value;
                scrollPanel.Controls.Add(captionHeightLabel);
                scrollPanel.Controls.Add(captionHeightNumeric);
                currentY += 30;
                // Редактирование наград
                var awardsLabel = new Label { Text = "Награды:", Location = new Point(20, currentY), Font = new Font("Arial", 9, FontStyle.Bold) };
                scrollPanel.Controls.Add(awardsLabel);
                currentY += 25;

                for (int i = 0; i < bpElement.Awards.Count; i++)
                {
                    var award = bpElement.Awards[i];
                    var awardLabel = new Label { Text = $"Награда {i + 1}:", Location = new Point(20, currentY) };
                    var editButton = new Button { Text = "Редактировать", Location = new Point(120, currentY), Size = new Size(100, 25), Tag = award };
                    editButton.Click += (s, e) => ShowAwardProperties((AwardElement)((Button)s).Tag);

                    scrollPanel.Controls.Add(awardLabel);
                    scrollPanel.Controls.Add(editButton);
                    currentY += 30;
                }

                // Кнопка добавления награды
                var addAwardButton = new Button { Text = "Добавить награду", Location = new Point(20, currentY), Size = new Size(120, 30) };
                addAwardButton.Click += (s, e) =>
                {
                    bpElement.AddAward(); // Используем новый метод

                    // Обновляем форму
                    scrollPanel.Controls.Clear();
                    AddBasicControls();
                    AddSpecificControls();
                    scrollPanel.Invalidate();
                };
                scrollPanel.Controls.Add(addAwardButton);
                currentY += 40;

                // Кнопка удаления последней награды
                var removeAwardButton = new Button { Text = "Удалить награду", Location = new Point(150, currentY - 40), Size = new Size(120, 30) };
                removeAwardButton.Click += (s, e) =>
                {
                    if (bpElement.Awards.Count > 0)
                    {
                        bpElement.RemoveLastAward(); // Используем новый метод

                        // Обновляем форму
                        scrollPanel.Controls.Clear();
                        AddBasicControls();
                        AddSpecificControls();
                        scrollPanel.Invalidate();
                    }
                };
                scrollPanel.Controls.Add(removeAwardButton);
                currentY += 10;

                // Редактирование элементов подписей
                if (bpElement.ShowCaptions)
                {

                    var firstLineImageTypeLabel = new Label { Text = "Иконка (1-я строка):", Location = new Point(40, currentY), Size = new Size(150, 20) };
                    var firstLineImageTypeComboBox = new ComboBox { Location = new Point(190, currentY), Size = new Size(150, 20), DropDownStyle = ComboBoxStyle.DropDownList };
                    firstLineImageTypeComboBox.Items.AddRange(Enum.GetNames(typeof(AwardImageType)));
                    // Устанавливаем текущий тип (берём у первого элемента первой строки)
                    if (bpElement.CaptionElements.Count > 1 && bpElement.CaptionElements[1] is ImageElement firstIcon)
                    {
                        var currentType = firstIcon.ImageType.ToString();
                        if (firstLineImageTypeComboBox.Items.Contains(currentType))
                            firstLineImageTypeComboBox.SelectedItem = currentType;
                    }

                    firstLineImageTypeComboBox.SelectedIndexChanged += (s, e) =>
                    {
                        var combo = (ComboBox)s;
                        if (combo.SelectedItem == null) return;
                        var newType = (AwardImageType)Enum.Parse(typeof(AwardImageType), combo.SelectedItem.ToString());
                        Console.WriteLine(newType.ToString());
                        string newPath = newType == AwardImageType.Custom ? "" : GetImagePathByType(newType);

                        // Применяем ТОЛЬКО к иконкам первой строки
                        for (int i = 1; i < bpElement.CaptionElements.Count; i += 4)
                        {
                            if (bpElement.CaptionElements[i] is ImageElement img)
                            {
                                img.ImageType = newType;
                                if (newType != AwardImageType.Custom)
                                    img.ImagePath = newPath;
                            }
                        }
                    };
                    scrollPanel.Controls.Add(firstLineImageTypeLabel);
                    scrollPanel.Controls.Add(firstLineImageTypeComboBox);
                    currentY += 30;
                    var firstLineLoadButton = new Button { Text = "Загрузить иконку (1-я)", Location = new Point(190, currentY), Size = new Size(150, 25) };
                    firstLineLoadButton.Click += (s, e) =>
                    {
                        using (var dialog = new OpenFileDialog())
                        {
                            dialog.Filter = "Изображения|*.png;*.jpg;*.jpeg;*.bmp|Все файлы|*.*";
                            if (dialog.ShowDialog() == DialogResult.OK)
                            {
                                string path = dialog.FileName;
                                for (int i = 1; i < bpElement.CaptionElements.Count; i += 4)
                                {
                                    if (bpElement.CaptionElements[i] is ImageElement img)
                                    {
                                        img.ImageType = AwardImageType.Custom;
                                        img.ImagePath = path;
                                    }
                                }
                                // Сбрасываем ComboBox на Custom
                                if (firstLineImageTypeComboBox.Items.Contains(AwardImageType.Custom.ToString()))
                                    firstLineImageTypeComboBox.SelectedItem = AwardImageType.Custom.ToString();
                            }
                        }
                    };
                    scrollPanel.Controls.Add(firstLineLoadButton);
                    currentY += 30;
                    var firstLineScaleLabel = new Label { Text = "Масштаб (1-я строка):", Location = new Point(40, currentY), Size = new Size(150, 20) };
                    var firstLineScaleComboBox = new ComboBox { Location = new Point(190, currentY), Size = new Size(150, 20), DropDownStyle = ComboBoxStyle.DropDownList };
                    firstLineScaleComboBox.Items.AddRange(Enum.GetNames(typeof(ImageScaleType)));
                    // Берём значение у первого элемента первой строки
                    if (bpElement.CaptionElements.Count > 1 && bpElement.CaptionElements[1] is ImageElement firstImg)
                    {
                        firstLineScaleComboBox.SelectedItem = firstImg.ImageScaleType.ToString();
                    }

                    firstLineScaleComboBox.SelectedIndexChanged += (s, e) =>
                    {
                        if (firstLineScaleComboBox.SelectedItem == null) return;
                        var scaleType = (ImageScaleType)Enum.Parse(typeof(ImageScaleType), firstLineScaleComboBox.SelectedItem.ToString());
                        for (int i = 1; i < bpElement.CaptionElements.Count; i += 4)
                        {
                            if (bpElement.CaptionElements[i] is ImageElement img)
                            {
                                img.ImageScaleType = scaleType;
                            }
                        }
                    };
                    scrollPanel.Controls.Add(firstLineScaleLabel);
                    scrollPanel.Controls.Add(firstLineScaleComboBox);
                    currentY += 30;
                    var firstLineSizeLabel = new Label { Text = "Размер иконки (1-я):", Location = new Point(40, currentY), Size = new Size(150, 20) };
                    var firstLineSizeNumeric = new NumericUpDown
                    {
                        Location = new Point(190, currentY),
                        Size = new Size(60, 20),
                        Minimum = 8,
                        Maximum = 64,
                        Value = bpElement.CaptionIconSize1 // ← используем уже существующее свойство BPElement
                    };
                    firstLineSizeNumeric.ValueChanged += (s, e) =>
                    {
                        int size = (int)firstLineSizeNumeric.Value;
                        bpElement.CaptionIconSize1 = size;
                        for (int i = 1; i < bpElement.CaptionElements.Count; i += 4)
                        {
                            if (bpElement.CaptionElements[i] is ImageElement img)
                            {
                                img.Width = size;
                                img.Height = size;
                            }
                        }
                    };
                    scrollPanel.Controls.Add(firstLineSizeLabel);
                    scrollPanel.Controls.Add(firstLineSizeNumeric);
                    currentY += 30;

                    var secondLineImageTypeLabel = new Label { Text = "Иконка (2-я строка):", Location = new Point(40, currentY), Size = new Size(150, 20) };
                    var secondLineImageTypeComboBox = new ComboBox { Location = new Point(190, currentY), Size = new Size(150, 20), DropDownStyle = ComboBoxStyle.DropDownList };
                    secondLineImageTypeComboBox.Items.AddRange(Enum.GetNames(typeof(AwardImageType)));
                    // Устанавливаем текущий тип (берём у первого элемента первой строки)
                    if (bpElement.CaptionElements.Count > 3 && bpElement.CaptionElements[3] is ImageElement secondIcon)
                    {
                        var currentType = secondIcon.ImageType.ToString();
                        if (secondLineImageTypeComboBox.Items.Contains(currentType))
                            secondLineImageTypeComboBox.SelectedItem = currentType;
                    }

                    secondLineImageTypeComboBox.SelectedIndexChanged += (s, e) =>
                    {
                        var combo = (ComboBox)s;
                        if (combo.SelectedItem == null) return;
                        var newType = (AwardImageType)Enum.Parse(typeof(AwardImageType), combo.SelectedItem.ToString());
						Console.WriteLine(newType.ToString());
						string newPath = newType == AwardImageType.Custom ? "" : GetImagePathByType(newType);

                        // Применяем ТОЛЬКО к иконкам первой строки
                        for (int i = 3; i < bpElement.CaptionElements.Count; i += 4)
                        {
                            if (bpElement.CaptionElements[i] is ImageElement img)
                            {
                                img.ImageType = newType;
                                if (newType != AwardImageType.Custom)
                                    img.ImagePath = newPath;
                            }
                        }
                    };
                    scrollPanel.Controls.Add(secondLineImageTypeLabel);
                    scrollPanel.Controls.Add(secondLineImageTypeComboBox);
                    currentY += 30;
                    var secondLineLoadButton = new Button { Text = "Загрузить иконку (2-я)", Location = new Point(190, currentY), Size = new Size(150, 25) };
                    secondLineLoadButton.Click += (s, e) =>
                    {
                        using (var dialog = new OpenFileDialog())
                        {
                            dialog.Filter = "Изображения|*.png;*.jpg;*.jpeg;*.bmp|Все файлы|*.*";
                            if (dialog.ShowDialog() == DialogResult.OK)
                            {
                                string path = dialog.FileName;
                                for (int i = 3; i < bpElement.CaptionElements.Count; i += 4)
                                {
                                    if (bpElement.CaptionElements[i] is ImageElement img)
                                    {
                                        img.ImageType = AwardImageType.Custom;
                                        img.ImagePath = path;
                                    }
                                }
                                if (secondLineImageTypeComboBox.Items.Contains(AwardImageType.Custom.ToString()))
                                    secondLineImageTypeComboBox.SelectedItem = AwardImageType.Custom.ToString();
                            }
                        }
                    };
                    scrollPanel.Controls.Add(secondLineLoadButton);
                    currentY += 30;
                    var secondLineScaleLabel = new Label { Text = "Масштаб (2-я строка):", Location = new Point(40, currentY), Size = new Size(150, 20) };
                    var secondLineScaleComboBox = new ComboBox { Location = new Point(190, currentY), Size = new Size(150, 20), DropDownStyle = ComboBoxStyle.DropDownList };
                    secondLineScaleComboBox.Items.AddRange(Enum.GetNames(typeof(ImageScaleType)));
                    if (bpElement.CaptionElements.Count > 3 && bpElement.CaptionElements[3] is ImageElement secondImg)
                    {
                        secondLineScaleComboBox.SelectedItem = secondImg.ImageScaleType.ToString();
                    }

                    secondLineScaleComboBox.SelectedIndexChanged += (s, e) =>
                    {
                        if (secondLineScaleComboBox.SelectedItem == null) return;
                        var scaleType = (ImageScaleType)Enum.Parse(typeof(ImageScaleType), secondLineScaleComboBox.SelectedItem.ToString());
                        for (int i = 3; i < bpElement.CaptionElements.Count; i += 4)
                        {
                            if (bpElement.CaptionElements[i] is ImageElement img)
                            {
                                img.ImageScaleType = scaleType;
                            }
                        }
                    };
                    scrollPanel.Controls.Add(secondLineScaleLabel);
                    scrollPanel.Controls.Add(secondLineScaleComboBox);
                    currentY += 30;
                    var secondLineSizeLabel = new Label { Text = "Размер иконки (2-я):", Location = new Point(40, currentY), Size = new Size(150, 20) };
                    var secondLineSizeNumeric = new NumericUpDown
                    {
                        Location = new Point(190, currentY),
                        Size = new Size(60, 20),
                        Minimum = 8,
                        Maximum = 64,
                        Value = bpElement.CaptionIconSize2 // ← можно разделить, но пока удобно общий
                    };
                    secondLineSizeNumeric.ValueChanged += (s, e) =>
                    {
                        int size = (int)secondLineSizeNumeric.Value;
                        bpElement.CaptionIconSize2 = size;
                        for (int i = 3; i < bpElement.CaptionElements.Count; i += 4)
                        {
                            if (bpElement.CaptionElements[i] is ImageElement img)
                            {
                                img.Width = size;
                                img.Height = size;
                            }
                        }
                    };
                    scrollPanel.Controls.Add(secondLineSizeLabel);
                    scrollPanel.Controls.Add(secondLineSizeNumeric);
                    currentY += 30;
                    var captionsLabel = new Label
                    {
                        Text = "Элементы подписей:",
                        Location = new Point(20, currentY),
                        Font = new Font("Arial", 9, FontStyle.Bold)
                    };
                    scrollPanel.Controls.Add(captionsLabel);
                    currentY += 25;

                    for (int i = 0; i < bpElement.CaptionElements.Count; i++)
                    {
                        var captionElement = bpElement.CaptionElements[i];
                        var elementLabel = new Label
                        {
                            Text = $"{captionElement.Name}:",
                            Location = new Point(20, currentY),
                            Size = new Size(250, 25),
                        };
                        var editButton = new Button
                        {
                            Text = "Редактировать",
                            Location = new Point(270, currentY),
                            Size = new Size(100, 25),
                            Tag = captionElement
                        };
                        editButton.Click += (s, e) => ShowElementProperties((BaseElement)((Button)s).Tag);

                        scrollPanel.Controls.Add(elementLabel);
                        scrollPanel.Controls.Add(editButton);
                        currentY += 30;
                    }
                }
            }
            else if (element is AwardElement awardElement)
            {
                // Текст награды
                var textLabel = new Label { Text = "Текст:", Location = new Point(20, currentY), Size = new Size(100, 20) };
                var textTextBox = new TextBox { Text = awardElement.Text, Location = new Point(130, currentY), Size = new Size(200, 20) };
                textTextBox.TextChanged += (s, e) => awardElement.Text = textTextBox.Text;
                scrollPanel.Controls.Add(textLabel);
                scrollPanel.Controls.Add(textTextBox);
                currentY += 40;

                // Тип изображения
                var imageTypeLabel = new Label { Text = "Тип изображения:", Location = new Point(20, currentY), Size = new Size(100, 20) };
                var imageTypeComboBox = new ComboBox { Location = new Point(130, currentY), Size = new Size(150, 20), DropDownStyle = ComboBoxStyle.DropDownList };
                imageTypeComboBox.Items.AddRange(Enum.GetNames(typeof(AwardImageType)));
                // Устанавливаем выбранный элемент безопасно
                var selectedType = awardElement.ImageType.ToString();
                if (imageTypeComboBox.Items.Contains(selectedType))
                {
                    imageTypeComboBox.SelectedItem = selectedType;
                }
                else if (imageTypeComboBox.Items.Count > 0)
                {
                    imageTypeComboBox.SelectedIndex = 0;
                }
                imageTypeComboBox.SelectedIndexChanged += (s, e) =>
                {
                    var combo = s as ComboBox;
                    if (combo.SelectedItem != null)
                    {
                        awardElement.ImageType = (AwardImageType)Enum.Parse(typeof(AwardImageType), combo.SelectedItem.ToString());
                        if (awardElement.ImageType != AwardImageType.Custom)
                        {    
                            switch (awardElement.ImageType)
                            { 
                                 case AwardImageType.Points:
                                    awardElement.ImagePath = "Images/Awards/Points.png";
                                    break;
                                case AwardImageType.Experience:
                                    awardElement.ImagePath = "Images/Awards/Experince.png";
                                    break;
                                case AwardImageType.Spheres:
                                    awardElement.ImagePath = "Images/Awards/Spheres.png";
                                    break;
                                case AwardImageType.SSRTicket:
                                    awardElement.ImagePath = "Images/Awards/SSRTicket.png";
                                    break;
                                case AwardImageType.BossMedals:
                                    awardElement.ImagePath = "Images/Awards/BossMedals.png";
                                    break;
                                case AwardImageType.Ideas:
                                    awardElement.ImagePath = "Images/Awards/Idea.png";
                                    break;
                                case AwardImageType.Boxes:
                                    awardElement.ImagePath = "Images/Awards/Boxes.png";
                                    break;
                                case AwardImageType.Backpacks:
                                    awardElement.ImagePath = "Images/Awards/Backpack.png";
                                    break;
                                case AwardImageType.Bags:
                                    awardElement.ImagePath = "Images/Awards/Bags.png";
                                    break;
                                case AwardImageType.EpicBoxes:
                                    awardElement.ImagePath = "Images/Awards/EpicBoxes.png";
                                    break;
                                case AwardImageType.LegendBoxes:
                                    awardElement.ImagePath = "Images/Awards/LegendBoxes.png";
                                    break;
                                case AwardImageType.Glazgo:
                                    awardElement.ImagePath = "Images/Awards/Glazgo.png";
                                    break;
                                case AwardImageType.Burai:
                                    awardElement.ImagePath = "Images/Awards/Burai.png";
                                    break;
                                case AwardImageType.NightPolice:
                                    awardElement.ImagePath = "Images/Awards/KnightPolice.png";
                                    break;
                                case AwardImageType.Sazerlend:
                                    awardElement.ImagePath = "Images/Awards/Sazerland.png";
                                    break;
                                case AwardImageType.Gloster:
                                    awardElement.ImagePath = "Images/Awards/Gloster.png";
                                    break;
                                case AwardImageType.Gekka:
                                    awardElement.ImagePath = "Images/Awards/Gekka.png";
                                    break;
                                case AwardImageType.Akazuki:
                                    awardElement.ImagePath = "Images/Awards/Akazuki.png";
                                    break;
                                case AwardImageType.Vincent:
                                    awardElement.ImagePath = "Images/Awards/Vincent.png";
                                    break;
                                case AwardImageType.Diamonds:
                                    awardElement.ImagePath = "Images/Awards/diamond.png";
                                    break;
                                case AwardImageType.Gold:
                                    awardElement.ImagePath = "Images/Awards/gold.png";
                                    break;
                                case AwardImageType.Nightmares:
                                    awardElement.ImagePath = "Images/Awards/Nightmares.png";
                                    break;
                                case AwardImageType.Lancelot:
                                    awardElement.ImagePath = "Images/Awards/Lancelot.png";
                                    break;
                                case AwardImageType.Raid:
                                    awardElement.ImagePath = "Images/Awards/Raid.png";
                                    break;
                                case AwardImageType.Sakuradite:
                                    awardElement.ImagePath = "Images/Awards/Sakuradite.png";
                                    break;
                                case AwardImageType.SealGold:
                                    awardElement.ImagePath = "Images/Awards/SealGold.png";
                                    break;
                                case AwardImageType.SealLove:
                                    awardElement.ImagePath = "Images/Awards/SealLove.png";
                                    break;
                                case AwardImageType.SealSpace:
                                    awardElement.ImagePath = "Images/Awards/SealSpace.png";
                                    break;
                                case AwardImageType.SealSpiritual:
                                    awardElement.ImagePath = "Images/Awards/SealSpiritual.png";
                                    break;
                                case AwardImageType.SealTerrible:
                                    awardElement.ImagePath = "Images/Awards/SealTerrible.png";
                                    break;
                                case AwardImageType.SoulFragments:
                                    awardElement.ImagePath = "Images/Awards/SoulFragments.png";
                                    break;
								case AwardImageType.Books:
									awardElement.ImagePath = "Images/Awards/Books.png";
									break;
							}
                            
                        }
                    }
                };
                scrollPanel.Controls.Add(imageTypeLabel);
                scrollPanel.Controls.Add(imageTypeComboBox);
                currentY += 40;

                // Кнопка загрузки изображения
                var loadImageLabel = new Label { Text = "Изображение:", Location = new Point(20, currentY), Size = new Size(100, 20) };
                var loadImageButton = new Button { Text = "Загрузить", Location = new Point(130, currentY), Size = new Size(100, 30) };
                loadImageButton.Click += (s, e) =>
                {
                    using (var dialog = new OpenFileDialog())
                    {
                        dialog.Filter = "Изображения|*.jpg;*.jpeg;*.png;*.bmp|Все файлы|*.*";
                        if (dialog.ShowDialog() == DialogResult.OK)
                        {
                            awardElement.ImagePath = dialog.FileName;
                            awardElement.ImageType = AwardImageType.Custom;
                            // Обновляем ComboBox
                            if (imageTypeComboBox.Items.Contains(AwardImageType.Custom.ToString()))
                            {
                                imageTypeComboBox.SelectedItem = AwardImageType.Custom.ToString();
                            }
                        }
                    }
                };
                scrollPanel.Controls.Add(loadImageLabel);
                scrollPanel.Controls.Add(loadImageButton);
                currentY += 40;
                // Прозрачность текста
                var textOpacityLabel = new Label { Text = "Прозрачность текста:", Location = new Point(20, currentY), Size = new Size(120, 20) };
                var textOpacityTrackBar = new TrackBar
                {
                    Location = new Point(150, currentY),
                    Size = new Size(150, 40),
                    Minimum = 0,
                    Maximum = 100,
                    Value = awardElement.TextOpacity
                };
                var textOpacityValueLabel = new Label { Text = $"{awardElement.TextOpacity}%", Location = new Point(310, currentY), Size = new Size(40, 20) };

                textOpacityTrackBar.ValueChanged += (s, e) =>
                {
                    awardElement.TextOpacity = textOpacityTrackBar.Value;
                    textOpacityValueLabel.Text = $"{textOpacityTrackBar.Value}%";
                };

                scrollPanel.Controls.Add(textOpacityLabel);
                scrollPanel.Controls.Add(textOpacityTrackBar);
                scrollPanel.Controls.Add(textOpacityValueLabel);
                currentY += 55;
                // Цвет текста
                var textColorLabel = new Label { Text = "Цвет текста:", Location = new Point(20, currentY), Size = new Size(100, 20) };
                var textColorButton = new Button { Text = "Выбрать", Location = new Point(130, currentY), Size = new Size(100, 30) };
                textColorButton.BackColor = awardElement.TextColor;
                textColorButton.Click += (s, e) =>
                {
                    using (var colorDialog = new ColorDialog { Color = awardElement.TextColor })
                    {
                        if (colorDialog.ShowDialog() == DialogResult.OK)
                        {
                            awardElement.TextColor = colorDialog.Color;
                            textColorButton.BackColor = colorDialog.Color;
                        }
                    }
                };
                scrollPanel.Controls.Add(textColorLabel);
                scrollPanel.Controls.Add(textColorButton);
                currentY += 40;
                                // Цвет фона
                var bgColorLabel = new Label { Text = "Цвет фона:", Location = new Point(20, currentY), Size = new Size(100, 20) };
                var bgColorButton = new Button { Text = "Выбрать", Location = new Point(130, currentY), Size = new Size(100, 30) };
                bgColorButton.BackColor = awardElement.BackgroundColor;
                bgColorButton.Click += (s, e) =>
                {
                    using (var colorDialog = new ColorDialog { Color = awardElement.BackgroundColor })
                    {
                        if (colorDialog.ShowDialog() == DialogResult.OK)
                        {
                            awardElement.BackgroundColor = colorDialog.Color;
                            bgColorButton.BackColor = colorDialog.Color;
                        }
                    }
                };
                scrollPanel.Controls.Add(bgColorLabel);
                scrollPanel.Controls.Add(bgColorButton);
                currentY += 40;
                // Цвет фона
                var arrowColorLabel = new Label { Text = "Стрелка:", Location = new Point(20, currentY), Size = new Size(100, 20) };
                var arrowColorButton = new Button { Text = "Выбрать", Location = new Point(130, currentY), Size = new Size(100, 30) };
                arrowColorButton.BackColor = awardElement.ArrowColor;
                arrowColorButton.Click += (s, e) =>
                {
                    using (var colorDialog = new ColorDialog { Color = awardElement.ArrowColor })
                    {
                        if (colorDialog.ShowDialog() == DialogResult.OK)
                        {
                            awardElement.ArrowColor = colorDialog.Color;
                            arrowColorButton.BackColor = colorDialog.Color;
                        }
                    }
                };
                scrollPanel.Controls.Add(arrowColorLabel);
                scrollPanel.Controls.Add(arrowColorButton);
                currentY += 40;
                // Цвет рамки
                var borderColorLabel = new Label { Text = "Цвет рамки:", Location = new Point(20, currentY), Size = new Size(100, 20) };
                var borderColorButton = new Button { Text = "Выбрать", Location = new Point(130, currentY), Size = new Size(100, 30) };
                borderColorButton.BackColor = awardElement.BorderColor;
                borderColorButton.Click += (s, e) =>
                {
                    using (var colorDialog = new ColorDialog { Color = awardElement.BorderColor })
                    {
                        if (colorDialog.ShowDialog() == DialogResult.OK)
                        {
                            awardElement.BorderColor = colorDialog.Color;
                            borderColorButton.BackColor = colorDialog.Color;
                        }
                    }
                };
                scrollPanel.Controls.Add(borderColorLabel);
                scrollPanel.Controls.Add(borderColorButton);
                currentY += 40;


                // Тип масштабирования изображения
                var scaleTypeLabel = new Label { Text = "Масштаб изображения:", Location = new Point(20, currentY), Size = new Size(120, 20) };
                var scaleTypeComboBox = new ComboBox { Location = new Point(150, currentY), Size = new Size(150, 20), DropDownStyle = ComboBoxStyle.DropDownList };
                scaleTypeComboBox.Items.AddRange(Enum.GetNames(typeof(ImageScaleType)));
                scaleTypeComboBox.SelectedItem = awardElement.ImageScaleType.ToString();
                scaleTypeComboBox.SelectedIndexChanged += (s, e) =>
                {
                    awardElement.ImageScaleType = (ImageScaleType)Enum.Parse(typeof(ImageScaleType), scaleTypeComboBox.SelectedItem.ToString());
                };
                scrollPanel.Controls.Add(scaleTypeLabel);
                scrollPanel.Controls.Add(scaleTypeComboBox);
                currentY += 40;
                // Шрифт
                var fontLabel = new Label { Text = "Шрифт:", Location = new Point(20, currentY), Size = new Size(100, 20) };
                var fontButton = new Button { Text = "Выбрать шрифт", Location = new Point(130, currentY), Size = new Size(150, 30) };
                fontButton.Click += (s, e) =>
                {
                    using (var fontDialog = new FontDialog { Font = awardElement.Font })
                    {
                        if (fontDialog.ShowDialog() == DialogResult.OK)
                        {
                            awardElement.Font = fontDialog.Font;
                            awardElement.FontName = fontDialog.Font.Name;
                            awardElement.FontSize = fontDialog.Font.Size;
                            awardElement.FontStyle = fontDialog.Font.Style;
                        }
                    }
                };
                scrollPanel.Controls.Add(fontLabel);
                scrollPanel.Controls.Add(fontButton);
                currentY += 40;

                // Показывать стрелку
                var showArrowCheckBox = new CheckBox
                {
                    Text = "Показывать стрелку",
                    Location = new Point(20, currentY),
                    Size = new Size(150, 20),
                    Checked = awardElement.ShowArrow
                };
                showArrowCheckBox.CheckedChanged += (s, e) => awardElement.ShowArrow = showArrowCheckBox.Checked;
                scrollPanel.Controls.Add(showArrowCheckBox);
                currentY += 30;

                // Направление стрелки
                var arrowDirectionLabel = new Label { Text = "Направление:", Location = new Point(20, currentY), Size = new Size(100, 20) };
                var arrowDirectionComboBox = new ComboBox { Location = new Point(130, currentY), Size = new Size(150, 20), DropDownStyle = ComboBoxStyle.DropDownList };
                arrowDirectionComboBox.Items.AddRange(Enum.GetNames(typeof(ArrowDirection)));
                // Устанавливаем выбранный элемент безопасно
                var selectedDirection = awardElement.ArrowDirection.ToString();
                if (arrowDirectionComboBox.Items.Contains(selectedDirection))
                {
                    arrowDirectionComboBox.SelectedItem = selectedDirection;
                }
                else if (arrowDirectionComboBox.Items.Count > 0)
                {
                    arrowDirectionComboBox.SelectedIndex = 0;
                }
                arrowDirectionComboBox.SelectedIndexChanged += (s, e) =>
                {
                    var combo = s as ComboBox;
                    if (combo.SelectedItem != null)
                    {
                        awardElement.ArrowDirection = (ArrowDirection)Enum.Parse(typeof(ArrowDirection), combo.SelectedItem.ToString());
                    }
                };
                scrollPanel.Controls.Add(arrowDirectionLabel);
                scrollPanel.Controls.Add(arrowDirectionComboBox);
            }
            if(currentY>scrollPanel.Height) scrollPanel.Height = currentY+200;
        }

        private void AddImageButtonToRow(ImageRowElement imageRow, Panel imagesPanel)
        {
            using (var dialog = new OpenFileDialog())
            {
                dialog.Filter = "Изображения|*.jpg;*.jpeg;*.png;*.bmp|Все файлы|*.*";
                dialog.Multiselect = true;
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    // Добавляем выбранные изображения к существующим
                    imageRow.ImagePaths.AddRange(dialog.FileNames);
                    // Обновляем миниатюры
                    UpdateImageThumbnails(imagesPanel, imageRow.ImagePaths);
                }
            }
        }
        private void UpdateImageThumbnails(Panel imagesPanel, List<string> imagePaths)
        {
            try
            {
                // Очищаем панель
                imagesPanel.Controls.Clear();

                // Отображаем миниатюры
                int thumbX = 5;
                foreach (var imagePath in imagePaths)
                {
                    if (!string.IsNullOrEmpty(imagePath) && System.IO.File.Exists(imagePath))
                    {
                        try
                        {
                            using (var originalImage = Image.FromFile(imagePath))
                            {
                                // Создаем миниатюру безопасно
                                var thumbnail = new Bitmap(80, 80);
                                using (var g = Graphics.FromImage(thumbnail))
                                {
                                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                                    g.DrawImage(originalImage, 0, 0, 80, 80);
                                }

                                var pictureBox = new PictureBox
                                {
                                    Size = new Size(80, 80),
                                    Location = new Point(thumbX, 10),
                                    SizeMode = PictureBoxSizeMode.Zoom,
                                    Image = thumbnail,
                                    BorderStyle = BorderStyle.FixedSingle
                                };
                                imagesPanel.Controls.Add(pictureBox);
                                thumbX += 85;
                            }
                        }
                        catch
                        {
                            // Создаем пустую миниатюру для недействительных изображений
                            var pictureBox = new PictureBox
                            {
                                Size = new Size(80, 80),
                                Location = new Point(thumbX, 10),
                                SizeMode = PictureBoxSizeMode.CenterImage,
                                BorderStyle = BorderStyle.FixedSingle,
                                BackColor = Color.LightGray
                            };
                            var label = new Label
                            {
                                Text = "Ошибка",
                                AutoSize = true,
                                Location = new Point(25, 30)
                            };
                            pictureBox.Controls.Add(label);
                            imagesPanel.Controls.Add(pictureBox);
                            thumbX += 85;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // В случае ошибки просто очищаем панель
                imagesPanel.Controls.Clear();
            }
        }

        private void LoadElementProperties()
        {
            nameTextBox.Text = element.Name;

            xNumericUpDown.Maximum = canvasSize.Width;
            yNumericUpDown.Maximum = canvasSize.Height;
            widthNumericUpDown.Maximum = canvasSize.Width;
            heightNumericUpDown.Maximum = canvasSize.Height;

            if (element is TextElement textElement)
            {
                xNumericUpDown.Value = Math.Min(textElement.X, (int)xNumericUpDown.Maximum);
                yNumericUpDown.Value = Math.Min(textElement.Y, (int)yNumericUpDown.Maximum);
                xNumericUpDown.ValueChanged += (s, e) => textElement.X = (int)xNumericUpDown.Value;
                yNumericUpDown.ValueChanged += (s, e) => textElement.Y = (int)yNumericUpDown.Value;
            }
            else if (element is ImageElement imageElement)
            {
                xNumericUpDown.Value = Math.Min(imageElement.X, (int)xNumericUpDown.Maximum);
                yNumericUpDown.Value = Math.Min(imageElement.Y, (int)yNumericUpDown.Maximum);
                widthNumericUpDown.Value = Math.Min(imageElement.Width, (int)widthNumericUpDown.Maximum);
                heightNumericUpDown.Value = Math.Min(imageElement.Height, (int)heightNumericUpDown.Maximum);
                xNumericUpDown.ValueChanged += (s, e) => imageElement.X = (int)xNumericUpDown.Value;
                yNumericUpDown.ValueChanged += (s, e) => imageElement.Y = (int)yNumericUpDown.Value;
                widthNumericUpDown.ValueChanged += (s, e) => imageElement.Width = (int)widthNumericUpDown.Value;
                heightNumericUpDown.ValueChanged += (s, e) => imageElement.Height = (int)heightNumericUpDown.Value;
            }
            else if (element is AwardElement awardElement)
            {
                xNumericUpDown.Value = Math.Min(Math.Max(awardElement.X, 0), (int)xNumericUpDown.Maximum);
                yNumericUpDown.Value = Math.Min(Math.Max(awardElement.Y, 0), (int)yNumericUpDown.Maximum);
                widthNumericUpDown.Value = Math.Min(Math.Max(awardElement.Width, 20), (int)widthNumericUpDown.Maximum);
                heightNumericUpDown.Value = Math.Min(Math.Max(awardElement.Height, 20), (int)heightNumericUpDown.Maximum);

                xNumericUpDown.ValueChanged += (s, e) => awardElement.X = (int)xNumericUpDown.Value;
                yNumericUpDown.ValueChanged += (s, e) => awardElement.Y = (int)yNumericUpDown.Value;
                widthNumericUpDown.ValueChanged += (s, e) => awardElement.Width = (int)widthNumericUpDown.Value;
                heightNumericUpDown.ValueChanged += (s, e) => awardElement.Height = (int)heightNumericUpDown.Value;
            }
            else if (element is ImageRowElement imageRow)
            {
                xNumericUpDown.Value = Math.Min(Math.Max(imageRow.X, 0), (int)xNumericUpDown.Maximum);
                yNumericUpDown.Value = Math.Min(Math.Max(imageRow.Y, 0), (int)yNumericUpDown.Maximum);
                xNumericUpDown.ValueChanged += (s, e) => imageRow.X = (int)xNumericUpDown.Value;
                yNumericUpDown.ValueChanged += (s, e) => imageRow.Y = (int)yNumericUpDown.Value;
                widthNumericUpDown.Enabled = false;
                heightNumericUpDown.Enabled = false;
            }
            else if (element is EventElement eventElement)
            {
                xNumericUpDown.Value = Math.Min(Math.Max(eventElement.X, 0), (int)xNumericUpDown.Maximum);
                yNumericUpDown.Value = Math.Min(Math.Max(eventElement.Y, 0), (int)yNumericUpDown.Maximum);
                widthNumericUpDown.Value = Math.Min(Math.Max(eventElement.Width, 100), (int)widthNumericUpDown.Maximum);
                heightNumericUpDown.Value = Math.Min(Math.Max(eventElement.Height, 50), (int)heightNumericUpDown.Maximum);

                xNumericUpDown.ValueChanged += (s, e) => eventElement.X = (int)xNumericUpDown.Value;
                yNumericUpDown.ValueChanged += (s, e) => eventElement.Y = (int)yNumericUpDown.Value;
                widthNumericUpDown.ValueChanged += (s, e) => eventElement.Width = (int)widthNumericUpDown.Value;
                heightNumericUpDown.ValueChanged += (s, e) => eventElement.Height = (int)heightNumericUpDown.Value;

                // Инициализируем элементы подписей если они не созданы
                if (eventElement.ShowCaptions && eventElement.CaptionElements.Count == 0)
                {
                    eventElement.InitializeCaptions();
                }
            }
            else if (element is BPElement bpElement)
            {
                xNumericUpDown.Value = Math.Min(Math.Max(bpElement.X, 0), (int)xNumericUpDown.Maximum);
                yNumericUpDown.Value = Math.Min(Math.Max(bpElement.Y, 0), (int)yNumericUpDown.Maximum);
                widthNumericUpDown.Value = Math.Min(Math.Max(bpElement.Width, 100), (int)widthNumericUpDown.Maximum);
                heightNumericUpDown.Value = Math.Min(Math.Max(bpElement.Height, 100), (int)heightNumericUpDown.Maximum);

                xNumericUpDown.ValueChanged += (s, e) => bpElement.X = (int)xNumericUpDown.Value;
                yNumericUpDown.ValueChanged += (s, e) => bpElement.Y = (int)yNumericUpDown.Value;
                widthNumericUpDown.ValueChanged += (s, e) => bpElement.Width = (int)widthNumericUpDown.Value;
                heightNumericUpDown.ValueChanged += (s, e) => bpElement.Height = (int)heightNumericUpDown.Value;
            }
        }
        // Добавьте вспомогательный метод для редактирования наград
        private void ShowAwardProperties(AwardElement award)
        {
            var propertyForm = new PropertyForm(award, canvasSize);
            propertyForm.ShowDialog();
        }
        private void ShowElementProperties(BaseElement element)
        {
            var propertyForm = new PropertyForm(element, canvasSize);
            propertyForm.ShowDialog();
        }
        private void OkButton_Click(object sender, EventArgs e)
        {
            element.Name = nameTextBox.Text;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
