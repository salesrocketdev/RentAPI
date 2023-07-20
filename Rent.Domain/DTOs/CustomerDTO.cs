﻿using System.ComponentModel.DataAnnotations;

namespace Rent.Domain.DTOs
{
    public class CustomerDTO : Core.Models.BaseDTO
    {
        public string? Name { get; set; }
        public int Age { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public DocumentDTO? Document { get; set; }
    }
}
