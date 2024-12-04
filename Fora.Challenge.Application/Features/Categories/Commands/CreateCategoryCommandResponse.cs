using Fora.Challenge.Application.Features.Responses;

namespace Fora.Challenge.Application.Features.Categories.Commands
{
    public class CreateCategoryCommandResponse : BaseResponse
    {
        public CreateCategoryCommandResponse() : base()
        { }

        public CreateCategoryDto Category { get; set; } = default!;
    }
}
