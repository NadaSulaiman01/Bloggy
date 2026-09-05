using AutoMapper;
using Bloggy.Application.Contracts.Blogs.ResponseDtos;
using Bloggy.Domain.BlogAggregate;

namespace Bloggy.Application
{
    public class BlogProfile : Profile
    {
        public BlogProfile()
        {
            CreateMap<Blog, BlogDto>();
        }
    }
}
