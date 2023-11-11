using System.Data.SqlClient;
using System.Data;
using Syspotec.Core.entities;
using Syspotec.Core.Interfaces;
using Syspotec.Infrastructure.Data;
using Syspotec.Core.DTOs;
using System.Net.Sockets;

namespace Syspotec.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        DB db = new DB();
        public async Task<List<UserDTO>> Get()
        {
            var users = new List<UserDTO>();
            try
            {
                using (var sql = new SqlConnection(db.DBConnection()))
                {
                    using (var cmd = new SqlCommand("showUsers", sql))
                    {

                        await sql.OpenAsync();
                        cmd.CommandType = CommandType.StoredProcedure;

                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                var user = new UserDTO();
                                user.NIT = (int) reader["nit"];
                                user.First_name = (string)reader["first_name"];
                                user.Last_name = (string)reader["last_name"];
                                users.Add(user);
                            }
                        }
                    }
                }
                return users;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return users;
            }
        }

        public async Task<User> GetByID(int user_id)
        {
            var user = new User();
            try
            {
                using (var sql = new SqlConnection(db.DBConnection()))
                {
                    using (var cmd = new SqlCommand("showUserById", sql))
                    {
                        await sql.OpenAsync();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("id", user_id);
                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                user.NIT = (int)reader["nit"];
                                user.First_name = (string)reader["first_name"];
                                user.Last_name = (string)reader["last_name"];
                                user.Email = (string)reader["email"];
                                user.Password = (string)reader["password"];
                            }
                        }
                    }
                }
                return user;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return user;
            }
        }

        public async Task<bool> Post(User user, string pwd_hash)
        {
            try
            {
                using (var sql = new SqlConnection(db.DBConnection()))
                {
                    using (var cmd = new SqlCommand("insertUser", sql))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("nit", user.NIT);
                        cmd.Parameters.AddWithValue("Fname", user.First_name);
                        cmd.Parameters.AddWithValue("Lname", user.Last_name);
                        cmd.Parameters.AddWithValue("email", user.Email);
                        cmd.Parameters.AddWithValue("pwd", pwd_hash);
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
        public async Task<bool> Delete(int user_id)
        {
            try
            {
                using (var sql = new SqlConnection(db.DBConnection()))
                {
                    using (var cmd = new SqlCommand("deleteUser", sql))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("id", user_id);
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

        public async Task<bool> Update(int user_id, User user)
        {
            try
            {
                using (var sql = new SqlConnection(db.DBConnection()))
                {
                    using (var cmd = new SqlCommand("updateTicket", sql))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("id", user_id);
                        cmd.Parameters.AddWithValue("Fname", user.First_name);
                        cmd.Parameters.AddWithValue("Lname", user.Last_name);
                        cmd.Parameters.AddWithValue("email", user.Email);
                        cmd.Parameters.AddWithValue("pwd", user.Password);
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
