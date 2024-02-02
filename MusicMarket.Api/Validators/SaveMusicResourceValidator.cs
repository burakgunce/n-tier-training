using FluentValidation;
using MusicMarket.Api.DTO;

namespace MusicMarket.Api.Validators
{
    public class SaveMusicResourceValidator : AbstractValidator<SaveMusicDTO>
    {
        public SaveMusicResourceValidator()
        {
            RuleFor(m => m.Name).NotEmpty().MaximumLength(50);
        }
    }
}
