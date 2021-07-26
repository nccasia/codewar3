using System.Threading.Tasks;
using AutoGenerateTestcase.Models.TokenAuth;
using AutoGenerateTestcase.Web.Controllers;
using Shouldly;
using Xunit;

namespace AutoGenerateTestcase.Web.Tests.Controllers
{
    public class HomeController_Tests: AutoGenerateTestcaseWebTestBase
    {
        [Fact]
        public async Task Index_Test()
        {
            await AuthenticateAsync(null, new AuthenticateModel
            {
                UserNameOrEmailAddress = "admin",
                Password = "123qwe"
            });

            //Act
            var response = await GetResponseAsStringAsync(
                GetUrl<HomeController>(nameof(HomeController.Index))
            );

            //Assert
            response.ShouldNotBeNullOrEmpty();
        }
    }
}