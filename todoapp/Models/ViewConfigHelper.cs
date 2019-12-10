using System.Collections.Generic;

namespace todoapp.Models
{
    public enum Category
    {
        Private, Work, Other, None
    }

    public enum CompletedFilter
    {
        all, notCompleted, completed
    }

    internal static class ViewConfigHelper
    {
        public static Category categoryFilter = Category.None;
        public static CompletedFilter completedFilter = CompletedFilter.notCompleted;
        public static Dictionary<string, Category> categoryStringToEnum;

        static ViewConfigHelper()
        {
            categoryStringToEnum = new Dictionary<string, Category>{};

            foreach (var item in System.Enum.GetNames(typeof(Category)))
            {
                if (System.Enum.TryParse(item, out Category category))
                {
                    categoryStringToEnum.Add(item, category);
                }
            }
        }
    }
}
