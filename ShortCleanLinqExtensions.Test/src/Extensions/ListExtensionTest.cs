using ShortCleanLinqExtensions.src.Extensions;

namespace ShortCleanLinqExtensions.Test.src.Extensions
{
    public class ListExtensionTest
    {
        [Fact(DisplayName = "Check if List Was converted to object string json")]
        public void Check_List_Was_Converted_ToJson()
        {
            var dataList = new List<string>() { "1", "2", "3" };

            var listConverted = dataList.ToJson();

            string expectedObjectConvertedJson = "[\"1\",\"2\",\"3\"]";

            Assert.Equal(expectedObjectConvertedJson, listConverted);
        }

        [Fact(DisplayName = "Check if two list was merged")]
        public void Check_New_List_Created_Was_Merge()
        {
            var manyList = new List<List<int>>()
            {
                new List<int>()
                {
                    1, 2, 3
                },
                new List<int>()
                {
                   4, 5, 6, 7
                }
            };

            var listCollapsed = manyList
                .Collapse()
                .ToList();

            var expectedListMerged = new List<int>()
            {
                 1, 2, 3, 4, 5, 6, 7
            };

            Assert.Equal(expectedListMerged, listCollapsed);
        }

        [Fact(DisplayName = "Given lists check if new list get difference")]
        public void Given_List_Check_If_Get_Difference()
        {
            List<int> firstList = new List<int>() { 1, 2, 3, 4, 5 };

            List<int> secondList = new List<int>() { 2, 4, 6, 8 };

            List<int> listdiff = firstList.Diff(secondList);

            var expectedListDifference = new List<int>() { 1, 3, 5 };

            Assert.Equal(expectedListDifference, listdiff);
        }
    }
}