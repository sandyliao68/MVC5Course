using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace MVC5Course.Models
{
    class 此欄位必須至少出現一個空白字元Attribute : DataTypeAttribute
    {
        public 此欄位必須至少出現一個空白字元Attribute() : base(DataType.Text)
        {

        }
        public override bool IsValid(object value)
        {
            var str = (string)value;
            return str.Contains(" ");
        }
    }
}
