using Syspotec.Core.entities;
using Syspotec.Core.Interfaces;

namespace Syspotec.Core.Services
{
    public class TicketService : ITicketService
    {
        private readonly ITicketRepository _ticketRepository;

        public TicketService(ITicketRepository ticketRepository)
        {
            _ticketRepository = ticketRepository;
        }
        public Task<List<Ticket>> Get()
        {
            return  _ticketRepository.Get();
        }
        public Task<Ticket> GetByID(int ticket_id)
        {
            return _ticketRepository.GetByID(ticket_id);
        }
        public Task<int> Post(Ticket ticket)
        {
            return _ticketRepository.Post(ticket);
        }
        public Task<bool> Delete(int ticket_id)
        {
            return _ticketRepository.Delete(ticket_id);
        }
        public Task<bool> Update(int ticket_id, Ticket ticket)
        {
            return _ticketRepository.Update(ticket_id, ticket);
        }
    }
}
