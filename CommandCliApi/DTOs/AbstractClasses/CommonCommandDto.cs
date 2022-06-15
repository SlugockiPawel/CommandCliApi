using System.ComponentModel.DataAnnotations;

namespace CommandCliApi.DTOs.AbstractClasses
{
    /// <summary>
    /// This class is used to instantiate CommandCreateDto and CommandUpdateDto as they are the same
    /// </summary>
    public abstract class CommonCommandDto
    {
        // id will be populated by the database, no need to put it here

        [Required]
        [MaxLength(250)]
        public string HowTo { get; set; }
        [Required] public string Line { get; set; }
        [Required] public string Platform { get; set; }
    }
}
