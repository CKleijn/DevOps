using Domain.States.Pipeline;
using Domain.States.Sprint;
using CancelledState = Domain.States.Sprint.CancelledState;
using ClosedState = Domain.States.BacklogItem.ClosedState;
using FinishedState = Domain.States.Sprint.FinishedState;

namespace Domain.Tests
{
    [Collection("SequentialTest")]
    public class SprintTests
    {
        [Fact]
        public void CreateSprint_GivenTitleStartDateEndDateScrumMasterProject_WhenNoPreConditions_ThenCreateSprint()
        {
            // Arrange
            var mockFactory = new Mock<ISprintFactory<SprintReview>>();
            var mockDeveloper = new Mock<Developer>(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<List<NotificationProvider>>());
            var mockVersionControl = new Mock<IVersionControlStrategy>();
            var mockProductOwner = new Mock<ProductOwner>(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<List<NotificationProvider>>());
            var mockProject = new Mock<Project>(It.IsAny<string>(), It.IsAny<string>(), mockProductOwner.Object, mockVersionControl.Object);

            string title = "Initial Sprint";
            DateTime startTime = DateTime.Now;
            DateTime endTime = DateTime.Now.AddDays(14);

            mockFactory.Setup(f => f.CreateSprint(It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<Developer>(), It.IsAny<Project>())).Returns(new SprintReview(title, startTime, endTime, mockDeveloper.Object, mockProject.Object));

            // Act
            SprintReview sprint = mockFactory.Object.CreateSprint(title, startTime, endTime, mockDeveloper.Object, mockProject.Object);

            // Assert
            Assert.NotNull(sprint);
            Assert.Equal(title, sprint.Title);
            Assert.Equal(startTime, sprint.StartDate);
            Assert.Equal(endTime, sprint.EndDate);
            Assert.Equal(mockDeveloper.Object, sprint.ScrumMaster);
            Assert.Equal(mockProject.Object, sprint.Project);
        }
        
        [Fact]
        public void UpdateSprint_GivenTitleStartDateEndDateScrumMasterProjectReviewDeveloperTesterReport_WhenSprintIsInInitialState_ThenUpdateSprint()
        {
            // Arrange
            var mockFactory = new Mock<ISprintFactory<SprintReview>>();
            var mockDeveloper = new Mock<Developer>(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<List<NotificationProvider>>());
            var mockVersionControl = new Mock<IVersionControlStrategy>();
            var mockProductOwner = new Mock<ProductOwner>(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<List<NotificationProvider>>());
            var mockProject = new Mock<Project>(It.IsAny<string>(), It.IsAny<string>(), mockProductOwner.Object, mockVersionControl.Object);

            string initialTitle = "Initial Sprint";
            DateTime initialStartTime = DateTime.Now;
            DateTime initialEndTime = DateTime.Now.AddDays(14);

            string newTitle = "New Title";
            DateTime newStartTime = DateTime.Now.AddDays(1);
            DateTime newEndTime = DateTime.Now.AddDays(15);

            mockFactory.Setup(f => f.CreateSprint(It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<Developer>(), It.IsAny<Project>())).Returns(new SprintReview(initialTitle, initialStartTime, initialEndTime, mockDeveloper.Object, mockProject.Object));

            var mockPipeline = new Mock<TestPipeline>("Test Pipeline", mockFactory.Object.CreateSprint(initialTitle, initialStartTime, initialEndTime, mockDeveloper.Object, mockProject.Object));
            var mockReport = new Mock<Report>(It.IsAny<string>(), mockFactory.Object.CreateSprint(initialTitle, initialStartTime, initialEndTime, mockDeveloper.Object, mockProject.Object), It.IsAny<ReportExtension>());

            // Act
            var sprint = mockFactory.Object.CreateSprint(initialTitle, initialStartTime, initialEndTime, mockDeveloper.Object, mockProject.Object);
            sprint.Pipeline = mockPipeline.Object;

            sprint.Title = newTitle;
            sprint.StartDate = newStartTime;
            sprint.EndDate = newEndTime;
            sprint.ScrumMaster = mockDeveloper.Object;

            sprint.AddDeveloper(mockDeveloper.Object);
            sprint.RemoveDeveloper(mockDeveloper.Object);
            sprint.AddDeveloper(mockDeveloper.Object);
            sprint.AddTester(mockDeveloper.Object);
            sprint.RemoveTester(mockDeveloper.Object);
            sprint.AddTester(mockDeveloper.Object);
            sprint.AddReport(mockReport.Object);
            sprint.RemoveReport(mockReport.Object);
            sprint.AddReport(mockReport.Object);

            // Assert
            Assert.NotNull(sprint);
            Assert.Equal(1, sprint.Developers.Count);
            Assert.Equal(1, sprint.Testers.Count);
            Assert.Equal(1, sprint.Reports.Count);
            Assert.Equal(newTitle, sprint.Title);
            Assert.NotEqual(initialTitle, sprint.Title);
            Assert.Equal(newStartTime, sprint.StartDate);
            Assert.NotEqual(initialStartTime, sprint.StartDate);
            Assert.Equal(newEndTime, sprint.EndDate);
            Assert.NotEqual(initialEndTime, sprint.EndDate);
        }
        
        [Fact]
        public void UpdateSprint_GivenTitleStartDateEndDateScrumMasterProjectReviewDeveloperTesterReport_WhenSprintIsInExecutedState_ThenDontUpdateSprint()
        {
            // Arrange
            var mockFactory = new Mock<ISprintFactory<SprintReview>>();
            var mockDeveloper = new Mock<Developer>(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<List<NotificationProvider>>());
            var mockVersionControl = new Mock<IVersionControlStrategy>();
            var mockProductOwner = new Mock<ProductOwner>(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<List<NotificationProvider>>());
            var mockProject = new Mock<Project>(It.IsAny<string>(), It.IsAny<string>(), mockProductOwner.Object, mockVersionControl.Object);

            string initialTitle = "Initial Sprint";
            DateTime initialStartTime = DateTime.Now;
            DateTime initialEndTime = DateTime.Now.AddDays(14);

            string newTitle = "New Title";
            DateTime newStartTime = DateTime.Now.AddDays(1);
            DateTime newEndTime = DateTime.Now.AddDays(15);

            mockFactory.Setup(f => f.CreateSprint(It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<Developer>(), It.IsAny<Project>())).Returns(new SprintReview(initialTitle, initialStartTime, initialEndTime, mockDeveloper.Object, mockProject.Object));

            var mockPipeline = new Mock<TestPipeline>("Test Pipeline", mockFactory.Object.CreateSprint(initialTitle, initialStartTime, initialEndTime, mockDeveloper.Object, mockProject.Object));
            var mockReport = new Mock<Report>(It.IsAny<string>(), mockFactory.Object.CreateSprint(initialTitle, initialStartTime, initialEndTime, mockDeveloper.Object, mockProject.Object), It.IsAny<ReportExtension>());

            // Act
            var sprint = mockFactory.Object.CreateSprint(initialTitle, initialStartTime, initialEndTime, mockDeveloper.Object, mockProject.Object);
            sprint.Pipeline = mockPipeline.Object;
            sprint.ExecuteSprint();

            sprint.Title = newTitle;
            sprint.StartDate = newStartTime;
            sprint.EndDate = newEndTime;
            sprint.ScrumMaster = mockDeveloper.Object;

            sprint.AddDeveloper(mockDeveloper.Object);
            sprint.RemoveDeveloper(mockDeveloper.Object);
            sprint.AddDeveloper(mockDeveloper.Object);
            sprint.AddTester(mockDeveloper.Object);
            sprint.RemoveTester(mockDeveloper.Object);
            sprint.AddTester(mockDeveloper.Object);
            sprint.AddReport(mockReport.Object);
            sprint.RemoveReport(mockReport.Object);
            sprint.AddReport(mockReport.Object);

            // Assert
            Assert.NotNull(sprint);
            Assert.Equal(0, sprint.Developers.Count);
            Assert.Equal(0, sprint.Testers.Count);
            Assert.Equal(0, sprint.Reports.Count);
            Assert.Equal(initialTitle, sprint.Title);
            Assert.NotEqual(newTitle, sprint.Title);
            Assert.Equal(initialStartTime, sprint.StartDate);
            Assert.NotEqual(newStartTime, sprint.StartDate);
            Assert.Equal(initialEndTime, sprint.EndDate);
            Assert.NotEqual(newEndTime, sprint.EndDate);
        }

        [Fact]
        public void UpdateSprint_GivenTitleStartDateEndDateScrumMasterProjectReviewDeveloperTesterReport_WhenPipelineInExecutingState_ThenDontUpdateSprint()
        {
            // Arrange
            var mockFactory = new Mock<ISprintFactory<SprintReview>>();
            var mockDeveloper = new Mock<Developer>(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<List<NotificationProvider>>());
            var mockVersionControl = new Mock<IVersionControlStrategy>();
            var mockProductOwner = new Mock<ProductOwner>(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<List<NotificationProvider>>());
            var mockProject = new Mock<Project>(It.IsAny<string>(), It.IsAny<string>(), mockProductOwner.Object, mockVersionControl.Object);

            string initialTitle = "Initial Sprint";
            DateTime initialStartTime = DateTime.Now;
            DateTime initialEndTime = DateTime.Now.AddDays(14);

            string newTitle = "New Title";
            DateTime newStartTime = DateTime.Now.AddDays(1);
            DateTime newEndTime = DateTime.Now.AddDays(15);

            mockFactory.Setup(f => f.CreateSprint(It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<Developer>(), It.IsAny<Project>())).Returns(new SprintReview(initialTitle, initialStartTime, initialEndTime, mockDeveloper.Object, mockProject.Object));

            var mockPipeline = new Mock<TestPipeline>("Test Pipeline", mockFactory.Object.CreateSprint(initialTitle, initialStartTime, initialEndTime, mockDeveloper.Object, mockProject.Object));
            var mockReport = new Mock<Report>(It.IsAny<string>(), mockFactory.Object.CreateSprint(initialTitle, initialStartTime, initialEndTime, mockDeveloper.Object, mockProject.Object), It.IsAny<ReportExtension>());

            // Act
            var sprint = mockFactory.Object.CreateSprint(initialTitle, initialStartTime, initialEndTime, mockDeveloper.Object, mockProject.Object);
            sprint.Pipeline = mockPipeline.Object;
            sprint.Pipeline.ExecutePipeline();

            sprint.Title = newTitle;
            sprint.StartDate = newStartTime;
            sprint.EndDate = newEndTime;
            sprint.ScrumMaster = mockDeveloper.Object;

            sprint.AddDeveloper(mockDeveloper.Object);
            sprint.RemoveDeveloper(mockDeveloper.Object);
            sprint.AddDeveloper(mockDeveloper.Object);
            sprint.AddTester(mockDeveloper.Object);
            sprint.RemoveTester(mockDeveloper.Object);
            sprint.AddTester(mockDeveloper.Object);
            sprint.AddReport(mockReport.Object);
            sprint.RemoveReport(mockReport.Object);
            sprint.AddReport(mockReport.Object);

            // Assert
            Assert.NotNull(sprint);
            Assert.Equal(0, sprint.Developers.Count);
            Assert.Equal(0, sprint.Testers.Count);
            Assert.Equal(0, sprint.Reports.Count);
            Assert.Equal(initialTitle, sprint.Title);
            Assert.NotEqual(newTitle, sprint.Title);
            Assert.Equal(initialStartTime, sprint.StartDate);
            Assert.NotEqual(newStartTime, sprint.StartDate);
            Assert.Equal(initialEndTime, sprint.EndDate);
            Assert.NotEqual(newEndTime, sprint.EndDate);
            Assert.Equal(typeof(FailedState), sprint.Pipeline.CurrentStatus.GetType());
        }
        
        [Fact]
        public void ReviewSprintAutomatically_GivenTitleStartDateEndDateScrumMasterProject_WhenSprintGoesThroughProperPhasesAfterSprintEnded_ThenReviewSprint()
        {
            // Arrange
            var mockFactory = new Mock<ISprintFactory<SprintReview>>();
            var mockDeveloper = new Mock<Developer>(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<List<NotificationProvider>>());
            var mockVersionControl = new Mock<IVersionControlStrategy>();
            var mockProductOwner = new Mock<ProductOwner>(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<List<NotificationProvider>>());
            var mockProject = new Mock<Project>(It.IsAny<string>(), It.IsAny<string>(), mockProductOwner.Object, mockVersionControl.Object);

            string initialTitle = "Initial Sprint";
            DateTime initialStartTime = DateTime.Now;
            DateTime initialEndTime = DateTime.Now.AddDays(-1);

            mockFactory.Setup(f => f.CreateSprint(It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<Developer>(), It.IsAny<Project>())).Returns(new SprintReview(initialTitle, initialStartTime, initialEndTime, mockDeveloper.Object, mockProject.Object));

            var mockItem = new Mock<Item>(It.IsAny<string>(), It.IsAny<string>(), mockDeveloper.Object, 1, mockFactory.Object.CreateSprint(initialTitle, initialStartTime, initialEndTime, mockDeveloper.Object, mockProject.Object).SprintBacklog);
            var mockActivity = new Mock<Activity>(It.IsAny<string>(), mockItem.Object, mockDeveloper.Object);
            mockItem.Object.AddActivityToItem(mockActivity.Object);
            mockActivity.Object.IsFinished = true;

            mockItem.Object.DevelopBacklogItem();
            mockItem.Object.FinalizeDevelopmentBacklogItem();
            mockItem.Object.TestingBacklogItem();
            mockItem.Object.FinalizeTestingBacklogItem();
            mockItem.Object.FinalizeBacklogItem();
            mockItem.Object.CloseBacklogItem();

            var mockPipeline = new Mock<TestPipeline>("Test Pipeline", mockFactory.Object.CreateSprint(initialTitle, initialStartTime, initialEndTime, mockDeveloper.Object, mockProject.Object));

            var sprint = mockFactory.Object.CreateSprint(initialTitle, initialStartTime, initialEndTime, mockDeveloper.Object, mockProject.Object);
            sprint.Pipeline = mockPipeline.Object;
            sprint.SprintBacklog.AddItemToBacklog(mockItem.Object);

            // Act
            sprint.Title = "New Title";

            // Assert
            Assert.NotNull(sprint);
            Assert.Equal(typeof(ReviewState), sprint.CurrentStatus.GetType());
        }

        [Fact]
        public void ReleaseSprintAutomatically_GivenTitleStartDateEndDateScrumMasterProject_WhenSprintGoesThroughProperPhasesAfterSprintEnded_ThenReleaseSprint()
        {
            // Arrange
            var mockFactory = new Mock<ISprintFactory<SprintRelease>>();
            var mockDeveloper = new Mock<Developer>(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<List<NotificationProvider>>());
            var mockVersionControl = new Mock<IVersionControlStrategy>();
            var mockProductOwner = new Mock<ProductOwner>(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<List<NotificationProvider>>());
            var mockProject = new Mock<Project>(It.IsAny<string>(), It.IsAny<string>(), mockProductOwner.Object, mockVersionControl.Object);

            string initialTitle = "Initial Sprint";
            DateTime initialStartTime = DateTime.Now;
            DateTime initialEndTime = DateTime.Now.AddDays(-1);

            mockFactory.Setup(f => f.CreateSprint(It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<Developer>(), It.IsAny<Project>())).Returns(new SprintRelease(initialTitle, initialStartTime, initialEndTime, mockDeveloper.Object, mockProject.Object));

            var mockItem = new Mock<Item>(It.IsAny<string>(), It.IsAny<string>(), mockDeveloper.Object, 1, mockFactory.Object.CreateSprint(initialTitle, initialStartTime, initialEndTime, mockDeveloper.Object, mockProject.Object).SprintBacklog);
            var mockActivity = new Mock<Activity>(It.IsAny<string>(), mockItem.Object, mockDeveloper.Object);
            mockItem.Object.AddActivityToItem(mockActivity.Object);
            mockActivity.Object.IsFinished = true;

            mockItem.Object.DevelopBacklogItem();
            mockItem.Object.FinalizeDevelopmentBacklogItem();
            mockItem.Object.TestingBacklogItem();
            mockItem.Object.FinalizeTestingBacklogItem();
            mockItem.Object.FinalizeBacklogItem();
            mockItem.Object.CloseBacklogItem();

            var mockPipeline = new Mock<TestPipeline>("Test Pipeline", mockFactory.Object.CreateSprint(initialTitle, initialStartTime, initialEndTime, mockDeveloper.Object, mockProject.Object));

            var sprint = mockFactory.Object.CreateSprint(initialTitle, initialStartTime, initialEndTime, mockDeveloper.Object, mockProject.Object);
            sprint.Pipeline = mockPipeline.Object;
            sprint.SprintBacklog.AddItemToBacklog(mockItem.Object);

            // Act
            sprint.Title = "New Title";

            // Assert
            Assert.NotNull(sprint);
            Assert.Equal(typeof(ReleaseState), sprint.CurrentStatus.GetType());
        }
        
         [Fact]
        public void ExecuteSprint_GivenTitleStartDateEndDateScrumMasterProject_WhenSprintGoesThroughProperPhases_ThenExecuteSprint()
        {
            // Arrange
            var mockFactory = new Mock<ISprintFactory<SprintRelease>>();
            var mockDeveloper = new Mock<Developer>(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<List<NotificationProvider>>());
            var mockVersionControl = new Mock<IVersionControlStrategy>();
            var mockProductOwner = new Mock<ProductOwner>(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<List<NotificationProvider>>());
            var mockProject = new Mock<Project>(It.IsAny<string>(), It.IsAny<string>(), mockProductOwner.Object, mockVersionControl.Object);

            mockFactory.Setup(f => f.CreateSprint(It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<Developer>(), It.IsAny<Project>())).Returns(new SprintRelease("Sprint", DateTime.Now, DateTime.Now.AddDays(5), mockDeveloper.Object, mockProject.Object));

            var mockItem = new Mock<Item>(It.IsAny<string>(), It.IsAny<string>(), mockDeveloper.Object, 1, mockFactory.Object.CreateSprint("Sprint", DateTime.Now, DateTime.Now.AddDays(5), mockDeveloper.Object, mockProject.Object).SprintBacklog);
            var mockActivity = new Mock<Activity>(It.IsAny<string>(), mockItem.Object, mockDeveloper.Object);
            mockItem.Object.AddActivityToItem(mockActivity.Object);
            mockActivity.Object.IsFinished = true;

            mockItem.Object.DevelopBacklogItem();
            mockItem.Object.FinalizeDevelopmentBacklogItem();
            mockItem.Object.TestingBacklogItem();
            mockItem.Object.FinalizeTestingBacklogItem();
            mockItem.Object.FinalizeBacklogItem();
            mockItem.Object.CloseBacklogItem();

            var sprint = mockFactory.Object.CreateSprint("Sprint", DateTime.Now, DateTime.Now.AddDays(5), mockDeveloper.Object, mockProject.Object);
            sprint.SprintBacklog.AddItemToBacklog(mockItem.Object);

            // Act
            sprint.ExecuteSprint();

            // Assert
            Assert.NotNull(sprint);
            Assert.NotNull(sprint.SprintBacklog);
            Assert.Equal(typeof(ClosedState), sprint.SprintBacklog.Items[0]!.CurrentStatus.GetType());
            Assert.Equal(typeof(ExecutedState), sprint.CurrentStatus.GetType());
        }

        [Fact]
        public void FinishSprint_GivenTitleStartDateEndDateScrumMasterProject_WhenSprintGoesThroughProperPhases_ThenFinishSprint()
        {
            // Arrange
            var mockFactory = new Mock<ISprintFactory<SprintRelease>>();
            var mockDeveloper = new Mock<Developer>(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<List<NotificationProvider>>());
            var mockVersionControl = new Mock<IVersionControlStrategy>();
            var mockProductOwner = new Mock<ProductOwner>(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<List<NotificationProvider>>());
            var mockProject = new Mock<Project>(It.IsAny<string>(), It.IsAny<string>(), mockProductOwner.Object, mockVersionControl.Object);

            mockFactory.Setup(f => f.CreateSprint(It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<Developer>(), It.IsAny<Project>())).Returns(new SprintRelease("Sprint", DateTime.Now, DateTime.Now.AddDays(5), mockDeveloper.Object, mockProject.Object));

            var mockItem = new Mock<Item>(It.IsAny<string>(), It.IsAny<string>(), mockDeveloper.Object, 1, mockFactory.Object.CreateSprint("Sprint", DateTime.Now, DateTime.Now.AddDays(5), mockDeveloper.Object, mockProject.Object).SprintBacklog);
            var mockActivity = new Mock<Activity>(It.IsAny<string>(), mockItem.Object, mockDeveloper.Object);
            mockItem.Object.AddActivityToItem(mockActivity.Object);
            mockActivity.Object.IsFinished = true;

            mockItem.Object.DevelopBacklogItem();
            mockItem.Object.FinalizeDevelopmentBacklogItem();
            mockItem.Object.TestingBacklogItem();
            mockItem.Object.FinalizeTestingBacklogItem();
            mockItem.Object.FinalizeBacklogItem();
            mockItem.Object.CloseBacklogItem();

            var mockPipeline = new Mock<TestPipeline>("Test Pipeline", mockFactory.Object.CreateSprint("Sprint", DateTime.Now, DateTime.Now.AddDays(5), mockDeveloper.Object, mockProject.Object));

            var sprint = mockFactory.Object.CreateSprint("Sprint", DateTime.Now, DateTime.Now.AddDays(5), mockDeveloper.Object, mockProject.Object);
            sprint.Pipeline = mockPipeline.Object;
            sprint.SprintBacklog.AddItemToBacklog(mockItem.Object);

            // Act
            sprint.ExecuteSprint();
            sprint.FinishSprint();

            // Assert
            Assert.NotNull(sprint);
            Assert.NotNull(sprint.SprintBacklog);
            Assert.Equal(typeof(ClosedState), sprint.SprintBacklog.Items[0]!.CurrentStatus.GetType());
            Assert.Equal(typeof(FinishedState), sprint.CurrentStatus.GetType());
        }
        
        [Fact]
        public void CancelSprint_GivenTitleStartDateEndDateScrumMasterProject_WhenSprintGoesThroughProperPhases_ThenCancelSprint()
        {
            // Arrange
            var mockFactory = new Mock<ISprintFactory<SprintRelease>>();
            var mockDeveloper = new Mock<Developer>(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<List<NotificationProvider>>());
            var mockVersionControl = new Mock<IVersionControlStrategy>();
            var mockProductOwner = new Mock<ProductOwner>(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<List<NotificationProvider>>());
            var mockProject = new Mock<Project>(It.IsAny<string>(), It.IsAny<string>(), mockProductOwner.Object, mockVersionControl.Object);

            mockFactory.Setup(f => f.CreateSprint(It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<Developer>(), It.IsAny<Project>())).Returns(new SprintRelease("Sprint", DateTime.Now, DateTime.Now.AddDays(5), mockDeveloper.Object, mockProject.Object));

            var mockItem = new Mock<Item>(It.IsAny<string>(), It.IsAny<string>(), mockDeveloper.Object, 1, mockFactory.Object.CreateSprint("Sprint", DateTime.Now, DateTime.Now.AddDays(5), mockDeveloper.Object, mockProject.Object).SprintBacklog);
            var mockActivity = new Mock<Activity>(It.IsAny<string>(), mockItem.Object, mockDeveloper.Object);
            mockItem.Object.AddActivityToItem(mockActivity.Object);
            mockActivity.Object.IsFinished = true;

            mockItem.Object.DevelopBacklogItem();
            mockItem.Object.FinalizeDevelopmentBacklogItem();
            mockItem.Object.TestingBacklogItem();
            mockItem.Object.FinalizeTestingBacklogItem();
            mockItem.Object.FinalizeBacklogItem();
            mockItem.Object.CloseBacklogItem();

            var mockPipeline = new Mock<TestPipeline>("Test Pipeline", mockFactory.Object.CreateSprint("Sprint", DateTime.Now, DateTime.Now.AddDays(5), mockDeveloper.Object, mockProject.Object));

            var sprint = mockFactory.Object.CreateSprint("Sprint", DateTime.Now, DateTime.Now.AddDays(5), mockDeveloper.Object, mockProject.Object);
            sprint.Pipeline = mockPipeline.Object;
            sprint.SprintBacklog.AddItemToBacklog(mockItem.Object);

            // Act
            sprint.ExecuteSprint();
            sprint.FinishSprint();
            sprint.CancelSprint();

            // Assert
            Assert.NotNull(sprint);
            Assert.NotNull(sprint.SprintBacklog);
            Assert.Equal(typeof(ClosedState), sprint.SprintBacklog.Items[0]!.CurrentStatus.GetType());
            Assert.Equal(typeof(CancelledState), sprint.CurrentStatus.GetType());
        }

        [Fact]
        public void ReleaseSprint_GivenTitleStartDateEndDateScrumMasterProject_WhenSprintGoesThroughProperPhases_ThenReleaseSprint()
        {
            // Arrange
            var mockFactory = new Mock<ISprintFactory<SprintRelease>>();
            var mockDeveloper = new Mock<Developer>(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<List<NotificationProvider>>());
            var mockVersionControl = new Mock<IVersionControlStrategy>();
            var mockProductOwner = new Mock<ProductOwner>(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<List<NotificationProvider>>());
            var mockProject = new Mock<Project>(It.IsAny<string>(), It.IsAny<string>(), mockProductOwner.Object, mockVersionControl.Object);

            mockFactory.Setup(f => f.CreateSprint(It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<Developer>(), It.IsAny<Project>())).Returns(new SprintRelease("Sprint", DateTime.Now, DateTime.Now.AddDays(5), mockDeveloper.Object, mockProject.Object));

            var mockItem = new Mock<Item>(It.IsAny<string>(), It.IsAny<string>(), mockDeveloper.Object, 1, mockFactory.Object.CreateSprint("Sprint", DateTime.Now, DateTime.Now.AddDays(5), mockDeveloper.Object, mockProject.Object).SprintBacklog);
            var mockActivity = new Mock<Activity>(It.IsAny<string>(), mockItem.Object, mockDeveloper.Object);
            mockItem.Object.AddActivityToItem(mockActivity.Object);
            mockActivity.Object.IsFinished = true;

            mockItem.Object.DevelopBacklogItem();
            mockItem.Object.FinalizeDevelopmentBacklogItem();
            mockItem.Object.TestingBacklogItem();
            mockItem.Object.FinalizeTestingBacklogItem();
            mockItem.Object.FinalizeBacklogItem();
            mockItem.Object.CloseBacklogItem();

            var mockPipeline = new Mock<TestPipeline>("Test Pipeline", mockFactory.Object.CreateSprint("Sprint", DateTime.Now, DateTime.Now.AddDays(5), mockDeveloper.Object, mockProject.Object));

            var sprint = mockFactory.Object.CreateSprint("Sprint", DateTime.Now, DateTime.Now.AddDays(5), mockDeveloper.Object, mockProject.Object);
            sprint.Pipeline = mockPipeline.Object;
            sprint.SprintBacklog.AddItemToBacklog(mockItem.Object);

            // Act
            sprint.ExecuteSprint();
            sprint.FinishSprint();
            sprint.ReleaseSprint();

            // Assert
            Assert.NotNull(sprint);
            Assert.NotNull(sprint.SprintBacklog);
            Assert.Equal(typeof(ClosedState), sprint.SprintBacklog.Items[0]!.CurrentStatus.GetType());
            Assert.Equal(typeof(ReleaseState), sprint.CurrentStatus.GetType());
        }
        
        [Fact]
        public void CancelSprint_GivenTitleStartDateEndDateScrumMasterProject_WhenSprintGoesThroughProperPhasesAfterRelease_ThenCancelSprint()
        {
            // Arrange
            var mockFactory = new Mock<ISprintFactory<SprintRelease>>();
            var mockDeveloper = new Mock<Developer>(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<List<NotificationProvider>>());
            var mockVersionControl = new Mock<IVersionControlStrategy>();
            var mockProductOwner = new Mock<ProductOwner>(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<List<NotificationProvider>>());
            var mockProject = new Mock<Project>(It.IsAny<string>(), It.IsAny<string>(), mockProductOwner.Object, mockVersionControl.Object);

            mockFactory.Setup(f => f.CreateSprint(It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<Developer>(), It.IsAny<Project>())).Returns(new SprintRelease("Sprint", DateTime.Now, DateTime.Now.AddDays(5), mockDeveloper.Object, mockProject.Object));

            var mockItem = new Mock<Item>(It.IsAny<string>(), It.IsAny<string>(), mockDeveloper.Object, 1, mockFactory.Object.CreateSprint("Sprint", DateTime.Now, DateTime.Now.AddDays(5), mockDeveloper.Object, mockProject.Object).SprintBacklog);
            var mockActivity = new Mock<Activity>(It.IsAny<string>(), mockItem.Object, mockDeveloper.Object);
            mockItem.Object.AddActivityToItem(mockActivity.Object);
            mockActivity.Object.IsFinished = true;

            mockItem.Object.DevelopBacklogItem();
            mockItem.Object.FinalizeDevelopmentBacklogItem();
            mockItem.Object.TestingBacklogItem();
            mockItem.Object.FinalizeTestingBacklogItem();
            mockItem.Object.FinalizeBacklogItem();
            mockItem.Object.CloseBacklogItem();

            var mockPipeline = new Mock<TestPipeline>("Test Pipeline", mockFactory.Object.CreateSprint("Sprint", DateTime.Now, DateTime.Now.AddDays(5), mockDeveloper.Object, mockProject.Object));

            var sprint = mockFactory.Object.CreateSprint("Sprint", DateTime.Now, DateTime.Now.AddDays(5), mockDeveloper.Object, mockProject.Object);
            sprint.Pipeline = mockPipeline.Object;
            sprint.SprintBacklog.AddItemToBacklog(mockItem.Object);

            // Act
            sprint.ExecuteSprint();
            sprint.FinishSprint();
            sprint.ReleaseSprint();
            sprint.CancelSprint();

            // Assert
            Assert.NotNull(sprint);
            Assert.NotNull(sprint.SprintBacklog);
            Assert.Equal(typeof(ClosedState), sprint.SprintBacklog.Items[0]!.CurrentStatus.GetType());
            Assert.Equal(typeof(CancelledState), sprint.CurrentStatus.GetType());
        }

        [Fact]
        public void ReviewSprint_GivenTitleStartDateEndDateScrumMasterProject_WhenSprintGoesThroughProperPhases_ThenReviewSprint()
        {
            // Arrange
            var mockFactory = new Mock<ISprintFactory<SprintReview>>();
            var mockDeveloper = new Mock<Developer>(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<List<NotificationProvider>>());
            var mockVersionControl = new Mock<IVersionControlStrategy>();
            var mockProductOwner = new Mock<ProductOwner>(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<List<NotificationProvider>>());
            var mockProject = new Mock<Project>(It.IsAny<string>(), It.IsAny<string>(), mockProductOwner.Object, mockVersionControl.Object);

            mockFactory.Setup(f => f.CreateSprint(It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<Developer>(), It.IsAny<Project>())).Returns(new SprintReview("Sprint", DateTime.Now, DateTime.Now.AddDays(5), mockDeveloper.Object, mockProject.Object));

            var mockItem = new Mock<Item>(It.IsAny<string>(), It.IsAny<string>(), mockDeveloper.Object, 1, mockFactory.Object.CreateSprint("Sprint", DateTime.Now, DateTime.Now.AddDays(5), mockDeveloper.Object, mockProject.Object).SprintBacklog);
            var mockActivity = new Mock<Activity>(It.IsAny<string>(), mockItem.Object, mockDeveloper.Object);
            mockItem.Object.AddActivityToItem(mockActivity.Object);
            mockActivity.Object.IsFinished = true;

            mockItem.Object.DevelopBacklogItem();
            mockItem.Object.FinalizeDevelopmentBacklogItem();
            mockItem.Object.TestingBacklogItem();
            mockItem.Object.FinalizeTestingBacklogItem();
            mockItem.Object.FinalizeBacklogItem();
            mockItem.Object.CloseBacklogItem();

            var mockPipeline = new Mock<TestPipeline>("Test Pipeline", mockFactory.Object.CreateSprint("Sprint", DateTime.Now, DateTime.Now.AddDays(5), mockDeveloper.Object, mockProject.Object));

            var sprint = mockFactory.Object.CreateSprint("Sprint", DateTime.Now, DateTime.Now.AddDays(5), mockDeveloper.Object, mockProject.Object);
            sprint.Pipeline = mockPipeline.Object;
            sprint.SprintBacklog.AddItemToBacklog(mockItem.Object);

            // Act
            sprint.ExecuteSprint();
            sprint.FinishSprint();
            sprint.ReviewSprint();

            // Assert
            Assert.NotNull(sprint);
            Assert.NotNull(sprint.SprintBacklog);
            Assert.Equal(typeof(ClosedState), sprint.SprintBacklog.Items[0]!.CurrentStatus.GetType());
            Assert.Equal(typeof(ReviewState), sprint.CurrentStatus.GetType());
        }
        
       [Fact]
        public void CancelSprint_GivenTitleStartDateEndDateScrumMasterProject_WhenSprintGoesThroughProperPhasesAfterReview_ThenCancelSprint()
        {
            // Arrange
            var mockFactory = new Mock<ISprintFactory<SprintReview>>();
            var mockDeveloper = new Mock<Developer>(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<List<NotificationProvider>>());
            var mockVersionControl = new Mock<IVersionControlStrategy>();
            var mockProductOwner = new Mock<ProductOwner>(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<List<NotificationProvider>>());
            var mockProject = new Mock<Project>(It.IsAny<string>(), It.IsAny<string>(), mockProductOwner.Object, mockVersionControl.Object);

            mockFactory.Setup(f => f.CreateSprint(It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<Developer>(), It.IsAny<Project>())).Returns(new SprintReview("Sprint", DateTime.Now, DateTime.Now.AddDays(5), mockDeveloper.Object, mockProject.Object));

            var mockItem = new Mock<Item>(It.IsAny<string>(), It.IsAny<string>(), mockDeveloper.Object, 1, mockFactory.Object.CreateSprint("Sprint", DateTime.Now, DateTime.Now.AddDays(5), mockDeveloper.Object, mockProject.Object).SprintBacklog);
            var mockActivity = new Mock<Activity>(It.IsAny<string>(), mockItem.Object, mockDeveloper.Object);
            mockItem.Object.AddActivityToItem(mockActivity.Object);
            mockActivity.Object.IsFinished = true;

            mockItem.Object.DevelopBacklogItem();
            mockItem.Object.FinalizeDevelopmentBacklogItem();
            mockItem.Object.TestingBacklogItem();
            mockItem.Object.FinalizeTestingBacklogItem();
            mockItem.Object.FinalizeBacklogItem();
            mockItem.Object.CloseBacklogItem();

            var mockPipeline = new Mock<TestPipeline>("Test Pipeline", mockFactory.Object.CreateSprint("Sprint", DateTime.Now, DateTime.Now.AddDays(5), mockDeveloper.Object, mockProject.Object));

            var sprint = mockFactory.Object.CreateSprint("Sprint", DateTime.Now, DateTime.Now.AddDays(5), mockDeveloper.Object, mockProject.Object);
            sprint.Pipeline = mockPipeline.Object;
            sprint.SprintBacklog.AddItemToBacklog(mockItem.Object);

            // Act
            sprint.ExecuteSprint();
            sprint.FinishSprint();
            sprint.ReviewSprint();
            sprint.CancelSprint();

            // Assert
            Assert.NotNull(sprint);
            Assert.NotNull(sprint.SprintBacklog);
            Assert.Equal(typeof(ClosedState), sprint.SprintBacklog.Items[0]!.CurrentStatus.GetType());
            Assert.Equal(typeof(CancelledState), sprint.CurrentStatus.GetType());
        }

        [Fact]
        public void CloseSprint_GivenTitleStartDateEndDateScrumMasterProject_WhenSprintGoesThroughProperPhasesAfterUploadingReview_ThenCloseSprint()
        {
            // Arrange
            var mockFactory = new Mock<ISprintFactory<SprintReview>>();
            var mockDeveloper = new Mock<Developer>(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<List<NotificationProvider>>());
            var mockVersionControl = new Mock<IVersionControlStrategy>();
            var mockProductOwner = new Mock<ProductOwner>(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<List<NotificationProvider>>());
            var mockProject = new Mock<Project>(It.IsAny<string>(), It.IsAny<string>(), mockProductOwner.Object, mockVersionControl.Object);

            mockFactory.Setup(f => f.CreateSprint(It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<Developer>(), It.IsAny<Project>())).Returns(new SprintReview("Sprint", DateTime.Now, DateTime.Now.AddDays(5), mockDeveloper.Object, mockProject.Object));

            var mockItem = new Mock<Item>(It.IsAny<string>(), It.IsAny<string>(), mockDeveloper.Object, 1, mockFactory.Object.CreateSprint("Sprint", DateTime.Now, DateTime.Now.AddDays(5), mockDeveloper.Object, mockProject.Object).SprintBacklog);
            var mockActivity = new Mock<Activity>(It.IsAny<string>(), mockItem.Object, mockDeveloper.Object);
            mockItem.Object.AddActivityToItem(mockActivity.Object);
            mockActivity.Object.IsFinished = true;

            mockItem.Object.DevelopBacklogItem();
            mockItem.Object.FinalizeDevelopmentBacklogItem();
            mockItem.Object.TestingBacklogItem();
            mockItem.Object.FinalizeTestingBacklogItem();
            mockItem.Object.FinalizeBacklogItem();
            mockItem.Object.CloseBacklogItem();

            var mockPipeline = new Mock<TestPipeline>("Test Pipeline", mockFactory.Object.CreateSprint("Sprint", DateTime.Now, DateTime.Now.AddDays(5), mockDeveloper.Object, mockProject.Object));

            var sprint = mockFactory.Object.CreateSprint("Sprint", DateTime.Now, DateTime.Now.AddDays(5), mockDeveloper.Object, mockProject.Object);
            sprint.Pipeline = mockPipeline.Object;
            sprint.SprintBacklog.AddItemToBacklog(mockItem.Object);

            // Act
            sprint.ExecuteSprint();
            sprint.FinishSprint();
            sprint.ReviewSprint();
            sprint.AddReview(new Review("Review", "Sprint review summary"));
            sprint.ReviewSprint();

            // Assert
            Assert.NotNull(sprint);
            Assert.NotNull(sprint.SprintBacklog);
            Assert.Equal(1, sprint.Reviews.Count);
            Assert.Equal(typeof(ClosedState), sprint.SprintBacklog.Items[0]!.CurrentStatus.GetType());
            Assert.Equal(typeof(States.Sprint.ClosedState), sprint.CurrentStatus.GetType());
        }
        
        [Fact]
        public void AddReview_GivenReview_WhenNotInReviewState_ThenDontAddReview()
        {
            // Arrange
            var mockFactory = new Mock<ISprintFactory<SprintReview>>();
            var mockDeveloper = new Mock<Developer>(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<List<NotificationProvider>>());
            var mockVersionControl = new Mock<IVersionControlStrategy>();
            var mockProductOwner = new Mock<ProductOwner>(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<List<NotificationProvider>>());
            var mockProject = new Mock<Project>(It.IsAny<string>(), It.IsAny<string>(), mockProductOwner.Object, mockVersionControl.Object);

            mockFactory.Setup(f => f.CreateSprint(It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<Developer>(), It.IsAny<Project>())).Returns(new SprintReview("Sprint", DateTime.Now, DateTime.Now.AddDays(5), mockDeveloper.Object, mockProject.Object));

            var sprint = mockFactory.Object.CreateSprint("Sprint", DateTime.Now, DateTime.Now.AddDays(5), mockDeveloper.Object, mockProject.Object);

            // Act
            sprint.AddReview(new Review("Review", "Sprint review summary"));

            // Assert
            Assert.Equal(0, sprint.Reviews.Count);
        }

        [Fact]
        public void AddReview_GivenReview_WhenInReviewState_ThenAddReview()
        {
            // Arrange
            var mockFactory = new Mock<ISprintFactory<SprintReview>>();
            var mockDeveloper = new Mock<Developer>(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<List<NotificationProvider>>());
            var mockVersionControl = new Mock<IVersionControlStrategy>();
            var mockProductOwner = new Mock<ProductOwner>(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<List<NotificationProvider>>());
            var mockProject = new Mock<Project>(It.IsAny<string>(), It.IsAny<string>(), mockProductOwner.Object, mockVersionControl.Object);

            mockFactory.Setup(f => f.CreateSprint(It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<Developer>(), It.IsAny<Project>())).Returns(new SprintReview("Sprint", DateTime.Now, DateTime.Now.AddDays(5), mockDeveloper.Object, mockProject.Object));

            var mockItem = new Mock<Item>(It.IsAny<string>(), It.IsAny<string>(), mockDeveloper.Object, 1, mockFactory.Object.CreateSprint("Sprint", DateTime.Now, DateTime.Now.AddDays(5), mockDeveloper.Object, mockProject.Object).SprintBacklog);
            var mockActivity = new Mock<Activity>(It.IsAny<string>(), mockItem.Object, mockDeveloper.Object);
            mockItem.Object.AddActivityToItem(mockActivity.Object);
            mockActivity.Object.IsFinished = true;

            mockItem.Object.DevelopBacklogItem();
            mockItem.Object.FinalizeDevelopmentBacklogItem();
            mockItem.Object.TestingBacklogItem();
            mockItem.Object.FinalizeTestingBacklogItem();
            mockItem.Object.FinalizeBacklogItem();
            mockItem.Object.CloseBacklogItem();

            var mockPipeline = new Mock<TestPipeline>("Test Pipeline", mockFactory.Object.CreateSprint("Sprint", DateTime.Now, DateTime.Now.AddDays(5), mockDeveloper.Object, mockProject.Object));

            var sprint = mockFactory.Object.CreateSprint("Sprint", DateTime.Now, DateTime.Now.AddDays(5), mockDeveloper.Object, mockProject.Object);
            sprint.Pipeline = mockPipeline.Object;
            sprint.SprintBacklog.AddItemToBacklog(mockItem.Object);

            // Act
            sprint.ExecuteSprint();
            sprint.FinishSprint();
            sprint.ReviewSprint();
            sprint.AddReview(new Review("Review", "Sprint review summary"));

            // Assert
            Assert.Equal(1, sprint.Reviews.Count);
        }
        
        [Fact]
        public void AddDifferentReview_GivenReview_WhenInReviewStateAfterRemovingOldReview_ThenAddReview()
        {
            // Arrange
            var mockFactory = new Mock<ISprintFactory<SprintReview>>();
            var mockDeveloper = new Mock<Developer>(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<List<NotificationProvider>>());
            var mockVersionControl = new Mock<IVersionControlStrategy>();
            var mockProductOwner = new Mock<ProductOwner>(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<List<NotificationProvider>>());
            var mockProject = new Mock<Project>(It.IsAny<string>(), It.IsAny<string>(), mockProductOwner.Object, mockVersionControl.Object);

            mockFactory.Setup(f => f.CreateSprint(It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<Developer>(), It.IsAny<Project>())).Returns(new SprintReview("Sprint", DateTime.Now, DateTime.Now.AddDays(5), mockDeveloper.Object, mockProject.Object));

            var mockItem = new Mock<Item>(It.IsAny<string>(), It.IsAny<string>(), mockDeveloper.Object, 1, mockFactory.Object.CreateSprint("Sprint", DateTime.Now, DateTime.Now.AddDays(5), mockDeveloper.Object, mockProject.Object).SprintBacklog);
            var mockActivity = new Mock<Activity>(It.IsAny<string>(), mockItem.Object, mockDeveloper.Object);
            mockItem.Object.AddActivityToItem(mockActivity.Object);
            mockActivity.Object.IsFinished = true;

            mockItem.Object.DevelopBacklogItem();
            mockItem.Object.FinalizeDevelopmentBacklogItem();
            mockItem.Object.TestingBacklogItem();
            mockItem.Object.FinalizeTestingBacklogItem();
            mockItem.Object.FinalizeBacklogItem();
            mockItem.Object.CloseBacklogItem();

            var mockPipeline = new Mock<TestPipeline>("Test Pipeline", mockFactory.Object.CreateSprint("Sprint", DateTime.Now, DateTime.Now.AddDays(5), mockDeveloper.Object, mockProject.Object));

            var sprint = mockFactory.Object.CreateSprint("Sprint", DateTime.Now, DateTime.Now.AddDays(5), mockDeveloper.Object, mockProject.Object);
            sprint.Pipeline = mockPipeline.Object;
            sprint.SprintBacklog.AddItemToBacklog(mockItem.Object);

            Review initialReview = new Review("Review", "Sprint review summary");
            Review newReview = new Review("newReview", "Sprint review summary");

            // Act
            sprint.ExecuteSprint();
            sprint.FinishSprint();
            sprint.ReviewSprint();
            sprint.AddReview(initialReview);
            sprint.RemoveReview(initialReview);
            sprint.AddReview(newReview);

            // Assert
            Assert.Equal(1, sprint.Reviews.Count);
            Assert.NotEqual(initialReview, sprint.Reviews[0]);
            Assert.Equal(newReview, sprint.Reviews[0]);
        }
    }
}