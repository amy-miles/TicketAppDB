using Microsoft.EntityFrameworkCore;
using TicketApp.Models;

namespace TicketApp.Repository
{
    public class TicketRepository : ITicketRepository
    {
        private readonly TicketContext context;
        private object ViewBag;
        

        public TicketRepository(TicketContext context)
        {
            context = context;
        }

        public IEnumerable<Ticket> GetTasks(string id)
        {
            var filters = new Filters(id);            
            IQueryable<Ticket> query = context.Tickets
                           .Include(t => t.Status);
            if (filters.HasStatus)
            {
                query = query.Where(t => t.StatusID == filters.StatusID);
            }
            var tasks = query.OrderBy(t => t.StatusID).ToList();
            return (tasks);
        }

        public IEnumerable<Ticket> GetAllTickets()
        {
            return context.Tickets.ToList();
        }

        public void AddTicket(Ticket ticket)
        {
            context.Tickets.Add(ticket);
            context.SaveChanges();
        }

        public void UpdateTicket(Ticket ticket)
        {
            context.Tickets.Update(ticket);
            context.SaveChanges();
        }

        public void DeleteTicket(int id)
        {
            var ticket = context.Tickets.Find(id);
            if (ticket != null)
            {
                context.Tickets.Remove(ticket);
                context.SaveChanges();
            }
        }

        public IEnumerable<Ticket> GetTicketsByStatus(string id)
        {
            var filters = new Filters(id);

            IQueryable<Ticket> query = context.Tickets.Include(t => t.Status);

            if (filters.HasStatus)
            {
                query = query.Where(t => t.StatusID == filters.StatusID);
            }

            var tasks = query.OrderBy(t => t.StatusID).ToList();
            return tasks;
        }

        public Ticket GetTicketById(int id)
        {
            return context.Tickets
                .Where(t => t.TicketID == id )
                .FirstOrDefault();
        }

        public void SaveChanges()
        {
            context.SaveChanges();
        }


        public IEnumerable<Status> GetStatuses()
        {
            return context.Statuses?.ToList() ?? Enumerable.Empty<Status>();
        }
    }
}
