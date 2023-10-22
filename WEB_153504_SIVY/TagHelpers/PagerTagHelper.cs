using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace WEB_153504_SIVY.TagHelpers
{
    public class PagerTagHelper : TagHelper
    {
        private readonly LinkGenerator _linkGenerator;
        private readonly HttpContext _context;

        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public bool Admin { get; set; } = false;
        public string Category { get; set; }
        public PagerTagHelper(LinkGenerator linkGenerator, IHttpContextAccessor httpContextAccessor)
        {
            _linkGenerator = linkGenerator;
            _context = httpContextAccessor.HttpContext;
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var ul = new TagBuilder("ul");
            ul.AddCssClass("pagination");

            int prev = CurrentPage == 1 ? 1 : CurrentPage - 1;
            ul.InnerHtml.AppendHtml(GeneratePagerItem(prev, "Previous"));

            for (int i = 1; i <= TotalPages; i++)
            {
                ul.InnerHtml.AppendHtml(GeneratePagerItem(i));
            }

            int next = CurrentPage == TotalPages ? TotalPages : CurrentPage + 1;
            ul.InnerHtml.AppendHtml(GeneratePagerItem(next, "Next"));

            output.Content.AppendHtml(ul);
        }


        private TagBuilder GeneratePagerItem(int i, string? value = null)
        {
            var li = new TagBuilder("li");
            string liClass = i == CurrentPage && value == null ? "page-item active" : "page-item";
            li.AddCssClass(liClass);

            var a = new TagBuilder("a");
            a.AddCssClass("page-link async-request");

            if (Admin)
                a.Attributes.Add("href",
                    _linkGenerator.GetPathByPage(_context, values: new { pageno = i }));
            else
                a.Attributes.Add("href", _linkGenerator.GetPathByAction(_context,
                    controller: "CarModel", action: "Index", values: new { category = Category, pageno = i }));

            a.InnerHtml.AppendHtml(value != null ? value : i.ToString());
            li.InnerHtml.AppendHtml(a);

            return li;
        }
    }
}
