using EasyNotice;
using EasyNotice.Email;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class EmailExtension
    {
        public static EasyNoticeOptions UseEmail(
            this EasyNoticeOptions options,
            Action<EmailOptions, IServiceProvider> configure)
        {
            if (configure == null)
            {
                throw new ArgumentNullException(nameof(configure));
            }

            options.RegisterExtension(new EmailOptionsExtension(configure));
            return options;
        }
    }
}
