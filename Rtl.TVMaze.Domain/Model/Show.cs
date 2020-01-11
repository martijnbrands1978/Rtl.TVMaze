using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Rtl.TVMaze.Domain.Model
{
    public class Show
    {
        public string id { get; set; }

        public string Name { get; set; }

        public IEnumerable<CastMember> Cast { get; set; }
    }
}
