using API.Models;
using API.ViewModel;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectsController : ControllerBase
    {
        private readonly PelajaranContext myContext;


        public SubjectsController(PelajaranContext myContext, IConfiguration configuration)
        {
            this.myContext = myContext;
            _configuration = configuration;
        }
        IConfiguration _configuration { get; }

        [HttpPost]
        [Route("Create")]
        public async Task<int> Create(MapelVM mapelVM)
        {
            TbMMataPelajaran mapel = new TbMMataPelajaran();
            mapel.Name = mapelVM.Name;
            await myContext.TbMMataPelajarans.AddAsync(mapel);
            var create = myContext.SaveChanges();
            return create;
        }
        [HttpPut]
        public async Task<int> Update(MapelVM mapelVM)
        {
            var getId = await myContext.TbMMataPelajarans.FindAsync(mapelVM.Id);
            getId.Name = mapelVM.Name;
            var Update = myContext.SaveChanges();
            return Update;
        }
        [HttpDelete("{id}")]
        public async Task<int> Delete(int id)
        {
            var getId = await myContext.TbMMataPelajarans.FirstOrDefaultAsync(x => x.Id == id);
            /*         if(getId != null)
                        {
                            myContext.Students.Remove(getId);
                            myContext.SaveChanges();
                            return
                        }*/
            myContext.TbMMataPelajarans.Remove(getId);
            var delete = myContext.SaveChanges();
            return delete;
        }

        [HttpGet]
        public async Task<List<TbMMataPelajaran>> GetAll()
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DevConnection")))
            {
                var result = connection.Query<TbMMataPelajaran>("[dbo].[LoadSubject]").ToList();
                return result;
            }
        }
        [HttpGet("{id}")]
        public async Task<TbMMataPelajaran> GetById(int id)
        {
            /*using (var connection = new SqlConnection(_configuration.GetConnectionString("DevConnection")))
            {
                var result = connection.Query<Student>($"[dbo].[StudentGetById] @Id={id}").SingleOrDefault();
                return result;
            }*/
            var getId = await myContext.TbMMataPelajarans.FirstOrDefaultAsync(x => x.Id == id);
            return getId;
        }
    }
}
