using System;
using System.Collections;
using System.ComponentModel;
using FluentValidation;
using FluentValidation.Attributes;
using Veritaware.Toolkits.LightVM.Net.Common;

namespace Veritaware.Toolkits.LightVM.Net
{
    /// <inheritdoc cref="NotifyingObject" />
    /// <summary>
    /// For validation purposes FluentValidation library is required.
    /// </summary>
    public class ModelBase : NotifyingObject , INotifyDataErrorInfo
    {
        private IValidator Validator 
            => new AttributedValidatorFactory().GetValidator(GetType());

        /// <inheritdoc />
        public IEnumerable GetErrors(string propertyName) 
            => Validator.Validate(this, propertyName).Errors;

        public bool HasErrors =>  !(Validator?.Validate(this).IsValid ?? true);
#pragma warning disable 67
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;
#pragma warning restore 67
    }
}
