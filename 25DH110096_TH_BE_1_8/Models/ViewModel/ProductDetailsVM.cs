using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList.Mvc;
using System.Security.Permissions;


namespace _25DH110096_TH_BE_1_8.Models.ViewModel
{
    public class ProductDetailsVM
    {
        public Product product { get; set; }
        public int quantity { get; set; } = 1;

        public decimal estimatedValue { get; set; }
        public int PageNumber {  get; set; }
        public int PageSize { get; set; } = 3;
        public PagedList.IPagedList<Product> RelatedProducts { get; set; }
        //public List<Product> RelatedProducts { get; set; }

        public PagedList.IPagedList<Product> TopProducts { get; set; }

    }
}