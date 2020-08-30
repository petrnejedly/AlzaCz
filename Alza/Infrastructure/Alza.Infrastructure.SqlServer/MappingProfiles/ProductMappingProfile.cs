using AutoMapper;

namespace Alza.Infrastructure.SqlServer.MappingProfiles
{
    /// <summary>
    /// Product mapping profile (From infrastructure to domain).
    /// </summary>
    /// <seealso cref="AutoMapper.Profile" />
    public class ProductMappingProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProductMappingProfile" /> class.
        /// </summary>
        public ProductMappingProfile()
        {
            this.CreateMap<Entities.Product, Domain.Entities.Product>()
                .MapBaseFields()
                .ForMember(dst => dst.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dst => dst.ImgUri, opt => opt.MapFrom(src => src.ImgUri))
                .ForMember(dst => dst.Price, opt => opt.MapFrom(src => src.Price))
                .ForMember(dst => dst.Description, opt => opt.MapFrom(src => src.Description))
                .ForAllOtherMembers(opt => opt.Ignore());
        }
    }
}