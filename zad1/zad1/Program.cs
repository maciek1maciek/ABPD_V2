using System.Text.RegularExpressions;
using zad1;

 
if (args.Length < 1) throw new ArgumentNullException(); //sprawdzenie ilosci argumentow

bool isValidUrl = Uri.IsWellFormedUriString(args[0], UriKind.Absolute); 
if (isValidUrl != true) throw new ArgumentException(); //sprawdzenie URL


var url = args[0];
var httpClient = new HttpClient();

var response = await httpClient.GetAsync(url);

var codeResponse = (int)response.StatusCode;
if (codeResponse < 200 && codeResponse > 299) throw new  Exception("Błąd w czasie pobierania strony"); //sprawdzenie kodu odpowiedzi
 
string content = await response.Content.ReadAsStringAsync();

var regex = new Regex(@"[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+");

MatchCollection matches = regex.Matches(content);
if (matches.Count == 0) throw new Exception("NIe znaleziono adresów"); //sprawdzenie czy znaleziono jakies adresy

List<string> uniqueMatches = new List<string>(); //usuniecie dupliaktów
foreach (Match match in matches)
{
    string value = match.Value;
    if (!uniqueMatches.Contains(value))
    {
        uniqueMatches.Add(value);
    }
}

// Wyświetlenie unikalnych dopasowań.
foreach (string match in uniqueMatches)
{
    Console.WriteLine(match);
}
 