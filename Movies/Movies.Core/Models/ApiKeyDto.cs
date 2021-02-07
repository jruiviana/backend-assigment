using System;

namespace Movies.Core.Models
{
    public class ApiKeyDto
    {
        public string Id { get; set; }
        public string Owner { get; set; }
        public string Key { get; set; }
        public DateTime Created { get; set; }
    }
}
