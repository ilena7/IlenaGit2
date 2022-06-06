using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Artezanias2.Infrastructure
{
    public class FileExtensionAttribute: ValidationAttribute

    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            //var context =(Artezanias2Context)validationContextGetService(typeof(Artezanias2Context));

            var file = value as IFormFile;
            if (file !=null)
            {
                var extension = Path.GetExtension(file.FileName);
                string[] extensions = { "jpg", "png","jpeg" };
                bool result = extensions.Any(x => extension.EndsWith(x));

                if (!result)
                {
                    return new ValidationResult(GetErrorMassage());
                }
                }
                return ValidationResult.Success;
            }
           
        private string GetErrorMassage()
        {
            return "Las extensiones permitidas son jpg y png";
        }


    }
}
