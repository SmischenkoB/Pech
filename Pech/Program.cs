using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Pech
{
    interface Transoprt {
        
        int ReturnPrice();
    }
     abstract class DeliveryPrice
     {
        public abstract int CountPrice(); 
        public abstract List<Transoprt>  CreateTransport(int CargoSize);
     }


    class DeliveryPriceTrain : DeliveryPrice
    {
        public List<Train> wagons { get; private  set; } = new List<Train>();
        public List<Locomotive> locomotives { get; private set; } = new List<Locomotive>();

        public override int CountPrice()
        {
            if (wagons.Equals(null) || locomotives.Equals(null)) return 0;
            int price = 0;
            foreach (Train t in wagons) { price += t.ReturnPrice(); }
            foreach (Locomotive t in locomotives) { price += t.ReturnPrice(); }
            return price;
        }

        public override List<Transoprt> CreateTransport(int CargoSize)
        {
            if (CargoSize <= 0) return null;
            
            int amountWagons = CargoSize % Train.Size == 0 ? CargoSize / Train.Size : CargoSize / Train.Size + 1;
            wagons.AddRange(new Train[amountWagons]);
            for (int i = 0; i < wagons.Count; i++) { wagons[i] = new Train(); }        

            int amountLocom = wagons.Count % Locomotive.SizeOfWagons == 0 ? 
                wagons.Count / Locomotive.SizeOfWagons : wagons.Count / Locomotive.SizeOfWagons + 1;
            locomotives.AddRange(new Locomotive[amountLocom]);
            for (int i = 0; i < locomotives.Count; i++) { locomotives[i] = new Locomotive(); }
            List<Transoprt> output = new List<Transoprt>();
            output.AddRange(wagons);
            output.AddRange(locomotives);
            return output;
            
        }
    }
    class DeliveryPriceCar : DeliveryPrice
    {
        List<Car> cars = new List<Car>();

        public override int CountPrice()
        {
            int price = 0;
            if (cars.Equals(null)) return 0;
            foreach (Car c in cars){ price += c.ReturnPrice(); }
            return price;
        }

        public override List<Transoprt> CreateTransport(int CargoSize)
        {
            if (CargoSize <= 0) return null;

            int amountCars = (CargoSize % Car.Size) == 0 ? CargoSize / Car.Size : CargoSize / Car.Size + 1;// нужна ли
            cars.AddRange(new Car[amountCars]);                                                              // доп. машина
            for (int i = 0; i < cars.Count; i++) { cars[i] = new Car(); }
            List<Transoprt> output = new List<Transoprt>();
            output.AddRange(cars);
            return output;
        }
    }


    class Train : Transoprt
    {
        public static int Size { get; } = 60;// кол - во товара
        public int ReturnPrice()
        {
            return 20;
        }
    }
    class Locomotive : Transoprt
    {
        public static int SizeOfWagons { get; } = 10;// кол-во вагонов, которые можно присоединить
        public int ReturnPrice()
        {
            return 15;
        }
    }
    class Car : Transoprt
    {
        public static int Size { get; } = 15;// кол - во товара
        public int ReturnPrice()
        {
            return 10;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            DeliveryPrice obj = new DeliveryPriceTrain();
           // Console.WriteLine((obj.CreateTransport(60)).Count);
            Console.WriteLine();
            //Console.WriteLine( ((DeliveryPriceTrain)obj).locomotives.Count);
            Console.WriteLine(obj.CountPrice());
            Console.ReadLine();
        }
    }
    

}
