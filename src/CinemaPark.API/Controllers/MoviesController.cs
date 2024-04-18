using CinemaPark.Business.DTOs.MovieDtos;
using CinemaPark.Business.Services.Interfaces;
using CinemaPark.Business.Utilities.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CinemaPark.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieService _movieService;
        public MoviesController(IMovieService movieService)
        {
            _movieService = movieService;
        }
        [HttpGet("")]
        public async Task <IActionResult> GetAll () 
        {
            return Ok( await _movieService.GetAllAsync());
        }
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                return Ok(await _movieService.GetByIdAsync(id));
            }
            catch (NotFoundException ex)
            {
                return StatusCode(ex.statusCode, ex.Message);
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPost("[action]")]
        public async Task <IActionResult> Create(MoviePostDto moviePostDto)
        {
            try
            {
                await _movieService.CreateAsync(moviePostDto);
            }
            catch (NotFoundException ex)
            {
                return StatusCode(ex.statusCode, ex.Message);
            }
            catch (Exception)
            {

                throw;
            }
            return Created();
        }
        [HttpPut("[action]/{id}")]
        public async Task <IActionResult> Update([FromRoute]int id, [FromBody] MoviePostDto moviePostDto)
        {
            try
            {
                await _movieService.UpdateAsync(id, moviePostDto);
            }
            catch (NotFoundException ex)
            {
                return StatusCode(ex.statusCode, ex.Message);
            }
            catch (Exception)
            {

                throw;
            }
            return Ok();
        }
        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> Delete([FromRoute]int id)
        {
            try
            {
                await _movieService.DeleteAsync(id);
            }
            catch (NotFoundException ex)
            {
                return StatusCode(ex.statusCode, ex.Message);
            }
            catch (Exception)
            {

                throw;
            }
            return Ok();
        }
    }
}
