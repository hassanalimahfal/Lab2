using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Lab2.Models
{
    public partial class Teacher
    {
        public Teacher()
        {
            Subjects = new HashSet<Subject>();
        }

        [Key]
        public int Id { get; set; }
        [StringLength(100)]
        public string Name { get; set; } = null!;
        [Column(TypeName = "decimal(18, 0)")]
        public decimal? Salary { get; set; }
        [StringLength(250)]
        public string? Details { get; set; }
        public bool? IsActive { get; set; }

        [InverseProperty("Teacher")]
        public virtual ICollection<Subject> Subjects { get; set; }
    }
}
