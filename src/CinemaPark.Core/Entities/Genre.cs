using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaPark.Core.Entities;

public class Genre:BaseEntity
{
    public string Name { get; set; }    
    public List<Movie> Movies { get; set; }
}
