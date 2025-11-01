using Microsoft.Extensions.DependencyInjection;
using System;

namespace EasyNotice.Weixin
{
    public class WeixinOptionsExtension : IEasyNoticeOptionsExtension
    {
        private readonly Action<WeixinOptions, IServiceProvider> configure;

        public WeixinOptionsExtension(Action<WeixinOptions, IServiceProvider> configure)
        {
            this.configure = configure;
        }

        public void AddServices(IServiceCollection services)
        {
            services.AddOptions();
            services.AddOptions<WeixinOptions>().Configure(configure);
            services.AddTransient<IWeixinProvider, WeixinProvider>();
        }
    }
}
