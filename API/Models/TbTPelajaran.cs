using System;
using System.Collections.Generic;

#nullable disable

namespace API.Models
{
    public partial class TbTPelajaran
    {
        public int Id { get; set; }
        public int SiswaId { get; set; }
        public int MataPelajaranId { get; set; }

        public virtual TbMMataPelajaran MataPelajaran { get; set; }
        public virtual TbMSiswa Siswa { get; set; }
    }
}
