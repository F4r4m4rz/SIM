using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;
using SIM.CodeEngine.Commands;
using SIM.Core.DataBase;
using SIM.Core.Objects;
using SIM.DataBase;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SIM.Blazor.Components
{
    public class SimListPropertyRepresantationBase : ComponentBase
    {
        [Parameter]
        public ISimObject Owner { get; set; }

        [Parameter]
        public PropertyInfo BackingProprty { get; set; }

        [Parameter]
        public bool Editable { get; set; }

        [Parameter]
        public EventCallback OnAddPromptCallback { get; set; }

        [Inject]
        public ISimRepository Repository { get; set; }

        [Inject]
        public IMediator Mediator { get; set; }

        public DataTable Table { get; private set; }

        private DataTable GetPropertyValueAsTable()
        {
            DataTable table = new DataTable(BackingProprty.Name);
            
            return table;
        }

        private void GenerateHeader()
        {
            // Get properties on backing property value
            Table.Columns.Add("Instance", typeof(object));
            BackingProprty.PropertyType
                .GetGenericArguments()[0]
                .GetProperties()
                .OrderBy(a => (a.GetCustomAttribute(typeof(JsonPropertyAttribute), true) as JsonPropertyAttribute)?.Order)
                .ToList()
                .ForEach(a =>
                {
                    Table.Columns.Add(a.Name, typeof(string));
                });
        }

        protected override Task OnInitializedAsync()
        {
            // Create table
            Table = GetPropertyValueAsTable();

            // Populate headers
            GenerateHeader();

            // Populate table
            PopulateTable();

            return base.OnInitializedAsync();
        }

        private void PopulateTable()
        {
            var backinPropValue = (BackingProprty.GetValue(Owner) as IEnumerable)?.GetEnumerator();

            if (backinPropValue == null) return;

            while (backinPropValue.MoveNext())
            {
                var current = backinPropValue.Current;
                var row = Table.NewRow();
                row[Table.Columns[0]] = current;

                for (int i = 1; i < Table.Columns.Count; i++)
                {
                    var value = current.GetType().GetProperty(Table.Columns[i].ColumnName).GetValue(current);
                    string valueStr = string.Empty;
                    if (value.GetType().GetInterface("IEnumerable") != null && !value.GetType().Equals(typeof(string)))
                    {
                        var enumerator = (value as IEnumerable).GetEnumerator();
                        while (enumerator.MoveNext())
                        {
                            var c = enumerator.Current;
                            valueStr = string.Format("{0}{1} ", valueStr, c.ToString());
                        }
                    }
                    else
                    {
                        valueStr = value.ToString();
                    }
                    row[Table.Columns[i]] = valueStr;
                }

                Table.Rows.Add(row);
            }
        }

        protected void DeleteRow(DataRow row)
        {
            var instance = row.ItemArray[0];
            var backingPropVal = BackingProprty.GetValue(Owner);
            backingPropVal.GetType().GetMethod("Remove", new[] { instance.GetType() }).Invoke(backingPropVal, new[] { instance });

            // Save json
            var cmd = new RepositoryAsJsonCommand(Repository, $"SIM.Aibel.{Mediator.Context}");
            cmd.Execute();
        }
    }
}
