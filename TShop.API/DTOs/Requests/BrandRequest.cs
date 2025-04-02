﻿using System.ComponentModel.DataAnnotations;

namespace TShop.API.DTOs.Requests
{
    public class BrandRequest
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public IFormFile Logo { get; set; }

        public bool Status { get; set; }
    }
}
