using System.Threading.Tasks;
using DRIMA.Models.TokenAuth;
using DRIMA.Web.Controllers;
using Shouldly;
using Xunit;

namespace DRIMA.Web.Tests.Controllers
{
    public class HomeController_Tests: DRIMAWebTestBase
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