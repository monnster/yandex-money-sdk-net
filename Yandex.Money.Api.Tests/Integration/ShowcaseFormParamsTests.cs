using System;
using System.Diagnostics.Eventing;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;
using Yandex.Money.Api.Sdk.Net;
using Yandex.Money.Api.Sdk.Requests;

namespace Yandex.Money.Api.Tests.Integration
{
	public class ShowcaseFormParamsTests: IntegrationTestBase
	{
		[Fact]
		public async Task ShowcaseShouldReturnFormParams()
		{
			var merchants = new ShowcaseSearchRequest("1")
				.Perform(new DefaultHttpPostClient())
				.Result
				.Results;

			//TraceObject(merchants);

			foreach (var merchant in merchants)
			{
				if (!merchant.IsFormDescriptionAvailable)
				{
					TraceWrite("Form params not available for merchant {0} (id {1})", merchant.Title, merchant.Id);
					continue;
				}

				try
				{
					var merchantParams= new ShowcaseFormParamsRequest(merchant.Id, merchant.Params)
						.Perform(new DefaultHttpPostClient())
						.Result;

					TraceObject(merchantParams);
				}
				catch (Exception ex)
				{
					TraceWrite("Unable to get form params for merchant {0} (id {1})", merchant.Title, merchant.Id);
				}
			}



			//Assert.NotNull(merchantParams);
			//Assert.NotNull(merchantParams.Form);
			//Assert.NotEmpty(merchantParams.Form);

			//TraceObject(merchantParams);
		}

		public ShowcaseFormParamsTests(ITestOutputHelper outputHelper) : base(outputHelper)
		{
		}
	}
}