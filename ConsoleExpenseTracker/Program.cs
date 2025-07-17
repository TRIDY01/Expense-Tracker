using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

class Program
{
    static string dataFile = "expense_data.json";
    static List<Expense> expenses = new List<Expense>();

    static void Main()
    {
        LoadData();
        while (true)
        {
            Console.WriteLine("\nExpense Tracker");
            Console.WriteLine("1. Add Expense");
            Console.WriteLine("2. View Expenses");
            Console.WriteLine("3. Total Expenses");
            Console.WriteLine("4. Exit");
            Console.Write("Choose an option: ");
            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    AddExpense();
                    break;
                case "2":
                    ViewExpenses();
                    break;
                case "3":
                    ShowTotal();
                    break;
                case "4":
                    SaveData();
                    return;
                default:
                    Console.WriteLine("Invalid choice.");
                    break;
            }
        }
    }

    static void AddExpense()
    {
        Console.Write("Date (yyyy-mm-dd): ");
        DateTime date = DateTime.Parse(Console.ReadLine());

        Console.Write("Category: ");
        string category = Console.ReadLine();

        Console.Write("Note: ");
        string note = Console.ReadLine();

        Console.Write("Amount: ");
        decimal amount = decimal.Parse(Console.ReadLine());

        expenses.Add(new Expense { Date = date, Category = category, Note = note, Amount = amount });
        Console.WriteLine("Expense added successfully!");
    }

    static void ViewExpenses()
    {
        Console.WriteLine("\n--- Expense History ---");
        foreach (var e in expenses)
        {
            Console.WriteLine($"{e.Date.ToShortDateString()} | {e.Category} | {e.Note} | ₹{e.Amount:F2}");
        }
    }

    static void ShowTotal()
    {
        decimal total = 0;
        foreach (var e in expenses)
            total += e.Amount;

        Console.WriteLine($"\nTotal Expenses: ₹{total:F2}");
    }

    static void LoadData()
    {
        if (File.Exists(dataFile))
        {
            string json = File.ReadAllText(dataFile);
            expenses = JsonSerializer.Deserialize<List<Expense>>(json) ?? new List<Expense>();
        }
    }

    static void SaveData()
    {
        string json = JsonSerializer.Serialize(expenses, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(dataFile, json);
    }
}
