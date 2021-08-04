using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using RACB.APIs.DTOs;
using RACB.APIs.Helpers.ModelBindings;
using RACB.APIs.Models;

namespace RACB.APIs.Controllers
{
    [ApiController]
    [Route("api/authorcollections")]
    public class AuthorCollectionsController : Controller
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IMapper _mapper;

        public AuthorCollectionsController(ICourseRepository courseRepository, IMapper mapper)
        {
            _courseRepository = courseRepository ??
                                throw new ArgumentException(nameof(courseRepository));
            _mapper = mapper ??
                      throw new ArgumentException(nameof(mapper));
        }

        [HttpGet("({ids})", Name = "GetAuthors")]
        public ActionResult<IEnumerable<AuthorDto>> GetAuthors(
            [FromRoute]
            [ModelBinder(BinderType = typeof(ArrayModelBinder))]
            IEnumerable<Guid> ids)
        {
            if (ids == null)
            {
                return BadRequest();
            }

            var authors = _courseRepository.GetAuthors(ids);

            if (authors.Count() != ids.Count())
            {
                return NotFound();
            }

            return Ok(_mapper.Map<IEnumerable<AuthorDto>>(authors));
        }

        [HttpPost]
        public ActionResult<IEnumerable<AuthorDto>> CreateAuthors(IEnumerable<NewAuthorDto> newAuthors)
        {
            var authors = _mapper.Map<IEnumerable<Author>>(newAuthors);

            foreach (var author in authors)
            {
                _courseRepository.AddAuthor(author);
            }

            _courseRepository.Save();

            var authorsToReturn = _mapper.Map<IEnumerable<AuthorDto>>(authors);
            var idsString = string.Join(",", authorsToReturn.Select(x => x.Id));

            return CreatedAtRoute("GetAuthors",
                new { ids = idsString },
                authorsToReturn);
        }
    }
}
