using System.Collections.Generic;
using Autofac;
using AutoMapper;
using Microsoft.Extensions.Configuration;


namespace Scout.Auth.StartupLogic
{
    public class RegistrationModule : Module
    {
        private IConfiguration _configuration;
        public RegistrationModule(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        protected override void Load(ContainerBuilder builder)
        {
            //builder.Register(c => new Maps.ScoutDtoMap()).As<Profile>();

            builder.Register(ctx => new MapperConfiguration(cfg =>
            {
                foreach (var profile in ctx.Resolve<IEnumerable<Profile>>())
                {
                    cfg.AddProfile(profile);
                }
            }));

            builder.Register(ctx => ctx.Resolve<MapperConfiguration>()
            .CreateMapper())
            .As<IMapper>()
            .SingleInstance();

            builder.Register(c=>_configuration).As<IConfiguration>();
        }
    }
}
