using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaPark.Business.DTOs.MovieDtos;

public class MoviePostDto
{
    public int GenreId { get; set; }
    public string Name { get; set; }
    public string Desc { get; set; }
    public double SalePrice { get; set; }
    public double CostPrice { get; set; }
    public bool IsDeleted { get; set; }
}

public class MovieCreateValidator:AbstractValidator<MoviePostDto>
{
    public MovieCreateValidator()
    {
        RuleFor(m => m.Name)
            .NotNull().WithMessage("Name can't be null")
            .NotEmpty().WithMessage("Name can't be empty")
            .MaximumLength(40).WithMessage("Max lentgh must be 40 char")
            .MinimumLength(1).WithMessage("Min lentgh must be 1 char");

        RuleFor(m => m.Desc)
            .NotNull().WithMessage("Name can't be null")
            .NotEmpty().WithMessage("Name can't be empty")
            .MaximumLength(250).WithMessage("Max lentgh must be 250 char")
            .MinimumLength(1).WithMessage("Min lentgh must be 1 char");

        RuleFor(m => m.SalePrice)
            .NotNull().WithMessage("SalePrice can't be null")
            .GreaterThanOrEqualTo(1).When(m => !m.IsDeleted).WithMessage("SalePrice must be up than 1 when Isdeleted is false");

        RuleFor(m => m.CostPrice)
            .NotNull().WithMessage("CostPrice can't be null")
            .GreaterThanOrEqualTo(1).When(m => !m.IsDeleted).WithMessage("SalePrice must be up than 1 when Isdeleted is false");


        RuleFor(m => m).Custom((m, context) =>
        {
            if (m.SalePrice < m.CostPrice)
            {
                context.AddFailure("SalePrice", "SalePrice must be up than CostPrice");
            }
        });
    }
}