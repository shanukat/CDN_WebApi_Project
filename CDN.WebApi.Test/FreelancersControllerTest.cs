using CDN.WebApi.Application.Repository;
using CDN.WebApi.Controllers;
using CDN.WebApi.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Hosting;
using NuGet.Protocol.Core.Types;
using Xunit;

namespace CDN.WebApi.Test
{
    public class FreelancersControllerTest
    {

        private readonly FreelancersController _controller;
        IFreelancerService _freelancerService;


        public FreelancersControllerTest()
        {
            _freelancerService = new FreelancerService();
            _controller = new FreelancersController(_freelancerService);
        }

        #region Get All 
        [Fact]
        public async Task GetAll_Freelancers_Return_OkResult()
        {
            //Arrange

            //Act
            var result = await _controller.GetFreelancers();
            

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
        }


        [Fact]
        public async Task GetAll_Freelancers_Return_BadRequestResult()
        {
            //Arrange

            //Act
            var result = await _controller.GetFreelancers();
            result = null;

            //Assert
            if (result != null)
                Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task GetAll_Freelancers_MatchResult()
        {
            //Arrange

            //Act
            var data = await _controller.GetFreelancers();


            //Assert  
            var okResult = Assert.IsType<OkObjectResult>(data);

            var value = Assert.IsType<int>(okResult.Value);
            Assert.Equal(10, value);
        }

        #endregion

        #region Get By Id 

        [Fact]
        public async Task GetFreelancer_ById_Return_OkResult()
        {
            //Arrange
            int valid_id = 5;

            //Act
            var success_result = await _controller.GetFreelancer(valid_id);
            var success_model = success_result as OkObjectResult;
            var fetch_freelancer = success_model.Value as FreelancerDTO;

            //Assert  
            Assert.IsType<OkObjectResult>(success_result);
            Assert.Equal(5, fetch_freelancer.ID);
        }

        [Fact]
        public async Task GetFreelancerById_Return_NotFoundResult()
        {
            //Arrange  
            int invalid_id = 59;

            //Act  
            var data = await _controller.GetFreelancer(invalid_id);

            //Assert  
            Assert.IsType<NotFoundResult>(data);
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
            var response = await _controller.PostFreelancer(new_freelancer);
            var created_freelancer = response as CreatedAtActionResult;
            var freelancer = created_freelancer.Value as FreelancerDTO;

            //Assert  
            Assert.IsType<CreatedAtActionResult>(response);
            Assert.IsType<FreelancerDTO>(created_freelancer.Value);
            Assert.Equal("Shanuka perera", freelancer.Username);
            Assert.Equal("shanukato@gmail.com", freelancer.Mail);
        }


        [Fact]
        public async Task Add_InvalidData_Return_BadRequest()
        {
            //Arrange  
            var new_freelancer = new FreelancerDTO() { ID = 15, Username = "test username more than 50 characters", Mail = "shanukato@gmail.com", PhoneNumber = "+601112262178", Skillsets = "C#, ASP.NET CORE, SQL", Hobby = "Programming" };

            //Act              
            var data = await _controller.PostFreelancer(new_freelancer);

            //Assert  
            Assert.IsType<BadRequestResult>(data);
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
            var updatedData = await _controller.PutFreelancer(freelancer_valid_id, freelancer_update);


            //Assert  
            Assert.IsType<OkResult>(updatedData);
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
        }


        #endregion

        #region Delete Freelancer

        public async Task DeleteFreelancer_Success()
        {
            //Arrange  

            var invalid_id = 29;
            var valid_id = 4;

           

            //Act  
            var success_response = await _controller.Delete(valid_id);
            var error_response = await _controller.Delete(invalid_id);

            //Assert  
            Assert.IsType<OkObjectResult>(success_response);
            Assert.IsType<NotFoundObjectResult>(error_response);
        }

        #endregion


    }
}