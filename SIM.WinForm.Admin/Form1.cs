using SIM.CodeEngine.Commands;
using SIM.CodeEngine.Dynamic;
using SIM.CodeEngine.Factory;
using SIM.Core.Commands;
using SIM.Core.Objects;
using SIM.DataBase;
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
                grbDetails.Controls.Add(new Label()
                {
                    Location = new Point(5, 30 * i),
                    Text = paramaters[i - 1].Name,
                    Size = new Size(70, 15)
                });

                if (paramaters[i - 1].ParameterType == typeof(string))
                {
                    grbDetails.Controls.Add(new TextBox()
                    {
                        Location = new Point(80, 30 * i),
                        Name = paramaters[i - 1].Name
                    });
                }
                else if (paramaters[i - 1].ParameterType == typeof(bool))
                {
                    grbDetails.Controls.Add(new CheckBox()
                    {
                        Location = new Point(80, 30 * i),
                        Name = paramaters[i - 1].Name
                    });
                }
            }

            var applyBtn = new Button()
            {
                Text = "Apply",
                Location = new Point(5, (paramaters.Length + 1) * 30)
            };
            
            grbDetails.Controls.Add(applyBtn);
            applyBtn.Click += ApplyBtn_Click;
        }

        private void ApplyBtn_Click(object sender, EventArgs e)
        {
            var arguments = grbDetails.Controls.OfType<TextBox>().Select(c => c.Text);
            var visible = grbDetails.Controls.OfType<CheckBox>().Select(c => c.Checked);
            var com = new NewDynamicNodeCommand(new AdminRepository(), arguments.ElementAt(0), arguments.ElementAt(1), visible.ElementAt(0));
            com.Execute();
            UpdateNodes();
            grbDetails.Controls.Clear();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            UpdateNodes();
            UpdateRelations();
        }

        private void UpdateNodes()
        {
            lstNodes.DataSource = null;
            lstNodes.DataSource = (new AdminRepository()).Nodes;
            lstNodes.DisplayMember = "Name";
        }

        private void UpdateRelations()
        {
            lstRelations.DataSource = null;
            lstRelations.DataSource = (new AdminRepository()).Relations;
            lstRelations.DisplayMember = "Name";
        }

        private void UpdateProperties(DynamicNode dynamicNode)
        {
            lstProperties.DataSource = null;
            lstProperties.DataSource = dynamicNode.Properties;
            lstProperties.DisplayMember = "Name";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if ((new AdminRepository()).GetAll() != null)
                ClearAll();

            var ns = lblNamespace.Text.Trim();
            var com = new LoadRepositoryAsJson(new AdminRepository(), ns);
            com.Execute();
            UpdateNodes();
            UpdateRelations();
        }

        private void ClearAll()
        {
            lstNodes.DataSource = null;
            lstRelations.DataSource = null;
            lstProperties.DataSource = null;
        }

        private void btnSaveAsJson_Click(object sender, EventArgs e)
        {
            var ns = lblNamespace.Text.Trim();
            var com = new RepositoryAsJsonCommand(new AdminRepository(), ns);
            com.Execute();
        }

        private void btnAddRelation_Click(object sender, EventArgs e)
        {
            var factory = new SimDynamicFactory();
            var paramaters = factory.GetConstructionArguments(typeof(DynamicRelation));
            for (int i = 1; i < paramaters.Length + 1; i++)
            {
                grbDetails.Controls.Add(new Label()
                {
                    Location = new Point(5, 30 * i),
                    Text = paramaters[i - 1].Name,
                    Size = new Size(70 , 15),
                    BackColor = Color.Aqua
                });

                grbDetails.Controls.Add(new TextBox()
                {
                    Location = new Point(80, 30 * i),
                    Name = paramaters[i - 1].Name,
                    Text = paramaters[i - 1].Name == "nameSpace" ? lblNamespace.Text : string.Empty
                });
            }

            var applyBtn = new Button()
            {
                Text = "Apply",
                Location = new Point(5, (paramaters.Length + 1) * 30)
            };

            grbDetails.Controls.Add(applyBtn);
            applyBtn.Click += ApplyBtn2_Click;
        }

        private void ApplyBtn2_Click(object sender, EventArgs e)
        {
            var arguments = grbDetails.Controls.OfType<TextBox>().Select(c => c.Text);
            var com = new NewDynamicRelationCommand(new AdminRepository(), 
                arguments.ElementAt(0), arguments.ElementAt(1), arguments.ElementAt(2).Split(' '), arguments.ElementAt(3).Split(' '));
            com.Execute();
            UpdateRelations();
            grbDetails.Controls.Clear();
        }

        private void lstNodes_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedNode = lstNodes.SelectedItem;
            if (selectedNode != null)
                UpdateProperties(selectedNode as DynamicNode);
        }

        private void btnAddProperty_Click(object sender, EventArgs e)
        {
            //var factory = new SimDynamicFactory();
            //var paramaters = factory.GetConstructionArguments(typeof(DynamicProperty));
            var paramaters = typeof(NewDynamicPropertyCommand).GetConstructors().FirstOrDefault().GetParameters();
            for (int i = 1; i < paramaters.Length + 1; i++)
            {
                if (paramaters[i - 1].ParameterType == typeof(ISimRepository) || 
                    paramaters[i - 1].ParameterType.GetInterfaces().Contains(typeof(ISimRepository))) continue; 

                grbDetails.Controls.Add(new Label()
                {
                    Location = new Point(5, 30 * i),
                    Text = paramaters[i - 1].Name,
                    Size = new Size(70, 15)
                });

                if (paramaters[i - 1].ParameterType == typeof(string))
                {
                    grbDetails.Controls.Add(new TextBox()
                    {
                        Location = new Point(80, 30 * i),
                        Name = paramaters[i - 1].Name,
                        Text = paramaters[i - 1].Name == "nameSpace" ? lblNamespace.Text :
                               paramaters[i - 1].Name == "ownerObject"? (lstNodes.SelectedItem as DynamicNode).Name : string.Empty
                    });
                }
                else if (paramaters[i - 1].ParameterType == typeof(bool))
                {
                    grbDetails.Controls.Add(new CheckBox()
                    {
                        Location = new Point(80, 30 * i),
                        Name = paramaters[i - 1].Name
                    });
                }
            }

            var applyBtn = new Button()
            {
                Text = "Apply",
                Location = new Point(5, (paramaters.Length + 1) * 30)
            };

            grbDetails.Controls.Add(applyBtn);
            applyBtn.Click += ApplyBtn3_Click;
        }

        private void ApplyBtn3_Click(object sender, EventArgs e)
        {
            var arguments = grbDetails.Controls.OfType<TextBox>().Select(c => c.Text);
            var boolArgs = grbDetails.Controls.OfType<CheckBox>().Select(c => c.Checked);
            var com = new NewDynamicPropertyCommand(new AdminRepository(),
                arguments.ElementAt(0), arguments.ElementAt(1), arguments.ElementAt(2), arguments.ElementAt(3), boolArgs.ElementAt(0), boolArgs.ElementAt(1), boolArgs.ElementAt(2));
            com.Execute();
            UpdateProperties(lstNodes.SelectedItem as DynamicNode);
            grbDetails.Controls.Clear();
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

        private void btnRemoveNode_Click(object sender, EventArgs e)
        {
            RemoveSelected(lstNodes);
            UpdateNodes();
        }

        private void RemoveSelected(ListBox listBox)
        {
            var item = listBox.SelectedItem;
            var repos = new AdminRepository();
            repos.Remove(item as ISimObject);
        }

        private void btnRemoveRelation_Click(object sender, EventArgs e)
        {
            RemoveSelected(lstRelations);
            UpdateRelations();
        }

        private void btnRemoveProperty_Click(object sender, EventArgs e)
        {
            var repos = new AdminRepository();
            var node = repos.Get(c => c is DynamicNode && (c as DynamicNode).Name == (lstNodes.SelectedItem as DynamicNode).Name) as DynamicNode;
            node.Properties.Remove(lstProperties.SelectedItem as DynamicProperty);
            UpdateProperties(lstNodes.SelectedItem as DynamicNode);
        }
    }
}
