using Newtonsoft.Json; // Biblioteka do obsługi operacji JSON, w tym deserializacji i serializacji danych JSON.
using System; // Przestrzeń nazw zawierająca ogólne typy i funkcje języka C#.
using System.Net.Http; // Biblioteka do wykonywania operacji z wykorzystaniem protokołu HTTP.
using System.Threading.Tasks; // Biblioteka do obsługi asynchronicznych operacji i zadań w języku C#.


public class ApiService
{
    private readonly HttpClient _httpClient; // Obiekt HttpClient służący do wykonywania żądań HTTP.
    private readonly string _apiKey = "lunb9s4mgS2Q3xxxP8h8uPOvNUQjq0Gt2KdRuKC2"; //klucz API
    public string category_name;  // Nazwa kategorii
    public string category = "";   // Parametr kategorii dla zapytania API
    public string difficulty_name;  // Nazwa trudności
    public string difficulty = "";   // Parametr trudności dla zapytania API
    public int limit_number;  // Limit ilości pytań
    public string limit = "";   // Parametr limitu dla zapytania API
    public string[] Tags;  // Tablica tagów
    public string tag = "";   // Parametr tagów dla zapytania API

    public ApiService(HttpClient httpClient) //Konstruktor klasy ApiService
    {
        _httpClient = httpClient; // Konstruktor klasy ApiService, inicjalizuje pole _httpClient za pomocą dostarczonego obiektu HttpClient.
    }

    public async Task<QuestionModel[]> GetQuestionsAsync() // Pobiera pytania z API na podstawie wybranych parametrów i zwraca je jako tablicę QuestionModel.
    {
        if (category_name != null)
        {
            category = "&category=" + category_name;  // Dodanie parametru kategorii do URL
        }
        if (difficulty_name != null)
        {
            difficulty = "&difficulty=" + difficulty_name;  // Dodanie parametru trudności do URL
        }
        if (limit_number != null)
        {
            limit = "&limit=" + limit_number.ToString();  // Dodanie parametru limitu do URL
        }
        if (Tags != null)
        {
            tag = "&tags=";
            for (int i = 0; i < Tags.Length; i++)
            {
                if (i != Tags.Length - 1)
                {
                    tag += Tags[i] + ",";  // Tworzenie ciągu tagów rozdzielonych przecinkami
                }
                else
                {
                    tag += Tags[i];
                }
            }
        }

        // Tworzenie pełnego URL z parametrami
        HttpResponseMessage response = await _httpClient.GetAsync("https://quizapi.io/api/v1/questions?apiKey=" + _apiKey + category + difficulty + limit + tag);

        if (response.IsSuccessStatusCode) // Sprawdza, czy odpowiedź HTTP ma status sukcesu, co oznacza, że żądanie zostało pomyślnie obsłużone.
        {
            var content = await response.Content.ReadAsStringAsync(); // Odczytuje zawartość odpowiedzi HTTP jako ciąg znaków.
            var questions = JsonConvert.DeserializeObject<QuestionModel[]>(content);  // Deserializacja danych JSON do obiektu

            return questions; // Zwraca deserializowane pytania w postaci tablicy obiektów QuestionModel.
        }

        return null;
    }
}

