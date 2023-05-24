using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson;
using System.Web;
using MongoDB.Bson.Serialization.Attributes;


namespace DemoProject.Models
{
    public class EmployeeDetails
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public String Id { get; set; }
        public string Name { get; set; }
        public string Department { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
    }
}