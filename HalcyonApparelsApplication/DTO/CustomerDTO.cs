using HalcyonApparelsDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HalcyonApparelsApplication.DTO
{
    public class CustomerDTO
    {

        public string ContactId { get; set; } = null!;


        public string Fname { get; set; } = null!;


        public string Lname { get; set; } = null!;


        public string? Email { get; set; } = null!;

        public List<OrderDetails>? orderList { get; set; }

    }

}
