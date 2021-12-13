using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Dapper;

namespace PracticeLab12._8.Models
{
    public class Movie
    {
        [Key]
        [Required]
        public int ID { get; set; }
        [MaxLength(30)]
        public string Title { get; set; }
        public string Genre { get; set; }
        [Range(1880, 2021)]
        public int Year { get; set; }

        public int RunTime { get; set; }

    }
}
