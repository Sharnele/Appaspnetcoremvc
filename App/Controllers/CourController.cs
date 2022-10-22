using App.Data;
using App.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace App.Controllers
{
    public class CourController : Controller
    {
        private readonly Appcontext appcontext;

        public CourController(Appcontext appcontext)
        {
            this.appcontext = appcontext;
        }
        // affiche la liste des cours de la base de donnees
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var cours = await appcontext.cours.ToListAsync();
            return View(cours);


        }
        //initialise 
        [HttpGet]

        public IActionResult Add()
        {
           
            return View();



        }
        // ajouter un cour dans la base de donnees
        [HttpPost]
        public async Task<IActionResult> Add(Cour addcoursrequest)
        {
            var cour = new Cour();
            {
                cour.Id = Guid.NewGuid();
                cour.Nom = addcoursrequest.Nom;

            }

            await appcontext.cours.AddAsync(cour);
            await appcontext.SaveChangesAsync();
            return RedirectToAction("Index");



        }
        // recuperer l'id afin de selection  l'enregistrement a modifier
        [HttpGet]
        public async Task<IActionResult> View(Guid id)
        {
            var cour = await appcontext.cours.FirstOrDefaultAsync(x => x.Id == id);
            if (cour != null)
            {
                var viewmodel = new Cour()
                {
                    Id = cour.Id,
                    Nom = cour.Nom
                };

                return await Task.Run(() => View("View", viewmodel));
            }
            return RedirectToAction("Index");
        }
        //modifier un cour

        [HttpPost]
        public async Task<IActionResult> View(Cour model)
        {
            var cour = await appcontext.cours.FindAsync(model.Id);
            if (cour != null)
            {
                cour.Id = model.Id;
                cour.Nom = model.Nom;

                await appcontext.SaveChangesAsync();
                return RedirectToAction("Index");

            }
            return RedirectToAction("Index");
        }

        [HttpPost]

        public async Task<IActionResult> Delete(Cour model)
        {
            var cour = await appcontext.cours.FindAsync(model.Id);
            if (cour != null)
            {
                appcontext.cours.Remove(cour);
                await appcontext.SaveChangesAsync();
                return RedirectToAction("Index");
            }


            return RedirectToAction("Index");


        }

    }
}
