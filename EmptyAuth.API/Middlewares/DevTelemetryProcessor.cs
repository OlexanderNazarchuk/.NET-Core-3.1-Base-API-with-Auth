using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.ApplicationInsights.Extensibility;

namespace EmptyAuth.API.Middlewares
{
	public class DevTelemetryProcessor : ITelemetryProcessor
	{
		private readonly ITelemetryProcessor _next;

		public DevTelemetryProcessor(ITelemetryProcessor next)
		{
			_next = next;
		}

		public void Process(ITelemetry telemetry)
		{
			if (telemetry is RequestTelemetry request)
			{
				return;
			}
			_next.Process(telemetry);
		}
	}
}
