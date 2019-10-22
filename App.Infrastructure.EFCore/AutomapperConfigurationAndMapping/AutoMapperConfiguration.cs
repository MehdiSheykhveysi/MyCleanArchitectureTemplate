using App.ApplicationService.Shaared.Attributes;
using App.Domain.Contracts;
using App.Infrastructure.Utilities;
using AutoMapper;

namespace App.Infrastructure.AutomapperConfigurationAndMapping
{
    [ServiceMark]
    public class AutoMapperConfiguration : IMapperFacade
    {
        private readonly MapperConfiguration _mapperConfiguration;

        public AutoMapperConfiguration()
        {
            _mapperConfiguration = new MapperConfiguration(c =>
            {
                c.ApplyConfiguration(this.GetType().Assembly);
            });
            _mapperConfiguration.CompileMappings();
        }
        public TOutPut Map<TOutPut, TInput>(TInput input)
        {
            return _mapperConfiguration.CreateMapper().Map<TOutPut>(input);
        }
    }
}
