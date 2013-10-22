using System;
using LangExt;
using NUnit.Framework;

namespace ParameterizedDsl.NUnit
{
    public class TestCaseDataSeq : Seq<TestCaseData>
    {
        readonly System.Collections.Generic.List<TestCaseData> cases = new System.Collections.Generic.List<TestCaseData>();

        public void Add<TContextData>(TestCaseGroup<TContextData> testCaseGroup)
        {
            testCaseGroup.Tests.Iter(test =>
                cases.Add(
                    new TestCaseData(
                        new Action(() => {
                            test.DoTest(() => testCaseGroup.Data());
                        })
                    ).SetDescription(string.Join(", ", testCaseGroup.Descriptions))
                     .SetName(string.Join(", ", testCaseGroup.Descriptions) + test.Args.ToString("(", ", ", ")"))
                )
            );
        }

        public System.Collections.Generic.IEnumerator<TestCaseData> GetEnumerator()
        {
            return this.cases.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
