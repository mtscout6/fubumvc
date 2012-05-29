using FubuMVC.Core.Urls;
using FubuMVC.Diagnostics.Features.Requests;

namespace FubuMVC.Diagnostics.Core.Grids.Columns.Requests
{
	public class RequestUrlColumn : GridColumnBase<RecordedRequestModel>
	{
		private readonly IUrlRegistry _urls;

		public RequestUrlColumn(IUrlRegistry urls)
			: base("RequestUrl")
		{
			_urls = urls;
		}

		public override string ValueFor(RecordedRequestModel target)
		{
			var url = _urls.UrlFor(new RecordedRequestRequestModel {Id = target.Id});

		    return url;
		}

		public override bool IsIdentifier()
		{
			return true;
		}

		public override bool IsHidden(RecordedRequestModel target)
		{
			return true;
		}

		public override bool HideFilter(RecordedRequestModel target)
		{
			return true;
		}
	}
}