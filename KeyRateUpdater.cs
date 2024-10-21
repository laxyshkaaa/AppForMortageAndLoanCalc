using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

public class KeyRateUpdater
{
    private static readonly string cbrApiUrl = "https://www.cbr-xml-daily.ru/daily_json.js"; 

    public async Task<double> GetKeyRate()
    {
        using (HttpClient client = new HttpClient())
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync(cbrApiUrl);
                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();
                var json = JObject.Parse(responseBody);

                
                double keyRate = json["Valute"]["USD"]["Value"].ToObject<double>();  
                return keyRate;
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("Ошибка при получении данных: " + e.Message);
                return -1;  // Вернуть -1 в случае ошибки
            }
        }
    }
}
