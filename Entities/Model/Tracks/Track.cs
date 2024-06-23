using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Model.Tracks
{
    public class Track : BaseModel<Guid>
    {
        public string Title { get; set; }
    }
}
