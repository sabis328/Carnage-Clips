
namespace Carnage_Clips
{
    partial class SearchForm
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
            this.treeDetailView = new System.Windows.Forms.TreeView();
            this.listResutlsView = new System.Windows.Forms.ListView();
            this.label4 = new System.Windows.Forms.Label();
            this.txtUserSearch = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.btnClearSearch = new System.Windows.Forms.Button();
            this.lblDetailedUser = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.lblStatus = new System.Windows.Forms.Label();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // treeDetailView
            // 
            this.treeDetailView.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(40)))), ((int)(((byte)(46)))));
            this.treeDetailView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.treeDetailView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeDetailView.Font = new System.Drawing.Font("Microsoft YaHei UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.treeDetailView.ForeColor = System.Drawing.Color.White;
            this.treeDetailView.Location = new System.Drawing.Point(0, 43);
            this.treeDetailView.Name = "treeDetailView";
            this.treeDetailView.Size = new System.Drawing.Size(374, 223);
            this.treeDetailView.TabIndex = 0;
            // 
            // listResutlsView
            // 
            this.listResutlsView.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(50)))), ((int)(((byte)(56)))));
            this.listResutlsView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listResutlsView.Dock = System.Windows.Forms.DockStyle.Right;
            this.listResutlsView.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listResutlsView.ForeColor = System.Drawing.Color.White;
            this.listResutlsView.HideSelection = false;
            this.listResutlsView.Location = new System.Drawing.Point(374, 0);
            this.listResutlsView.Name = "listResutlsView";
            this.listResutlsView.Size = new System.Drawing.Size(434, 266);
            this.listResutlsView.TabIndex = 1;
            this.listResutlsView.UseCompatibleStateImageBehavior = false;
            this.listResutlsView.View = System.Windows.Forms.View.List;
            this.listResutlsView.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
            this.listResutlsView.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listResutlsView_MouseDoubleClick);
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(171, 36);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(448, 20);
            this.label4.TabIndex = 20;
            this.label4.Text = "Enter name with or without bungie id code";
            this.label4.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // txtUserSearch
            // 
            this.txtUserSearch.Font = new System.Drawing.Font("Microsoft YaHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUserSearch.Location = new System.Drawing.Point(298, 72);
            this.txtUserSearch.Name = "txtUserSearch";
            this.txtUserSearch.Size = new System.Drawing.Size(190, 25);
            this.txtUserSearch.TabIndex = 19;
            this.txtUserSearch.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(40)))), ((int)(((byte)(46)))));
            this.btnSearch.FlatAppearance.BorderSize = 0;
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.Font = new System.Drawing.Font("Microsoft YaHei", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSearch.ForeColor = System.Drawing.Color.White;
            this.btnSearch.Location = new System.Drawing.Point(298, 103);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(190, 34);
            this.btnSearch.TabIndex = 18;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(171, 167);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(448, 34);
            this.label3.TabIndex = 17;
            this.label3.Text = "Double click a user in the grid to load detailed information and characters. You " +
    "can then click the character on the left you wish to view matches for.\r\n";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // btnClearSearch
            // 
            this.btnClearSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(40)))), ((int)(((byte)(46)))));
            this.btnClearSearch.FlatAppearance.BorderSize = 0;
            this.btnClearSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearSearch.Font = new System.Drawing.Font("Microsoft YaHei", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClearSearch.ForeColor = System.Drawing.Color.White;
            this.btnClearSearch.Location = new System.Drawing.Point(650, 209);
            this.btnClearSearch.Name = "btnClearSearch";
            this.btnClearSearch.Size = new System.Drawing.Size(157, 34);
            this.btnClearSearch.TabIndex = 16;
            this.btnClearSearch.Text = "Clear search results";
            this.btnClearSearch.UseVisualStyleBackColor = false;
            // 
            // lblDetailedUser
            // 
            this.lblDetailedUser.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblDetailedUser.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblDetailedUser.ForeColor = System.Drawing.Color.White;
            this.lblDetailedUser.Location = new System.Drawing.Point(0, 0);
            this.lblDetailedUser.Name = "lblDetailedUser";
            this.lblDetailedUser.Size = new System.Drawing.Size(374, 43);
            this.lblDetailedUser.TabIndex = 4;
            this.lblDetailedUser.Text = "No user selected";
            this.lblDetailedUser.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.treeDetailView);
            this.panel2.Controls.Add(this.lblDetailedUser);
            this.panel2.Controls.Add(this.listResutlsView);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 249);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(808, 266);
            this.panel2.TabIndex = 22;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.lblStatus);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 515);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(808, 36);
            this.panel3.TabIndex = 23;
            // 
            // lblStatus
            // 
            this.lblStatus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblStatus.ForeColor = System.Drawing.Color.White;
            this.lblStatus.Location = new System.Drawing.Point(0, 0);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(808, 36);
            this.lblStatus.TabIndex = 3;
            this.lblStatus.Text = "Idle";
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // SearchForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(50)))), ((int)(((byte)(56)))));
            this.ClientSize = new System.Drawing.Size(808, 551);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtUserSearch);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnClearSearch);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel3);
            this.Font = new System.Drawing.Font("Microsoft YaHei UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximumSize = new System.Drawing.Size(808, 551);
            this.MinimumSize = new System.Drawing.Size(808, 551);
            this.Name = "SearchForm";
            this.Text = "SearchForm";
            this.Load += new System.EventHandler(this.SearchForm_Load);
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView treeDetailView;
        private System.Windows.Forms.ListView listResutlsView;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtUserSearch;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnClearSearch;
        private System.Windows.Forms.Label lblDetailedUser;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label lblStatus;
    }
}