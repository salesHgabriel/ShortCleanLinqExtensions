using ShortCleanLinqExtensions.src.Extensions;

namespace ShortCleanLinqExtensions.Test.src.Extensions;

public class EnumerableExtensionTest
{
        [Fact(DisplayName = "Return Same Value when enumerable is null")]
        public void GivenEnumerableNull_Shoud_Return_Same_value_WhenIsnull()
        {
            IEnumerable<Guid> list = null;


            var listNull = list.WhereNull(i => i == Guid.Empty);

            IEnumerable<Guid> expectedListSameValue = null;

            Assert.Equal(expectedListSameValue, list);
        }

        [Fact(DisplayName = "Return Same Value when enumerable is null")]
        public void GivenEnumerableNull_Shoud_Return_Same_valueWithValue()
        {
            IEnumerable<Guid> list = [Guid.Parse("30593109-4948-46A4-9CF1-174390E6C857")];

            var listNull = list.WhereNull(i => i == Guid.Empty);

            IEnumerable<Guid> expectedListSameValue = [Guid.Parse("30593109-4948-46A4-9CF1-174390E6C857")];

            Assert.Equal(expectedListSameValue, list);
        }

}
