using Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Repositories
{
    public class StudyRepository : BaseRepository<Study>
    {
        public StudyRepository(MedifyDbContext context) : base(context) { }


    }
}
