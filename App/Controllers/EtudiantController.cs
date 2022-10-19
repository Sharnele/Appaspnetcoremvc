using App.Data;
using App.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;

namespace App.Controllers
{

    public class EtudiantController : Controller
    {
        private readonly Appcontext appcontext;

        public EtudiantController(Appcontext appcontext)
        {
            this.appcontext = appcontext;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var etudiants = await appcontext.etudiants.ToListAsync();
            return View(etudiants);


        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();

        }

        [HttpPost]

        public async Task<IActionResult> Add(AddEtudiantviewmodel addEtudiantRequest)
        {
            var etudiant = new Etudiant();
            {
                etudiant.Id = Guid.NewGuid();
                etudiant.Nom = addEtudiantRequest.Nom;

            }

            await appcontext.etudiants.AddAsync(etudiant);
            await appcontext.SaveChangesAsync();
            return RedirectToAction("Index");


        }


        [HttpPost]

        public async Task<IActionResult> Delete(updateEtudiant model)
        {
            var etudiant = await appcontext.etudiants.FindAsync(model.Id);
          if(etudiant != null)
            {
                appcontext.etudiants.Remove(etudiant);
                await appcontext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            
           
            return RedirectToAction("Index");


        }


        [HttpGet]
        public async Task<IActionResult> View(Guid id)
        {
            var etudiant = await appcontext.etudiants.FirstOrDefaultAsync(x => x.Id == id);
            if (etudiant != null)
            {
                var viewmodel = new updateEtudiant()
                {
                    Id = etudiant.Id,
                    Nom = etudiant.Nom
                };

                return await Task.Run(() => View("View",viewmodel));
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> View(updateEtudiant model)
        {
            var etudiant = await appcontext.etudiants.FindAsync(model.Id);
            if(etudiant != null)
            {
                etudiant.Id= model.Id ;
                etudiant.Nom = model.Nom ;
            
            await appcontext.SaveChangesAsync();
            return RedirectToAction("Index");

            }
            return RedirectToAction("Index");
        }

    }

}
