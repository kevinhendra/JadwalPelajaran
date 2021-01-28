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
    public class StudentsController : ControllerBase
    {
        private readonly PelajaranContext myContext;
        

        public StudentsController(PelajaranContext myContext, IConfiguration configuration)
        {
            this.myContext = myContext;
            _configuration = configuration;
        }
        IConfiguration _configuration { get; }

        [HttpPost]
        public async Task<int> Create(StudentVM studentVM)
        {
            TbMSiswa siswa = new TbMSiswa();
            siswa.Name = studentVM.Name;
            await myContext.TbMSiswas.AddAsync(siswa);
            var create = myContext.SaveChanges();
            return create;
        }
        [HttpPut("{id}")]
        public async Task<int> Update(TbMSiswa student)
        {
            var getId = await myContext.TbMSiswas.FirstOrDefaultAsync(x => x.Id == student.Id);
            getId.Name = student.Name;
            var Update = myContext.SaveChanges();
            return Update;  
        }
        [HttpDelete("{id}")]
        public async Task<int> Delete (int id)
        {
           var getId = await myContext.TbMSiswas.FirstOrDefaultAsync(x=> x.Id == id);
            /*         if(getId != null)
                        {
                            myContext.Students.Remove(getId);
                            myContext.SaveChanges();
                            return
                        }*/
            myContext.TbMSiswas.Remove(getId);
           var delete = myContext.SaveChanges();
            return delete;
        }

        [HttpGet]
        public async Task<List<TbMSiswa>> GetAll()
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DevConnection")))
            {
                var result = connection.Query<TbMSiswa>("[dbo].[LoadStudent]").ToList();
                return result;
            }
        }
        [HttpGet("{id}")]
        public async Task<TbMSiswa> GetById(int id)
        {
            /*using (var connection = new SqlConnection(_configuration.GetConnectionString("DevConnection")))
            {
                var result = connection.Query<Student>($"[dbo].[StudentGetById] @Id={id}").SingleOrDefault();
                return result;
            }*/
            var getId = await myContext.TbMSiswas.FirstOrDefaultAsync(x => x.Id == id);
            return getId;
        }
    }
}
