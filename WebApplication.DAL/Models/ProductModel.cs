using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication.DAL.Models
{
    public class ProductModel
    {
        public int Id { get; set; }
        public string? ProductName { get; set; }
        public string? UserId { get; set; }
        public DateTime CreatedTime { get; set; } = DateTime.Now;
        public DateTime UpdatedTime { get; set; }
        public DateTime StartDate { get; set; }
        public string? Price { get; set; }
        public int DurationInDay { get; set; }
        public byte[] ImagePath { get; set;}



        public CategoryModel CategoryModel { get; set; }
        public int CategoryModelId { get; set; }

    }
}
