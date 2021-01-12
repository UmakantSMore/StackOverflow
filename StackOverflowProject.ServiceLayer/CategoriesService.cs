using System;
using System.Collections.Generic;
using System.Linq;
using StackOverflowProject.DomainModels;
using StackOverflowProject.ViewModels;
using StackOverflowProject.Repositories;
using AutoMapper;
using AutoMapper.Configuration;

namespace StackOverflowProject.ServiceLayer
{
    public interface ICategoriesService
    {
        void InsertCategory(CategoryViewModel cvm);
        void UpdateCategory(CategoryViewModel cvm);
        void DeleteCategory(int CategoryID);
        List<CategoryViewModel> GetCategories();
        CategoryViewModel GetCategoryByCategoryID(int CategoryID);
    }

    public class CategoriesService : ICategoriesService
    {
        Repositories.ICategoriesService cr;
        public CategoriesService()
        {
            cr = new CategoriesRepository();
        }

        public void DeleteCategory(int CategoryID)
        {
            cr.DeleteCategory(CategoryID);

        }

        public List<CategoryViewModel> GetCategories()
        {
            List<Category> c = cr.GetCategories();
            var config = new MapperConfiguration(temp => { temp.CreateMap<Category, CategoryViewModel> (); temp.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            List<CategoryViewModel> cvm = mapper.Map< List<Category>,List<CategoryViewModel>>(c);
            return cvm;
        }

        public CategoryViewModel GetCategoryByCategoryID(int CategoryID)
        {
            Category c = cr.GetCategoryByCategoryID(CategoryID).FirstOrDefault();
            CategoryViewModel cvm = null;
            if (c != null)
            {
                var config = new MapperConfiguration(temp => { temp.CreateMap<Category, CategoryViewModel>(); temp.IgnoreUnmapped(); });
                IMapper mapper = config.CreateMapper();
                cvm = mapper.Map<Category, CategoryViewModel>(c);
            }
            
            return cvm;
        }

        public void InsertCategory(CategoryViewModel cvm)
        {
            var config = new MapperConfiguration(temp => { temp.CreateMap<CategoryViewModel, Category>(); temp.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            Category c = mapper.Map<CategoryViewModel, Category>(cvm);
            cr.InsertCategory(c);

        }

        public void UpdateCategory(CategoryViewModel cvm)
        {
            var config = new MapperConfiguration(temp => { temp.CreateMap<CategoryViewModel, Category>(); temp.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            Category c = mapper.Map<CategoryViewModel, Category>(cvm);
            cr.UpdateCategory(c);

        }
    }
}
