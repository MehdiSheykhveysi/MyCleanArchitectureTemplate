using System;

namespace App.ApplicationService.Shaared.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class ServiceMark : Attribute
    {
    }
}
