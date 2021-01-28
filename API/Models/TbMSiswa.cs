using System;
using System.Collections.Generic;

#nullable disable

namespace API.Models
{
    public partial class TbMSiswa
    {
        public TbMSiswa()
        {
            TbTPelajarans = new HashSet<TbTPelajaran>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<TbTPelajaran> TbTPelajarans { get; set; }
    }
}
