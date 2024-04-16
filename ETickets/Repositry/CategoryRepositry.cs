using ETickets.Data;
using ETickets.IRepositry;
using ETickets.Models;

namespace ETickets.Repositry
{
    public class CategoryRepositry : ICategoryRepositry
    {
        ApplicationDbContext context;//= new ApplicationDbContext();


        public CategoryRepositry(ApplicationDbContext context)
        {
            this.context = context;
        }

        public void Create(Category category)
        {
            context.Categories.Add(category);

            context.SaveChanges();
        }

        public void Delete(int id)
        {
            Category ?category = context.Categories.Find(id);

            if (category != null)
            {
                context.Categories.Remove(category);
                context.SaveChanges();
            }

        }
        
        public List<Category> GetAll()
        {
            return context.Categories.ToList();
        }

        public Category GetById(int id)
        {
            return context.Categories.Find(id);
            
        }

        public void Update(Category _category)
        {
            Category? category = context.Categories.Find(_category.Id);

            if (category != null)
            {
                category.Name = _category.Name;
                

                context.SaveChanges();
            }
        }
        }
    }
