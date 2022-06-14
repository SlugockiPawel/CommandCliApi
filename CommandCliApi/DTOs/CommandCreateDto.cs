using System.ComponentModel.DataAnnotations;

namespace CommandCliApi.DTOs
{
    public class CommandCreateDto
    {
        // id will be populated by the database, no need to put it here

        [Required]
        [MaxLength(250)]
        public string HowTo { get; set; }
        [Required] public string Line { get; set; }
        [Required] public string Platform { get; set; }
    }
}
