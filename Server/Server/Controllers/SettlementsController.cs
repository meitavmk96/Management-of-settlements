using Microsoft.AspNetCore.Mvc;
using Server.Models;
using static DBservices;

//For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SettlementsController : ControllerBase
    {
        // GET: api/<SettlementsController>
        [HttpGet]
        public IEnumerable<Settlement> Get()
        {
            Settlement settlement = new Settlement();
            return settlement.Read();
        }

        // POST api/<SettlementsController>
        [HttpPost]
        public IActionResult Post([FromBody] Settlement settlement)
        {
            InsertResult result = settlement.Insert(out int id);
            switch (result)
            {
                case InsertResult.Duplicate:
                    return BadRequest("רשומה כבר קיימת במערכת");
                case InsertResult.Error:
                    return BadRequest("בעיה בשרת");
                case InsertResult.Succses:
                default:
                    settlement.Id = id;
                    return Ok(settlement);
            }
        }

        // PUT api/<SettlementsController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Settlement settlement)
        {
            settlement.Id = id;
            InsertResult result = settlement.Update();
            switch (result)
            {
                case InsertResult.Duplicate:
                    return BadRequest("רשומה כבר קיימת במערכת");
                case InsertResult.Error:
                    return BadRequest("בעיה בשרת");
                case InsertResult.Succses:
                default:
                    settlement.Id = id;
                    return Ok(settlement);
            }
        }


        // DELETE api/<SettlementsController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Settlement settlement = new Settlement();
            int numAffected = settlement.Delete(id);
            if (numAffected == 1)
                return Ok(id);
            else
                return NotFound("Id " + id.ToString() + " was not found");
        }
    }
}
