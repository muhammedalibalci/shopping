using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Models
{
    [Table("Order")]
    public class Order
    {
        [Key]
        public int Id { get; set; }
        public string ShipName { get; set; }
        public string ShipAddress { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int Timestamp { get; set; }
        public int Amount { get; set; }
        public int? UserId { get; set; }
        public User User { get; set; }
        public List<Product> Products { get; set; }
    }
}
