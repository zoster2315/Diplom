using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestingModule.Models
{
    public class UnitTest
    {
        public int ID { get; set; }
        public string Arguments { get; set; }
        public string Value { get; set; }
        public int DutyId { get; set; }

        public Duty Duty { get; set; }
    }
}
