using CDN.WebApi.Domain;
using CDN.WebApi.Infra.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDN.WebApi.Application.Repository
{
    //Author: Shanuka Perera~~~
    public class FreelancerService : IFreelancerService
    {
        private readonly freelancersContext _dbContext;

       
        public FreelancerService()
        {
            _dbContext = new freelancersContext();
        }

        /// <summary>
        /// This Method is used to Get All Freelancers
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<FreelancerDTO>> GetFreelancers()
        {
            return await _dbContext.TblFreelancers.Select(x => new FreelancerDTO { ID = x.Id, Username = x.Username, Mail = x.Mail, PhoneNumber = x.PhoneNumber, Skillsets = x.Skillsets, Hobby=x.Hobby}).ToListAsync();
        }



        /// <summary>
        /// To Get Specific freelancer by id...
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<FreelancerDTO> GetFreelancerById(int id)
        {
            var freelancer = new FreelancerDTO();
            var qry = await _dbContext.TblFreelancers.Where(x => x.Id == id).FirstOrDefaultAsync();
            if(qry != null)
            {

                freelancer.ID = qry.Id;
                freelancer.Username = qry.Username;
                freelancer.Mail = qry.Mail;
                freelancer.PhoneNumber = qry.PhoneNumber;
                freelancer.Skillsets = qry.Skillsets;
                freelancer.Hobby = qry.Hobby;
            }
            else
            {
                freelancer = null;
            }


            return freelancer;
        }

        /// <summary>
        /// To Create New Freelancer
        /// </summary>
        /// <param name="freelancer"></param>
        /// <returns></returns>
        public async Task<FreelancerDTO> PostFreelancer(FreelancerDTO freelancer)
        {
            var newfreelancer = new TblFreelancer()
            {
                Id = freelancer.ID,
                Username = freelancer.Username,
                Mail = freelancer.Mail,
                PhoneNumber = freelancer.PhoneNumber,
                Skillsets = freelancer.Skillsets,
                Hobby = freelancer.Hobby
            };
            await _dbContext.TblFreelancers.AddAsync(newfreelancer);
            await _dbContext.SaveChangesAsync();
            return freelancer;
        }


        /// <summary>
        /// To Update existing freelancer details....
        /// </summary>
        /// <param name="id"></param>
        /// <param name="freelancer"></param>
        /// <returns></returns>
        public async Task<FreelancerDTO> PutFreelancer(int id, FreelancerDTO freelancer)
        {
            var query = await _dbContext.TblFreelancers.FindAsync(id);
            if (query!=null)
            {
                query.Username = freelancer.Username;
                query.Mail = freelancer.Mail;
                query.PhoneNumber = freelancer.PhoneNumber;
                query.Skillsets = freelancer.Skillsets;
                query.Hobby = freelancer.Hobby;

                await _dbContext.SaveChangesAsync();
                
            }
            else
            {
                freelancer = null;
            }
            return freelancer;

        }
        /// <summary>
        /// To delete the existing Freelancer....
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> DeleteFreelancer(int id)
        {
            var query = await _dbContext.TblFreelancers.FindAsync(id);
            if (query != null)
            {
                 _dbContext.TblFreelancers.Remove(query);
                 await _dbContext.SaveChangesAsync();
                return true;

            }
            else
            {
                return false;
            }
           
        }

        /// <summary>
        /// Check the Existing username or Mail
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> ExistFreelancer(string username)
        {
            var query = await _dbContext.TblFreelancers.Where(a => a.Username == username).FirstOrDefaultAsync();
            if (query == null)
            {
               
                return false; //not exist

            }
            else
            {
                return true;
            }

        }



    }
}
