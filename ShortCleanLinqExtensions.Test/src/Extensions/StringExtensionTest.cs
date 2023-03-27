using ShortCleanLinqExtensions.src.Extensions;

namespace ShortCleanLinqExtensions.Test.src.Extensions
{
    public class StringExtensionTest
    {
        [Fact(DisplayName = "Given string check if method converted to title")]
        public void Given_String_Check_If_Method_Converted_To_Tittle()
        {
            string input = "ula leila";

            var title = input.Title();

            string expectedTitle = "Ula Leila";

            Assert.Equal(expectedTitle, title);
        }
    }
}