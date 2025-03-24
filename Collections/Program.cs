namespace Collections
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<string> todoList = new List<string>();
            bool programRunning = true;

            while (programRunning)
            {
                DisplayMainMenu();
                string choice = PromptForChoice();
                switch (choice)
                {
                    case "a":

                        break;
                    case "b":
                        break;
                    case "c":
                        break;
                    case "q":
                        programRunning = false;
                        break;

                }

                
            }
        }
        static void DisplayMainMenu()
        {
            string menu = """
                Select from the following options:
                [a] Print To-Do List
                [b] Add new item to To-Do List
                [c] Remove item from To-Do List
                [q] Quit program
                """;
            Console.WriteLine(menu);
        }
        static string PromptForChoice()
        {
            Console.Write("Enter a choice: ");
            return Console.ReadLine().ToLower();
        }
        static void 
    }
}
