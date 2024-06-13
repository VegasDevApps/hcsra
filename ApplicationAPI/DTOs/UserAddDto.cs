using System.ComponentModel.DataAnnotations;

namespace ApplicationAPI.DTOs
{
    public class UserAddDto
    {
        [Length(minimumLength: 3, maximumLength: 50)]
        public string Name { get; set; }
        [EmailAddress]
        public string Email { get; set; }
    }
}