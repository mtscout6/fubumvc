using FubuMVC.Core.Urls;
using FubuMVC.Diagnostics.Core.Grids.Columns;
using FubuMVC.Diagnostics.Instrumentation.Features.Routes.Models;

namespace FubuMVC.Diagnostics.Instrumentation.Features.Routes.Columns
{
    public class RequestUrlColumn : GridColumnBase<RouteInstrumentationModel>
	{
		private readonly IUrlRegistry _urls;

		public RequestUrlColumn(IUrlRegistry urls)
			: base("RequestUrl")
		{
			_urls = urls;
		}

        public override string ValueFor(RouteInstrumentationModel target)
		{
			return _urls.UrlFor(new InstrumentationInputModel {Id = target.Id});
		}

		public override bool IsIdentifier()
		{
			return true;
		}

        public override bool IsHidden(RouteInstrumentationModel target)
		{
			return true;
		}

        public override bool HideFilter(RouteInstrumentationModel target)
		{
			return true;
		}
	}
}