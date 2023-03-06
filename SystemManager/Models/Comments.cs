using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemManager.Models
{
    internal class Comments
    {
        public int Id { get; set; }
        public string? Text { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
