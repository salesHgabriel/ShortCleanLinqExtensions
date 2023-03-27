namespace ShortCleanLinqExtensions.src.Utils.PaginatorHelper
{
    public class PagedResponse<T> : Response<T>
    {
        public PagedResponse(T data, int page, int limit)
        {
            Page = page;
            Limit = limit;
            Data = data;
        }

        public int Page { get; set; }
        public int Limit { get; set; }
        public Uri? FirstPage { get; set; }
        public Uri? LastPage { get; set; }
        public int Total { get; set; }
        public Uri? NextPage { get; set; }
        public Uri? PreviousPage { get; set; }

        public string Links()
        {
            bool hasNext = NextPage is not null;

            bool hasPrevious = PreviousPage is not null && Page > 1;

            string style = ".pagination a {\r\n  color: black;\r\n  float: left;\r\n  padding: 8px 16px;\r\n  text-decoration: none;\r\n  transition: background-color .3s;\r\n  border: 1px solid #ddd;\r\n}\r\n\r\n.pagination a.active {\r\n  background-color: #4CAF50;\r\n  color: white;\r\n  border: 1px solid #4CAF50;\r\n}\r\n\r\n.pagination a:hover:not(.active) {background-color: #ddd;} .isDisabled {\r\n  color: currentColor;\r\n  cursor: not-allowed;\r\n  opacity: 0.5;\r\n  text-decoration: none;  pointer-events: none;\r\n cursor: default; \r\n }";

            string html = $"<style>{style}</style>" +
            $"<div class='pagination' > " +
            $"<a  {(hasPrevious ? "" : "disabled class='isDisabled'")} href='{PreviousPage}'>❮</a>" +
            $"<a  {(hasNext ? "" : "disabled class='isDisabled'")} href='{NextPage}'>❯</a>" +
            $"</div>";

            return html;
        }
    }
}