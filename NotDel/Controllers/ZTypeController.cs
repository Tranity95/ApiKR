using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Zombi;

namespace ApiKR2.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ZTypeController : ControllerBase
    {
        private readonly User16Context context;

        public ZTypeController(User16Context context) 
        {
            this.context = context;
        }

        [HttpPost("AddZType")]
        public async void AddZType(ZombieTypeDTO zombieType)
        {
            context.ZombieTypes.Add(new ZombieType
            {
                Name = zombieType.Name
            });
            context.SaveChanges();
        }
        [HttpGet("GetZTypes")]
        public async Task<ActionResult<List<ZombieTypeDTO>>> GetZTypes()
        {
            List<ZombieTypeDTO> zombieTypes = context.ZombieTypes.ToList().Select(s=>new ZombieTypeDTO { Id = s.Id, Name = s.Name }).ToList();
            return zombieTypes;
        }

    }
}
