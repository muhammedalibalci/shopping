using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Dto
{
    public class UserDto
    {
        public UserDto(int Id,string FirstName,string LastName, string Address, string AccessToken,string Role)
        {
            this.Id = Id;
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.Address = Address;
            this.AccessToken = AccessToken;
            this.Role = Role;
        }
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Role { get; set; }
        public string AccessToken { get; set; }

      
    }
}
