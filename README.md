# Squares API
## Problem Definition
### The bigger picture
The Squares solution is designed to enable our enterprise consumers to identify squares from coordinates and thus make the world a better place.

A user interface allows a user to import a list of points, add and delete a point to an existing list. On top of that the user interface displays a list of squares that our amazing solution identified.

### The task
The Squares UI team is taking care of the front-end side of things, however they need your help for the backend solution.

Create an API that from a given set of points in a 2D plane - enables the consumer to find out sets of points that make squares and how many squares can be drawn. A point is a pair of integer X and Y coordinates. A square is a set of 4 points that when connected make up, well, a square. 

### Example of a list of points that make up a square:
```[(-1;1), (1;1), (1;-1), (-1;-1)]```

### API request/response contracts
Up to you! Design API contracts how you desire - as long as the UI team can use the API to solve user's problems.

## Requirements
### Functional
* I as a user can import a list of points
* I as a user can add a point to an existing list
* I as a user can delete a point from an existing list
* I as a user can retrieve the squares identified

### Non-fuctional
* Include prerequisites and steps to launch in `README`
* The solution code must be in a `git` repository
* The API should be implemented using .NET Core framework (ideally the newest stable version)
* The API must have some sort of persistance layer
* After sending a request the consumer shouldn't wait more than 5 seconds for a response

### Bonus points stuff!
* RESTful API
* Documentation generated from code (hint - `Swagger`)
* Automated tests
* Containerization/deployment (hint - `Docker`)
* Performance considerations
* Measuring SLI's
* Considerations for scalability of the API
* Comments/thoughts on the decisions you made

### A quick tip:
Don't reinvent the wheel when it comes to identifying squares. There are plenty of existing solutions to the problem online!

## The time for the solution
Take *as long as you need* on the solution but we suggest to limit yourself at 8 hours. Do let us know how much time it took you!

The task is not made to be completed in the period of 8 hours and no one expects you to! However, knowing how much time you spent and seeing the solution you came up with allows for seeing what you prioritize and where you would consider cutting corners on a sharp deadline.


# IMPLEMENTATION
## Prerequisites:
* Docker installed
* Latest .NET 8 SDK

## How to Launch:
1. Clone the repository.
2. In the source directory where docker-compose.yaml files are located, run the command: `docker-compose build`
Followed By: `docker-compose up`
3. Access the application via Postman or open https://localhost:8081/swagger.

## How to Run Tests:
Navigate to the directory `SquaresAPI\SquareAPI.Services.Tests.Unit` and run the command: `dotnet test`.
Alternatively, run tests manually in Visual Studio from the test tab.

## Performance Considerations:
* The primary bottleneck will be the list size of points and square detection efficiency. While the current algorithm is efficient compared to basic implementations, there may be room for improvement with further research.
* Considering parallelism for the inner loop of the detectingSquares method.

## Considerations for Scalability of the API:
* Components are loosely coupled, facilitating the addition of new features or extension of existing ones.
* Optimize the database with indexes.
* Implement monitoring and alerting to scale API resources as needed.
* Use load balancing across multiple API instances.

## Development Process Overview:
With only 8 hours allocated, priorities were set as follows:
1. **Initial Research:** Spent the first half-hour researching algorithms for finding squares.
2. **Decision on SLI Measurement:** Decided against implementing SLI measurement due to time constraints. And due to experience not facing it.
3. **Focus on Non-functional Requirements:**
    * Ensured code was in a git repository.
    * Used .NET Core framework for the API.
    * Included a persistence layer.

### Additional Steps:
Keeping in mind an bonus point for docker, this took time for project setup.

* Set up Docker as a bonus consideration.
* Focused on CRUD operations and the algorithm for detecting squares.
* Aimed to deliver a RESTful API with documentation generated from the code (Swagger).
* Prioritized response times: After sending a request, the consumer should not wait more than 5 seconds for a response.
* Implemented automated tests.

## Reflections on the Project:
* The 1/4 of the time was spent identifying and implementing an efficient algorithm for square detection.
* Chose nSubstitute for mocking in tests due to its ease of use following vulnerabilities found in moq.
* Implemented custom timeout middleware rather than using the built-in .NET 8 feature due to reported issues.
* Selected MSSQL for the database and ensured data persistence using a persistent volume. Due to experience using this technology.
* Another decision was made when user imports the list, that list will be loaded only. It clears the existing points.

Overall, the focus was on delivering a scalable API within the constraints of an 8-hour timeline. The time taken to develop a solution, documenting decisions and processes took about 8,5 hours.
