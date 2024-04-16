using ETickets.Models;

namespace ETickets.IRepositry
{
    public interface ICategoryRepositry
    {
        public void Delete(int id);

        public void Update(Category categry);

        public Category GetById(int id);
        public List<Category> GetAll();

        public void Create(Category category);

    }
}
