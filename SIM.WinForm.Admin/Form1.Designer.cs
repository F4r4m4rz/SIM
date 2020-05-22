namespace SIM.WinForm.Admin
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
            this.Nodes = new System.Windows.Forms.Label();
            this.btnAddNode = new System.Windows.Forms.Button();
            this.lstNodes = new System.Windows.Forms.ListBox();
            this.btnLoadFromJson = new System.Windows.Forms.Button();
            this.btnSaveAsJson = new System.Windows.Forms.Button();
            this.lstRelations = new System.Windows.Forms.ListBox();
            this.btnAddRelation = new System.Windows.Forms.Button();
            this.lblRelations = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lstProperties = new System.Windows.Forms.ListBox();
            this.btnAddProperty = new System.Windows.Forms.Button();
            this.lblProperties = new System.Windows.Forms.Label();
            this.btnCsCode = new System.Windows.Forms.Button();
            this.btnCompile = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Nodes
            // 
            this.Nodes.AutoSize = true;
            this.Nodes.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Nodes.Location = new System.Drawing.Point(8, 26);
            this.Nodes.Name = "Nodes";
            this.Nodes.Size = new System.Drawing.Size(60, 20);
            this.Nodes.TabIndex = 1;
            this.Nodes.Text = "Nodes";
            // 
            // btnAddNode
            // 
            this.btnAddNode.Location = new System.Drawing.Point(12, 49);
            this.btnAddNode.Name = "btnAddNode";
            this.btnAddNode.Size = new System.Drawing.Size(75, 23);
            this.btnAddNode.TabIndex = 2;
            this.btnAddNode.Text = "Add";
            this.btnAddNode.UseVisualStyleBackColor = true;
            this.btnAddNode.Click += new System.EventHandler(this.btnAddNode_Click);
            // 
            // lstNodes
            // 
            this.lstNodes.FormattingEnabled = true;
            this.lstNodes.Location = new System.Drawing.Point(12, 77);
            this.lstNodes.Name = "lstNodes";
            this.lstNodes.Size = new System.Drawing.Size(238, 472);
            this.lstNodes.TabIndex = 3;
            this.lstNodes.SelectedIndexChanged += new System.EventHandler(this.lstNodes_SelectedIndexChanged);
            // 
            // btnLoadFromJson
            // 
            this.btnLoadFromJson.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnLoadFromJson.Location = new System.Drawing.Point(12, 0);
            this.btnLoadFromJson.Name = "btnLoadFromJson";
            this.btnLoadFromJson.Size = new System.Drawing.Size(727, 23);
            this.btnLoadFromJson.TabIndex = 4;
            this.btnLoadFromJson.Text = "Load from JSON";
            this.btnLoadFromJson.UseVisualStyleBackColor = true;
            this.btnLoadFromJson.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnSaveAsJson
            // 
            this.btnSaveAsJson.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnSaveAsJson.Location = new System.Drawing.Point(12, 563);
            this.btnSaveAsJson.Name = "btnSaveAsJson";
            this.btnSaveAsJson.Size = new System.Drawing.Size(727, 23);
            this.btnSaveAsJson.TabIndex = 5;
            this.btnSaveAsJson.Text = "Save as JSON";
            this.btnSaveAsJson.UseVisualStyleBackColor = true;
            this.btnSaveAsJson.Click += new System.EventHandler(this.btnSaveAsJson_Click);
            // 
            // lstRelations
            // 
            this.lstRelations.FormattingEnabled = true;
            this.lstRelations.Location = new System.Drawing.Point(270, 77);
            this.lstRelations.Name = "lstRelations";
            this.lstRelations.Size = new System.Drawing.Size(238, 472);
            this.lstRelations.TabIndex = 8;
            // 
            // btnAddRelation
            // 
            this.btnAddRelation.Location = new System.Drawing.Point(270, 49);
            this.btnAddRelation.Name = "btnAddRelation";
            this.btnAddRelation.Size = new System.Drawing.Size(75, 23);
            this.btnAddRelation.TabIndex = 7;
            this.btnAddRelation.Text = "Add";
            this.btnAddRelation.UseVisualStyleBackColor = true;
            this.btnAddRelation.Click += new System.EventHandler(this.btnAddRelation_Click);
            // 
            // lblRelations
            // 
            this.lblRelations.AutoSize = true;
            this.lblRelations.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRelations.Location = new System.Drawing.Point(266, 26);
            this.lblRelations.Name = "lblRelations";
            this.lblRelations.Size = new System.Drawing.Size(85, 20);
            this.lblRelations.TabIndex = 6;
            this.lblRelations.Text = "Relations";
            // 
            // groupBox1
            // 
            this.groupBox1.Location = new System.Drawing.Point(526, 87);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(212, 223);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "groupBox1";
            // 
            // lstProperties
            // 
            this.lstProperties.FormattingEnabled = true;
            this.lstProperties.Location = new System.Drawing.Point(526, 368);
            this.lstProperties.Name = "lstProperties";
            this.lstProperties.Size = new System.Drawing.Size(212, 186);
            this.lstProperties.TabIndex = 10;
            // 
            // btnAddProperty
            // 
            this.btnAddProperty.Location = new System.Drawing.Point(526, 336);
            this.btnAddProperty.Name = "btnAddProperty";
            this.btnAddProperty.Size = new System.Drawing.Size(75, 23);
            this.btnAddProperty.TabIndex = 12;
            this.btnAddProperty.Text = "Add";
            this.btnAddProperty.UseVisualStyleBackColor = true;
            this.btnAddProperty.Click += new System.EventHandler(this.btnAddProperty_Click);
            // 
            // lblProperties
            // 
            this.lblProperties.AutoSize = true;
            this.lblProperties.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProperties.Location = new System.Drawing.Point(522, 313);
            this.lblProperties.Name = "lblProperties";
            this.lblProperties.Size = new System.Drawing.Size(91, 20);
            this.lblProperties.TabIndex = 11;
            this.lblProperties.Text = "Properties";
            // 
            // btnCsCode
            // 
            this.btnCsCode.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnCsCode.Location = new System.Drawing.Point(11, 592);
            this.btnCsCode.Name = "btnCsCode";
            this.btnCsCode.Size = new System.Drawing.Size(727, 23);
            this.btnCsCode.TabIndex = 13;
            this.btnCsCode.Text = "Generate cs code";
            this.btnCsCode.UseVisualStyleBackColor = true;
            this.btnCsCode.Click += new System.EventHandler(this.btnCsCode_Click);
            // 
            // btnCompile
            // 
            this.btnCompile.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnCompile.Location = new System.Drawing.Point(11, 621);
            this.btnCompile.Name = "btnCompile";
            this.btnCompile.Size = new System.Drawing.Size(727, 23);
            this.btnCompile.TabIndex = 14;
            this.btnCompile.Text = "Compile";
            this.btnCompile.UseVisualStyleBackColor = true;
            this.btnCompile.Click += new System.EventHandler(this.btnCompile_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(750, 652);
            this.Controls.Add(this.btnCompile);
            this.Controls.Add(this.btnCsCode);
            this.Controls.Add(this.btnAddProperty);
            this.Controls.Add(this.lblProperties);
            this.Controls.Add(this.lstProperties);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lstRelations);
            this.Controls.Add(this.btnAddRelation);
            this.Controls.Add(this.lblRelations);
            this.Controls.Add(this.btnSaveAsJson);
            this.Controls.Add(this.btnLoadFromJson);
            this.Controls.Add(this.lstNodes);
            this.Controls.Add(this.btnAddNode);
            this.Controls.Add(this.Nodes);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label Nodes;
        private System.Windows.Forms.Button btnAddNode;
        private System.Windows.Forms.ListBox lstNodes;
        private System.Windows.Forms.Button btnLoadFromJson;
        private System.Windows.Forms.Button btnSaveAsJson;
        private System.Windows.Forms.ListBox lstRelations;
        private System.Windows.Forms.Button btnAddRelation;
        private System.Windows.Forms.Label lblRelations;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListBox lstProperties;
        private System.Windows.Forms.Button btnAddProperty;
        private System.Windows.Forms.Label lblProperties;
        private System.Windows.Forms.Button btnCsCode;
        private System.Windows.Forms.Button btnCompile;
    }
}

