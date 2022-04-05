using System.ComponentModel.DataAnnotations.Schema;

namespace RestApiWithDatabase.Models
{
    [Table("actor")]
    public class Actor
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}