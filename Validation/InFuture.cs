using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TryValidation.Validation
{
    public class InFuture:ValidationAttribute
    {
        //public override bool IsValid(object value)
        //{
        //    //return base.IsValid(value);
        //}
    }
}