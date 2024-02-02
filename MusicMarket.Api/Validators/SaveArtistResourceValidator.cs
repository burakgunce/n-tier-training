using FluentValidation;
using MusicMarket.Api.DTO;

namespace MusicMarket.Api.Validators
{
    public class SaveArtistResourceValidator : AbstractValidator<SaveArtistDTO>
    {
        public SaveArtistResourceValidator()
        {
            RuleFor(a => a.Name).NotEmpty().MaximumLength(50);
            
        }
    }
}
