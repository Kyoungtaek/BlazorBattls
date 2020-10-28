using BlazorBattles.Shared;
using Blazored.Toast.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace BlazorBattles.Client.Services
{
    public class UnitService : IUnitService
    {
        private readonly IToastService toastService;
        private readonly HttpClient http;
        public UnitService(IToastService toastService, HttpClient http)
        {
            this.toastService = toastService;
            this.http = http;
        }
        public IList<Unit> Units { get; set; } = new List<Unit>();

        public IList<UserUnit> MyUnits { get; set; } = new List<UserUnit>();

        public void AddUnit(int unitId)
        {
            Unit unit = Units.First(x => x.Id == unitId);

            MyUnits.Add(new UserUnit { UnitId = unit.Id, HitPoints = unit.HitPoints });
            toastService.ShowSuccess($"Your {unit.Title} has been built!", "Unit built!");
        }

        public async Task LoadUnitsAsync()
        {
            if (Units.Count == 0)
            {
                Units = await http.GetFromJsonAsync<IList<Unit>>("api/Unit");
            }
        }
    }
}
