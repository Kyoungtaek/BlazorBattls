using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorBattles.Server.Data;
using BlazorBattles.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlazorBattles.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UnitController : ControllerBase
    {
        private readonly DataContext context;
        public UnitController(DataContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetUnits()
        {
            var units = await context.Units.ToListAsync();

            return Ok(units);
        }

        [HttpPost]
        public async Task<IActionResult> AddUnit(Unit unit)
        {
            await context.Units.AddAsync(unit);
            await context.SaveChangesAsync();

            return Ok(await context.Units.ToListAsync());
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUnit(int id, Unit unit)
        {
            Unit dbUnit = await context.Units.FirstOrDefaultAsync(x => x.Id == id);

            if(dbUnit == null)
            {
                return NotFound("Unit with the ginven id doesn't exist.");
            }

            dbUnit.Title = unit.Title;
            dbUnit.Attack = unit.Attack;
            dbUnit.Defense = unit.Defense;
            dbUnit.BananaCost = unit.BananaCost;
            dbUnit.HitPoints = unit.HitPoints;

            await context.SaveChangesAsync();

            return Ok(dbUnit);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> UpdateUnit(int id)
        {
            Unit dbUnit = await context.Units.FirstOrDefaultAsync(x => x.Id == id);

            if (dbUnit == null)
            {
                return NotFound("Unit with the ginven id doesn't exist.");
            }

            context.Units.Remove(dbUnit);

            await context.SaveChangesAsync();

            return Ok(await context.Units.ToListAsync());
        }
    }
}
