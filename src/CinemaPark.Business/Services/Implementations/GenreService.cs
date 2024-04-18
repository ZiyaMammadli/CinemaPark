using CinemaPark.Business.DTOs.GenreDtos;
using CinemaPark.Business.Services.Interfaces;
using CinemaPark.Business.Utilities.Exceptions;
using CinemaPark.Core.Entities;
using CinemaPark.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaPark.Business.Services.Implementations;

public class GenreService : IGenreService
{
    private readonly IGenreRepository _genreRepository;
    public GenreService(IGenreRepository genreRepository)
    {
        _genreRepository = genreRepository;
    }
    public async Task CreateAsync(GenrePostDto genrePostDto)
    {
        if (genrePostDto is null) throw new NotFoundException(404,"Genre is not found");
        Genre genre = new Genre()
        {
            Name= genrePostDto.Name,
            IsDeleted=genrePostDto.IsDeleted,
            CreatedDate=DateTime.UtcNow,
            UpdatedDate=DateTime.UtcNow,
        };
        await _genreRepository.InsertAsync(genre);
        await _genreRepository.CommitAsync();
    }

    public async Task DeleteAsync(int id)
    {
       var genre= await _genreRepository.GetByIdAsync(id);
        if(genre is null) throw new NotFoundException(404,"Genre is not found");
        _genreRepository.Delete(genre);
        await _genreRepository.CommitAsync();
    }

    public async Task<IEnumerable<GenreGetDto>> GetAllAsync()
    {
        List<GenreGetDto> genreGetDtos = new List<GenreGetDto>();

        var genres= await _genreRepository.GetAllAsync();
        foreach(var genre in genres)
        {
            GenreGetDto dto = new GenreGetDto()
            {
                Id= genre.Id,
                Name = genre.Name,
            };
            genreGetDtos.Add(dto);
        }
        return genreGetDtos;
    }

    public async Task<GenreGetDto> GetByIdAsync(int id)
    {
        var genre=await _genreRepository.GetByIdAsync(id);
        if(genre is null) throw new NotFoundException(404,"Genre is not found!");
        GenreGetDto genreGetDto = new GenreGetDto()
        {
            Id = genre.Id,
            Name = genre.Name,
        };
        return genreGetDto;
    }

    public async Task UpdateAsync(int id, GenrePostDto genrePostDto)
    {
        var currentGenre=await _genreRepository.GetByIdAsync(id);
        if (currentGenre is null) throw new NotFoundException(404, "Genre is not found!");
        if (genrePostDto is null) throw new NotFoundException(404, "Genre is not found!");
        currentGenre.UpdatedDate = DateTime.UtcNow;
        currentGenre.Name= genrePostDto.Name;
        currentGenre.IsDeleted=genrePostDto.IsDeleted;
        await _genreRepository.CommitAsync();
    }

}
