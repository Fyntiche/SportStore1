﻿using SportStore.Domain.Abstract;
using SportStore.WebUI.Models;
using System.Linq;
using System.Web.Mvc;


namespace SportStore.WebUI.Controllers
{
    public class ProductController : Controller
    {
        public IProductRepository repository;
        public int PageSize = 4;
        public ProductController(IProductRepository productRepository)
        {
            this.repository = productRepository;
        }

        public ViewResult List(int page = 1)
        {
            ProductsListViewModel model = new ProductsListViewModel
            {
                Products = repository.Products
                .OrderBy( p => p.ProductID )
                .Skip( (page - 1) * PageSize )
                .Take( PageSize ) ,
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page ,
                    ItemsPerPage = PageSize ,
                    TotalItems = repository.Products.Count()
                }
            };
            return View( model );
        }

    }
}