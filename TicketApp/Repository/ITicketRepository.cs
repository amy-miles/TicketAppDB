using TicketApp.Models;

namespace TicketApp.Repository
{
    public interface ITicketRepository
    {
        IEnumerable<Ticket> GetAllTickets();
        IEnumerable<Status> GetStatuses();
        Ticket GetTicketById(int id);
        void AddTicket(Ticket ticket);
        void UpdateTicket(Ticket ticket);
        void DeleteTicket(int id);
        IEnumerable<Ticket> GetTicketsByStatus(string statusId);
        void SaveChanges();
        
    }
}
