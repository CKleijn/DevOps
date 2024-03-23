namespace Domain.Tests;

[Collection("SequentialTest")]
public class ReportTests
{
    [Fact]
    public void CreateReport_GivenTitleSprintExtensionHeaderFooter_WhenNoPreConditions_ThenCreateUser()
    {
        // Arrange
        var mockFactory = new Mock<ISprintFactory<SprintReview>>();
        var mockDeveloper = new Mock<Developer>(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<List<NotificationProvider>>());
        var mockProductOwner = new Mock<ProductOwner>(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<List<NotificationProvider>>());
        var mockProject = new Mock<Project>(It.IsAny<string>(), It.IsAny<string>(), mockProductOwner.Object, new GitHub());

        var mockSprint = new Mock<SprintReview>(It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<DateTime>(), mockDeveloper.Object, mockProject.Object);
        mockFactory.Setup(f => f.CreateSprint(It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<Developer>(), It.IsAny<Project>())).Returns(mockSprint.Object);

        ReportElement header = new("Company", new byte[10], "Avans", 1, DateTime.Now, ReportElementType.HEADER);
        ReportElement footer = new("Company", new byte[10], "Avans", 1, DateTime.Now, ReportElementType.FOOTER);

        //Act
        Report report = new("Report", mockSprint.Object, ReportExtension.PDF, header, header);

        // Assert
        Assert.IsType<Report>(report);
        Assert.NotNull(report);
        Assert.NotNull(report.Header);
        Assert.NotNull(report.Footer);
    }

    [Fact]
    public void CreateReport_GivenTitleSprintExtensionHeaderFooter_WhenNoExtensions_ThenCreateUser()
    {
        // Arrange
        var mockFactory = new Mock<ISprintFactory<SprintReview>>();
        var mockDeveloper = new Mock<Developer>(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<List<NotificationProvider>>());
        var mockProductOwner = new Mock<ProductOwner>(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<List<NotificationProvider>>());
        var mockProject = new Mock<Project>(It.IsAny<string>(), It.IsAny<string>(), mockProductOwner.Object, new GitHub());

        var mockSprint = new Mock<SprintReview>(It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<DateTime>(), mockDeveloper.Object, mockProject.Object);
        mockFactory.Setup(f => f.CreateSprint(It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<Developer>(), It.IsAny<Project>())).Returns(mockSprint.Object);

        ReportElement header = new("Company", new byte[10], "Avans", 1, DateTime.Now, ReportElementType.HEADER);
        ReportElement footer = new("Company", new byte[10], "Avans", 1, DateTime.Now, ReportElementType.FOOTER);

        //Act
        Report report = new("Report", mockSprint.Object, ReportExtension.PDF);
        report.Header = header;
        report.Footer = footer;

        // Assert
        Assert.IsType<Report>(report);
        Assert.NotNull(report);
    }

    [Fact]
    public void GenerateReport_GivenTitleSprintExtensionHeaderFooter_WhenReportCreated_ThenGenerateReport()
    {
        // Arrange
        var mockFactory = new Mock<ISprintFactory<SprintReview>>();
        var mockDeveloper = new Mock<Developer>(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<List<NotificationProvider>>());
        var mockProductOwner = new Mock<ProductOwner>(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<List<NotificationProvider>>());
        var mockProject = new Mock<Project>(It.IsAny<string>(), It.IsAny<string>(), mockProductOwner.Object, new GitHub());

        var mockSprint = new Mock<SprintReview>(It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<DateTime>(), mockDeveloper.Object, mockProject.Object);
        mockFactory.Setup(f => f.CreateSprint(It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<Developer>(), It.IsAny<Project>())).Returns(mockSprint.Object);

        ReportElement header = new("Company", new byte[10], "Avans", 1, DateTime.Now, ReportElementType.HEADER);
        ReportElement footer = new("Company", new byte[10], "Avans", 1, DateTime.Now, ReportElementType.FOOTER);

        //Act
        Report report = new("Report", mockSprint.Object, ReportExtension.PDF);
        report.Header = header;
        report.Footer = footer;

        string result = report.GenerateReport();

        // Assert
        Assert.IsType<Report>(report);
        Assert.NotNull(report);
        Assert.NotNull(result);
        Assert.IsType<string>(result);
    }
}