using SQLite;
using TodoGenericRepo.Models;

namespace TodoGenericRepo
{
	public class TodoItem: Entity
    {
		public string Name { get; set; }
		public string Notes { get; set; }
		public bool Done { get; set; }
	}
}

