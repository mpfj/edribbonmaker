namespace ED_Ribbon_Maker
{
    partial class FormRibbonMaker
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.ListViewItem listViewItem13 = new System.Windows.Forms.ListViewItem(new string[] {
            "65K",
            "0"}, -1);
            System.Windows.Forms.ListViewItem listViewItem14 = new System.Windows.Forms.ListViewItem(new string[] {
            "alliance",
            "1"}, -1);
            System.Windows.Forms.ListViewItem listViewItem15 = new System.Windows.Forms.ListViewItem(new string[] {
            "jonk",
            "1"}, -1);
            System.Windows.Forms.ListViewItem listViewItem16 = new System.Windows.Forms.ListViewItem(new string[] {
            "snap",
            "65"}, -1);
            this.textBoxRibbonSource = new System.Windows.Forms.TextBox();
            this.buttonRibbonSource = new System.Windows.Forms.Button();
            this.labelRibbonSource = new System.Windows.Forms.Label();
            this.buttonRibbonRescan = new System.Windows.Forms.Button();
            this.panelRibbonsImage = new System.Windows.Forms.Panel();
            this.numericRibbonWidth = new System.Windows.Forms.NumericUpDown();
            this.numericRibbonHeight = new System.Windows.Forms.NumericUpDown();
            this.labelImageWidth = new System.Windows.Forms.Label();
            this.labelImageHeight = new System.Windows.Forms.Label();
            this.listViewSelected = new System.Windows.Forms.ListView();
            this.columnHeaderName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderNumber = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.listViewFull = new System.Windows.Forms.ListView();
            this.columnHeaderRibbon = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.labelPreview = new System.Windows.Forms.Label();
            this.numericUpDownCount = new System.Windows.Forms.NumericUpDown();
            this.labelRibbonCount = new System.Windows.Forms.Label();
            this.buttonMoveUp = new System.Windows.Forms.Button();
            this.buttonMoveDown = new System.Windows.Forms.Button();
            this.buttonColour = new System.Windows.Forms.Button();
            this.buttonFont = new System.Windows.Forms.Button();
            this.labelPreviewImage = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numericRibbonWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericRibbonHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCount)).BeginInit();
            this.SuspendLayout();
            // 
            // textBoxRibbonSource
            // 
            this.textBoxRibbonSource.Location = new System.Drawing.Point(513, 12);
            this.textBoxRibbonSource.Name = "textBoxRibbonSource";
            this.textBoxRibbonSource.Size = new System.Drawing.Size(182, 20);
            this.textBoxRibbonSource.TabIndex = 2;
            this.textBoxRibbonSource.Text = "C:\\Users\\micro\\Downloads\\DW Ribbons\\260px";
            // 
            // buttonRibbonSource
            // 
            this.buttonRibbonSource.AutoSize = true;
            this.buttonRibbonSource.BackColor = System.Drawing.SystemColors.Control;
            this.buttonRibbonSource.Location = new System.Drawing.Point(700, 10);
            this.buttonRibbonSource.Name = "buttonRibbonSource";
            this.buttonRibbonSource.Size = new System.Drawing.Size(26, 23);
            this.buttonRibbonSource.TabIndex = 3;
            this.buttonRibbonSource.Text = "...";
            this.buttonRibbonSource.UseVisualStyleBackColor = true;
            this.buttonRibbonSource.Click += new System.EventHandler(this.buttonSourceDirectory_Click);
            // 
            // labelRibbonSource
            // 
            this.labelRibbonSource.AutoSize = true;
            this.labelRibbonSource.Location = new System.Drawing.Point(466, 15);
            this.labelRibbonSource.Name = "labelRibbonSource";
            this.labelRibbonSource.Size = new System.Drawing.Size(41, 13);
            this.labelRibbonSource.TabIndex = 4;
            this.labelRibbonSource.Text = "Source";
            // 
            // buttonRibbonRescan
            // 
            this.buttonRibbonRescan.Location = new System.Drawing.Point(732, 10);
            this.buttonRibbonRescan.Name = "buttonRibbonRescan";
            this.buttonRibbonRescan.Size = new System.Drawing.Size(75, 23);
            this.buttonRibbonRescan.TabIndex = 5;
            this.buttonRibbonRescan.Text = "Rescan";
            this.buttonRibbonRescan.UseVisualStyleBackColor = true;
            this.buttonRibbonRescan.Click += new System.EventHandler(this.buttonRibbonRescan_Click);
            // 
            // panelRibbonsImage
            // 
            this.panelRibbonsImage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelRibbonsImage.AutoScroll = true;
            this.panelRibbonsImage.Location = new System.Drawing.Point(12, 256);
            this.panelRibbonsImage.Name = "panelRibbonsImage";
            this.panelRibbonsImage.Size = new System.Drawing.Size(839, 256);
            this.panelRibbonsImage.TabIndex = 4;
            // 
            // numericRibbonWidth
            // 
            this.numericRibbonWidth.Location = new System.Drawing.Point(640, 72);
            this.numericRibbonWidth.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericRibbonWidth.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericRibbonWidth.Name = "numericRibbonWidth";
            this.numericRibbonWidth.Size = new System.Drawing.Size(56, 20);
            this.numericRibbonWidth.TabIndex = 6;
            this.numericRibbonWidth.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numericRibbonWidth.ValueChanged += new System.EventHandler(this.RefreshVisibleRibbons);
            // 
            // numericRibbonHeight
            // 
            this.numericRibbonHeight.Location = new System.Drawing.Point(640, 98);
            this.numericRibbonHeight.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericRibbonHeight.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericRibbonHeight.Name = "numericRibbonHeight";
            this.numericRibbonHeight.Size = new System.Drawing.Size(56, 20);
            this.numericRibbonHeight.TabIndex = 7;
            this.numericRibbonHeight.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numericRibbonHeight.ValueChanged += new System.EventHandler(this.RefreshVisibleRibbons);
            // 
            // labelImageWidth
            // 
            this.labelImageWidth.AutoSize = true;
            this.labelImageWidth.Location = new System.Drawing.Point(546, 74);
            this.labelImageWidth.Name = "labelImageWidth";
            this.labelImageWidth.Size = new System.Drawing.Size(84, 13);
            this.labelImageWidth.TabIndex = 8;
            this.labelImageWidth.Text = "# Ribbons Wide";
            // 
            // labelImageHeight
            // 
            this.labelImageHeight.AutoSize = true;
            this.labelImageHeight.Location = new System.Drawing.Point(546, 100);
            this.labelImageHeight.Name = "labelImageHeight";
            this.labelImageHeight.Size = new System.Drawing.Size(81, 13);
            this.labelImageHeight.TabIndex = 9;
            this.labelImageHeight.Text = "# Ribbons High";
            // 
            // listViewSelected
            // 
            this.listViewSelected.AllowDrop = true;
            this.listViewSelected.BackColor = System.Drawing.SystemColors.Window;
            this.listViewSelected.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderName,
            this.columnHeaderNumber});
            this.listViewSelected.FullRowSelect = true;
            this.listViewSelected.GridLines = true;
            this.listViewSelected.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listViewSelected.HideSelection = false;
            this.listViewSelected.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem13,
            listViewItem14,
            listViewItem15,
            listViewItem16});
            this.listViewSelected.LabelWrap = false;
            this.listViewSelected.Location = new System.Drawing.Point(246, 12);
            this.listViewSelected.Name = "listViewSelected";
            this.listViewSelected.Size = new System.Drawing.Size(214, 208);
            this.listViewSelected.TabIndex = 10;
            this.listViewSelected.UseCompatibleStateImageBehavior = false;
            this.listViewSelected.View = System.Windows.Forms.View.Details;
            this.listViewSelected.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.listViewSelected_ItemDrag);
            this.listViewSelected.SelectedIndexChanged += new System.EventHandler(this.listViewSelected_SelectedIndexChanged);
            this.listViewSelected.DragDrop += new System.Windows.Forms.DragEventHandler(this.listViewSelected_DragDrop);
            this.listViewSelected.DragOver += new System.Windows.Forms.DragEventHandler(this.listViewSelected_DragOver);
            // 
            // columnHeaderName
            // 
            this.columnHeaderName.Text = "Ribbon";
            this.columnHeaderName.Width = 150;
            // 
            // columnHeaderNumber
            // 
            this.columnHeaderNumber.Text = "Number";
            this.columnHeaderNumber.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // listViewFull
            // 
            this.listViewFull.AllowDrop = true;
            this.listViewFull.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderRibbon});
            this.listViewFull.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.listViewFull.Location = new System.Drawing.Point(12, 12);
            this.listViewFull.Name = "listViewFull";
            this.listViewFull.Size = new System.Drawing.Size(228, 237);
            this.listViewFull.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.listViewFull.TabIndex = 11;
            this.listViewFull.UseCompatibleStateImageBehavior = false;
            this.listViewFull.View = System.Windows.Forms.View.Details;
            this.listViewFull.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.listViewFull_ItemDrag);
            this.listViewFull.SelectedIndexChanged += new System.EventHandler(this.listViewFull_SelectedIndexChanged);
            this.listViewFull.DragDrop += new System.Windows.Forms.DragEventHandler(this.listViewFull_DragDrop);
            this.listViewFull.DragOver += new System.Windows.Forms.DragEventHandler(this.listViewFull_DragOver);
            // 
            // columnHeaderRibbon
            // 
            this.columnHeaderRibbon.Text = "Ribbon";
            // 
            // labelPreview
            // 
            this.labelPreview.AutoSize = true;
            this.labelPreview.Location = new System.Drawing.Point(466, 159);
            this.labelPreview.Name = "labelPreview";
            this.labelPreview.Size = new System.Drawing.Size(45, 13);
            this.labelPreview.TabIndex = 13;
            this.labelPreview.Text = "Preview";
            // 
            // numericUpDownCount
            // 
            this.numericUpDownCount.Location = new System.Drawing.Point(640, 124);
            this.numericUpDownCount.Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            this.numericUpDownCount.Minimum = new decimal(new int[] {
            99,
            0,
            0,
            -2147483648});
            this.numericUpDownCount.Name = "numericUpDownCount";
            this.numericUpDownCount.Size = new System.Drawing.Size(56, 20);
            this.numericUpDownCount.TabIndex = 14;
            this.numericUpDownCount.ValueChanged += new System.EventHandler(this.numericUpDownCount_ValueChanged);
            // 
            // labelRibbonCount
            // 
            this.labelRibbonCount.AutoSize = true;
            this.labelRibbonCount.Location = new System.Drawing.Point(546, 126);
            this.labelRibbonCount.Name = "labelRibbonCount";
            this.labelRibbonCount.Size = new System.Drawing.Size(72, 13);
            this.labelRibbonCount.TabIndex = 15;
            this.labelRibbonCount.Text = "Ribbon Count";
            // 
            // buttonMoveUp
            // 
            this.buttonMoveUp.Location = new System.Drawing.Point(246, 226);
            this.buttonMoveUp.Name = "buttonMoveUp";
            this.buttonMoveUp.Size = new System.Drawing.Size(75, 23);
            this.buttonMoveUp.TabIndex = 16;
            this.buttonMoveUp.Text = "Move Up";
            this.buttonMoveUp.UseVisualStyleBackColor = true;
            this.buttonMoveUp.Click += new System.EventHandler(this.buttonMoveUp_Click);
            // 
            // buttonMoveDown
            // 
            this.buttonMoveDown.Location = new System.Drawing.Point(385, 227);
            this.buttonMoveDown.Name = "buttonMoveDown";
            this.buttonMoveDown.Size = new System.Drawing.Size(75, 23);
            this.buttonMoveDown.TabIndex = 17;
            this.buttonMoveDown.Text = "Move Down";
            this.buttonMoveDown.UseVisualStyleBackColor = true;
            this.buttonMoveDown.Click += new System.EventHandler(this.buttonDown_Click);
            // 
            // buttonColour
            // 
            this.buttonColour.Location = new System.Drawing.Point(756, 175);
            this.buttonColour.Name = "buttonColour";
            this.buttonColour.Size = new System.Drawing.Size(75, 23);
            this.buttonColour.TabIndex = 18;
            this.buttonColour.Text = "Colour";
            this.buttonColour.UseVisualStyleBackColor = true;
            this.buttonColour.Click += new System.EventHandler(this.buttonColour_Click);
            // 
            // buttonFont
            // 
            this.buttonFont.Location = new System.Drawing.Point(756, 204);
            this.buttonFont.Name = "buttonFont";
            this.buttonFont.Size = new System.Drawing.Size(75, 23);
            this.buttonFont.TabIndex = 19;
            this.buttonFont.Text = "Font";
            this.buttonFont.UseVisualStyleBackColor = true;
            this.buttonFont.Click += new System.EventHandler(this.buttonFont_Click);
            // 
            // labelPreviewImage
            // 
            this.labelPreviewImage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelPreviewImage.Location = new System.Drawing.Point(466, 175);
            this.labelPreviewImage.Name = "labelPreviewImage";
            this.labelPreviewImage.Size = new System.Drawing.Size(260, 74);
            this.labelPreviewImage.TabIndex = 20;
            this.labelPreviewImage.Text = "123";
            this.labelPreviewImage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FormRibbonMaker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(863, 524);
            this.Controls.Add(this.buttonFont);
            this.Controls.Add(this.buttonColour);
            this.Controls.Add(this.buttonMoveDown);
            this.Controls.Add(this.buttonMoveUp);
            this.Controls.Add(this.labelRibbonCount);
            this.Controls.Add(this.numericUpDownCount);
            this.Controls.Add(this.labelPreview);
            this.Controls.Add(this.listViewFull);
            this.Controls.Add(this.listViewSelected);
            this.Controls.Add(this.labelImageHeight);
            this.Controls.Add(this.labelImageWidth);
            this.Controls.Add(this.numericRibbonHeight);
            this.Controls.Add(this.numericRibbonWidth);
            this.Controls.Add(this.buttonRibbonRescan);
            this.Controls.Add(this.labelRibbonSource);
            this.Controls.Add(this.buttonRibbonSource);
            this.Controls.Add(this.panelRibbonsImage);
            this.Controls.Add(this.textBoxRibbonSource);
            this.Controls.Add(this.labelPreviewImage);
            this.Name = "FormRibbonMaker";
            this.Text = "ED Ribbon Maker";
            this.Load += new System.EventHandler(this.FormRibbonMaker_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numericRibbonWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericRibbonHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCount)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox textBoxRibbonSource;
        private System.Windows.Forms.Button buttonRibbonSource;
        private System.Windows.Forms.Label labelRibbonSource;
        private System.Windows.Forms.Button buttonRibbonRescan;
        private System.Windows.Forms.Panel panelRibbonsImage;
        private System.Windows.Forms.NumericUpDown numericRibbonWidth;
        private System.Windows.Forms.NumericUpDown numericRibbonHeight;
        private System.Windows.Forms.Label labelImageWidth;
        private System.Windows.Forms.Label labelImageHeight;
        private System.Windows.Forms.ListView listViewSelected;
        private System.Windows.Forms.ColumnHeader columnHeaderName;
        private System.Windows.Forms.ColumnHeader columnHeaderNumber;
        private System.Windows.Forms.ListView listViewFull;
        private System.Windows.Forms.ColumnHeader columnHeaderRibbon;
        private System.Windows.Forms.Label labelPreview;
        private System.Windows.Forms.NumericUpDown numericUpDownCount;
        private System.Windows.Forms.Label labelRibbonCount;
        private System.Windows.Forms.Button buttonMoveUp;
        private System.Windows.Forms.Button buttonMoveDown;
        private System.Windows.Forms.Button buttonColour;
        private System.Windows.Forms.Button buttonFont;
        private System.Windows.Forms.Label labelPreviewImage;
    }
}

