using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLiteSample.Models
{
    public class Grade
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int StudentId { get; set; }
        public string Class { get; set; }
        public string Result { get; set; }
    }
}
