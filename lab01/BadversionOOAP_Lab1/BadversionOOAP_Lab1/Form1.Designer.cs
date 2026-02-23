using System.Windows.Forms;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace BadversionOOAP_Lab1
{
    partial class PCBuilder
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            comboBoxCPU = new ComboBox();
            comboBoxMB = new ComboBox();
            comboBoxRAM = new ComboBox();
            comboBoxGPU = new ComboBox();
            comboBoxHDD = new ComboBox();
            comboBoxSSD = new ComboBox();
            comboBoxPSU = new ComboBox();
            CPULabel = new Label();
            RAMlabel = new Label();
            MBlabel = new Label();
            GPUlabel = new Label();
            HDDlabel = new Label();
            SSDlabel = new Label();
            PSUlabel = new Label();
            PriceCountButton = new Button();
            BuildButton = new Button();
            menuStrip1 = new MenuStrip();
            ToolStripMenuItem = new ToolStripMenuItem();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // comboBoxCPU
            // 
            comboBoxCPU.FormattingEnabled = true;
            comboBoxCPU.Location = new Point(196, 51);
            comboBoxCPU.Name = "comboBoxCPU";
            comboBoxCPU.Size = new Size(121, 23);
            comboBoxCPU.TabIndex = 0;
            // 
            // comboBoxMB
            // 
            comboBoxMB.FormattingEnabled = true;
            comboBoxMB.Location = new Point(196, 162);
            comboBoxMB.Name = "comboBoxMB";
            comboBoxMB.Size = new Size(121, 23);
            comboBoxMB.TabIndex = 1;
            // 
            // comboBoxRAM
            // 
            comboBoxRAM.FormattingEnabled = true;
            comboBoxRAM.Location = new Point(196, 215);
            comboBoxRAM.Name = "comboBoxRAM";
            comboBoxRAM.Size = new Size(121, 23);
            comboBoxRAM.TabIndex = 2;
            // 
            // comboBoxGPU
            // 
            comboBoxGPU.FormattingEnabled = true;
            comboBoxGPU.Location = new Point(196, 105);
            comboBoxGPU.Name = "comboBoxGPU";
            comboBoxGPU.Size = new Size(121, 23);
            comboBoxGPU.TabIndex = 3;
            // 
            // comboBoxHDD
            // 
            comboBoxHDD.FormattingEnabled = true;
            comboBoxHDD.Location = new Point(196, 271);
            comboBoxHDD.Name = "comboBoxHDD";
            comboBoxHDD.Size = new Size(121, 23);
            comboBoxHDD.TabIndex = 4;
            // 
            // comboBoxSSD
            // 
            comboBoxSSD.FormattingEnabled = true;
            comboBoxSSD.Location = new Point(196, 328);
            comboBoxSSD.Name = "comboBoxSSD";
            comboBoxSSD.Size = new Size(121, 23);
            comboBoxSSD.TabIndex = 5;
            // 
            // comboBoxPSU
            // 
            comboBoxPSU.FormattingEnabled = true;
            comboBoxPSU.Location = new Point(196, 384);
            comboBoxPSU.Name = "comboBoxPSU";
            comboBoxPSU.Size = new Size(121, 23);
            comboBoxPSU.TabIndex = 6;
            // 
            // CPULabel
            // 
            CPULabel.AutoSize = true;
            CPULabel.Font = new System.Drawing.Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            CPULabel.Location = new Point(12, 53);
            CPULabel.Name = "CPULabel";
            CPULabel.Size = new Size(89, 21);
            CPULabel.TabIndex = 7;
            CPULabel.Text = "Процессор";
            // 
            // RAMlabel
            // 
            RAMlabel.AutoSize = true;
            RAMlabel.Font = new System.Drawing.Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            RAMlabel.Location = new Point(12, 217);
            RAMlabel.Name = "RAMlabel";
            RAMlabel.Size = new Size(160, 21);
            RAMlabel.TabIndex = 8;
            RAMlabel.Text = "Оперативная память";
            // 
            // MBlabel
            // 
            MBlabel.AutoSize = true;
            MBlabel.Font = new System.Drawing.Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            MBlabel.Location = new Point(12, 164);
            MBlabel.Name = "MBlabel";
            MBlabel.Size = new Size(149, 21);
            MBlabel.TabIndex = 9;
            MBlabel.Text = "Материнская плата";
            // 
            // GPUlabel
            // 
            GPUlabel.AutoSize = true;
            GPUlabel.Font = new System.Drawing.Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            GPUlabel.Location = new Point(12, 107);
            GPUlabel.Name = "GPUlabel";
            GPUlabel.Size = new Size(94, 21);
            GPUlabel.TabIndex = 10;
            GPUlabel.Text = "Видеокарта";
            // 
            // HDDlabel
            // 
            HDDlabel.AutoSize = true;
            HDDlabel.Font = new System.Drawing.Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            HDDlabel.Location = new Point(12, 273);
            HDDlabel.Name = "HDDlabel";
            HDDlabel.Size = new Size(109, 21);
            HDDlabel.TabIndex = 11;
            HDDlabel.Text = "Жесткий диск";
            // 
            // SSDlabel
            // 
            SSDlabel.AutoSize = true;
            SSDlabel.Font = new System.Drawing.Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            SSDlabel.Location = new Point(12, 330);
            SSDlabel.Name = "SSDlabel";
            SSDlabel.Size = new Size(39, 21);
            SSDlabel.TabIndex = 12;
            SSDlabel.Text = "SSD";
            // 
            // PSUlabel
            // 
            PSUlabel.AutoSize = true;
            PSUlabel.Font = new System.Drawing.Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            PSUlabel.Location = new Point(12, 386);
            PSUlabel.Name = "PSUlabel";
            PSUlabel.Size = new Size(107, 21);
            PSUlabel.TabIndex = 13;
            PSUlabel.Text = "Блок питания";
            // 
            // PriceCountButton
            // 
            PriceCountButton.Location = new Point(562, 53);
            PriceCountButton.Name = "PriceCountButton";
            PriceCountButton.Size = new Size(98, 39);
            PriceCountButton.TabIndex = 14;
            PriceCountButton.Text = "Рассчитать стоимость";
            PriceCountButton.UseVisualStyleBackColor = true;
            PriceCountButton.Click += PriceCurrentButton_Click;
            // 
            // BuildButton
            // 
            BuildButton.Location = new Point(562, 146);
            BuildButton.Name = "BuildButton";
            BuildButton.Size = new Size(98, 39);
            BuildButton.TabIndex = 15;
            BuildButton.Text = "Собрать";
            BuildButton.UseVisualStyleBackColor = true;
            BuildButton.Click += BuildButton_Click;
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { ToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(800, 24);
            menuStrip1.TabIndex = 16;
            menuStrip1.Text = "menuStrip1";
            // 
            // ToolStripMenuItem
            // 
            ToolStripMenuItem.Name = "ToolStripMenuItem";
            ToolStripMenuItem.Size = new Size(94, 20);
            ToolStripMenuItem.Text = "О программе";
            ToolStripMenuItem.Click += ToolStripMenuItem_Click;
            // 
            // PCBuilder
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 449);
            Controls.Add(BuildButton);
            Controls.Add(PriceCountButton);
            Controls.Add(PSUlabel);
            Controls.Add(SSDlabel);
            Controls.Add(HDDlabel);
            Controls.Add(GPUlabel);
            Controls.Add(MBlabel);
            Controls.Add(RAMlabel);
            Controls.Add(CPULabel);
            Controls.Add(comboBoxPSU);
            Controls.Add(comboBoxSSD);
            Controls.Add(comboBoxHDD);
            Controls.Add(comboBoxGPU);
            Controls.Add(comboBoxRAM);
            Controls.Add(comboBoxMB);
            Controls.Add(comboBoxCPU);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "PCBuilder";
            Text = "PCBuilder";
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox comboBoxCPU;
        private ComboBox comboBoxMB;
        private ComboBox comboBoxRAM;
        private ComboBox comboBoxGPU;
        private ComboBox comboBoxHDD;
        private ComboBox comboBoxSSD;
        private ComboBox comboBoxPSU;
        private Label CPULabel;
        private Label RAMlabel;
        private Label MBlabel;
        private Label GPUlabel;
        private Label HDDlabel;
        private Label SSDlabel;
        private Label PSUlabel;
        private Button PriceCountButton;
        private Button BuildButton;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem ToolStripMenuItem;
    }
}

