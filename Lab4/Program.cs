public class Program
{
    public static void Main(string[] args)
    {
        // Przygotujemy kilka stałych liczb zespolonych do różnych zadań:
        var z1 = new ComplexNumber(6, 7);   // 6 + 7i
        var z2 = new ComplexNumber(1, 2);   // 1 + 2i
        var z3 = new ComplexNumber(6, 7);   // 6 + 7i (duplikat z1)
        var z4 = new ComplexNumber(1, -2);  // 1 - 2i
        var z5 = new ComplexNumber(-5, 9);  // -5 + 9i

        // =========================
        // 2. TABLICA
        // =========================
        Console.WriteLine("=== 2. TABLICA ===");

        ComplexNumber[] array = new ComplexNumber[]
        {
            new ComplexNumber(3, 4),
            new ComplexNumber(1, -2),
            new ComplexNumber(-5, 9),
            new ComplexNumber(0, -7),
            new ComplexNumber(2, 0)
        };

        // 2a. Wypisz je wykorzystując foreach
        Console.WriteLine("Tablica – oryginalna:");
        foreach (var z in array)
            Console.WriteLine(z);

        // 2b. Sortowanie po module (dzięki IComparable)
        Array.Sort(array); // używa CompareTo => po Module()
        Console.WriteLine("\nTablica – po sortowaniu (po module):");
        foreach (var z in array)
            Console.WriteLine($"{z}  |Z|={z.Module():F2}");

        // 2c. Minimum i maksimum tablicy
        var minArray = array.Min(); // wymaga IComparable
        var maxArray = array.Max();
        Console.WriteLine($"\nMinimum w tablicy: {minArray}  |Z|={minArray.Module():F2}");
        Console.WriteLine($"Maksimum w tablicy: {maxArray}  |Z|={maxArray.Module():F2}");

        // 2d. Odfiltruj liczby o ujemnej części urojonej
        Console.WriteLine("\nTablica – liczby z nieujemną częścią urojoną (Im >= 0):");
        var filteredArray = array.Where(z => z.Im >= 0);
        foreach (var z in filteredArray)
            Console.WriteLine(z);

        // =========================
        // 3. LISTA
        // =========================
        Console.WriteLine("\n=== 3. LISTA ===");

        var list = new List<ComplexNumber>
        {
            new ComplexNumber(3, 4),
            new ComplexNumber(1, -2),
            new ComplexNumber(6, 7),
            new ComplexNumber(1, 2),
            new ComplexNumber(-5, 9)
        };

        Console.WriteLine("Lista – oryginalna:");
        foreach (var z in list)
            Console.WriteLine(z);

        // Te same operacje co na tablicy:
        // sortowanie:
        list.Sort(); // po module
        Console.WriteLine("\nLista – po sortowaniu (po module):");
        foreach (var z in list)
            Console.WriteLine($"{z}  |Z|={z.Module():F2}");

        // min / max
        var minList = list.Min();
        var maxList = list.Max();
        Console.WriteLine($"\nMinimum w liście: {minList}  |Z|={minList.Module():F2}");
        Console.WriteLine($"Maksimum w liście: {maxList}  |Z|={maxList.Module():F2}");

        // filtrowanie
        Console.WriteLine("\nLista – liczby z nieujemną częścią urojoną (Im >= 0):");
        var filteredList = list.Where(z => z.Im >= 0);
        foreach (var z in filteredList)
            Console.WriteLine(z);

        // 3a. Usuń drugi element z listy (indeks 1)
        if (list.Count > 1)
        {
            list.RemoveAt(1);
        }
        Console.WriteLine("\nLista po usunięciu drugiego elementu:");
        foreach (var z in list)
            Console.WriteLine(z);

        // 3b. Usuń najmniejszy element z listy
        if (list.Count > 0)
        {
            var minElement = list.Min();
            list.Remove(minElement);
        }
        Console.WriteLine("\nLista po usunięciu najmniejszego elementu:");
        foreach (var z in list)
            Console.WriteLine(z);

        // 3c. Usuń wszystkie elementy z listy
        list.Clear();
        Console.WriteLine("\nLista po wyczyszczeniu (Count): " + list.Count);

        // =========================
        // 4. HASHSET
        // =========================
        Console.WriteLine("\n=== 4. HASHSET ===");
        var set = new HashSet<ComplexNumber>
        {
            z1, // 6+7i
            z2, // 1+2i
            z3, // 6+7i (duplikat z1)
            z4, // 1-2i
            z5  // -5+9i
        };

        // 4a. Zawartość zbioru
        Console.WriteLine("Zbiór (HashSet) – elementy:");
        foreach (var z in set)
            Console.WriteLine(z);
        // Uwaga: z1 i z3 są równe, więc w zbiorze będzie tylko jedna kopia

        // 4b. Czy możemy wykonać min, max, sortowanie, filtrowanie?
        var minSet = set.Min(); // działa dzięki IComparable
        var maxSet = set.Max();
        Console.WriteLine($"\nMinimum w zbiorze: {minSet}  |Z|={minSet.Module():F2}");
        Console.WriteLine($"Maksimum w zbiorze: {maxSet}  |Z|={maxSet.Module():F2}");

        Console.WriteLine("\nZbiór – posortowany (po module):");
        var sortedSet = set.OrderBy(z => z); // domyślnie po CompareTo (module)
        foreach (var z in sortedSet)
            Console.WriteLine($"{z}  |Z|={z.Module():F2}");

        Console.WriteLine("\nZbiór – liczby z nieujemną Im (Im >= 0):");
        var filteredSet = set.Where(z => z.Im >= 0);
        foreach (var z in filteredSet)
            Console.WriteLine(z);

        // =========================
        // 5. SŁOWNIK
        // =========================
        Console.WriteLine("\n=== 5. SŁOWNIK ===");
        var dict = new Dictionary<string, ComplexNumber>
        {
            ["z1"] = z1,
            ["z2"] = z2,
            ["z3"] = z3,
            ["z4"] = z4,
            ["z5"] = z5
        };

        // 5a. Wypisz (klucz, wartość)
        Console.WriteLine("Słownik – (klucz, wartość):");
        foreach (var kvp in dict)
            Console.WriteLine($"{kvp.Key} -> {kvp.Value}");

        // 5b. Osobno wszystkie klucze i wszystkie wartości
        Console.WriteLine("\nKlucze w słowniku:");
        foreach (var key in dict.Keys)
            Console.WriteLine(key);

        Console.WriteLine("\nWartości w słowniku:");
        foreach (var value in dict.Values)
            Console.WriteLine(value);

        // 5c. Czy istnieje klucz "z6"?
        Console.WriteLine("\nCzy słownik zawiera klucz \"z6\"? " + dict.ContainsKey("z6"));

        // 5d. Zrób 2c i 2d na słowniku (na wartościach)
        var minDict = dict.Values.Min();
        var maxDict = dict.Values.Max();
        Console.WriteLine($"\nMinimum (wartości słownika): {minDict}  |Z|={minDict.Module():F2}");
        Console.WriteLine($"Maksimum (wartości słownika): {maxDict}  |Z|={maxDict.Module():F2}");

        Console.WriteLine("\nWartości słownika z nieujemną Im (Im >= 0):");
        var filteredDictValues = dict.Where(kvp => kvp.Value.Im >= 0);
        foreach (var kvp in filteredDictValues)
            Console.WriteLine($"{kvp.Key} -> {kvp.Value}");

        // 5e. Usuń element o kluczu "z3"
        dict.Remove("z3");
        Console.WriteLine("\nSłownik po usunięciu klucza \"z3\":");
        foreach (var kvp in dict)
            Console.WriteLine($"{kvp.Key} -> {kvp.Value}");

        // 5f. Usuń drugi element ze słownika (wg kolejności enumeracji)
        if (dict.Count > 1)
        {
            string secondKey = dict.Keys.Skip(1).First();
            dict.Remove(secondKey);
        }
        Console.WriteLine("\nSłownik po usunięciu drugiego elementu:");
        foreach (var kvp in dict)
            Console.WriteLine($"{kvp.Key} -> {kvp.Value}");

        // 5g. Wyczyść słownik
        dict.Clear();
        Console.WriteLine("\nSłownik po wyczyszczeniu – Count: " + dict.Count);

        // 6. Kolejka i stos – minimalny przykład
        Console.WriteLine("\n=== 6. KOLEJKA i STOS (krótki przykład) ===");

        // Kolejka (FIFO)
        Queue<ComplexNumber> queue = new Queue<ComplexNumber>();
        queue.Enqueue(new ComplexNumber(1, 1));
        queue.Enqueue(new ComplexNumber(2, 2));
        Console.WriteLine("Kolejka – Dequeue:");
        while (queue.Count > 0)
            Console.WriteLine(queue.Dequeue());

        // Stos (LIFO)
        Stack<ComplexNumber> stack = new Stack<ComplexNumber>();
        stack.Push(new ComplexNumber(3, 3));
        stack.Push(new ComplexNumber(4, 4));
        Console.WriteLine("Stos – Pop:");
        while (stack.Count > 0)
            Console.WriteLine(stack.Pop());

        Console.WriteLine("\nKoniec programu. Naciśnij Enter...");
        Console.ReadLine();
    }
}
