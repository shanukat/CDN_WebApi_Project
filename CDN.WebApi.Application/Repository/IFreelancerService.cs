using CDN.WebApi.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDN.WebApi.Application.Repository
{
    public interface IFreelancerService
    {
        Task<IEnumerable<FreelancerDTO>> GetFreelancers();

        Task<FreelancerDTO> GetFreelancerById(int id);

        Task<FreelancerDTO> PostFreelancer(FreelancerDTO freelancer);

        Task<FreelancerDTO> PutFreelancer(int id, FreelancerDTO freelancer);

        Task<bool> DeleteFreelancer(int id);

        Task<bool> ExistFreelancer(string username);

    }
}
