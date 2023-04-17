using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace _0_Framework.Application
{
    public class FileExtensionAttribute: ValidationAttribute,IClientModelValidator  
    {
        private readonly string[] _extensions;

        public FileExtensionAttribute(string[] extensions)
        {
            _extensions = extensions;
        }

        public override bool IsValid(object value)
        {
            var val = value as IFormFile;
            if (val == null) return true;
            string fileExtension = Path.GetExtension(val.FileName);
            return _extensions.Contains(fileExtension);
        }

        public void AddValidation(ClientModelValidationContext context)
        {
            var exist = context.Attributes.Keys.Contains("data-val");
            if (!exist)
            {
                context.Attributes.Add("data-val", "true");
            }
            context.Attributes.Add("data-val-extensions", ErrorMessage);
        }
    }

}