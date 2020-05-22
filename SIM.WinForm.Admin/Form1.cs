using SIM.CodeEngine.Commands;
using SIM.CodeEngine.Dynamic;
using SIM.CodeEngine.Factory;
using SIM.Core.Commands;
using SIM.Core.Objects;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SIM.WinForm.Admin
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnAddNode_Click(object sender, EventArgs e)
        {
            var factory = new SimDynamicFactory();
            var paramaters = factory.GetConstructionArguments(typeof(DynamicNode));
            for (int i = 1; i < paramaters.Length + 1; i++)
            {
                groupBox1.Controls.Add(new Label()
                {
                    Location = new Point(5, 30 * i),
                    Text = paramaters[i - 1].Name,
                    Size = new Size(paramaters[i - 1].Name.Length * 8, 15)
                });

                groupBox1.Controls.Add(new TextBox()
                {
                    Location = new Point(80, 30 * i),
                    Name = paramaters[i - 1].Name
                });
            }

            var applyBtn = new Button()
            {
                Text = "Apply",
                Location = new Point(5, (paramaters.Length + 1) * 30)
            };
            
            groupBox1.Controls.Add(applyBtn);
            applyBtn.Click += ApplyBtn_Click;
        }

        private void ApplyBtn_Click(object sender, EventArgs e)
        {
            var arguments = groupBox1.Controls.OfType<TextBox>().Select(c => c.Text);
            var com = new NewDynamicNodeCommand(new AdminRepository(), arguments.ElementAt(0), arguments.ElementAt(1));
            com.Execute();
            UpdateNodes();
            groupBox1.Controls.Clear();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            UpdateNodes();
            UpdateRelations();
        }

        private void UpdateNodes()
        {
            lstNodes.DataSource = (new AdminRepository()).Nodes;
            lstNodes.DisplayMember = "Name";
        }

        private void UpdateRelations()
        {
            lstRelations.DataSource = (new AdminRepository()).Relations;
            lstRelations.DisplayMember = "Name";
        }

        private void UpdateProperties(DynamicNode dynamicNode)
        {
            lstProperties.DataSource = dynamicNode.Properties;
            lstProperties.DisplayMember = "Name";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var com = new LoadRepositoryAsJson(new AdminRepository(), "SIM.Aibel.JSB");
            com.Execute();
            UpdateNodes();
            UpdateRelations();
        }

        private void btnSaveAsJson_Click(object sender, EventArgs e)
        {
            var com = new RepositoryAsJsonCommand(new AdminRepository(), "SIM.Aibel.JSB");
            com.Execute();
        }

        private void btnAddRelation_Click(object sender, EventArgs e)
        {
            var factory = new SimDynamicFactory();
            var paramaters = factory.GetConstructionArguments(typeof(DynamicRelation));
            for (int i = 1; i < paramaters.Length + 1; i++)
            {
                groupBox1.Controls.Add(new Label()
                {
                    Location = new Point(5, 30 * i),
                    Text = paramaters[i - 1].Name,
                    Size = new Size(paramaters[i - 1].Name.Length * 8, 15)
                });

                groupBox1.Controls.Add(new TextBox()
                {
                    Location = new Point(80, 30 * i),
                    Name = paramaters[i - 1].Name
                });
            }

            var applyBtn = new Button()
            {
                Text = "Apply",
                Location = new Point(5, (paramaters.Length + 1) * 30)
            };

            groupBox1.Controls.Add(applyBtn);
            applyBtn.Click += ApplyBtn2_Click;
        }

        private void ApplyBtn2_Click(object sender, EventArgs e)
        {
            var arguments = groupBox1.Controls.OfType<TextBox>().Select(c => c.Text);
            var com = new NewDynamicRelationCommand(new AdminRepository(), 
                arguments.ElementAt(0), arguments.ElementAt(1), arguments.ElementAt(2).Split(' '), arguments.ElementAt(3).Split(' '));
            com.Execute();
            UpdateRelations();
            groupBox1.Controls.Clear();
        }

        private void lstNodes_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedNode = lstNodes.SelectedItem;
            if (selectedNode != null)
                UpdateProperties(selectedNode as DynamicNode);
        }

        private void btnAddProperty_Click(object sender, EventArgs e)
        {
            var factory = new SimDynamicFactory();
            var paramaters = factory.GetConstructionArguments(typeof(DynamicProperty));
            for (int i = 1; i < paramaters.Length + 1; i++)
            {
                groupBox1.Controls.Add(new Label()
                {
                    Location = new Point(5, 30 * i),
                    Text = paramaters[i - 1].Name,
                    Size = new Size(paramaters[i - 1].Name.Length * 8, 15)
                });

                groupBox1.Controls.Add(new TextBox()
                {
                    Location = new Point(80, 30 * i),
                    Name = paramaters[i - 1].Name
                });
            }

            var applyBtn = new Button()
            {
                Text = "Apply",
                Location = new Point(5, (paramaters.Length + 1) * 30)
            };

            groupBox1.Controls.Add(applyBtn);
            applyBtn.Click += ApplyBtn2_Click;
        }

        private void btnCsCode_Click(object sender, EventArgs e)
        {
            var com = new GenerateCSharpCodeCommand(new AdminRepository());
            com.Execute();
        }

        private void btnCompile_Click(object sender, EventArgs e)
        {
            var com = new CompileRepositoryCommand(new AdminRepository());
            com.Execute();
        }
    }
}
