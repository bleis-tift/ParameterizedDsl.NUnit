using System;
using NUnit.Framework;

namespace ParameterizedDsl.NUnit
{
    public sealed class TestEntryPointAttribute : TestCaseSourceAttribute
    {
        public TestEntryPointAttribute() : base("Source") { }
    }
}
