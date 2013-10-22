using System;
using System.ComponentModel;
using LangExt;
using NUnit.Framework;

namespace ParameterizedDsl.NUnit
{
    [TestFixture]
    public abstract partial class Dsl
    {
        public ContextHolder<TContextData> Context<TContextData>(string description, Func<TContextData> context)
        {
            return new ContextHolder<TContextData>(description, context);
        }

        public readonly TestCaseArg _ = new TestCaseArg();

        protected abstract Seq<TestCaseData> GenSource();

        [EditorBrowsable(EditorBrowsableState.Never)]
        public Seq<TestCaseData> Source { get { return GenSource(); } }
    }
}
