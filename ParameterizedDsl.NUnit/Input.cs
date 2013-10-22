using System;
using LangExt;

namespace ParameterizedDsl.NUnit
{
    using TestCaseArgList = System.Collections.Generic.List<TestCaseArg>;

    public class Input : System.Collections.Generic.IEnumerable<TestCaseArg>
    {
        readonly TestCaseArgList args = new TestCaseArgList();

        public void Add<T>(T value) { this.args.Add(new TestCaseArg(value)); }

        public void Add(TestCaseArg arg) { this.args.Add(arg); }

        public TestTemplate<TContext> Test<TContext>(Tester<TContext> tester) { return new TestTemplate<TContext>(tester, this.args); }

        public System.Collections.Generic.IEnumerator<TestCaseArg> GetEnumerator()
        {
            return this.args.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.args.GetEnumerator();
        }
    }
}
