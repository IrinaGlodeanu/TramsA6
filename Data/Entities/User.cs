using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class User
    {
        private User()
        {
            // EF Core
        }

        public Guid Id { get; set; }

        [Required, MaxLength(20)]
        public string Name { get; private set; }

        public static User Create(string name)
        {
            var userToCreate = new User
            {
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