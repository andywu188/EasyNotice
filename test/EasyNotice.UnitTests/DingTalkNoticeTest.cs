using EasyNotice.Core;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using Xunit;

namespace EasyNotice.UnitTests
{
    /// <summary>
    /// 配置钉钉群机器人官方文档
    /// https://developers.dingtalk.com/document/app/custom-robot-access
    /// </summary>
    public class DingTalkNoticeTest
    {
        private readonly IDingtalkProvider _dingtalkProvider;

        public DingTalkNoticeTest()
        {
            IServiceCollection services = new ServiceCollection();
            services.AddEasyNotice(config =>
            {
                config.IntervalSeconds = 10;//同一标题的消息，10秒内只能发一条，避免短时间内大量发送重复消息
                config.UseDingTalk((option, serviceProvider) =>
                {
                    option.WebHook = "https://oapi.dingtalk.com/robot/send?access_token=xxx";//通知地址
                    option.Secret = "secret";//签名校验
                });
            });
            IServiceProvider serviceProvider = services.BuildServiceProvider();

            _dingtalkProvider = serviceProvider.GetService<IDingtalkProvider>();
        }

        [Fact]
        public async Task DingTalk_Send_Should_Be_Succeed()
        {
            var response = await _dingtalkProvider.SendAsync("通知标题", new Exception("custom exception"));
            Assert.True(response.IsSuccess);
        }

        [Fact]
        public async Task DingTalk_Send_AtUser_Should_Be_Succeed()
        {
            var response = await _dingtalkProvider.SendAsync("通知标题", new Exception("custom exception"), new EasyNoticeAtUser() { IsAtAll = false, Mobile = new[] { "138xxxxxxxx" } });
            Assert.True(response.IsSuccess);
        }

        [Fact]
        public async Task DingTalk_Send_Markdown_Should_Be_Succeed()
        {
            var response = await _dingtalkProvider.SendMarkdownAsync("通知标题", "#### <font color=#008800>订单审核通过</font> 信息如下：\r\n订单号：11111111111\r\n商品名：手机");
            Assert.True(response.IsSuccess);
        }

    }
}
