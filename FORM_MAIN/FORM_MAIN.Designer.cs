namespace TyrannosaurusPlex
{
    partial class FORM_MAIN
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FORM_MAIN));
            this.GRPBOX_DATABASE = new System.Windows.Forms.GroupBox();
            this.BTN_EDIT_RECIPE = new System.Windows.Forms.Button();
            this.BTN_DELETE_RECIPE = new System.Windows.Forms.Button();
            this.BTN_ADD_RECIPE = new System.Windows.Forms.Button();
            this.DGV_RECIPE_TABLE = new System.Windows.Forms.DataGridView();
            this.MENU_STRIP = new System.Windows.Forms.MenuStrip();
            this.MENU_ITEM_TOOL_STRIP = new System.Windows.Forms.ToolStripMenuItem();
            this.BTN_INJECT = new System.Windows.Forms.Button();
            this.GRPBOX_DATABASE.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGV_RECIPE_TABLE)).BeginInit();
            this.MENU_STRIP.SuspendLayout();
            this.SuspendLayout();
            // 
            // GRPBOX_DATABASE
            // 
            this.GRPBOX_DATABASE.Controls.Add(this.BTN_INJECT);
            this.GRPBOX_DATABASE.Controls.Add(this.BTN_EDIT_RECIPE);
            this.GRPBOX_DATABASE.Controls.Add(this.BTN_DELETE_RECIPE);
            this.GRPBOX_DATABASE.Controls.Add(this.BTN_ADD_RECIPE);
            this.GRPBOX_DATABASE.Location = new System.Drawing.Point(12, 28);
            this.GRPBOX_DATABASE.Name = "GRPBOX_DATABASE";
            this.GRPBOX_DATABASE.Size = new System.Drawing.Size(162, 175);
            this.GRPBOX_DATABASE.TabIndex = 9;
            this.GRPBOX_DATABASE.TabStop = false;
            this.GRPBOX_DATABASE.Text = "Database Tools";
            // 
            // BTN_EDIT_RECIPE
            // 
            this.BTN_EDIT_RECIPE.Location = new System.Drawing.Point(6, 143);
            this.BTN_EDIT_RECIPE.Name = "BTN_EDIT_RECIPE";
            this.BTN_EDIT_RECIPE.Size = new System.Drawing.Size(150, 25);
            this.BTN_EDIT_RECIPE.TabIndex = 10;
            this.BTN_EDIT_RECIPE.Text = "Edit Recipe";
            this.BTN_EDIT_RECIPE.UseVisualStyleBackColor = true;
            this.BTN_EDIT_RECIPE.Click += new System.EventHandler(this.ADD_EDIT_RECIPE);
            // 
            // BTN_DELETE_RECIPE
            // 
            this.BTN_DELETE_RECIPE.Location = new System.Drawing.Point(6, 112);
            this.BTN_DELETE_RECIPE.Name = "BTN_DELETE_RECIPE";
            this.BTN_DELETE_RECIPE.Size = new System.Drawing.Size(150, 25);
            this.BTN_DELETE_RECIPE.TabIndex = 9;
            this.BTN_DELETE_RECIPE.Text = "Delete Recipe";
            this.BTN_DELETE_RECIPE.UseVisualStyleBackColor = true;
            this.BTN_DELETE_RECIPE.Click += new System.EventHandler(this.DELETE_RECORD);
            // 
            // BTN_ADD_RECIPE
            // 
            this.BTN_ADD_RECIPE.Location = new System.Drawing.Point(6, 81);
            this.BTN_ADD_RECIPE.Name = "BTN_ADD_RECIPE";
            this.BTN_ADD_RECIPE.Size = new System.Drawing.Size(150, 25);
            this.BTN_ADD_RECIPE.TabIndex = 7;
            this.BTN_ADD_RECIPE.Text = "Add Recipe";
            this.BTN_ADD_RECIPE.UseVisualStyleBackColor = true;
            this.BTN_ADD_RECIPE.Click += new System.EventHandler(this.ADD_EDIT_RECIPE);
            // 
            // DGV_RECIPE_TABLE
            // 
            this.DGV_RECIPE_TABLE.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGV_RECIPE_TABLE.Location = new System.Drawing.Point(180, 34);
            this.DGV_RECIPE_TABLE.Name = "DGV_RECIPE_TABLE";
            this.DGV_RECIPE_TABLE.Size = new System.Drawing.Size(240, 169);
            this.DGV_RECIPE_TABLE.TabIndex = 10;
            // 
            // MENU_STRIP
            // 
            this.MENU_STRIP.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MENU_ITEM_TOOL_STRIP});
            this.MENU_STRIP.Location = new System.Drawing.Point(0, 0);
            this.MENU_STRIP.Name = "MENU_STRIP";
            this.MENU_STRIP.Size = new System.Drawing.Size(434, 24);
            this.MENU_STRIP.TabIndex = 11;
            this.MENU_STRIP.Text = "Menu Strip";
            // 
            // MENU_ITEM_TOOL_STRIP
            // 
            this.MENU_ITEM_TOOL_STRIP.Name = "MENU_ITEM_TOOL_STRIP";
            this.MENU_ITEM_TOOL_STRIP.Size = new System.Drawing.Size(44, 20);
            this.MENU_ITEM_TOOL_STRIP.Text = "Logs";
            this.MENU_ITEM_TOOL_STRIP.Click += new System.EventHandler(this.MENU_ITEM_TOOL_STRIP_Click);
            // 
            // BTN_INJECT
            // 
            this.BTN_INJECT.Enabled = false;
            this.BTN_INJECT.Location = new System.Drawing.Point(6, 19);
            this.BTN_INJECT.Name = "BTN_INJECT";
            this.BTN_INJECT.Size = new System.Drawing.Size(150, 56);
            this.BTN_INJECT.TabIndex = 12;
            this.BTN_INJECT.Text = "Inject Data";
            this.BTN_INJECT.UseVisualStyleBackColor = true;
            this.BTN_INJECT.Click += new System.EventHandler(this.button1_Click);
            // 
            // FORM_MAIN
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(434, 215);
            this.Controls.Add(this.DGV_RECIPE_TABLE);
            this.Controls.Add(this.GRPBOX_DATABASE);
            this.Controls.Add(this.MENU_STRIP);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.MENU_STRIP;
            this.Name = "FORM_MAIN";
            this.Text = "Tyrannosaurus Plex";
            this.Load += new System.EventHandler(this.FORM_MAIN_Load);
            this.GRPBOX_DATABASE.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DGV_RECIPE_TABLE)).EndInit();
            this.MENU_STRIP.ResumeLayout(false);
            this.MENU_STRIP.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.GroupBox GRPBOX_DATABASE;
        private System.Windows.Forms.Button BTN_ADD_RECIPE;
        private System.Windows.Forms.DataGridView DGV_RECIPE_TABLE;
        private System.Windows.Forms.Button BTN_DELETE_RECIPE;
        private System.Windows.Forms.Button BTN_EDIT_RECIPE;
        private System.Windows.Forms.MenuStrip MENU_STRIP;
        private System.Windows.Forms.ToolStripMenuItem MENU_ITEM_TOOL_STRIP;
        private System.Windows.Forms.Button BTN_INJECT;
    }
}

