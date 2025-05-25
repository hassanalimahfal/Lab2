using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Lab2.Models
{
    public partial class Subject
    {
        [Key]
        public int Id { get; set; }
        [StringLength(100)]
        public string Name { get; set; } = null!;
        public int? TeacherId { get; set; }
        [StringLength(250)]
        public string? Details { get; set; }
        public bool? IsActive { get; set; }

        [ForeignKey("TeacherId")]
        [InverseProperty("Subjects")]
        public virtual Teacher? Teacher { get; set; }
    }
}
