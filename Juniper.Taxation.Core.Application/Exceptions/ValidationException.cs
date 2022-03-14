using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentValidation.Results;

namespace Juniper.Taxation.Core.Application.Exceptions
{
    public class ValidationException : ApplicationException
    {
        public ValidationException() : base("One or more validation failures have occurred.")
        {
            Errors = new List<string>();
        }
        public List<string> Errors { get; }
        public ValidationException(ValidationResult result)
            : this()
        {
            Errors = result.Errors.Select(x => x.ErrorMessage).ToList();
        }

    }
}
