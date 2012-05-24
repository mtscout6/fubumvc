using FubuMVC.Core.Urls;
using FubuMVC.Diagnostics.Core.Grids.Columns;
using FubuMVC.Diagnostics.Instrumentation.Handlers.Routes.Models;

namespace FubuMVC.Diagnostics.Instrumentation.Handlers.Routes.Columns
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
            //This should be getting the url for the details page for this action(the drill down page)
			//return _urls.UrlFor(new RecordedRequestRequestModel {Id = target.Id});
            return string.Empty;
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