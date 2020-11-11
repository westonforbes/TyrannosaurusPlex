namespace TyrannosaurusPlex
{
    partial class FORM_ADD_RECIPE
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FORM_ADD_RECIPE));
            this.TXT_BOX_PART_NUM = new System.Windows.Forms.TextBox();
            this.LBL_PART_NUM = new System.Windows.Forms.Label();
            this.BTN_SAVE = new System.Windows.Forms.Button();
            this.BTN_CANCEL = new System.Windows.Forms.Button();
            this.LISTBOX_CHECKSHEET_TYPE = new System.Windows.Forms.ListBox();
            this.LBL_CHECKSHEET_TYPE = new System.Windows.Forms.Label();
            this.LBL_CSV_FILE = new System.Windows.Forms.Label();
            this.TXT_BOX_KEYENCE = new System.Windows.Forms.TextBox();
            this.BTN_BROWSE_KEYENCE = new System.Windows.Forms.Button();
            this.GRP_BOX_DATA = new System.Windows.Forms.GroupBox();
            this.DGV1 = new System.Windows.Forms.DataGridView();
            this.BTN_PREVIEW = new System.Windows.Forms.Button();
            this.GRP_BOX_COLUMN_ASSIGNERS = new System.Windows.Forms.GroupBox();
            this.BTN_TIMESTAMP = new System.Windows.Forms.Button();
            this.BTN_M = new System.Windows.Forms.Button();
            this.BTN_L = new System.Windows.Forms.Button();
            this.BTN_K = new System.Windows.Forms.Button();
            this.BTN_J = new System.Windows.Forms.Button();
            this.BTN_I = new System.Windows.Forms.Button();
            this.BTN_H = new System.Windows.Forms.Button();
            this.BTN_G = new System.Windows.Forms.Button();
            this.BTN_F = new System.Windows.Forms.Button();
            this.BTN_E = new System.Windows.Forms.Button();
            this.BTN_D = new System.Windows.Forms.Button();
            this.BTN_C = new System.Windows.Forms.Button();
            this.BTN_B = new System.Windows.Forms.Button();
            this.BTN_A = new System.Windows.Forms.Button();
            this.BTN_RECORD_START = new System.Windows.Forms.Button();
            this.BTN_RECORD_STOP = new System.Windows.Forms.Button();
            this.BTN_REPLAY = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.GRP_BOX_DATA.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGV1)).BeginInit();
            this.GRP_BOX_COLUMN_ASSIGNERS.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.SuspendLayout();
            // 
            // TXT_BOX_PART_NUM
            // 
            this.TXT_BOX_PART_NUM.Location = new System.Drawing.Point(15, 30);
            this.TXT_BOX_PART_NUM.Name = "TXT_BOX_PART_NUM";
            this.TXT_BOX_PART_NUM.Size = new System.Drawing.Size(100, 20);
            this.TXT_BOX_PART_NUM.TabIndex = 0;
            // 
            // LBL_PART_NUM
            // 
            this.LBL_PART_NUM.Location = new System.Drawing.Point(15, 15);
            this.LBL_PART_NUM.Name = "LBL_PART_NUM";
            this.LBL_PART_NUM.Size = new System.Drawing.Size(100, 15);
            this.LBL_PART_NUM.TabIndex = 1;
            this.LBL_PART_NUM.Text = "Internal Part Number";
            // 
            // BTN_SAVE
            // 
            this.BTN_SAVE.Location = new System.Drawing.Point(15, 175);
            this.BTN_SAVE.Name = "BTN_SAVE";
            this.BTN_SAVE.Size = new System.Drawing.Size(100, 25);
            this.BTN_SAVE.TabIndex = 2;
            this.BTN_SAVE.Text = "Save";
            this.BTN_SAVE.UseVisualStyleBackColor = true;
            this.BTN_SAVE.Click += new System.EventHandler(this.BTN_SAVE_CLICK);
            // 
            // BTN_CANCEL
            // 
            this.BTN_CANCEL.Location = new System.Drawing.Point(15, 205);
            this.BTN_CANCEL.Name = "BTN_CANCEL";
            this.BTN_CANCEL.Size = new System.Drawing.Size(100, 25);
            this.BTN_CANCEL.TabIndex = 3;
            this.BTN_CANCEL.Text = "Cancel";
            this.BTN_CANCEL.UseVisualStyleBackColor = true;
            this.BTN_CANCEL.Click += new System.EventHandler(this.CLOSE);
            // 
            // LISTBOX_CHECKSHEET_TYPE
            // 
            this.LISTBOX_CHECKSHEET_TYPE.FormattingEnabled = true;
            this.LISTBOX_CHECKSHEET_TYPE.Location = new System.Drawing.Point(15, 75);
            this.LISTBOX_CHECKSHEET_TYPE.Name = "LISTBOX_CHECKSHEET_TYPE";
            this.LISTBOX_CHECKSHEET_TYPE.Size = new System.Drawing.Size(100, 95);
            this.LISTBOX_CHECKSHEET_TYPE.TabIndex = 4;
            // 
            // LBL_CHECKSHEET_TYPE
            // 
            this.LBL_CHECKSHEET_TYPE.Location = new System.Drawing.Point(15, 60);
            this.LBL_CHECKSHEET_TYPE.Name = "LBL_CHECKSHEET_TYPE";
            this.LBL_CHECKSHEET_TYPE.Size = new System.Drawing.Size(100, 15);
            this.LBL_CHECKSHEET_TYPE.TabIndex = 5;
            this.LBL_CHECKSHEET_TYPE.Text = "Checksheet Type";
            // 
            // LBL_CSV_FILE
            // 
            this.LBL_CSV_FILE.Location = new System.Drawing.Point(125, 15);
            this.LBL_CSV_FILE.Name = "LBL_CSV_FILE";
            this.LBL_CSV_FILE.Size = new System.Drawing.Size(100, 15);
            this.LBL_CSV_FILE.TabIndex = 7;
            this.LBL_CSV_FILE.Text = "Keyence CSV File";
            // 
            // TXT_BOX_KEYENCE
            // 
            this.TXT_BOX_KEYENCE.Location = new System.Drawing.Point(125, 30);
            this.TXT_BOX_KEYENCE.Name = "TXT_BOX_KEYENCE";
            this.TXT_BOX_KEYENCE.Size = new System.Drawing.Size(285, 20);
            this.TXT_BOX_KEYENCE.TabIndex = 6;
            // 
            // BTN_BROWSE_KEYENCE
            // 
            this.BTN_BROWSE_KEYENCE.Location = new System.Drawing.Point(420, 27);
            this.BTN_BROWSE_KEYENCE.Name = "BTN_BROWSE_KEYENCE";
            this.BTN_BROWSE_KEYENCE.Size = new System.Drawing.Size(85, 25);
            this.BTN_BROWSE_KEYENCE.TabIndex = 8;
            this.BTN_BROWSE_KEYENCE.Text = "Browse";
            this.BTN_BROWSE_KEYENCE.UseVisualStyleBackColor = true;
            this.BTN_BROWSE_KEYENCE.Click += new System.EventHandler(this.BTN_BROWSE_KEYENCE_CLICK);
            // 
            // GRP_BOX_DATA
            // 
            this.GRP_BOX_DATA.Controls.Add(this.DGV1);
            this.GRP_BOX_DATA.Location = new System.Drawing.Point(125, 60);
            this.GRP_BOX_DATA.Name = "GRP_BOX_DATA";
            this.GRP_BOX_DATA.Size = new System.Drawing.Size(470, 265);
            this.GRP_BOX_DATA.TabIndex = 106;
            this.GRP_BOX_DATA.TabStop = false;
            this.GRP_BOX_DATA.Text = "Keyence CSV Preview";
            // 
            // DGV1
            // 
            this.DGV1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGV1.Location = new System.Drawing.Point(10, 20);
            this.DGV1.Name = "DGV1";
            this.DGV1.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Sunken;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DGV1.RowHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.DGV1.Size = new System.Drawing.Size(450, 235);
            this.DGV1.TabIndex = 99;
            // 
            // BTN_PREVIEW
            // 
            this.BTN_PREVIEW.Location = new System.Drawing.Point(510, 27);
            this.BTN_PREVIEW.Name = "BTN_PREVIEW";
            this.BTN_PREVIEW.Size = new System.Drawing.Size(85, 25);
            this.BTN_PREVIEW.TabIndex = 107;
            this.BTN_PREVIEW.Text = "Preview Data";
            this.BTN_PREVIEW.UseVisualStyleBackColor = true;
            this.BTN_PREVIEW.Click += new System.EventHandler(this.LOAD_CSV_DATA);
            // 
            // GRP_BOX_COLUMN_ASSIGNERS
            // 
            this.GRP_BOX_COLUMN_ASSIGNERS.Controls.Add(this.BTN_TIMESTAMP);
            this.GRP_BOX_COLUMN_ASSIGNERS.Controls.Add(this.BTN_M);
            this.GRP_BOX_COLUMN_ASSIGNERS.Controls.Add(this.BTN_L);
            this.GRP_BOX_COLUMN_ASSIGNERS.Controls.Add(this.BTN_K);
            this.GRP_BOX_COLUMN_ASSIGNERS.Controls.Add(this.BTN_J);
            this.GRP_BOX_COLUMN_ASSIGNERS.Controls.Add(this.BTN_I);
            this.GRP_BOX_COLUMN_ASSIGNERS.Controls.Add(this.BTN_H);
            this.GRP_BOX_COLUMN_ASSIGNERS.Controls.Add(this.BTN_G);
            this.GRP_BOX_COLUMN_ASSIGNERS.Controls.Add(this.BTN_F);
            this.GRP_BOX_COLUMN_ASSIGNERS.Controls.Add(this.BTN_E);
            this.GRP_BOX_COLUMN_ASSIGNERS.Controls.Add(this.BTN_D);
            this.GRP_BOX_COLUMN_ASSIGNERS.Controls.Add(this.BTN_C);
            this.GRP_BOX_COLUMN_ASSIGNERS.Controls.Add(this.BTN_B);
            this.GRP_BOX_COLUMN_ASSIGNERS.Controls.Add(this.BTN_A);
            this.GRP_BOX_COLUMN_ASSIGNERS.Location = new System.Drawing.Point(125, 335);
            this.GRP_BOX_COLUMN_ASSIGNERS.Name = "GRP_BOX_COLUMN_ASSIGNERS";
            this.GRP_BOX_COLUMN_ASSIGNERS.Size = new System.Drawing.Size(470, 95);
            this.GRP_BOX_COLUMN_ASSIGNERS.TabIndex = 108;
            this.GRP_BOX_COLUMN_ASSIGNERS.TabStop = false;
            this.GRP_BOX_COLUMN_ASSIGNERS.Text = "Column Assigners";
            // 
            // BTN_TIMESTAMP
            // 
            this.BTN_TIMESTAMP.Location = new System.Drawing.Point(10, 55);
            this.BTN_TIMESTAMP.Name = "BTN_TIMESTAMP";
            this.BTN_TIMESTAMP.Size = new System.Drawing.Size(100, 30);
            this.BTN_TIMESTAMP.TabIndex = 13;
            this.BTN_TIMESTAMP.Text = "TimeStamp";
            this.BTN_TIMESTAMP.UseVisualStyleBackColor = true;
            this.BTN_TIMESTAMP.Click += new System.EventHandler(this.BTN_CLICK);
            // 
            // BTN_M
            // 
            this.BTN_M.Location = new System.Drawing.Point(430, 20);
            this.BTN_M.Name = "BTN_M";
            this.BTN_M.Size = new System.Drawing.Size(30, 30);
            this.BTN_M.TabIndex = 12;
            this.BTN_M.Text = "M";
            this.BTN_M.UseVisualStyleBackColor = true;
            this.BTN_M.Click += new System.EventHandler(this.BTN_CLICK);
            // 
            // BTN_L
            // 
            this.BTN_L.Location = new System.Drawing.Point(395, 20);
            this.BTN_L.Name = "BTN_L";
            this.BTN_L.Size = new System.Drawing.Size(30, 30);
            this.BTN_L.TabIndex = 11;
            this.BTN_L.Text = "L";
            this.BTN_L.UseVisualStyleBackColor = true;
            this.BTN_L.Click += new System.EventHandler(this.BTN_CLICK);
            // 
            // BTN_K
            // 
            this.BTN_K.Location = new System.Drawing.Point(360, 20);
            this.BTN_K.Name = "BTN_K";
            this.BTN_K.Size = new System.Drawing.Size(30, 30);
            this.BTN_K.TabIndex = 10;
            this.BTN_K.Text = "K";
            this.BTN_K.UseVisualStyleBackColor = true;
            this.BTN_K.Click += new System.EventHandler(this.BTN_CLICK);
            // 
            // BTN_J
            // 
            this.BTN_J.Location = new System.Drawing.Point(325, 20);
            this.BTN_J.Name = "BTN_J";
            this.BTN_J.Size = new System.Drawing.Size(30, 30);
            this.BTN_J.TabIndex = 9;
            this.BTN_J.Text = "J";
            this.BTN_J.UseVisualStyleBackColor = true;
            this.BTN_J.Click += new System.EventHandler(this.BTN_CLICK);
            // 
            // BTN_I
            // 
            this.BTN_I.Location = new System.Drawing.Point(290, 20);
            this.BTN_I.Name = "BTN_I";
            this.BTN_I.Size = new System.Drawing.Size(30, 30);
            this.BTN_I.TabIndex = 8;
            this.BTN_I.Text = "I";
            this.BTN_I.UseVisualStyleBackColor = true;
            this.BTN_I.Click += new System.EventHandler(this.BTN_CLICK);
            // 
            // BTN_H
            // 
            this.BTN_H.Location = new System.Drawing.Point(255, 20);
            this.BTN_H.Name = "BTN_H";
            this.BTN_H.Size = new System.Drawing.Size(30, 30);
            this.BTN_H.TabIndex = 7;
            this.BTN_H.Text = "H";
            this.BTN_H.UseVisualStyleBackColor = true;
            this.BTN_H.Click += new System.EventHandler(this.BTN_CLICK);
            // 
            // BTN_G
            // 
            this.BTN_G.Location = new System.Drawing.Point(220, 20);
            this.BTN_G.Name = "BTN_G";
            this.BTN_G.Size = new System.Drawing.Size(30, 30);
            this.BTN_G.TabIndex = 6;
            this.BTN_G.Text = "G";
            this.BTN_G.UseVisualStyleBackColor = true;
            this.BTN_G.Click += new System.EventHandler(this.BTN_CLICK);
            // 
            // BTN_F
            // 
            this.BTN_F.Location = new System.Drawing.Point(185, 20);
            this.BTN_F.Name = "BTN_F";
            this.BTN_F.Size = new System.Drawing.Size(30, 30);
            this.BTN_F.TabIndex = 5;
            this.BTN_F.Text = "F";
            this.BTN_F.UseVisualStyleBackColor = true;
            this.BTN_F.Click += new System.EventHandler(this.BTN_CLICK);
            // 
            // BTN_E
            // 
            this.BTN_E.Location = new System.Drawing.Point(150, 20);
            this.BTN_E.Name = "BTN_E";
            this.BTN_E.Size = new System.Drawing.Size(30, 30);
            this.BTN_E.TabIndex = 4;
            this.BTN_E.Text = "E";
            this.BTN_E.UseVisualStyleBackColor = true;
            this.BTN_E.Click += new System.EventHandler(this.BTN_CLICK);
            // 
            // BTN_D
            // 
            this.BTN_D.Location = new System.Drawing.Point(115, 20);
            this.BTN_D.Name = "BTN_D";
            this.BTN_D.Size = new System.Drawing.Size(30, 30);
            this.BTN_D.TabIndex = 3;
            this.BTN_D.Text = "D";
            this.BTN_D.UseVisualStyleBackColor = true;
            this.BTN_D.Click += new System.EventHandler(this.BTN_CLICK);
            // 
            // BTN_C
            // 
            this.BTN_C.Location = new System.Drawing.Point(80, 20);
            this.BTN_C.Name = "BTN_C";
            this.BTN_C.Size = new System.Drawing.Size(30, 30);
            this.BTN_C.TabIndex = 2;
            this.BTN_C.Text = "C";
            this.BTN_C.UseVisualStyleBackColor = true;
            this.BTN_C.Click += new System.EventHandler(this.BTN_CLICK);
            // 
            // BTN_B
            // 
            this.BTN_B.Location = new System.Drawing.Point(45, 20);
            this.BTN_B.Name = "BTN_B";
            this.BTN_B.Size = new System.Drawing.Size(30, 30);
            this.BTN_B.TabIndex = 1;
            this.BTN_B.Text = "B";
            this.BTN_B.UseVisualStyleBackColor = true;
            this.BTN_B.Click += new System.EventHandler(this.BTN_CLICK);
            // 
            // BTN_A
            // 
            this.BTN_A.Location = new System.Drawing.Point(10, 20);
            this.BTN_A.Name = "BTN_A";
            this.BTN_A.Size = new System.Drawing.Size(30, 30);
            this.BTN_A.TabIndex = 0;
            this.BTN_A.Text = "A";
            this.BTN_A.UseVisualStyleBackColor = true;
            this.BTN_A.Click += new System.EventHandler(this.BTN_CLICK);
            // 
            // BTN_RECORD_START
            // 
            this.BTN_RECORD_START.Location = new System.Drawing.Point(15, 355);
            this.BTN_RECORD_START.Name = "BTN_RECORD_START";
            this.BTN_RECORD_START.Size = new System.Drawing.Size(100, 30);
            this.BTN_RECORD_START.TabIndex = 109;
            this.BTN_RECORD_START.Text = "Train Fields";
            this.BTN_RECORD_START.UseVisualStyleBackColor = true;
            this.BTN_RECORD_START.Click += new System.EventHandler(this.RECORD_SEQUENCE_START);
            // 
            // BTN_RECORD_STOP
            // 
            this.BTN_RECORD_STOP.Location = new System.Drawing.Point(15, 390);
            this.BTN_RECORD_STOP.Name = "BTN_RECORD_STOP";
            this.BTN_RECORD_STOP.Size = new System.Drawing.Size(100, 30);
            this.BTN_RECORD_STOP.TabIndex = 111;
            this.BTN_RECORD_STOP.Text = "Stop Training";
            this.BTN_RECORD_STOP.UseVisualStyleBackColor = true;
            this.BTN_RECORD_STOP.Click += new System.EventHandler(this.RECORD_SEQUENCE_STOP);
            // 
            // BTN_REPLAY
            // 
            this.BTN_REPLAY.Location = new System.Drawing.Point(15, 245);
            this.BTN_REPLAY.Name = "BTN_REPLAY";
            this.BTN_REPLAY.Size = new System.Drawing.Size(100, 30);
            this.BTN_REPLAY.TabIndex = 112;
            this.BTN_REPLAY.Text = "Replay";
            this.BTN_REPLAY.UseVisualStyleBackColor = true;
            this.BTN_REPLAY.Click += new System.EventHandler(this.REPLAY_SEQUENCE_START);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(623, 130);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Sunken;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView1.Size = new System.Drawing.Size(170, 235);
            this.dataGridView1.TabIndex = 100;
            // 
            // dataGridView2
            // 
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Location = new System.Drawing.Point(799, 130);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Sunken;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView2.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridView2.Size = new System.Drawing.Size(170, 235);
            this.dataGridView2.TabIndex = 113;
            // 
            // FORM_ADD_RECIPE
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(901, 454);
            this.Controls.Add(this.dataGridView2);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.BTN_REPLAY);
            this.Controls.Add(this.BTN_RECORD_STOP);
            this.Controls.Add(this.BTN_RECORD_START);
            this.Controls.Add(this.GRP_BOX_COLUMN_ASSIGNERS);
            this.Controls.Add(this.BTN_PREVIEW);
            this.Controls.Add(this.GRP_BOX_DATA);
            this.Controls.Add(this.BTN_BROWSE_KEYENCE);
            this.Controls.Add(this.LBL_CSV_FILE);
            this.Controls.Add(this.TXT_BOX_KEYENCE);
            this.Controls.Add(this.LBL_CHECKSHEET_TYPE);
            this.Controls.Add(this.LISTBOX_CHECKSHEET_TYPE);
            this.Controls.Add(this.BTN_CANCEL);
            this.Controls.Add(this.BTN_SAVE);
            this.Controls.Add(this.LBL_PART_NUM);
            this.Controls.Add(this.TXT_BOX_PART_NUM);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FORM_ADD_RECIPE";
            this.Text = "Add Recipe";
            this.GRP_BOX_DATA.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DGV1)).EndInit();
            this.GRP_BOX_COLUMN_ASSIGNERS.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox TXT_BOX_PART_NUM;
        private System.Windows.Forms.Label LBL_PART_NUM;
        private System.Windows.Forms.Button BTN_SAVE;
        private System.Windows.Forms.Button BTN_CANCEL;
        private System.Windows.Forms.ListBox LISTBOX_CHECKSHEET_TYPE;
        private System.Windows.Forms.Label LBL_CHECKSHEET_TYPE;
        private System.Windows.Forms.Label LBL_CSV_FILE;
        private System.Windows.Forms.TextBox TXT_BOX_KEYENCE;
        private System.Windows.Forms.Button BTN_BROWSE_KEYENCE;
        private System.Windows.Forms.GroupBox GRP_BOX_DATA;
        private System.Windows.Forms.DataGridView DGV1;
        private System.Windows.Forms.Button BTN_PREVIEW;
        private System.Windows.Forms.GroupBox GRP_BOX_COLUMN_ASSIGNERS;
        private System.Windows.Forms.Button BTN_M;
        private System.Windows.Forms.Button BTN_L;
        private System.Windows.Forms.Button BTN_K;
        private System.Windows.Forms.Button BTN_J;
        private System.Windows.Forms.Button BTN_I;
        private System.Windows.Forms.Button BTN_H;
        private System.Windows.Forms.Button BTN_G;
        private System.Windows.Forms.Button BTN_F;
        private System.Windows.Forms.Button BTN_E;
        private System.Windows.Forms.Button BTN_D;
        private System.Windows.Forms.Button BTN_C;
        private System.Windows.Forms.Button BTN_B;
        private System.Windows.Forms.Button BTN_A;
        private System.Windows.Forms.Button BTN_TIMESTAMP;
        private System.Windows.Forms.Button BTN_RECORD_START;
        private System.Windows.Forms.Button BTN_RECORD_STOP;
        private System.Windows.Forms.Button BTN_REPLAY;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridView dataGridView2;
    }
}