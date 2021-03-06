﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class User : BaseEntity
    {
        private User()
        {
        }

        [Required]
        [MaxLength(25)]
        public string Name { get; private set; }

        public string Password { get; private set; }

        public byte[] Salt { get; private set; }

        public string Username { get; private set; }

        public string Email { get; private set; }

        public double Trust { get; private set; }

        public int NumberOfVotes { get; private set; }

        public List<Comment> Comments { get; private set; }

        public static User Create(string name, string password, string username, string email, double trust,
            List<Comment> comments)
        {
            var userToCreate = new User
            {
                Name = name,
                Password = password,
                Username = username,
                Email = email,
                Trust = trust,
                Comments = comments
            };

            return userToCreate;
        }

        public void Update(string name, string password, string username, string email, double trust,
            List<Comment> comments)
        {
            Name = name;
            Password = password;
            Username = username;
            Email = email;
            Trust = trust;
            Comments = comments;
        }

        public void SetAuthenticationData(string password, byte[]salt)
        {
            Password = password;
            Salt = salt;
        }

        public void AddCommentToList(Comment com)
        {
            if (Comments == null)
            {
                Comments = new List<Comment>();
            }
            Comments.Add(com);
        }

        public void EditTrust(double newValue)
        {
            NumberOfVotes = NumberOfVotes + 1;
            Trust = (Trust + newValue) / NumberOfVotes;
        }
    }
}