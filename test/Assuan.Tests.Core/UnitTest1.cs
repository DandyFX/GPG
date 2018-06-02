using System;
using Dandy.GPG.Assuan;
using Dandy.GPG.Rt;
using Xunit;

namespace Assuan.Tests.Core
{
    public class UnitTest1
    {
        public UnitTest1()
        {
            Runtime.Init();
        }
        
        [Fact]
        public void TestRuntimeVersion()
        {
            var version = Runtime.CheckVersion("1.0");
            Assert.NotNull(version);
        }

        [Fact]
        public void Test1()
        {
            using (var ctx = new Context()) {
            }
        }
    }
}
