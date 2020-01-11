using System.Text.Json.Serialization;

namespace Rtl.TVMaze.Service.DTO
{
    public class TvMazeShowDto
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string  Name { get; set; }
    }
}
