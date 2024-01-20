using System.Drawing;
using Classes;

class Program
{
    public class InvalidOptionException : Exception
    {
        public InvalidOptionException()
            : base("Invalid option. Must be between 0 and 6 inclusive.") { }
    }

    static void readCustomerData(List<Customer> CustomerList, List<PointCard> PointCardsList)
    {
        string filePath = "C:\\Codes For Days\\C#\\customers.csv";
        string[] lines = File.ReadAllLines(filePath);

        bool firstLineSkipped = false;
        foreach (string line in lines)
        {
            if (!firstLineSkipped)
            {
                firstLineSkipped = true;
                continue;
            }

            string[] data = line.Split(',');

            if (data.Length == 6)
            {
                string Name = data[0].Trim();
                int MemberID = Convert.ToInt32(data[1].Trim());
                DateTime DOB = Convert.ToDateTime(data[2].Trim());
                string MemberTier = data[3].Trim();
                int Points = Convert.ToInt32(data[4].Trim());
                int PunchCard = Convert.ToInt32(data[5].Trim());

                PointCard newPointCard = new PointCard(Points, PunchCard);
                Customer newCustomer = new Customer(Name, MemberID, DOB);

                newCustomer.rewards = newPointCard;
                newPointCard.Tier = MemberTier;

                CustomerList.Add(newCustomer);
                PointCardsList.Add(newPointCard);
            }
            else
            {
                Console.WriteLine($"Invalid data format: {line}");
            }
        }

        foreach (var customer in CustomerList)
        {
            System.Console.WriteLine($"{customer}, {customer.rewards}");
        }
    }

    static void RegisterNewCustomer(List<Customer> CustomerList, List<PointCard> PointCardsList)
    {
        string Input1 = "";
        string Name = "";
        int MemberID = 0;
        DateTime DOB = DateTime.MinValue;
        string Input2 = " ";
        string MemberTier = " ";
        int Points = 0;
        int PunchCard = 0;

        bool inputValid = false;

        while (!inputValid)
        {
            try
            {
                Console.WriteLine("Enter customer name: ");
                Input1 = Console.ReadLine();
                Name = char.ToUpper(Input1[0]) + Input1.Substring(1).ToLower();

                Console.WriteLine("Enter customer member ID: ");
                MemberID = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine("Enter customer DOB(dd/mm/yyyy): ");
                DOB = Convert.ToDateTime(Console.ReadLine());

                Console.WriteLine("Enter Member tier: ");
                Input2 = Console.ReadLine();
                MemberTier = char.ToUpper(Input2[0]) + Input2.Substring(1).ToLower();

                Console.WriteLine("Enter number of points: ");
                Points = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine("Enter PunchCard number: ");
                PunchCard = Convert.ToInt32(Console.ReadLine());

                inputValid = true;
            }
            catch (FormatException a)
            {
                Console.WriteLine("Invalid input format. Please enter valid data.");
                System.Console.WriteLine("customers registration unsuccessful");
            }
            catch (IndexOutOfRangeException b)
            {
                System.Console.WriteLine("Data entered cannot be null.");
                System.Console.WriteLine("customers registration unsuccessful");
            }
            catch (Exception c)
            {
                System.Console.WriteLine("Something went wrong");
                System.Console.WriteLine("customers registration unsuccessful");
            }
        }

        PointCard newPointCard = new PointCard(Points, PunchCard);
        Customer newCustomer = new Customer(Name, MemberID, DOB);

        newCustomer.rewards = newPointCard;
        newPointCard.Tier = MemberTier;

        CustomerList.Add(newCustomer);
        PointCardsList.Add(newPointCard);

        using (
            StreamWriter writer = new StreamWriter("C:\\Codes For Days\\C#\\customers.csv", true)
        )
        {
            string DOBwithoutTime = DOB.ToString("dd/MM/yyyy");
            string patientData =
                $"{Name},{MemberID},{DOBwithoutTime},{MemberTier},{Points},{PunchCard}";
            writer.WriteLine("");
            writer.Write(patientData);

            System.Console.WriteLine("Customer registration successful!");
        }
    }

    static void CreateOrder(List<Customer> CustomerList, List<PointCard> PointCardsList)
    {
        bool cont = true;
        while (cont)
        {
            readCustomerData(CustomerList, PointCardsList);

            int memberId;
            do
            {
                Console.Write("Enter the MemberId of the customer: ");
            } while (
                !int.TryParse(Console.ReadLine(), out memberId)
                || !CustomerList.Any(c => c.MemberID == memberId)
            );

            Customer selectedCustomer = CustomerList.First(c => c.MemberID == memberId);

            Console.WriteLine($"Selected Customer: {selectedCustomer.name}");

            string option = "";

            try
            {
                System.Console.WriteLine("Enter Ice cream option(waffle, cone or cup): ");
                option = Console.ReadLine();
            }
            catch (FormatException a)
            {
                System.Console.WriteLine("Invalid data format entered.");
            }
            catch (Exception b)
            {
                System.Console.WriteLine("Something went wrong.");
            }

            if (option == "cup")
            {
                System.Console.WriteLine("Enter flavour 1: ");
            }

            // If needed, you can use 'selectedCustomer' to perform further operations or create an order.
            // For example, you can call a method to create an order for the selected customer.
            // createOrderForCustomer(selectedCustomer);
        }
    }

    static void Menu()
    {
        System.Console.WriteLine("------------------Menu------------------");
        System.Console.WriteLine("[1] List all customers");
        System.Console.WriteLine("[2] List all current orders");
        System.Console.WriteLine("[3] Register a new Customer");
        System.Console.WriteLine("[4] Create a customer's order");
        System.Console.WriteLine("[5] Display order detail of a customer");
        System.Console.WriteLine("[6] Modify Order details");
        System.Console.WriteLine("[0] Exit");
        System.Console.WriteLine("----------------------------------------");
    }

    public static void Main(string[] args)
    {
        List<Customer> CustomerList = new List<Customer>();
        List<PointCard> PointCardsList = new List<PointCard>();
        List<Flavour> FlavourList = new List<Flavour>();
        List<Topping> ToppingList = new List<Topping>();

        bool loop = true;
        while (loop)
        {
            Menu();
            int option = 0;

            try
            {
                System.Console.WriteLine("Enter option: ");
                option = Convert.ToInt32(Console.ReadLine());

                if (option < 0 || option > 6)
                {
                    throw new InvalidOptionException();
                }
            }
            catch (FormatException a)
            {
                System.Console.WriteLine(
                    "FormatException a occurred, please do not enter a string, only enter a integer between 0 and 6 inclusive."
                );
                continue;
            }
            catch (InvalidOptionException b)
            {
                Console.WriteLine($"InvalidOptionException: {b.Message}");
                continue;
            }
            catch (ArgumentNullException c)
            {
                System.Console.WriteLine("The option cannot be null");
                continue;
            }
            catch (Exception d)
            {
                System.Console.WriteLine("Something went wrong!");
                continue;
            }

            switch (option)
            {
                case 1:
                    readCustomerData(CustomerList, PointCardsList);
                    break;
                case 2:
                    break;
                case 3:
                    RegisterNewCustomer(CustomerList, PointCardsList);
                    break;
                case 4:
                    break;
                case 5:
                    break;
                case 6:
                    break;
                case 0:
                    loop = false;
                    break;
            }
        }
    }
}
