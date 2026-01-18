using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Xml.Serialization;
using System.Linq;

namespace FileTasks
{
    // ZADANIE 5 – klasa Student
    public class Student
    {
        public string Imie { get; set; } = "";
        public string Nazwisko { get; set; } = "";
        public List<int> Oceny { get; set; } = new List<int>();
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Ćwiczenie 5 – Praca z plikami ===");
            Console.WriteLine("Wybierz numer zadania (2–12):");

            int wybor = int.Parse(Console.ReadLine());

            switch (wybor)
            {
                case 2: Zadanie2(); break;
                case 3: Zadanie3(); break;
                case 4: Zadanie4(); break;
                case 6: Zadanie6_JSON_Serialize(); break;
                case 7: Zadanie7_JSON_Deserialize(); break;
                case 8: Zadanie8_XML_Serialize(); break;
                case 9: Zadanie9_XML_Deserialize(); break;
                case 10: Zadanie10_ReadCSV(); break;
                case 11: Zadanie11_CalcAverages(); break;
                case 12: Zadanie12_FilterIris(); break;
                default: Console.WriteLine("Niepoprawny numer zadania."); break;
            }
        }

        // ----------------- ZADANIE 2 -----------------
        static void Zadanie2()
        {
            Console.WriteLine("Ile linii chcesz wprowadzić?");
            int n = int.Parse(Console.ReadLine());

            using StreamWriter sw = new StreamWriter("tekst.txt");
            for (int i = 0; i < n; i++)
            {
                Console.Write($"Linia {i + 1}: ");
                sw.WriteLine(Console.ReadLine());
            }

            Console.WriteLine("Dane zapisane do pliku tekst.txt");
        }

        // ----------------- ZADANIE 3 -----------------
        static void Zadanie3()
        {
            if (!File.Exists("tekst.txt"))
            {
                Console.WriteLine("Plik nie istnieje.");
                return;
            }

            foreach (var line in File.ReadLines("tekst.txt"))
                Console.WriteLine(line);
        }

        // ----------------- ZADANIE 4 -----------------
        static void Zadanie4()
        {
            Console.WriteLine("Ile linii chcesz dopisać?");
            int n = int.Parse(Console.ReadLine());

            using StreamWriter sw = new StreamWriter("tekst.txt", true);
            for (int i = 0; i < n; i++)
            {
                Console.Write($"Nowa linia {i + 1}: ");
                sw.WriteLine(Console.ReadLine());
            }

            Console.WriteLine("Linie dopisane.");
        }

        // ----------------- ZADANIE 6 -----------------
        static void Zadanie6_JSON_Serialize()
        {
            List<Student> students = new()
            {
                new Student { Imie="Jan", Nazwisko="Kowalski", Oceny={5,4,3} },
                new Student { Imie="Anna", Nazwisko="Nowak", Oceny={3,5,5} }
            };

            string json = JsonSerializer.Serialize(
                students,
                new JsonSerializerOptions { WriteIndented = true });

            File.WriteAllText("studenci.json", json);
            Console.WriteLine("Zapisano dane do studenci.json");
        }

        // ----------------- ZADANIE 7 -----------------
        static void Zadanie7_JSON_Deserialize()
        {
            if (!File.Exists("studenci.json"))
            {
                Console.WriteLine("Brak pliku studenci.json");
                return;
            }

            var students = JsonSerializer.Deserialize<List<Student>>(
                File.ReadAllText("studenci.json"));

            foreach (var s in students)
            {
                Console.WriteLine($"{s.Imie} {s.Nazwisko}");
                Console.WriteLine("Oceny: " + string.Join(", ", s.Oceny));
            }
        }

        // ----------------- ZADANIE 8 -----------------
        static void Zadanie8_XML_Serialize()
        {
            List<Student> students = new()
            {
                new Student { Imie="Tomasz", Nazwisko="Wiśniewski", Oceny={5,4,4} },
                new Student { Imie="Ewa", Nazwisko="Jankowska", Oceny={3,4,5} }
            };

            XmlSerializer serializer = new(typeof(List<Student>));
            using FileStream fs = new FileStream("studenci.xml", FileMode.Create);
            serializer.Serialize(fs, students);

            Console.WriteLine("Zapisano dane do studenci.xml");
        }

        // ----------------- ZADANIE 9 -----------------
        static void Zadanie9_XML_Deserialize()
        {
            if (!File.Exists("studenci.xml"))
            {
                Console.WriteLine("Brak pliku studenci.xml");
                return;
            }

            XmlSerializer serializer = new(typeof(List<Student>));
            using FileStream fs = new FileStream("studenci.xml", FileMode.Open);
            var students = (List<Student>)serializer.Deserialize(fs);

            foreach (var s in students)
            {
                Console.WriteLine($"{s.Imie} {s.Nazwisko}");
                Console.WriteLine("Oceny: " + string.Join(", ", s.Oceny));
            }
        }

        // ----------------- ZADANIE 10 -----------------
        static void Zadanie10_ReadCSV()
        {
            if (!File.Exists("iris.csv"))
            {
                Console.WriteLine("Brak iris.csv");
                return;
            }

            foreach (var line in File.ReadLines("iris.csv"))
                Console.WriteLine(line);
        }

        // ----------------- ZADANIE 11 -----------------
        static void Zadanie11_CalcAverages()
        {
            var lines = File.ReadAllLines("iris.csv");
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

        // ----------------- ZADANIE 12 -----------------
        static void Zadanie12_FilterIris()
        {
            var lines = File.ReadAllLines("iris.csv");
            var header = lines[0].Split(',');

            int sl = Array.FindIndex(header, h => h.Contains("sepal") && h.Contains("length"));
            int sw = Array.FindIndex(header, h => h.Contains("sepal") && h.Contains("width"));
            int cl = Array.FindIndex(header, h => h.Contains("class"));

            List<string> result = new()
            {
                "sepal length,sepal width,class"
            };

            for (int i = 1; i < lines.Length; i++)
            {
                var cols = lines[i].Split(',');
                if (double.Parse(cols[sl]) < 5)
                    result.Add($"{cols[sl]},{cols[sw]},{cols[cl]}");
            }

            File.WriteAllLines("iris_filtered.csv", result);
            Console.WriteLine("Zapisano iris_filtered.csv");
        }
    }
}

