using eSky.RecruitmentTask.Helper;
using eSky.RecruitmentTask.Models;

namespace eSky.RecruitmentTask.Test.MockData
{
    public class AuthorMockService
    {
        public static IEnumerable<string> GetAuthors(int numberOfAuthors)
        {
            if (numberOfAuthors <= 0)
                throw new ArgumentOutOfRangeException(ErrorMessages.INCORRECT_NUMBER_OF_AUTHORS,
                  nameof(numberOfAuthors));

            IEnumerable<string> authors = new List<string>
            {
                "Alexander Pope",
                "James Whitcomb Riley",
                "Ann Taylor"
            };

            return authors;
        }

        public static List<string> GetAuthorsNoData(int numberOfAuthors)
        {
            if (numberOfAuthors <= 0)
                throw new ArgumentOutOfRangeException(ErrorMessages.INCORRECT_NUMBER_OF_AUTHORS,
                  nameof(numberOfAuthors));

            return new List<string>();
        }

        public static IEnumerable<Poem>? GetPoems(IEnumerable<string> authors)
        {
            if ((authors == null) || (!authors.Any()))
            {
                throw new ArgumentOutOfRangeException(ErrorMessages.LIST_OF_AUTHORS_CANT_BE_NULL_OR_EMPTY, nameof(authors));
            }

            IEnumerable<Poem> poems = new List<Poem> { 
               new Poem
               {
                   Title = "Some title",
                   Author = "Alexander Pope",
                   Lines = new List<string> {"Line1","Line2","Line3"},
                   Linecount = "Linecount"
               },
               new Poem
               {
                   Title = "Some title",
                   Author = "James Whitcomb Riley",
                   Lines = new List<string> {"Line1","Line2","Line3"},
                   Linecount = "Linecount"
               },
                     new Poem
               {
                   Title = "Some title",
                   Author = "Ann Taylor",
                   Lines = new List<string> {"Line1","Line2","Line3"},
                   Linecount = "Linecount"
               }
            };

            return poems;
        }

        public static IEnumerable<Poem>? NoPoems(IEnumerable<string> authors)
        {
            if ((authors == null) || (!authors.Any()))
            {
                throw new ArgumentOutOfRangeException(ErrorMessages.LIST_OF_AUTHORS_CANT_BE_NULL_OR_EMPTY, nameof(authors));
            }

            IEnumerable<Poem> poems = new List<Poem> {};

            return poems;
        }

        public static IEnumerable<Author> GetPoemsByAuthor(IEnumerable<Poem>? poems, IEnumerable<string> authors)
        {
            if ((poems == null) || (!poems.Any()))
            {
                throw new ArgumentOutOfRangeException(ErrorMessages.INCORRECT_NUMBER_OF_POEMS, nameof(poems));
            }

            if ((authors == null) || (!authors.Any()))
            {
                throw new ArgumentOutOfRangeException(ErrorMessages.LIST_OF_AUTHORS_CANT_BE_NULL_OR_EMPTY, nameof(authors));
            }

            IEnumerable<Models.Author> listOfAuthors = new List<Author>
            {
                new Author
                {
                    Name = "Alexander Pope",
                    Poems = new List<string>
                    {
                        "Poem 1", "Poem 2", "Poem 3"
                    }
                },
                new Author
                {
                    Name = "James Whitcomb Riley",
                    Poems = new List<string>
                    {
                        "Poem 1", "Poem 2", "Poem 3"
                    }
                },
                new Author
                {
                    Name = "Ann Taylor",
                    Poems = new List<string>
                    {
                        "Poem 1", "Poem 2", "Poem 3"
                    }
                }
            };
            return listOfAuthors;
        }

        public static IEnumerable<Author> NoPoemsByAuthor(IEnumerable<Poem>? poems, IEnumerable<string> authors)
        {
            if ((poems == null) || (!poems.Any()))
            {
                throw new ArgumentOutOfRangeException(ErrorMessages.INCORRECT_NUMBER_OF_POEMS, nameof(poems));
            }

            if ((authors == null) || (!authors.Any()))
            {
                throw new ArgumentOutOfRangeException(ErrorMessages.LIST_OF_AUTHORS_CANT_BE_NULL_OR_EMPTY, nameof(authors));
            }

            IEnumerable<Author> listOfAuthors = new List<Author> {};
            return listOfAuthors;
        }













    }
}
