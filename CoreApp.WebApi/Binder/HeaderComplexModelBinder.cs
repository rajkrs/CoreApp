using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CoreApp.WebApi
{
    public class HeaderComplexModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
            {
                throw new ArgumentNullException(nameof(bindingContext));
            }

            if (bindingContext == null)
            {
                throw new ArgumentNullException(nameof(bindingContext));
            }

            var headerModel = bindingContext.HttpContext.Request.Headers["RequestModel"].FirstOrDefault();
            var modelType = bindingContext.ModelMetadata.ModelType;

            bindingContext.Model = JsonConvert.DeserializeObject(headerModel, modelType);
            bindingContext.Result = ModelBindingResult.Success(bindingContext.Model);

            return Task.CompletedTask;
        }
    }

}
