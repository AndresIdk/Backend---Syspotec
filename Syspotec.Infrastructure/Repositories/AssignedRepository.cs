using System.Data;
using System.Data.SqlClient;
using Syspotec.Core.DTOs;
using Syspotec.Core.entities;
using Syspotec.Core.Interfaces;
using Syspotec.Infrastructure.Data;

namespace Syspotec.Infrastructure.Repositories
{
    public class AssignedRepository : IAssignedRepository
    {
        DB db = new DB();

        public async Task<List<Assigned>> Get()
        {
            var assigneds = new List<Assigned>();
            try
            {
                using (var sql = new SqlConnection(db.DBConnection()))
                {
                    using (var cmd = new SqlCommand("showAssigned", sql))
                    {

                        await sql.OpenAsync();
                        cmd.CommandType = CommandType.StoredProcedure;

                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                var assigned = new Assigned();
                                assigned.NIT = (int) reader["nit"];
                                assigned.Id_ticket = (int) reader["id_ticket"];
                                assigned.ID_status = (int) reader["id_status"];
                                assigned.Date = (DateTime) reader["date"];
                                assigneds.Add(assigned);
                            }
                        }
                    }
                }
                return assigneds;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return assigneds;
            }
        }

        public async Task<Assigned> GetByID(int assigned_id)
        {
            var assigned = new Assigned();
            try
            {
                using (var sql = new SqlConnection(db.DBConnection()))
                {
                    using (var cmd = new SqlCommand("showAssignedById", sql))
                    {
                        await sql.OpenAsync();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("id", assigned_id);
                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                assigned.NIT = (int)reader["nit"];
                                assigned.Id_ticket = (int)reader["id_ticket"];
                                assigned.ID_status = (int)reader["id_status"];
                                assigned.Date = (DateTime)reader["date"];
                            }
                        }
                    }
                }
                return assigned;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return assigned;
            }
        }

        public async Task<bool> Post(AssignedDTO assigned, DateTime date)
        {
            try
            {
                using (var sql = new SqlConnection(db.DBConnection()))
                {
                    using (var cmd = new SqlCommand("insertAssigned", sql))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("nit", assigned.NIT);
                        cmd.Parameters.AddWithValue("id_ticket", assigned.Id_ticket);
                        cmd.Parameters.AddWithValue("id_status", assigned.ID_status);
                        cmd.Parameters.AddWithValue("date", date);
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

        public async Task<bool> Delete(int assigned_id)
        {
            try
            {
                using (var sql = new SqlConnection(db.DBConnection()))
                {
                    using (var cmd = new SqlCommand("deleteAssigned", sql))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("id", assigned_id);
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

        public async Task<bool> Update(int assigned_id, Assigned assigned, DateTime date)
        {
            try
            {
                using (var sql = new SqlConnection(db.DBConnection()))
                {
                    using (var cmd = new SqlCommand("updateAssigned", sql))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("id", assigned_id);
                        cmd.Parameters.AddWithValue("nit", assigned.NIT);
                        cmd.Parameters.AddWithValue("id_ticket", assigned.Id_ticket);
                        cmd.Parameters.AddWithValue("id_status", assigned.ID_status);
                        cmd.Parameters.AddWithValue("date", date);
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
    }
}
