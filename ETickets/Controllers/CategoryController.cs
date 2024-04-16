using ETickets.IRepositry;
using ETickets.Models;
using ETickets.Repositry;
using ETickets.Services;
using ETickets.ViewModels.Category;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;



namespace ETickets.Controllers
{
    [Authorize(Roles ="Admin")]
    public class CategoryController : Controller
    {
        ICategoryRepositry categoryRepositry;
       
        public CategoryController(ICategoryRepositry categoryRepositry)
        {
            this.categoryRepositry= categoryRepositry;          
        }
    


        public IActionResult Index()
        {
            List<CreateEditViewModel> categories = categoryRepositry.GetAll().Select(MapRepositry.MapToCreateEditViewModel).ToList();
            return View(categories);
        }

        [HttpGet]
        public ActionResult Create() {

            return View();
        }
        
        [HttpPost]
        public ActionResult Create(string Name) {
            if (ModelState.IsValid)
            {
               categoryRepositry.Create(new Category { Name = Name });
                 return RedirectToAction("Index");
            }

            return View();






        }

        [HttpGet]
        public ActionResult Edit(int id)
        {

            CreateEditViewModel categoryVm = MapRepositry.MapToCreateEditViewModel(categoryRepositry.GetById(id));

            return View(categoryVm);
        }

        [HttpPost]

        public IActionResult Edit(CreateEditViewModel categoryVm)
        {
            
            Category category = MapRepositry.MapToCategory(categoryVm);
                categoryRepositry.Update(category);

                return RedirectToAction("Index");
            
        }



    }
}
