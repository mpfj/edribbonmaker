using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Xml;

namespace ED_Ribbon_Maker
{
    public partial class FormRibbonMaker : Form
    {
        const int MAX_RIBBONS_WIDE = 10;
        const int MAX_RIBBONS_HIGH = 10;
        const int DEFAULT_RIBBON_WIDTH = 260;
        const int DEFAULT_RIBBON_HEIGHT = 74;

        const string RIBBON_SUFFIX = "-ribbon-260.png";

        bool dragFromSource = false;

        Color colourCount = Color.Black;
        Font fontCount = new Font("Arial", 42, FontStyle.Bold);

        string dirLoadSave = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

        public FormRibbonMaker()
        {
            InitializeComponent();
        }

        private void buttonSourceDirectory_Click(object sender, EventArgs e)
        {
            try
            {
                // create new directory dialog
                FolderBrowserDialog dlg = new FolderBrowserDialog();
                // set current dirctory
                dlg.SelectedPath = textBoxRibbonSource.Text;
                // show dialog
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    // update path
                    textBoxRibbonSource.Text = dlg.SelectedPath;

                    // rescan directory
                    buttonRibbonRescan_Click(sender, e);
                }
            }
            catch (Exception)
            {
            }
        }

        private void FormRibbonMaker_Load(object sender, EventArgs e)
        {
            // init size spinners
            numericRibbonWidth.Maximum = MAX_RIBBONS_WIDE;
            numericRibbonHeight.Maximum = MAX_RIBBONS_HIGH;

            // init preview font and colour
            labelPreviewImage.ForeColor = colourCount;
            labelPreviewImage.Font = fontCount;

            // create ribbon image grid
            for (int row = 0; row < MAX_RIBBONS_HIGH; row++)
            {
                for (int col = 0; col < MAX_RIBBONS_WIDE; col++)
                {
                    // create ribbon label
                    Label l = new Label();
                    l.Margin = new Padding(0);
                    l.BorderStyle = BorderStyle.None;
                    l.Width = DEFAULT_RIBBON_WIDTH;
                    l.Height = DEFAULT_RIBBON_HEIGHT;
                    l.Left = col * DEFAULT_RIBBON_WIDTH;
                    l.Top = row * DEFAULT_RIBBON_HEIGHT;
                    l.Visible = true;
                    l.Name = "Count" + ((row * MAX_RIBBONS_WIDE) + col).ToString();
                    l.Text = "";
                    l.ForeColor = colourCount;
                    l.Font = fontCount;
                    l.TextAlign = ContentAlignment.MiddleCenter;
                    panelRibbonsImage.Controls.Add(l);
                }
            }

            // show visible ribbons
            RefreshVisibleRibbons(sender, e);

            // init source ribbon list

            // init selected ribbon list

            // update main ribbon panel
            UpdateRibbonPanel(true, true, true, true);
        }

        private void RefreshVisibleRibbons(object sender, EventArgs e)
        {
            try
            {
                for (decimal row = 0; row < numericRibbonHeight.Value; row++)
                {
                    for (decimal col = 0; col < numericRibbonWidth.Value; col++)
                    {
                        int idx = panelRibbonsImage.Controls.IndexOfKey("Count" + ((row * MAX_RIBBONS_WIDE) + col).ToString());
                        Label l = (Label)panelRibbonsImage.Controls[idx];
                        l.Visible = true;
                        l.BringToFront();
                    }

                    for (decimal col = numericRibbonWidth.Value; col < MAX_RIBBONS_WIDE; col++)
                    {
                        int idx = panelRibbonsImage.Controls.IndexOfKey("Count" + ((row * MAX_RIBBONS_WIDE) + col).ToString());
                        Label l = (Label)panelRibbonsImage.Controls[idx];
                        l.Visible = false;
                    }
                }
                for (decimal row = numericRibbonHeight.Value; row < MAX_RIBBONS_HIGH; row++)
                {
                    for (decimal col = 0; col < MAX_RIBBONS_WIDE; col++)
                    {
                        int idx = panelRibbonsImage.Controls.IndexOfKey("Count" + ((row * MAX_RIBBONS_WIDE) + col).ToString());
                        Label l = (Label)panelRibbonsImage.Controls[idx];
                        l.Visible = false;
                    }
                }

                // update main ribbon image
                UpdateRibbonPanel(true, true, true, true);
            }
            catch (Exception)
            {
            }
        }

        private void listViewSelected_ItemDrag(object sender, ItemDragEventArgs e)
        {
            dragFromSource = false;
            DoDragDrop(e.Item, DragDropEffects.Move);
        }

        private void listViewSelected_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.All;
        }

        private void listViewSelected_DragDrop(object sender, DragEventArgs e)
        {
            // ensure we only get items from the other list
            if (dragFromSource == false)
                return;

            try
            {
                if (e.Data.GetDataPresent(typeof(ListViewItem)))
                {
                    ListViewItem item = (ListViewItem)e.Data.GetData(typeof(ListViewItem));
                    ListViewItem newitem = listViewSelected.Items.Add(item.Text);
                    newitem.SubItems.Add("0");
                    newitem.SubItems.Add(GetRibbonFilename(item.Text));

                    // update main ribbon image
                    UpdateRibbonPanel(true, true, true, true);
                }
            }
            catch (Exception)
            {
            }
        }

        private void listViewFull_ItemDrag(object sender, ItemDragEventArgs e)
        {
            dragFromSource = true;
            DoDragDrop(e.Item, DragDropEffects.Move);
        }

        private void listViewFull_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.All;
        }

        private void listViewFull_DragDrop(object sender, DragEventArgs e)
        {
            // ensure we only get items from the other list
            if (dragFromSource == true)
                return;

            try
            {
                if (e.Data.GetDataPresent(typeof(ListViewItem)))
                {
                    ListViewItem item = (ListViewItem)e.Data.GetData(typeof(ListViewItem));
                    listViewSelected.Items.Remove(item);

                    // update main ribbon image
                    UpdateRibbonPanel(true, true, true, true);
                }
            }
            catch (Exception)
            {
            }
        }

        private void buttonRibbonRescan_Click(object sender, EventArgs e)
        {
            try
            {
                // scan source directory for images ... "*-ribbon-260.png"
                string[] files = Directory.GetFiles(textBoxRibbonSource.Text, "*" + RIBBON_SUFFIX);

                // clear any items
                listViewFull.Items.Clear();

                // add each filename to the ribbon list (minus the suffix)
                foreach (string filename in files)
                {
                    string name = Path.GetFileNameWithoutExtension(filename);
                    // add name without suffix
                    listViewFull.Items.Add(name.Substring(0, name.Length - 11));
                }

                // update column width
                listViewFull.Columns[0].AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
            }
            catch (Exception)
            {
            }
        }

        private void listViewFull_SelectedIndexChanged(object sender, EventArgs e)
        {
            // do nothing if no items selected
            if (listViewFull.SelectedItems.Count == 0)
                return;

            try
            {
                // preview selected ribbon
                LoadPicture(labelPreviewImage, listViewFull.SelectedItems[0].Text);
            }
            catch (Exception)
            {
            }
        }

        private void UpdateRibbonPanel(bool updatePicture, bool updateCount, bool updateColour, bool updateFont)
        {
            int index = 0;

            for (decimal row = 0; row < numericRibbonHeight.Value; row++)
            {
                for (decimal col = 0; col < numericRibbonWidth.Value; col++, index++)
                {
                    // get count grid control
                    int idxC = panelRibbonsImage.Controls.IndexOfKey("Count" + ((row * MAX_RIBBONS_WIDE) + col).ToString());
                    Label l = (Label)panelRibbonsImage.Controls[idxC];

                    // get next selected ribbon
                    if (index >= listViewSelected.Items.Count)
                    {
                        // remove any existing picture
                        l.Image = null;
                        // clear count
                        l.Text = "";
                    }
                    else
                    {
                        // update ribbon image ?
                        if (updatePicture)
                        {
                            LoadPicture(l, listViewSelected.Items[index]);
                        }
                        // update count ?
                        if (updateCount)
                        {
                            SetCount(l, int.Parse(listViewSelected.Items[index].SubItems[1].Text));
                        }
                        // update colour ?
                        if (updateColour)
                        {
                            l.ForeColor = colourCount;
                        }
                        // update font ?
                        if (updateFont)
                        {
                            l.Font = fontCount;
                        }
                    }
                }
            }
        }

        private string GetRibbonFilename(string name)
        {
            return textBoxRibbonSource.Text + "\\" + name + RIBBON_SUFFIX;
        }

        private void LoadPicture(Label l, ListViewItem item)
        {
            if (item.SubItems.Count > 2)
            {
                Image i = Image.FromFile(item.SubItems[2].Text);
                l.Image = i;
            }
            else
            {
                l.Image = null;
            }
        }

        private void LoadPicture(Label l, string name)
        {
            Image i = Image.FromFile(GetRibbonFilename(name));
            l.Image = i;
        }

        private void pictureBoxPreview_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < listViewSelected.Items.Count ; i++)
                Console.WriteLine("item check" + i.ToString() + " = " + listViewSelected.Items[i].Selected.ToString());
        }

        private void buttonMoveUp_Click(object sender, EventArgs e)
        {
            // if nothing selected, quit
            if (listViewSelected.SelectedItems.Count == 0)
                return;

            ListViewItem item = listViewSelected.SelectedItems[0];
            int index = item.Index;

            // if not already at top
            if (index != 0)
            {
                // move item up one
                listViewSelected.Items.Remove(item);
                listViewSelected.Items.Insert(index - 1, item);
                listViewSelected.Items[index - 1].Selected = true;

                // update main ribbon image
                UpdateRibbonPanel(true, true, false, false);
            }
        }

        private void buttonDown_Click(object sender, EventArgs e)
        {
            // if nothing selected, quit
            if (listViewSelected.SelectedItems.Count == 0)
                return;

            ListViewItem item = listViewSelected.SelectedItems[0];
            int index = item.Index;

            // if not already at bottom
            if (index != (listViewSelected.Items.Count - 1))
            {
                // move item down
                listViewSelected.Items.RemoveAt(index);
                listViewSelected.Items.Insert(index + 1, item);
                listViewSelected.Items[index + 1].Selected = true;

                // update main ribbon image
                UpdateRibbonPanel(true, true, true, true);
            }
        }

        private void listViewSelected_SelectedIndexChanged(object sender, EventArgs e)
        {
            // do nothing if no items selected
            if (listViewSelected.SelectedItems.Count == 0)
                return;

            try
            {
                // get selected item
                ListViewItem item = listViewSelected.SelectedItems[0];

                // preview selected ribbon
                LoadPicture(labelPreviewImage, item);

                // populate count spinner
                numericUpDownCount.Value = int.Parse(item.SubItems[1].Text);
            }
            catch (Exception)
            {
            }
        }

        private void numericUpDownCount_ValueChanged(object sender, EventArgs e)
        {
            // do nothing if no items selected
            if (listViewSelected.SelectedItems.Count == 0)
                return;

            try
            {
                // get selected item
                ListViewItem item = listViewSelected.SelectedItems[0];

                // update count 
                item.SubItems[1].Text = numericUpDownCount.Value.ToString();

                // update ribbon image
                int index = item.Index;
                index = ((index / (int)numericRibbonWidth.Value) * MAX_RIBBONS_WIDE) + (index % (int)numericRibbonWidth.Value);
                int idxC = panelRibbonsImage.Controls.IndexOfKey("Count" + index.ToString());
                Label l = (Label)panelRibbonsImage.Controls[idxC];
                SetCount(l, int.Parse(item.SubItems[1].Text));
            }
            catch (Exception e2)
            {
                Console.WriteLine(e2.ToString());
            }
        }

        private void SetCount(Label lbl, int count)
        {
            try
            {
                // blank count ?
                if (count == 0)
                {
                    lbl.Text = "";
                }
                // +ve count ?
                else if (count > 0)
                {
                    lbl.Text = count.ToString();
                }
                // -ve count ... set to "*"s
                else
                {
                    lbl.Text = new string('★', -count);
                }
            }
            catch (Exception)
            {
            }
        }

        private void buttonColour_Click(object sender, EventArgs e)
        {
            try
            {
                // create new colour dialog
                ColorDialog dlg = new ColorDialog();
                // set current colour
                dlg.Color = colourCount;
                // show dialog
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    // update colour
                    colourCount = dlg.Color;

                    // update main ribbon image
                    UpdateRibbonPanel(false, false, true, false);

                    // update preview image
                    labelPreviewImage.ForeColor = colourCount;
                }
            }
            catch (Exception)
            {
            }
        }

        private void buttonFont_Click(object sender, EventArgs e)
        {
            try
            {
                // create new font dialog
                FontDialog dlg = new FontDialog();
                // set current font
                dlg.Font = fontCount;
                // show dialog
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    // update font
                    fontCount = dlg.Font; 

                    // update main ribbon image
                    UpdateRibbonPanel(false, false, false, true);

                    // update preview image
                    labelPreviewImage.Font = fontCount;
                }
            }
            catch (Exception)
            {
            }
        }

        private void buttonSaveConfig_Click(object sender, EventArgs e)
        {
            try
            {
                // create new save dialog
                SaveFileDialog dlg = new SaveFileDialog();
                // set initial directory
                dlg.InitialDirectory = dirLoadSave;
                // show dialog
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        // save xml config
                        XmlWriterSettings xs = new XmlWriterSettings();
                        xs.Indent = true;
                        XmlWriter xw = XmlWriter.Create(dlg.FileName, xs);

                        // write header
                        xw.WriteStartDocument();
                        xw.WriteStartElement("edribbonmaker");

                        // write version
                        xw.WriteElementString("version", "1v0");

                        // save width & height
                        xw.WriteElementString("width", numericRibbonWidth.Value.ToString());
                        xw.WriteElementString("height", numericRibbonHeight.Value.ToString());

                        // save list of ribbons
                        xw.WriteStartElement("ribbons");
                        foreach (ListViewItem item in listViewSelected.Items)
                        {
                            xw.WriteStartElement("ribbon");
                            xw.WriteElementString("name", item.SubItems[0].Text);
                            xw.WriteElementString("count", item.SubItems[1].Text);
                            if (item.SubItems.Count > 2)
                            {
                                xw.WriteElementString("path", item.SubItems[2].Text);
                            }
                            xw.WriteEndElement();
                        }
                        xw.WriteEndElement();

                        // save colour
                        xw.WriteElementString("colour", colourCount.ToArgb().ToString("X").Substring(2, 6));

                        // save font
                        xw.WriteStartElement("font");
                        xw.WriteElementString("family", fontCount.FontFamily.Name.ToString());
                        xw.WriteElementString("size", fontCount.Size.ToString());
                        xw.WriteElementString("style", fontCount.Style.GetHashCode().ToString());
                        xw.WriteEndElement();

                        xw.WriteEndElement();
                        xw.Flush();
                        xw.Close();
                    }
                    catch (Exception)
                    {
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error saving config file");
            }
        }

        private void buttonLoadConfig_Click(object sender, EventArgs e)
        {
            try
            {
                // create new open dialog
                OpenFileDialog dlg = new OpenFileDialog();
                // set initial directory
                dlg.InitialDirectory = dirLoadSave;
                // show dialog
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        // open xml config
                        XmlDocument xd = new XmlDocument();
                        xd.Load(dlg.FileName);

                        // make sure this is one of our config files
                        XmlNode node = xd.SelectSingleNode("edribbonmaker");
                        if (node == null)
                        {
                            MessageBox.Show("Invalid config file");
                            return;
                        }

                        // scan all the children
                        foreach (XmlNode xn in node.ChildNodes)
                        {
                            switch (xn.Name)
                            {
                                case "version":
                                    break;
                                case "width":
                                    numericRibbonWidth.Value = int.Parse(xn.InnerText);
                                    break;
                                case "height":
                                    numericRibbonHeight.Value = int.Parse(xn.InnerText);
                                    break;
                                case "ribbons":
                                    // clear existing selected ribbon list
                                    listViewSelected.Items.Clear();

                                    // add new ribbons
                                    foreach (XmlNode xn2 in xn.ChildNodes)
                                    {
                                        // make sure we have all required info
                                        XmlNode xname = xn2.SelectSingleNode("name");
                                        XmlNode xcount = xn2.SelectSingleNode("count");
                                        XmlNode xpath = xn2.SelectSingleNode("path");
                                        if ((xname != null) && (xcount != null) && (xpath != null))
                                        {
                                            // create ribbon entry
                                            ListViewItem item = new ListViewItem(xname.InnerText);
                                            item.SubItems.Add(xcount.InnerText);
                                            item.SubItems.Add(xpath.InnerText);

                                            // add to ribbon list
                                            listViewSelected.Items.Add(item);
                                        }
                                    }
                                    break;
                                case "colour":
                                    colourCount = ColorTranslator.FromHtml("#" + xn.InnerText);
                                    break;
                                case "font":
                                    // make sure we have all required info
                                    XmlNode xfamily = xn.SelectSingleNode("family");
                                    XmlNode xsize = xn.SelectSingleNode("size");
                                    XmlNode xstyle = xn.SelectSingleNode("style");
                                    if ((xfamily != null) && (xsize != null) && (xstyle != null))
                                    {
                                        // set new font
                                        fontCount = new Font(new FontFamily(xfamily.InnerText), int.Parse(xsize.InnerText), (FontStyle)int.Parse(xstyle.InnerText));
                                    }
                                    break;
                            }
                        }

                        // update main ribbon image
                        UpdateRibbonPanel(true, true, true, true);
                    }
                    catch (Exception)
                    {
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error loading config file");
            }

        }

        private void buttonSaveImage_Click(object sender, EventArgs e)
        {
            try
            {
                // create new save dialog
                SaveFileDialog dlg = new SaveFileDialog();
                // set initial directory
                dlg.InitialDirectory = dirLoadSave;
                // show dialog
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    // save image
                    int width = (int)numericRibbonWidth.Value * DEFAULT_RIBBON_WIDTH;
                    int height = (int)numericRibbonHeight.Value * DEFAULT_RIBBON_HEIGHT;

                    Bitmap bm = new Bitmap(width, height);
                    panelRibbonsImage.DrawToBitmap(bm, new Rectangle(0, 0, width, height));
                    bm.Save(dlg.FileName, System.Drawing.Imaging.ImageFormat.Png);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error saving config file");
            }
        }
    }
}
