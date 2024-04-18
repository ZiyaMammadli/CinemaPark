using AutoMapper;
using CinemaPark.Business.DTOs.GenreDtos;
using CinemaPark.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaPark.Business.MappingProfiles;

public class GenreMapProfile:Profile
{
    public GenreMapProfile()
    {
        CreateMap<GenreGetDto,Genre>().ReverseMap();
        CreateMap<GenrePutDto,Genre>().ReverseMap();
        CreateMap<GenrePostDto,Genre>().ReverseMap();
    }
}
