﻿using System.ComponentModel.DataAnnotations;

namespace TodoAPI.Domain
{
    public class TodoItem
    {
        [Required]
        public string ID { get; set; } = "";

        [Required]
        public string Name { get; set; } = "";

        [Required]
        public string Notes { get; set; } = "";

        public bool Done { get; set; }
    }
}
