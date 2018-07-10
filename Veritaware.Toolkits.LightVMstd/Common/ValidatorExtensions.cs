﻿using System.Collections.Generic;
using FluentValidation;
using FluentValidation.Internal;
using FluentValidation.Results;

namespace Veritaware.Toolkits.LightVMstd.Common
{
    public static class ValidatorExtensions
    {
        public static ValidationResult Validate(
            this IValidator validator,
            object instance,
            string propertyName)
        {
            var properties = new List<string> { propertyName };
            var context = new ValidationContext(
                instance,
                new PropertyChain(),
                new MemberNameValidatorSelector(properties));

            var result = validator.Validate(context);

            return result;
        }
    }
}
