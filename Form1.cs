using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TextEditor
{
    public partial class Form1 : Form
    {
        /// <summary>
        /// Индекс для документов.
        /// </summary>
        private int indexForDocs = 1;;

        /// <summary>
        /// Цвет фона.
        /// </summary>
        private Color mainBackColor = Color.White;

        /// <summary>
        /// Частота автосохранения.
        /// </summary>
        private int mainFreq = -1;

        /// <summary>
        /// Конструктор основной формы.
        /// </summary>
        public Form1()
        {
            InitializeComponent();
            timer.Stop();
            int r = 255;
            int g = 255;
            int b = 255;
            int freq = -1;

            ReadingOptions(ref r, ref g, ref b, ref freq);
            ReadingRecentTabs();

            if (freq != -1)
            {
                mainFreq = freq;
                if (freq == 15000)
                {
                    autoSaveInfoLabel.Text = "AutoSave Interval: 15 secs";
                }
                else if (freq == 30000)
                {
                    autoSaveInfoLabel.Text = "AutoSave Interval: 30 secs";
                }
                else if (freq == 45000)
                {
                    autoSaveInfoLabel.Text = "AutoSave Interval: 45 secs";
                }
                else if (freq == 60000)
                {
                    autoSaveInfoLabel.Text = "AutoSave Interval: 1 min";
                }
                else if (freq == 300000)
                {
                    autoSaveInfoLabel.Text = "AutoSave Interval: 5 min";
                }
                else if (freq == 600000)
                {
                    autoSaveInfoLabel.Text = "AutoSave Interval: 10 min";
                }
                timer.Start();
            }

            ChangeTheme(mainBackColor, Color.Black);
        }

        /// <summary>
        /// Чтение недавно закрытых вкладок.
        /// </summary>
        private void ReadingRecentTabs()
        {
            try
            {
                string[] tabs = File.ReadAllLines("Tabs.txt");
                foreach (string tab in tabs)
                {
                    if (File.Exists(tab))
                    {
                        OpenRecentTabs(tab);
                    }
                }

            }
            catch (Exception)
            {
                MessageBox.Show("Some unexpected error with tabs", "Error");
            }
        }

        /// <summary>
        /// Чтение настроек.
        /// </summary>
        /// <param name="r">R.</param>
        /// <param name="g">G.</param>
        /// <param name="b">B.</param>
        /// <param name="freq">Частота автосохранений.</param>
        private void ReadingOptions(ref int r, ref int g, ref int b, ref int freq)
        {
            try
            {
                string[] lines = File.ReadAllLines("Options.txt");
                freq = int.Parse(lines[0]);
                string[] rgb = lines[1].Split(", ");
                r = int.Parse(rgb[0]);
                g = int.Parse(rgb[1]);
                b = int.Parse(rgb[2]);
                mainBackColor = Color.FromArgb(r, g, b);
            }
            catch (Exception)
            {
                MessageBox.Show("Some unexpected error with options", "Error");
            }
        }

        /// <summary>
        /// Открытие недавного вкладки.
        /// </summary>
        /// <param name="filePath">Путь к файлу.</param>
        private void OpenRecentTabs(string filePath)
        {
            // Опендайлог чтобы было удобней узнать путь и название файла.
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.FileName = filePath;
            string fileContent = File.ReadAllText(filePath);
            TabPage newTab = new TabPage();
            RichTextBox rtb = new RichTextBox();
            rtb.MouseDown += new System.Windows.Forms.MouseEventHandler(this.RichTextBox_MouseDown);

            rtb.Dock = DockStyle.Fill;
            newTab.Controls.Add(rtb);

            tabControl1.TabPages.Add(newTab);
            newTab.Text = openFile.SafeFileName;
            newTab.Name = openFile.FileName;
            tabControl1.SelectTab(newTab);

            // Открытие вкладок.
            try
            {
                if (Path.GetExtension(filePath) == ".rtf")
                {
                    ((RichTextBox)tabControl1.SelectedTab.Controls[0]).Rtf = fileContent;
                }
                else
                {
                    ((RichTextBox)tabControl1.SelectedTab.Controls[0]).Text = fileContent;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Incorrect file format!");
            }
        }

        /// <summary>
        /// Кнопка вырезать.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ((RichTextBox)tabControl1.SelectedTab.Controls[0]).Cut();
        }

        /// <summary>
        /// Кнопка копировать.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ((RichTextBox)tabControl1.SelectedTab.Controls[0]).Copy();
        }

        /// <summary>
        /// Кнопка вставить.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ((RichTextBox)tabControl1.SelectedTab.Controls[0]).Paste();
        }

        /// <summary>
        /// Кнопка выделить все.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SelectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ((RichTextBox)tabControl1.SelectedTab.Controls[0]).SelectAll();
        }

        /// <summary>
        /// Кнопка назад.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UndoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ((RichTextBox)tabControl1.SelectedTab.Controls[0]).Undo();
        }

        /// <summary>
        /// Кнопка вперед.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RedoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ((RichTextBox)tabControl1.SelectedTab.Controls[0]).Redo();
        }

        /// <summary>
        /// Кнопка выхода.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Кнопка сохранить как.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveAs = new SaveFileDialog();

            saveAs.Filter = "Rich Text Format|*.rtf|Text file|*.txt|C# Script|*.cs"; ;
            saveAs.ShowDialog();

            if (saveAs.FileName != "")
            {
                StreamWriter file = new StreamWriter(saveAs.OpenFile());

                switch (saveAs.FilterIndex)
                {
                    case 1:
                        file.WriteLine(((RichTextBox)tabControl1.SelectedTab.Controls[0]).Rtf);
                        file.Close();
                        break;

                    case 2:
                        file.WriteLine(((RichTextBox)tabControl1.SelectedTab.Controls[0]).Text);
                        file.Close();
                        break;
                }
            }

            tabControl1.SelectedTab.Name = saveAs.FileName;
        }

        /// <summary>
        /// Кнопка открытия нового документа.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TabPage tabPage = new TabPage($"New Document {indexForDocs}");
            RichTextBox richTextBox = new RichTextBox();
            richTextBox.BackColor = mainBackColor;
            richTextBox.Dock = DockStyle.Fill;
            richTextBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.RichTextBox_MouseDown);

            tabPage.Controls.Add(richTextBox);
            tabControl1.TabPages.Add(tabPage);
            indexForDocs++;
        }

        /// <summary>
        /// Кнопка открытия файла.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();

            string PATH;
            string fileContent = "";
            openFile.InitialDirectory = "c:\\";
            openFile.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            openFile.FilterIndex = 2;

            if (openFile.ShowDialog() == DialogResult.OK)
            {
                PATH = openFile.FileName;
                var fileStream = openFile.OpenFile();
                TabPage newTab = new TabPage();
                RichTextBox rtb = new RichTextBox();
                rtb.BackColor = mainBackColor;
                rtb.Dock = DockStyle.Fill;
                rtb.MouseDown += new System.Windows.Forms.MouseEventHandler(this.RichTextBox_MouseDown);
                newTab.Controls.Add(rtb);
                tabControl1.TabPages.Add(newTab);
                newTab.Text = openFile.SafeFileName;
                newTab.Name = openFile.FileName;
                tabControl1.SelectTab(tabControl1.TabPages.Count - 1);

                using (StreamReader reader = new StreamReader(fileStream))
                {
                    fileContent = reader.ReadToEnd();
                }

                try
                {
                    if (Path.GetExtension(PATH) == ".rtf")
                    {
                        ((RichTextBox)tabControl1.SelectedTab.Controls[0]).Rtf = fileContent;
                    }
                    else
                    {
                        ((RichTextBox)tabControl1.SelectedTab.Controls[0]).Text = fileContent;
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Incorrect file format!");
                }

            }
        }

        /// <summary>
        /// Кнопка сохранить.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (File.Exists(tabControl1.SelectedTab.Name)) // Иф чтобы проверить существует ли файл.
            {
                int count = tabControl1.SelectedTab.Name.Split(".").Length;
                if (tabControl1.SelectedTab.Name.Split(".")[count - 1] == "rtf")
                {
                    StreamWriter file = new StreamWriter(tabControl1.SelectedTab.Name);
                    file.WriteLine(((RichTextBox)tabControl1.SelectedTab.Controls[0]).Rtf);
                    file.Close();
                }
                else
                {
                    StreamWriter file = new StreamWriter(tabControl1.SelectedTab.Name);
                    file.WriteLine(((RichTextBox)tabControl1.SelectedTab.Controls[0]).Text);
                    file.Close();
                }
            }
            else
            {
                SaveAsToolStripMenuItem_Click(sender, e);
            }
        }

        /// <summary>
        /// Действия при закрытие формы.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Сохранение настроек.
            File.WriteAllText("Options.txt", $"{mainFreq}\r\n{mainBackColor.R}, {mainBackColor.G}, {mainBackColor.B}");

            // Сохранение вкладок.
            int count = tabControl1.TabPages.Count;
            string[] paths = new string[count];
            for (int i = 0; i < count; i++)
            {
                tabControl1.SelectTab(i);
                paths[i] = tabControl1.SelectedTab.Name;
            }
            File.WriteAllLines("Tabs.txt", paths);

            // Сохранить все или выйти.
            var res = MessageBox.Show("Do you want to save file(s)?", "Save file", MessageBoxButtons.YesNoCancel);
            if (res == DialogResult.Yes)
            {
                SaveAllFiles(sender, e);
                count = tabControl1.TabPages.Count;
                paths = new string[count];
                for (int i = 0; i < count; i++)
                {
                    tabControl1.SelectTab(i);
                    paths[i] = tabControl1.SelectedTab.Name;
                }
                File.WriteAllLines("Tabs.txt", paths);
            }
            else if (res == DialogResult.Cancel)
            {
                e.Cancel = true;
            }
        }

        /// <summary>
        /// Кнопка сохранения всех файлов.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveAllFiles(object sender, EventArgs e)
        {
            for (int i = 0; i < tabControl1.TabCount; i++)
            {
                tabControl1.SelectTab(i);

                if (File.Exists(tabControl1.SelectedTab.Name)) // Иф чтобы проверить существует ли файл.
                {
                    string[] formats = tabControl1.SelectedTab.Name.Split(".");
                    string format = formats[formats.Length - 1];
                    try
                    {
                        if (format == "rtf") // Формат РТФ.
                        {
                            File.WriteAllText(tabControl1.SelectedTab.Name, ((RichTextBox)tabControl1.SelectedTab.Controls[0]).Rtf);
                        }
                        else // Любой другой формат.
                        {
                            File.WriteAllText(tabControl1.SelectedTab.Name, ((RichTextBox)tabControl1.SelectedTab.Controls[0]).Text);
                        }
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Failed to save a file due to incorrect file format.");
                    }
                }
                else
                {
                    // Если файла не существует сохраняем его в папке bin.
                    File.WriteAllText($"{tabControl1.SelectedTab.Text}.txt", ((RichTextBox)tabControl1.SelectedTab.Controls[0]).Text);
                    tabControl1.SelectedTab.Name = $"{tabControl1.SelectedTab.Text}.txt";
                }
            }
            MessageBox.Show("All your files were saved and Files created in bin folder if it weren't created before.", "Files saved");
        }

        /// <summary>
        /// Изменение формата шрифта.
        /// </summary>
        private void ChangeFont()
        {
            FontDialog fd = new FontDialog();
            fd.ShowColor = true;
            if (fd.ShowDialog() == DialogResult.OK)
            {
                ((RichTextBox)tabControl1.SelectedTab.Controls[0]).SelectionColor = fd.Color;
                ((RichTextBox)tabControl1.SelectedTab.Controls[0]).SelectionFont = fd.Font;
            }
        }

        /// <summary>
        /// Кнопка помощи.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HelpToolStripButton_Click(object sender, EventArgs e)
        {
            Help help = new Help();
            help.BackColor = mainBackColor;
            help.Show();
        }
        
        /// <summary>
        /// Кнопка изменения формата текста.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditTextButton_Click(object sender, EventArgs e)
        {
            ChangeFont();
        }

        /// <summary>
        /// Кнопка изменения формата текста.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CustomizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ((RichTextBox)tabControl1.SelectedTab.Controls[0]).SelectAll();
            ChangeFont();
        }

        /// <summary>
        /// Контекстное меню.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RichTextBox_MouseDown(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Right:
                {
                    contextMenuStrip1.Show(this, new Point(e.X + 10, e.Y + 45));
                }
                break;
            }
        }

        /// <summary>
        /// Кнопка закрытия вкладки.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CloseTabButton_Click(object sender, EventArgs e)
        {
            int count = tabControl1.TabPages.Count;
            if (count != 1) // Если вкладка не одна сохраняем ее не закрывая потом форму.
            {
                var message = MessageBox.Show("Do you want to save file?", "Save file", MessageBoxButtons.YesNoCancel);
                if (message == DialogResult.Yes)
                {
                    SaveToolStripMenuItem_Click(sender, e);
                    var current = tabControl1.SelectedIndex;
                    tabControl1.TabPages.Remove(tabControl1.TabPages[current]);
                }
                else if (message == DialogResult.No)
                {
                    var current = tabControl1.SelectedIndex;
                    tabControl1.TabPages.Remove(tabControl1.TabPages[current]);
                }
            }
            else // закрываем форму после сохранения.
            {
                this.Close();
            }
        }

        /// <summary>
        /// Кнопка открывающее новое окно.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NewWindow_Click(object sender, EventArgs e)
        {
            File.WriteAllText("Tabs.txt", "");
            Form1 form = new Form1();
            form.Show();
        }

        /// <summary>
        /// Кнопка закрытия вкладки.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CloseCurrTabButton_Click(object sender, EventArgs e)
        {
            CloseTabButton_Click(sender, e);
        }

        /// <summary>
        /// Тик таймера.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Timer_Tick(object sender, EventArgs e)
        {
            timer.Stop();

            SaveAllFiles(sender, e);

            timer.Start();
        }

        /// <summary>
        /// Кнопка нет автосохранения.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NoAutoSave_Click(object sender, EventArgs e)
        {
            autoSaveInfoLabel.Text = "AutoSave Interval: No";
            mainFreq = -1;
            timer.Stop();
        }

        /// <summary>
        /// Кнопка 15 сек автосохранение.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FifteenAutoSave_Click(object sender, EventArgs e)
        {
            autoSaveInfoLabel.Text = "AutoSave Interval: 15 secs";
            timer.Interval = 15000;
            mainFreq = 15000;
            timer.Start();
        }

        /// <summary>
        /// Кнопка для 30 сек автосохранение.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ThirtyAutoSave_Click(object sender, EventArgs e)
        {
            autoSaveInfoLabel.Text = "AutoSave Interval: 30 secs";
            timer.Interval = 30000;
            mainFreq = 30000;
            timer.Start();
        }

        /// <summary>
        /// Кнопка для 45 сек автосохранение.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FourtyFiveAutoSave_Click(object sender, EventArgs e)
        {
            autoSaveInfoLabel.Text = "AutoSave Interval: 45 secs";
            timer.Interval = 45000;
            mainFreq = 45000;
            timer.Start();
        }

        /// <summary>
        /// Кнопка для 1 минутного автосохранение.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OneMinuteAutoSave_Click(object sender, EventArgs e)
        {
            autoSaveInfoLabel.Text = "AutoSave Interval: 1 min";
            timer.Interval = 60000;
            mainFreq = 60000;
            timer.Start();
        }

        /// <summary>
        /// Кнопка для 5 минутного автосохранение.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FiveMinuteAutoSave_Click(object sender, EventArgs e)
        {
            autoSaveInfoLabel.Text = "AutoSave Interval: 5 min";
            timer.Interval = 300000;
            mainFreq = 300000;
            timer.Start();
        }

        /// <summary>
        /// Кнопка для 10 минутного автосохранение.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TenMinuteAutoSave_Click(object sender, EventArgs e)
        {
            autoSaveInfoLabel.Text = "AutoSave Interval: 10 min";
            timer.Interval = 600000;
            mainFreq = 600000;
            timer.Start();
        }

        /// <summary>
        /// Белая (классическая) тема.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LightTheme_Click(object sender, EventArgs e)
        {
            mainBackColor = Color.White;
            ChangeTheme(Color.White, Color.Black);
        }

        /// <summary>
        /// Ярко желтая тема.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LightYellowTheme_Click(object sender, EventArgs e)
        {
            mainBackColor = Color.FromArgb(235, 255, 179);
            ChangeTheme(Color.FromArgb(235, 255, 179), Color.Black);
        }

        /// <summary>
        /// Ярко зеленая тема.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LightGreenTheme_Click(object sender, EventArgs e)
        {
            mainBackColor = Color.FromArgb(179, 255, 179);
            ChangeTheme(Color.FromArgb(179, 255, 179), Color.Black);
        }

        /// <summary>
        /// Ярко голубая тема.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LightBlueTheme_Click(object sender, EventArgs e)
        {
            mainBackColor = Color.FromArgb(179, 240, 255);
            ChangeTheme(Color.FromArgb(179, 240, 255), Color.Black);
        }

        /// <summary>
        /// Метод чтобы поменять тему.
        /// </summary>
        /// <param name="backColor"></param>
        /// <param name="foreColor"></param>
        private void ChangeTheme(Color backColor, Color foreColor)
        {
            this.BackColor = backColor;
            menuStrip.BackColor = backColor;
            fileToolStripMenuItem.BackColor = backColor;
            editToolStripMenuItem.BackColor = backColor;
            toolsToolStripMenuItem.BackColor = backColor;
            toolStrip1.BackColor = backColor;
            toolStrip1.BackColor = backColor;
            autoSaveInfoLabel.BackColor = backColor;
            closeCurrTabButton.BackColor = backColor;

            this.ForeColor = foreColor;
            menuStrip.ForeColor = foreColor;
            fileToolStripMenuItem.ForeColor = foreColor;
            editToolStripMenuItem.ForeColor = foreColor;
            toolsToolStripMenuItem.ForeColor = foreColor;
            toolStrip1.ForeColor = foreColor;
            autoSaveInfoLabel.ForeColor = foreColor;
            closeCurrTabButton.ForeColor = foreColor;

            int count = ChamgeColorOfItems(backColor, foreColor);

            count = tabControl1.TabPages.Count;

            for (int i = 0; i < count; i++)
            {
                tabControl1.SelectTab(i);
                ((RichTextBox)tabControl1.SelectedTab.Controls[0]).BackColor = backColor;
                ((RichTextBox)tabControl1.SelectedTab.Controls[0]).ForeColor = foreColor;
            }
        }

        /// <summary>
        /// Метод чтобы изменить цвета объектов в меню.
        /// </summary>
        /// <param name="backColor"></param>
        /// <param name="foreColor"></param>
        /// <returns></returns>
        private int ChamgeColorOfItems(Color backColor, Color foreColor)
        {
            int count = fileToolStripMenuItem.DropDownItems.Count;
            for (int i = 0; i < count; i++)
            {
                fileToolStripMenuItem.DropDownItems[i].BackColor = backColor;
                fileToolStripMenuItem.DropDownItems[i].ForeColor = foreColor;
            }
            count = editToolStripMenuItem.DropDownItems.Count;
            for (int i = 0; i < count; i++)
            {
                editToolStripMenuItem.DropDownItems[i].BackColor = backColor;
                editToolStripMenuItem.DropDownItems[i].ForeColor = foreColor;
            }
            count = toolsToolStripMenuItem.DropDownItems.Count;
            for (int i = 0; i < count; i++)
            {
                toolsToolStripMenuItem.DropDownItems[i].BackColor = backColor;
                toolsToolStripMenuItem.DropDownItems[i].ForeColor = foreColor;
            }
            count = optionsToolStripMenuItem.DropDownItems.Count;
            for (int i = 0; i < count; i++)
            {
                optionsToolStripMenuItem.DropDownItems[i].BackColor = backColor;
                optionsToolStripMenuItem.DropDownItems[i].ForeColor = foreColor;
            }
            count = autoSaveFreq.DropDownItems.Count;
            for (int i = 0; i < count; i++)
            {
                autoSaveFreq.DropDownItems[i].BackColor = backColor;
                autoSaveFreq.DropDownItems[i].ForeColor = foreColor;
            }
            count = theme.DropDownItems.Count;
            for (int i = 0; i < count; i++)
            {
                theme.DropDownItems[i].BackColor = backColor;
                theme.DropDownItems[i].ForeColor = foreColor;
            }

            return count;
        }
    }
}

