using System.Numerics;

namespace Collections
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // A List is similiar to an array, but it allows us to change the size of the collection at run time, rather
            // than having a fixed size like with an array.
            
            // Creating a new list is a little different than an array. We start with the List<> data type, and inside of the angle brackets
            // we insert the datatype of the list we are creating. We must use the "new" keyword, like with arrays, but we write the List<type> again
            // but this time with parentheses. It is the same as with an object's constructor.

            // The following program will use a List to manage a List of Todo Items (tasks to complete). It can add, update, or remove task
            // using the functionalities available to us with a list.

            // List<type>    name      new List<type>   parentheses
            //   |            |             |           |
            //   V            V             V           V
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

        /// <summary>
        /// Name: DisplayMainMenu
        /// Purpose: Displays the main menu to the user.
        /// </summary>
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
        /// <summary>
        /// Name: PrintTodoList
        /// Purpose: Prints every item in the todo list 
        /// </summary>
        /// <param name="todoList">The list of todo items</param>
        static void PrintTodoList(List<TodoItem> todoList)
        {
            // Lists have the Count property, which will return the number of items currently in the list.
            if (todoList.Count == 0)
                Console.WriteLine("There are no items in the To Do list.");
            else
            {
                Console.WriteLine("Description".PadRight(30) + "Completed");
                Console.WriteLine("".PadRight(40,'-'));

                // We start at 1, because we want to print the value of the index to the user 
                for (int i = 0; i < todoList.Count; i++)
                {
                    // Lists can be indexed just like arrays.
                    string listItem = $"{i + 1}. {todoList[i].Description}".PadRight(30);
                    Console.WriteLine($"{listItem}{todoList[i].Completed}");
                }
            }
        }
        #endregion

        #region Add / Remove 
        /// <summary>
        /// Name: AddTodoItem
        /// Purpose: Adds a new TodoItem to the provided list.
        /// </summary>
        /// <param name="todoList">The list of todo items</param>
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
        /// <summary>
        /// Name: RemoveItemFromTodoList
        /// Purpose: Removes an item from the provided todo list.
        /// </summary>
        /// <param name="todoList">The list of todo items</param>
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
                    if (ValidateTodoSelection(todoList.Count, out int itemChoice))
                    {
                        todoList.RemoveAt(itemChoice - 1);
                        continueRemoveOption = false;
                    }
                }
            }
        }

        #endregion

        #region Todo Item Methods
        /// <summary>
        /// Name: CompleteTodoItem
        /// Purpose: Completes a Todo Item.
        /// </summary>
        /// <param name="todoList">The list of todo items</param>
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
                    if (ValidateTodoSelection(todoList.Count, out int itemChoice))
                    {
                        todoList[itemChoice - 1].Completed = true;
                        continueCompleteOption = false;
                    }
                }
            }
        }
        /// <summary>
        /// Name: DisplayItemDetails
        /// Purpose: 
        /// </summary>
        /// <param name="todoList">The list of todo items</param>
        static void DisplayItemDetails(List<TodoItem> todoList)
        {
            if (todoList.Count == 0)
                Console.WriteLine("There are no items in the To Do list.");
            else
            {
                Console.Write("For which item would you like the details? ");
                if (ValidateTodoSelection(todoList.Count, out int itemChoice))
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

        #region Utils
        /// <summary>
        /// Name: ValidateTodoSelection
        /// Purpose: Makes sure that the option that a user enters
        /// when selection an option from the todo list is valid.
        /// </summary>
        /// <param name="todoCount">The number of items that are in the list</param>
        /// <param name="itemChoice">The user's choice converted to an int (out param)</param>
        /// <returns>Boolean indicating if the choice is valid.</returns>
        static bool ValidateTodoSelection(int todoCount, out int itemChoice)
        {
            string choice = Console.ReadLine();
            bool parseSuccess = int.TryParse(choice, out itemChoice);
            if (!parseSuccess || itemChoice < 1 || itemChoice > todoCount)
            {
                Console.WriteLine("That is not a valid choice. Try again.");
                parseSuccess = false;
            }
            return parseSuccess;
        }
        #endregion
    }
}
