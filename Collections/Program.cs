using System.Diagnostics;
using System.Numerics;

namespace Collections
{
    internal class Program
    {
        static void Main(string[] args)
        {
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
                        AddItemToList(todoList);
                        break;
                    case "c":
                        RemoveItemFromList(todoList);
                        break;
                    case "d":
                        DisplayItemDetails(todoList);
                        break;
                    case "e":
                        CompleteTodoItem(todoList);
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
                [d] Display details about item
                [e] Complete Todo item
                [q] Quit program
                """;
            Console.WriteLine(menu);
        }
        static void PrintTodoList(List<TodoItem> todoList)
        {
            if (todoList.Count == 0)
                Console.WriteLine("There are not items.");
            else
            {
                for (int i = 0; i < todoList.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. " + todoList[i].Description);
                }
            }
        }
        static void AddItemToList(List<TodoItem> todoList)
        {
            bool validInput = false;

            while (!validInput)
            {
                try
                {
                    TodoItem item = new TodoItem();
                    item.Description = PromptString("Enter a description: ");
                    item.DueDate = PromptString("Enter a date (YYYY-MM-DD): ");

                    Console.Write("Would you like to add details? (y/n) ");
                    if (Console.ReadLine().ToLower() == "y")
                    {
                        item.Details = PromptString("Enter details: ");
                    }

                    item.Completed = false;

                    todoList.Add(item);
                    validInput = true;
                }
                catch
                {
                    Console.WriteLine("Try again");
                }

            }
        }
        static void RemoveItemFromList(List<TodoItem> todoList)
        {
            bool continueRemovine = true;
            PrintTodoList(todoList);

            while (continueRemovine)
            {
                string choice = PromptString("Enter number or 'c' to cancel: ");

                if (choice == "c")
                {
                    continueRemovine = false;
                }
                else
                {
                    if (ValidateTodoSelection(choice, todoList.Count, out int itemChoice))
                    {
                        todoList.RemoveAt(itemChoice - 1);
                        continueRemovine = false;
                    }
                }
            }
        }
        static void DisplayItemDetails(List<TodoItem> todoList)
        {
            if (todoList == null || todoList.Count == 0)
            {
                Console.WriteLine("There are no todo items.");
            }
            else
            {
                bool continueShowingDetails = true;
                PrintTodoList(todoList);

                while (continueShowingDetails)
                {
                    string choice = PromptString("Enter number or 'c' to cancel: ");

                    if (choice == "c")
                    {
                        continueShowingDetails = false;
                    }
                    else
                    {
                        if (ValidateTodoSelection(choice, todoList.Count, out int itemChoice))
                        {
                            todoList[itemChoice - 1].DisplayItemDetails();
                        }
                    }
                }
            }
        }

        static void CompleteTodoItem(List<TodoItem> todoList)
        {
            if (todoList == null || todoList.Count == 0)
            {
                Console.WriteLine("There are no todo items.");
            }
            else
            {
                bool continueCompletion = true;
                PrintTodoList(todoList);

                while (continueCompletion)
                {
                    string choice = PromptString("Enter number or 'c' to cancel: ");

                    if (choice == "c")
                    {
                        continueCompletion = false;
                    }
                    else
                    {
                        if (ValidateTodoSelection(choice, todoList.Count, out int itemChoice))
                        {
                            todoList[itemChoice - 1].Completed = true;
                            continueCompletion = false;
                        }
                    }
                }
            }
        }
        static string PromptString(string message)
        {
            string input = "";
            while (string.IsNullOrWhiteSpace(input))
            {
                Console.Write(message);
                input = Console.ReadLine();
            }
            return input;
        }
        static bool ValidateTodoSelection(string choice, int todoCount, out int itemChoice)
        {
            bool parseSuccess = int.TryParse(choice, out itemChoice);
            if (!parseSuccess || itemChoice < 1 || itemChoice > todoCount)
            {
                Console.WriteLine("Not valid choice");
                parseSuccess = false;
            }
            return parseSuccess;
        }
    }
}
