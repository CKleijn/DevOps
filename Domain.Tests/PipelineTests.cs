using Domain.Actions;
using Domain.States.Pipeline;
using Moq;

namespace Domain.Tests;

[Collection("SequentialTest")]
public class PipelineTests
{
    [Fact]
    public void CreatePipeline_GivenTitleSprint_WhenNoPreConditions_ThenCreatePipeline()
    {
        // Arrange
        var title = "Pipeline 1";
        var productOwnerMock = new Mock<ProductOwner>("Piet", "piet@mail.com", "Password", new List<NotificationProvider> { NotificationProvider.MAIL });
        var developerMock = new Mock<Developer>("Klaas", "klaas@mail.com", "Password1", new List<NotificationProvider> { NotificationProvider.MAIL });
        var githubMock = new Mock<GitHub>();
        var projectMock = new Mock<Project>("Project 1", "Description 1", productOwnerMock.Object, githubMock.Object);
        var sprintMock = new Mock<SprintRelease>("Sprint 1", DateTime.Now, DateTime.Now, developerMock.Object, projectMock.Object);

        // Act
        var releasePipeline = new ReleasePipeline(title, sprintMock.Object);

        // Assert
        Assert.NotNull(releasePipeline);
        Assert.IsAssignableFrom<Pipeline>(releasePipeline);
        Assert.IsType<ReleasePipeline>(releasePipeline);
        Assert.Equal(title, releasePipeline.Name);
        Assert.Equal(sprintMock.Object, releasePipeline.Sprint);
    }

    [Fact]
    public void CreatePipelineWithActions_GivenTitleSprint_WhenNoPreConditions_ThenCreatePipelineWithActions()
    {
        // Arrange
        var title = "Pipeline 1";
        var productOwnerMock = new Mock<ProductOwner>("Piet", "piet@mail.com", "Password", new List<NotificationProvider> { NotificationProvider.MAIL });
        var developerMock = new Mock<Developer>("Klaas", "klaas@mail.com", "Password1", new List<NotificationProvider> { NotificationProvider.MAIL });
        var githubMock = new Mock<GitHub>();
        var projectMock = new Mock<Project>("Project 1", "Description 1", productOwnerMock.Object, githubMock.Object);
        var sprintMock = new Mock<SprintRelease>("Sprint 1", DateTime.Now, DateTime.Now, developerMock.Object, projectMock.Object);
        var buildAction = new NpmBuildAction();

        var mockTextWriter = new StringWriter();
        Console.SetOut(mockTextWriter);

        // Act
        var releasePipeline = new ReleasePipeline(title, sprintMock.Object);
        var allActions = releasePipeline.Print();
        var allSelectedActionsBeforeAdd = releasePipeline.PrintSelectedActions();
        releasePipeline.AddAction(buildAction);
        var allSelectedActionsAfterAdd = releasePipeline.PrintSelectedActions();

        var output = mockTextWriter.ToString().Trim();

        // Assert
        Assert.NotNull(releasePipeline);
        Assert.IsAssignableFrom<Pipeline>(releasePipeline);
        Assert.IsType<ReleasePipeline>(releasePipeline);
        Assert.Equal(title, releasePipeline.Name);
        Assert.Equal(sprintMock.Object, releasePipeline.Sprint);
        Assert.Contains("All pipeline actions", allActions);
        Assert.Contains($"Item {buildAction.Id} has been added!", output);
        Assert.Contains("Selected pipeline actions", allSelectedActionsBeforeAdd);
        Assert.DoesNotContain(nameof(NpmBuildAction), allSelectedActionsBeforeAdd);
        Assert.Contains("Selected pipeline actions", allSelectedActionsAfterAdd);
        Assert.Contains(nameof(NpmBuildAction), allSelectedActionsAfterAdd);
    }

    [Fact]
    public void UpdatePipelineWithActions_GivenAction_WhenPipelineExists_ThenUpdatePipelineWithActions()
    {
        // Arrange
        var title = "Pipeline 1";
        var productOwnerMock = new Mock<ProductOwner>("Piet", "piet@mail.com", "Password", new List<NotificationProvider> { NotificationProvider.MAIL });
        var developerMock = new Mock<Developer>("Klaas", "klaas@mail.com", "Password1", new List<NotificationProvider> { NotificationProvider.MAIL });
        var githubMock = new Mock<GitHub>();
        var projectMock = new Mock<Project>("Project 1", "Description 1", productOwnerMock.Object, githubMock.Object);
        var sprintMock = new Mock<SprintRelease>("Sprint 1", DateTime.Now, DateTime.Now, developerMock.Object, projectMock.Object);
        var buildAction = new NpmBuildAction();

        var releasePipeline = new ReleasePipeline(title, sprintMock.Object);
        releasePipeline.AddAction(buildAction);

        var mockTextWriter = new StringWriter();
        Console.SetOut(mockTextWriter);

        // Act
        var allActions = releasePipeline.Print();
        var allSelectedActionsBeforeRemove = releasePipeline.PrintSelectedActions();
        releasePipeline.RemoveAction(buildAction);
        var allSelectedActionsAfterRemove = releasePipeline.PrintSelectedActions();

        var output = mockTextWriter.ToString().Trim();

        // Assert
        Assert.NotNull(releasePipeline);
        Assert.IsAssignableFrom<Pipeline>(releasePipeline);
        Assert.IsType<ReleasePipeline>(releasePipeline);
        Assert.Equal(title, releasePipeline.Name);
        Assert.Equal(sprintMock.Object, releasePipeline.Sprint);
        Assert.Contains("All pipeline actions", allActions);
        Assert.Contains($"Item {buildAction.Id} has been removed!", output);
        Assert.Contains("Selected pipeline actions", allSelectedActionsBeforeRemove);
        Assert.Contains(nameof(NpmBuildAction), allSelectedActionsBeforeRemove);
        Assert.Contains("Selected pipeline actions", allSelectedActionsAfterRemove);
        Assert.DoesNotContain(nameof(NpmBuildAction), allSelectedActionsAfterRemove);
    }

    [Fact]
    public void CreatePipelineWithActions_GivenTitleSprint_WhenNoPreConditions_ThenCreatePipelineWithActionsAndFinishAfterExecuting()
    {
        // Arrange
        var title = "Pipeline 1";
        var productOwnerMock = new Mock<ProductOwner>("Piet", "piet@mail.com", "Password", new List<NotificationProvider> { NotificationProvider.MAIL });
        var developerMock = new Mock<Developer>("Klaas", "klaas@mail.com", "Password1", new List<NotificationProvider> { NotificationProvider.MAIL });
        var githubMock = new Mock<GitHub>();
        var projectMock = new Mock<Project>("Project 1", "Description 1", productOwnerMock.Object, githubMock.Object);
        var sprintMock = new Mock<SprintRelease>("Sprint 1", DateTime.Now, DateTime.Now, developerMock.Object, projectMock.Object);
        var sourceAction = new GitCloneAction();
        var packageAction = new DotnetRestoreAction();
        var buildAction = new DotnetBuildAction();
        var testAction = new DotnetTestAction();
        var analyseAction = new DotnetAnalyzeAction();
        var deployAction = new DotnetPublishAction();
        var utilityAction = new DotnetCleanAction();

        var mockTextWriter = new StringWriter();
        Console.SetOut(mockTextWriter);

        // Act
        var releasePipeline = new ReleasePipeline(title, sprintMock.Object);
        releasePipeline.AddAction(sourceAction);
        releasePipeline.AddAction(packageAction);
        releasePipeline.AddAction(buildAction);
        releasePipeline.AddAction(testAction);
        releasePipeline.AddAction(analyseAction);
        releasePipeline.AddAction(deployAction);
        releasePipeline.AddAction(utilityAction);
        releasePipeline.ExecutePipeline();

        var output = mockTextWriter.ToString().Trim();

        // Assert
        Assert.NotNull(releasePipeline);
        Assert.IsAssignableFrom<Pipeline>(releasePipeline);
        Assert.IsType<ReleasePipeline>(releasePipeline);
        Assert.Equal(title, releasePipeline.Name);
        Assert.Equal(sprintMock.Object, releasePipeline.Sprint);
        Assert.Contains($"Item {sourceAction.Id} has been added!", output);
        Assert.Contains($"Item {packageAction.Id} has been added!", output);
        Assert.Contains($"Item {buildAction.Id} has been added!", output);
        Assert.Contains($"Item {testAction.Id} has been added!", output);
        Assert.Contains($"Item {analyseAction.Id} has been added!", output);
        Assert.Contains($"Item {deployAction.Id} has been added!", output);
        Assert.Contains($"Item {utilityAction.Id} has been added!", output);
        Assert.Contains($"Successfully executed {sourceAction.Command} without any errors!", output);
        Assert.Contains($"Successfully executed {packageAction.Command} without any errors!", output);
        Assert.Contains($"Successfully executed {buildAction.Command} without any errors!", output);
        Assert.Contains($"Successfully executed {testAction.Command} without any errors!", output);
        Assert.Contains($"Successfully executed {analyseAction.Command} without any errors!", output);
        Assert.Contains($"Successfully executed {deployAction.Command} without any errors!", output);
        Assert.Contains($"Successfully executed {utilityAction.Command} without any errors!", output);
        Assert.Equal(typeof(ExecutingState), releasePipeline.PreviousStatus!.GetType());
        Assert.Equal(typeof(FinishedState), releasePipeline.CurrentStatus.GetType());
    }

    [Fact]
    public void CreatePipelineWithoutAllActions_GivenTitleSprint_WhenNoPreConditions_ThenCreatePipelineWithoutAllActionsAndFailAfterExecuting()
    {
        // Arrange
        var title = "Pipeline 1";
        var productOwnerMock = new Mock<ProductOwner>("Piet", "piet@mail.com", "Password", new List<NotificationProvider> { NotificationProvider.MAIL });
        var developerMock = new Mock<Developer>("Klaas", "klaas@mail.com", "Password1", new List<NotificationProvider> { NotificationProvider.MAIL });
        var githubMock = new Mock<GitHub>();
        var projectMock = new Mock<Project>("Project 1", "Description 1", productOwnerMock.Object, githubMock.Object);
        var sprintMock = new Mock<SprintRelease>("Sprint 1", DateTime.Now, DateTime.Now, developerMock.Object, projectMock.Object);

        var mockTextWriter = new StringWriter();
        Console.SetOut(mockTextWriter);

        // Act
        var releasePipeline = new ReleasePipeline(title, sprintMock.Object);
        releasePipeline.ExecutePipeline();

        var output = mockTextWriter.ToString().Trim();

        // Assert
        Assert.NotNull(releasePipeline);
        Assert.IsAssignableFrom<Pipeline>(releasePipeline);
        Assert.IsType<ReleasePipeline>(releasePipeline);
        Assert.Equal(title, releasePipeline.Name);
        Assert.Equal(sprintMock.Object, releasePipeline.Sprint);
        Assert.Contains("Pipeline failed... need all required actions!", output);
        Assert.Equal(typeof(ExecutingState), releasePipeline.PreviousStatus!.GetType());
        Assert.Equal(typeof(FailedState), releasePipeline.CurrentStatus.GetType());
    }

    [Fact]
    public void CreatePipelineWithActionsButWithoutAnalyseAction_GivenTitleSprint_WhenNoPreConditions_ThenCreatePipelineWithActionsButWithoutAnalyseActionAndFailAfterExecuting()
    {
        // Arrange
        var title = "Pipeline 1";
        var productOwnerMock = new Mock<ProductOwner>("Piet", "piet@mail.com", "Password", new List<NotificationProvider> { NotificationProvider.MAIL });
        var developerMock = new Mock<Developer>("Klaas", "klaas@mail.com", "Password1", new List<NotificationProvider> { NotificationProvider.MAIL });
        var githubMock = new Mock<GitHub>();
        var projectMock = new Mock<Project>("Project 1", "Description 1", productOwnerMock.Object, githubMock.Object);
        var sprintMock = new Mock<SprintRelease>("Sprint 1", DateTime.Now, DateTime.Now, developerMock.Object, projectMock.Object);
        var sourceAction = new GitCloneAction();
        var packageAction = new NpmInstallAction();
        var buildAction = new NpmBuildAction();
        var deployAction = new NpmPublishAction();
        var testAction = new NpmTestAction();
        var utilityAction = new NpmRunCopyFilesAction();

        var mockTextWriter = new StringWriter();
        Console.SetOut(mockTextWriter);

        // Act
        var releasePipeline = new ReleasePipeline(title, sprintMock.Object);
        releasePipeline.AddAction(sourceAction);
        releasePipeline.AddAction(packageAction);
        releasePipeline.AddAction(buildAction);
        releasePipeline.AddAction(deployAction);
        releasePipeline.AddAction(testAction);
        releasePipeline.AddAction(utilityAction);
        releasePipeline.ExecutePipeline();

        var output = mockTextWriter.ToString().Trim();

        // Assert
        Assert.NotNull(releasePipeline);
        Assert.IsAssignableFrom<Pipeline>(releasePipeline);
        Assert.IsType<ReleasePipeline>(releasePipeline);
        Assert.Equal(title, releasePipeline.Name);
        Assert.Equal(sprintMock.Object, releasePipeline.Sprint);
        Assert.Contains($"Item {sourceAction.Id} has been added!", output);
        Assert.Contains($"Item {packageAction.Id} has been added!", output);
        Assert.Contains($"Item {buildAction.Id} has been added!", output);
        Assert.Contains($"Item {testAction.Id} has been added!", output);
        Assert.Contains($"Item {deployAction.Id} has been added!", output);
        Assert.Contains($"Item {utilityAction.Id} has been added!", output);
        Assert.Contains("Pipeline failed... need all required actions!", output);
        Assert.Equal(typeof(ExecutingState), releasePipeline.PreviousStatus!.GetType());
        Assert.Equal(typeof(FailedState), releasePipeline.CurrentStatus.GetType());
    }

    [Fact]
    public void CreatePipelineWithActionsButWithoutDeployAction_GivenTitleSprint_WhenNoPreConditions_ThenCreatePipelineWithActionsButWithoutDeployActionAndFailAfterExecuting()
    {
        // Arrange
        var title = "Pipeline 1";
        var productOwnerMock = new Mock<ProductOwner>("Piet", "piet@mail.com", "Password", new List<NotificationProvider> { NotificationProvider.MAIL });
        var developerMock = new Mock<Developer>("Klaas", "klaas@mail.com", "Password1", new List<NotificationProvider> { NotificationProvider.MAIL });
        var githubMock = new Mock<GitHub>();
        var projectMock = new Mock<Project>("Project 1", "Description 1", productOwnerMock.Object, githubMock.Object);
        var sprintMock = new Mock<SprintRelease>("Sprint 1", DateTime.Now, DateTime.Now, developerMock.Object, projectMock.Object);
        var sourceAction = new GitCloneAction();
        var packageAction = new NpmInstallAction();
        var buildAction = new NpmBuildAction();
        var analyseAction = new NpmEslintAction();
        var testAction = new NpmTestAction();
        var utilityAction = new NpmRunCopyFilesAction();

        var mockTextWriter = new StringWriter();
        Console.SetOut(mockTextWriter);

        // Act
        var releasePipeline = new ReleasePipeline(title, sprintMock.Object);
        releasePipeline.AddAction(sourceAction);
        releasePipeline.AddAction(packageAction);
        releasePipeline.AddAction(buildAction);
        releasePipeline.AddAction(analyseAction);
        releasePipeline.AddAction(testAction);
        releasePipeline.AddAction(utilityAction);
        releasePipeline.ExecutePipeline();

        var output = mockTextWriter.ToString().Trim();

        // Assert
        Assert.NotNull(releasePipeline);
        Assert.IsAssignableFrom<Pipeline>(releasePipeline);
        Assert.IsType<ReleasePipeline>(releasePipeline);
        Assert.Equal(title, releasePipeline.Name);
        Assert.Equal(sprintMock.Object, releasePipeline.Sprint);
        Assert.Contains($"Item {sourceAction.Id} has been added!", output);
        Assert.Contains($"Item {packageAction.Id} has been added!", output);
        Assert.Contains($"Item {buildAction.Id} has been added!", output);
        Assert.Contains($"Item {testAction.Id} has been added!", output);
        Assert.Contains($"Item {analyseAction.Id} has been added!", output);
        Assert.Contains($"Item {utilityAction.Id} has been added!", output);
        Assert.Contains("Pipeline failed... need all required actions!", output);
        Assert.Equal(typeof(ExecutingState), releasePipeline.PreviousStatus!.GetType());
        Assert.Equal(typeof(FailedState), releasePipeline.CurrentStatus.GetType());
    }

    [Fact]
    public void CreatePipelineWithActionsButWithoutUtilityAction_GivenTitleSprint_WhenNoPreConditions_ThenCreatePipelineWithActionsButWithoutUtilityActionFailAfterExecuting()
    {
        // Arrange
        var title = "Pipeline 1";
        var productOwnerMock = new Mock<ProductOwner>("Piet", "piet@mail.com", "Password", new List<NotificationProvider> { NotificationProvider.MAIL });
        var developerMock = new Mock<Developer>("Klaas", "klaas@mail.com", "Password1", new List<NotificationProvider> { NotificationProvider.MAIL });
        var githubMock = new Mock<GitHub>();
        var projectMock = new Mock<Project>("Project 1", "Description 1", productOwnerMock.Object, githubMock.Object);
        var sprintMock = new Mock<SprintRelease>("Sprint 1", DateTime.Now, DateTime.Now, developerMock.Object, projectMock.Object);
        var sourceAction = new GitCloneAction();
        var packageAction = new NpmInstallAction();
        var buildAction = new NpmBuildAction();
        var analyseAction = new NpmEslintAction();
        var deployAction = new NpmPublishAction();
        var testAction = new NpmTestAction();

        var mockTextWriter = new StringWriter();
        Console.SetOut(mockTextWriter);

        // Act
        var releasePipeline = new ReleasePipeline(title, sprintMock.Object);
        releasePipeline.AddAction(sourceAction);
        releasePipeline.AddAction(packageAction);
        releasePipeline.AddAction(buildAction);
        releasePipeline.AddAction(analyseAction);
        releasePipeline.AddAction(testAction);
        releasePipeline.AddAction(deployAction);
        releasePipeline.ExecutePipeline();

        var output = mockTextWriter.ToString().Trim();

        // Assert
        Assert.NotNull(releasePipeline);
        Assert.IsAssignableFrom<Pipeline>(releasePipeline);
        Assert.IsType<ReleasePipeline>(releasePipeline);
        Assert.Equal(title, releasePipeline.Name);
        Assert.Equal(sprintMock.Object, releasePipeline.Sprint);
        Assert.Contains($"Item {sourceAction.Id} has been added!", output);
        Assert.Contains($"Item {packageAction.Id} has been added!", output);
        Assert.Contains($"Item {buildAction.Id} has been added!", output);
        Assert.Contains($"Item {testAction.Id} has been added!", output);
        Assert.Contains($"Item {analyseAction.Id} has been added!", output);
        Assert.Contains($"Item {deployAction.Id} has been added!", output);
        Assert.Contains("Pipeline failed... need all required actions!", output);
        Assert.Equal(typeof(ExecutingState), releasePipeline.PreviousStatus!.GetType());
        Assert.Equal(typeof(FailedState), releasePipeline.CurrentStatus.GetType());
    }

    [Fact]
    public void CreatePipelineWithActionsButWithoutSourceAction_GivenTitleSprint_WhenNoPreConditions_ThenCreatePipelineWithActionsButWithoutSourceActionFailAfterExecuting()
    {
        // Arrange
        var title = "Pipeline 1";
        var productOwnerMock = new Mock<ProductOwner>("Piet", "piet@mail.com", "Password", new List<NotificationProvider> { NotificationProvider.MAIL });
        var developerMock = new Mock<Developer>("Klaas", "klaas@mail.com", "Password1", new List<NotificationProvider> { NotificationProvider.MAIL });
        var githubMock = new Mock<GitHub>();
        var projectMock = new Mock<Project>("Project 1", "Description 1", productOwnerMock.Object, githubMock.Object);
        var sprintMock = new Mock<SprintRelease>("Sprint 1", DateTime.Now, DateTime.Now, developerMock.Object, projectMock.Object);
        var utilityAction = new NpmRunCopyFilesAction();
        var packageAction = new NpmInstallAction();
        var buildAction = new NpmBuildAction();
        var analyseAction = new NpmEslintAction();
        var testAction = new NpmTestAction();
        utilityAction.ConnectToPhase();
        packageAction.ConnectToPhase();
        buildAction.ConnectToPhase();
        analyseAction.ConnectToPhase();
        testAction.ConnectToPhase();

        var mockTextWriter = new StringWriter();
        Console.SetOut(mockTextWriter);

        // Act
        var testPipeline = new TestPipeline(title, sprintMock.Object);
        testPipeline.AddAction(utilityAction);
        testPipeline.AddAction(packageAction);
        testPipeline.AddAction(buildAction);
        testPipeline.AddAction(analyseAction);
        testPipeline.AddAction(testAction);
        testPipeline.ExecutePipeline();

        var output = mockTextWriter.ToString().Trim();

        // Assert
        Assert.NotNull(testPipeline);
        Assert.IsAssignableFrom<Pipeline>(testPipeline);
        Assert.IsType<TestPipeline>(testPipeline);
        Assert.Equal(title, testPipeline.Name);
        Assert.Equal(sprintMock.Object, testPipeline.Sprint);
        Assert.Contains($"Item {utilityAction.Id} has been added!", output);
        Assert.Contains($"Item {packageAction.Id} has been added!", output);
        Assert.Contains($"Item {buildAction.Id} has been added!", output);
        Assert.Contains($"Item {testAction.Id} has been added!", output);
        Assert.Contains($"Item {analyseAction.Id} has been added!", output);
        Assert.Contains("Pipeline failed... need all required actions!", output);
        Assert.Equal(typeof(ExecutingState), testPipeline.PreviousStatus!.GetType());
        Assert.Equal(typeof(FailedState), testPipeline.CurrentStatus.GetType());
    }

    [Fact]
    public void CreatePipelineWithActionsButWithoutPackageAction_GivenTitleSprint_WhenNoPreConditions_ThenCreatePipelineWithActionsButWithoutPackageActionAndFailAfterExecuting()
    {
        // Arrange
        var title = "Pipeline 1";
        var productOwnerMock = new Mock<ProductOwner>("Piet", "piet@mail.com", "Password", new List<NotificationProvider> { NotificationProvider.MAIL });
        var developerMock = new Mock<Developer>("Klaas", "klaas@mail.com", "Password1", new List<NotificationProvider> { NotificationProvider.MAIL });
        var githubMock = new Mock<GitHub>();
        var projectMock = new Mock<Project>("Project 1", "Description 1", productOwnerMock.Object, githubMock.Object);
        var sprintMock = new Mock<SprintRelease>("Sprint 1", DateTime.Now, DateTime.Now, developerMock.Object, projectMock.Object);
        var utilityAction = new NpmRunCopyFilesAction();
        var sourceAction = new GitCloneAction();
        var buildAction = new NpmBuildAction();
        var analyseAction = new NpmEslintAction();
        var deployAction = new NpmPublishAction();
        var testAction = new NpmTestAction();

        var mockTextWriter = new StringWriter();
        Console.SetOut(mockTextWriter);

        // Act
        var releasePipeline = new ReleasePipeline(title, sprintMock.Object);
        releasePipeline.AddAction(utilityAction);
        releasePipeline.AddAction(sourceAction);
        releasePipeline.AddAction(buildAction);
        releasePipeline.AddAction(analyseAction);
        releasePipeline.AddAction(testAction);
        releasePipeline.AddAction(deployAction);
        releasePipeline.ExecutePipeline();

        var output = mockTextWriter.ToString().Trim();

        // Assert
        Assert.NotNull(releasePipeline);
        Assert.IsAssignableFrom<Pipeline>(releasePipeline);
        Assert.IsType<ReleasePipeline>(releasePipeline);
        Assert.Equal(title, releasePipeline.Name);
        Assert.Equal(sprintMock.Object, releasePipeline.Sprint);
        Assert.Contains($"Item {utilityAction.Id} has been added!", output);
        Assert.Contains($"Item {sourceAction.Id} has been added!", output);
        Assert.Contains($"Item {buildAction.Id} has been added!", output);
        Assert.Contains($"Item {testAction.Id} has been added!", output);
        Assert.Contains($"Item {analyseAction.Id} has been added!", output);
        Assert.Contains($"Item {deployAction.Id} has been added!", output);
        Assert.Contains("Pipeline failed... need all required actions!", output);
        Assert.Equal(typeof(ExecutingState), releasePipeline.PreviousStatus!.GetType());
        Assert.Equal(typeof(FailedState), releasePipeline.CurrentStatus.GetType());
    }

    [Fact]
    public void CreatePipelineWithActionsButWithoutBuildAction_GivenTitleSprint_WhenNoPreConditions_ThenCreatePipelineWithActionsButWithoutBuildActionAndFailAfterExecuting()
    {
        // Arrange
        var title = "Pipeline 1";
        var productOwnerMock = new Mock<ProductOwner>("Piet", "piet@mail.com", "Password", new List<NotificationProvider> { NotificationProvider.MAIL });
        var developerMock = new Mock<Developer>("Klaas", "klaas@mail.com", "Password1", new List<NotificationProvider> { NotificationProvider.MAIL });
        var githubMock = new Mock<GitHub>();
        var projectMock = new Mock<Project>("Project 1", "Description 1", productOwnerMock.Object, githubMock.Object);
        var sprintMock = new Mock<SprintRelease>("Sprint 1", DateTime.Now, DateTime.Now, developerMock.Object, projectMock.Object);
        var utilityAction = new NpmRunCopyFilesAction();
        var sourceAction = new GitCloneAction();
        var packageAction = new NpmInstallAction();
        var analyseAction = new NpmEslintAction();
        var deployAction = new NpmPublishAction();
        var testAction = new NpmTestAction();

        var mockTextWriter = new StringWriter();
        Console.SetOut(mockTextWriter);

        // Act
        var releasePipeline = new ReleasePipeline(title, sprintMock.Object);
        releasePipeline.AddAction(utilityAction);
        releasePipeline.AddAction(sourceAction);
        releasePipeline.AddAction(packageAction);
        releasePipeline.AddAction(analyseAction);
        releasePipeline.AddAction(testAction);
        releasePipeline.AddAction(deployAction);
        releasePipeline.ExecutePipeline();

        var output = mockTextWriter.ToString().Trim();

        // Assert
        Assert.NotNull(releasePipeline);
        Assert.IsAssignableFrom<Pipeline>(releasePipeline);
        Assert.IsType<ReleasePipeline>(releasePipeline);
        Assert.Equal(title, releasePipeline.Name);
        Assert.Equal(sprintMock.Object, releasePipeline.Sprint);
        Assert.Contains($"Item {utilityAction.Id} has been added!", output);
        Assert.Contains($"Item {sourceAction.Id} has been added!", output);
        Assert.Contains($"Item {packageAction.Id} has been added!", output);
        Assert.Contains($"Item {testAction.Id} has been added!", output);
        Assert.Contains($"Item {analyseAction.Id} has been added!", output);
        Assert.Contains($"Item {deployAction.Id} has been added!", output);
        Assert.Contains("Pipeline failed... need all required actions!", output);
        Assert.Equal(typeof(ExecutingState), releasePipeline.PreviousStatus!.GetType());
        Assert.Equal(typeof(FailedState), releasePipeline.CurrentStatus.GetType());
    }

    [Fact]
    public void CreatePipelineWithActions_GivenTitleSprint_WhenNoPreConditions_ThenCreatePipelineWithActionsButWithoutBuildActionAndFinalizeAfterFinalizing()
    {
        // Arrange
        var title = "Pipeline 1";
        var productOwnerMock = new Mock<ProductOwner>("Piet", "piet@mail.com", "Password", new List<NotificationProvider> { NotificationProvider.MAIL });
        var developerMock = new Mock<Developer>("Klaas", "klaas@mail.com", "Password1", new List<NotificationProvider> { NotificationProvider.MAIL });
        var githubMock = new Mock<GitHub>();
        var projectMock = new Mock<Project>("Project 1", "Description 1", productOwnerMock.Object, githubMock.Object);
        var sprintMock = new Mock<SprintRelease>("Sprint 1", DateTime.Now, DateTime.Now, developerMock.Object, projectMock.Object);
        var sourceAction = new GitCloneAction();
        var packageAction = new NpmInstallAction();
        var buildAction = new NpmBuildAction();
        var analyseAction = new NpmEslintAction();
        var deployAction = new NpmPublishAction();
        var testAction = new NpmTestAction();
        var utilityAction = new NpmRunCopyFilesAction();

        var mockTextWriter = new StringWriter();
        Console.SetOut(mockTextWriter);

        // Act
        var releasePipeline = new ReleasePipeline(title, sprintMock.Object);
        releasePipeline.AddAction(sourceAction);
        releasePipeline.AddAction(packageAction);
        releasePipeline.AddAction(buildAction);
        releasePipeline.AddAction(analyseAction);
        releasePipeline.AddAction(deployAction);
        releasePipeline.AddAction(testAction);
        releasePipeline.AddAction(utilityAction);
        releasePipeline.ExecutePipeline();

        var output = mockTextWriter.ToString().Trim();

        // Assert
        Assert.NotNull(releasePipeline);
        Assert.IsAssignableFrom<Pipeline>(releasePipeline);
        Assert.IsType<ReleasePipeline>(releasePipeline);
        Assert.Equal(title, releasePipeline.Name);
        Assert.Equal(sprintMock.Object, releasePipeline.Sprint);
        Assert.Contains($"Item {sourceAction.Id} has been added!", output);
        Assert.Contains($"Item {packageAction.Id} has been added!", output);
        Assert.Contains($"Item {buildAction.Id} has been added!", output);
        Assert.Contains($"Item {analyseAction.Id} has been added!", output);
        Assert.Contains($"Item {testAction.Id} has been added!", output);
        Assert.Contains($"Item {deployAction.Id} has been added!", output);
        Assert.Contains($"Item {utilityAction.Id} has been added!", output);
        Assert.Contains($"Successfully executed {sourceAction.Command} without any errors!", output);
        Assert.Contains($"Successfully executed {packageAction.Command} without any errors!", output);
        Assert.Contains($"Successfully executed {buildAction.Command} without any errors!", output);
        Assert.Contains($"Successfully executed {testAction.Command} without any errors!", output);
        Assert.Contains($"Successfully executed {analyseAction.Command} without any errors!", output);
        Assert.Contains($"Successfully executed {deployAction.Command} without any errors!", output);
        Assert.Contains($"Successfully executed {utilityAction.Command} without any errors!", output);
        Assert.Equal(typeof(ExecutingState), releasePipeline.PreviousStatus!.GetType());
        Assert.Equal(typeof(FinishedState), releasePipeline.CurrentStatus.GetType());
    }

    [Fact]
    public void CreatePipelineWithActions_GivenTitleSprint_WhenNoPreConditions_ThenCreatePipelineWithActionsButWithoutBuildActionAndFailAfterCallingFail()
    {
        // Arrange
        var title = "Pipeline 1";
        var productOwnerMock = new Mock<ProductOwner>("Piet", "piet@mail.com", "Password", new List<NotificationProvider> { NotificationProvider.MAIL });
        var developerMock = new Mock<Developer>("Klaas", "klaas@mail.com", "Password1", new List<NotificationProvider> { NotificationProvider.MAIL });
        var githubMock = new Mock<GitHub>();
        var projectMock = new Mock<Project>("Project 1", "Description 1", productOwnerMock.Object, githubMock.Object);
        var sprintMock = new Mock<SprintRelease>("Sprint 1", DateTime.Now, DateTime.Now, developerMock.Object, projectMock.Object);
        var sourceAction = new GitCloneAction();
        var packageAction = new NpmInstallAction();
        var buildAction = new NpmBuildAction();
        var analyseAction = new NpmEslintAction();
        var deployAction = new NpmPublishAction();
        var testAction = new NpmTestAction();

        var mockTextWriter = new StringWriter();
        Console.SetOut(mockTextWriter);

        // Act
        var releasePipeline = new ReleasePipeline(title, sprintMock.Object);
        releasePipeline.AddAction(sourceAction);
        releasePipeline.AddAction(packageAction);
        releasePipeline.AddAction(buildAction);
        releasePipeline.AddAction(analyseAction);
        releasePipeline.AddAction(deployAction);
        releasePipeline.AddAction(testAction);
        releasePipeline.ExecutePipeline();

        var output = mockTextWriter.ToString().Trim();

        // Assert
        Assert.NotNull(releasePipeline);
        Assert.IsAssignableFrom<Pipeline>(releasePipeline);
        Assert.IsType<ReleasePipeline>(releasePipeline);
        Assert.Equal(title, releasePipeline.Name);
        Assert.Equal(sprintMock.Object, releasePipeline.Sprint);
        Assert.Contains($"Item {sourceAction.Id} has been added!", output);
        Assert.Contains($"Item {packageAction.Id} has been added!", output);
        Assert.Contains($"Item {buildAction.Id} has been added!", output);
        Assert.Contains($"Item {analyseAction.Id} has been added!", output);
        Assert.Contains($"Item {testAction.Id} has been added!", output);
        Assert.Contains($"Item {deployAction.Id} has been added!", output);
        Assert.Contains("Pipeline failed... need all required actions!", output);
        Assert.Equal(typeof(ExecutingState), releasePipeline.PreviousStatus!.GetType());
        Assert.Equal(typeof(FailedState), releasePipeline.CurrentStatus.GetType());
    }

    [Fact]
    public void CreatePipelineWithActions_GivenTitleSprint_WhenNoPreConditions_ThenCreatePipelineWithActionsButWithoutBuildActionAndReexecuteAfterFailing()
    {
        // Arrange
        var title = "Pipeline 1";
        var productOwnerMock = new Mock<ProductOwner>("Piet", "piet@mail.com", "Password", new List<NotificationProvider> { NotificationProvider.MAIL });
        var developerMock = new Mock<Developer>("Klaas", "klaas@mail.com", "Password1", new List<NotificationProvider> { NotificationProvider.MAIL });
        var githubMock = new Mock<GitHub>();
        var projectMock = new Mock<Project>("Project 1", "Description 1", productOwnerMock.Object, githubMock.Object);
        var sprintMock = new Mock<SprintRelease>("Sprint 1", DateTime.Now, DateTime.Now, developerMock.Object, projectMock.Object);
        var sourceAction = new GitCloneAction();
        var packageAction = new NpmInstallAction();
        var buildAction = new NpmBuildAction();
        var analyseAction = new NpmEslintAction();
        var deployAction = new NpmPublishAction();
        var testAction = new NpmTestAction();

        var mockTextWriter = new StringWriter();
        Console.SetOut(mockTextWriter);

        // Act
        var releasePipeline = new ReleasePipeline(title, sprintMock.Object);
        releasePipeline.AddAction(sourceAction);
        releasePipeline.AddAction(packageAction);
        releasePipeline.AddAction(buildAction);
        releasePipeline.AddAction(analyseAction);
        releasePipeline.AddAction(deployAction);
        releasePipeline.AddAction(testAction);
        releasePipeline.ExecutePipeline();

        var output = mockTextWriter.ToString().Trim();

        // Assert
        Assert.NotNull(releasePipeline);
        Assert.IsAssignableFrom<Pipeline>(releasePipeline);
        Assert.IsType<ReleasePipeline>(releasePipeline);
        Assert.Equal(title, releasePipeline.Name);
        Assert.Equal(sprintMock.Object, releasePipeline.Sprint);
        Assert.Contains($"Item {sourceAction.Id} has been added!", output);
        Assert.Contains($"Item {packageAction.Id} has been added!", output);
        Assert.Contains($"Item {buildAction.Id} has been added!", output);
        Assert.Contains($"Item {analyseAction.Id} has been added!", output);
        Assert.Contains($"Item {testAction.Id} has been added!", output);
        Assert.Contains($"Item {deployAction.Id} has been added!", output);
        Assert.Contains("Pipeline failed... need all required actions!", output);
        Assert.Equal(typeof(ExecutingState), releasePipeline.PreviousStatus!.GetType());
        Assert.Equal(typeof(FailedState), releasePipeline.CurrentStatus.GetType());
    }

    [Fact]
    public void CreatePipelineWithActions_GivenTitleSprint_WhenNoPreConditions_ThenCreatePipelineWithActionsButWithoutBuildActionAndReexecuteAfterCancel()
    {
        // Arrange
        var title = "Pipeline 1";
        var productOwnerMock = new Mock<ProductOwner>("Piet", "piet@mail.com", "Password", new List<NotificationProvider> { NotificationProvider.MAIL });
        var developerMock = new Mock<Developer>("Klaas", "klaas@mail.com", "Password1", new List<NotificationProvider> { NotificationProvider.MAIL });
        var githubMock = new Mock<GitHub>();
        var projectMock = new Mock<Project>("Project 1", "Description 1", productOwnerMock.Object, githubMock.Object);
        var sprintMock = new Mock<SprintRelease>("Sprint 1", DateTime.Now, DateTime.Now, developerMock.Object, projectMock.Object);
        var sourceAction = new GitCloneAction();
        var packageAction = new NpmInstallAction();
        var buildAction = new NpmBuildAction();
        var analyseAction = new NpmEslintAction();
        var deployAction = new NpmPublishAction();
        var testAction = new NpmTestAction();
        var utilityAction = new NpmRunCopyFilesAction();

        var mockTextWriter = new StringWriter();
        Console.SetOut(mockTextWriter);

        // Act
        var releasePipeline = new ReleasePipeline(title, sprintMock.Object);
        releasePipeline.AddAction(sourceAction);
        releasePipeline.AddAction(packageAction);
        releasePipeline.AddAction(buildAction);
        releasePipeline.AddAction(analyseAction);
        releasePipeline.AddAction(deployAction);
        releasePipeline.AddAction(utilityAction);
        releasePipeline.AddAction(testAction);
        releasePipeline.CurrentStatus = new ExecutingState(releasePipeline);
        releasePipeline.CancelPipeline();
        releasePipeline.ExecutePipeline();

        var output = mockTextWriter.ToString().Trim();

        // Assert
        Assert.NotNull(releasePipeline);
        Assert.IsAssignableFrom<Pipeline>(releasePipeline);
        Assert.IsType<ReleasePipeline>(releasePipeline);
        Assert.Equal(title, releasePipeline.Name);
        Assert.Equal(sprintMock.Object, releasePipeline.Sprint);
        Assert.Contains($"Item {sourceAction.Id} has been added!", output);
        Assert.Contains($"Item {packageAction.Id} has been added!", output);
        Assert.Contains($"Item {buildAction.Id} has been added!", output);
        Assert.Contains($"Item {analyseAction.Id} has been added!", output);
        Assert.Contains($"Item {testAction.Id} has been added!", output);
        Assert.Contains($"Item {deployAction.Id} has been added!", output);
        Assert.Contains($"Successfully executed {sourceAction.Command} without any errors!", output);
        Assert.Contains($"Successfully executed {packageAction.Command} without any errors!", output);
        Assert.Contains($"Successfully executed {buildAction.Command} without any errors!", output);
        Assert.Contains($"Successfully executed {testAction.Command} without any errors!", output);
        Assert.Contains($"Successfully executed {analyseAction.Command} without any errors!", output);
        Assert.Contains($"Successfully executed {deployAction.Command} without any errors!", output);
        Assert.Contains($"Successfully executed {utilityAction.Command} without any errors!", output);
        Assert.Equal(typeof(ExecutingState), releasePipeline.PreviousStatus!.GetType());
        Assert.Equal(typeof(FinishedState), releasePipeline.CurrentStatus.GetType());
    }

    [Fact]
    public void CreatePipelineWithActions_GivenTitleSprint_WhenNoPreConditions_ThenCreatePipelineWithActionsButWithoutBuildActionAndCancelAfterFailing()
    {
        // Arrange
        var title = "Pipeline 1";
        var productOwnerMock = new Mock<ProductOwner>("Piet", "piet@mail.com", "Password", new List<NotificationProvider> { NotificationProvider.MAIL });
        var developerMock = new Mock<Developer>("Klaas", "klaas@mail.com", "Password1", new List<NotificationProvider> { NotificationProvider.MAIL });
        var githubMock = new Mock<GitHub>();
        var projectMock = new Mock<Project>("Project 1", "Description 1", productOwnerMock.Object, githubMock.Object);
        var sprintMock = new Mock<SprintRelease>("Sprint 1", DateTime.Now, DateTime.Now, developerMock.Object, projectMock.Object);
        var sourceAction = new GitCloneAction();
        var packageAction = new NpmInstallAction();
        var buildAction = new NpmBuildAction();
        var analyseAction = new NpmEslintAction();
        var deployAction = new NpmPublishAction();
        var testAction = new NpmTestAction();

        var mockTextWriter = new StringWriter();
        Console.SetOut(mockTextWriter);

        // Act
        var releasePipeline = new ReleasePipeline(title, sprintMock.Object);
        releasePipeline.AddAction(sourceAction);
        releasePipeline.AddAction(packageAction);
        releasePipeline.AddAction(buildAction);
        releasePipeline.AddAction(analyseAction);
        releasePipeline.AddAction(deployAction);
        releasePipeline.AddAction(testAction);
        releasePipeline.ExecutePipeline();
        releasePipeline.CancelPipeline();

        var output = mockTextWriter.ToString().Trim();

        // Assert
        Assert.NotNull(releasePipeline);
        Assert.IsAssignableFrom<Pipeline>(releasePipeline);
        Assert.IsType<ReleasePipeline>(releasePipeline);
        Assert.Equal(title, releasePipeline.Name);
        Assert.Equal(sprintMock.Object, releasePipeline.Sprint);
        Assert.Contains($"Item {sourceAction.Id} has been added!", output);
        Assert.Contains($"Item {packageAction.Id} has been added!", output);
        Assert.Contains($"Item {buildAction.Id} has been added!", output);
        Assert.Contains($"Item {analyseAction.Id} has been added!", output);
        Assert.Contains($"Item {testAction.Id} has been added!", output);
        Assert.Contains($"Item {deployAction.Id} has been added!", output);
        Assert.Contains("Pipeline failed... need all required actions!", output);
        Assert.Equal(typeof(FailedState), releasePipeline.PreviousStatus!.GetType());
        Assert.Equal(typeof(CancelledState), releasePipeline.CurrentStatus.GetType());
    }

    [Fact]
    public void CreatePipelineWithActionsButWithoutTestAction_GivenTitleSprint_WhenNoPreConditions_ThenCreatePipelineWithActionsButWithoutTestActionAndFailAfterExecuting()
    {
        // Arrange
        var title = "Pipeline 1";
        var productOwnerMock = new Mock<ProductOwner>("Piet", "piet@mail.com", "Password", new List<NotificationProvider> { NotificationProvider.MAIL });
        var developerMock = new Mock<Developer>("Klaas", "klaas@mail.com", "Password1", new List<NotificationProvider> { NotificationProvider.MAIL });
        var githubMock = new Mock<GitHub>();
        var projectMock = new Mock<Project>("Project 1", "Description 1", productOwnerMock.Object, githubMock.Object);
        var sprintMock = new Mock<SprintRelease>("Sprint 1", DateTime.Now, DateTime.Now, developerMock.Object, projectMock.Object);
        var sourceAction = new GitCloneAction();
        var packageAction = new NpmInstallAction();
        var buildAction = new NpmBuildAction();
        var analyseAction = new NpmEslintAction();
        var deployAction = new NpmPublishAction();
        var utilityAction = new NpmRunCopyFilesAction();

        var mockTextWriter = new StringWriter();
        Console.SetOut(mockTextWriter);

        // Act
        var releasePipeline = new ReleasePipeline(title, sprintMock.Object);
        releasePipeline.AddAction(sourceAction);
        releasePipeline.AddAction(packageAction);
        releasePipeline.AddAction(buildAction);
        releasePipeline.AddAction(analyseAction);
        releasePipeline.AddAction(deployAction);
        releasePipeline.AddAction(utilityAction);
        releasePipeline.ExecutePipeline();

        var output = mockTextWriter.ToString().Trim();

        // Assert
        Assert.NotNull(releasePipeline);
        Assert.IsAssignableFrom<Pipeline>(releasePipeline);
        Assert.IsType<ReleasePipeline>(releasePipeline);
        Assert.Equal(title, releasePipeline.Name);
        Assert.Equal(sprintMock.Object, releasePipeline.Sprint);
        Assert.Contains($"Item {sourceAction.Id} has been added!", output);
        Assert.Contains($"Item {packageAction.Id} has been added!", output);
        Assert.Contains($"Item {buildAction.Id} has been added!", output);
        Assert.Contains($"Item {deployAction.Id} has been added!", output);
        Assert.Contains($"Item {analyseAction.Id} has been added!", output);
        Assert.Contains($"Item {utilityAction.Id} has been added!", output);
        Assert.Contains("Pipeline failed... need all required actions!", output);
        Assert.Equal(typeof(ExecutingState), releasePipeline.PreviousStatus!.GetType());
        Assert.Equal(typeof(FailedState), releasePipeline.CurrentStatus.GetType());
    }

    [Fact]
    public void CreatePipelineWithActionsButWithoutTestAction_GivenTitleSprint_WhenNoPreConditions_ThenCreatePipelineWithActionsButWithoutTestActionAndFailAfterExecutingAndSendNotificationToScrumMaster()
    {
        // Arrange
        var title = "Pipeline 1";
        var productOwnerMock = new Mock<ProductOwner>("Piet", "piet@mail.com", "Password", new List<NotificationProvider> { NotificationProvider.MAIL });
        var developerMock = new Mock<Developer>("Klaas", "klaas@mail.com", "Password1", new List<NotificationProvider> { NotificationProvider.MAIL, NotificationProvider.SLACK });
        var githubMock = new Mock<GitHub>();
        var projectMock = new Mock<Project>("Project 1", "Description 1", productOwnerMock.Object, githubMock.Object);
        var sprintMock = new Mock<SprintRelease>("Sprint 1", DateTime.Now, DateTime.Now, developerMock.Object, projectMock.Object);
        var sourceAction = new GitCloneAction();
        var packageAction = new NpmInstallAction();
        var buildAction = new NpmBuildAction();
        var analyseAction = new NpmEslintAction();
        var deployAction = new NpmPublishAction();
        var utilityAction = new NpmRunCopyFilesAction();

        var mockTextWriter = new StringWriter();
        Console.SetOut(mockTextWriter);

        // Act
        var releasePipeline = new ReleasePipeline(title, sprintMock.Object);
        releasePipeline.AddAction(sourceAction);
        releasePipeline.AddAction(packageAction);
        releasePipeline.AddAction(buildAction);
        releasePipeline.AddAction(analyseAction);
        releasePipeline.AddAction(deployAction);
        releasePipeline.AddAction(utilityAction);
        releasePipeline.ExecutePipeline();

        var output = mockTextWriter.ToString().Trim();

        // Assert
        Assert.NotNull(releasePipeline);
        Assert.IsAssignableFrom<Pipeline>(releasePipeline);
        Assert.IsType<ReleasePipeline>(releasePipeline);
        Assert.Equal(title, releasePipeline.Name);
        Assert.Equal(sprintMock.Object, releasePipeline.Sprint);
        Assert.Contains($"Item {sourceAction.Id} has been added!", output);
        Assert.Contains($"Item {packageAction.Id} has been added!", output);
        Assert.Contains($"Item {buildAction.Id} has been added!", output);
        Assert.Contains($"Item {deployAction.Id} has been added!", output);
        Assert.Contains($"Item {analyseAction.Id} has been added!", output);
        Assert.Contains($"Item {utilityAction.Id} has been added!", output);
        Assert.Contains("Pipeline failed... need all required actions!", output);
        Assert.Equal(typeof(ExecutingState), releasePipeline.PreviousStatus!.GetType());
        Assert.Equal(typeof(FailedState), releasePipeline.CurrentStatus.GetType());
        Assert.Contains("Send mail to klaas@mail.com with title (Pipeline release failed)", output);
        Assert.Contains("Send slack to", output);
        Assert.Contains("All notifications sent successfully!", output);
    }

    [Fact]
    public void CreatePipelineWithActions_GivenTitleSprint_WhenNoPreConditions_ThenCreatePipelineWithActionsAndSendNotificationToScrumMasterAndProductOwner()
    {
        // Arrange
        var title = "Pipeline 1";
        var productOwnerMock = new Mock<ProductOwner>("Piet", "piet@mail.com", "Password", new List<NotificationProvider> { NotificationProvider.MAIL, NotificationProvider.TEAMS });
        var developerMock = new Mock<Developer>("Klaas", "klaas@mail.com", "Password1", new List<NotificationProvider> { NotificationProvider.MAIL, NotificationProvider.SLACK });
        var githubMock = new Mock<GitHub>();
        var projectMock = new Mock<Project>("Project 1", "Description 1", productOwnerMock.Object, githubMock.Object);
        var sprintMock = new Mock<SprintRelease>("Sprint 1", DateTime.Now, DateTime.Now, developerMock.Object, projectMock.Object);
        var sourceAction = new GitCloneAction();
        var packageAction = new NpmInstallAction();
        var buildAction = new NpmBuildAction();
        var testAction = new NpmTestAction();
        var analyseAction = new NpmEslintAction();
        var deployAction = new NpmPublishAction();
        var utilityAction = new NpmRunCopyFilesAction();

        var mockTextWriter = new StringWriter();
        Console.SetOut(mockTextWriter);

        // Act
        var releasePipeline = new ReleasePipeline(title, sprintMock.Object);
        releasePipeline.AddAction(sourceAction);
        releasePipeline.AddAction(packageAction);
        releasePipeline.AddAction(buildAction);
        releasePipeline.AddAction(testAction);
        releasePipeline.AddAction(analyseAction);
        releasePipeline.AddAction(deployAction);
        releasePipeline.AddAction(utilityAction);
        releasePipeline.ExecutePipeline();

        var output = mockTextWriter.ToString().Trim();

        // Assert
        Assert.NotNull(releasePipeline);
        Assert.IsAssignableFrom<Pipeline>(releasePipeline);
        Assert.IsType<ReleasePipeline>(releasePipeline);
        Assert.Equal(title, releasePipeline.Name);
        Assert.Equal(sprintMock.Object, releasePipeline.Sprint);
        Assert.Contains($"Item {sourceAction.Id} has been added!", output);
        Assert.Contains($"Item {packageAction.Id} has been added!", output);
        Assert.Contains($"Item {buildAction.Id} has been added!", output);
        Assert.Contains($"Item {testAction.Id} has been added!", output);
        Assert.Contains($"Item {analyseAction.Id} has been added!", output);
        Assert.Contains($"Item {deployAction.Id} has been added!", output);
        Assert.Contains($"Item {utilityAction.Id} has been added!", output);
        Assert.Contains($"Successfully executed {sourceAction.Command} without any errors!", output);
        Assert.Contains($"Successfully executed {packageAction.Command} without any errors!", output);
        Assert.Contains($"Successfully executed {buildAction.Command} without any errors!", output);
        Assert.Contains($"Successfully executed {testAction.Command} without any errors!", output);
        Assert.Contains($"Successfully executed {analyseAction.Command} without any errors!", output);
        Assert.Contains($"Successfully executed {deployAction.Command} without any errors!", output);
        Assert.Contains($"Successfully executed {utilityAction.Command} without any errors!", output);
        Assert.Equal(typeof(ExecutingState), releasePipeline.PreviousStatus!.GetType());
        Assert.Equal(typeof(FinishedState), releasePipeline.CurrentStatus.GetType());
        Assert.Contains("Send mail to piet@mail.com with title (Pipeline release succeeded)", output);
        Assert.Contains("Send mail to klaas@mail.com with title (Pipeline release succeeded)", output);
        Assert.Contains("Send Teams message", output);
        Assert.Contains("Send slack to", output);
        Assert.Contains("All notifications sent successfully!", output);
    }

    [Fact]
    public void CreatePipelineWithActions_GivenTitleSprint_WhenNoPreConditions_ThenCreatePipelineWithActionsAndCancelAndSendNotificationToScrumMaster()
    {
        // Arrange
        var title = "Pipeline 1";
        var productOwnerMock = new Mock<ProductOwner>("Piet", "piet@mail.com", "Password", new List<NotificationProvider> { NotificationProvider.MAIL, NotificationProvider.TEAMS });
        var developerMock = new Mock<Developer>("Klaas", "klaas@mail.com", "Password1", new List<NotificationProvider> { NotificationProvider.MAIL, NotificationProvider.SLACK });
        var githubMock = new Mock<GitHub>();
        var projectMock = new Mock<Project>("Project 1", "Description 1", productOwnerMock.Object, githubMock.Object);
        var sprintMock = new Mock<SprintRelease>("Sprint 1", DateTime.Now, DateTime.Now, developerMock.Object, projectMock.Object);
        var sourceAction = new GitCloneAction();
        var packageAction = new NpmInstallAction();
        var buildAction = new NpmBuildAction();
        var testAction = new NpmTestAction();
        var analyseAction = new NpmEslintAction();
        var deployAction = new NpmPublishAction();
        var utilityAction = new NpmRunCopyFilesAction();

        var mockTextWriter = new StringWriter();
        Console.SetOut(mockTextWriter);

        // Act
        var releasePipeline = new ReleasePipeline(title, sprintMock.Object);
        releasePipeline.AddAction(sourceAction);
        releasePipeline.AddAction(packageAction);
        releasePipeline.AddAction(buildAction);
        releasePipeline.AddAction(testAction);
        releasePipeline.AddAction(analyseAction);
        releasePipeline.AddAction(deployAction);
        releasePipeline.AddAction(utilityAction);
        releasePipeline.CurrentStatus = new ExecutingState(releasePipeline);
        releasePipeline.CancelPipeline();

        var output = mockTextWriter.ToString().Trim();

        // Assert
        Assert.NotNull(releasePipeline);
        Assert.IsAssignableFrom<Pipeline>(releasePipeline);
        Assert.IsType<ReleasePipeline>(releasePipeline);
        Assert.Equal(title, releasePipeline.Name);
        Assert.Equal(sprintMock.Object, releasePipeline.Sprint);
        Assert.Contains($"Item {sourceAction.Id} has been added!", output);
        Assert.Contains($"Item {packageAction.Id} has been added!", output);
        Assert.Contains($"Item {buildAction.Id} has been added!", output);
        Assert.Contains($"Item {testAction.Id} has been added!", output);
        Assert.Contains($"Item {analyseAction.Id} has been added!", output);
        Assert.Contains($"Item {deployAction.Id} has been added!", output);
        Assert.Contains($"Item {utilityAction.Id} has been added!", output);
        Assert.Equal(typeof(ExecutingState), releasePipeline.PreviousStatus!.GetType());
        Assert.Equal(typeof(CancelledState), releasePipeline.CurrentStatus.GetType());
        Assert.Contains("Send mail to klaas@mail.com with title (Pipeline release cancelled)", output);
        Assert.Contains("Send slack to", output);
        Assert.Contains("All notifications sent successfully!", output);
    }
}
