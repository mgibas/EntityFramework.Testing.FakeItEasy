using System.ComponentModel.DataAnnotations;

namespace EntityFramework.Testing.FakeItEasy.Tests
{
    public class TestModel
    {
        [Key]
        public int Id { get; set; }

        public string Property { get; set; }
    }
}
