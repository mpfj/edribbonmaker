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

            // popoulate ribbon image grid
            for (int row = 0; row < MAX_RIBBONS_HIGH; row++)
            {
                for (int col = 0; col < MAX_RIBBONS_WIDE; col++)
                {
                    PictureBox p = new PictureBox();
                    p.Margin = new Padding(0);
                    p.BorderStyle = BorderStyle.FixedSingle;
                    p.Width = DEFAULT_RIBBON_WIDTH;
                    p.Height = DEFAULT_RIBBON_HEIGHT;
                    p.Left = col * DEFAULT_RIBBON_WIDTH;
                    p.Top = row * DEFAULT_RIBBON_HEIGHT;
                    p.Visible = false;
                    p.Name = "Ribbon" + ((row * MAX_RIBBONS_WIDE) + col).ToString();
                    panelRibbonsImage.Controls.Add(p);
                }
            }

            // show visible ribbons
            RefreshVisibleRibbons(sender, e);

            // init source ribbon list

            // init selected ribbon list

            // update main ribbon panel
            UpdateRibbonPanel();
        }

        private void RefreshVisibleRibbons(object sender, EventArgs e)
        {
            try
            {
                for (decimal row = 0; row < numericRibbonHeight.Value; row++)
                {
                    for (decimal col = 0; col < numericRibbonWidth.Value; col++)
                    {
                        int idx = panelRibbonsImage.Controls.IndexOfKey("Ribbon" + ((row * MAX_RIBBONS_WIDE) + col).ToString());
                        PictureBox p = (PictureBox)panelRibbonsImage.Controls[idx];
                        p.Visible = true;
                    }

                    for (decimal col = numericRibbonWidth.Value; col < MAX_RIBBONS_WIDE; col++)
                    {
                        int idx = panelRibbonsImage.Controls.IndexOfKey("Ribbon" + ((row * MAX_RIBBONS_WIDE) + col).ToString());
                        PictureBox p = (PictureBox)panelRibbonsImage.Controls[idx];
                        p.Visible = false;
                    }
                }
                for (decimal row = numericRibbonHeight.Value; row < MAX_RIBBONS_HIGH; row++)
                {
                    for (decimal col = 0; col < MAX_RIBBONS_WIDE; col++)
                    {
                        int idx = panelRibbonsImage.Controls.IndexOfKey("Ribbon" + ((row * MAX_RIBBONS_WIDE) + col).ToString());
                        PictureBox p = (PictureBox)panelRibbonsImage.Controls[idx];
                        p.Visible = false;
                    }
                }

                // update main ribbon image
                UpdateRibbonPanel();
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
                    UpdateRibbonPanel();
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
                    UpdateRibbonPanel();
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
                LoadPicture(pictureBoxPreview, listViewFull.SelectedItems[0].Text);
            }
            catch (Exception)
            {
            }
        }


        private void UpdateRibbonPanel()
        {
            int index = 0;

            for (decimal row = 0; row < numericRibbonHeight.Value; row++)
            {
                for (decimal col = 0; col < numericRibbonWidth.Value; col++, index++)
                {
                    // get picture grid control
                    int idx = panelRibbonsImage.Controls.IndexOfKey("Ribbon" + ((row * MAX_RIBBONS_WIDE) + col).ToString());
                    PictureBox p = (PictureBox)panelRibbonsImage.Controls[idx];

                    // get next selected ribbon
                    if (index >= listViewSelected.Items.Count)
                    {
                        // remove any existing picture
                        p.Image = null;
                    }
                    else
                    {
                        // update ribbon image
                        LoadPicture(p, listViewSelected.Items[index].Text);
                    }
                }
            }
        }

        private void LoadPicture(PictureBox p, string name)
        {
            string filename = textBoxRibbonSource.Text + "\\" + name + RIBBON_SUFFIX;
            p.LoadAsync(filename);
        }

        private void pictureBoxPreview_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < listViewSelected.Items.Count ; i++)
                Console.WriteLine("item check" + i.ToString() + " = " + listViewSelected.Items[i].Selected.ToString());
        }

        private void listViewSelected_KeyUp(object sender, KeyEventArgs e)
        {
            for (int i = 0; i < 4; i++)
                Console.WriteLine("item" + i.ToString() + " = " + listViewSelected.Items[i].Selected.ToString());

            // if nothing selected, quit
            if (listViewSelected.SelectedItems.Count == 0)
                return;

            ListViewItem item = listViewSelected.SelectedItems[0];
            int index = item.Index;

            // UP key pressed ?
            if ((e.KeyCode == Keys.Up) && (index != 0))
            {
                // move item up one
                //listViewSelected.BeginUpdate();
                listViewSelected.Items.Remove(item);
                Console.WriteLine("item count A = " + listViewSelected.Items.Count.ToString());
                //listViewSelected.Items.
                listViewSelected.Items.Insert(index - 1, item);
                Console.WriteLine("item count B = " + listViewSelected.Items.Count.ToString());
                for (int i = 0; i < listViewSelected.Items.Count; i++)
                    listViewSelected.Items[i].Selected = false;
                //listViewSelected.EndUpdate();
                //listViewSelected.Items[index - 1].Selected = true;

                // update main ribbon image
                UpdateRibbonPanel();

                e.Handled = true;
                e.SuppressKeyPress = true;
                return;
            }

            // DOWN key presses ?
            if ((e.KeyCode == Keys.Down) && (index != (listViewSelected.Items.Count - 1)))
            {
                // move item down one
                listViewSelected.Items.RemoveAt(index);
                listViewSelected.Items.Insert(index + 1, item);
                //listViewSelected.Items[index + 1].Selected = true;

                // update main ribbon image
                UpdateRibbonPanel();

                e.Handled = true;
                e.SuppressKeyPress = true;
                return;
            }
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
                UpdateRibbonPanel();
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
                UpdateRibbonPanel();
            }
        }
    }
}
