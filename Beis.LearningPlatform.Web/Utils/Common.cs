using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Beis.LearningPlatform.Web.Utils
{
    /// <summary>
    /// Checks the attribute in relation to the value of the property specified in propertyName
    /// </summary>
    public class RequiredIfNotNullAttribute : RequiredAttribute
    {
        private String PropertyName { get; set; }
        private String ErrorMessageOut { get; set; }

        public RequiredIfNotNullAttribute(String propertyName, string errorMessage = "Cannot be empty.")
        {
            PropertyName = propertyName;
            ErrorMessageOut = errorMessage;
        }

        protected override ValidationResult IsValid(object value, ValidationContext context)
        {
            Object instance = context.ObjectInstance;
            Type type = instance.GetType();
            Object proprtyvalue = type.GetProperty(PropertyName).GetValue(instance, null);
            if (proprtyvalue != null)
            {
                if (!(bool)proprtyvalue && string.IsNullOrWhiteSpace((string)value))
                {
                    ValidationResult result = base.IsValid(value, context);
                    result.ErrorMessage = context.MemberName + ErrorMessageOut;
                    return result;
                }
            }
            return ValidationResult.Success;
        }
    }

    public class RequiredIfBooleanValueAttribute : RequiredAttribute
    {
        private String PropertyName { get; set; }
        private String ErrorMessageOut { get; set; }
        private bool TestBoolean { get; set; }

        public RequiredIfBooleanValueAttribute(String dependentPropertyName, bool booleanValueToCheck, string errorMessage = "Cannot be empty.")
        {
            PropertyName = dependentPropertyName;
            ErrorMessageOut = errorMessage;
            TestBoolean = booleanValueToCheck;
        }

        protected override ValidationResult IsValid(object value, ValidationContext context)
        {
            Object instance = context.ObjectInstance;
            Type type = instance.GetType();
            Object proprtyvalue = type.GetProperty(PropertyName).GetValue(instance, null);
            if ((bool)proprtyvalue)
            {
                if ((bool)proprtyvalue != TestBoolean)
                {

                    ValidationResult result = base.IsValid(value, context);
                    result.ErrorMessage = context.MemberName + ErrorMessageOut;
                    return result;

                }
            }
            return ValidationResult.Success;
        }
    }


    public class RequiredIfControlTypeOfAttribute : RequiredAttribute
    {
        private String PropertyName { get; set; }
        private String ErrorPropertyName { get; set; }
        private String ErrorMessageOut { get; set; }
        private string TestType { get; set; }

        public RequiredIfControlTypeOfAttribute(String dependentPropertyName, string objType, string ErrorMessagePropertyName, string errorMessage = "Cannot be empty.")
        {
            PropertyName = dependentPropertyName;
            TestType = objType;
            ErrorPropertyName = ErrorMessagePropertyName;
            ErrorMessageOut = errorMessage;
        }

        protected override ValidationResult IsValid(object value, ValidationContext context)
        {
            Object instance = context.ObjectInstance;
            Type type = instance.GetType();
            Object propertyvalue = type.GetProperty(PropertyName).GetValue(instance, null);
            Object errorpropertyvalue = type.GetProperty(ErrorPropertyName).GetValue(instance, null);


            if (propertyvalue.ToString() == TestType && string.IsNullOrWhiteSpace((string)value))
            {
                ValidationResult result = base.IsValid(value, context);
                result.ErrorMessage = /*(context.MemberName + )*/ErrorMessageOut;
                errorpropertyvalue = string.IsNullOrWhiteSpace((string)errorpropertyvalue) ? result.ErrorMessage : errorpropertyvalue.ToString() + "\r" + result.ErrorMessage;

                if (!string.IsNullOrWhiteSpace(ErrorPropertyName))
                {
                    type.GetProperty(ErrorPropertyName).SetValue(instance, errorpropertyvalue);
                }
                return result;

            }

            return ValidationResult.Success;
        }
    }

    public static class CamelCaseConverter
        {
            public static string Delimiter(string input, string delimeter)
            {
                if(string.IsNullOrWhiteSpace(input))
                {
                    return input;
                }
                string strRegex = @"(?<=[a-z])([A-Z])|(?<=[A-Z])([A-Z][a-z])";
                Regex myRegex = new(strRegex, RegexOptions.None);

                var ret = myRegex.Replace(input, delimeter + "$1").ToLower();
            return ret;
            }
        }

    public static class ListJoinFormatter
    {
        public static string ReplaceLastCommaWith(string input, string joiningWord)
        {
            var retval = 
            string.IsNullOrWhiteSpace(input) ? input :
                (input.LastIndexOf(",") > input.IndexOf(",")) ? input.Substring(0, input.LastIndexOf(",")) + " " + joiningWord + " " + input.Substring(input.LastIndexOf(",") + 1) 
                : input.Replace(",", joiningWord);
            return retval;
        }

        public static string ReplaceLastCharacterWith(string input, string searchword, string joiningWord)
        {
            var retval =
            string.IsNullOrWhiteSpace(input) ? input :
        (input.LastIndexOf(searchword) > input.IndexOf(searchword)) ? input.Substring(0, input.LastIndexOf(searchword)) + " " + joiningWord + " " 
        + input.Substring(input.LastIndexOf(searchword) + 1) : input.Replace(searchword, " " + joiningWord);
            return retval;
        }
    }

    public static class ComparisonToolFieldValidator
    {
        public static string ParseStringField(string fieldValue)
        {
            return string.IsNullOrWhiteSpace(fieldValue) ? "No Value in Table" : fieldValue;
        }
    }
}