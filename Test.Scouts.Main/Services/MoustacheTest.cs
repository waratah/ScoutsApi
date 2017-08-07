using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Moq;
using Scouts.Services;

namespace Test.Scouts.Main.Services
{
    public class MoustacheTest
    {
        [Fact]
        public void MoustacheDictionaryReplaceAtStart()
        {
            var dict = new Dictionary<string, object>
            {
                {"name", "Name replacement" },
                {"date" , "date replacement"},
                {"start" , "value start"},
            };

            var input = "{{start}}. We must replace {{name}} at {{date}}.";
            var expected = "value start. We must replace Name replacement at date replacement.";
            var result = input.Moustache(dict);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void MoustacheDictionaryReplace()
        {
            var dict = new Dictionary<string, object>
            {
                {"name", "Name replacement" },
                {"date" , "date replacement"},
                {"start" , "value start"},
            };

            var input = "We must replace {{name}} at {{date}}.";
            var expected = "We must replace Name replacement at date replacement.";
            var result = input.Moustache(dict);
            Assert.Equal(expected, result);
        }

    }
}
