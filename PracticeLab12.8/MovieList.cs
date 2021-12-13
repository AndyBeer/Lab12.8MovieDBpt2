using PracticeLab12._8.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace PracticeLab12._8
{
    public class MovieList  //Will this help with list scope between controller actions?
    {
        [MinLength(1, ErrorMessage ="No movies found.  Use the 'Create Movie' link to add.") ]
        public static List<Movie> Movies { get; set; }
    }
}
