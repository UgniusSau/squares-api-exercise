using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using NSubstitute;
using Xunit;
using Repository.Repositories.Coordinates;
using Data.DTOs;
using Data.DB.CoordinatesDB;
using PointsServiceNamespace = Services.PointsService;

namespace Services.Tests
{
    public class PointsServiceTests
    {
        private readonly PointsServiceNamespace.PointsService _pointsService;
        private readonly ICoordinatesRepository _mockCoordinatesRepository;

        public PointsServiceTests()
        {
            _mockCoordinatesRepository = Substitute.For<ICoordinatesRepository>();
            _pointsService = new PointsServiceNamespace.PointsService(_mockCoordinatesRepository);
        }

        [Fact]
        public async Task GetPoints_ShouldReturnPoints()
        {
            // Arrange
            var points = new List<Point> { new Point { X = 1, Y = 2 } };
            _mockCoordinatesRepository.GetPoints(Arg.Any<CancellationToken>()).Returns(points);

            // Act
            var result = await _pointsService.GetPoints(CancellationToken.None);

            // Assert
            Assert.Equal(points, result);
        }

        [Fact]
        public async Task AddPoint_ShouldAddPoint_WhenPointDoesNotExist()
        {
            // Arrange
            var pointDto = new PointDTO { X = 1, Y = 2 };
            _mockCoordinatesRepository.GetPoints(Arg.Any<CancellationToken>()).Returns(new List<Point>());
            _mockCoordinatesRepository.AddPoint(Arg.Any<Point>(), Arg.Any<CancellationToken>()).Returns(Task.CompletedTask);

            // Act
            await _pointsService.AddPoint(pointDto, CancellationToken.None);

            // Assert
            await _mockCoordinatesRepository.Received(1).AddPoint(Arg.Is<Point>(p => p.X == pointDto.X && p.Y == pointDto.Y), Arg.Any<CancellationToken>());
        }

        [Fact]
        public async Task AddPoint_ShouldThrowException_WhenPointExists()
        {
            // Arrange
            var pointDto = new PointDTO { X = 1, Y = 2 };
            var existingPoints = new List<Point> { new Point { X = 1, Y = 2 } };
            _mockCoordinatesRepository.GetPoints(Arg.Any<CancellationToken>()).Returns(existingPoints);

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => _pointsService.AddPoint(pointDto, CancellationToken.None));
        }

        [Fact]
        public async Task DeletePoint_ShouldReturnTrue_WhenPointIsDeleted()
        {
            // Arrange
            var pointDto = new PointDTO { X = 1, Y = 2 };
            _mockCoordinatesRepository.DeletePoint(pointDto.X, pointDto.Y, Arg.Any<CancellationToken>()).Returns(Task.FromResult(true));

            // Act
            var result = await _pointsService.DeletePoint(pointDto, CancellationToken.None);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task ImportPoints_ShouldThrowException_WhenPointsDtoIsNullOrEmpty()
        {
            // Act and Assert
            await Assert.ThrowsAsync<Exception>(() => _pointsService.ImportPoints(new List<PointDTO>(), CancellationToken.None));
        }

        [Fact]
        public async Task ImportPoints_ShouldThrowException_WhenDuplicatePointsExist()
        {
            // Arrange
            var pointsDto = new List<PointDTO>
            {
                new PointDTO { X = 1, Y = 2 },
                new PointDTO { X = 1, Y = 2 }
            };

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => _pointsService.ImportPoints(pointsDto, CancellationToken.None));
        }

        [Fact]
        public async Task ImportPoints_ShouldImportPoints_WhenNoDuplicatesExist()
        {
            // Arrange
            var pointsDto = new List<PointDTO>
            {
                new PointDTO { X = 1, Y = 2 },
                new PointDTO { X = 3, Y = 4 }
            };
            _mockCoordinatesRepository.ImportPoints(Arg.Any<IEnumerable<Point>>(), Arg.Any<CancellationToken>()).Returns(Task.CompletedTask);

            // Act
            await _pointsService.ImportPoints(pointsDto, CancellationToken.None);

            // Assert
            await _mockCoordinatesRepository.Received(1).ImportPoints(Arg.Is<IEnumerable<Point>>(points => points.Count() == pointsDto.Count), Arg.Any<CancellationToken>());
        }
    }
}
