using eSky.RecruitmentTask.Helper;
using eSky.RecruitmentTask.Services;
using Microsoft.AspNetCore.Mvc;

namespace eSky.RecruitmentTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private IAuthorService _authorService;
        private readonly ILogger _logger;

        public AuthorsController(IAuthorService authorService,
            ILogger<AuthorsController> logger)
        {
            _authorService = authorService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetPoemsByAuthor(int numberOfAuthors = 3)
        {
            var authors = await _authorService.GetAuthors(numberOfAuthors);
            if (authors == null || !authors.Any()) 
            {
                _logger.LogError(ErrorMessages.CANNOT_GET_LIST_OF_AUTHORS);
                return NoContent();
            }
                      
            var poems = await _authorService.GetPoems(authors);
            if(poems == null || !poems.Any())
            {
                _logger.LogError(ErrorMessages.CANNOT_GET_LIST_OF_POEMS);
                return NoContent();
            }
          
            var poemsByAuthor = _authorService.GetPoemsByAuthor(poems, authors);
            if (poemsByAuthor == null || !poemsByAuthor.Any())
            {
                _logger.LogError(ErrorMessages.CANNOT_GET_LIST_OF_POEMSBYAUTHOR);
                return NoContent();
            }

            return Ok(poemsByAuthor);
        }
    }
}
