using FastEndpoints;
using Microsoft.Extensions.Configuration;
using PriceComparer.Application;
using PriceComparer.Application.DTOs.Offer;
using PriceComparer.Endpoints;

namespace PriceComparer.UnitTests
{
    public class CreateOfferUnitTests
    {
        public CreateOfferUnitTests()
        {

        }

        [Fact]
        public async Task ShouldCreateOffer()
        {
            // Arrange
            var fakeConfig = A.Fake<IConfiguration>();
            var fakeService = A.Fake<IOffersService>();

            var createOffer = new CreateOffer
            {
                UserId = "test-user-001",
                OfferTypesIds = new List<int> { },
                Name = "test",
                Address = "str. Studentilor 9/11, Chisinau"
            };
            A.CallTo(() => fakeService.Create(createOffer, default)).Returns(Task.CompletedTask);
            var ep = Factory.Create<CreateOfferEndpoint>(fakeService);

            // Act
            await ep.HandleAsync(createOffer, default);
            var response = ep.Response;

            // Assert
            Assert.False(ep.ValidationFailed);
        }


        // TODO: fix this
        //[Fact]
        //public async Task ShouldFailValidation()
        //{
        //    // Arrange
        //    var fakeConfig = A.Fake<IConfiguration>();
        //    var fakeService = A.Fake<IOffersService>();

        //    var createOffer = new CreateOffer
        //    {
        //        UserId = "test-user-001",
        //        OfferTypesIds = new List<int> { },
        //        Address = "str. Studentilor 9/11, Chisinau"
        //    };
        //    A.CallTo(() => fakeService.Create(createOffer, default)).Returns(Task.CompletedTask);
        //    var ep = Factory.Create<CreateOfferEndpoint>(fakeService);

        //    // Act
        //    await ep.HandleAsync(createOffer, default);
        //    var response = ep.Response;

        //    // Assert
        //    Assert.True(ep.ValidationFailed);
        //}
    }
}