using FluentValidation;
using System;
using System.Collections.Generic;

namespace BevCapital.Stocks.Domain.Entities
{
    public class AppUser : Entity
    {
        public Guid Id { get; }
        public string Name { get; }
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
        public AppUser(Guid id, string name, string email)
        {
            Id = id;
            Name = name;
            Email = email;

            Validate(this, new AppUserValidator());
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
