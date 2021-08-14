using Core.Entities;
using Core.Utilities.Results;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.CrossCuttingConcerns.Validation
{
    public static class ValidationTool
    {
        public static IResult IsValid(IValidator validator, object entity)
        {
            var context = new ValidationContext<object>(entity);
            var result = validator.Validate(context);
            if (!result.IsValid)
            {
                return new ErrorResult(result.Errors[0].ToString());
            }
            return new SuccessResult();
        }

        public static void Validate(IValidator validator, object entity)
        {
            //var result = validator.Validate(entity); eski
            var context = new ValidationContext<object>(entity);
            var result = validator.Validate(context);
            if (!result.IsValid)
            {
                throw new ValidationException(result.Errors);
            }
        }
    }
}
