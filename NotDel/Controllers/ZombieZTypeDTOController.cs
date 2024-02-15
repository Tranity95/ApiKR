using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;
using Zombi;

namespace ApiKR2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ZombieZTypeDTOController : ControllerBase
    {
        private readonly User16Context _context;

        public ZombieZTypeDTOController(User16Context context)
        {
            _context = context;
        }

        [HttpPost("PostZTypes")]
        public async Task<ActionResult<ZombieTypeDTO>> GetZombieTypeDTO(ZombieDTO zombieDTO)
        {
            ZombieTypeDTO zombieTypeDTO = GetZombieTypeById(zombieDTO.ZombieTypeId);
            if (zombieTypeDTO == null)
                return NotFound();
            return Ok(zombieTypeDTO);
        }

        private List<ZombieDTO> GetZombiesByType(ZombieTypeDTO zombieTypeDTO)
        {
            var zombies = _context.Zombies.Where(s => s.ZombieTypeId == zombieTypeDTO.Id).ToList();
            var ZombiesDTO = zombies.Select(s => new ZombieDTO
            {
                Id = Convert.ToInt32(s.Id),
                Name = s.Name,
                Description = s.Description,
                Health = s.Health,
                ZombieTypeId = s.ZombieTypeId
            }).ToList();
            return ZombiesDTO;
        }

        private ZombieTypeDTO GetZombieTypeById(int zombieTypeId)
        {
            var zombieType = _context.ZombieTypes.FirstOrDefault(s => s.Id == zombieTypeId);
            if (zombieType == null)
                return null;
            var zombieTypeDTO = new ZombieTypeDTO
            {
                Id = zombieTypeId,
                Name = zombieType.Name

            };
            return zombieTypeDTO;
        }

        [HttpPost("GetZombies")]
        public async Task<ActionResult<List<ZombieDTO>>> GetZombieDTO(ZombieTypeDTO zombieTypeDTO)
        {
            List<ZombieDTO> zombies = GetZombiesByType(zombieTypeDTO);
            if (zombies == null)
                return NotFound();
            return Ok(zombies);
        }
        
    }
}
