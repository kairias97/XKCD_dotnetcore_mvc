using System;

namespace XKCDDemo.Util.Attributes
{
    [AttributeUsage(AttributeTargets.Interface)]
    public class ScopedAttribute : System.Attribute
    {
        public ScopeCoverage ScopeCoverage { get; set; }

    }
    public enum ScopeCoverage : int
    {
        Singleton,
        Scoped
    }
}
