using Syspotec.Core.entities;

namespace Syspotec.Core.Interfaces
{
    public interface ITicketRepository
    {
        public Task<List<Ticket>> Get();
        public Task<bool> Post(Ticket ticket);
        public Task<bool> Delete(int ticket_id);
        public Task<bool> Update(int  ticket_id, Ticket ticket);
        public Task<Ticket> GetByID(int ticket_id);
    }
}
