//
// namespace Domain.Tests
// {
//     public class DomainTests
//     {
//         //** SPRINT **
//         
//         [Fact]
//         public void CreateSprint_GivenTitleStartDateEndDateScrumMaster_WhenNoPreConditions_ThenInstantiateSprint()
//         {
//             // Arrange
//             ISprintFactory<SprintReview> sprintReview = new SprintReviewFactory();
//
//             // Act
//             string title = "Initial Sprint";
//             DateTime startTime = DateTime.Now;
//             DateTime endTime = DateTime.Now.AddDays(14);
//             User scrumMaster = new Developer("name", "email", "password");
//
//             SprintReview sprint = sprintReview.CreateSprint(title, startTime, endTime, scrumMaster);
//
//             // Assert
//             Assert.Equal(sprint.Title, title);
//             Assert.Equal(sprint.StartDate, startTime);
//             Assert.Equal(sprint.EndDate, endTime);
//             Assert.Equal(sprint.ScrumMaster, scrumMaster);
//         }
//         
//         //TODO: Test for failed creating object
//         
//         [Fact]
//         public void UpdateSprint_GivenTitleStartDateEndDate_WhenSprintIsInInitialState_ThenChangeValues()
//         {
//             // Arrange
//             ISprintFactory<SprintReview> sprintReview = new SprintReviewFactory();
//
//             string initialTitle = "Initial Sprint";
//             DateTime initialStartTime = DateTime.Now;
//             DateTime initialEndTime = DateTime.Now.AddDays(14);
//             User scrumMaster = new Developer("name", "email", "password");
//
//             SprintReview sprint = sprintReview.CreateSprint(initialTitle, initialStartTime, initialEndTime, scrumMaster);
//
//             // Act
//             string newTitle = "New Title";
//             DateTime newStartTime = DateTime.Now.AddDays(1);
//             DateTime newEndTime = DateTime.Now.AddDays(15);
//             
//             sprint.Title = newTitle;
//             sprint.StartDate = newStartTime;
//             sprint.EndDate = newEndTime;
//
//             // Assert
//             Assert.Equal(sprint.Title, newTitle);
//             Assert.NotEqual(sprint.Title, initialTitle);
//             
//             Assert.Equal(sprint.StartDate, newStartTime);
//             Assert.NotEqual(sprint.StartDate, initialStartTime);
//             
//             Assert.Equal(sprint.EndDate, newEndTime);
//             Assert.NotEqual(sprint.EndDate, initialEndTime);
//         }
//         
//         [Fact]
//         public void UpdateSprint_GivenTitleStartDateEndDate_WhenSprintIsInExecutedState_ThenDontChangeValues()
//         {
//             // Arrange
//             ISprintFactory<SprintReview> sprintReview = new SprintReviewFactory();
//
//             string initialTitle = "Initial Sprint";
//             DateTime initialStartTime = DateTime.Now;
//             DateTime initialEndTime = DateTime.Now.AddDays(14);
//             User scrumMaster = new Developer("name", "email", "password");
//             
//             SprintReview sprint = sprintReview.CreateSprint(initialTitle, initialStartTime, initialEndTime, scrumMaster);
//
//             // Act
//             sprint.ExecuteSprint();
//             
//             string newTitle = "New Title";
//             DateTime newStartTime = DateTime.Now.AddDays(1);
//             DateTime newEndTime = DateTime.Now.AddDays(15);
//             
//             sprint.Title = newTitle;
//             sprint.StartDate = newStartTime;
//             sprint.EndDate = newEndTime;
//
//             // Assert
//             Assert.Equal(sprint.Title, initialTitle);
//             Assert.Equal(sprint.StartDate, initialStartTime);
//             Assert.Equal(sprint.EndDate, initialEndTime);
//         }
//         
//         //** END SPRINT **
//         
//         //** NOTIFICATION **
//         
//         //** END NOTIFICATION **
//     }
// }