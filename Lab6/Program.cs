using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Linq;
using System.Collections.Generic;

// Klasa Student
public class Student
{
    public int StudentId { get; set; }
    public string Imie { get; set; } = string.Empty;
    public string Nazwisko { get; set; } = string.Empty;
    public List<Ocena> Oceny { get; set; } = new List<Ocena>();
}

// Klasa Ocena
public class Ocena
{
    public int OcenaId { get; set; }
    public double Wartosc { get; set; }
    public string Przedmiot { get; set; } = string.Empty;
    public int StudentId { get; set; }
}

public class Program
{
    public static void Main()
    {
        string connStr =
            "Data Source=10.200.2.28;" +
            "Initial Catalog=studenci_71452;" +
            "Integrated Security=True;" +
            "Encrypt=True;" +
            "TrustServerCertificate=True";

        try
        {
            using var conn = new SqlConnection(connStr);
            conn.Open();
            Console.WriteLine("Połączenie z bazą danych nawiązane.");

            Console.WriteLine("\n--- ZADANIE 4 ---");
            PokazStudentow(conn);

            Console.WriteLine("\n--- ZADANIE 5 ---");
            PokazStudenta(conn, 1);

            Console.WriteLine("\n--- ZADANIE 6 ---");
            var lista = PobierzStudentowZOcenami(conn);
            WypiszStudentowZOcenami(lista);

            Console.WriteLine("\n--- ZADANIE 7 ---");
            DodajNowegoStudenta(conn, new Student
            {
                Imie = "Adam",
                Nazwisko = "Nowak"
            });
            Console.WriteLine("Student dodany.");

            Console.WriteLine("\n--- ZADANIE 8 ---");
            DodajNowaOcene(conn, new Ocena
            {
                StudentId = 1,
                Przedmiot = "matematyka",
                Wartosc = 4.5
            });
            Console.WriteLine("Ocena dodana.");

            Console.WriteLine("\n--- ZADANIE 9 ---");
            SkasujGeografie(conn);
            Console.WriteLine("Oceny z geografii usunięte.");

            Console.WriteLine("\n--- ZADANIE 10 ---");
            ZmienOcene(conn, 1, 5.0);
            Console.WriteLine("Ocena zaktualizowana.");
        }
        catch (Exception e)
        {
            Console.WriteLine("Błąd aplikacji: " + e.Message);
        }
    }

    // 4. Wyświetlenie studentów
    static void PokazStudentow(SqlConnection conn)
    {
        string sql = "SELECT StudentId, Imie, Nazwisko FROM Student";
        using var cmd = new SqlCommand(sql, conn);
        using var rdr = cmd.ExecuteReader();

        if (!rdr.HasRows)
        {
            Console.WriteLine("Brak rekordów.");
            return;
        }

        while (rdr.Read())
        {
            Console.WriteLine(
                $"ID: {rdr.GetInt32(0)}, Imię: {rdr.GetString(1)}, Nazwisko: {rdr.GetString(2)}"
            );
        }
    }

    // 5. Student po ID
    static void PokazStudenta(SqlConnection conn, int id)
    {
        string sql = "SELECT Imie, Nazwisko FROM Student WHERE StudentId=@id";
        using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@id", id);

        using var rdr = cmd.ExecuteReader();
        if (rdr.Read())
            Console.WriteLine($"Student: {rdr.GetString(0)} {rdr.GetString(1)}");
        else
            Console.WriteLine("Student nie istnieje.");
    }

    // 6. Pobranie studentów z ocenami
    static List<Student> PobierzStudentowZOcenami(SqlConnection conn)
    {
        string sql = @"
            SELECT s.StudentId, s.Imie, s.Nazwisko,
                   o.OcenaId, o.Wartosc, o.Przedmiot
            FROM Student s
            LEFT JOIN Ocena o ON s.StudentId = o.StudentId
            ORDER BY s.StudentId";

        using var cmd = new SqlCommand(sql, conn);
        using var rdr = cmd.ExecuteReader();

        List<Student> wynik = new();

        while (rdr.Read())
        {
            int sid = rdr.GetInt32(0);
            var st = wynik.FirstOrDefault(x => x.StudentId == sid);

            if (st == null)
            {
                st = new Student
                {
                    StudentId = sid,
                    Imie = rdr.GetString(1),
                    Nazwisko = rdr.GetString(2)
                };
                wynik.Add(st);
            }

            if (!rdr.IsDBNull(3))
            {
                st.Oceny.Add(new Ocena
                {
                    OcenaId = rdr.GetInt32(3),
                    Wartosc = rdr.GetDouble(4),
                    Przedmiot = rdr.GetString(5),
                    StudentId = sid
                });
            }
        }
        return wynik;
    }

    // Wypisanie studentów z ocenami
    static void WypiszStudentowZOcenami(List<Student> studenci)
    {
        foreach (var s in studenci)
        {
            Console.WriteLine($"\n{s.StudentId} - {s.Imie} {s.Nazwisko}");
            foreach (var o in s.Oceny)
                Console.WriteLine($"   {o.Przedmiot}: {o.Wartosc}");
        }
    }

    // 7. Dodanie studenta
    static void DodajNowegoStudenta(SqlConnection conn, Student s)
    {
        string sql = "INSERT INTO Student(Imie, Nazwisko) VALUES (@i,@n)";
        using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@i", s.Imie);
        cmd.Parameters.AddWithValue("@n", s.Nazwisko);
        cmd.ExecuteNonQuery();
    }

    // Sprawdzenie poprawności oceny
    static bool CzyOcenaPoprawna(double o)
    {
        return o >= 2 && o <= 5 && (o * 10) % 5 == 0 && o != 2.5;
    }

    // 8. Dodanie oceny
    static void DodajNowaOcene(SqlConnection conn, Ocena o)
    {
        if (!CzyOcenaPoprawna(o.Wartosc))
        {
            Console.WriteLine("Błędna ocena.");
            return;
        }

        string sql = "INSERT INTO Ocena(Wartosc, Przedmiot, StudentId) VALUES (@w,@p,@s)";
        using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@w", o.Wartosc);
        cmd.Parameters.AddWithValue("@p", o.Przedmiot);
        cmd.Parameters.AddWithValue("@s", o.StudentId);
        cmd.ExecuteNonQuery();
    }

    // 9. Usunięcie geografii
    static void SkasujGeografie(SqlConnection conn)
    {
        string sql = "DELETE FROM Ocena WHERE Przedmiot='geografia'";
        using var cmd = new SqlCommand(sql, conn);
        cmd.ExecuteNonQuery();
    }

    // 10. Aktualizacja oceny
    static void ZmienOcene(SqlConnection conn, int id, double nowa)
    {
        if (!CzyOcenaPoprawna(nowa))
        {
            Console.WriteLine("Nieprawidłowa nowa ocena.");
            return;
        }

        string sql = "UPDATE Ocena SET Wartosc=@w WHERE OcenaId=@id";
        using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@w", nowa);
        cmd.Parameters.AddWithValue("@id", id);
        cmd.ExecuteNonQuery();
    }
}
