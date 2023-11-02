namespace eSky.RecruitmentTask.Helper
{
    public static class Helper
    {
        public static List<T> GetRandomAuthors<T>(this IEnumerable<T> list, int elementsCount)
        {
            return list.OrderBy(arg => Guid.NewGuid()).Take(elementsCount).ToList();
        }
    }
}
