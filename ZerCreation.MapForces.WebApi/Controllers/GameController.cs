using Microsoft.AspNetCore.Mvc;
using ZerCreation.MapForces.WebApi.Dtos;
using ZerCreation.MapForcesEngine;

namespace ZerCreation.MapForces.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly EngineDispatcher engineDispatcher;

        public GameController(EngineDispatcher engineDispatcher)
        {
            this.engineDispatcher = engineDispatcher;
        }

        [HttpGet]
        public ActionResult<string> Get()
        {
            return "Map Force game API.";
        }

        [HttpPost("init")]
        public ActionResult<string> Initialize([FromBody] GameInitDto gameInitDto)
        {
            this.engineDispatcher.BuildMap(gameInitDto.MapName);

            return "Game was created.";
        }
    }
}