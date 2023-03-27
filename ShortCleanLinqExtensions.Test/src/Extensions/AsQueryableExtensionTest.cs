using ShortCleanLinqExtensions.src.Extensions;
using ShortCleanLinqExtensions.src.Utils.PaginatorHelper;

namespace ShortCleanLinqExtensions.Test.src.Extensions
{
    public class AsQueryableExtensionTest
    {
        [Fact(DisplayName = "Filter list when conditional is true")]
        public void GivenListAn_List_When_Condition_Is_True_Filtered()
        {
            var listC1 = new List<int>() { 4, 5, 6, 7 };

            int filter = 4;

            bool condition = true;

            var newListFiltered = listC1
                .AsQueryable()
                .When(condition, l => l.Equals(filter))
                .ToList();

            var expectedListFilted = new List<int>() { 4 };

            Assert.Equal(expectedListFilted, newListFiltered);
        }

        [Fact(DisplayName = "Create object PageResponse() when type is asqueryable")]
        public void if_data_is_AsQueryable_return_Type_PaginateResponse()
        {
            var list = new List<string>() { "1", "2", "3" };

            int page = 1;

            int limit = 10;

            var listPagineted = list
                .AsQueryable()
                .Paginate(page, limit);

            var expectedListPaginated = new PagedResponse<List<string>>(list, page, limit);

            Assert.IsType(listPagineted.GetType(), expectedListPaginated);
        }
    }
}