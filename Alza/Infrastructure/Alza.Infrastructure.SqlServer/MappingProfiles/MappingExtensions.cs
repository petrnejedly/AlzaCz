using AutoMapper;

namespace Alza.Infrastructure.SqlServer.MappingProfiles
{
    /// <summary>
    /// Mapping extensions.
    /// </summary>
    public static class MappingExtensions
    {

        /// <summary>
        /// Maps the base fields.
        /// </summary>
        /// <typeparam name="TSource">The type of the source.</typeparam>
        /// <typeparam name="TDestination">The type of the destination.</typeparam>
        /// <param name="mappingExpression">The mapping expression.</param>
        /// <returns>The expression.</returns>
        public static IMappingExpression<TSource, TDestination> MapBaseFields<TSource, TDestination>(this IMappingExpression<TSource, TDestination> mappingExpression)
            where TSource : Entities.Base
            where TDestination : Domain.Entities.Base
        {
            return mappingExpression
                .ForMember(dst => dst.Id, opt => opt.MapFrom(src => src.Id));
        }
    }
}