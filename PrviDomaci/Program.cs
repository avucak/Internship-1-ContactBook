using System;
using System.Collections.Generic;
namespace PrviDomaci
{
    class Program
    {
        static void Main(string[] args)
        {
            var addressBook = new Dictionary<string, Tuple<string, string, string>>();
            Console.WriteLine("Unesite 1 za dodavanje novog upisa" +
                "\n2 za promjenu imena,adrese ili broja \n3 za brisanje upisa" +
                "\n4 za pretragu po broju \n5 za pretragu po imenu" +
                "\ni 6 za izlazak iz programa");
            int choice;
            string number, name, surname, address;
            var searchResults = new List<Tuple<string, string, string, string>>();

            do
            {
                Console.WriteLine("Odaberite opciju: ");
                choice = int.Parse(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        Console.WriteLine("Unesite ime:");
                        name = Console.ReadLine();
                        Console.WriteLine("Unesite prezime:");
                        surname = Console.ReadLine();
                        Console.WriteLine("Unesite adresu:");
                        address = Console.ReadLine();
                        Console.WriteLine("Unesite broj:");
                        number = Console.ReadLine();
                        number = RemoveExceptNumbers(number);
                        Console.WriteLine("Ponovno unesite isti broj za potvrdu odabira: ");
                        if (RemoveExceptNumbers(Console.ReadLine()) != number)
                        {
                            Console.WriteLine("Brojevi se ne podudaraju, podatci nisu upisani.");
                            break;
                        }
                        addressBook[number] = new Tuple<string, string, string>(name, surname, address);
                        break;
                    case 2:
                        Console.WriteLine("Za koji broj želite mijenjati podatke?: ");
                        var oldNumber = Console.ReadLine();
                        Console.WriteLine("Ponovno unesite isti broj za potvrdu odabira: ");
                        if (RemoveExceptNumbers(Console.ReadLine()) != RemoveExceptNumbers(oldNumber))
                        {
                            Console.WriteLine("Brojevi se ne podudaraju, mijenjanje podataka zaustavljeno.");
                            break;
                        }
                        if (addressBook.ContainsKey(oldNumber))
                        {
                            Console.WriteLine("Unesite novo ime:");
                            name = Console.ReadLine();
                            Console.WriteLine("Unesite novo prezime:");
                            surname = Console.ReadLine();
                            Console.WriteLine("Unesite novu adresu:");
                            address = Console.ReadLine();
                            Console.WriteLine("Unesite novi broj:");
                            number = Console.ReadLine();
                            number = RemoveExceptNumbers(number);
                            Console.WriteLine("Ponovno unesite isti broj za potvrdu odabira: ");
                            if (RemoveExceptNumbers(Console.ReadLine()) != number)
                            {
                                Console.WriteLine("Brojevi se ne podudaraju, mijenjanje podataka zaustavljeno.");
                                break;
                            }
                            addressBook.Remove(oldNumber);
                            addressBook[number] = new Tuple<string, string, string>(name, surname, address);

                        }
                        else { Console.WriteLine("Taj broj ne postoji!"); }
                        break;
                    case 3:
                        Console.WriteLine("Za koji broj želite brisati podatke?: ");
                        number = Console.ReadLine();
                        number = RemoveExceptNumbers(number);
                        Console.WriteLine("Ponovno unesite isti broj za potvrdu odabira: ");
                        if (RemoveExceptNumbers(Console.ReadLine()) != number)
                        {
                            Console.WriteLine("Brojevi se ne podudaraju, brisanje zaustavljeno.");
                            break;
                        }
                        if (addressBook.ContainsKey(number))
                        {
                            addressBook.Remove(number);
                            Console.WriteLine("Podaci obrisani.");
                        }
                        else
                            Console.WriteLine("Taj broj ne postoji u imeniku.");
                        break;
                    case 4:
                        Console.WriteLine("Za koji broj želite pronaći informacije? ");
                        number = Console.ReadLine();
                        if (addressBook.ContainsKey(number))
                        {
                            Console.WriteLine($"Ime: {addressBook[number].Item1} Prezime:{addressBook[number].Item2} Adresa:{addressBook[number].Item3} ");
                        }
                        else
                            Console.WriteLine("Taj broj ne postoji u imeniku.");
                        break;
                    case 5:
                        Console.WriteLine("Za koje ime želite pronaći informacije? ");
                        name = Console.ReadLine();
                        searchResults.Clear();
                        Tuple<string, string, string, string> result;
                        foreach (var item in addressBook)
                        {
                            if (item.Value.Item1.ToLower().StartsWith(name.ToLower()) || item.Value.Item2.ToLower().StartsWith(name.ToLower()))
                            {
                                result = new Tuple<string, string, string, string>(item.Key, item.Value.Item1, item.Value.Item2, item.Value.Item3);
                                searchResults.Add(result);
                            }
                        }
                        SortByName(searchResults);
                        foreach (var item in searchResults)
                        {
                            Console.WriteLine($"Ime: {item.Item2} Prezime: {item.Item3} Adresa: {item.Item4} Broj: {item.Item1}");
                        }
                        if (searchResults.Count == 0)
                            Console.WriteLine("U imeniku ne postoje podatci za to ime.");
                        break;
                    default:
                        break;

                }
            }
            while (choice != 6);
        }
        static string RemoveExceptNumbers(string number)
        {
            var withRemoved = "";
            foreach (var c in number)
                if (System.Char.IsDigit(c))
                    withRemoved += c;
            return withRemoved;
        }

        static void SortByName(List<Tuple<string, string, string, string>> list)
        {
            list.Sort((x, y) => x.Item2.ToUpper().CompareTo(y.Item2.ToUpper()));
            list.Sort((x, y) => x.Item3.ToUpper().CompareTo(y.Item3.ToUpper()));

        }
    }
}
