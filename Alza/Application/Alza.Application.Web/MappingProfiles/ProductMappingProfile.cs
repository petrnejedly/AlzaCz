using Alza.Application.Web.Helpers;
using AutoMapper;

namespace Alza.Application.Web.MappingProfiles
{
    /// <summary>
    /// Product mapping profile (From domain to application).
    /// </summary>
    /// <seealso cref="AutoMapper.Profile" />
    public class ProductMappingProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProductMappingProfile" /> class.
        /// </summary>
        public ProductMappingProfile()
        {
            this.CreateMap<Domain.Entities.Product, Models.ProductViewModel>()
                .ForMember(dst => dst.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dst => dst.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dst => dst.ImgUri, opt => opt.MapFrom((src, dst, mem, ctx) =>
                {
                    string imageUrlPrefix = (string)ctx.Items["ImageUrlPrefix"];
                    string imageNull = (string)ctx.Items["ImageNull"];

                    return (!string.IsNullOrEmpty(src.ImgUri) ? StringHelper.CombineUrl(imageUrlPrefix, src.ImgUri) : imageNull);
                }))
                .ForMember(dst => dst.Price, opt => opt.MapFrom(src => src.Price))
                .ForMember(dst => dst.Description, opt => opt.MapFrom(src => src.Description))
                .ForAllOtherMembers(opt => opt.Ignore());
        }
    }
}