using System.Numerics;

namespace Collections
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // A List is similiar to an array 
            List<TodoItem> todoList = new List<TodoItem>();
            bool programRunning = true;

            while (programRunning)
            {
                DisplayMainMenu();
                Console.Write("Enter a choice: ");
                string choice = Console.ReadLine().ToLower();
                switch (choice)
                {
                    case "a":
                        PrintTodoList(todoList);
                        break;
                    case "b":
                        AddTodoItem(todoList);
                        break;
                    case "c":
                        PrintTodoList(todoList);
                        RemoveItemFromTodoList(todoList);
                        break;
                    case "d":
                        PrintTodoList(todoList);
                        DisplayItemDetails(todoList);
                        break;
                    case "e":
                        PrintTodoList(todoList);
                        CompleteTodoItem(todoList);
                        break;
                    case "q":
                        programRunning = false;
                        break;
                }
            }
        }

        #region UI
        static void DisplayMainMenu()
        {
            string menu = """
                Select from the following options:
                [a] Print To-Do List
                [b] Add new item to To-Do List
                [c] Remove item from To-Do List
                [d] Display details about item
                [e] Complete Todo item
                [q] Quit program
                """;
            Console.WriteLine(menu);
        }
        static void PrintTodoList(List<TodoItem> todoList)
        {
            if (todoList.Count == 0)
                Console.WriteLine("There are no items in the To Do list.");
            else
            {
                Console.WriteLine("Description".PadRight(30) + "Completed");
                Console.WriteLine("".PadRight(40,'-'));
                for (int i = 1; i <= todoList.Count; i++)
                {
                    string listItem = $"{i}. {todoList[i].Description}".PadRight(30);
                    Console.WriteLine($"{listItem}{todoList[i - 1].Completed}");
                }
            }
        }
        #endregion

        #region Add / Remove 
        static void AddTodoItem(List<TodoItem> todoList)
        {
            bool validInput = false;

            while (!validInput)
            {
                Console.Write("Provide a description for the new item: ");
                string description = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(description))
                    Console.WriteLine("Cannot enter an empty value.");
                else if (description.Length > 20)
                    Console.WriteLine("Description must be 20 characters or less.")
                else
                {
                    TodoItem item = new TodoItem(description);
                    todoList.Add(item);
                    validInput = true;
                }
            }
        }
        static void RemoveItemFromTodoList(List<TodoItem> todoList)
        {
            bool continueRemoveOption = true;

            while (continueRemoveOption)
            {
                Console.Write("Enter the number of the item you wish to remove (enter 'c' to cancel): ");
                string choice = Console.ReadLine().ToLower();
                if (choice == "c")
                {
                    continueRemoveOption = false;
                }
                else
                {
                    bool parseSuccess = int.TryParse(choice, out int itemChoice);
                    if (!parseSuccess || itemChoice < 1 || itemChoice > todoList.Count)
                    {
                        Console.WriteLine("That is not a valid choice. Try again.");
                    }
                    else
                    {
                        todoList.RemoveAt(itemChoice - 1);
                        continueRemoveOption = false;
                    }
                }
            }
        }

        #endregion

        #region Todo Item Methods

        static void CompleteTodoItem(List<TodoItem> todoList)
        {
            bool continueCompleteOption = true;

            while (continueCompleteOption)
            {
                Console.Write("Which item would you like to complete? (enter 'c' to cancel): ");
                string choice = Console.ReadLine().ToLower();
                if (choice == "c")
                {
                    continueCompleteOption = false;
                }
                else
                {
                    bool parseSuccess = int.TryParse(choice, out int itemChoice);
                    if (!parseSuccess || itemChoice < 1 || itemChoice > todoList.Count)
                    {
                        Console.WriteLine("That is not a valid choice. Try again.");
                    }
                    else
                    {
                        todoList[itemChoice - 1].Completed = true;
                        continueCompleteOption = false;
                    }
                }
            }
        }
        static void DisplayItemDetails(List<TodoItem> todoList)
        {
            if (todoList.Count == 0)
                Console.WriteLine("There are no items in the To Do list.");
            else
            {
                Console.Write("For which item would you like the details? ");
                string choice = Console.ReadLine();

                bool parseSuccess = int.TryParse(choice, out int itemChoice);
                if (!parseSuccess || itemChoice < 1 || itemChoice > todoList.Count)
                {
                    Console.WriteLine("That is not a valid choice. Try again.");
                }
                else
                {
                    TodoItem item = todoList[itemChoice - 1];
                    if (string.IsNullOrWhiteSpace(item.Details))
                    {
                        Console.Write("There are no details for that item. Would you like to add some (y/n)? ");
                        string addDetailsChoice = Console.ReadLine();
                        if (addDetailsChoice == "y")
                        {
                            item.AddDetails();
                        }
                    }
                    else
                    {
                        Console.WriteLine(item.Details);
                    }
                }
            }
        }
        #endregion
    }
}
