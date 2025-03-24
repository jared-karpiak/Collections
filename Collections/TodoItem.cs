namespace Collections
{
    public class TodoItem
    {
        public string Description { get; private set; }
        public bool Completed { get; set; }
        public string Details { get; private set; }
        public TodoItem(string description)
        {
            Description = description;
            Completed = false;
            Details = "";
        }
        public void AddDetails()
        {
            Console.WriteLine("Enter details about the task\n" +
                $"{Description}:");
            string details = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(details))
                Details = details;
        }
    }
}
