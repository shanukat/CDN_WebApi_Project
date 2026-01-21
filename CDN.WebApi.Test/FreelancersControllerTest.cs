using CDN.WebApi.Application.Repository;
using CDN.WebApi.Controllers;
using CDN.WebApi.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Operations;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Hosting;
using Moq;
using NuGet.Protocol.Core.Types;
using Xunit;

namespace CDN.WebApi.Test
{
    public class FreelancersControllerTest
    {      //Unit tests should only test:

            //Controller logic
            //Response types
            //Model validation
            //Branching logic

            //Unit tests should NOT depend on:

            //Real database
            //Real service implementation
            //Real network


        private readonly FreelancersController _controller;
        private readonly Mock<IFreelancerService> _service;


        public FreelancersControllerTest()
        {
            //Mock the service
            _service = new Mock<IFreelancerService>();// Moq is use to stop the use of real services in unit tests. The real Service calls the real DB
            _controller = new FreelancersController(_service.Object);

        }

        #region Get All 
        [Fact]
        public async Task GetAll_Freelancers_Return_OkResult()
        {
            //Arrange

            var freelancerList = new List<FreelancerDTO>
                {
                  new FreelancerDTO { ID = 1, Username = "A", Mail = "shanukato@gmail.com" },
                  new FreelancerDTO { ID = 2, Username = "B", Mail = "kausn@gmail.com" },
                };
            
            _service.Setup(s => s.GetFreelancers()).ReturnsAsync(freelancerList);

            //Act

            var result = await _controller.GetFreelancers();

            //Assert

            var ok = Assert.IsType<OkObjectResult>(result);
            var value = Assert.IsAssignableFrom<IEnumerable<FreelancerDTO>>(ok.Value);
            Assert.Equal(2, value.Count());

        }


        [Fact]
        public async Task GetAll_Freelancers_Return_BadRequestResult()
        {
            //Arrange

            //Act
            _service.Setup(s => s.GetFreelancers())
                    .ReturnsAsync(new List<FreelancerDTO>());

            var result = await _controller.GetFreelancers();

            //Assert

            var ok = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("Null", ok.Value);
        }

        

        #endregion

        #region Get By Id 

        [Fact]
        public async Task GetFreelancer_ById_Return_OkResult()
        {
            //Arrange
            int valid_id = 5;
            var dto = new FreelancerDTO { ID = 5, Username = "Testing 5" };
            _service.Setup(s => s.GetFreelancerById(valid_id)).ReturnsAsync(dto);

            //Act
            var result = await _controller.GetFreelancer(valid_id);

            //Assert  
            var ok = Assert.IsType<OkObjectResult>(result);
            var freelancer = Assert.IsType<FreelancerDTO>(ok.Value);
            Assert.Equal(5, freelancer.ID);
        }

        [Fact]
        public async Task GetFreelancerById_Return_NotFoundResult()
        {
            //Arrange  
            int invalid_id = 59;

            _service.Setup(s => s.GetFreelancerById(59))
                    .ReturnsAsync((FreelancerDTO)null!);

            //Act  
            var result = await _controller.GetFreelancer(invalid_id);

            //Assert  
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task GetFreelancerById_Return_BadRequestResult()
        {
            //Arrange  
            
            int? id = null;

            //Act  
             var data = await _controller.GetFreelancer(id.GetValueOrDefault());

            //Assert  
            Assert.IsType<BadRequestResult>(data);

        }
        #endregion

        #region Add New Freelancer
        [Fact]
        public async Task Add_ValidData_Return_Success()
        {
            //Arrange  
           
            var new_freelancer = new FreelancerDTO() { ID = 15, Username = "Shanuka perera", Mail = "shanukato@gmail.com", PhoneNumber = "+601112262178",Skillsets = "C#, ASP.NET CORE, SQL", Hobby = "Programming"  };

            //Act  

            _service.Setup(s => s.ExistFreelancer(new_freelancer.Username)).ReturnsAsync(false);
            _service.Setup(s => s.PostFreelancer(new_freelancer)).ReturnsAsync(new_freelancer);

            var result = await _controller.PostFreelancer(new_freelancer);

            //Assert  

            var created = Assert.IsType<CreatedAtActionResult>(result);
            var dto = Assert.IsType<FreelancerDTO>(created.Value);
            Assert.Equal("Shanuka perera", dto.Username);
            Assert.Equal("shanukato@gmail.com", dto.Mail);
        }

        [Fact]
        public async Task PostFreelancer_WhenUserExists_Returns_Ok_String()
        {
            var input = new FreelancerDTO { Username = "ExistingUser" };

            _service.Setup(s => s.ExistFreelancer("ExistingUser")).ReturnsAsync(true);

            var result = await _controller.PostFreelancer(input);

            var ok = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("User already Exist", ok.Value);
        }


       

        [Fact]
        public async Task Add_EmptyData_Return_BadRequest()
        {
            //Arrange  
            var new_freelancer = new FreelancerDTO() { Username = "" };
            _controller.ModelState.AddModelError("Username", "Required");
            //Act              
            var data = await _controller.PostFreelancer(new_freelancer);

            //Assert  
            Assert.IsType<BadRequestObjectResult>(data);
        }


        #endregion



        #region Update Existing Freelancer

        [Fact]
        public async Task Update_ValidData_Return_OkResult()
        {
            //Arrange  
            
            var freelancer_valid_id = 2;
            var freelancer_update = new FreelancerDTO();
            freelancer_update.Username = "Shanuka";
            freelancer_update.Mail = "shanukato@gmail.com";
            freelancer_update.PhoneNumber = "+601112262178";
            freelancer_update.Skillsets = "C#, ASP.NET CORE, SQL";
            freelancer_update.Hobby = "Programming";



            //Act  

            _service.Setup(s => s.PutFreelancer(freelancer_valid_id, freelancer_update))
                   .ReturnsAsync(new FreelancerDTO { ID = 2 });

            var updatedData = await _controller.PutFreelancer(freelancer_valid_id, freelancer_update);


            //Assert  
            var ok = Assert.IsType<OkObjectResult>(updatedData);
            Assert.Equal("Updated Successfully", ok.Value);
        }

        [Fact]
        public async Task PutFreelancer_NotFound_Returns_NotFound()
        {
            _service.Setup(s => s.PutFreelancer(999, It.IsAny<FreelancerDTO>()))
                    .ReturnsAsync((FreelancerDTO)null!);

            var result = await _controller.PutFreelancer(999, new FreelancerDTO());

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Update_InvalidData_Return_NotFound()
        {
            //Arrange  
           
            var invalid_id = 29;

            var fl_update = new FreelancerDTO();
            fl_update.Username = "test username more than 50 characters";
            fl_update.Mail = "shanukato@gmail.com";
            fl_update.PhoneNumber = "+601112262178";
            fl_update.Skillsets = "C#, ASP.NET CORE, SQL";
            fl_update.Hobby = "Programming";

            //Act  
            var invalidData = await _controller.PutFreelancer(invalid_id, fl_update);

            //Assert  
            Assert.IsType<NotFoundResult>(invalidData);
            //Assert.IsType<BadRequestObjectResult>(invalidData);
        }

        [Fact]
        public async Task PutFreelancer_InvalidModel_Returns_BadRequest()
        {
            var input = new FreelancerDTO { Username = "" };
            _controller.ModelState.AddModelError("Username", "Required");

            var result = await _controller.PutFreelancer(1, input);

            Assert.IsType<BadRequestObjectResult>(result);
        }


        #endregion

        #region Delete Freelancer

        [Fact]
        public async Task DeleteFreelancer_Success()
        {
            //Arrange  

            var invalid_id = 29;
            var valid_id = 4;




            //Act  
            _service.Setup(s => s.DeleteFreelancer(4)).ReturnsAsync(true);
            _service.Setup(s => s.DeleteFreelancer(29)).ReturnsAsync(false);

            var success_response = await _controller.Delete(valid_id);
            var error_response = await _controller.Delete(invalid_id);

            //Assert  
            var ok = Assert.IsType<OkObjectResult>(success_response);
            Assert.Equal("The Freelancer has been Deleted", ok.Value);

            var nf = Assert.IsType<NotFoundObjectResult>(error_response);
            Assert.Equal("Freelancer not found", nf.Value);
            
        }

        #endregion


    }
}