using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaPark.Business.DTOs.GenreDtos;

public class GenreDeleteDto
{
    public int Id { get; set; } 
}

public class GenreDeleteValidator:AbstractValidator<GenreDeleteDto> 
{
    public GenreDeleteValidator()
    {
        RuleFor(g => g.Id)
       .NotNull()
       .GreaterThan(0)
       .WithMessage("Id must be up than 0");
    }
}