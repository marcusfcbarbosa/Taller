using FluentValidation;
using FluentValidation.Results;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Taller.Api.Entities;
using Taller.Api.Repositories;
using Taller.Core.Commands;

namespace Taller.Api.Commands
{
    public class AddCarCommand : Command
    {
        public string Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public string Color { get; set; }
        public decimal Price { get; set; }
        public override bool Valid()
        {
            validationResult = new AddCarValidation().Validate(this);
            return validationResult.IsValid;
        }
        public class AddCarValidation : AbstractValidator<AddCarCommand>
        {
            public AddCarValidation()
            {
                RuleFor(c => c.Make)
                    .NotEmpty()
                    .NotNull()
                    .WithMessage("Invalid value Make");
                RuleFor(c => c.Model)
                    .NotEmpty()
                    .NotNull()
                    .WithMessage("Invalid value  Model");

                RuleFor(c => c.Color)
                    .NotEmpty()
                    .NotNull()
                    .WithMessage("Invalid Color value");

                RuleFor(c => c.Year)
                 .GreaterThan(0)
                 .WithMessage("Invalid Year value");

                RuleFor(c => c.Price)
                    .GreaterThan(0)
                    .WithMessage("Invalid price value");
            }
        }
    }


    public class AddCarCommandHandler : CommandHandler,
        IRequestHandler<AddCarCommand, ValidationResult>
    {
        private readonly ICarRepository _carRepository;
        public AddCarCommandHandler(ICarRepository carRepository)
        {
            _carRepository = carRepository;
        }

        public async Task<ValidationResult> Handle(AddCarCommand request, CancellationToken cancellationToken)
        {
            if (!request.Valid()) return request.validationResult;
            _carRepository.Add(MappCar(request));
            return await PersistData(_carRepository.UnitOfWork);
        }

        private Car MappCar(AddCarCommand request)
        {
            return new Car(request.Make, request.Model, request.Year, request.Color, request.Price);
        }
    }
}
