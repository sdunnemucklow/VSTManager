// Original work Copyright (c) 2017 Samuel Dunne-Mucklow

namespace VSTManager
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.installButton = new System.Windows.Forms.Button();
            this.storesBox = new System.Windows.Forms.ComboBox();
            this.connectButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.removeButton = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.storeTabPage = new System.Windows.Forms.TabPage();
            this.storeDeselectAllButton = new System.Windows.Forms.Button();
            this.storeSelectAllButton = new System.Windows.Forms.Button();
            this.storeVstBox = new System.Windows.Forms.CheckedListBox();
            this.localTabPage = new System.Windows.Forms.TabPage();
            this.browseButton = new System.Windows.Forms.Button();
            this.locationRemoveButton = new System.Windows.Forms.Button();
            this.locationSelectButton = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.locationBox = new System.Windows.Forms.ComboBox();
            this.localDeselectAllButton = new System.Windows.Forms.Button();
            this.localSelectAllButton = new System.Windows.Forms.Button();
            this.uninstallButton = new System.Windows.Forms.Button();
            this.reinstallButton = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.localVstBox = new System.Windows.Forms.CheckedListBox();
            this.installLabel = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.storeTabPage.SuspendLayout();
            this.localTabPage.SuspendLayout();
            this.SuspendLayout();
            // 
            // installButton
            // 
            this.installButton.Location = new System.Drawing.Point(431, 223);
            this.installButton.Name = "installButton";
            this.installButton.Size = new System.Drawing.Size(75, 23);
            this.installButton.TabIndex = 2;
            this.installButton.Text = "Install";
            this.installButton.UseVisualStyleBackColor = true;
            this.installButton.Click += new System.EventHandler(this.installButton_Click);
            // 
            // storesBox
            // 
            this.storesBox.FormattingEnabled = true;
            this.storesBox.Location = new System.Drawing.Point(22, 31);
            this.storesBox.Name = "storesBox";
            this.storesBox.Size = new System.Drawing.Size(257, 21);
            this.storesBox.TabIndex = 3;
            // 
            // connectButton
            // 
            this.connectButton.Location = new System.Drawing.Point(318, 30);
            this.connectButton.Name = "connectButton";
            this.connectButton.Size = new System.Drawing.Size(75, 23);
            this.connectButton.TabIndex = 4;
            this.connectButton.Text = "Select";
            this.connectButton.UseVisualStyleBackColor = true;
            this.connectButton.Click += new System.EventHandler(this.selectButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label1.Location = new System.Drawing.Point(18, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 20);
            this.label1.TabIndex = 5;
            this.label1.Text = "Store";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label2.Location = new System.Drawing.Point(18, 63);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(115, 20);
            this.label2.TabIndex = 6;
            this.label2.Text = "Available VSTs";
            // 
            // removeButton
            // 
            this.removeButton.Location = new System.Drawing.Point(431, 30);
            this.removeButton.Name = "removeButton";
            this.removeButton.Size = new System.Drawing.Size(75, 23);
            this.removeButton.TabIndex = 8;
            this.removeButton.Text = "Remove";
            this.removeButton.UseVisualStyleBackColor = true;
            this.removeButton.Click += new System.EventHandler(this.removeButton_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.localTabPage);
            this.tabControl1.Controls.Add(this.storeTabPage);
            this.tabControl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(560, 461);
            this.tabControl1.TabIndex = 9;
            // 
            // storeTabPage
            // 
            this.storeTabPage.Controls.Add(this.storeDeselectAllButton);
            this.storeTabPage.Controls.Add(this.storeSelectAllButton);
            this.storeTabPage.Controls.Add(this.storeVstBox);
            this.storeTabPage.Controls.Add(this.storesBox);
            this.storeTabPage.Controls.Add(this.removeButton);
            this.storeTabPage.Controls.Add(this.installButton);
            this.storeTabPage.Controls.Add(this.label2);
            this.storeTabPage.Controls.Add(this.connectButton);
            this.storeTabPage.Controls.Add(this.label1);
            this.storeTabPage.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.storeTabPage.Location = new System.Drawing.Point(4, 29);
            this.storeTabPage.Name = "storeTabPage";
            this.storeTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.storeTabPage.Size = new System.Drawing.Size(552, 428);
            this.storeTabPage.TabIndex = 0;
            this.storeTabPage.Text = "Store";
            this.storeTabPage.UseVisualStyleBackColor = true;
            // 
            // storeDeselectAllButton
            // 
            this.storeDeselectAllButton.Location = new System.Drawing.Point(138, 391);
            this.storeDeselectAllButton.Name = "storeDeselectAllButton";
            this.storeDeselectAllButton.Size = new System.Drawing.Size(75, 23);
            this.storeDeselectAllButton.TabIndex = 11;
            this.storeDeselectAllButton.Text = "Deselect All";
            this.storeDeselectAllButton.UseVisualStyleBackColor = true;
            this.storeDeselectAllButton.Click += new System.EventHandler(this.storeDeselectAllButton_Click);
            // 
            // storeSelectAllButton
            // 
            this.storeSelectAllButton.Location = new System.Drawing.Point(21, 391);
            this.storeSelectAllButton.Name = "storeSelectAllButton";
            this.storeSelectAllButton.Size = new System.Drawing.Size(75, 23);
            this.storeSelectAllButton.TabIndex = 10;
            this.storeSelectAllButton.Text = "Select All";
            this.storeSelectAllButton.UseVisualStyleBackColor = true;
            this.storeSelectAllButton.Click += new System.EventHandler(this.storeSelectAllButton_Click);
            // 
            // storeVstBox
            // 
            this.storeVstBox.CheckOnClick = true;
            this.storeVstBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.storeVstBox.FormattingEnabled = true;
            this.storeVstBox.Location = new System.Drawing.Point(22, 86);
            this.storeVstBox.Name = "storeVstBox";
            this.storeVstBox.Size = new System.Drawing.Size(385, 298);
            this.storeVstBox.TabIndex = 9;
            // 
            // localTabPage
            // 
            this.localTabPage.Controls.Add(this.browseButton);
            this.localTabPage.Controls.Add(this.locationRemoveButton);
            this.localTabPage.Controls.Add(this.locationSelectButton);
            this.localTabPage.Controls.Add(this.label5);
            this.localTabPage.Controls.Add(this.locationBox);
            this.localTabPage.Controls.Add(this.localDeselectAllButton);
            this.localTabPage.Controls.Add(this.localSelectAllButton);
            this.localTabPage.Controls.Add(this.uninstallButton);
            this.localTabPage.Controls.Add(this.reinstallButton);
            this.localTabPage.Controls.Add(this.label4);
            this.localTabPage.Controls.Add(this.localVstBox);
            this.localTabPage.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.localTabPage.Location = new System.Drawing.Point(4, 29);
            this.localTabPage.Name = "localTabPage";
            this.localTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.localTabPage.Size = new System.Drawing.Size(552, 428);
            this.localTabPage.TabIndex = 1;
            this.localTabPage.Text = "Local";
            this.localTabPage.UseVisualStyleBackColor = true;
            // 
            // browseButton
            // 
            this.browseButton.Location = new System.Drawing.Point(276, 30);
            this.browseButton.Name = "browseButton";
            this.browseButton.Size = new System.Drawing.Size(24, 23);
            this.browseButton.TabIndex = 17;
            this.browseButton.Text = "...";
            this.browseButton.UseVisualStyleBackColor = true;
            this.browseButton.Click += new System.EventHandler(this.browseButton_Click);
            // 
            // locationRemoveButton
            // 
            this.locationRemoveButton.Location = new System.Drawing.Point(431, 30);
            this.locationRemoveButton.Name = "locationRemoveButton";
            this.locationRemoveButton.Size = new System.Drawing.Size(75, 23);
            this.locationRemoveButton.TabIndex = 16;
            this.locationRemoveButton.Text = "Remove";
            this.locationRemoveButton.UseVisualStyleBackColor = true;
            this.locationRemoveButton.Click += new System.EventHandler(this.locationRemoveButton_Click);
            // 
            // locationSelectButton
            // 
            this.locationSelectButton.Location = new System.Drawing.Point(318, 30);
            this.locationSelectButton.Name = "locationSelectButton";
            this.locationSelectButton.Size = new System.Drawing.Size(75, 23);
            this.locationSelectButton.TabIndex = 15;
            this.locationSelectButton.Text = "Select";
            this.locationSelectButton.UseVisualStyleBackColor = true;
            this.locationSelectButton.Click += new System.EventHandler(this.locationSelectButton_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label5.Location = new System.Drawing.Point(18, 10);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(116, 20);
            this.label5.TabIndex = 14;
            this.label5.Text = "Install Location";
            // 
            // locationBox
            // 
            this.locationBox.FormattingEnabled = true;
            this.locationBox.Location = new System.Drawing.Point(22, 31);
            this.locationBox.Name = "locationBox";
            this.locationBox.Size = new System.Drawing.Size(257, 21);
            this.locationBox.TabIndex = 13;
            // 
            // localDeselectAllButton
            // 
            this.localDeselectAllButton.Location = new System.Drawing.Point(138, 391);
            this.localDeselectAllButton.Name = "localDeselectAllButton";
            this.localDeselectAllButton.Size = new System.Drawing.Size(75, 23);
            this.localDeselectAllButton.TabIndex = 12;
            this.localDeselectAllButton.Text = "Deselect All";
            this.localDeselectAllButton.UseVisualStyleBackColor = true;
            this.localDeselectAllButton.Click += new System.EventHandler(this.localDeselectAllButton_Click);
            // 
            // localSelectAllButton
            // 
            this.localSelectAllButton.Location = new System.Drawing.Point(21, 391);
            this.localSelectAllButton.Name = "localSelectAllButton";
            this.localSelectAllButton.Size = new System.Drawing.Size(75, 23);
            this.localSelectAllButton.TabIndex = 11;
            this.localSelectAllButton.Text = "Select All";
            this.localSelectAllButton.UseVisualStyleBackColor = true;
            this.localSelectAllButton.Click += new System.EventHandler(this.localSelectAllButton_Click);
            // 
            // uninstallButton
            // 
            this.uninstallButton.Location = new System.Drawing.Point(431, 173);
            this.uninstallButton.Name = "uninstallButton";
            this.uninstallButton.Size = new System.Drawing.Size(75, 23);
            this.uninstallButton.TabIndex = 10;
            this.uninstallButton.Text = "Uninstall";
            this.uninstallButton.UseVisualStyleBackColor = true;
            this.uninstallButton.Click += new System.EventHandler(this.uninstallButton_Click);
            // 
            // reinstallButton
            // 
            this.reinstallButton.Location = new System.Drawing.Point(431, 241);
            this.reinstallButton.Name = "reinstallButton";
            this.reinstallButton.Size = new System.Drawing.Size(75, 23);
            this.reinstallButton.TabIndex = 8;
            this.reinstallButton.Text = "Reinstall";
            this.reinstallButton.UseVisualStyleBackColor = true;
            this.reinstallButton.Click += new System.EventHandler(this.reinstallButton_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label4.Location = new System.Drawing.Point(18, 63);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(112, 20);
            this.label4.TabIndex = 7;
            this.label4.Text = "Installed VSTs";
            // 
            // localVstBox
            // 
            this.localVstBox.CheckOnClick = true;
            this.localVstBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.localVstBox.FormattingEnabled = true;
            this.localVstBox.Location = new System.Drawing.Point(22, 86);
            this.localVstBox.Name = "localVstBox";
            this.localVstBox.Size = new System.Drawing.Size(385, 298);
            this.localVstBox.TabIndex = 0;
            // 
            // installLabel
            // 
            this.installLabel.Location = new System.Drawing.Point(35, 490);
            this.installLabel.Name = "installLabel";
            this.installLabel.Size = new System.Drawing.Size(502, 21);
            this.installLabel.TabIndex = 13;
            this.installLabel.Text = "No install currently in progress.";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 512);
            this.Controls.Add(this.installLabel);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "VST Manager";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabControl1.ResumeLayout(false);
            this.storeTabPage.ResumeLayout(false);
            this.storeTabPage.PerformLayout();
            this.localTabPage.ResumeLayout(false);
            this.localTabPage.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button installButton;
        private System.Windows.Forms.ComboBox storesBox;
        private System.Windows.Forms.Button connectButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button removeButton;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage storeTabPage;
        private System.Windows.Forms.TabPage localTabPage;
        private System.Windows.Forms.CheckedListBox storeVstBox;
        private System.Windows.Forms.CheckedListBox localVstBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button storeSelectAllButton;
        private System.Windows.Forms.Button localSelectAllButton;
        private System.Windows.Forms.Button uninstallButton;
        private System.Windows.Forms.Button reinstallButton;
        private System.Windows.Forms.Button storeDeselectAllButton;
        private System.Windows.Forms.Label installLabel;
        private System.Windows.Forms.Button localDeselectAllButton;
        private System.Windows.Forms.Button locationRemoveButton;
        private System.Windows.Forms.Button locationSelectButton;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox locationBox;
        private System.Windows.Forms.Button browseButton;
    }
}

