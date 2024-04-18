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
        public MovieService(IMovieRepository movieRepository,IGenreRepository genreRepository)
        {
            _movieRepository = movieRepository;
            _genreRepository = genreRepository;
        }
        public async Task CreateAsync(MoviePostDto moviePostDto)
        {
            if (moviePostDto is null) throw new NotFoundException(404, "Movie is not found");
            if(!await _genreRepository.Table.AnyAsync(g => g.Id == moviePostDto.GenreId))
            {
                throw new NotFoundException("Genre is not found");
            }
            Movie movie = new Movie()
            {
                Name = moviePostDto.Name,
                Desc = moviePostDto.Desc,
                SalePrice = moviePostDto.SalePrice,
                CostPrice = moviePostDto.CostPrice,
                IsDeleted = moviePostDto.IsDeleted,
                GenreId = moviePostDto.GenreId,
                CreatedDate=DateTime.UtcNow,
                UpdatedDate=DateTime.UtcNow,
            };
            await _movieRepository.InsertAsync(movie);
            await _movieRepository.CommitAsync();
        }

        public async Task DeleteAsync(int id)
        {
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
                MovieGetDto movieGetDto = new MovieGetDto()
                {
                    Id = movie.Id,
                    Name=movie.Name,
                    Desc=movie.Desc,
                    GenreName=movie.Genre.Name,
                    SalePrice=movie.SalePrice,                    
                };
                movieGetDtos.Add(movieGetDto);
            }
            return movieGetDtos;
        }

        public async Task<MovieGetDto> GetByIdAsync(int id)
        {
            var movie=await _movieRepository.GetSingleAsync(m=>m.Id==id,"Genre");
            if(movie is null) throw new NotFoundException(404,"Movie is not found");
            MovieGetDto movieGetDto = new MovieGetDto()
            {
                Id=movie.Id,
                Name=movie.Name,
                Desc=movie.Desc,
                GenreName=movie.Genre.Name,
                SalePrice=movie.SalePrice,               
            };
            return movieGetDto;
        }

        public async Task UpdateAsync(int id, MoviePostDto moviePostDto)
        {
            if(!await _genreRepository.Table.AnyAsync(g=>g.Id==moviePostDto.GenreId)) { throw new NotFoundException("Genre is not found"); }
            var currentMovie = await _movieRepository.GetByIdAsync(id);
            if (currentMovie is null) throw new NotFoundException(404, "Movie is not found");

            currentMovie.Name = moviePostDto.Name;
            currentMovie.Desc = moviePostDto.Desc;
            currentMovie.GenreId = moviePostDto.GenreId;
            currentMovie.SalePrice = moviePostDto.SalePrice;
            currentMovie.CostPrice = moviePostDto.CostPrice;
            currentMovie.IsDeleted = moviePostDto.IsDeleted;
            currentMovie.UpdatedDate = DateTime.UtcNow;

            await _movieRepository.CommitAsync();
        }
    }
}
