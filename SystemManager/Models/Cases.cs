using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemManager.Models.Entities;

namespace SystemManager.Models
{
    internal class Cases
    {
       
        public int Id { get; set; }
        public string Description { get; set; } = null!;
        public DateTime CreatedTime { get; set; } = DateTime.Now;
        public string Status { get; set; } = null!;
        public Customers Customer { get; set; } = null!;
    }
}
