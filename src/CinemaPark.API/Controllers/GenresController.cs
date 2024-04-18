using CinemaPark.Business.DTOs.GenreDtos;
using CinemaPark.Business.Services.Interfaces;
using CinemaPark.Business.Utilities.Exceptions;
using CinemaPark.Core.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CinemaPark.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly IGenreService _genreService;
        public GenresController(IGenreService genreService)
        {
            _genreService = genreService;
        }

        [HttpGet("")]
        public async Task< IActionResult> GetAll()
        { 
            return Ok( await _genreService.GetAllAsync());
        }
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                return Ok( await _genreService.GetByIdAsync(id));
            }
            catch (NotFoundException ex)
            {
                return StatusCode(ex.statusCode,ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }
        [HttpPost("[Action]")]
        public async Task<IActionResult> Create(GenrePostDto genrePostDto)
        {
            try
            {
                await _genreService.CreateAsync(genrePostDto);
            }
            catch (NotFoundException ex)
            {
                return StatusCode(ex.statusCode, ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Created();
        }
        [HttpPut("[action]/{id}")]
        public async Task <IActionResult> Update([FromRoute]int id, [FromBody]GenrePutDto genrePutDto)
        {
            try
            {
                await _genreService.UpdateAsync(id, genrePutDto);
            }
            catch (NotFoundException ex)
            {
                return StatusCode(ex.statusCode, ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }
        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id, [FromBody]GenreDeleteDto genreDeleteDto)
        {
            try
            {
                await _genreService.DeleteAsync(id,genreDeleteDto);
            }
            catch (NotFoundException ex)
            {
                return StatusCode(ex.statusCode, ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }
    }
}
