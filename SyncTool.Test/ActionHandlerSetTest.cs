using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using SyncTool.Interface;
using Xunit.Abstractions;

namespace SyncTool.Test
{
    public class ActionHandlerSetTest : TestBase
    {
        public ActionHandlerSetTest(ITestOutputHelper output) : base(output)
        {
        }

        [Fact]
        public void ValidateDataLoad()
        {
            ServiceProviderFactory builder = new ServiceProviderFactory();
            var provider = builder.Provider;

            IActionHandlerEnumerator handlerSet = provider.GetService<IActionHandlerEnumerator>();

            handlerSet.Handlers.Should().HaveCount(3);

        }
    }
}