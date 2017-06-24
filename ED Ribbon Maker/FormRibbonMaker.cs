using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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

        Color colourCount = Color.White;
        Font fontCount = new Font(FontFamily.GenericSansSerif, 24, FontStyle.Bold);

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
                    listViewFull.Items.Remove(item);

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
                    listViewFull.Items.Add(item.Text);
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
                            LoadPicture(l, listViewSelected.Items[index].Text);
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

        private void LoadPicture(Label l, string name)
        {
            string filename = textBoxRibbonSource.Text + "\\" + name + RIBBON_SUFFIX;
            Image i = Image.FromFile(filename);
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
                LoadPicture(labelPreviewImage, item.Text);

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
                int idxC = panelRibbonsImage.Controls.IndexOfKey("Count" + ((index / MAX_RIBBONS_WIDE) + (index % MAX_RIBBONS_WIDE)).ToString());
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
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
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
    }
}
