using System;
using System.Collections.Generic;
using System.IO;

// Structure to store employee details with fields for ID, Name, Position, and Salary
struct Employee
{
    public int Id { get; set; }    
    public string Name { get; set; }       
    public string Position { get; set; }   
    public double Salary { get; set; }    

    // Constructor to initialize an Employee object
    public Employee(int id, string name, string position, double salary)
    {
        Id = id;
        Name = name;
        Position = position;
        Salary = salary;
    }

    // Override ToString method to display employee information in a readable format
    public override string ToString()
    {
        return $"ID: {Id}, Name: {Name}, Position: {Position}, Salary: {Salary:C}";
    }
}

// Class that handles operations related to employee management
class EmployeeManager
{
    private List<Employee> employees = new List<Employee>(); 
    private const string FilePath = "employees.txt";          

    // Adds a new employee by prompting user for input
    public void AddEmployee()
    {
        Console.Write("Enter ID: ");
        #nullable disable
        int id = int.Parse(Console.ReadLine());
        Console.Write("Enter Name: ");
        string name = Console.ReadLine();
        Console.Write("Enter Position: ");
        string position = Console.ReadLine();
        Console.Write("Enter Salary: ");
        double salary = double.Parse(Console.ReadLine());

        employees.Add(new Employee(id, name, position, salary)); 
        Console.WriteLine("Employee Added Successfully!\n");
    }

    // Displays all employees currently in the list
    public void DisplayEmployees()
    {
        if (employees.Count == 0)
        {
            Console.WriteLine("No employees found.");
            return;
        }

        foreach (var emp in employees)
        {
            Console.WriteLine(emp); 
        }
    }

    // Allows editing an existing employee's data by searching via ID
    public void EditEmployee()
    {
        Console.Write("Enter Employee ID to edit: ");
        int id = int.Parse(Console.ReadLine());
        int index = employees.FindIndex(e => e.Id == id); 

        if (index == -1)
        {
            Console.WriteLine("Employee not found.");
            return;
        }

        Employee emp = employees[index];

        Console.Write("Enter New Name (leave blank to keep current): ");
        string name = Console.ReadLine();
        if (!string.IsNullOrEmpty(name)) emp.Name = name;

        Console.Write("Enter New Position (leave blank to keep current): ");
        string position = Console.ReadLine();
        if (!string.IsNullOrEmpty(position)) emp.Position = position;

        Console.Write("Enter New Salary (leave blank to keep current): ");
        string salaryInput = Console.ReadLine();
        if (!string.IsNullOrEmpty(salaryInput)) emp.Salary = double.Parse(salaryInput);

        employees[index] = emp; 

        Console.WriteLine("Employee Updated Successfully!\n");
    }

    // Saves all employee records to a text file
    public void SaveToFile()
    {
        using (StreamWriter writer = new StreamWriter(FilePath))
        {
            foreach (var emp in employees)
            {
                // Save each employee as comma-separated values
                writer.WriteLine($"{emp.Id},{emp.Name},{emp.Position},{emp.Salary}");
            }
        }
        Console.WriteLine("Employees saved to file.\n");
    }

    // Loads employee records from the file and populates the employee list
    public void LoadFromFile()
    {
        if (File.Exists(FilePath))
        {
            employees.Clear(); 
            string[] lines = File.ReadAllLines(FilePath);
            foreach (string line in lines)
            {
                string[] parts = line.Split(',');
                // Reconstruct and add employee from file data
                employees.Add(new Employee(int.Parse(parts[0]), parts[1], parts[2], double.Parse(parts[3])));
            }
            Console.WriteLine("Employees loaded from file.\n");
        }
    }
}

// Main program that acts as the console interface for the user
class Program
{
    static void Main()
    {
        EmployeeManager manager = new EmployeeManager();
        manager.LoadFromFile(); 

        while (true)
        {
            // Display menu options
            Console.WriteLine("1. Add Employee");
            Console.WriteLine("2. Display Employees");
            Console.WriteLine("3. Edit Employee");
            Console.WriteLine("4. Save Employees to File");
            Console.WriteLine("5. Exit");
            Console.Write("Choose an option: ");

            int choice = int.Parse(Console.ReadLine());
            Console.WriteLine();

            // Handle user's menu choice
            switch (choice)
            {
                case 1:
                    manager.AddEmployee();
                    break;
                case 2:
                    manager.DisplayEmployees();
                    break;
                case 3:
                    manager.EditEmployee();
                    break;
                case 4:
                    manager.SaveToFile();
                    break;
                case 5:
                    return; 
                default:
                    Console.WriteLine("Invalid choice, please try again.");
                    break;
            }
            Console.WriteLine();
        }
    }
}
