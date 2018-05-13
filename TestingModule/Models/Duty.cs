using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestingModule.Models
{
    public class Duty
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int RubricId { get; set; }
        public string Description { get; set; }

        public Rubric Rubric { get; set; }
    }
}
