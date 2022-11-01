using App.Models;
using Microsoft.AspNetCore.Mvc;
using App.Data;
using Microsoft.EntityFrameworkCore;

namespace App.Controllers
{
    public class ParticipantController : Controller
    {
        private readonly Appcontext appcontext;
     

        public ParticipantController(Appcontext appcontext)
        {
            this.appcontext = appcontext;
        }

        // affiche la liste des participant de la base de donnees
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var participants = await appcontext.participants.ToListAsync();
            return View(participants);


        }
        //initialise l'ajout
        [HttpGet]

        public IActionResult Add()
        {

            return View();



        }
        // ajouter un participant dans la base de donnees
        [HttpPost]
        public async Task<IActionResult> Add(Participant addparticipantrequest)
        {
            var participant = new Participant();
            {
                participant.Id = Guid.NewGuid();
                participant.etudiant = addparticipantrequest.etudiant;
                participant.cour = addparticipantrequest.cour;
                participant.datep = addparticipantrequest.datep;

            }

            await appcontext.participants.AddAsync(participant);
            await appcontext.SaveChangesAsync();
            return RedirectToAction("Index");



        }
        // recuperer l'id afin de selectionner  l'enregistrement a modifier
        [HttpGet]
        public async Task<IActionResult> View(Guid id)
        {
            var particpant = await appcontext.participants.FirstOrDefaultAsync(x => x.Id == id);
            if (particpant != null)
            {
                var viewmodel = new Participant()
                {
                    Id = particpant.Id,
                    etudiant = particpant.etudiant,
                    cour=particpant.cour,  
                    datep = particpant.datep   
                };

                return await Task.Run(() => View("View", viewmodel));
            }
            return RedirectToAction("Index");
        }
        //modifier un participant

        [HttpPost]
        public async Task<IActionResult> View(Participant model)
        {
            var participant = await appcontext.participants.FindAsync(model.Id);
            if (participant != null)
            {
                participant.Id = model.Id;
                participant.etudiant = model.etudiant;
                participant.cour = model.cour;
                participant.datep = model.datep;
         

                await appcontext.SaveChangesAsync();
                return RedirectToAction("Index");

            }
            return RedirectToAction("Index");
        }

        [HttpPost]

        public async Task<IActionResult> Delete(Participant model)
        {
            var particiipant = await appcontext.participants.FindAsync(model.Id);
            if (particiipant != null)
            {
                appcontext.participants.Remove(particiipant);
                await appcontext.SaveChangesAsync();
                return RedirectToAction("Index");
            }


            return RedirectToAction("Index");


        }
    }
}
