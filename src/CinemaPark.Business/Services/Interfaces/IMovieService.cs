using CinemaPark.Business.DTOs.MovieDtos;
using CinemaPark.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaPark.Business.Services.Interfaces;

public interface IMovieService
{
    Task<IEnumerable<MovieGetDto>> GetAllAsync();
    Task<MovieGetDto>GetByIdAsync(int id);
    Task CreateAsync(MoviePostDto moviePostDto);
    Task UpdateAsync(int id,MoviePutDto moviePutDto);
    Task DeleteAsync(int id,MovieDeleteDto movieDeleteDto);
}
