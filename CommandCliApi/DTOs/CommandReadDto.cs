using System.ComponentModel.DataAnnotations;

namespace CommandCliApi.DTOs
{
    public class CommandReadDto
    {
        public int Id { get; set; }
        public string HowTo { get; set; }
        public string Line { get; set; }

        // assume that we do not want to pass platform to the client (maybe it should be confidential etc.)
    }
}