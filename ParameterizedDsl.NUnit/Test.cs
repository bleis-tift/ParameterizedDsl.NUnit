using System;
using LangExt;
using NUnit.Framework;

namespace ParameterizedDsl.NUnit
{
    public class TestTemplate<TContext>
    {
        internal readonly Tester<TContext> Tester;
        internal readonly System.Collections.Generic.List<TestCaseArg> Args;

        internal TestTemplate(Tester<TContext> tester, System.Collections.Generic.List<TestCaseArg> args)
        {
            this.Tester = tester;
            this.Args = args;
        }
    }

    public class Test<TContext>
    {
        readonly Tester<TContext> tester;
        internal readonly Seq<object> Args;

        internal Test(Tester<TContext> tester, Seq<TestCaseArg> args)
        {
            if (args.Exists(a => a.IsPlaceholder))
                throw new ArgumentException();
            this.tester = tester;
            this.Args = args.Map(a => a.Value);
        }

        public void DoTest(Func<TContext> context) { this.tester.DoTest(context, this.Args); }
    }

    public abstract class Tester<TContext>
    {
        public abstract void DoTest(Func<TContext> context, Seq<object> args);
    }

    internal sealed class EqTester<TContext> : Tester<TContext>
    {
        readonly object expected;
        readonly Func<TContext, object[], object> body;

        public EqTester(object expected, Func<TContext, object[], object> body)
        {
            this.expected = expected;
            this.body = body;
        }

        public override void DoTest(Func<TContext> context, Seq<object> args)
        {
            Assert.That(this.body(context(), args.ToArray()), Is.EqualTo(this.expected));
        }
    }

    public static class Tester
    {
        public static Tester<TContext> EqTester<TContext>(object expected, Func<TContext, object[], object> body)
        {
            return new EqTester<TContext>(expected, body);
        }
    }
}
