using CinemaPark.Business.DTOs.GenreDtos;
using CinemaPark.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaPark.Business.Services.Interfaces;

public interface IGenreService
{
    Task <IEnumerable<GenreGetDto>> GetAllAsync ();
    Task <GenreGetDto> GetByIdAsync (int id);
    Task CreateAsync(GenrePostDto genrePostDto);  
    Task UpdateAsync (int id, GenrePostDto genrePostDto);
    Task DeleteAsync (int id);
}
