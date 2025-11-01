using EasyNotice;
using EasyNotice.Core;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class NoticeExtensions
    {
        public static IServiceCollection AddNotice(this IServiceCollection services, IConfiguration configuration, Action<NoticeOptions>? configure = null, string? sectionName = null)
        {
            sectionName ??= NoticeOptions.SectionName;

            var baseConfiguration = configuration.GetSection(sectionName);
            var noticeOptions = baseConfiguration.Get<NoticeOptions>();
            configure?.Invoke(noticeOptions);

            services.AddEasyNotice(config =>
            {
                //同一消息发送间隔 默认10秒
                config.IntervalSeconds = noticeOptions.IntervalSeconds;
                var mailOptions = baseConfiguration.GetSection(EmailOptions.SectionName).Get<EmailOptions>();
                if (mailOptions != null)
                {
                    config.UseEmail((option, serviceProvider) =>
                    {
                        option.Password = mailOptions.Password;
                        option.Host = mailOptions.Host;
                        option.FromAddress = mailOptions.FromAddress;
                        option.FromName = mailOptions.FromName;
                        option.Port = mailOptions.Port;
                        option.ToAddress = mailOptions.ToAddress;
                    });
                }

                var dingtalkOptions = baseConfiguration.GetSection(DingtalkOptions.SectionName).Get<DingtalkOptions>();
                if (dingtalkOptions != null)
                {
                    config.UseDingTalk((x, serviceProvider) =>
                    {
                        x.Secret = dingtalkOptions.Secret;
                        x.WebHook = dingtalkOptions.WebHook;
                    });
                }

                var feishuOptions = baseConfiguration.GetSection(FeishuOptions.SectionName).Get<DingtalkOptions>();
                if (feishuOptions != null)
                {
                    config.UseFeishu((option, serviceProvider) =>
                    {
                        option.Secret = feishuOptions.Secret;
                        option.WebHook = feishuOptions.WebHook;
                    });
                }

                var weixinOptions = baseConfiguration.GetSection(WeixinOptions.SectionName).Get<WeixinOptions>();
                if (weixinOptions != null)
                {
                    config.UseWeixin((option, serviceProvider) =>
                    {
                        option.WebHook = weixinOptions.WebHook;
                    });
                }
            });

            return services;
        }
    }
}
