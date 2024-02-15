using Microsoft.AspNetCore.Mvc;
using NotDel.Controllers;
using NotDel;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Zombi;
using Microsoft.Identity.Client;
using System.IO.Compression;
using System.ComponentModel.Design;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore;

namespace ApiKR2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ZombieController : ControllerBase
    {
        private readonly User16Context _context;

        public ZombieController(User16Context context)
        {
            _context = context;
        }

        [HttpPost("AddZondri")]
        public async void AddZombie(ZombieDTO zombie)
        {
            _context.Add(new Zombie
            {
                Name = zombie.Name,
                Description = zombie.Description,
                Health = zombie.Health,
                ZombieTypeId = zombie.ZombieTypeId
            });
            _context.SaveChanges();
        }
        [HttpGet("GetZondri")]
        public async Task<ActionResult<List<ZombieDTO>>>GetZombie()
        {
            List<ZombieDTO> zombies = _context.Zombies.Include(s => s.ZombieType).ToList().Select(s => new ZombieDTO 
            { Id = s.Id, Description = s.Description, Health = s.Health,
                ZombieTypeId = s.ZombieTypeId, Name = s.Name, ZombieType = s.ZombieType.Name}).ToList();
            return zombies;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteZombie(int id)
        {
            if (_context.Zombies == null)
            {
                return NotFound();
            }
            var zombie = await _context.Zombies.FindAsync(id);
            if (zombie == null)
            {
                return NotFound();
            }

            _context.Zombies.Remove(zombie);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Zombie>> EditZombie(int id, ZombieDTO zombieDTO)
        {
            // Проверка, что переданные данные валидны
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Поиск существующего зомби в базе данных
            var existingZombie = await _context.Zombies.FirstOrDefaultAsync(s => s.Id == id);
            if (existingZombie == null)
            {
                return NotFound();
            }

            // Обновление свойств существующего зомби
            existingZombie.Name = zombieDTO.Name;
            existingZombie.Description = zombieDTO.Description;
            existingZombie.Health = zombieDTO.Health;
            existingZombie.ZombieTypeId = zombieDTO.ZombieTypeId;
          

            
            _context.Entry(existingZombie).State = EntityState.Modified;

            try
            {
                // Сохранение изменений в базе данных
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ZombieExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            // Возвращение обновленного объекта
            return NoContent();
        }


        private bool ZombieExists(int id)
        {
            return (_context.Zombies?.Any(e => e.Id == id)).GetValueOrDefault();
        }


    }
}
