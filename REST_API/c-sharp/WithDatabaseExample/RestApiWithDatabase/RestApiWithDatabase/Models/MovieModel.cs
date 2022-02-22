using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;


namespace RestApiWithDatabase.Models
{
    public class MovieModel
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public double Rating { get; set; }
    }
}
