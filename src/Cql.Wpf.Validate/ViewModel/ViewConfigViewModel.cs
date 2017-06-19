using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cql.Wpf.Validate.Config;

namespace Cql.Wpf.Validate.ViewModel
{
	/// <summary>
	/// Wraps a view configuration. This is the object you will normally use in your view models.
	/// </summary>
    public class ViewConfigViewModel
    {
        public ViewConfigViewModel(ViewConfig config)
        {
            if (config != null)
            {
                Name = config.Name;
                Fields = config.Fields.Select(f => new FieldConfigViewModel(f)).ToList();
            }
        }

        public string Name { get; set; }
        public List<FieldConfigViewModel> Fields { get; set; } = new List<FieldConfigViewModel>();

        public void AddCustomValidator(string fieldName, Func<object, bool> validate, string messageKey)
        {
            var field = Fields.FirstOrDefault(f => f.Name == fieldName);
            field?.FieldConfig.AddCustomValidator(validate, messageKey);
        }

        public void AddCustomAsyncValidator(string fieldName, Func<object, Task<bool>> validate, string messageKey)
        {
            var field = Fields.FirstOrDefault(f => f.Name == fieldName);
            field?.FieldConfig.AddCustomAsyncValidator(validate, messageKey);
        }
    }
}