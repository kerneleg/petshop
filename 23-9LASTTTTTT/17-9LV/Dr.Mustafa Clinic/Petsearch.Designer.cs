namespace Dr.Mustafa_Clinic
{
    partial class Petsearch
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
            this.PetSearchdataGrid = new System.Windows.Forms.DataGridView();
            this.select = new System.Windows.Forms.DataGridViewButtonColumn();
            this.Closebut = new System.Windows.Forms.Button();
            this.owner_filter = new System.Windows.Forms.TextBox();
            this.pet_filter = new System.Windows.Forms.TextBox();
            this.breed_filter = new System.Windows.Forms.TextBox();
            this.type_filter = new System.Windows.Forms.TextBox();
            this.vaccin_filter = new System.Windows.Forms.CheckBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.PetSearchdataGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // PetSearchdataGrid
            // 
            this.PetSearchdataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.PetSearchdataGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.select});
            this.PetSearchdataGrid.Location = new System.Drawing.Point(12, 43);
            this.PetSearchdataGrid.Name = "PetSearchdataGrid";
            this.PetSearchdataGrid.Size = new System.Drawing.Size(637, 439);
            this.PetSearchdataGrid.TabIndex = 1;
            this.PetSearchdataGrid.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.PetSearchdataGrid_CellContentClick);
            // 
            // select
            // 
            this.select.HeaderText = "Select";
            this.select.Name = "select";
            this.select.Text = "Select";
            this.select.UseColumnTextForButtonValue = true;
            // 
            // Closebut
            // 
            this.Closebut.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Closebut.ForeColor = System.Drawing.Color.White;
            this.Closebut.Location = new System.Drawing.Point(12, 488);
            this.Closebut.Name = "Closebut";
            this.Closebut.Size = new System.Drawing.Size(636, 40);
            this.Closebut.TabIndex = 2;
            this.Closebut.Text = "Close";
            this.Closebut.UseVisualStyleBackColor = true;
            this.Closebut.Click += new System.EventHandler(this.button1_Click);
            // 
            // owner_filter
            // 
            this.owner_filter.Location = new System.Drawing.Point(131, 17);
            this.owner_filter.Name = "owner_filter";
            this.owner_filter.Size = new System.Drawing.Size(101, 20);
            this.owner_filter.TabIndex = 9;
            // 
            // pet_filter
            // 
            this.pet_filter.Location = new System.Drawing.Point(238, 17);
            this.pet_filter.Name = "pet_filter";
            this.pet_filter.Size = new System.Drawing.Size(101, 20);
            this.pet_filter.TabIndex = 10;
            // 
            // breed_filter
            // 
            this.breed_filter.Location = new System.Drawing.Point(453, 17);
            this.breed_filter.Name = "breed_filter";
            this.breed_filter.Size = new System.Drawing.Size(98, 20);
            this.breed_filter.TabIndex = 12;
            // 
            // type_filter
            // 
            this.type_filter.Location = new System.Drawing.Point(345, 17);
            this.type_filter.Name = "type_filter";
            this.type_filter.Size = new System.Drawing.Size(102, 20);
            this.type_filter.TabIndex = 14;
            // 
            // vaccin_filter
            // 
            this.vaccin_filter.AutoSize = true;
            this.vaccin_filter.ForeColor = System.Drawing.Color.White;
            this.vaccin_filter.Location = new System.Drawing.Point(557, 20);
            this.vaccin_filter.Name = "vaccin_filter";
            this.vaccin_filter.Size = new System.Drawing.Size(97, 17);
            this.vaccin_filter.TabIndex = 15;
            this.vaccin_filter.Text = "Week Vaccines";
            this.vaccin_filter.UseVisualStyleBackColor = true;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Dr.Mustafa_Clinic.Properties.Resources.Search;
            this.pictureBox1.Location = new System.Drawing.Point(12, 17);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(25, 20);
            this.pictureBox1.TabIndex = 8;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // Petsearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DodgerBlue;
            this.ClientSize = new System.Drawing.Size(661, 540);
            this.Controls.Add(this.vaccin_filter);
            this.Controls.Add(this.type_filter);
            this.Controls.Add(this.breed_filter);
            this.Controls.Add(this.pet_filter);
            this.Controls.Add(this.owner_filter);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.Closebut);
            this.Controls.Add(this.PetSearchdataGrid);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Petsearch";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Petsearch";
            ((System.ComponentModel.ISupportInitialize)(this.PetSearchdataGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView PetSearchdataGrid;
        private System.Windows.Forms.Button Closebut;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox owner_filter;
        private System.Windows.Forms.DataGridViewButtonColumn select;
        private System.Windows.Forms.TextBox pet_filter;
        private System.Windows.Forms.TextBox breed_filter;
        private System.Windows.Forms.TextBox type_filter;
        private System.Windows.Forms.CheckBox vaccin_filter;
    }
}