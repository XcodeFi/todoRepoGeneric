using SQLite;
using TodoGenericRepo.Models;

namespace TodoGenericRepo
{
    public class TodoItem : Entity
    {
        string _Name;

        public string Name
        {
            get { return _Name; }
            set
            {
                if (value.Equals(_Name))
                {
                    return;
                }
                _Name = value;
                OnPropertyChanged("Name");
            }
        }
        string _Notes;

        public string Notes
        {
            get { return _Notes; }
            set
            {
                if (value.Equals(_Notes))
                {
                    return;
                }
                _Notes = value;
                OnPropertyChanged("Notes");
            }
        }
        public bool Done { get; set; }
    }
}

