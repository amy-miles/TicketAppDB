
using Microsoft.AspNetCore.Mvc;
using Moq;
using TicketApp.Controllers;
using TicketApp.Models;
using TicketApp.Repository;

namespace TicketAppTest
{
    public class UnitTest1
    {
        [Fact]
        public void Index_ReturnsViewResult_WithListOfTickets()
        {
            // Arrange
            var mockRepository = new Mock<ITicketRepository>();
            var tickets = new List<Ticket>
            {
                new Ticket { TicketID = 1, StatusID = "todo" },
                new Ticket { TicketID = 2, StatusID = "inprogress" },
               
            };
            mockRepository.Setup(repo => repo.GetAllTickets()).Returns(tickets);

            var controller = new HomeController(mockRepository.Object);

            // Act
            var result = controller.Index(null) as ViewResult;
            var model = result?.Model as List<Ticket>;

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(model);
            Assert.Equal(tickets.Count, model.Count);
        }
    }
}