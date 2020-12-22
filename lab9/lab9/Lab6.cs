using System;



namespace lab6
{

    abstract class Vehicle
    {
        protected double kmToMile = 0.62;
        public int KmPerHour { get; set; }
        public double Price { get; set; }
        public override string ToString()
        {
            return $"It's a vehicle. ";
        }
        public double MilePerHour()
        {
            return KmPerHour * kmToMile;
        }

    }

    class Car : Vehicle
    {

        protected string brand;
        protected string model;

        public string Brand
        {
            get { return brand; }
            private set { brand = value; }
        }

        public string Model
        {
            get { return model; }
            private set { model = value; }
        }

        public Car(double price, int kmPerHour, string brand, string model)
        {
            Price = price;
            KmPerHour = kmPerHour;
            Brand = brand;
            Model = model;
        }
        public virtual void BrumBrum()
        {
            Console.WriteLine("Brum brum!");
        }

        public override string ToString()
        {
            return base.ToString() + "It's a car. ";
        }
    }

    class Bicycle : Vehicle
    {
        private int tireSize;

        public int TireSize
        {
            get { return tireSize; }
            private set
            {
                tireSize = value > 0 ? value : 10;
            }
        }

        public bool HasExtremeWheels { get; set; }

        public Bicycle(double price, int kmPerHour, int tireSize, bool hasExtremeWheels)
        {
            Price = price;
            KmPerHour = kmPerHour;
            TireSize = tireSize;
            HasExtremeWheels = hasExtremeWheels;
        }

        public override string ToString() { return base.ToString() + "It's a bicycle. "; }

        public bool IsItForChildren() { return HasExtremeWheels && tireSize < 10; }
    }

    class PassengerCar : Car
    {
        private int year;
        public int Year
        {
            private set
            {
                if (value >= 1900 && value < 2020) year = value;
                else year = DateTime.Now.Year;
            }
            get { return year; }
        }

        public string Color
        {
            set;
            get;
        } = "Yellow";

        public PassengerCar(double price, int kmPerHour, string brand, string model, string color, int year) : base(price, kmPerHour, brand, model)
        {
            Color = color;
            Year = year;
        }

        public override void BrumBrum()
        {
            Console.WriteLine("Bip bip!");
        }

        public bool IsNew()
        {
            return year == DateTime.Now.Year;
        }

        public override string ToString()
        {
            return base.ToString() + "It's a passenger car. ";
        }
    }

    class TruckCar : Car
    {
        private int loadCapacity;
        private int height;

        public int Height
        {
            get { return height; }
            private set
            {
                height = value > 0 ? value : 5;
            }
        }

        public int LoadCapacity
        {
            get { return loadCapacity; }
            private set { loadCapacity = value > 0 ? value : 100; }
        }

        public TruckCar(double price, int kmPerHour, string brand, string model, int loadCapacity, int height) : base(price, kmPerHour, brand, model)
        {
            LoadCapacity = loadCapacity;
            Height = height;
        }

        public override void BrumBrum()
        {
            base.BrumBrum();
            Console.WriteLine("Brrrrrrr!");
        }

        public override string ToString()
        {
            return base.ToString() + "It's a truck car. ";
        }

        public bool WillItFitUnderViaduct(int heightOfViaduct)
        {
            return height > heightOfViaduct ? false : true;
        }

    }


    interface IFigure
    {
        void moveTo(double x, double y);
    }

    interface IHasInterior
    {
        string InteriorColor { get; set; }
    }

    class FirstInterface : IFigure
    {
        public void moveTo(double x, double y)
        {
            Console.WriteLine($"{x} --> {y}");
        }
    }

    class BothInterfaces : IFigure, IHasInterior
    {
        public void moveTo(double x, double y)
        {
            Console.WriteLine($"{y} --> {x}");
        }
        string IHasInterior.InteriorColor { get; set; } = "Green";

    }


    class Program
    {
        static int TotalCapacity(Vehicle[] array)
        {
            int totalCapacity = 0;

            foreach (Vehicle vehicle in array)
            {
                if (vehicle is TruckCar) totalCapacity += (vehicle as TruckCar).LoadCapacity;
            }
            return totalCapacity;
        }

        static void PrintVehicle(Vehicle[] array)
        {
            foreach (Vehicle vehicle in array)
            {
                Console.WriteLine(vehicle.ToString());
                if (vehicle is Car) (vehicle as Car).BrumBrum();
            }
        }

        static void Task1()
        {

            Vehicle[] array = {
                new Car(15300.0, 100, "Ford", "Fusion"),
                new Bicycle(100.0, 30, 15, false),
                new PassengerCar(3123.30, 150, "Fiat", "126p", "Red", 1995),
                new TruckCar(102000.0, 90, "Daf", "14X", 300, 6),
                new TruckCar(20000.0, 87, "Tir", "Extra", 200, 4)};

            PrintVehicle(array);

            Console.WriteLine($"Total capacity = {TotalCapacity(array)}");
        }

        static void WriteColors(object[] array)
        {
            foreach (var item in array)
            {
                if (item is IHasInterior) Console.WriteLine((item as IHasInterior).InteriorColor);
                else Console.WriteLine("No color");
            }
        }
        static void Task2()
        {
            object[] array = { new FirstInterface(), new BothInterfaces(), new BothInterfaces(), new FirstInterface(), 3 };
            WriteColors(array);

        }

        static void MainLab6(string[] args)
        {
            //Task1();
            //Task2();
        }
    }
}
