using System;
using LangExt;
using NUnit.Framework;
using ParameterizedDsl.NUnit;

namespace ParameterizedDsl.NUnit.Tests
{
    public static class StringExtension
    {
        public static string SubstrAfter(this string self, string separator)
        {
            if (self == null) return null;
            if (self == "" || separator == null) return "";
            if (separator == "") return self;

            var start = self.IndexOf(separator);
            if (start == -1)
                return "";
            return self.Substring(start + separator.Length);
        }
    }

    public class TestTargetTest : Dsl
    {
        [TestEntryPoint]
        public void Test(Action tester) { tester(); }

        protected override Seq<TestCaseData> GenSource()
        {
            const string NullStr = null;

            var normal = Context("self is abcdefg", () => "abcdefg");
            var receiverIsNull = Context("self is null", () => NullStr);
            var receiverIsEmpty = Context("self is empty", () => "");
            var separatorIsNull = Context("separator is null", () => NullStr);
            var separatorIsEmpty = Context("separator is empty", () => "");

            Func<string, string, string> fixedRec = StringExtension.SubstrAfter;
            Func<string, string, string> fixedSep = (sep, self) => self.SubstrAfter(sep);
            return new TestCaseDataSeq
            {
                normal.Cases(new[] {
                    new Input { fixedRec, NullStr }.Test(SubstrAfter(expected: "")),
                    new Input { _,        ""      }.Test(SubstrAfter(expected: "abcdefg")),
                    new Input { _,        "def"   }.Test(SubstrAfter(expected: "g")),
                    new Input { _,        "defn"  }.Test(SubstrAfter(expected: "")),
                }),
                receiverIsNull.Cases(new[] {
                    new Input { fixedRec, NullStr }.Test(SubstrAfter(expected: null)),
                    new Input { _,        ""      }.Test(SubstrAfter(expected: null)),
                    new Input { _,        "def"   }.Test(SubstrAfter(expected: null)),
                }),
                receiverIsEmpty.Cases(new[] {
                    new Input { fixedRec, NullStr }.Test(SubstrAfter(expected: "")),
                    new Input { _,        ""      }.Test(SubstrAfter(expected: "")),
                    new Input { _,        "def"   }.Test(SubstrAfter(expected: "")),
                }),
                separatorIsNull.Cases(new[] {
                    new Input { fixedSep, NullStr   }.Test(SubstrAfter(expected: null)),
                    new Input { _,        ""        }.Test(SubstrAfter(expected: "")),
                    new Input { _,        "abcdefg" }.Test(SubstrAfter(expected: "")),
                }),
                separatorIsEmpty.Cases(new[] {
                    new Input { fixedSep, NullStr   }.Test(SubstrAfter(expected: null)),
                    new Input { _,        ""        }.Test(SubstrAfter(expected: "")),
                    new Input { _,        "abcdefg" }.Test(SubstrAfter(expected: "abcdefg")),
                }),
            };
        }

        Tester<string> SubstrAfter(string expected)
        {
            return Tester.EqTester<string>(expected, (context, input) => ((Func<string, string, string>)input[0])(context, (string)input[1]));
        }
    }
}
