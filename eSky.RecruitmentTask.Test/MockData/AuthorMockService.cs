using eSky.RecruitmentTask.Models;

namespace eSky.RecruitmentTask.Test.MockData
{
    public class AuthorMockService
    {
        public static List<string> GetAuthors(int numberOfAuthors)
        {
            if (numberOfAuthors <= 0)
                throw new ArgumentOutOfRangeException("Number of authors should be bigger then zero!", nameof(numberOfAuthors));

            return new List<string>
            {
                "Alexander Pope",
                "James Whitcomb Riley",
                "Ann Taylor"
            };
        }

        public static List<string> GetAuthorsNoData(int numberOfAuthors)
        {
            if (numberOfAuthors <= 0)
                throw new ArgumentOutOfRangeException("Number of authors should be bigger then zero!", nameof(numberOfAuthors));

            return new List<string>();
        }

        public static List<Author> GetPoemsByAuthor(List<string> authors)
        {
            if (authors == null && !authors.Any())
                throw new ArgumentOutOfRangeException("No authors", nameof(authors));

            return new List<Author>
            {
                new Author
                {
                    Name =  "Alexander Pope",
                    Poems = new List<string>
                    {
                        "Epitaph. Intended for Sir Isaac Newton, in Westminster Abbey.",
                        "Ode on St Cecilia's Day,",
                        "The Alley.",
                        "The Garden.",
                        "Epitaph. on General Henry Withers, in Westminster Abbey, 1729.",
                        "Epitaph. on Two Lovers Struck Dead by Lightning.",
                        "Book IV. Ode I. to Venus.",
                        "Part of the Ninth Ode of the Fourth Book.",
                        "Autumn.",
                        "Winter.",
                        "Messiah.",
                        "An Essay on Criticism.",
                        "The Rape of the Lock:",
                        "Two Choruses to the Tragedy of Brutus.",
                        "Elegy to the Memory of an Unfortunate Lady",
                        "Weeping.",
                        "On Silence.",
                        "Artemisia.",
                        "Phryne.",
                        "The Happy Life of a Country Parson.",
                        "Epistle to Robert Earl of Oxford and Earl Mortimer.",
                        "Epistle to Mr Jervas, With Mr Dryden's Translation of Fresnoy's 'art of Painting.'",
                        "Epistle to Miss Blount, With the Works of Voiture.",
                        "Epistle to Mrs Teresa Blount.  on Her Leaving the Town After the Coronation."
                    }
                },
                new Author
                {
                    Name = "James Whitcomb Riley",
                    Poems = new List<string>
                    {
                        "There Was a Cherry-Tree",
                        "Liberty",
                        "Who Bides His Time",
                        "A Summer Afternoon",
                        "Our Hired Girl",
                        "A Life-Lesson",
                        "The Song of Yesterday",
                        "A Song of the Road",
                        "Ike Walton's Prayer"
                    }
            },
                new Author
                {
                    Name = "Ann Taylor",
                    Poems = new List<string>
                    {
                        "The Baby's Dance",
                        "About the Little Girl that Beat Her Sister",
                        "The Star",
                        "For a Naughty Little Girl",
                        "The Little Cripple's Complaint"
                    }
                }
            };
        }

        public static List<Author> GetPoemsByAuthorNoData(List<string> authors)
        {
            if (authors == null && !authors.Any())
                throw new ArgumentOutOfRangeException("No authors", nameof(authors));

            return new List<Author>();

        }
    }
}
