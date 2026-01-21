using CDN.WebApi.Application.Repository;
using CDN.WebApi.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Swashbuckle.AspNetCore.Annotations;


namespace CDN.WebApi.Controllers
{
    [Authorize] //all the APIs under this FreelancersController will be secured with the token
    [Route("api/[controller]")]
    [ApiController]
    public class FreelancersController : ControllerBase
    {
        private readonly IFreelancerService _freelancerService;

        public FreelancersController(IFreelancerService freelancerService) // Inject Dependency as Constructor Injection
        {
            _freelancerService = freelancerService;
          
        }


        [HttpGet]
        public async Task<IActionResult> GetFreelancers()
        {
            try
            {
                var data = await _freelancerService.GetFreelancers();

                if (data.Count() != 0 && data != null)
                {
                    return Ok(data);
                }
                else
                {
                    return Ok("Null");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // GET api/<FreelancersController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetFreelancer(int id)
        {
            try
            {
                if (id != 0)
                {
                    var freelancer = await _freelancerService.GetFreelancerById(id);
                    if (freelancer != null)
                    {
                        return Ok(freelancer);
                    }
                    else
                    {
                        return NotFound();
                    }
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }

        // POST api/<FreelancersController>
        [HttpPost]
        [SwaggerResponse(StatusCodes.Status201Created, "Submit Data successfully")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "Unauthorized, Request")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Can't get Post right now")]
        public async Task<IActionResult> PostFreelancer([FromBody] FreelancerDTO freelancerDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                  
                }

                var exist = await _freelancerService.ExistFreelancer(freelancerDTO.Username);
                if (!exist)
                {
                    var newfreelancer = await _freelancerService.PostFreelancer(freelancerDTO);

                    return CreatedAtAction(nameof(GetFreelancer),
                    new { id = newfreelancer.ID }, newfreelancer);
                }
                else
                {
                    return Ok("User already Exist");
                }
            }
            catch (Exception ex)
            {
              
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error creating new freelancer record");
            }
           
            
        }

        // PUT api/<FreelancersController>/5
        [HttpPut("{id}")]
        [SwaggerResponse(StatusCodes.Status200OK, "Update Data successfully")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "Unauthorized, Request")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Can't get Post right now")]
        public async Task<IActionResult> PutFreelancer(int id, [FromBody] FreelancerDTO freelancerDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);

                }
                var freelancer = await _freelancerService.PutFreelancer(id, freelancerDTO);
                if (freelancer!=null)
                {
                   
                    return Ok("Updated Successfully");
                }
                else
                {
                    return NotFound();
                }


            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error updating existing freelancer record");
            }


        }

        // DELETE api/<FreelancersController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (await _freelancerService.DeleteFreelancer(id))
            {
               
                return Ok("The Freelancer has been Deleted");
            }
            else
                return NotFound("Freelancer not found");
        }
    }
}
