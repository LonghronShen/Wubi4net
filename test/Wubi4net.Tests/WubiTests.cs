using System;
using System.ComponentModel;
using System.Linq;
using Wubi4net.Format;
using Xunit;

namespace Wubi4net.Tests
{

    public class WubiTests
    {

        [Theory()]
        [InlineData('我', "trnt")]
        public void SingleCharacterConvertTest(char input, string code)
        {
            var codes = WubiHelper.ConvertSingleCharacter(input, false, OutputStyle.LowerCase);
            Assert.Equal(code, codes.FirstOrDefault());
        }

        [Theory()]
        [InlineData("我爱你", "tewq")]
        public void PhraseConvertTest(string input, string code)
        {
            var codes = WubiHelper.ConvertPhraseAsSingleCode(input, OutputStyle.LowerCase);
            Assert.Equal(code, codes);
        }

    }

}