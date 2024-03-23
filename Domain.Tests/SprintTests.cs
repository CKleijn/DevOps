using Domain.States.Pipeline;
using Domain.States.Sprint;
using CancelledState = Domain.States.Sprint.CancelledState;
using ClosedState = Domain.States.BacklogItem.ClosedState;
using FinishedState = Domain.States.Sprint.FinishedState;

namespace Domain.Tests
{
    public class SprintTests
    {
        [Fact]
        public void CreateSprint_GivenTitleStartDateEndDateScrumMasterProject_WhenNoPreConditions_ThenCreateSprint()
        {
            // Arrange
            ISprintFactory<SprintReview> factory = new SprintReviewFactory();
            
            List<NotificationProvider> notificationProviders = new();
            notificationProviders.Add(NotificationProvider.MAIL);
            Developer scrumMaster = new("name", "email", "password", notificationProviders);
            
            string title = "Initial Sprint";
            DateTime startTime = DateTime.Now;
            DateTime endTime = DateTime.Now.AddDays(14);
            
            IVersionControlStrategy versionControl = new GitHub();
            ProductOwner productOwner = new("name", "email", "password", notificationProviders);
         
            Project project = new("Project", "Description", productOwner, versionControl);

            // Act
            SprintReview sprint = factory.CreateSprint(title, startTime, endTime, scrumMaster, project);

            // Assert
            Assert.NotNull(sprint);
            Assert.Equal(title, sprint.Title);
            Assert.Equal(startTime, sprint.StartDate);
            Assert.Equal(endTime, sprint.EndDate);
            Assert.Equal(scrumMaster, sprint.ScrumMaster);
            Assert.Equal(project, sprint.Project);
        }
        
        [Fact]
        public void UpdateSprint_GivenTitleStartDateEndDateScrumMasterProjectReviewDeveloperTesterReport_WhenSprintIsInInitialState_ThenUpdateSprint()
        {
            // Arrange
            ISprintFactory<SprintReview> factory = new SprintReviewFactory();
        
            string initialTitle = "Initial Sprint";
            DateTime initialStartTime = DateTime.Now;
            DateTime initialEndTime = DateTime.Now.AddDays(14);
            
            List<NotificationProvider> notificationProviders = new();
            notificationProviders.Add(NotificationProvider.MAIL);
            Developer scrumMaster = new("name", "email", "password", notificationProviders);
            Developer newScrumMaster = new("name", "email", "password", notificationProviders);
            
            IVersionControlStrategy versionControl = new GitHub();
            ProductOwner productOwner = new("name", "email", "password", notificationProviders);
         
            Project project = new("Project", "Description", productOwner, versionControl);
        
            SprintReview sprint = factory.CreateSprint(initialTitle, initialStartTime, initialEndTime, scrumMaster, project);
            TestPipeline pipeline = new("Test Pipeline", sprint);
            sprint.Pipeline = pipeline;
            
            string newTitle = "New Title";
            DateTime newStartTime = DateTime.Now.AddDays(1);
            DateTime newEndTime = DateTime.Now.AddDays(15);

            Developer developer = new("name", "email", "password", notificationProviders);
            Developer newDeveloper = new("name", "email", "password", notificationProviders);
            
            Developer tester = new("name", "email", "password", notificationProviders);
            Developer newTester = new("name", "email", "password", notificationProviders);
            
            Report report = new("Report", sprint, ReportExtension.JPG);
            Report newReport = new("newReport", sprint, ReportExtension.PDF);
        
            // Act
            sprint.Title = newTitle;
            sprint.StartDate = newStartTime;
            sprint.EndDate = newEndTime;
            sprint.ScrumMaster = newScrumMaster;

            sprint.AddDeveloper(developer);
            sprint.RemoveDeveloper(developer);
            sprint.AddDeveloper(newDeveloper);
            sprint.AddTester(tester);
            sprint.RemoveTester(tester);
            sprint.AddTester(newTester);
            sprint.AddReport(report);
            sprint.RemoveReport(report);
            sprint.AddReport(newReport);
            
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
            ISprintFactory<SprintReview> factory = new SprintReviewFactory();
        
            string initialTitle = "Initial Sprint";
            DateTime initialStartTime = DateTime.Now;
            DateTime initialEndTime = DateTime.Now.AddDays(14);
            
            List<NotificationProvider> notificationProviders = new();
            notificationProviders.Add(NotificationProvider.MAIL);
            Developer scrumMaster = new("name", "email", "password", notificationProviders);
            Developer newScrumMaster = new("name", "email", "password", notificationProviders);
            
            IVersionControlStrategy versionControl = new GitHub();
            ProductOwner productOwner = new("name", "email", "password", notificationProviders);
         
            Project project = new("Project", "Description", productOwner, versionControl);
        
            SprintReview sprint = factory.CreateSprint(initialTitle, initialStartTime, initialEndTime, scrumMaster, project);
            TestPipeline pipeline = new("Test Pipeline", sprint);
            sprint.Pipeline = pipeline;
            
            string newTitle = "New Title";
            DateTime newStartTime = DateTime.Now.AddDays(1);
            DateTime newEndTime = DateTime.Now.AddDays(15);

            Developer developer = new("name", "email", "password", notificationProviders);
            Developer newDeveloper = new("name", "email", "password", notificationProviders);
            
            Developer tester = new("name", "email", "password", notificationProviders);
            Developer newTester = new("name", "email", "password", notificationProviders);
            
            Report report = new("Report", sprint, ReportExtension.JPG);
            Report newReport = new("newReport", sprint, ReportExtension.PDF);
        
            // Act
            sprint.ExecuteSprint();
            
            sprint.Title = newTitle;
            sprint.StartDate = newStartTime;
            sprint.EndDate = newEndTime;
            sprint.ScrumMaster = newScrumMaster;

            sprint.AddDeveloper(developer);
            sprint.RemoveDeveloper(developer);
            sprint.AddDeveloper(newDeveloper);
            sprint.AddTester(tester);
            sprint.RemoveTester(tester);
            sprint.AddTester(newTester);
            sprint.AddReport(report);
            sprint.RemoveReport(report);
            sprint.AddReport(newReport);
            
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
            ISprintFactory<SprintReview> factory = new SprintReviewFactory();
        
            string initialTitle = "Initial Sprint";
            DateTime initialStartTime = DateTime.Now;
            DateTime initialEndTime = DateTime.Now.AddDays(14);
            
            List<NotificationProvider> notificationProviders = new();
            notificationProviders.Add(NotificationProvider.MAIL);
            Developer scrumMaster = new("name", "email", "password", notificationProviders);
            Developer newScrumMaster = new("name", "email", "password", notificationProviders);
            
            IVersionControlStrategy versionControl = new GitHub();
            ProductOwner productOwner = new("name", "email", "password", notificationProviders);
         
            Project project = new("Project", "Description", productOwner, versionControl);
        
            SprintReview sprint = factory.CreateSprint(initialTitle, initialStartTime, initialEndTime, scrumMaster, project);
            TestPipeline pipeline = new("Test Pipeline", sprint);
            sprint.Pipeline = pipeline;
            
            string newTitle = "New Title";
            DateTime newStartTime = DateTime.Now.AddDays(1);
            DateTime newEndTime = DateTime.Now.AddDays(15);

            Developer developer = new("name", "email", "password", notificationProviders);
            Developer newDeveloper = new("name", "email", "password", notificationProviders);
            
            Developer tester = new("name", "email", "password", notificationProviders);
            Developer newTester = new("name", "email", "password", notificationProviders);
            
            Report report = new("Report", sprint, ReportExtension.JPG);
            Report newReport = new("newReport", sprint, ReportExtension.PDF);
        
            // Act
            sprint.Pipeline.ExecutePipeline();
            
            sprint.Title = newTitle;
            sprint.StartDate = newStartTime;
            sprint.EndDate = newEndTime;
            sprint.ScrumMaster = newScrumMaster;

            sprint.AddDeveloper(developer);
            sprint.RemoveDeveloper(developer);
            sprint.AddDeveloper(newDeveloper);
            sprint.AddTester(tester);
            sprint.RemoveTester(tester);
            sprint.AddTester(newTester);
            sprint.AddReport(report);
            sprint.RemoveReport(report);
            sprint.AddReport(newReport);
            
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
            ISprintFactory<SprintReview> factory = new SprintReviewFactory();
        
            string initialTitle = "Initial Sprint";
            DateTime initialStartTime = DateTime.Now;
            DateTime initialEndTime = DateTime.Now.AddDays(-1);
            
            List<NotificationProvider> notificationProviders = new();
            notificationProviders.Add(NotificationProvider.MAIL);
            Developer scrumMaster = new("name", "email", "password", notificationProviders);
            
            IVersionControlStrategy versionControl = new GitHub();
            ProductOwner productOwner = new("name", "email", "password", notificationProviders);
         
            Project project = new("Project", "Description", productOwner, versionControl);
    
            SprintReview sprint = factory.CreateSprint(initialTitle, initialStartTime, initialEndTime, scrumMaster, project);
            
            Item item = new("Item", "Description", scrumMaster, 1, sprint.SprintBacklog);
            Activity activity = new("Activity", item, scrumMaster);
            item.AddActivityToItem(activity);
            activity.IsFinished = true;
    
            item.DevelopBacklogItem();
            item.FinalizeDevelopmentBacklogItem();
            item.TestingBacklogItem();
            item.FinalizeTestingBacklogItem();
            item.FinalizeBacklogItem();
            item.CloseBacklogItem();
        
            TestPipeline pipeline = new("Test Pipeline", sprint);
            sprint.Pipeline = pipeline;
            
            sprint.SprintBacklog.AddItemToBacklog(item);
        
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
            ISprintFactory<SprintRelease> factory = new SprintReleaseFactory();
        
            string initialTitle = "Initial Sprint";
            DateTime initialStartTime = DateTime.Now;
            DateTime initialEndTime = DateTime.Now.AddDays(-1);
            
            List<NotificationProvider> notificationProviders = new();
            notificationProviders.Add(NotificationProvider.MAIL);
            Developer scrumMaster = new("name", "email", "password", notificationProviders);
            
            IVersionControlStrategy versionControl = new GitHub();
            ProductOwner productOwner = new("name", "email", "password", notificationProviders);
         
            Project project = new("Project", "Description", productOwner, versionControl);
    
            SprintRelease sprint = factory.CreateSprint(initialTitle, initialStartTime, initialEndTime, scrumMaster, project);
            
            Item item = new("Item", "Description", scrumMaster, 1, sprint.SprintBacklog);
            Activity activity = new("Activity", item, scrumMaster);
            item.AddActivityToItem(activity);
            activity.IsFinished = true;
    
            item.DevelopBacklogItem();
            item.FinalizeDevelopmentBacklogItem();
            item.TestingBacklogItem();
            item.FinalizeTestingBacklogItem();
            item.FinalizeBacklogItem();
            item.CloseBacklogItem();
        
            TestPipeline pipeline = new("Test Pipeline", sprint);
            sprint.Pipeline = pipeline;
            
            sprint.SprintBacklog.AddItemToBacklog(item);
        
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
            ISprintFactory<SprintRelease> factory = new SprintReleaseFactory();
            
            List<NotificationProvider> notificationProviders = new();
            notificationProviders.Add(NotificationProvider.MAIL);
            Developer scrumMaster = new("name", "email", "password", notificationProviders);
            
            IVersionControlStrategy versionControl = new GitHub();
            ProductOwner productOwner = new("name", "email", "password", notificationProviders);
         
            Project project = new("Project", "Description", productOwner, versionControl);
    
            SprintRelease sprint = factory.CreateSprint("Sprint", DateTime.Now, DateTime.Now.AddDays(5), scrumMaster, project);
        
            Item item = new("Item", "Description", scrumMaster, 1, sprint.SprintBacklog);
            sprint.SprintBacklog.AddItemToBacklog(item);
            
            Activity activity = new("Activity", item, scrumMaster);
            item.AddActivityToItem(activity);
            activity.IsFinished = true;
    
            item.DevelopBacklogItem();
            item.FinalizeDevelopmentBacklogItem();
            item.TestingBacklogItem();
            item.FinalizeTestingBacklogItem();
            item.FinalizeBacklogItem();
            item.CloseBacklogItem();
            
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
            ISprintFactory<SprintRelease> factory = new SprintReleaseFactory();
            
            List<NotificationProvider> notificationProviders = new();
            notificationProviders.Add(NotificationProvider.MAIL);
            Developer scrumMaster = new("name", "email", "password", notificationProviders);
            
            IVersionControlStrategy versionControl = new GitHub();
            ProductOwner productOwner = new("name", "email", "password", notificationProviders);
         
            Project project = new("Project", "Description", productOwner, versionControl);
    
            SprintRelease sprint = factory.CreateSprint("Sprint", DateTime.Now, DateTime.Now.AddDays(5), scrumMaster, project);
            
            Item item = new("Item", "Description", scrumMaster, 1, sprint.SprintBacklog);
            sprint.SprintBacklog.AddItemToBacklog(item);
            
            Activity activity = new("Activity", item, scrumMaster);
            item.AddActivityToItem(activity);
            activity.IsFinished = true;
    
            item.DevelopBacklogItem();
            item.FinalizeDevelopmentBacklogItem();
            item.TestingBacklogItem();
            item.FinalizeTestingBacklogItem();
            item.FinalizeBacklogItem();
            item.CloseBacklogItem();
        
            TestPipeline pipeline = new("Test Pipeline", sprint);
            sprint.Pipeline = pipeline;
            
        
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
            ISprintFactory<SprintRelease> factory = new SprintReleaseFactory();
            
            List<NotificationProvider> notificationProviders = new();
            notificationProviders.Add(NotificationProvider.MAIL);
            Developer scrumMaster = new("name", "email", "password", notificationProviders);
            
            IVersionControlStrategy versionControl = new GitHub();
            ProductOwner productOwner = new("name", "email", "password", notificationProviders);
         
            Project project = new("Project", "Description", productOwner, versionControl);
    
            SprintRelease sprint = factory.CreateSprint("Sprint", DateTime.Now, DateTime.Now.AddDays(5), scrumMaster, project);
            
            Item item = new("Item", "Description", scrumMaster, 1, sprint.SprintBacklog);
            sprint.SprintBacklog.AddItemToBacklog(item);
            
            Activity activity = new("Activity", item, scrumMaster);
            item.AddActivityToItem(activity);
            activity.IsFinished = true;
    
            item.DevelopBacklogItem();
            item.FinalizeDevelopmentBacklogItem();
            item.TestingBacklogItem();
            item.FinalizeTestingBacklogItem();
            item.FinalizeBacklogItem();
            item.CloseBacklogItem();
        
            TestPipeline pipeline = new("Test Pipeline", sprint);
            sprint.Pipeline = pipeline;
            
        
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
            ISprintFactory<SprintRelease> factory = new SprintReleaseFactory();
            
            List<NotificationProvider> notificationProviders = new();
            notificationProviders.Add(NotificationProvider.MAIL);
            Developer scrumMaster = new("name", "email", "password", notificationProviders);
            
            IVersionControlStrategy versionControl = new GitHub();
            ProductOwner productOwner = new("name", "email", "password", notificationProviders);
         
            Project project = new("Project", "Description", productOwner, versionControl);
    
            SprintRelease sprint = factory.CreateSprint("Sprint", DateTime.Now, DateTime.Now.AddDays(5), scrumMaster, project);
            
            Item item = new("Item", "Description", scrumMaster, 1, sprint.SprintBacklog);
            sprint.SprintBacklog.AddItemToBacklog(item);
            
            Activity activity = new("Activity", item, scrumMaster);
            item.AddActivityToItem(activity);
            activity.IsFinished = true;
    
            item.DevelopBacklogItem();
            item.FinalizeDevelopmentBacklogItem();
            item.TestingBacklogItem();
            item.FinalizeTestingBacklogItem();
            item.FinalizeBacklogItem();
            item.CloseBacklogItem();
        
            TestPipeline pipeline = new("Test Pipeline", sprint);
            sprint.Pipeline = pipeline;
            
        
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
            ISprintFactory<SprintRelease> factory = new SprintReleaseFactory();
            
            List<NotificationProvider> notificationProviders = new();
            notificationProviders.Add(NotificationProvider.MAIL);
            Developer scrumMaster = new("name", "email", "password", notificationProviders);
            
            IVersionControlStrategy versionControl = new GitHub();
            ProductOwner productOwner = new("name", "email", "password", notificationProviders);
         
            Project project = new("Project", "Description", productOwner, versionControl);
    
            SprintRelease sprint = factory.CreateSprint("Sprint", DateTime.Now, DateTime.Now.AddDays(5), scrumMaster, project);
            
            Item item = new("Item", "Description", scrumMaster, 1, sprint.SprintBacklog);
            sprint.SprintBacklog.AddItemToBacklog(item);
            
            Activity activity = new("Activity", item, scrumMaster);
            item.AddActivityToItem(activity);
            activity.IsFinished = true;
    
            item.DevelopBacklogItem();
            item.FinalizeDevelopmentBacklogItem();
            item.TestingBacklogItem();
            item.FinalizeTestingBacklogItem();
            item.FinalizeBacklogItem();
            item.CloseBacklogItem();
        
            TestPipeline pipeline = new("Test Pipeline", sprint);
            sprint.Pipeline = pipeline;
            
        
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
            ISprintFactory<SprintReview> factory = new SprintReviewFactory();
            
            List<NotificationProvider> notificationProviders = new();
            notificationProviders.Add(NotificationProvider.MAIL);
            Developer scrumMaster = new("name", "email", "password", notificationProviders);
            
            IVersionControlStrategy versionControl = new GitHub();
            ProductOwner productOwner = new("name", "email", "password", notificationProviders);
         
            Project project = new("Project", "Description", productOwner, versionControl);
    
            SprintReview sprint = factory.CreateSprint("Sprint", DateTime.Now, DateTime.Now.AddDays(5), scrumMaster, project);
            
            Item item = new("Item", "Description", scrumMaster, 1, sprint.SprintBacklog);
            sprint.SprintBacklog.AddItemToBacklog(item);
            
            Activity activity = new("Activity", item, scrumMaster);
            item.AddActivityToItem(activity);
            activity.IsFinished = true;
    
            item.DevelopBacklogItem();
            item.FinalizeDevelopmentBacklogItem();
            item.TestingBacklogItem();
            item.FinalizeTestingBacklogItem();
            item.FinalizeBacklogItem();
            item.CloseBacklogItem();
        
            TestPipeline pipeline = new("Test Pipeline", sprint);
            sprint.Pipeline = pipeline;
            
        
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
            ISprintFactory<SprintReview> factory = new SprintReviewFactory();
            
            List<NotificationProvider> notificationProviders = new();
            notificationProviders.Add(NotificationProvider.MAIL);
            Developer scrumMaster = new("name", "email", "password", notificationProviders);
            
            IVersionControlStrategy versionControl = new GitHub();
            ProductOwner productOwner = new("name", "email", "password", notificationProviders);
         
            Project project = new("Project", "Description", productOwner, versionControl);
    
            SprintReview sprint = factory.CreateSprint("Sprint", DateTime.Now, DateTime.Now.AddDays(5), scrumMaster, project);
            
            Item item = new("Item", "Description", scrumMaster, 1, sprint.SprintBacklog);
            sprint.SprintBacklog.AddItemToBacklog(item);
            
            Activity activity = new("Activity", item, scrumMaster);
            item.AddActivityToItem(activity);
            activity.IsFinished = true;
    
            item.DevelopBacklogItem();
            item.FinalizeDevelopmentBacklogItem();
            item.TestingBacklogItem();
            item.FinalizeTestingBacklogItem();
            item.FinalizeBacklogItem();
            item.CloseBacklogItem();
        
            TestPipeline pipeline = new("Test Pipeline", sprint);
            sprint.Pipeline = pipeline;
        
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
            ISprintFactory<SprintReview> factory = new SprintReviewFactory();
            
            List<NotificationProvider> notificationProviders = new();
            notificationProviders.Add(NotificationProvider.MAIL);
            Developer scrumMaster = new("name", "email", "password", notificationProviders);
            
            IVersionControlStrategy versionControl = new GitHub();
            ProductOwner productOwner = new("name", "email", "password", notificationProviders);
         
            Project project = new("Project", "Description", productOwner, versionControl);
    
            SprintReview sprint = factory.CreateSprint("Sprint", DateTime.Now, DateTime.Now.AddDays(5), scrumMaster, project);
            
            Item item = new("Item", "Description", scrumMaster, 1, sprint.SprintBacklog);
            sprint.SprintBacklog.AddItemToBacklog(item);
            
            Activity activity = new("Activity", item, scrumMaster);
            item.AddActivityToItem(activity);
            activity.IsFinished = true;
    
            item.DevelopBacklogItem();
            item.FinalizeDevelopmentBacklogItem();
            item.TestingBacklogItem();
            item.FinalizeTestingBacklogItem();
            item.FinalizeBacklogItem();
            item.CloseBacklogItem();
        
            TestPipeline pipeline = new("Test Pipeline", sprint);
            sprint.Pipeline = pipeline;
        
            // Act
            sprint.ExecuteSprint();
            sprint.FinishSprint();
            sprint.ReviewSprint();
            
            //upload review rapport
            sprint.AddReview(new Review("Review", "Sprint review summary"));
            
            //review sprint again
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
            ISprintFactory<SprintReview> factory = new SprintReviewFactory();
            
            List<NotificationProvider> notificationProviders = new();
            notificationProviders.Add(NotificationProvider.MAIL);
            Developer scrumMaster = new("name", "email", "password", notificationProviders);
            
            IVersionControlStrategy versionControl = new GitHub();
            ProductOwner productOwner = new("name", "email", "password", notificationProviders);
         
            Project project = new("Project", "Description", productOwner, versionControl);
    
            SprintReview sprint = factory.CreateSprint("Sprint", DateTime.Now, DateTime.Now.AddDays(5), scrumMaster, project);
            
            // Act
            sprint.AddReview(new Review("Review", "Sprint review summary"));
            
            // Assert
            Assert.Equal(0, sprint.Reviews.Count);
        }
        
                [Fact]
        public void AddReview_GivenReview_WhenInReviewState_ThenAddReview()
        {
            // Arrange
            ISprintFactory<SprintReview> factory = new SprintReviewFactory();
            
            List<NotificationProvider> notificationProviders = new();
            notificationProviders.Add(NotificationProvider.MAIL);
            Developer scrumMaster = new("name", "email", "password", notificationProviders);
            
            IVersionControlStrategy versionControl = new GitHub();
            ProductOwner productOwner = new("name", "email", "password", notificationProviders);
         
            Project project = new("Project", "Description", productOwner, versionControl);
    
            SprintReview sprint = factory.CreateSprint("Sprint", DateTime.Now, DateTime.Now.AddDays(5), scrumMaster, project);
            
            Item item = new("Item", "Description", scrumMaster, 1, sprint.SprintBacklog);
            sprint.SprintBacklog.AddItemToBacklog(item);
            
            Activity activity = new("Activity", item, scrumMaster);
            item.AddActivityToItem(activity);
            activity.IsFinished = true;
    
            item.DevelopBacklogItem();
            item.FinalizeDevelopmentBacklogItem();
            item.TestingBacklogItem();
            item.FinalizeTestingBacklogItem();
            item.FinalizeBacklogItem();
            item.CloseBacklogItem();
        
            TestPipeline pipeline = new("Test Pipeline", sprint);
            sprint.Pipeline = pipeline;
        
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
            ISprintFactory<SprintReview> factory = new SprintReviewFactory();
            
            List<NotificationProvider> notificationProviders = new();
            notificationProviders.Add(NotificationProvider.MAIL);
            Developer scrumMaster = new("name", "email", "password", notificationProviders);
            
            IVersionControlStrategy versionControl = new GitHub();
            ProductOwner productOwner = new("name", "email", "password", notificationProviders);
         
            Project project = new("Project", "Description", productOwner, versionControl);
    
            SprintReview sprint = factory.CreateSprint("Sprint", DateTime.Now, DateTime.Now.AddDays(5), scrumMaster, project);
            
            Item item = new("Item", "Description", scrumMaster, 1, sprint.SprintBacklog);
            sprint.SprintBacklog.AddItemToBacklog(item);
            
            Activity activity = new("Activity", item, scrumMaster);
            item.AddActivityToItem(activity);
            activity.IsFinished = true;
    
            item.DevelopBacklogItem();
            item.FinalizeDevelopmentBacklogItem();
            item.TestingBacklogItem();
            item.FinalizeTestingBacklogItem();
            item.FinalizeBacklogItem();
            item.CloseBacklogItem();
        
            TestPipeline pipeline = new("Test Pipeline", sprint);
            sprint.Pipeline = pipeline;

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