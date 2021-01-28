using System;
using System.Collections.Generic;

#nullable disable

namespace API.Models
{
    public partial class TbMMataPelajaran
    {
        public TbMMataPelajaran()
        {
            TbTPelajarans = new HashSet<TbTPelajaran>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<TbTPelajaran> TbTPelajarans { get; set; }
    }
}
