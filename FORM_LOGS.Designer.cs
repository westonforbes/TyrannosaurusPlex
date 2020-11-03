namespace TyrannosaurusPlex
{
    partial class FORM_LOGS
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FORM_LOGS));
            this.GRP_BOX_LOG = new System.Windows.Forms.GroupBox();
            this.BTN_CLEAR_LOG = new System.Windows.Forms.Button();
            this.LBL_VERBOSE = new System.Windows.Forms.Label();
            this.DGV_LOG = new System.Windows.Forms.DataGridView();
            this.TRCK_VERBOSE = new System.Windows.Forms.TrackBar();
            this.GRP_BOX_LOG.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGV_LOG)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TRCK_VERBOSE)).BeginInit();
            this.SuspendLayout();
            // 
            // GRP_BOX_LOG
            // 
            this.GRP_BOX_LOG.Controls.Add(this.BTN_CLEAR_LOG);
            this.GRP_BOX_LOG.Controls.Add(this.LBL_VERBOSE);
            this.GRP_BOX_LOG.Controls.Add(this.DGV_LOG);
            this.GRP_BOX_LOG.Controls.Add(this.TRCK_VERBOSE);
            this.GRP_BOX_LOG.Location = new System.Drawing.Point(10, 10);
            this.GRP_BOX_LOG.Name = "GRP_BOX_LOG";
            this.GRP_BOX_LOG.Size = new System.Drawing.Size(850, 330);
            this.GRP_BOX_LOG.TabIndex = 114;
            this.GRP_BOX_LOG.TabStop = false;
            this.GRP_BOX_LOG.Text = "Log";
            // 
            // BTN_CLEAR_LOG
            // 
            this.BTN_CLEAR_LOG.Location = new System.Drawing.Point(665, 18);
            this.BTN_CLEAR_LOG.Name = "BTN_CLEAR_LOG";
            this.BTN_CLEAR_LOG.Size = new System.Drawing.Size(175, 25);
            this.BTN_CLEAR_LOG.TabIndex = 7;
            this.BTN_CLEAR_LOG.TabStop = false;
            this.BTN_CLEAR_LOG.Text = "Clear Data Log";
            this.BTN_CLEAR_LOG.UseVisualStyleBackColor = true;
            // 
            // LBL_VERBOSE
            // 
            this.LBL_VERBOSE.AutoSize = true;
            this.LBL_VERBOSE.Location = new System.Drawing.Point(76, 24);
            this.LBL_VERBOSE.Name = "LBL_VERBOSE";
            this.LBL_VERBOSE.Size = new System.Drawing.Size(117, 13);
            this.LBL_VERBOSE.TabIndex = 110;
            this.LBL_VERBOSE.Text = "Verbose Detail Level: 3";
            // 
            // DGV_LOG
            // 
            this.DGV_LOG.AllowUserToAddRows = false;
            this.DGV_LOG.AllowUserToDeleteRows = false;
            this.DGV_LOG.AllowUserToResizeRows = false;
            this.DGV_LOG.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGV_LOG.Location = new System.Drawing.Point(10, 50);
            this.DGV_LOG.Name = "DGV_LOG";
            this.DGV_LOG.ReadOnly = true;
            this.DGV_LOG.RowHeadersVisible = false;
            this.DGV_LOG.Size = new System.Drawing.Size(830, 265);
            this.DGV_LOG.TabIndex = 99;
            // 
            // TRCK_VERBOSE
            // 
            this.TRCK_VERBOSE.LargeChange = 1;
            this.TRCK_VERBOSE.Location = new System.Drawing.Point(10, 20);
            this.TRCK_VERBOSE.Maximum = 3;
            this.TRCK_VERBOSE.Minimum = 1;
            this.TRCK_VERBOSE.Name = "TRCK_VERBOSE";
            this.TRCK_VERBOSE.Size = new System.Drawing.Size(60, 45);
            this.TRCK_VERBOSE.TabIndex = 109;
            this.TRCK_VERBOSE.TickStyle = System.Windows.Forms.TickStyle.None;
            this.TRCK_VERBOSE.Value = 3;
            // 
            // FORM_LOGS
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(869, 351);
            this.Controls.Add(this.GRP_BOX_LOG);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FORM_LOGS";
            this.Text = "Logs";
            this.GRP_BOX_LOG.ResumeLayout(false);
            this.GRP_BOX_LOG.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGV_LOG)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TRCK_VERBOSE)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox GRP_BOX_LOG;
        private System.Windows.Forms.Button BTN_CLEAR_LOG;
        private System.Windows.Forms.Label LBL_VERBOSE;
        private System.Windows.Forms.DataGridView DGV_LOG;
        private System.Windows.Forms.TrackBar TRCK_VERBOSE;
    }
}