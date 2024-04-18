using AutoMapper;
using CinemaPark.Business.DTOs.GenreDtos;
using CinemaPark.Business.DTOs.MovieDtos;
using CinemaPark.Business.Services.Interfaces;
using CinemaPark.Business.Utilities.Exceptions;
using CinemaPark.Core.Entities;
using CinemaPark.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaPark.Business.Services.Implementations;

public class GenreService : IGenreService
{
    private readonly IGenreRepository _genreRepository;
    private readonly IMapper _mapper;
    public GenreService(IGenreRepository genreRepository,IMapper mapper)
    {
        _genreRepository = genreRepository;
        _mapper = mapper;
    }
    public async Task CreateAsync(GenrePostDto genrePostDto)
    {
        if (genrePostDto is null) throw new NotFoundException(404,"Genre is not found");
        Genre genre=_mapper.Map<Genre>(genrePostDto);

        genre.CreatedDate = DateTime.UtcNow;
        genre.UpdatedDate = DateTime.UtcNow;

        await _genreRepository.InsertAsync(genre);
        await _genreRepository.CommitAsync();
    }

    public async Task DeleteAsync(int id,GenreDeleteDto genreDeleteDto)
    {
        if (id != genreDeleteDto.Id) throw new Exception("Id must be valid");
        if (!await _genreRepository.IsExist(g => g.Id == genreDeleteDto.Id)) throw new NotFoundException(404, "Genre is not found");
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
            GenreGetDto genreGetDto=_mapper.Map<GenreGetDto>(genre);
            genreGetDtos.Add(genreGetDto);
        }
        return genreGetDtos;
    }

    public async Task<GenreGetDto> GetByIdAsync(int id)
    {
        var genre=await _genreRepository.GetByIdAsync(id);
        if(genre is null) throw new NotFoundException(404,"Genre is not found!");
        GenreGetDto genreGetDto= _mapper.Map<GenreGetDto>(genre);
        return genreGetDto;
    }

    public async Task UpdateAsync(int id, GenrePutDto genrePutDto)
    {
        if (id != genrePutDto.Id) throw new Exception("id must be valid");
        if (!await _genreRepository.IsExist(g => g.Id == genrePutDto.Id)) throw new NotFoundException(404, "Genre is not found");
        var currentGenre=await _genreRepository.GetByIdAsync(id);
        if (currentGenre is null) throw new NotFoundException(404, "Genre is not found!");
        if (genrePutDto is null) throw new NotFoundException(404, "Genre is not found!");
        Genre genre=_mapper.Map(genrePutDto, currentGenre);
        genre.UpdatedDate = DateTime.UtcNow;
        await _genreRepository.CommitAsync();
    }

}
