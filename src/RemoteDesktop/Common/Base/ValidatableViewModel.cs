using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

using ValidationResult = System.ComponentModel.DataAnnotations.ValidationResult;

namespace RemoteDesktop.Common.Base;

/// <summary>
/// Extends BaseViewModel with support for data validation using DataAnnotations.
/// </summary>
internal abstract class ValidatableViewModel : BaseViewModel, INotifyDataErrorInfo
{
    private readonly IDictionary<string, IList<string>> _errors = new Dictionary<string, IList<string>>();

    /// <summary>
    /// Event raised when the validation errors for a property have changed.
    /// </summary>
    public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

    /// <summary>
    /// Indicates whether the object has any validation errors.
    /// </summary>
    public bool HasErrors => _errors.Any();

    /// <summary>
    /// Gets the validation errors for a specific property.
    /// </summary>
    public IEnumerable GetErrors(string propertyName)
    {
        if (string.IsNullOrEmpty(propertyName))
        {
            return _errors.SelectMany(e => e.Value);
        }

        return _errors.TryGetValue(propertyName, out var errors) ? errors : Enumerable.Empty<string>();
    }

    /// <summary>
    /// Raises PropertyChanged and validates the changed property.
    /// </summary>
    protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        base.OnPropertyChanged(propertyName);
        ValidateProperty(propertyName);
    }

    /// <summary>
    /// Validates a single property using DataAnnotation attributes.
    /// Updates the internal error collection and raises ErrorsChanged if necessary.
    /// </summary>
    protected virtual void ValidateProperty(string propertyName)
    {
        if (string.IsNullOrEmpty(propertyName))
        {
            return;
        }

        var propertyInfo = GetType().GetProperty(propertyName);
        if (propertyInfo == null)
        {
            return;
        }

        var value = propertyInfo.GetValue(this);
        var context = new ValidationContext(this)
        {
            MemberName = propertyName
        };

        var results = new List<ValidationResult>();
        Validator.TryValidateProperty(value, context, results);

        if (_errors.ContainsKey(propertyName))
        {
            _errors.Remove(propertyName);
        }

        if (results.Any())
        {
            _errors[propertyName] = [.. results.Select(r => r.ErrorMessage)];
        }

        ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
    }

    /// <summary>
    /// Validates all public properties of the object.
    /// Useful when submitting a form or performing global checks.
    /// </summary>
    public void ValidateAllProperties()
    {
        foreach (var property in GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance))
        {
            ValidateProperty(property.Name);
        }
    }
}