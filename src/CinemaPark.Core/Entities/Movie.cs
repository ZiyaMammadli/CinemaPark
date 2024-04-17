using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaPark.Core.Entities;

public class Movie:BaseEntity
{
    public int GenreId { get; set; }
    public string Name { get; set; }
    public string Desc { get; set; }
    public double SalePrice { get; set; }
    public double CostPrice { get; set; }
    public Genre Genre { get; set; }
}
