using AutoMapper;
using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class ProductManager: IProductService
    {
        private IProductDal productDal;
        //private readonly IMapper _mapper;
        public ProductManager(IProductDal productDal, IMapper mapper)
        {
            this.productDal = productDal;
           // _mapper = mapper;
        }
        public void Add(Product product)
        {
           // var mapProduct = _mapper.Map<Product>(product);
            productDal.Add(product);
        }

        public void Delete(Product product)
        {
           //var mapProduct = _mapper.Map<Product>(product);
            productDal.Delete(product);
        }

        public Product GetById(int productId)
        {
            var mapProduct = productDal.Get(x => x.Id == productId).Result;
            return mapProduct;
        }

        public List<Product> GetList()
        {
            var products = productDal.GetList().Result.ToList();
            //var productsList= _mapper.Map<List<ProductListDto>>(products);
            return products;
        }

        public void Update(Product product)
        {
            //var updateProduct = _mapper.Map<Product>(product);
            //updateProduct.Id= Id;
            productDal.Update(product);
        }
    }
}
