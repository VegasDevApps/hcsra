using System.ComponentModel.DataAnnotations;

namespace ApplicationAPI.DTOs
{
    public class UserUpdateDto
    {
        public int Id { get; set; }

        [Length(minimumLength: 3, maximumLength: 50)]
        public string Name { get; set; }
        
        [EmailAddress]
        public string Email { get; set; }
    }
}