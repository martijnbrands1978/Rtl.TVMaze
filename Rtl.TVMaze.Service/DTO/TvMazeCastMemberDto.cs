using System.Text.Json.Serialization;

namespace Rtl.TVMaze.Service.DTO
{

    public class TvMazeCastMemberDto
    {
        [JsonPropertyName("person")]
        public Person Person { get; set; }
    }

    public class Person
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("birthday")]
        public string BirthDay { get; set; }
    }
}
