using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace TodoGenericRepo.Data
{
    public class TotoRepository : BaseRepository<TodoItem>
    {
        public TotoRepository() : base(App.DbConnectionAsync,App.DbConnection)
        {
            
        }
    }
}
