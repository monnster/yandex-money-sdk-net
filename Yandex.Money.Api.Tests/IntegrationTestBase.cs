using Newtonsoft.Json;
using Xunit.Abstractions;

namespace Yandex.Money.Api.Tests
{
	public class IntegrationTestBase
	{
		private readonly ITestOutputHelper _outputHelper;

		public IntegrationTestBase(ITestOutputHelper outputHelper)
		{
			_outputHelper = outputHelper;
		}

		protected void TraceWrite(string message, params object[] args)
		{
			_outputHelper.WriteLine(message, args);
		}

		protected void TraceObject(object obj)
		{
			_outputHelper.WriteLine(JsonConvert.SerializeObject(obj, Formatting.Indented));
		}
	}
}