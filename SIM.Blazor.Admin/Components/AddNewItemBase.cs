using Microsoft.AspNetCore.Components;
using SIM.Blazor.Admin.Data;
using SIM.CodeEngine.Commands;
using SIM.CodeEngine.Dynamic;
using SIM.CodeEngine.Factory;
using SIM.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace SIM.Blazor.Admin.Components
{
    public class AddNewItemBase : ComponentBase
    {
        [Parameter]
        public NewItemTypeEnum Type { get; set; }

        [Inject]
        public ISimRepository repository { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public IMediator Mediator { get; set; }

        [Inject]
        public SimDynamicFactory SimFactory { get; set; }
        protected IDictionary<ParameterInfo, ParameterValue> Arguments { get; set; }
        private Type CommandType { get; set; }
        protected bool isSaved = false;

        protected override Task OnInitializedAsync()
        {
            // Get required arguments
            Arguments = GenerateArgumentDictionary();

            return base.OnInitializedAsync();
        }

        private IDictionary<ParameterInfo, ParameterValue> GenerateArgumentDictionary()
        {
            Dictionary<ParameterInfo, ParameterValue> result = new Dictionary<ParameterInfo, ParameterValue>();
            var args = GetArguments();
            foreach (var arg in args)
            {
                ParameterValue value = new ParameterValue();
                if (arg.ParameterType == typeof(ISimRepository)) value.ObjectValue = repository;
                result.Add(arg, value);
            }
            return result;
        }

        private IEnumerable<ParameterInfo> GetArguments()
        {
            switch (Type)
            {
                case NewItemTypeEnum.Node:
                    CommandType = typeof(NewDynamicNodeCommand);
                    return SimFactory.GetConstructionArguments(CommandType);
                case NewItemTypeEnum.Relation:
                    CommandType = typeof(NewDynamicRelationCommand);
                    return SimFactory.GetConstructionArguments(CommandType);
                case NewItemTypeEnum.NodeProperty:
                case NewItemTypeEnum.RelationProperty:
                    CommandType = typeof(NewDynamicPropertyCommand);
                    return SimFactory.GetConstructionArguments(CommandType);
                default:
                    return null;
            }
        }

        protected void Submit()
        {
            dynamic cmd = Activator.CreateInstance(CommandType,
                Arguments.Values.Select(a => a.BooleanValue != null ? a.BooleanValue :
                                             a.StringArrayValue != null ? a.StringArrayValue :
                                             a.StringValue != null ? a.StringValue :
                                             a.ObjectValue != null ? a.ObjectValue : null).ToArray());
            cmd.Execute();

            // update json file
            var com = new RepositoryAsJsonCommand(repository, $"SIM.Aibel.{Mediator.Context}");
            com.Execute();
            isSaved = true;
        }
    }
}
