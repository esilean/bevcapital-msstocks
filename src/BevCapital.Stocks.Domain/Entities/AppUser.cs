using FluentValidation;
using System;
using System.Collections.Generic;

namespace BevCapital.Stocks.Domain.Entities
{
    public class AppUser : Entity
    {
        public Guid Id { get; }
        public string Name { get; private set; }
        public string Email { get; }
        public ICollection<AppUserStock> AppUserStocks { get; private set; }

        /// <summary>
        /// Ef Core
        /// </summary>
        protected AppUser() { }

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="email"></param>
        private AppUser(Guid id, string name, string email)
        {
            Id = id;
            Name = name;
            Email = email;

            Validate(this, new AppUserValidator());
        }

        public static AppUser Create(Guid id, string name, string email)
        {
            return new AppUser(id, name, email);
        }

        public void Update(string name)
        {
            Name = name;
        }

        internal class AppUserValidator : AbstractValidator<AppUser>
        {
            public AppUserValidator()
            {
                RuleFor(a => a.Id)
                    .NotEmpty()
                    .WithMessage("Invalid id");

                RuleFor(a => a.Name)
                    .NotEmpty()
                    .WithMessage("Invalid name");

                RuleFor(a => a.Email)
                    .NotEmpty()
                    .WithMessage("Invalid email");
            }
        }
    }
}
