using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace ImageConstructorApp
{

    // Добавьте в AwardData:
    public class AwardData
    {
        public AwardData() { }
        public string Text { get; set; } = "";
        public string ImagePath { get; set; } = "";
        public AwardImageType ImageType { get; set; } = AwardImageType.Custom;
    }


    // Исправленный EventElement:
    public enum AwardImageType
    {
        Custom,
        Books,
        Points,
        Experience,
        Spheres,
        SSRTicket,
        BossMedals,
        Ideas,
        Boxes,
        Backpacks,
        Bags,
        EpicBoxes,
        LegendBoxes,
        Glazgo,
        Burai,
        NightPolice,
        Sazerlend,
        Gloster,
        Gekka,
        Akazuki,
        Vincent,
        Diamonds,
        Gold,
        Lancelot,
        Nightmares,
        Raid,
        Sakuradite,
        SealGold,
        SealLove,
        SealSpace,
        SealSpiritual,
        SealTerrible,
        SoulFragments       
    }

    public enum ArrowDirection
    {
        Right,
        Left,
        Down,
        Up
    }
    public class ProjectData
    {
        public ProjectData() { }
        public List<BaseElement> Elements { get; set; } = new List<BaseElement>();
        public int CanvasWidth { get; set; } = 800;
        public int CanvasHeight { get; set; } = 600;

    }
    [XmlInclude(typeof(BackgroundElement))]
    [XmlInclude(typeof(TextElement))]
    [XmlInclude(typeof(ImageElement))]
    [XmlInclude(typeof(AwardElement))]
    [XmlInclude(typeof(ImageRowElement))]
    [XmlInclude(typeof(AwardCaption))]
    [XmlInclude(typeof(EventElement))]
    [XmlInclude(typeof(BPElement))]
    public abstract class BaseElement
    {
        public string Name { get; set; }
        public abstract void Draw(Graphics graphics, Size canvasSize);
    }
    // Добавим класс для хранения элементов подписи
    public class AwardCaption
    {
        public TextElement TextElement1 { get; set; } = new TextElement
        {
            Text = "Текст 1",
            Font = new Font("Arial", 10),
            Color = Color.Black
        };

        public TextElement TextElement2 { get; set; } = new TextElement
        {
            Text = "Текст 2",
            Font = new Font("Arial", 10),
            Color = Color.Black
        };

        public ImageElement ImageElement { get; set; } = new ImageElement
        {
            ImagePath = "Images/Awards/Idea.png",
            Width = 16,
            Height = 16
        };
    }
    public class EventElement : BaseElement
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int HorizontalSpacing { get; set; } = 10;
        public int VerticalSpacing { get; set; } = 0;
        [XmlArray("Awards")]
        [XmlArrayItem("Award")]
        public List<AwardElement> Awards { get; set; } = new List<AwardElement>();

        public bool ShowCaptions { get; set; } = false;

        [XmlArray("CaptionElements")]
        [XmlArrayItem("BaseElement")]
        public List<BaseElement> CaptionElements { get; set; } = new List<BaseElement>();
        public int CaptionSpacing { get; set; } = 5; // Отступ между элементами подписей
        public override void Draw(Graphics graphics, Size canvasSize)
        {
            if (Awards.Count != 5) return;

            // Используем настраиваемые отступы вместо фиксированных
            int awardWidth = (Width - HorizontalSpacing * 6) / 5;
            int awardHeight = Height - VerticalSpacing * 2;

            for (int i = 0; i < 5; i++)
            {
                var award = Awards[i];
                award.X = X + HorizontalSpacing + i * (awardWidth + HorizontalSpacing);
                award.Y = Y + VerticalSpacing;
                award.Width = awardWidth;
                award.Height = awardHeight;
                award.Draw(graphics, canvasSize);

                if (ShowCaptions)
                {
                    DrawCaption(graphics, canvasSize, award, i);
                }
            }
        }

        private void DrawCaption(Graphics graphics,Size canvasSize, AwardElement award, int index)
        {
                // Рассчитываем позиции для элементов подписи
                int captionStartY = award.Y + award.Height + CaptionSpacing; // Увеличиваем отступ
                int textElementIndex = index * 3; // 3 элемента на каждую подпись: TextElement, TextElement, ImageElement

                if (CaptionElements.Count > textElementIndex + 2)
                {
                    // Первый текстовый элемент - центрируем под наградой
                    var textElement1 = CaptionElements[textElementIndex] as TextElement;
                    if (textElement1 != null)
                    {
                        // Измеряем текст для точного позиционирования
                        var textSize = graphics.MeasureString(textElement1.Text, textElement1.Font);
                        textElement1.X = award.X + (award.Width - (int)textSize.Width) / 2; // Центрируем по горизонтали
                        textElement1.Y = captionStartY;
                        textElement1.Draw(graphics, canvasSize);

                        // Второй текстовый элемент - центрируем под первым
                        var textElement2 = CaptionElements[textElementIndex + 1] as TextElement;
                        if (textElement2 != null)
                        {
                            var textSize2 = graphics.MeasureString(textElement2.Text, textElement2.Font);
                            textElement2.X = award.X + (award.Width - (int)textSize2.Width) / 2; // Центрируем по горизонтали
                            textElement2.Y = captionStartY + (int)textSize.Height + 5; // Отступ 5px после первого текста
                            textElement2.Draw(graphics, canvasSize);

                            // Элемент изображения - позиционируем справа от второго текста
                            var imageElement = CaptionElements[textElementIndex + 2] as ImageElement;
                            if (imageElement != null)
                            {
                                // Используем сохраненные настройки размера
                                int imageWidth = imageElement.Width > 0 ? imageElement.Width : 16;
                                int imageHeight = imageElement.Height > 0 ? imageElement.Height : 16;

                                // Позиционируем изображение справа от второго текста с отступом
                                int imageX = textElement2.X + (int)textSize2.Width + imageElement.Width/2;
                                // Центрируем изображение по вертикали относительно второго текста
                                int imageY = textElement2.Y + (int)(textSize2.Height - imageHeight) / 2;

                                imageElement.X = imageX;
                                imageElement.Y = imageY;
                                imageElement.Width = imageWidth;
                                imageElement.Height = imageHeight;
                                imageElement.Draw(graphics, canvasSize);
                            }
                        }
                    }
                }
            }

        // Добавим метод для инициализации подписей с улучшенными настройками
        public void InitializeCaptions()
        {
            CaptionElements.Clear();

            for (int i = 0; i < 5; i++)
            {
                // Первый текстовый элемент
                CaptionElements.Add(new TextElement
                {
                    Text = $"Описание {i + 1}",
                    Font = new Font("Arial", 10),
                    Color = Color.Black,
                    Name = $"Описание {i + 1}",
                    Opacity = 100
                });

                // Второй текстовый элемент
                CaptionElements.Add(new TextElement
                {
                    Text = $"Значение {i + 1}",
                    Font = new Font("Arial", 10, FontStyle.Bold),
                    Color = Color.Black,
                    Name = $"Значение {i + 1}",
                    Opacity = 100
                });

                // Элемент изображения
                CaptionElements.Add(new ImageElement
                {
                    ImagePath = "Images/Awards/Idea.png",
                    Width = 16,
                    Height = 16,
                    Name = $"Иконка {i + 1}",
                    Style = ImageStyle.Normal
                });
            }
        }
    }

    // Elements.cs - обновленный класс BPElement
    public class BPElement : BaseElement
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int HorizontalSpacing { get; set; } = 10;
        public int VerticalSpacing { get; set; } = 10;
        public int ItemsPerRow { get; set; } = 5;
        public bool ShowCaptions { get; set; } = false;
        public bool ShowSecondCaptionLine { get; set; } = true; // Новая опция
        public bool ShowFirstLineIcon { get; set; } = true;   // Иконка в первой строке подписи
        public bool ShowSecondLineIcon { get; set; } = true;   // Иконка во второй строке подписи
        public int CaptionSpacing { get; set; } = 5;
        public int CaptionSpacingLines { get; set; } = 5;
        public int CaptionHeight { get; set; } = 40; // Добавляем свойство для высоты подписей
        public int CaptionIconSize1 { get; set; } = 16;
        public int CaptionIconSize2 { get; set; } = 16;
        // Можно добавить отступы для большей гибкости
        public int CaptionIconOffsetX { get; set; } = 6;   // отступ от текста по горизонтали
        public int CaptionLineSpacing { get; set; } = 4;
        [XmlArray("Awards")]
        [XmlArrayItem("Award")]
        public List<AwardElement> Awards { get; set; } = new List<AwardElement>();

        [XmlArray("CaptionElements")]
        [XmlArrayItem("BaseElement")]
        public List<BaseElement> CaptionElements { get; set; } = new List<BaseElement>();

        public override void Draw(Graphics graphics, Size canvasSize)
        {
            if (Awards.Count == 0) return;

            int rows = (int)Math.Ceiling((double)Awards.Count / ItemsPerRow);

            // Рассчитываем высоту ряда с учетом подписей
            int rowHeightWithCaption = (Height - VerticalSpacing * (rows + 1)) / rows;
            int awardHeight = rowHeightWithCaption - (ShowCaptions ? CaptionHeight + CaptionSpacing : 0);

            // Защита от отрицательной высоты
            if (awardHeight < 20) awardHeight = 20;

            int awardWidth = (Width - HorizontalSpacing * (ItemsPerRow + 1)) / ItemsPerRow;

            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < ItemsPerRow; col++)
                {
                    int index = row * ItemsPerRow + col;
                    if (index >= Awards.Count) break;

                    var award = Awards[index];
                    

                    award.X = X + HorizontalSpacing + col * (awardWidth + HorizontalSpacing);

                    // Рассчитываем Y-координату с учетом высоты подписей предыдущих рядов
                    int rowY = Y + VerticalSpacing + row * (rowHeightWithCaption + VerticalSpacing);
                    award.Y = rowY;
                    award.Width = awardWidth;
                    award.Height = awardHeight;
                    award.Draw(graphics, canvasSize);
                    if (ShowCaptions)
                    {
                        DrawCaption(graphics, canvasSize, award, index, rowY + awardHeight);
                    }
                }
            }
        }

        private void DrawUpArrow(Graphics graphics, AwardElement fromAward, AwardElement toAward)
        {
            using (var brush = new SolidBrush(fromAward.ArrowColor))
            {
                int arrowSize = 15;
                int centerX = fromAward.X + fromAward.Width / 2;

                // Вертикальная линия от нижнего элемента до верхнего элемента следующего ряда
                int startY = fromAward.Y + fromAward.Height + (ShowCaptions ? CaptionHeight + CaptionSpacing : 0);
                int endY = toAward.Y - 10;

                // Горизонтальная линия к центру верхнего элемента
                int toCenterX = toAward.X + toAward.Width / 2;

                // Рисуем линии
                using (var pen = new Pen(fromAward.ArrowColor, 2))
                {
                    // Вертикальная линия от нижнего элемента
                    graphics.DrawLine(pen, centerX, startY, centerX, endY);

                    // Горизонтальная линия к верхнему элементу
                    graphics.DrawLine(pen, centerX, endY, toCenterX, endY);

                    // Стрелка вверх
                    Point[] arrowPoints = new Point[]
                    {
                    new Point(toCenterX, endY),
                    new Point(toCenterX - arrowSize / 2, endY + arrowSize),
                    new Point(toCenterX + arrowSize / 2, endY + arrowSize)
                    };
                    graphics.FillPolygon(brush, arrowPoints);
                }
            }
        }

        private void DrawCaption(Graphics graphics, Size canvasSize, AwardElement award, int index, int captionStartY)
        {
            int baseIndex = index * 4;  // теперь шаг 4 элемента на награду

            // Гарантируем наличие всех 4 элементов
            while (CaptionElements.Count <= baseIndex + 3)
            {
                int itemNum = (CaptionElements.Count / 4) + 1;

                // 1. Текст первой строки
                CaptionElements.Add(new TextElement
                {
                    Text = $"Описание {itemNum}",
                    Font = new Font("Arial", 10),
                    Color = Color.Black,
                    Name = $"Стр.1 текст {itemNum}",
                    Opacity = 100
                });

                // 2. Иконка первой строки
                CaptionElements.Add(new ImageElement
                {
                    ImagePath = "Images/Awards/Idea.png",
                    Width = CaptionIconSize1,
                    Height = CaptionIconSize1,
                    Name = $"Стр.1 иконка {itemNum}",
                    Style = ImageStyle.Normal
                });

                // 3. Текст второй строки
                CaptionElements.Add(new TextElement
                {
                    Text = ShowSecondCaptionLine ? $"Значение {itemNum}" : "",
                    Font = new Font("Arial", 10, FontStyle.Bold),
                    Color = Color.Black,
                    Name = $"Стр.2 текст {itemNum}",
                    Opacity = 100
                });

                // 4. Иконка второй строки
                CaptionElements.Add(new ImageElement
                {
                    ImagePath = "Images/Awards/Idea.png",
                    Width = CaptionIconSize2,
                    Height = CaptionIconSize2,
                    Name = $"Стр.2 иконка {itemNum}",
                    Style = ImageStyle.Normal
                });
            }

            if (CaptionElements.Count <= baseIndex + 3) return;

            // ───────────────────────────────────────────────────────────────
            // Первая строка
            // ───────────────────────────────────────────────────────────────
            var text1 = CaptionElements[baseIndex + 0] as TextElement;
            var icon1 = CaptionElements[baseIndex + 1] as ImageElement;

            if (text1 != null && !string.IsNullOrWhiteSpace(text1.Text))
            {
                var textSize1 = graphics.MeasureString(text1.Text, text1.Font);

                text1.X = award.X + (award.Width - (int)textSize1.Width) / 2;
                text1.Y = captionStartY + CaptionSpacing;
                text1.Draw(graphics, canvasSize);

                // Иконка первой строки — рисуем только если включена
                if (ShowFirstLineIcon && icon1 != null && !string.IsNullOrEmpty(icon1.ImagePath))
                {
                    int iconX = text1.X + (int)textSize1.Width + CaptionIconOffsetX;
                    int iconY = text1.Y + ((int)textSize1.Height - CaptionIconSize1) / 2;

                    icon1.X = iconX;
                    icon1.Y = iconY;
                    icon1.Width = CaptionIconSize1;
                    icon1.Height = CaptionIconSize1;
                    icon1.Draw(graphics, canvasSize);
                }
            }

            // ───────────────────────────────────────────────────────────────
            // Вторая строка
            // ───────────────────────────────────────────────────────────────
            if (!ShowSecondCaptionLine) return;

            var text2 = CaptionElements[baseIndex + 2] as TextElement;
            var icon2 = CaptionElements[baseIndex + 3] as ImageElement;

            if (text2 != null && !string.IsNullOrWhiteSpace(text2.Text))
            {
                var textSize2 = graphics.MeasureString(text2.Text, text2.Font);

                text2.X = award.X + (award.Width - (int)textSize2.Width) / 2;
                text2.Y = (text1 != null ? text1.Y + (int)textSize2.Height + CaptionLineSpacing : captionStartY + CaptionSpacing + 20);
                text2.Y += CaptionLineSpacing;
                text2.Draw(graphics, canvasSize);

                // Иконка второй строки
                if (ShowSecondLineIcon && icon2 != null && !string.IsNullOrEmpty(icon2.ImagePath))
                {
                    int iconX = text2.X + (int)textSize2.Width + CaptionIconOffsetX;
                    int iconY = text2.Y + ((int)textSize2.Height - CaptionIconSize2) / 2;

                    icon2.X = iconX;
                    icon2.Y = iconY;
                    icon2.Width = CaptionIconSize2;
                    icon2.Height = CaptionIconSize2;
                    icon2.Draw(graphics, canvasSize);
                }
            }
        }

        // Метод для добавления новой награды с автоматическим созданием подписей
        public void AddAward()
        {
            var newAward = new AwardElement
            {
                Text = $"Награда {Awards.Count + 1}",
                IsHiddenInList = true,
                ShowArrow = true
            };
            Awards.Add(newAward);

            if (Awards.Count % ItemsPerRow == 1)
                newAward.ShowArrow = false;

            // Автоматически создаем подписи для новой награды
            if (ShowCaptions)
            {
                int newIndex = Awards.Count - 1;
                CaptionElements.Add(new TextElement
                {
                    Text = $"Описание {newIndex + 1}",
                    Font = new Font("Arial", 10),
                    Color = Color.Black,
                    Name = $"Описание {newIndex + 1}",
                    Opacity = 100
                });

                // Добавляем вторую строку только если опция включена
                if (ShowSecondCaptionLine)
                {
                    CaptionElements.Add(new TextElement
                    {
                        Text = $"Значение {newIndex + 1}",
                        Font = new Font("Arial", 10, FontStyle.Bold),
                        Color = Color.Black,
                        Name = $"Значение {newIndex + 1}",
                        Opacity = 100
                    });

                    CaptionElements.Add(new ImageElement
                    {
                        ImagePath = "Images/Awards/Idea.png",
                        Width = 16,
                        Height = 16,
                        Name = $"Иконка {newIndex + 1}",
                        Style = ImageStyle.Normal
                    });
                }
                else
                {
                    // Добавляем пустые элементы для сохранения структуры
                    CaptionElements.Add(new TextElement
                    {
                        Text = "",
                        Font = new Font("Arial", 10, FontStyle.Bold),
                        Color = Color.Black,
                        Name = $"Значение {newIndex + 1}",
                        Opacity = 100
                    });

                    CaptionElements.Add(new ImageElement
                    {
                        ImagePath = "",
                        Width = 16,
                        Height = 16,
                        Name = $"Иконка {newIndex + 1}",
                        Style = ImageStyle.Normal
                    });
                }
            }
        }

        // Метод для удаления последней награды с удалением соответствующих подписей
        public void RemoveLastAward()
        {
            if (Awards.Count > 0)
            {
                Awards.RemoveAt(Awards.Count - 1);

                // Удаляем соответствующие элементы подписи (3 элемента на награду)
                if (CaptionElements.Count >= 3)
                {
                    CaptionElements.RemoveRange(CaptionElements.Count - 3, 3);
                }
            }
        }

        public void InitializeCaptions()
        {
            CaptionElements.Clear();

            for (int i = 0; i < Awards.Count; i++)
            {
                int num = i + 1;

                // 1. Текст первой строки (обычно описание/название)
                CaptionElements.Add(new TextElement
                {
                    Text = $"Описание {num}",
                    Font = new Font("Arial", 10),
                    Color = Color.Black,
                    Name = $"Строка 1 - Текст {num}",
                    Opacity = 100
                });

                // 2. Иконка первой строки
                CaptionElements.Add(new ImageElement
                {
                    ImagePath = "Images/Awards/Idea.png",
                    Width = CaptionIconSize1,
                    Height = CaptionIconSize1,
                    Name = $"Строка 1 - Иконка {num}",
                    Style = ImageStyle.Normal
                });

                // 3. Текст второй строки (обычно значение/количество)
                CaptionElements.Add(new TextElement
                {
                    Text = $"Значение {num}",
                    Font = new Font("Arial", 10, FontStyle.Bold),
                    Color = Color.Black,
                    Name = $"Строка 2 - Текст {num}",
                    Opacity = 100
                });

                // 4. Иконка второй строки
                CaptionElements.Add(new ImageElement
                {
                    ImagePath = "Images/Awards/Idea.png",
                    Width = CaptionIconSize2,
                    Height = CaptionIconSize2,
                    Name = $"Строка 2 - Иконка {num}",
                    Style = ImageStyle.Normal
                });
            }
        }
    }


    public class BackgroundElement : BaseElement
    {
        public string ImagePath { get; set; }

        [XmlIgnore]
        public Image Image { get; set; }

        private float _brightness = 0f;
        private float _contrast = 0f;

        public float Brightness
        {
            get => _brightness;
            set => _brightness = Math.Clamp(value, -1f, 1f); // Ограничиваем диапазон
        }

        public float Contrast
        {
            get => _contrast;
            set => _contrast = Math.Clamp(value, -1f, 1f); // Ограничиваем диапазон
        }

        public BackgroundStyle Style { get; set; } = BackgroundStyle.Stretch;

        // Кэшируем ImageAttributes для производительности (опционально)
        private ImageAttributes _cachedAttributes;
        private float _cachedBrightness = float.NaN;
        private float _cachedContrast = float.NaN;

        private ImageAttributes GetImageAttributes()
        {
            if (_cachedAttributes != null &&
                Math.Abs(_cachedBrightness - Brightness) < 0.001f &&
                Math.Abs(_cachedContrast - Contrast) < 0.001f)
            {
                return _cachedAttributes;
            }

            // Освобождаем старый кэш
            _cachedAttributes?.Dispose();

            // Формула: 
            //   brightness: добавляется как смещение (в диапазоне [-1, 1] → [-255, 255])
            //   contrast: масштаб (1 = норма, 0 = серый, >1 = выше контраст)
            float contrastFactor = Contrast + 1f; // [-1,1] → [0,2]
                                                  // Яркость: [-1, 1] → [-0.5, +0.5] — чтобы не "выбивать" в чёрный/белый
            float brightnessOffset = Math.Clamp(Brightness * 0.5f, -0.5f, 0.5f);

            var matrix = new ColorMatrix(new float[][]
            {
            new float[] { contrastFactor, 0, 0, 0, 0 },
            new float[] { 0, contrastFactor, 0, 0, 0 },
            new float[] { 0, 0, contrastFactor, 0, 0 },
            new float[] { 0, 0, 0, 1, 0 },
            new float[] { brightnessOffset, brightnessOffset, brightnessOffset, 0, 1 }
            });

            _cachedAttributes = new ImageAttributes();
            _cachedAttributes.SetColorMatrix(matrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);

            _cachedBrightness = Brightness;
            _cachedContrast = Contrast;

            return _cachedAttributes;
        }

        public override void Draw(Graphics graphics, Size canvasSize)
        {
            if (string.IsNullOrEmpty(ImagePath) || !File.Exists(ImagePath))
                return;

            try
            {
                if (Image == null)
                {
                    Image = Image.FromFile(ImagePath);
                }

                var destRect = new Rectangle(0, 0, canvasSize.Width, canvasSize.Height);
                var srcRect = new Rectangle(0, 0, Image.Width, Image.Height);
                var attributes = GetImageAttributes();

                switch (Style)
                {
                    case BackgroundStyle.Stretch:
                        graphics.DrawImage(Image, destRect, srcRect.X, srcRect.Y, srcRect.Width, srcRect.Height, GraphicsUnit.Pixel, attributes);
                        break;

                    case BackgroundStyle.Center:
                        {
                            float scaleX = (float)canvasSize.Width / Image.Width;
                            float scaleY = (float)canvasSize.Height / Image.Height;
                            float scale = Math.Min(scaleX, scaleY);
                            int newWidth = (int)(Image.Width * scale);
                            int newHeight = (int)(Image.Height * scale);
                            int x = (canvasSize.Width - newWidth) / 2;
                            int y = (canvasSize.Height - newHeight) / 2;
                            destRect = new Rectangle(x, y, newWidth, newHeight);
                            srcRect = new Rectangle(0, 0, Image.Width, Image.Height);
                            graphics.DrawImage(Image, destRect, srcRect.X, srcRect.Y, srcRect.Width, srcRect.Height, GraphicsUnit.Pixel, attributes);
                            break;
                        }

                    case BackgroundStyle.Tile:
                        {
                            // Tile не поддерживает ImageAttributes напрямую через FillRectangle,
                            // поэтому рисуем вручную
                            int tileWidth = Image.Width;
                            int tileHeight = Image.Height;
                            for (int y = 0; y < canvasSize.Height; y += tileHeight)
                            {
                                for (int x = 0; x < canvasSize.Width; x += tileWidth)
                                {
                                    graphics.DrawImage(
                                        Image,
                                        new Rectangle(x, y, tileWidth, tileHeight),
                                        0, 0, tileWidth, tileHeight,
                                        GraphicsUnit.Pixel,
                                        attributes
                                    );
                                }
                            }
                            break;
                        }
                }
            }
            catch
            {
                // Игнорируем ошибки загрузки изображения
            }
        }

    }

    public enum BackgroundStyle
    {
        Stretch,
        Center,
        Tile
    }

    public class TextElement : BaseElement
    {
        public string Text { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        [XmlIgnore]
        public Font Font { get; set; }

        public int Opacity { get; set; } = 100; // 0-100%
        public string FontName { get; set; } = "Arial";
        public float FontSize { get; set; } = 16;
        public FontStyle FontStyle { get; set; } = FontStyle.Regular;

        [XmlIgnore]
        public Color Color { get; set; }
        [XmlElement("Color")]
        public string ColorHtml
        {
            get { return ColorTranslator.ToHtml(Color); }
            set { Color = ColorTranslator.FromHtml(value); }
        }

        // === НОВОЕ: опции outline ===
        public bool HasOutline { get; set; } = false;
        [XmlIgnore]
        public Color OutlineColor { get; set; } = Color.Black;
        [XmlElement("OutlineColor")]
        public string OutlineColorHtml
        {
            get { return ColorTranslator.ToHtml(OutlineColor); }
            set { OutlineColor = ColorTranslator.FromHtml(value); }
        }
        public int OutlineThickness { get; set; } = 1; // толщина обводки (в пикселях)
                                                       // === конец новых полей ===

        public bool IsMainTitle { get; set; } = false;

        public void InitializeFont()
        {
            try
            {
                Font = new Font(FontName, FontSize, FontStyle);
            }
            catch
            {
                Font = new Font("Arial", 16, FontStyle.Regular);
                FontName = "Arial";
                FontSize = 16;
                FontStyle = FontStyle.Regular;
            }
        }

        public TextElement()
        {
            InitializeFont();
            Color = Color.Black;
            OutlineColor = Color.Black;
        }

        // Вспомогательный метод: рисует текст с обводкой
        private void DrawTextWithOutline(Graphics g, string text, Font font, Brush textBrush, Pen outlinePen, PointF location)
        {
            if (!HasOutline)
            {
                g.DrawString(text, font, textBrush, location);
                return;
            }

            // Смещения для толстой обводки (если OutlineThickness > 1)
            var offsets = new List<(int dx, int dy)>();
            for (int dx = -OutlineThickness; dx <= OutlineThickness; dx++)
            {
                for (int dy = -OutlineThickness; dy <= OutlineThickness; dy++)
                {
                    if (dx == 0 && dy == 0) continue; // пропускаем центр
                    offsets.Add((dx, dy));
                }
            }

            // Рисуем outline
            foreach (var (dx, dy) in offsets)
            {
                g.DrawString(text, font, outlinePen.Brush, location.X + dx, location.Y + dy);
            }

            // Основной текст
            g.DrawString(text, font, textBrush, location);
        }

        public override void Draw(Graphics graphics, Size canvasSize)
        {
            if (Font == null)
            {
                InitializeFont();
            }

            // Цвет с прозрачностью
            Color textColorWithAlpha = Color.FromArgb(
                (int)(Opacity * 2.55),
                Color.R, Color.G, Color.B
            );

            using (var textBrush = new SolidBrush(textColorWithAlpha))
            using (var outlinePen = new Pen(OutlineColor, OutlineThickness))
            {
                PointF drawPoint;
                if (IsMainTitle)
                {
                    var size = graphics.MeasureString(Text, Font);
                    float x = (canvasSize.Width - size.Width) / 2;
                    drawPoint = new PointF(x, Y);
                }
                else
                {
                    drawPoint = new PointF(X, Y);
                }

                DrawTextWithOutline(graphics, Text, Font, textBrush, outlinePen, drawPoint);
            }
        }
    }

    public class ImageElement : BaseElement
    {
        private string _imagePath;
        public string ImagePath
        {
            get => _imagePath;
            set
            {
                if (_imagePath != value)
                {
                    _imagePath = value;
                    // Сбрасываем кэш изображения
                    Image?.Dispose();
                    Image = null;
                    // Также сбрасываем кэш атрибутов, если нужно
                    _cachedAttributes?.Dispose();
                    _cachedAttributes = null;
                }
            }
        }
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public AwardImageType ImageType { get; set; } = AwardImageType.Custom;
        public ImageStyle Style { get; set; } = ImageStyle.Normal;
        public ImageScaleType ImageScaleType { get; set; } = ImageScaleType.Proportional; // Новое свойство

        // Новые свойства для рамки
        public bool ShowBorder { get; set; } = false;
        public int BorderWidth { get; set; } = 2;
        [XmlIgnore]
        public Color BorderColor { get; set; } = Color.Black;

        [XmlElement("BorderColor")]
        public string BorderColorHtml
        {
            get { return ColorTranslator.ToHtml(BorderColor); }
            set { BorderColor = ColorTranslator.FromHtml(value); }
        }

        [XmlIgnore]
        public Image Image { get; set; }

        private float _brightness = 0f;
        private float _contrast = 0f;

        public float Brightness
        {
            get => _brightness;
            set => _brightness = Math.Clamp(value, -1f, 1f);
        }

        public float Contrast
        {
            get => _contrast;
            set => _contrast = Math.Clamp(value, -1f, 1f);
        }

        // Кэширование ImageAttributes для производительности
        private ImageAttributes _cachedAttributes;
        private float _cachedBrightness = float.NaN;
        private float _cachedContrast = float.NaN;

        private ImageAttributes GetImageAttributes()
        {
            if (_cachedAttributes != null &&
                Math.Abs(_cachedBrightness - Brightness) < 0.001f &&
                Math.Abs(_cachedContrast - Contrast) < 0.001f)
            {
                return _cachedAttributes;
            }

            _cachedAttributes?.Dispose();

            float contrastFactor = Math.Clamp(Contrast + 1f, 0f, 2f);
            float brightnessOffset = Math.Clamp(Brightness * 0.5f, -0.5f, 0.5f); // мягкий диапазон

            var matrix = new ColorMatrix(new float[][]
            {
            new float[] { contrastFactor, 0, 0, 0, 0 },
            new float[] { 0, contrastFactor, 0, 0, 0 },
            new float[] { 0, 0, contrastFactor, 0, 0 },
            new float[] { 0, 0, 0, 1, 0 },
            new float[] { brightnessOffset, brightnessOffset, brightnessOffset, 0, 1 }
            });

            _cachedAttributes = new ImageAttributes();
            _cachedAttributes.SetColorMatrix(matrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);

            _cachedBrightness = Brightness;
            _cachedContrast = Contrast;

            return _cachedAttributes;
        }

        public override void Draw(Graphics graphics, Size canvasSize)
        {
            if (string.IsNullOrEmpty(ImagePath) || !File.Exists(ImagePath))
                return;

            try
            {
                if (Image == null)
                {
                    Image = Image.FromFile(ImagePath);
                }

                var destRect = new Rectangle(X, Y, Width, Height);
                var srcRect = new Rectangle(0, 0, Image.Width, Image.Height);
                var attributes = GetImageAttributes();

                // Рисуем изображение в зависимости от стиля
                switch (Style)
                {
                    case ImageStyle.Normal:
                        DrawScaledImage(graphics, Image, destRect, attributes);
                        break;

                    case ImageStyle.Rounded:
                        DrawRoundedImage(graphics, Image, destRect, attributes);
                        break;

                    case ImageStyle.Circle:
                        DrawCircularImage(graphics, Image, destRect, attributes);
                        break;
                }

                // Рисуем рамку если включено
                if (ShowBorder && BorderWidth > 0)
                {
                    DrawBorder(graphics, destRect);
                }
            }
            catch
            {
                // Игнорируем ошибки загрузки изображения
            }
        }

        private void DrawScaledImage(Graphics graphics, Image image, Rectangle destRect, ImageAttributes attributes)
        {
            var contentRect = destRect;

            // Если есть рамка, уменьшаем область для изображения
            if (ShowBorder && BorderWidth > 0)
            {
                contentRect = new Rectangle(
                    destRect.X + BorderWidth,
                    destRect.Y + BorderWidth,
                    destRect.Width - 2 * BorderWidth,
                    destRect.Height - 2 * BorderWidth
                );
            }

            switch (ImageScaleType)
            {
                case ImageScaleType.Stretch:
                    graphics.DrawImage(image, contentRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, attributes);
                    break;

                case ImageScaleType.Proportional:
                    float scaleX = (float)contentRect.Width / image.Width;
                    float scaleY = (float)contentRect.Height / image.Height;
                    float scale = Math.Min(scaleX, scaleY);

                    int scaledWidth = (int)(image.Width * scale);
                    int scaledHeight = (int)(image.Height * scale);

                    int offsetX = contentRect.X + (contentRect.Width - scaledWidth) / 2;
                    int offsetY = contentRect.Y + (contentRect.Height - scaledHeight) / 2;

                    graphics.DrawImage(image,
                        new Rectangle(offsetX, offsetY, scaledWidth, scaledHeight),
                        0, 0, image.Width, image.Height,
                        GraphicsUnit.Pixel, attributes);
                    break;

                case ImageScaleType.Fill:
                    float fillScaleX = (float)contentRect.Width / image.Width;
                    float fillScaleY = (float)contentRect.Height / image.Height;
                    float fillScale = Math.Max(fillScaleX, fillScaleY);

                    int fillWidth = (int)(image.Width * fillScale);
                    int fillHeight = (int)(image.Height * fillScale);

                    int fillOffsetX = contentRect.X + (contentRect.Width - fillWidth) / 2;
                    int fillOffsetY = contentRect.Y + (contentRect.Height - fillHeight) / 2;

                    graphics.DrawImage(image,
                        new Rectangle(fillOffsetX, fillOffsetY, fillWidth, fillHeight),
                        0, 0, image.Width, image.Height,
                        GraphicsUnit.Pixel, attributes);
                    break;
            }
        }

        private void DrawRoundedImage(Graphics graphics, Image image, Rectangle destRect, ImageAttributes attributes)
        {
            using (var path = new GraphicsPath())
            {
                int radius = 20;
                // Защита от слишком большого радиуса
                radius = Math.Min(radius, Math.Min(destRect.Width / 2, destRect.Height / 2));

                path.AddArc(destRect.X, destRect.Y, radius, radius, 180, 90);
                path.AddArc(destRect.X + destRect.Width - radius, destRect.Y, radius, radius, 270, 90);
                path.AddArc(destRect.X + destRect.Width - radius, destRect.Y + destRect.Height - radius, radius, radius, 0, 90);
                path.AddArc(destRect.X, destRect.Y + destRect.Height - radius, radius, radius, 90, 90);
                path.CloseFigure();

                graphics.SetClip(path);
                DrawScaledImage(graphics, image, destRect, attributes);
                graphics.ResetClip();
            }
        }

        private void DrawCircularImage(Graphics graphics, Image image, Rectangle destRect, ImageAttributes attributes)
        {
            using (var path = new GraphicsPath())
            {
                int diameter = Math.Min(destRect.Width, destRect.Height);
                var circleRect = new Rectangle(
                    destRect.X + (destRect.Width - diameter) / 2,
                    destRect.Y + (destRect.Height - diameter) / 2,
                    diameter,
                    diameter
                );

                path.AddEllipse(circleRect);
                path.CloseFigure();

                graphics.SetClip(path);
                DrawScaledImage(graphics, image, destRect, attributes);
                graphics.ResetClip();
            }
        }

        private void DrawBorder(Graphics graphics, Rectangle rect)
        {
            using (var pen = new Pen(BorderColor, BorderWidth))
            {
                switch (Style)
                {
                    case ImageStyle.Normal:
                        graphics.DrawRectangle(pen, rect);
                        break;

                    case ImageStyle.Rounded:
                        using (var path = new GraphicsPath())
                        {
                            int radius = 20;
                            radius = Math.Min(radius, Math.Min(rect.Width / 2, rect.Height / 2));

                            path.AddArc(rect.X, rect.Y, radius, radius, 180, 90);
                            path.AddArc(rect.X + rect.Width - radius, rect.Y, radius, radius, 270, 90);
                            path.AddArc(rect.X + rect.Width - radius, rect.Y + rect.Height - radius, radius, radius, 0, 90);
                            path.AddArc(rect.X, rect.Y + rect.Height - radius, radius, radius, 90, 90);
                            path.CloseFigure();

                            graphics.DrawPath(pen, path);
                        }
                        break;

                    case ImageStyle.Circle:
                        int diameter = Math.Min(rect.Width, rect.Height);
                        var circleRect = new Rectangle(
                            rect.X + (rect.Width - diameter) / 2,
                            rect.Y + (rect.Height - diameter) / 2,
                            diameter,
                            diameter
                        );
                        graphics.DrawEllipse(pen, circleRect);
                        break;
                }
            }
        }
    }

    public enum ImageStyle
    {
        Normal,
        Rounded,
         Circle  // Новый стиль
    }
    // Новый enum для типов масштабирования
    public enum ImageScaleType
    {
        Stretch,      // Растянуть
        Proportional, // Пропорционально
        Fill          // Заполнить с обрезкой
    }

    public class AwardElement : BaseElement
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; } = 100;
        public int Height { get; set; } = 100;
        public string Text { get; set; } = "Награда";
        public string ImagePath { get; set; }
        public AwardImageType ImageType { get; set; } = AwardImageType.Custom;

        // Новые свойства
        [XmlIgnore]
        public Color BackgroundColor { get; set; } = Color.LightGray;
        public ImageScaleType ImageScaleType { get; set; } = ImageScaleType.Proportional;

        [XmlIgnore]
        public Font Font { get; set; }
        public string FontName { get; set; } = "Arial";
        public float FontSize { get; set; } = 12;
        public FontStyle FontStyle { get; set; } = FontStyle.Bold;
        public int TextOpacity { get; set; } = 100; // 0-100%
        [XmlIgnore]
        public Color TextColor { get; set; } = Color.White;

        [XmlIgnore]
        public Color BorderColor { get; set; } = Color.Gold;
        public int BorderWidth { get; set; } = 3;

        [XmlIgnore]
        public Color ArrowColor { get; set; } = Color.Black;
        public bool ShowArrow { get; set; } = true;
        public ArrowDirection ArrowDirection { get; set; } = ArrowDirection.Left;
        public bool IsHiddenInList { get; set; } = false;

        // Для сериализации Color
        [XmlElement("BackgroundColor")]
        public string BackgroundColorHtml
        {
            get { return ColorTranslator.ToHtml(BackgroundColor); }
            set { BackgroundColor = ColorTranslator.FromHtml(value); }
        }
        [XmlElement("ArrowColor")]
        public string ArrowColorHtml
        {
            get { return ColorTranslator.ToHtml(ArrowColor); }
            set { ArrowColor = ColorTranslator.FromHtml(value); }
        }
        [XmlElement("TextColor")]
        public string TextColorHtml
        {
            get { return ColorTranslator.ToHtml(TextColor); }
            set { TextColor = ColorTranslator.FromHtml(value); }
        }

        [XmlElement("BorderColor")]
        public string BorderColorHtml
        {
            get { return ColorTranslator.ToHtml(BorderColor); }
            set { BorderColor = ColorTranslator.FromHtml(value); }
        }

        // Добавьте этот метод для инициализации шрифта после десериализации
        public void InitializeFont()
        {
            try
            {
                Font = new Font(FontName, FontSize, FontStyle);
            }
            catch
            {
                // Fallback to default font if the saved font is not available
                Font = new Font("Arial", 12, FontStyle.Bold);
                FontName = "Arial";
                FontSize = 12;
                FontStyle = FontStyle.Bold;
            }
        }
        public AwardElement()
        {
            InitializeFont();
        }

        public override void Draw(Graphics graphics, Size canvasSize)
        {
            var awardRect = new Rectangle(X, Y, Width, Height);
            DrawAward(graphics, awardRect);
            DrawText(graphics, awardRect);

            if (ShowArrow)
            {
                DrawArrow(graphics, awardRect);
            }
        }

        private void DrawAward(Graphics graphics, Rectangle rect)
        {
            using (var path = new GraphicsPath())
            {
                int radius = 15;
                path.AddArc(rect.X, rect.Y, radius, radius, 180, 90);
                path.AddArc(rect.X + rect.Width - radius, rect.Y, radius, radius, 270, 90);
                path.AddArc(rect.X + rect.Width - radius, rect.Y + rect.Height - radius, radius, radius, 0, 90);
                path.AddArc(rect.X, rect.Y + rect.Height - radius, radius, radius, 90, 90);
                path.CloseFigure();

                // Заливаем фон
                using (var bgBrush = new SolidBrush(BackgroundColor))
                {
                    graphics.FillPath(bgBrush, path);
                }

                // Рисуем изображение
                if (!string.IsNullOrEmpty(ImagePath) && File.Exists(ImagePath))
                {
                    try
                    {
                        using (var image = Image.FromFile(ImagePath))
                        {
                            var contentRect = new Rectangle(rect.X + BorderWidth, rect.Y + BorderWidth,
                                                         rect.Width - 2 * BorderWidth, rect.Height - 2 * BorderWidth);

                            graphics.SetClip(path);
                            DrawImageAccordingToScaleType(graphics, image, contentRect);
                            graphics.ResetClip();
                        }
                    }
                    catch
                    {
                        // Оставляем только фон если изображение не загрузилось
                    }
                }

                // Рамка
                using (var pen = new Pen(BorderColor, BorderWidth))
                {
                    graphics.DrawPath(pen, path);
                }
            }
        }

        private void DrawImageAccordingToScaleType(Graphics graphics, Image image, Rectangle destRect)
        {
            switch (ImageScaleType)
            {
                case ImageScaleType.Stretch:
                    graphics.DrawImage(image, destRect);
                    break;

                case ImageScaleType.Proportional:
                    float scaleX = (float)destRect.Width / image.Width;
                    float scaleY = (float)destRect.Height / image.Height;
                    float scale = Math.Min(scaleX, scaleY);

                    int scaledWidth = (int)(image.Width * scale);
                    int scaledHeight = (int)(image.Height * scale);

                    int offsetX = destRect.X + (destRect.Width - scaledWidth) / 2;
                    int offsetY = destRect.Y + (destRect.Height - scaledHeight) / 2;

                    graphics.DrawImage(image, offsetX, offsetY, scaledWidth, scaledHeight);
                    break;

                case ImageScaleType.Fill:
                    // Заполнение с обрезкой
                    float fillScaleX = (float)destRect.Width / image.Width;
                    float fillScaleY = (float)destRect.Height / image.Height;
                    float fillScale = Math.Max(fillScaleX, fillScaleY);

                    int fillWidth = (int)(image.Width * fillScale);
                    int fillHeight = (int)(image.Height * fillScale);

                    int fillOffsetX = destRect.X + (destRect.Width - fillWidth) / 2;
                    int fillOffsetY = destRect.Y + (destRect.Height - fillHeight) / 2;

                    graphics.DrawImage(image, fillOffsetX, fillOffsetY, fillWidth, fillHeight);
                    break;
            }
        }

        private void DrawImageProportional(Graphics graphics, Image image, Rectangle destRect)
        {
            // Рассчитываем пропорции для корректного масштабирования
            float scaleX = (float)destRect.Width / image.Width;
            float scaleY = (float)destRect.Height / image.Height;
            float scale = Math.Min(scaleX, scaleY);

            int scaledWidth = (int)(image.Width * scale);
            int scaledHeight = (int)(image.Height * scale);

            int offsetX = destRect.X + (destRect.Width - scaledWidth) / 2;
            int offsetY = destRect.Y + (destRect.Height - scaledHeight) / 2;

            graphics.DrawImage(image, offsetX, offsetY, scaledWidth, scaledHeight);
        }

        private void DrawText(Graphics graphics, Rectangle rect)
        {
            if (Font == null)
            {
                InitializeFont();
            }

            // Создаем цвет с учетом прозрачности
            Color textColorWithAlpha = Color.FromArgb(
                (int)(TextOpacity * 2.55), // Конвертируем 0-100% в 0-255
                TextColor.R, TextColor.G, TextColor.B
            );

            using (var brush = new SolidBrush(textColorWithAlpha))
            {
                var format = new StringFormat
                {
                    Alignment = StringAlignment.Center,
                    LineAlignment = StringAlignment.Center
                };

                graphics.DrawString(Text, Font, brush, rect, format);
            }
        }

        private void DrawArrow(Graphics graphics, Rectangle awardRect)
        {
            using (var brush = new SolidBrush(ArrowColor))
            using (var pen = new Pen(Color.Black, 2))
            {
                Point[] arrowPoints = null;
                int arrowSize = 20;
                int offset = 10;

                switch (ArrowDirection)
                {
                    case ArrowDirection.Right:
                        arrowPoints = new Point[]
                        {
                            new Point(awardRect.Right + offset, awardRect.Top + awardRect.Height / 2),
                            new Point(awardRect.Right + offset + arrowSize, awardRect.Top + awardRect.Height / 2 - arrowSize/2),
                            new Point(awardRect.Right + offset + arrowSize, awardRect.Top + awardRect.Height / 2 + arrowSize/2)
                        };
                        break;
                    case ArrowDirection.Left:
                        arrowPoints = new Point[]
                        {
                            new Point(awardRect.Left - offset, awardRect.Top + awardRect.Height / 2),
                            new Point(awardRect.Left - offset - arrowSize, awardRect.Top + awardRect.Height / 2 - arrowSize/2),
                            new Point(awardRect.Left - offset - arrowSize, awardRect.Top + awardRect.Height / 2 + arrowSize/2)
                        };
                        break;
                    case ArrowDirection.Down:
                        arrowPoints = new Point[]
                        {
                            new Point(awardRect.Left + awardRect.Width / 2, awardRect.Bottom + offset),
                            new Point(awardRect.Left + awardRect.Width / 2 - arrowSize/2, awardRect.Bottom + offset + arrowSize),
                            new Point(awardRect.Left + awardRect.Width / 2 + arrowSize/2, awardRect.Bottom + offset + arrowSize)
                        };
                        break;
                    case ArrowDirection.Up:
                        arrowPoints = new Point[]
                        {
                            new Point(awardRect.Left + awardRect.Width / 2, awardRect.Top - offset),
                            new Point(awardRect.Left + awardRect.Width / 2 - arrowSize/2, awardRect.Top - offset - arrowSize),
                            new Point(awardRect.Left + awardRect.Width / 2 + arrowSize/2, awardRect.Top - offset - arrowSize)
                        };
                        break;
                }

                if (arrowPoints != null)
                {
                    graphics.FillPolygon(brush, arrowPoints);
                }
            }
        }

        // Метод для клонирования награды
        public AwardElement Clone()
        {
            return new AwardElement
            {
                X = this.X,
                Y = this.Y,
                Width = this.Width,
                Height = this.Height,
                Text = this.Text,
                ImagePath = this.ImagePath,
                ImageType = this.ImageType,
                FontName = this.FontName,
                FontSize = this.FontSize,
                FontStyle = this.FontStyle,
                TextColor = this.TextColor,
                BorderColor = this.BorderColor,
                BorderWidth = this.BorderWidth,
                ShowArrow = this.ShowArrow,
                ArrowDirection = this.ArrowDirection,
                Name = this.Name
            };
        }
    }
    public class ImageRowElement : BaseElement
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int ItemWidth { get; set; } = 80;
        public int ItemHeight { get; set; } = 80;
        public int Spacing { get; set; } = 20;
        public int MaxRowWidth { get; set; } = 800;
        public bool WrapImages { get; set; } = false;
        public ImageScaleType ImageScaleType { get; set; } = ImageScaleType.Proportional;

        // Новые свойства для рамки
        public bool ShowBorder { get; set; } = false;
        public int BorderWidth { get; set; } = 2;
        [XmlIgnore]
        public Color BorderColor { get; set; } = Color.Black;

        [XmlElement("BorderColor")]
        public string BorderColorHtml
        {
            get { return ColorTranslator.ToHtml(BorderColor); }
            set { BorderColor = ColorTranslator.FromHtml(value); }
        }

        public List<string> ImagePaths { get; set; } = new List<string>();
        public bool IsHiddenInList { get; set; } = false;

        public override void Draw(Graphics graphics, Size canvasSize)
        {
            if (IsHiddenInList) return;

            int currentX = X;
            int currentY = Y;
            int rowHeight = 0;

            for (int i = 0; i < ImagePaths.Count; i++)
            {
                if (string.IsNullOrEmpty(ImagePaths[i]) || !File.Exists(ImagePaths[i]))
                    continue;

                // Проверяем, нужно ли переносить на следующую строку
                if (WrapImages && currentX + ItemWidth > X + MaxRowWidth && i > 0)
                {
                    currentX = X;
                    currentY += rowHeight + Spacing;
                    rowHeight = 0;
                }

                var itemRect = new Rectangle(currentX, currentY, ItemWidth, ItemHeight);

                try
                {
                    using (var image = Image.FromFile(ImagePaths[i]))
                    {
                        DrawImageAccordingToScaleType(graphics, image, itemRect);

                        // Рисуем рамку если включено
                        if (ShowBorder && BorderWidth > 0)
                        {
                            DrawBorder(graphics, itemRect);
                        }
                    }
                }
                catch
                {
                    // Рисуем пустой прямоугольник если изображение не загрузилось
                    using (var pen = new Pen(Color.Gray, 1))
                    {
                        graphics.DrawRectangle(pen, itemRect);
                    }
                }

                currentX += ItemWidth + Spacing;
                rowHeight = Math.Max(rowHeight, ItemHeight);
            }
        }

        private void DrawImageAccordingToScaleType(Graphics graphics, Image image, Rectangle destRect)
        {
            var contentRect = destRect;

            // Если есть рамка, уменьшаем область для изображения
            if (ShowBorder && BorderWidth > 0)
            {
                contentRect = new Rectangle(
                    destRect.X + BorderWidth,
                    destRect.Y + BorderWidth,
                    destRect.Width - 2 * BorderWidth,
                    destRect.Height - 2 * BorderWidth
                );
            }

            switch (ImageScaleType)
            {
                case ImageScaleType.Stretch:
                    graphics.DrawImage(image, contentRect);
                    break;

                case ImageScaleType.Proportional:
                    float scaleX = (float)contentRect.Width / image.Width;
                    float scaleY = (float)contentRect.Height / image.Height;
                    float scale = Math.Min(scaleX, scaleY);

                    int scaledWidth = (int)(image.Width * scale);
                    int scaledHeight = (int)(image.Height * scale);

                    int offsetX = contentRect.X + (contentRect.Width - scaledWidth) / 2;
                    int offsetY = contentRect.Y + (contentRect.Height - scaledHeight) / 2;

                    graphics.DrawImage(image, offsetX, offsetY, scaledWidth, scaledHeight);
                    break;

                case ImageScaleType.Fill:
                    float fillScaleX = (float)contentRect.Width / image.Width;
                    float fillScaleY = (float)contentRect.Height / image.Height;
                    float fillScale = Math.Max(fillScaleX, fillScaleY);

                    int fillWidth = (int)(image.Width * fillScale);
                    int fillHeight = (int)(image.Height * fillScale);

                    int fillOffsetX = contentRect.X + (contentRect.Width - fillWidth) / 2;
                    int fillOffsetY = contentRect.Y + (contentRect.Height - fillHeight) / 2;

                    graphics.DrawImage(image, fillOffsetX, fillOffsetY, fillWidth, fillHeight);
                    break;
            }
        }

        private void DrawBorder(Graphics graphics, Rectangle rect)
        {
            using (var pen = new Pen(BorderColor, BorderWidth))
            {
                graphics.DrawRectangle(pen, rect);
            }
        }
    }



}
