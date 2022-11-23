using GoogleGson;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using Newtonsoft.Json.Linq;

namespace EncounterBuilderApp;

public partial class MainPage : ContentPage
{

    public struct Monster
    {
        public string Name, Size, Type, ChallengeRating;
        public string SpeedJson;

        public Monster(string name, string size, string type, string challengerating, string speedjson)
        {
            Name = name;
            Size = size;
            Type = type;
            ChallengeRating = challengerating;
            SpeedJson = speedjson; // Walk, Swim, Burrow, Fly
        }
    }

    static List<Monster> monsters = new List<Monster>();

    public MainPage()
	{
        /*
        if (Connectivity.Current.NetworkAccess == NetworkAccess.None)
        {
            DisplayAlert("Connection Alert!", "You don't have connection to the ethernet!", "OK");
            Application.Current?.CloseWindow(Application.Current.MainPage.Window);
        }
        */
        InitializeComponent();
        RequestApiData("https://api.open5e.com/monsters/");
        
    }

    static readonly HttpClient client = new HttpClient();
    async public void RequestApiData(string startUrl) {
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        string json;
        try
        {
            HttpResponseMessage response = await client.GetAsync(startUrl);
            response.EnsureSuccessStatusCode(); // Check that the status code is in the 200 range. Throw an HttpRequestException if not
            string responseBody = await response.Content.ReadAsStringAsync();
            // Handle the response
            for (int i = 1; i < 31; i++) // API has 30 pages full of json -> resource path //?page=2
            {
                string temporaryUrl = startUrl + "?page=" + i;
                json = await client.GetStringAsync(temporaryUrl); // StringJson body
                var obj = JObject.Parse(json);
                foreach (var monster in obj["results"])
                {
                    monsters.Append(new Monster(Convert.ToString(monster["name"]), Convert.ToString(monster["size"]), Convert.ToString(monster["type"]), Convert.ToString(monster["challenge_rating"]), Convert.ToString(monster["speed"])));
                    // elvileg beolvassa (nem biztos), de nagyon lassu a convert miatt 
                    // a struktura helyett osztályt kellene használnom
                }
            }
        }
        catch (HttpRequestException e)
        {
            // Handle eny status code that indicates error.
            // Code available from e.StatusCode
        }


        //Console.WriteLine(text);
    }

}

