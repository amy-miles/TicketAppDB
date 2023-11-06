namespace TicketApp.Models
{
    public class Filters
    {
        public Filters(string filterstring)
        {
            Filterstring = filterstring ?? "all";
            StatusID = Filterstring;
        }
        public string Filterstring { get; }
        public string StatusID { get; }

        public bool HasStatus => StatusID.ToLower() != "all";
    }
}
