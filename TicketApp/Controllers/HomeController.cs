
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using TicketApp.Models;
using TicketApp.Repository;

namespace TicketApp.Controllers
{
    public class HomeController : Controller
    {
        //private TicketContext context;
        //public HomeController(TicketContext ctx) => context = ctx;

        private readonly ITicketRepository repository;
         public HomeController(ITicketRepository ticketRepository)
    {
        repository = ticketRepository;
       
    }


        public IActionResult Index(string id)
        {
            var filters = new Filters(id);

            ViewBag.Filters = filters;
            if (ViewBag.Statuses == null)
            {
                ViewBag.Statuses = repository.GetStatuses();
            }            

            IEnumerable<Ticket> query = repository.GetAllTickets();

            if (filters.HasStatus)
            {
                query = query.Where(t => t.StatusID == filters.StatusID);
            }

            var tasks = query.OrderBy(t => t.StatusID).ToList();
            return View(tasks);
        }

        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Statuses = repository.GetStatuses();
            var task = new Ticket { StatusID = "todo" };
            return View(task);

        }
        [HttpPost]
        public IActionResult Add(Ticket task)
        {
            if (ModelState.IsValid)
            {
                repository.AddTicket(task);                
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Statuses = repository.GetStatuses();
                return View(task);
            }
        }

        [HttpPost]
        public IActionResult Filter(string filter)
        {
            // Redirect to the Index action with the selected filter
            return RedirectToAction("Index", new { id = filter });
        }
        [HttpPost]
        public IActionResult MarkDone([FromRoute] string id, Ticket selected)
        {
            selected = repository.GetTicketById(selected.TicketID)!;          
            if (selected != null)
            {
                selected.StatusID = "done";
                repository.SaveChanges();

            }
            return RedirectToAction("Index", new { ID = id });
        }


        [HttpPost]
        public IActionResult MarkInProgress([FromRoute] string id, int TicketID)
        {
            var selected = repository.GetTicketById(TicketID);
            if (selected != null)
            {
                selected.StatusID = "inprogress";
                repository.SaveChanges();
            }

            return RedirectToAction("Index", new { id });
        }

        [HttpPost]
        public IActionResult MarkQA([FromRoute] string id, int TicketID)
        {
            var selected = repository.GetTicketById(TicketID);
            if (selected != null)
            {
                selected.StatusID = "qa";
                repository.SaveChanges();
            }

            return RedirectToAction("Index", new { id });
        }

        [HttpPost]
        public IActionResult DeleteDone(int id)
        {
            IEnumerable<Ticket> toDelete = repository.GetAllTickets();
                

            foreach (var ticket in toDelete)
            {
                if (ticket.Status.StatusName == "Done"){
                    repository.DeleteTicket(id);
                }
            }
            repository.SaveChanges();
            return RedirectToAction("Index");
        }

        [Authorize]
        public IActionResult Privacy()
        {
            return View();
        }
    }
}