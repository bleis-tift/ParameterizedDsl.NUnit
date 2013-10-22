using System;

namespace ParameterizedDsl.NUnit
{
    public sealed class TestCaseArg
    {
        internal readonly bool IsPlaceholder;
        internal readonly object Value;
        TestCaseArg(bool isPlaceholder, object value)
        {
            this.IsPlaceholder = isPlaceholder;
            this.Value = value;
        }

        internal TestCaseArg() : this(true, null) { }
        internal TestCaseArg(object value) : this(false, value) { }
    }
}
