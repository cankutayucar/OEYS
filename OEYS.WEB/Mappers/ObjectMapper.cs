using AutoMapper;

namespace OEYS.WEB.Mappers
{
    public static class ObjectMapper
    {
        private static readonly Lazy<IMapper> _lazy = new Lazy<IMapper>(() =>
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<DtoToEntityMapper>();
            });
            return config.CreateMapper();
        });
        public static IMapper Mapper => _lazy.Value;
    }
}
