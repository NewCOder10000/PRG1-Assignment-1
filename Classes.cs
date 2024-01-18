using System.Reflection.Emit;

namespace Class
{
    class Flavour
    {
        public string type { get; set; }
        public Boolean premium { get; set; }
        public int quantity { get; set; }

        public Flavour(string t, bool p, int q)
        {
            type = t;
            premium = p;
            quantity = q;
        }

        public override string ToString()
        {
            return $"Ice cream flavour type = {type}, premium = {premium}, quantity = {quantity}";
        }
    }

    class Topping { }

    abstract class IceCream
    {
        public string option { get; set; }
        public int scoops { get; set; }
        public List<Flavour> flavours { get; set; }

        public List<Topping> toppings { get; set; }

        public IceCream(string o, int s, List<Flavour> f, List<Topping> t)
        {
            option = o;
            scoops = s;
            flavours = f;
            toppings = t;
        }

        public abstract double CalculatePrice();

        public override string ToString()
        {
            return $"The IceCream cone is: {option}, number of scoops = {scoops}, flavours = {flavours}, toppings = {toppings}";
        }
    }

    class Cup : IceCream
    {
        public Cup(string o, int s, List<Flavour> f, List<Topping> t)
            : base(o, s, f, t)
        {
            option = o;
            scoops = s;
            flavours = f;
            toppings = t;
        }

        public override double CalculatePrice()
        {
            double totalPrice = 0.0;

            foreach (Flavour flavor in flavours)
            {
                if (flavor.premium)
                {
                    double scoopsAsDouble = Convert.ToDouble(scoops);
                    double price = 4.00 + (1.50 * scoopsAsDouble) + (2.00 * scoopsAsDouble);
                    Console.WriteLine($"Price for {flavor.type} with {scoops} scoops: {price}");
                    totalPrice += price;
                }
                else
                {
                    double scoopsAsDouble = Convert.ToDouble(scoops);
                    double price = 4.00 + (1.50 * scoopsAsDouble);
                    Console.WriteLine(
                        $"Price for premium {flavor.type} with {scoops} scoops: {price}"
                    );
                    totalPrice += price;
                }
            }

            return totalPrice;
        }

        public override string ToString()
        {
            string baseToString = base.ToString();

            double totalPrice = CalculatePrice();

            return $"{baseToString} Price: {totalPrice}";
        }
    }

    class Cone : IceCream
    {
        public bool dipped { get; set; }
        public Cone(string o, int s, List<Flavour> f, List<Topping> t, bool d)
            : base(o, s, f, t)
        {
            option = o;
            scoops = s;
            flavours = f;
            toppings = t;
            dipped = d;
        }

        public override double CalculatePrice()
        {
            double totalPrice = 0.0;

            foreach (Flavour flavor in flavours)
            {
                if (flavor.premium && dipped == true)
                {
                    double scoopsAsDouble = Convert.ToDouble(scoops);
                    double price = (4.00 + (1.50 * scoopsAsDouble) + (2.00 * scoopsAsDouble)) + 2.00;
                    Console.WriteLine($"Price for premium {flavor.type} with {scoops} scoops and chocolate dipped cone: {price}");
                    totalPrice += price;
                }
                else if (flavor.premium && dipped == false)
                {
                    double scoopsAsDouble = Convert.ToDouble(scoops);
                    double price = 4.00 + (1.50 * scoopsAsDouble) + (2.00 * scoopsAsDouble);
                    Console.WriteLine($"Price for {flavor.type} with {scoops} scoops: {price}");
                    totalPrice += price;
                }
                else if (!flavor.premium && dipped == true)
                {
                    double scoopsAsDouble = Convert.ToDouble(scoops);
                    double price = 4.00 + (1.50 * scoopsAsDouble) + 2.00;
                    Console.WriteLine($"Price for {flavor.type} with {scoops} scoops and chocolate dipped cone: {price}");
                    totalPrice += price;
                }
                else
                {
                    double scoopsAsDouble = Convert.ToDouble(scoops);
                    double price = 4.00 + (1.50 * scoopsAsDouble);
                    Console.WriteLine($"Price for {flavor.type} with {scoops} scoops: {price}");
                    totalPrice += price;
                }
            }
            return totalPrice;
        }

        public override string ToString()
        {
            string baseToString = base.ToString();

            double totalPrice = CalculatePrice();

            return $"{baseToString}, Chocolate dipped cone: {dipped}, Price: {totalPrice}";
        }
    }

    class Waffle : IceCream
    {
        public string waffleFlavour { get; set; }
        public Waffle(string o, int s, List<Flavour> f, List<Topping> t, string w)
            : base(o, s, f, t)
        {
            option = o;
            scoops = s;
            flavours = f;
            toppings = t;
            waffleFlavour = w;
        }

        public override double CalculatePrice()
        {
            double totalPrice = 0.0;

            foreach (Flavour flavor in flavours)
            {
                if (flavor.premium && waffleFlavour == "Red Velvet" || waffleFlavour == "Charcoal" || waffleFlavour == "Pandan")
                {
                    double scoopsAsDouble = Convert.ToDouble(scoops);
                    double price = (4.00 + (1.50 * scoopsAsDouble) + (2.00 * scoopsAsDouble)) + 3.00;
                    Console.WriteLine($"Price for premium {flavor.type} with {scoops} scoops and chocolate dipped cone: {price}");
                    totalPrice += price;
                }
                else if (flavor.premium && waffleFlavour == "Original")
                {
                    double scoopsAsDouble = Convert.ToDouble(scoops);
                    double price = 4.00 + (1.50 * scoopsAsDouble) + (2.00 * scoopsAsDouble);
                    Console.WriteLine($"Price for {flavor.type} with {scoops} scoops: {price}");
                    totalPrice += price;
                }
                else if (!flavor.premium && waffleFlavour == "Red Velvet" || waffleFlavour == "Charcoal" || waffleFlavour == "Pandan")
                {
                    double scoopsAsDouble = Convert.ToDouble(scoops);
                    double price = 4.00 + (1.50 * scoopsAsDouble) + 3.00;
                    Console.WriteLine($"Price for {flavor.type} with {scoops} scoops and chocolate dipped cone: {price}");
                    totalPrice += price;
                }
                else
                {
                    double scoopsAsDouble = Convert.ToDouble(scoops);
                    double price = 4.00 + (1.50 * scoopsAsDouble);
                    Console.WriteLine($"Price for {flavor.type} with {scoops} scoops: {price}");
                    totalPrice += price;
                }
            }
            return totalPrice;
        }

        public override string ToString()
        {
            string baseToString = base.ToString();

            double totalPrice = CalculatePrice();

            return $"{baseToString}, Waffle flavour : {waffleFlavour} Price: {totalPrice}";
        }
    }
}
