using System.Data;
using System.Data.SqlClient;
using Syspotec.Core.entities;
using Syspotec.Core.Interfaces;
using Syspotec.Infrastructure.Data;

namespace Syspotec.Infrastructure.Repositories
{
    public class TicketRepository : ITicketRepository
    {
        DB db = new DB(); 
        public async Task<List<Ticket>> Get()
        {
            var tickets = new List<Ticket>();
            try
            {
                using (var sql = new SqlConnection(db.DBConnection()))
                {
                    using (var cmd = new SqlCommand("showTickets", sql))
                    {

                        await sql.OpenAsync();
                        cmd.CommandType = CommandType.StoredProcedure;

                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                var ticket = new Ticket();
                                ticket.Id = (int)reader["id_ticket"];
                                ticket.Description = ((string)reader["description"]).Trim();
                                ticket.Priority = ((string)reader["priority"]).Trim();
                                tickets.Add(ticket);
                            }
                        }
                    }
                }
                return tickets;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return tickets;
            }
        }

        public async Task<Ticket> GetByID(int ticket_id)
        {
            var ticket = new Ticket();
            try
            {
                using (var sql = new SqlConnection(db.DBConnection()))
                {
                    using (var cmd = new SqlCommand("showTicketById", sql))
                    {
                        await sql.OpenAsync();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("id", ticket_id);
                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                ticket.Id = (int)reader["id_ticket"];
                                ticket.Description = ((string)reader["description"]).Trim();
                                ticket.Priority = ((string)reader["priority"]).Trim();
                            }
                        }
                    }
                }
                return ticket;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return ticket;
            }
        }
        public async Task<bool> Post(Ticket ticket)
        {
            try
            {
                using (var sql = new SqlConnection(db.DBConnection()))
                {
                    using (var cmd = new SqlCommand("insertTicket", sql))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("Description", ticket.Description);
                        cmd.Parameters.AddWithValue("Priority", ticket.Priority);
                        await sql.OpenAsync();
                        await cmd.ExecuteNonQueryAsync();
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }
        public async Task<bool> Delete(int ticket_id)
        {
            try
            {
                using(var sql =  new SqlConnection(db.DBConnection()))
                {
                    using(var cmd = new SqlCommand("deleteTicket", sql))
                    {
                        cmd.CommandType= CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("id", ticket_id);
                        await sql.OpenAsync();
                        await cmd.ExecuteNonQueryAsync();
                    }
                }
                return true;
            }catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }

        public async Task<bool> Update(int ticket_id, Ticket ticket)
        {
            try
            {
                using (var sql = new SqlConnection(db.DBConnection()))
                {
                    using (var cmd = new SqlCommand("updateTicket", sql))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("id", ticket_id);
                        cmd.Parameters.AddWithValue("description", ticket.Description);
                        cmd.Parameters.AddWithValue("priority", ticket.Priority);
                        await sql.OpenAsync();
                        await cmd.ExecuteNonQueryAsync();
                    }
                }
                return true;

            }catch(Exception ex) { 
        
                Console.WriteLine(ex.ToString());
                return false;
            }
        }

    }
}
