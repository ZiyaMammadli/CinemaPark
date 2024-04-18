using AutoMapper;
using CinemaPark.Business.DTOs.MovieDtos;
using CinemaPark.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaPark.Business.MappingProfiles;

public class MovieMapProfile:Profile
{
    public MovieMapProfile()
    {
        CreateMap<MoviePostDto, Movie>().ReverseMap();
        CreateMap<MoviePutDto, Movie>().ReverseMap();
        CreateMap<MovieGetDto, Movie>().ReverseMap();
    }
}
