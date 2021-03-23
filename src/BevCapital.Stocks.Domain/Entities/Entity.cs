﻿using FluentValidation;
using FluentValidation.Results;

namespace BevCapital.Stocks.Domain.Entities
{
    public abstract class Entity
    {
        public bool Valid { get; private set; }
        public bool Invalid => !Valid;
        public ValidationResult ValidationResult { get; private set; }

        [System.ComponentModel.DataAnnotations.Timestamp]
        public byte[] RowVersion { get; private set; }

        public bool Validate<TModel>(TModel model, AbstractValidator<TModel> validator)
        {
            ValidationResult = validator.Validate(model);
            return Valid = ValidationResult.IsValid;
        }
    }
}
