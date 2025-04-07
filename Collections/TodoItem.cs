using System.Text;

namespace Collections
{
    public class TodoItem
    {
        #region Properties
        private string _description;
        private string _details;
        private string _dueDate;
        private bool _completed;

        public string Description
        {
            get
            {
                return _description;
            }
            set
            {
                if (value.Length < 10 || value.Length > 30)
                    throw new Exception("Description length invalid.");

                _description = value;
            }
        }

        public string Details
        {
            get
            {
                return _details;
            }
            set
            {
                _details = value;
            }
        }

        public string DueDate
        {
            get
            {
                return _dueDate;
            }
            set
            {
                _dueDate = value;
            }
        }

        public bool Completed
        {
            get
            {
                return _completed;
            }
            set
            {
                _completed = value;
            }
        }

        #endregion

        public TodoItem() { }

        public TodoItem(string description, string dueDate, string details = "")
        {
            Description = description;
            DueDate = dueDate;
            Details = details;
            Completed = false;
        }

        public void DisplayItemDetails()
        {
            // showing all item properties in a well formatted way
            // If Details is null or empty, call AddItemDetails()

            StringBuilder builder = new StringBuilder();
            builder.AppendLine();
            builder.AppendLine($"Description:".PadRight(20) + $"{Description}");

            if (string.IsNullOrWhiteSpace(Details))
            {
                Console.Write("There are no details, do you wish to add them? (y/n)  ");
                string choice = Console.ReadLine().Trim().ToLower();

                if (choice == "y")
                {
                    Console.WriteLine("Please enter details below: ");
                    Details = Console.ReadLine();
                }
            }

            builder.AppendLine($"Details:".PadRight(20) + $"{Details}");
            builder.AppendLine($"Due Date:".PadRight(20) + $"{DueDate}");
            builder.AppendLine($"Completed:".PadRight(20) + $"{Completed}");
            builder.AppendLine();

            Console.WriteLine(builder.ToString());
        }
    }
}
