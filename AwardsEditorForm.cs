using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageConstructorApp
{
    public partial class AwardsEditorForm : Form
    {
        private List<AwardData> awards;
        private ListBox awardsListBox;
        private TextBox textTextBox;
        private TextBox imagePathTextBox;
        private ComboBox imageTypeComboBox;
        private Button browseButton;

        public AwardsEditorForm(List<AwardData> awards)
        {
            this.awards = awards;
            InitializeComponent();
            InitializeComponentUI();
            LoadAwardsList();
        }

        private void InitializeComponentUI()
        {
            this.Text = "Редактор наград";
            this.Size = new Size(500, 400);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            // Список наград
            awardsListBox = new ListBox
            {
                Location = new Point(10, 10),
                Size = new Size(150, 300)
            };
            awardsListBox.SelectedIndexChanged += AwardsListBox_SelectedIndexChanged;

            // Поля редактирования
            var textLabel = new Label { Text = "Текст:", Location = new Point(170, 10), Size = new Size(100, 20) };
            textTextBox = new TextBox { Location = new Point(170, 35), Size = new Size(200, 20) };
            textTextBox.TextChanged += TextTextBox_TextChanged;

            var imagePathLabel = new Label { Text = "Путь к изображению:", Location = new Point(170, 70), Size = new Size(150, 20) };
            imagePathTextBox = new TextBox { Location = new Point(170, 95), Size = new Size(150, 20) };
            browseButton = new Button { Text = "...", Location = new Point(325, 95), Size = new Size(30, 20) };
            browseButton.Click += BrowseButton_Click;

            var imageTypeLabel = new Label { Text = "Тип изображения:", Location = new Point(170, 130), Size = new Size(150, 20) };
            imageTypeComboBox = new ComboBox { Location = new Point(170, 155), Size = new Size(150, 20), DropDownStyle = ComboBoxStyle.DropDownList };
            imageTypeComboBox.Items.AddRange(Enum.GetNames(typeof(AwardImageType)));
            imageTypeComboBox.SelectedIndexChanged += ImageTypeComboBox_SelectedIndexChanged;

            var okButton = new Button { Text = "OK", Location = new Point(200, 320), Size = new Size(75, 30) };
            okButton.Click += (s, e) => this.Close();

            this.Controls.AddRange(new Control[] {
                awardsListBox, textLabel, textTextBox, imagePathLabel, imagePathTextBox,
                browseButton, imageTypeLabel, imageTypeComboBox, okButton
            });
        }

        private void LoadAwardsList()
        {
            awardsListBox.Items.Clear();
            for (int i = 0; i < awards.Count; i++)
            {
                awardsListBox.Items.Add($"Награда {i + 1}: {awards[i].Text}");
            }
            if (awardsListBox.Items.Count > 0)
            {
                awardsListBox.SelectedIndex = 0;
            }
        }

        private void AwardsListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (awardsListBox.SelectedIndex >= 0 && awardsListBox.SelectedIndex < awards.Count)
            {
                var award = awards[awardsListBox.SelectedIndex];
                textTextBox.Text = award.Text ?? "";
                imagePathTextBox.Text = award.ImagePath ?? "";
                var typeName = award.ImageType.ToString();
                if (imageTypeComboBox.Items.Contains(typeName))
                {
                    imageTypeComboBox.SelectedItem = typeName;
                }
            }
        }

        private void TextTextBox_TextChanged(object sender, EventArgs e)
        {
            if (awardsListBox.SelectedIndex >= 0 && awardsListBox.SelectedIndex < awards.Count)
            {
                awards[awardsListBox.SelectedIndex].Text = textTextBox.Text;
                // Обновляем отображение в списке
                awardsListBox.Items[awardsListBox.SelectedIndex] = $"Награда {awardsListBox.SelectedIndex + 1}: {textTextBox.Text}";
            }
        }

        private void BrowseButton_Click(object sender, EventArgs e)
        {
            using (var dialog = new OpenFileDialog())
            {
                dialog.Filter = "Изображения|*.jpg;*.jpeg;*.png;*.bmp|Все файлы|*.*";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    imagePathTextBox.Text = dialog.FileName;
                    if (awardsListBox.SelectedIndex >= 0 && awardsListBox.SelectedIndex < awards.Count)
                    {
                        awards[awardsListBox.SelectedIndex].ImagePath = dialog.FileName;
                    }
                }
            }
        }

        private void ImageTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (awardsListBox.SelectedIndex >= 0 && awardsListBox.SelectedIndex < awards.Count && imageTypeComboBox.SelectedItem != null)
            {
                awards[awardsListBox.SelectedIndex].ImageType = (AwardImageType)Enum.Parse(typeof(AwardImageType), imageTypeComboBox.SelectedItem.ToString());
            }
        }
    }
}
