using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Moq;
using Ninject;
using System.Linq;
using SportStore.Domain.Concrete;
using SportStore.Domain.Abstract;
using SportStore.Domain.Entities;


namespace SportStore.WebUI.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;

        public NinjectDependencyResolver(IKernel kernelParam)
        {
            kernel = kernelParam;
            AddBindings();
        }

        public object GetService(Type serviceType)
        {
            return kernel.TryGet( serviceType );
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll( serviceType );
        }

        private void AddBindings()
        {

            kernel.Bind<IProductRepository>().To<EFProductRepository>();

        }
    }
}