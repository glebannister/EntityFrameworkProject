﻿using System.ComponentModel.DataAnnotations;

namespace GlobalMarket.Core.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [MaxLength(50)]
        public string Email { get; set; }

        [Required]
        [MaxLength(100)]
        public byte[] PasswordHash { get; set; }

        [Required]
        [MaxLength(100)]
        public byte[] PasswordSalt { get; set; }
    }
}
