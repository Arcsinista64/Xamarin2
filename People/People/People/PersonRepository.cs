using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using People.Models;
using SQLite;

namespace People
{
    public class PersonRepository
    {
        public string StatusMessage { get; set; }

        private SQLiteAsyncConnection conn;

        public PersonRepository(string dbPath)
        {
            conn = new SQLiteAsyncConnection(dbPath);
            conn.CreateTableAsync<Person>();
        }

        public async Task AddNewPerson(string name)
        {
            int result = 0;
            try
            {
                //basic validation to ensure a name was entered
                if (string.IsNullOrEmpty(name))
                    throw new Exception("Valid name required");

                result = await conn.InsertAsync(new Person { Name = name });

                StatusMessage = string.Format("{0} record(s) added [Name: {1})", result, name);
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed to add {0}. Error: {1}", name, ex.Message);
            }

        }

        public async Task<List<Person>> GetAllPeople()
        {
            try
            {
                return await conn.Table<Person>().ToListAsync();
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Hubo un error al cargar la información. {0}", ex.Message);
            }

            return new List<Person>();
        }
    }
}