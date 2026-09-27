using System.Security.Cryptography.X509Certificates;

namespace adv_1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // 1.A class defined with a placeholder for its data type
            // type safety  code reusability  better performance
            // cleaner code 
            //2
            Container<int> coo = new Container<int>(10);
            Console.WriteLine(coo.Get());
            // 3 A generic class can have more than one type parameter
            Pair<int, int> pair = new Pair<int, int>(2, 0);
            Console.WriteLine(pair.Key);
            Console.WriteLine(pair.Value);
            //4 A method can be generic even if the class is not generic 
            int a = 10, b = 20;
            Console.WriteLine(a);
            Console.WriteLine(b);
            Utility.swap<int>(ref a, ref b);
            Console.WriteLine($"After Swap");
            Console.WriteLine(a);
            Console.WriteLine(b);
            //5 
            int[] numbers = { 5, 12, 3, 25, 8 };
            int maxNumber = find0.FindMax(numbers);
            Console.WriteLine($"Max Number: {maxNumber}");

            string[] names = { "Ahmed", "Salma", "Mona" };
            string maxName = find0.FindMax(names);
            Console.WriteLine($"Max Name: {maxName}");
            //6
            // Interfaces can also be generic . Any class that implements them must specify the type arguments. 
            // 7 
            // The struct constranint allows only value types
            //(including nullable value types)
            var b1 = new Box<int>(10);
            var b2 = new Box<double>(8.8);
            Console.WriteLine($"Box 1 Value: {b1.Value}");
            Console.WriteLine($"Box 2 Value: {b2.Value}");
            // 8
            // the class constraint allows only reference types.
            var r1 = new Repost<string>("Hello");
            Console.WriteLine($"Repost 1 : {r1.Item}");
            // 9
            // The new() constraint requires that the type has a public
            // parameterless (default) constructor.
            var Fact = new Fact<Product>();
            Product P = Fact.Create();
            Console.WriteLine(P.Id);
            // 10 
            // The interface constraint requires that the type implements
            // the specified interface.
            var p1 = new Printer<Doc>();
            p1.printItem(new Doc());

            var p2 = new Printer<Report>();
            p2.printItem(new Report());
            //11
            // restricts the generic type argument T to inherit from a specified base class.
            Dog dog = new Dog();
            dog.Name = "Max";
            AnimalContainer<Animal> animal = new();
            animal.DisplayAnimalName(dog);
            /* 12
             * The default keyword returns the default value of a generic type parameter T:
             * For reference types, it returns null.
             * For value types , it returns 0, false, or a zero-initialized struct.
             */
            //15
            //Covariance allows a method to return a more derived type than specified by the generic type parameter.
            // 16
            //Contravariance allows a method to accept arguments of a less derived (more base) type than specified
            //by the generic type parameter.
            //17
            //Covariance (out): Preserves assignment compatibility   Return types (Output)   IEnumerable
            //Contravariance (in): Reverses assignment compatibility   Parameter types (Input)  IComparer
            //19
            //Add new generic parameters

        }




        public class Container<T>  //2
        {

            public T Value { get; set; }
            public Container(T value)
            {
                Value = value;
            }
            public void Add(T value)
            {
                Value = value;
                Console.WriteLine($"Value {Value} is Added");
            }

            public T Get()
            {
                return Value;
            }
        }


        //3
        public class Pair<T1, T2>
        {
            public Pair(T1 key, T2 value)
            {
                Key = key;
                Value = value;
            }

            public T1 Key { get; set; }
            public T2 Value { get; set; }
        }


        //4
        public class Utility
        {
            public static void swap<T>(ref T a, ref T b)
            {
                T Temb = a;
                a = b;
                b = Temb;
            }
        }


        //5
        public class find0
        {
            public static T FindMax<T>(T[] values) where T : IComparable<T>
            {
                T max = values[0];
                for (int i = 0; i < values.Length; i++)
                {
                    if (values[i].CompareTo(max) > 0)
                    {
                        max = values[i];
                    }
                }
                return max;
            }
        }


        interface IRepository<T> // 6
        {
            void Add(T item);
            T? GetById(int id);
            List<T> GetAll();
            void Delete(int id);
        }


        public class Box<T> where T : struct //7
        {
            public T Value { get; set; }
            public Box(T value)
            {
                Value = value;
            }
        }
        public class Repost<T> where T : class //8
        {
            public Repost(T item)
            {
                Item = item;
            }

            public T Item { get; set; }
        }
        public class Fact<T> where T : new() //9
        {
            public T Create()
            {
                return new T();
            }
        }
        class Product
        {
            public Product()
            {
                Id = 0;
            }
            public Product(int id)
            {
                Id = id;
            }

            public int Id { get; set; }
        }
        public interface IPrintable //10
        {
            void print();
        }
        public class Doc : IPrintable
        {
            public void print()
            {
                Console.WriteLine("doc printed");
            }
        }
        public class Report : IPrintable
        {
            public void print()
            {
                Console.WriteLine("Report printed");
            }
        }
        public class Printer<T> where T : IPrintable
        {
            public void printItem(T item)
            {
                item.print();
            }
        }
        public class Animal //11
        {
            public string? Name { get; set; }
            public void MakeSound()
            {
                Console.WriteLine("Animal Sound");
            }
        }
        public class Dog : Animal { }
        public class AnimalContainer<T> where T : Animal
        {
            public void DisplayAnimalName(T animal)
            {
                Console.WriteLine(animal.Name);
                animal.MakeSound();
            }
        }

        public class SafeList<T> //14
        {
           
            private List<T> items = new List<T>();

            public T? GetItem(int index)
            {
                if (index < 0 || index >= items.Count)
                {
                    return default;
                }
                return items[index];
            }
        
            // 15 
            interface IProducer<out T>
            {
                T Get();
            }
            class Animal { public string Name { get; set; } }
            class Dog : Animal { }
            class DogProducer :IProducer<Dog>
            {
                public Dog Get()
                {
                    return new Dog { Name = "Buddy" };
                }
            }
               interface IProcessor<in T> //16
            {
                void Process(T item);
            }
            class Animall {public string Name { get; set; } }
            class Ddog : Animall { }
            class AniamlProcessor : IProcessor<Animall>
            {
                public void Process(Animall item)
                {
                    Console.WriteLine($"Processing aniaml :{item.Name}");
                }
            }
            //18
            public class Counter
            {
                public static int Count = 0;
            }

          
        }
    }
}
