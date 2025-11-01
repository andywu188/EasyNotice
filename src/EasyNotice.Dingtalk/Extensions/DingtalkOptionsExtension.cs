using Microsoft.Extensions.DependencyInjection;
using System;

namespace EasyNotice.Dingtalk
{
    public class DingtalkOptionsExtension : IEasyNoticeOptionsExtension
    {
        private readonly Action<DingtalkOptions, IServiceProvider> configure;

        public DingtalkOptionsExtension(Action<DingtalkOptions, IServiceProvider> configure)
        {
            this.configure = configure;
        }

        public void AddServices(IServiceCollection services)
        {
            services.AddOptions();
            services.AddOptions<DingtalkOptions>().Configure(configure);
            services.AddTransient<IDingtalkProvider, DingtalkProvider>();
        }
    }
}
