using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using MyWebsite.Models.ViewModels;
using System.Collections.Generic;

namespace MyWebsite.BusinessLogic.ArticlePagination
{
    [HtmlTargetElement("div", Attributes = "page-index")]
    public class PageLinkTagHelperBlog : TagHelper
    {
        private IUrlHelperFactory urlHelperFactoryBlog;
        public PageLinkTagHelperBlog(IUrlHelperFactory helperFactory)
        {
            urlHelperFactoryBlog = helperFactory;
        }
        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext ViewContext { get; set; }
        public PagingInfo PageIndex { get; set; }
        public string PageAction { get; set; }
        [HtmlAttributeName(DictionaryAttributePrefix = "page-url-")]
        public Dictionary<string, object> PageUrlValues { get; set; } = new Dictionary<string, object>();
        public bool PageClassesEnabled { get; set; } = false;
        public string PageClass { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            TagBuilder tagArrowRight = new TagBuilder("i");
            tagArrowRight.Attributes["class"] = "fa fa-chevron-right";
            tagArrowRight.Attributes["aria-hidden"] = "true";

            TagBuilder tagArrowLeft = new TagBuilder("i");
            tagArrowLeft.Attributes["class"] = "fa fa-chevron-left";
            tagArrowLeft.Attributes["aria-hidden"] = "true";

            IUrlHelper urlHelper = urlHelperFactoryBlog.GetUrlHelper(ViewContext);
            TagBuilder result = new TagBuilder("div");

            if (PageIndex.ShowPrevious)
            {
                var page = PageIndex.CurrentPage - 1;
                TagBuilder tagPrevious = new TagBuilder("a");

                PageUrlValues["articlePage"] = page;
                tagPrevious.Attributes["href"] = urlHelper.Action(PageAction, PageUrlValues);
                tagPrevious.AddCssClass("btn btn-primary float-left");
                tagPrevious.InnerHtml.AppendHtml(tagArrowLeft);
                tagPrevious.InnerHtml.AppendHtml(" Previous");
                result.InnerHtml.AppendHtml(tagPrevious);
            }

            if (PageIndex.ShowNext)
            {
                var page = PageIndex.CurrentPage + 1;
                TagBuilder tagNext = new TagBuilder("a");

                PageUrlValues["articlePage"] = page;
                tagNext.Attributes["href"] = urlHelper.Action(PageAction, PageUrlValues);
                tagNext.AddCssClass("btn btn-primary float-right");
                tagNext.InnerHtml.AppendHtml("Next ");
                tagNext.InnerHtml.AppendHtml(tagArrowRight);
                result.InnerHtml.AppendHtml(tagNext);
            }

            output.Content.AppendHtml(result.InnerHtml);
        }
    }

    [HtmlTargetElement("div", Attributes = "page-blog")]
    public class PageLinkTagHelperIndex : TagHelper
    {
        private IUrlHelperFactory urlHelperFactoryIndex;
        public PageLinkTagHelperIndex(IUrlHelperFactory helperFactory)
        {
            urlHelperFactoryIndex = helperFactory;
        }
        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext ViewContextIndex { get; set; }
        public PagingInfo PageBlog { get; set; }
        public string PageActionIndex { get; set; }
        [HtmlAttributeName(DictionaryAttributePrefix = "page-url-")]
        public Dictionary<string, object> PageUrlValuesIndex { get; set; } = new Dictionary<string, object>();
        public bool PageClassesEnabled { get; set; } = false;
        public string PageClass { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            IUrlHelper urlHelper = urlHelperFactoryIndex.GetUrlHelper(ViewContextIndex);
            TagBuilder result = new TagBuilder("div");

            TagBuilder tagArrow = new TagBuilder("i");
            tagArrow.Attributes["class"] = "fa fa-chevron-right";
            tagArrow.Attributes["aria-hidden"] = "true";

            var page = PageBlog.CurrentPage;
            TagBuilder tagOther = new TagBuilder("a");
            PageUrlValuesIndex["articlePage"] = page;
            tagOther.Attributes["href"] = urlHelper.Action(PageActionIndex, PageUrlValuesIndex);
            tagOther.AddCssClass("btn btn-primary float-left");
            tagOther.InnerHtml.AppendHtml(tagArrow);
            tagOther.InnerHtml.AppendHtml(" Other posts");
            result.InnerHtml.AppendHtml(tagOther);

            output.Content.AppendHtml(result.InnerHtml);
        }
    }
}