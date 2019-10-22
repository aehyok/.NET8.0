using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace aehyok.Core.AutoMapper
{
    public class AutoMapping: Profile
    {
        public AutoMapping()
        {
            CreateMap<User, UserDTO>(); 
        }
    }

    /// <summary>
    /// 测试用例
    /// </summary>
    public class User
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        // Constructor to initialize User
        public User()
        {
            Name = "Nicky";
            Email = "myemail@gmail.com";
            Phone = "+81234588";
        }
    }

    public class UserDTO
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }
}
