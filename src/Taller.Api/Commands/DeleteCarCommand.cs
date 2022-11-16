using FluentValidation;
using FluentValidation.Results;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Taller.Api.Repositories;
using Taller.Core.Commands;

namespace Taller.Api.Commands
{
    public class DeleteCarCommand : Command
    {
        public Guid Id { get; set; }
        public override bool Valid()
        {
            validationResult = new DeleteCarValidation().Validate(this);
            return validationResult.IsValid;
        }
    }

    public class DeleteCarValidation : AbstractValidator<DeleteCarCommand>
    {
        public DeleteCarValidation()
        {
            RuleFor(c => c.Id)
                  .NotEqual(Guid.Empty)
                  .WithMessage("Invalid id");
        }
    }
    public class DeleteCarCommandHandler : CommandHandler,
         IRequestHandler<DeleteCarCommand, ValidationResult>
    {
        private readonly ICarRepository _carRepository;

        public DeleteCarCommandHandler(ICarRepository carRepository)
        {
            _carRepository = carRepository;
        }

        public async Task<ValidationResult> Handle(DeleteCarCommand request, CancellationToken cancellationToken)
        {
            if (!request.Valid()) return request.validationResult;
           await _carRepository.DeleteById(request.Id);
            return await PersistData(_carRepository.UnitOfWork);
        }
    }
}
