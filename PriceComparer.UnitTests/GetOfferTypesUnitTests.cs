using FastEndpoints;
using PriceComparer.Application;
using PriceComparer.Application.DTOs.OfferType;
using PriceComparer.Domain;
using PriceComparer.Endpoints;

namespace PriceComparer.UnitTests
{
    public  class GetOfferTypesUnitTests
    {
        public GetOfferTypesUnitTests()
        {

        }

        [Fact]
        public async Task ShouldReturnOfferTypes()
        {
            var fakeService = A.Fake<IOfferTypesService>();
            var req = new GetOfferTypesList
            {
                Page = 0,
                PageSize = 2,
                Search = ""
            };

            var response = new List<OfferType>
            {
                new OfferType
                {
                    Id = 1,
                    Name = "Offer 1",
                    UserId = "user-0"
                },
                new OfferType
                {
                    Id = 2,
                    Name="Offer 2",
                    UserId = "user-1"
                }
            };
            A.CallTo(() => fakeService.GetList(req, default)).Returns(response);
            var ep = Factory.Create<GetOfferTypesEndpoint>(fakeService);

            // Act
            await ep.HandleAsync(req, default);
            var result = ep.Response;

            // Assert
            Assert.False(ep.ValidationFailed);
            Assert.NotNull(result);
        }
    }
}
