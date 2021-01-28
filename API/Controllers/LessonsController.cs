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
    public class LessonsController : ControllerBase
    {
        private readonly PelajaranContext myContext;


        public LessonsController(PelajaranContext myContext, IConfiguration configuration)
        {
            this.myContext = myContext;
            _configuration = configuration;
        }
        IConfiguration _configuration { get; }

        [HttpPost]
        public async Task<int> Create(LessonVM lessonVm)
        {
            TbTPelajaran pelajaran = new TbTPelajaran();
            pelajaran.MataPelajaranId = lessonVm.MataPelajaranId;
            pelajaran.SiswaId = lessonVm.SiswaId;
            await myContext.TbTPelajarans.AddAsync(pelajaran);
            var create = myContext.SaveChanges();
            return create;
        }
        [HttpPut]
        public async Task<int> Update(LessonVM lessonVm)
        {
            var getId = await myContext.TbTPelajarans.FindAsync(lessonVm.Id);
            getId.MataPelajaranId = lessonVm.MataPelajaranId;
            getId.SiswaId = lessonVm.SiswaId;
            myContext.Entry(getId).State = EntityState.Modified;
            var Update = myContext.SaveChanges();
            return Update;
        }

        [HttpDelete("{id}")]
        public async Task<int> Delete(int id)
        {
            var getId = await myContext.TbTPelajarans.FirstOrDefaultAsync(x => x.Id == id);
            /*         if(getId != null)
                        {
                            myContext.Students.Remove(getId);
                            myContext.SaveChanges();
                            return
                        }*/
            myContext.TbTPelajarans.Remove(getId);
            var delete = myContext.SaveChanges();
            return delete;
        }

        [HttpGet]
        public async Task<List<LessonVM>> GetAll()
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DevConnection")))
            {
                var result = connection.Query<LessonVM>("[dbo].[LoadLesson]").ToList();
                return result;
            }
        }
        [HttpGet("{id}")]
        public async Task<TbTPelajaran> GetById(int id)
        {
            /*using (var connection = new SqlConnection(_configuration.GetConnectionString("DevConnection")))
            {
                var result = connection.Query<Student>($"[dbo].[StudentGetById] @Id={id}").SingleOrDefault();
                return result;
            }*/
            var getId = await myContext.TbTPelajarans.FirstOrDefaultAsync(x => x.Id == id);
            return getId;
        }
    }
}
