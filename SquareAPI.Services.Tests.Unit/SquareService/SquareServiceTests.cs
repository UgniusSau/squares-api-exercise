using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using NSubstitute;
using Xunit;
using Data.DB.CoordinatesDB;
using Data.Models.Shape.Square;
using Repository.Repositories.Coordinates;

// Ensure you are using the correct namespace for SquareService
using SquareServiceNamespace = Services.SquareService;

namespace Services.Tests
{
    public class SquareServiceTests
    {
        private readonly SquareServiceNamespace.SquareService _squareService;
        private readonly ICoordinatesRepository _mockCoordinatesRepository;

        public SquareServiceTests()
        {
            _mockCoordinatesRepository = Substitute.For<ICoordinatesRepository>();
            _squareService = new SquareServiceNamespace.SquareService(_mockCoordinatesRepository);
        }

        [Fact]
        public async Task DetectSquares_ShouldDetectSimpleSquare()
        {
            // Arrange
            var points = new List<Point>
            {
                new Point { X = 5, Y = 5 },
                new Point { X = 5, Y = 10 },
                new Point { X = 10, Y = 10 },
                new Point { X = 10, Y = 5 }
            };
            _mockCoordinatesRepository.GetPoints(Arg.Any<CancellationToken>()).Returns(points);

            // Act
            var result = await _squareService.DetectSquares(CancellationToken.None);

            // Assert
            Assert.Equal(1, result);
        }

        [Fact]
        public async Task DetectSquares_ShouldDetect45DegreeRotatedSquare()
        {
            // Arrange
            var points = new List<Point>
            {
                new Point { X = 5, Y = 0 },
                new Point { X = 10, Y = 5 },
                new Point { X = 10, Y = -5 },
                new Point { X = 15, Y = 0 }
            };
            _mockCoordinatesRepository.GetPoints(Arg.Any<CancellationToken>()).Returns(points);

            // Act
            var result = await _squareService.DetectSquares(CancellationToken.None);

            // Assert
            Assert.Equal(1, result);
        }

        [Fact]
        public async Task DetectSquares_ShouldDetectTwoSquaresWithSameEdge()
        {
            // Arrange
            var points = new List<Point>
            {
                new Point { X = 5, Y = 5 },
                new Point { X = 5, Y = 10 },
                new Point { X = 10, Y = 10 },
                new Point { X = 10, Y = 5 },
                new Point { X = 15, Y = 5 },
                new Point { X = 15, Y = 10 }
            };
            _mockCoordinatesRepository.GetPoints(Arg.Any<CancellationToken>()).Returns(points);

            // Act
            var result = await _squareService.DetectSquares(CancellationToken.None);

            // Assert
            Assert.Equal(2, result);
        }

        [Fact]
        public async Task DetectSquares_ShouldDetectTwoOverlappingSquares()
        {
            // Arrange
            var points = new List<Point>
            {
                new Point { X = 5, Y = 5 },
                new Point { X = 5, Y = 10 },
                new Point { X = 10, Y = 10 },
                new Point { X = 10, Y = 5 },
                new Point { X = 8, Y = 5 },
                new Point { X = 8, Y = 10 },
                new Point { X = 15, Y = 5 },
                new Point { X = 15, Y = 10 }
            };
            _mockCoordinatesRepository.GetPoints(Arg.Any<CancellationToken>()).Returns(points);

            // Act
            var result = await _squareService.DetectSquares(CancellationToken.None);

            // Assert
            Assert.Equal(2, result);
        }

        [Fact]
        public async Task DetectSquares_ShouldThrowException_WhenNoPoints()
        {
            // Arrange
            var points = new List<Point>();
            _mockCoordinatesRepository.GetPoints(Arg.Any<CancellationToken>()).Returns(points);

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => _squareService.DetectSquares(CancellationToken.None));
        }

        [Fact]
        public async Task DetectSquares_ShouldThrowException_WhenLessThanFourPoints()
        {
            // Arrange
            var points = new List<Point>
            {
                new Point { X = 5, Y = 5 },
                new Point { X = 5, Y = 10 },
                new Point { X = 10, Y = 10 }
            };
            _mockCoordinatesRepository.GetPoints(Arg.Any<CancellationToken>()).Returns(points);

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => _squareService.DetectSquares(CancellationToken.None));
        }

        [Fact]
        public async Task DetectSquares_ShouldReturnZero_WhenNoSquaresFormed()
        {
            // Arrange
            var points = new List<Point>
            {
                new Point { X = 5, Y = 5 },
                new Point { X = 5, Y = 10 },
                new Point { X = 7, Y = 7 },
                new Point { X = 10, Y = 10 }
            };
            _mockCoordinatesRepository.GetPoints(Arg.Any<CancellationToken>()).Returns(points);

            // Act
            var result = await _squareService.DetectSquares(CancellationToken.None);

            // Assert
            Assert.Equal(0, result);
        }

        [Fact]
        public async Task DetectSquares_ShouldHandleDuplicatePoints()
        {
            // Arrange
            var points = new List<Point>
            {
                new Point { X = 5, Y = 5 },
                new Point { X = 5, Y = 10 },
                new Point { X = 10, Y = 10 },
                new Point { X = 10, Y = 5 },
                new Point { X = 5, Y = 5 },
                new Point { X = 10, Y = 10 }
            };
            _mockCoordinatesRepository.GetPoints(Arg.Any<CancellationToken>()).Returns(points);

            // Act
            var result = await _squareService.DetectSquares(CancellationToken.None);

            // Assert
            Assert.Equal(1, result);
        }
    }
}