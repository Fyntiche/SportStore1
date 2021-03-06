﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SportStore.Domain.Abstract;
using SportStore.Domain.Entities;
using SportStore.WebUI.Controllers;
using SportStore.WebUI.HtmlHelpers;
using SportStore.WebUI.Models;

namespace SportStore.UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Can_Paginate()
        {
            //Организация
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup( m => m.Products ).Returns( new Product [ ] {
                new Product { ProductID = 1, Name = "P1"},
                new Product { ProductID = 2, Name = "P2"},
                new Product { ProductID = 3, Name = "P3"},
                new Product { ProductID = 4, Name = "P4"},
                new Product { ProductID = 5, Name = "P5"},
            } );
            ProductController controller = new ProductController( mock.Object )
            {
                PageSize = 3
            };
            //действие
            ProductsListViewModel result = (ProductsListViewModel) controller.List( 2 ).Model;
            //Утвердение
            Product [ ] prodArray = result.Products.ToArray();
            Assert.IsTrue( prodArray.Length == 2 );
            Assert.AreEqual( prodArray [ 0 ].Name , "P4" );
            Assert.AreEqual( prodArray [ 1 ].Name , "P5" );
        }

        [TestMethod]
        public void Can_Generate_Page_Links()
        {
            //Организация - это определение вспомогательного метода HTML;
            //это необходимо для применения расширяющего метода
            HtmlHelper myHelper = null;
            //Организация - создания данных PagingInfo
            PagingInfo pagingInfo = new PagingInfo
            {
                CurrentPage = 2 ,
                TotalItems = 28 ,
                ItemsPerPage = 10
            };
            //Организация - настройка делегата с помощью лямда-выражения
            Func<int , string> pageUrlDelegate = i => "Page" + i;
            //Действие
            MvcHtmlString result = myHelper.PageLinks( pagingInfo , pageUrlDelegate );
            //Утверждение
            Assert.AreEqual(@"<a class=""btn btn-default"" href=""Page1"">1</a>"
                + @"<a class=""btn btn-default btn-primary selected"" href=""Page2"">2</a>"
                + @"<a class=""btn btn-default"" href=""Page3"">3</a>" , result.ToString() );
        }

        [TestMethod]
        public void Can_Send_Pagination_View_Model()
        {
            //Организация
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup( m => m.Products ).Returns( new Product [ ] {
                new Product { ProductID = 1, Name = "P1"},
                new Product { ProductID = 2, Name = "P2"},
                new Product { ProductID = 3, Name = "P3"},
                new Product { ProductID = 4, Name = "P4"},
                new Product { ProductID = 5, Name = "P5"},
            } );
            //Организация
            ProductController controller = new ProductController( mock.Object );
            controller.PageSize = 3;
            //действие
            ProductsListViewModel result = ( ProductsListViewModel ) controller.List( 2 ).Model;
            //Утвердение
            PagingInfo pageInfo = result.PagingInfo;
            Assert.AreEqual( pageInfo.CurrentPage , 2 );
            Assert.AreEqual( pageInfo.ItemsPerPage , 3 );
            Assert.AreEqual( pageInfo.TotalItems , 5 );
            Assert.AreEqual( pageInfo.TotalPages , 2 );

        }
    }
}
