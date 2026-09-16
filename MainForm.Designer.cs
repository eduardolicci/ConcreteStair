namespace ConcreteStair
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.OkApplyModifyGetOnOffCancel = new Tekla.Structures.Dialog.UIControls.OkApplyModifyGetOnOffCancel();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.ParametersTabPage = new System.Windows.Forms.TabPage();
            this.floorThickness = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.landingBox = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.alignmentBox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.stairWidthBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.runBox = new System.Windows.Forms.TextBox();
            this.numberOfTreadsBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.riseBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.firstRiserBox = new System.Windows.Forms.TextBox();
            this.saveLoad = new Tekla.Structures.Dialog.UIControls.SaveLoad();
            this.treadTickness = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.nosingRadiusBox = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.tableLayoutPanel.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.ParametersTabPage.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel
            // 
            this.structuresExtender.SetAttributeName(this.tableLayoutPanel, null);
            this.structuresExtender.SetAttributeTypeName(this.tableLayoutPanel, null);
            this.structuresExtender.SetBindPropertyName(this.tableLayoutPanel, null);
            this.tableLayoutPanel.ColumnCount = 1;
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel.Controls.Add(this.OkApplyModifyGetOnOffCancel, 0, 2);
            this.tableLayoutPanel.Controls.Add(this.tabControl, 0, 1);
            this.tableLayoutPanel.Controls.Add(this.saveLoad, 0, 0);
            this.tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.RowCount = 3;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel.Size = new System.Drawing.Size(712, 433);
            this.tableLayoutPanel.TabIndex = 0;
            // 
            // OkApplyModifyGetOnOffCancel
            // 
            this.structuresExtender.SetAttributeName(this.OkApplyModifyGetOnOffCancel, null);
            this.structuresExtender.SetAttributeTypeName(this.OkApplyModifyGetOnOffCancel, null);
            this.structuresExtender.SetBindPropertyName(this.OkApplyModifyGetOnOffCancel, null);
            this.OkApplyModifyGetOnOffCancel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.OkApplyModifyGetOnOffCancel.Location = new System.Drawing.Point(8, 390);
            this.OkApplyModifyGetOnOffCancel.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.OkApplyModifyGetOnOffCancel.Name = "OkApplyModifyGetOnOffCancel";
            this.OkApplyModifyGetOnOffCancel.Size = new System.Drawing.Size(696, 36);
            this.OkApplyModifyGetOnOffCancel.TabIndex = 19;
            this.OkApplyModifyGetOnOffCancel.OkClicked += new System.EventHandler(this.OkApplyModifyGetOnOffCancel_OkClicked);
            this.OkApplyModifyGetOnOffCancel.ApplyClicked += new System.EventHandler(this.OkApplyModifyGetOnOffCancel_ApplyClicked);
            this.OkApplyModifyGetOnOffCancel.ModifyClicked += new System.EventHandler(this.OkApplyModifyGetOnOffCancel_ModifyClicked);
            this.OkApplyModifyGetOnOffCancel.GetClicked += new System.EventHandler(this.OkApplyModifyGetOnOffCancel_GetClicked);
            this.OkApplyModifyGetOnOffCancel.OnOffClicked += new System.EventHandler(this.OkApplyModifyGetOnOffCancel_OnOffClicked);
            this.OkApplyModifyGetOnOffCancel.CancelClicked += new System.EventHandler(this.OkApplyModifyGetOnOffCancel_CancelClicked);
            // 
            // tabControl
            // 
            this.structuresExtender.SetAttributeName(this.tabControl, null);
            this.structuresExtender.SetAttributeTypeName(this.tabControl, null);
            this.structuresExtender.SetBindPropertyName(this.tabControl, null);
            this.tabControl.Controls.Add(this.ParametersTabPage);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(4, 71);
            this.tabControl.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(704, 308);
            this.tabControl.TabIndex = 18;
            // 
            // ParametersTabPage
            // 
            this.structuresExtender.SetAttributeName(this.ParametersTabPage, null);
            this.structuresExtender.SetAttributeTypeName(this.ParametersTabPage, null);
            this.structuresExtender.SetBindPropertyName(this.ParametersTabPage, null);
            this.ParametersTabPage.Controls.Add(this.label10);
            this.ParametersTabPage.Controls.Add(this.nosingRadiusBox);
            this.ParametersTabPage.Controls.Add(this.label9);
            this.ParametersTabPage.Controls.Add(this.treadTickness);
            this.ParametersTabPage.Controls.Add(this.floorThickness);
            this.ParametersTabPage.Controls.Add(this.label8);
            this.ParametersTabPage.Controls.Add(this.landingBox);
            this.ParametersTabPage.Controls.Add(this.label7);
            this.ParametersTabPage.Controls.Add(this.alignmentBox);
            this.ParametersTabPage.Controls.Add(this.label6);
            this.ParametersTabPage.Controls.Add(this.stairWidthBox);
            this.ParametersTabPage.Controls.Add(this.label5);
            this.ParametersTabPage.Controls.Add(this.runBox);
            this.ParametersTabPage.Controls.Add(this.numberOfTreadsBox);
            this.ParametersTabPage.Controls.Add(this.label4);
            this.ParametersTabPage.Controls.Add(this.riseBox);
            this.ParametersTabPage.Controls.Add(this.label3);
            this.ParametersTabPage.Controls.Add(this.label2);
            this.ParametersTabPage.Controls.Add(this.label1);
            this.ParametersTabPage.Controls.Add(this.firstRiserBox);
            this.ParametersTabPage.Location = new System.Drawing.Point(4, 25);
            this.ParametersTabPage.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ParametersTabPage.Name = "ParametersTabPage";
            this.ParametersTabPage.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ParametersTabPage.Size = new System.Drawing.Size(696, 279);
            this.ParametersTabPage.TabIndex = 2;
            this.ParametersTabPage.Text = "albl_Parameters";
            this.ParametersTabPage.UseVisualStyleBackColor = true;
            // 
            // floorThickness
            // 
            this.structuresExtender.SetAttributeName(this.floorThickness, "floorTickness");
            this.structuresExtender.SetAttributeTypeName(this.floorThickness, "Double");
            this.structuresExtender.SetBindPropertyName(this.floorThickness, null);
            this.floorThickness.Location = new System.Drawing.Point(206, 241);
            this.floorThickness.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.floorThickness.Name = "floorThickness";
            this.floorThickness.Size = new System.Drawing.Size(132, 22);
            this.floorThickness.TabIndex = 16;
            // 
            // label8
            // 
            this.structuresExtender.SetAttributeName(this.label8, null);
            this.structuresExtender.SetAttributeTypeName(this.label8, null);
            this.label8.AutoSize = true;
            this.structuresExtender.SetBindPropertyName(this.label8, null);
            this.label8.Location = new System.Drawing.Point(202, 220);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(147, 16);
            this.label8.TabIndex = 15;
            this.label8.Text = "Landing Floor Tickness";
            // 
            // landingBox
            // 
            this.structuresExtender.SetAttributeName(this.landingBox, "landingBox");
            this.structuresExtender.SetAttributeTypeName(this.landingBox, "Double");
            this.structuresExtender.SetBindPropertyName(this.landingBox, null);
            this.landingBox.Location = new System.Drawing.Point(196, 175);
            this.landingBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.landingBox.Name = "landingBox";
            this.landingBox.Size = new System.Drawing.Size(132, 22);
            this.landingBox.TabIndex = 14;
            // 
            // label7
            // 
            this.structuresExtender.SetAttributeName(this.label7, null);
            this.structuresExtender.SetAttributeTypeName(this.label7, null);
            this.label7.AutoSize = true;
            this.structuresExtender.SetBindPropertyName(this.label7, null);
            this.label7.Location = new System.Drawing.Point(195, 155);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(55, 16);
            this.label7.TabIndex = 13;
            this.label7.Text = "Landing";
            // 
            // alignmentBox
            // 
            this.structuresExtender.SetAttributeName(this.alignmentBox, "alignmentBox");
            this.structuresExtender.SetAttributeTypeName(this.alignmentBox, "String");
            this.structuresExtender.SetBindPropertyName(this.alignmentBox, null);
            this.alignmentBox.Location = new System.Drawing.Point(196, 115);
            this.alignmentBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.alignmentBox.Name = "alignmentBox";
            this.alignmentBox.Size = new System.Drawing.Size(132, 22);
            this.alignmentBox.TabIndex = 12;
            // 
            // label6
            // 
            this.structuresExtender.SetAttributeName(this.label6, null);
            this.structuresExtender.SetAttributeTypeName(this.label6, null);
            this.label6.AutoSize = true;
            this.structuresExtender.SetBindPropertyName(this.label6, null);
            this.label6.Location = new System.Drawing.Point(192, 95);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(224, 16);
            this.label6.TabIndex = 11;
            this.label6.Text = "Stair Alignment (Middle, Left or Right)";
            // 
            // stairWidthBox
            // 
            this.structuresExtender.SetAttributeName(this.stairWidthBox, "stairWidthBox");
            this.structuresExtender.SetAttributeTypeName(this.stairWidthBox, "Double");
            this.structuresExtender.SetBindPropertyName(this.stairWidthBox, null);
            this.stairWidthBox.Location = new System.Drawing.Point(196, 52);
            this.stairWidthBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.stairWidthBox.Name = "stairWidthBox";
            this.stairWidthBox.Size = new System.Drawing.Size(132, 22);
            this.stairWidthBox.TabIndex = 10;
            // 
            // label5
            // 
            this.structuresExtender.SetAttributeName(this.label5, null);
            this.structuresExtender.SetAttributeTypeName(this.label5, null);
            this.label5.AutoSize = true;
            this.structuresExtender.SetBindPropertyName(this.label5, null);
            this.label5.Location = new System.Drawing.Point(195, 31);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(71, 16);
            this.label5.TabIndex = 9;
            this.label5.Text = "Stair Width";
            // 
            // runBox
            // 
            this.structuresExtender.SetAttributeName(this.runBox, "runBox");
            this.structuresExtender.SetAttributeTypeName(this.runBox, "Double");
            this.structuresExtender.SetBindPropertyName(this.runBox, null);
            this.runBox.Location = new System.Drawing.Point(31, 115);
            this.runBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.runBox.Name = "runBox";
            this.runBox.Size = new System.Drawing.Size(132, 22);
            this.runBox.TabIndex = 8;
            // 
            // numberOfTreadsBox
            // 
            this.structuresExtender.SetAttributeName(this.numberOfTreadsBox, "numberOfTreadsBox");
            this.structuresExtender.SetAttributeTypeName(this.numberOfTreadsBox, "Double");
            this.structuresExtender.SetBindPropertyName(this.numberOfTreadsBox, null);
            this.numberOfTreadsBox.Location = new System.Drawing.Point(31, 240);
            this.numberOfTreadsBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.numberOfTreadsBox.Name = "numberOfTreadsBox";
            this.numberOfTreadsBox.Size = new System.Drawing.Size(132, 22);
            this.numberOfTreadsBox.TabIndex = 7;
            // 
            // label4
            // 
            this.structuresExtender.SetAttributeName(this.label4, null);
            this.structuresExtender.SetAttributeTypeName(this.label4, null);
            this.label4.AutoSize = true;
            this.structuresExtender.SetBindPropertyName(this.label4, null);
            this.label4.Location = new System.Drawing.Point(27, 220);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(116, 16);
            this.label4.TabIndex = 6;
            this.label4.Text = "Number of Treads";
            // 
            // riseBox
            // 
            this.structuresExtender.SetAttributeName(this.riseBox, "riseBox");
            this.structuresExtender.SetAttributeTypeName(this.riseBox, "Double");
            this.structuresExtender.SetBindPropertyName(this.riseBox, null);
            this.riseBox.Location = new System.Drawing.Point(31, 175);
            this.riseBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.riseBox.Name = "riseBox";
            this.riseBox.Size = new System.Drawing.Size(132, 22);
            this.riseBox.TabIndex = 5;
            // 
            // label3
            // 
            this.structuresExtender.SetAttributeName(this.label3, null);
            this.structuresExtender.SetAttributeTypeName(this.label3, null);
            this.label3.AutoSize = true;
            this.structuresExtender.SetBindPropertyName(this.label3, null);
            this.label3.Location = new System.Drawing.Point(27, 155);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 16);
            this.label3.TabIndex = 4;
            this.label3.Text = "Rise";
            // 
            // label2
            // 
            this.structuresExtender.SetAttributeName(this.label2, null);
            this.structuresExtender.SetAttributeTypeName(this.label2, null);
            this.label2.AutoSize = true;
            this.structuresExtender.SetBindPropertyName(this.label2, null);
            this.label2.Location = new System.Drawing.Point(27, 95);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 16);
            this.label2.TabIndex = 2;
            this.label2.Text = "Run";
            // 
            // label1
            // 
            this.structuresExtender.SetAttributeName(this.label1, null);
            this.structuresExtender.SetAttributeTypeName(this.label1, null);
            this.label1.AutoSize = true;
            this.structuresExtender.SetBindPropertyName(this.label1, null);
            this.label1.Location = new System.Drawing.Point(27, 31);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "First Riser";
            // 
            // firstRiserBox
            // 
            this.structuresExtender.SetAttributeName(this.firstRiserBox, "firstRiserBox");
            this.structuresExtender.SetAttributeTypeName(this.firstRiserBox, "Double");
            this.structuresExtender.SetBindPropertyName(this.firstRiserBox, null);
            this.firstRiserBox.Location = new System.Drawing.Point(31, 51);
            this.firstRiserBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.firstRiserBox.Name = "firstRiserBox";
            this.firstRiserBox.Size = new System.Drawing.Size(132, 22);
            this.firstRiserBox.TabIndex = 0;
            // 
            // saveLoad
            // 
            this.structuresExtender.SetAttributeName(this.saveLoad, null);
            this.structuresExtender.SetAttributeTypeName(this.saveLoad, null);
            this.saveLoad.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.structuresExtender.SetBindPropertyName(this.saveLoad, null);
            this.saveLoad.Dock = System.Windows.Forms.DockStyle.Top;
            this.saveLoad.HelpFileType = Tekla.Structures.Dialog.UIControls.SaveLoad.HelpFileTypeEnum.General;
            this.saveLoad.HelpKeyword = "";
            this.saveLoad.HelpUrl = "";
            this.saveLoad.Location = new System.Drawing.Point(8, 7);
            this.saveLoad.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.saveLoad.Name = "saveLoad";
            this.saveLoad.SaveAsText = "";
            this.saveLoad.Size = new System.Drawing.Size(696, 53);
            this.saveLoad.TabIndex = 0;
            this.saveLoad.UserDefinedHelpFilePath = null;
            // 
            // treadTickness
            // 
            this.structuresExtender.SetAttributeName(this.treadTickness, "treadTickness");
            this.structuresExtender.SetAttributeTypeName(this.treadTickness, "Double");
            this.structuresExtender.SetBindPropertyName(this.treadTickness, null);
            this.treadTickness.Location = new System.Drawing.Point(363, 240);
            this.treadTickness.Margin = new System.Windows.Forms.Padding(4);
            this.treadTickness.Name = "treadTickness";
            this.treadTickness.Size = new System.Drawing.Size(132, 22);
            this.treadTickness.TabIndex = 17;
            // 
            // label9
            // 
            this.structuresExtender.SetAttributeName(this.label9, null);
            this.structuresExtender.SetAttributeTypeName(this.label9, null);
            this.label9.AutoSize = true;
            this.structuresExtender.SetBindPropertyName(this.label9, null);
            this.label9.Location = new System.Drawing.Point(360, 220);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(102, 16);
            this.label9.TabIndex = 18;
            this.label9.Text = "Tread Tickness";
            // 
            // label10
            // 
            this.structuresExtender.SetAttributeName(this.label10, null);
            this.structuresExtender.SetAttributeTypeName(this.label10, null);
            this.label10.AutoSize = true;
            this.structuresExtender.SetBindPropertyName(this.label10, null);
            this.label10.Location = new System.Drawing.Point(520, 220);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(102, 16);
            this.label10.TabIndex = 19;
            this.label10.Text = "Nosing Radius";
            // 
            // nosingRadiusBox
            // 
            this.structuresExtender.SetAttributeName(this.nosingRadiusBox, "nosingRadiusBox");
            this.structuresExtender.SetAttributeTypeName(this.nosingRadiusBox, "Double");
            this.structuresExtender.SetBindPropertyName(this.nosingRadiusBox, null);
            this.nosingRadiusBox.Location = new System.Drawing.Point(523, 240);
            this.nosingRadiusBox.Margin = new System.Windows.Forms.Padding(4);
            this.nosingRadiusBox.Name = "nosingRadiusBox";
            this.nosingRadiusBox.Size = new System.Drawing.Size(132, 22);
            this.nosingRadiusBox.TabIndex = 20;
            // 
            // MainForm
            // 
            this.structuresExtender.SetAttributeName(this, null);
            this.structuresExtender.SetAttributeTypeName(this, null);
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.structuresExtender.SetBindPropertyName(this, null);
            this.ClientSize = new System.Drawing.Size(712, 433);
            this.Controls.Add(this.tableLayoutPanel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "MainForm";
            this.Text = "Concrete Stair";
            this.tableLayoutPanel.ResumeLayout(false);
            this.tabControl.ResumeLayout(false);
            this.ParametersTabPage.ResumeLayout(false);
            this.ParametersTabPage.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private Tekla.Structures.Dialog.UIControls.SaveLoad saveLoad;
        private Tekla.Structures.Dialog.UIControls.OkApplyModifyGetOnOffCancel OkApplyModifyGetOnOffCancel;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage ParametersTabPage;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox firstRiserBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox numberOfTreadsBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox riseBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox runBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox stairWidthBox;
        private System.Windows.Forms.TextBox alignmentBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox landingBox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox floorThickness;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox treadTickness;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox nosingRadiusBox;
        private System.Windows.Forms.Label label10;
    }
}