using Autofac;
using Scouts.Core.Interface;
using Microsoft.Extensions.Configuration;
namespace Scouts.Data
{
    public class RegistrationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(c => new ScoutContext(c.Resolve<IConfiguration>())).As<ScoutContext>();
            builder.Register(c => new ScoutRepository(c.Resolve<ScoutContext>())).As<IScoutData>();

            builder.Register(c => new AccountContext(c.Resolve<IConfiguration>())).As<AccountContext>();
            builder.Register(c => new AccountRepository(c.Resolve<AccountContext>())).As<IFeeData>();
            builder.Register(c => new AccountRepository(c.Resolve<AccountContext>())).As<ITransactionData>();
        }
    }
}
