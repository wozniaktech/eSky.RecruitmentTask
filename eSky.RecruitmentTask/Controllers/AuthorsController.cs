﻿using eSky.RecruitmentTask.Services;
using Microsoft.AspNetCore.Mvc;

namespace eSky.RecruitmentTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private IAuthorService _authorService;

        public AuthorsController(IAuthorService authorService)
        {
                _authorService = authorService;
        }

        [HttpGet]
        public async Task<IActionResult> GetPoemsByAuthor(int numberOfAuthors = 3)
        {
            var authors = await _authorService.GetAuthors(numberOfAuthors);

            if (authors == null || !authors.Any()) 
            {
                return NoContent();
            }

            //var poems = await _authorService.GetPoemsByAuthor(authors);

            var poems = await _authorService.GetPoems2(authors);

            if(poems == null || !poems.Any())
            {
                return NoContent();
            }
            return Ok(poems);
        }
    }
}
