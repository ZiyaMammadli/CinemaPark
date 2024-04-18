using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaPark.Business.DTOs.GenreDtos;

public class GenrePutDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public bool IsDeleted { get; set; }
}

public class GenrePutValidator : AbstractValidator<GenrePutDto>
{
    public GenrePutValidator()
    {
        RuleFor(g=>g.Id)
            .NotNull()
            .GreaterThan(0)
            .WithMessage("Id must be up than 0");

        RuleFor(g => g.Name)
          .NotNull().WithMessage("Name can't be null")
          .NotEmpty().WithMessage("Name can't be empty")
          .MaximumLength(40).WithMessage("Max lentgh must be 40 char")
          .MinimumLength(1).WithMessage("Min lentgh must be 1 char");
    }
}