using Entities.Enum.Artists;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Model.Artists
{
    public class Artist : BaseModel<Guid>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public ArtistType Type { get; set; }
    }
}
