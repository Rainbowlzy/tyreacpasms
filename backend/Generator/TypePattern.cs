using System.ComponentModel.DataAnnotations;

namespace Generator
{
    public class TypePattern
    {
        [Key] public int Id { get; set; }
        public string DbType { get; set; }
        public int Length { get; set; }
        public string RegexPattern { get; set; }
    }
}