using Microsoft.AspNetCore.Razor.TagHelpers;

namespace TicketApp.TagHelpers
{
	public class ButtonTagHelper : TagHelper
	{

		public override void Process(TagHelperContext context, TagHelperOutput output)
		{
			output.Attributes.SetAttribute("class", "btn btn-success");
		}
	}
}
