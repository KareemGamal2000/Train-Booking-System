using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class User : IdentityUser<Guid>
    {
        [StringLength(50)]
        [Required]
        public string FirstName { get; set; }

        [StringLength(50)]
        [Required]
        public string LastName { get; set; }

        [StringLength(100)]
        public string FullName => $"{FirstName} {LastName}";

        [Required]
        public DateTime? DateOfBirth { get; set; }

        [StringLength(10)]
        public string? Gender { get; set; }

        [StringLength(50)]
        [Required]
        public string? PhoneNumber { get; set; }

        [StringLength(500)]

        public string? Address { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime? UpdatedAt { get; set; }

        [StringLength(200)]
        public string? ProfilePicture { get; set; }

        public DateTime? LastLoginDate { get; set; }

        [StringLength(10)]
        public bool IsActive { get; set; } = true;

        public virtual ICollection<Booking> Bookings { get; set; } = new HashSet<Booking>();
    }
}
