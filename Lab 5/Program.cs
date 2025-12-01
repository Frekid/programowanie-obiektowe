using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Xml.Serialization;
using System.Linq;

namespace FileTasks
{
    public class Student
    {
        public string Imie { get; set; }
        public string Nazwisko { get; set; }
        public List<int> Oceny { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // tutaj trzeba wpisac jakij kod chce wykonac 
            //Zadanie2();
            //Zadanie3();
            //Zadanie4();
            //Zadanie6_JSON_Serialize();
            //Zadanie7_JSON_Deserialize();
            //Zadanie8_XML_Serialize();
            //Zadanie9_XML_Deserialize();
            //Zadanie10_ReadCSV();
            //Zadanie11_CalcAverages();
            //Zadanie12_FilterIris();
        }

        // ----------------- ZADANIE 2 -----------------
        static void Zadanie2()
        {
            Console.WriteLine("Ile linii chcesz wprowadzić?");
            int n = int.Parse(Console.ReadLine());

            string path = "tekst.txt";
            using (StreamWriter sw = new StreamWriter(path))
            {
                for (int i = 0; i < n; i++)
                {
                    Console.Write($"Linia {i + 1}: ");
                    string line = Console.ReadLine();
                    sw.WriteLine(line);
                }
            }
            Console.WriteLine("Zapisano!");
        }

        // ----------------- ZADANIE 3 -----------------
        static void Zadanie3()
        {
            string path = "tekst.txt";

            if (!File.Exists(path))
            {
                Console.WriteLine("Plik nie istnieje.");
                return;
            }

            foreach (var line in File.ReadLines(path))
                Console.WriteLine(line);
        }

        // ----------------- ZADANIE 4 -----------------
        static void Zadanie4()
        {
            string path = "tekst.txt";

            Console.WriteLine("Ile nowych linii chcesz dopisać?");
            int n = int.Parse(Console.ReadLine());

            using (StreamWriter sw = new StreamWriter(path, append: true))
            {
                for (int i = 0; i < n; i++)
                {
                    Console.Write($"Nowa linia {i + 1}: ");
                    string line = Console.ReadLine();
                    sw.WriteLine(line);
                }
            }
            Console.WriteLine("Dopisano!");
        }

        // ----------------- ZADANIE 6 -----------------
        static void Zadanie6_JSON_Serialize()
        {
            List<Student> students = new List<Student>
            {
                new Student { Imie = "Jan", Nazwisko = "Kowalski", Oceny = new List<int>{ 5,4,3 } },
                new Student { Imie = "Anna", Nazwisko = "Nowak", Oceny = new List<int>{ 3,5,5 } }
            };

            string json = JsonSerializer.Serialize(students, new JsonSerializerOptions { WriteIndented = true });

            File.WriteAllText("studenci.json", json);

            Console.WriteLine("Zapisano do studenci.json");
        }

        // ----------------- ZADANIE 7 -----------------
        static void Zadanie7_JSON_Deserialize()
        {
            if (!File.Exists("studenci.json"))
            {
                Console.WriteLine("Brak pliku studenci.json");
                return;
            }

            string json = File.ReadAllText("studenci.json");
            var students = JsonSerializer.Deserialize<List<Student>>(json);

            foreach (var s in students)
            {
                Console.WriteLine($"{s.Imie} {s.Nazwisko}");
                Console.WriteLine("Oceny: " + string.Join(", ", s.Oceny));
            }
        }

        //------------------ ZADANIE 8 -----------------
        static void Zadanie8_XML_Serialize()
        {
            List<Student> students = new List<Student>
            {
                new Student { Imie="Tomasz", Nazwisko="Wiśniewski", Oceny=new List<int>{5,4,4} },
                new Student { Imie="Ewa", Nazwisko="Jankowska", Oceny=new List<int>{3,4,5} }
            };

            XmlSerializer serializer = new XmlSerializer(typeof(List<Student>));

            using (FileStream fs = new FileStream("studenci.xml", FileMode.Create))
            {
                serializer.Serialize(fs, students);
            }

            Console.WriteLine("Zapisano do studenci.xml");
        }

        //------------------ ZADANIE 9 -----------------
        static void Zadanie9_XML_Deserialize()
        {
            if (!File.Exists("studenci.xml"))
            {
                Console.WriteLine("Brak pliku studenci.xml");
                return;
            }

            XmlSerializer serializer = new XmlSerializer(typeof(List<Student>));
            List<Student> students;

            using (FileStream fs = new FileStream("studenci.xml", FileMode.Open))
            {
                students = (List<Student>)serializer.Deserialize(fs);
            }

            foreach (var s in students)
            {
                Console.WriteLine($"{s.Imie} {s.Nazwisko}");
                Console.WriteLine("Oceny: " + string.Join(", ", s.Oceny));
            }
        }

        //------------------ ZADANIE 10 -----------------
        static void Zadanie10_ReadCSV()
        {
            string path = "iris.csv";

            if (!File.Exists(path))
            {
                Console.WriteLine("Brak iris.csv!");
                return;
            }

            foreach (var line in File.ReadLines(path))
                Console.WriteLine(line);
        }

        //------------------ ZADANIE 11 -----------------
        static void Zadanie11_CalcAverages()
        {
            string path = "iris.csv";
            if (!File.Exists(path))
            {
                Console.WriteLine("Brak iris.csv!");
                return;
            }

            var lines = File.ReadAllLines(path);
            var header = lines[0].Split(',');

            double[] sums = new double[header.Length - 1];
            int count = 0;

            for (int i = 1; i < lines.Length; i++)
            {
                var cols = lines[i].Split(',');
                for (int c = 0; c < cols.Length - 1; c++)
                    sums[c] += double.Parse(cols[c]);
                count++;
            }

            for (int i = 0; i < sums.Length; i++)
                Console.WriteLine($"{header[i]} avg = {sums[i] / count}");
        }

        //------------------ ZADANIE 12 -----------------
        static void Zadanie12_FilterIris()
        {
            string path = "iris.csv";
            if (!File.Exists(path))
            {
                Console.WriteLine("Brak iris.csv!");
                return;
            }

            var lines = File.ReadAllLines(path);
            var header = lines[0].Split(',');

            int idxSepalL = Array.IndexOf(header, "sepal length");
            int idxSepalW = Array.IndexOf(header, "sepal width");
            int idxClass = Array.IndexOf(header, "class");

            List<string> outLines = new List<string>();
            outLines.Add("sepal length,sepal width,class");

            for (int i = 1; i < lines.Length; i++)
            {
                var cols = lines[i].Split(',');
                double sl = double.Parse(cols[idxSepalL]);

                if (sl < 5)
                {
                    outLines.Add($"{cols[idxSepalL]},{cols[idxSepalW]},{cols[idxClass]}");
                }
            }

            File.WriteAllLines("iris_filtered.csv", outLines);
            Console.WriteLine("Zapisano iris_filtered.csv");
        }
    }
}
