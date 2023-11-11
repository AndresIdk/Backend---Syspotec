using System.Data.SqlClient;
using System.Data;
using Syspotec.Core.entities;
using Syspotec.Core.Interfaces;
using Syspotec.Infrastructure.Data;
using System.Net.Sockets;

namespace Syspotec.Infrastructure.Repositories
{
    public class StateRepository : IStateRepository
    {
        DB db = new DB();
        public async Task<List<State>> Get()
        {
            var states = new List<State>();
            try
            {
                using (var sql = new SqlConnection(db.DBConnection()))
                {
                    using (var cmd = new SqlCommand("showStates", sql))
                    {

                        await sql.OpenAsync();
                        cmd.CommandType = CommandType.StoredProcedure;

                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                var state = new State();
                                state.Id = (int)reader["id"];
                                state.Name = (string)reader["name"];
                                states.Add(state);
                            }
                        }
                    }
                }
                return states;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return states;
            }
        }

        public async Task<State> GetByID(int state_id)
        {
            var state = new State();
            try
            {
                using (var sql = new SqlConnection(db.DBConnection()))
                {
                    using (var cmd = new SqlCommand("showStateById", sql))
                    {
                        await sql.OpenAsync();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("id", state_id);
                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                state.Id = (int)reader["id"];
                                state.Name = (string)reader["name"];
                            }
                        }
                    }
                }
                return state;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return state;
            }
        }

        public async Task<bool> Post(State state)
        {
            try
            {
                using (var sql = new SqlConnection(db.DBConnection()))
                {
                    using (var cmd = new SqlCommand("insertState", sql))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("name", state.Name);
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
        public async Task<bool> Delete(int state_id)
        {
            try
            {
                using (var sql = new SqlConnection(db.DBConnection()))
                {
                    using (var cmd = new SqlCommand("deleteState", sql))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("id", state_id);
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
        public async Task<bool> Update(int state_id, State state)
        {
            try
            {
                using (var sql = new SqlConnection(db.DBConnection()))
                {
                    using (var cmd = new SqlCommand("updateState", sql))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("id", state_id);
                        cmd.Parameters.AddWithValue("name", state.Name);
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
