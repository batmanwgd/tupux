using System;
using System.ComponentModel;
using System.Collections;
using System.ComponentModel.Design;
using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace CustomValidation
{

    #region BaseCompareValidator
    public abstract class BaseCompareValidator : BaseValidator
    {

        private ValidationDataType _type = ValidationDataType.String;
        private string[] _typeTable = new string[5] {"System.Decimal", 
		                                                             "System.DateTime",
		                                                             "System.Double",
		                                                             "System.Int32",
		                                                             "System.String"};

        [Category("Behavior")]
        [Description("Sets or returns the data type that specifies how to interpret the values being compared.")]
        [DefaultValue(ValidationDataType.String)]
        public ValidationDataType Type
        {
            get { return _type; }
            set { _type = value; }
        }

        protected TypeConverter TypeConverter
        {
            get { return TypeDescriptor.GetConverter(System.Type.GetType(_typeTable[(int)_type])); }
        }

        protected bool CanConvert(string value)
        {
            try
            {
                TypeConverter _converter = TypeDescriptor.GetConverter(System.Type.GetType(_typeTable[(int)_type]));
                _converter.ConvertFrom(value);
                return true;
            }
            catch { return false; }
        }

        protected string Format(string value)
        {
            // If currency
            if (_type == ValidationDataType.Currency)
            {
                // Convert to decimal format ie remove currency formatting characters
                return Regex.Replace(value, "[$ .]", "");
            }
            return value;
        }
    }
    #endregion

}
