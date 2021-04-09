using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoListApp.Models
{
    public class ToDoItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ToDoItemID { get; set; }
        [StringLength(1000)]
        [Display(Name = "User")]
        public string UserEmail { get; set; }
        [StringLength(1000)]
        public string Title { get; set; }
        [StringLength(5000)]
        public string Description { get; set; }
        [StringLength(500)]
        public string AddedDate { get; set; }
        [StringLength(500)]
        public string DueDate { get; set; }
        [Display(Name = "Status: Checked for Completing task")]
        public bool Done { get; set; }
        [StringLength(500)]
        public string DoneDate { get; set; }
    }
}
