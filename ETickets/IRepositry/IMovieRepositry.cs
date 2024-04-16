using ETickets.Models;

namespace ETickets.IRepositry
{
    public interface IMovieRepositry
    {
        public void Delete(int id);
        
        public List<Movie> ReadAll();
        public void Update(Movie movie, List<int> selectedActors);


    public Movie ReadOneById(int id);


    public void Create(Movie movie, List<int> SelectedActors);

        public List<Movie> SearchByCategoryId(int id);
        public Movie ReadOneWithCinemaCategory(int id);
        public List<Movie> SearchByCinemaIdlWithCinemaCategory(int id);
        public List<Movie> SeacrhByNameWithCinemaAndCategory(string _Name);
        public List<Movie> ReadAllWithCinemaCategory();

        public Movie ReadOneWithCinemaCategoryActors(int id);
        void Updateviews(int id);
    }
}
