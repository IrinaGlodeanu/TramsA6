using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class User
    {
        public User()
        {
        }

        public Guid Id { get; private set; }

        [Required, MaxLength(20)]
        public string Name { get; private set; }


        public static User Create(string name)
        {
            var userToCreate = new User
            {
                Id = Guid.NewGuid(),
                Name = name,
            };

            return userToCreate;
        }

        public void Update(string name)
        {
            Name = name;
        }
    }
}