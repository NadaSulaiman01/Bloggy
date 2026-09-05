using Bloggy.Domain.Shared;
using FluentValidation;

namespace Bloggy.Application.Contracts.Blogs.RequestDtos
{
    public class CreateUpdateBlogRequestDto
    {
        public string Title { get; set; } = null!;
        public string Content { get; set; } = null!;
    }
    public class CreateUpdateBlogRequestDtoValidator : AbstractValidator<CreateUpdateBlogRequestDto>
    {
        public CreateUpdateBlogRequestDtoValidator()
        {
            RuleFor(x => x.Title)
                .NotNull()
                .NotEmpty()
                .MaximumLength(StringLengths.ShortTitleLength);

            RuleFor(x => x.Content)
                .NotNull()
                .NotEmpty()
                .MaximumLength(StringLengths.LongContentLength);
        }
    }
}
