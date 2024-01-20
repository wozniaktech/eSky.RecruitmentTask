namespace eSky.RecruitmentTask.Helper
{
    public struct ErrorMessages
    {
        public const string INCORRECT_NUMBER_OF_AUTHORS= "Number of authors should be bigger then zero.";
        public const string LIST_OF_AUTHORS_CANT_BE_NULL_OR_EMPTY = "List of authors can't be null or empty.";
        public const string CANNOT_DESERIALIZE_AUTHORS_LIST = "Cannot deserialize author's list.";
        public const string CANNOT_RETRIEVE_AUTHORS_FROM_SERVER = "Cannot retrieve authors from the server.";
        public const string INCORRECT_NUMBER_OF_POEMS = "Number of poems should be bigger then zero.";
        public const string URL_CANT_BE_NULL_OR_EMPTY = "Url can't be null or empty.";
        public const string CANT_RETREIVE_DATA_FROM_SERVER = "Can't retrieve data from server.";
        public const string CANNOT_GET_LIST_OF_AUTHORS = "Cannot get list of authors.";
        public const string CANNOT_GET_LIST_OF_POEMS = "Cannot get list of poems.";
        public const string CANNOT_GET_LIST_OF_POEMSBYAUTHOR = "Cannot get list of poems by author.";
    }
}