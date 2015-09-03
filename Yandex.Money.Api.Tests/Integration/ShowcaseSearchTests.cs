using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;
using Yandex.Money.Api.Sdk.Net;
using Yandex.Money.Api.Sdk.Requests;

namespace Yandex.Money.Api.Tests.Integration
{
	public class ShowcaseSearchTests: IntegrationTestBase
	{
		[Fact]
		public async Task ShowcaseShouldSearchByName()
		{
			var result = new ShowcaseSearchRequest("мгтс")
				.Perform(new DefaultHttpPostClient())
				.Result;

			Assert.NotNull(result);
			Assert.NotNull(result.Results);
			Assert.NotEmpty(result.Results);

			TraceObject(result);
		}

		[Fact]
		public async Task ShowcaseShouldLimitResults()
		{
			var result = new ShowcaseSearchRequest("1", 10)
				.Perform(new DefaultHttpPostClient())
				.Result;


			Assert.Equal(10, result.Results.Length);
			Assert.True(result.NextPage);

			TraceObject(result);
		}

		public ShowcaseSearchTests(ITestOutputHelper outputHelper) : base(outputHelper)
		{
		}
	}
}
