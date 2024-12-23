using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication.DAL.Models
{
    public class CategoryModel
    {
        public int Id { get; set; }
        public string? CategoryName { get; set; }



        public virtual Collection<ProductModel> ProductModel { get; set; }
    }
}
