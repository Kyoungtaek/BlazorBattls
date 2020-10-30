using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace BlazorBattles.Client.Services
{
    public class BananaService : IBananaService
    {
        private readonly HttpClient http;

        public BananaService(HttpClient http)
        {
            this.http = http;
        }
        public event Action OnChange;
        public int Bananas { get; set; } = 0;

        public void EatBananas(int amount)
        {
            Bananas -= amount;
            BananasChanged();
        }
        public void AddBananas(int amount)
        {
            Bananas += amount;
            BananasChanged();
        }
        void BananasChanged() => OnChange.Invoke();

        public async Task GetBananas()
        {
            Bananas = await http.GetFromJsonAsync<int>("api/User/GetBananas");
            BananasChanged();
        }
    }
}
