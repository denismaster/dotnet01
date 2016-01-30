using System.Text;
using System.Web;
using System.Web.Mvc;
using Courses.ViewModels;

namespace Courses.Gui.Helpers
{
    public static class PagingHelpers
    {
        public static MvcHtmlString PageLinks(this HtmlHelper html, PageInfo pageInfo, System.Func<int, string> pageUrl)
        {
            StringBuilder result = new StringBuilder();
            
            for (int i = 1; i <= pageInfo.TotalPages; i++)
            {
                TagBuilder liBuilder = new TagBuilder("li");
                TagBuilder tag = new TagBuilder("a");
                tag.MergeAttribute("href", pageUrl(i));
                tag.InnerHtml = i.ToString();
                // если текущая страница, то выделяем ее,
                // например, добавляя класс
                if (i == pageInfo.PageNumber)
                {
                    liBuilder.AddCssClass("active");
                }
                liBuilder.InnerHtml += tag.ToString();
                result.Append(liBuilder.ToString());
            }
            return MvcHtmlString.Create(result.ToString());
        }
    }
}