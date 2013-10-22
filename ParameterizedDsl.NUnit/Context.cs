using System;
using LangExt;
using LangExt.Unsafe;

namespace ParameterizedDsl.NUnit
{
    public sealed class ContextHolder<TContextData>
    {
        internal readonly string[] Descriptions;
        internal readonly Func<TContextData> Data;

        internal ContextHolder(string[] descriptions, Func<TContextData> data)
        {
            this.Descriptions = descriptions;
            this.Data = data;
        }

        internal ContextHolder(string description, Func<TContextData> data) : this(new[] { description }, data) { }

        public TestCaseGroup<TContextData> Cases(params TestTemplate<TContextData>[] templates)
        {
            var tests = new Test<TContextData>[templates.Length];
            var preArgs = templates[0].Args.ToSeq();
            tests[0] = new Test<TContextData>(templates[0].Tester, preArgs);
            for (int i = 1; i < templates.Length; i++)
            {
                var args = preArgs.Zip(templates[i].Args.ToSeq()).Map((pre, crnt) => crnt.IsPlaceholder ? pre : crnt);
                tests[i] = new Test<TContextData>(templates[i].Tester, args);
                preArgs = args;
            }
            return new TestCaseGroup<TContextData>(this.Descriptions, this.Data, tests);
        }
    }
}
