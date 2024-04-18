using AutoMapper;
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
using System.Threading.Tasks.Sources;

namespace CinemaPark.Business.Services.Implementations
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IGenreRepository _genreRepository;
        private readonly IMapper _mapper;
        public MovieService(IMovieRepository movieRepository,IGenreRepository genreRepository,IMapper mapper)
        {
            _movieRepository = movieRepository;
            _genreRepository = genreRepository;
            _mapper = mapper;

        }
        public async Task CreateAsync(MoviePostDto moviePostDto)
        {
            if (moviePostDto is null) throw new NotFoundException(404, "Movie is not found");
            if(!await _genreRepository.Table.AnyAsync(g => g.Id == moviePostDto.GenreId))
            {
                throw new NotFoundException(404, "Genre is not found");
            }

           Movie movie= _mapper.Map<Movie>(moviePostDto);

            movie.CreatedDate = DateTime.UtcNow;
            movie.UpdatedDate = DateTime.UtcNow;

            await _movieRepository.InsertAsync(movie);
            await _movieRepository.CommitAsync();
        }

        public async Task DeleteAsync(int id,MovieDeleteDto movieDeleteDto)
        {
            if (id != movieDeleteDto.Id) throw new Exception("id must be valid");
            if (!await _movieRepository.Table.AnyAsync(m => m.Id == movieDeleteDto.Id)) throw new NotFoundException(404, "movie is not found");
            var movie=await _movieRepository.GetByIdAsync(id);
            if (movie is null) throw new NotFoundException(404, "Movie is not found");
            _movieRepository.Delete(movie);
            await _movieRepository.CommitAsync();
        }

        public async Task<IEnumerable<MovieGetDto>> GetAllAsync()
        {
            List<MovieGetDto> movieGetDtos = new List<MovieGetDto>();
            var movies=await _movieRepository.GetAllAsync(null,"Genre");
            foreach (var movie in movies)
            {
                MovieGetDto movieGetDto= _mapper.Map<MovieGetDto>(movie);
                movieGetDtos.Add(movieGetDto);
            }
            return movieGetDtos;
        }

        public async Task<MovieGetDto> GetByIdAsync(int id)
        {
            var movie=await _movieRepository.GetSingleAsync(m=>m.Id==id,"Genre");
            if(movie is null) throw new NotFoundException(404,"Movie is not found");
            MovieGetDto movieGetDto= _mapper.Map<MovieGetDto>(movie);
            return movieGetDto;
        }

        public async Task UpdateAsync(int id, MoviePutDto moviePutDto )
        {
            if (id != moviePutDto.Id) throw new Exception("Id is not valid");
            if(!await _genreRepository.IsExist(g=>g.Id == moviePutDto.GenreId)) { throw new NotFoundException(404,"Genre is not found"); }
            var currentMovie = await _movieRepository.GetByIdAsync(id);
            if (currentMovie is null) throw new NotFoundException(404, "Movie is not found");
            Movie movie= _mapper.Map(moviePutDto,currentMovie);
            movie.UpdatedDate = DateTime.UtcNow;

            await _movieRepository.CommitAsync();
        }
    }
}
