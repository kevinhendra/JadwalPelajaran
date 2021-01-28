using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.ViewModel
{
    public class LessonVM
    {
        public int Id { get; set; }
        //Mata pelajaran
        public int MataPelajaranId { get; set; }
        public string MataPelajaranName { get; set; }

        //Siswa
        public int SiswaId { get; set; }
        public string SiswaName { get; set; }
    }
}
