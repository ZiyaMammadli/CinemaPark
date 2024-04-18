using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaPark.Business.DTOs.MovieDtos;

public class MovieDeleteDto
{
    public int Id { get; set; }
}

public class MovieDeleteValidator : AbstractValidator<MovieDeleteDto>
{
    public MovieDeleteValidator()
    {
        RuleFor(m => m.Id)
           .NotNull()
           .GreaterThan(0)
           .WithMessage("Id must be up than 0");
    }
}