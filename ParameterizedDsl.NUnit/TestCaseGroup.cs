using System;

namespace ParameterizedDsl.NUnit
{
    public sealed class TestCaseGroup<TContextData>
    {
        internal readonly string[] Descriptions;
        internal readonly Func<TContextData> Data;
        internal readonly Test<TContextData>[] Tests;

        internal TestCaseGroup(string[] descriptions, Func<TContextData> data, Test<TContextData>[] tests)
        {
            this.Descriptions = descriptions;
            this.Data = data;
            this.Tests = tests;
        }
    }
}
