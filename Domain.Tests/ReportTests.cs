namespace Domain.Tests;

public class ReportTests
{
    [Fact]
    public void CreateReport_GivenTitleSprintExtensionHeaderFooter_WhenNoPreConditions_ThenCreateUser()
    {
        // Arrange
        ISprintFactory<SprintReview> factory = new SprintReviewFactory();
            
        List<NotificationProvider> notificationProviders = new();
        notificationProviders.Add(NotificationProvider.MAIL);
        Developer scrumMaster = new("name", "email", "password", notificationProviders);
            
        ProductOwner productOwner = new("name", "email", "password", notificationProviders);
        Project project = new("Project", "Description", productOwner, new GitHub());

        // Act
        SprintReview sprint = factory.CreateSprint("Initial Sprint", DateTime.Now, DateTime.Now.AddDays(14), scrumMaster, project);
        
        ReportElement header = new("Company", new byte[10], "Avans", 1, DateTime.Now, ReportElementType.HEADER );
        ReportElement footer = new("Company", new byte[10], "Avans", 1, DateTime.Now, ReportElementType.FOOTER );
         
        //Act
        Report report = new("Report", sprint, ReportExtension.PDF, header, header);

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
        ISprintFactory<SprintReview> factory = new SprintReviewFactory();
            
        List<NotificationProvider> notificationProviders = new();
        notificationProviders.Add(NotificationProvider.MAIL);
        Developer scrumMaster = new("name", "email", "password", notificationProviders);
            
        ProductOwner productOwner = new("name", "email", "password", notificationProviders);
        Project project = new("Project", "Description", productOwner, new GitHub());

        // Act
        SprintReview sprint = factory.CreateSprint("Initial Sprint", DateTime.Now, DateTime.Now.AddDays(14), scrumMaster, project);
        
        ReportElement header = new("Company", new byte[10], "Avans", 1, DateTime.Now, ReportElementType.HEADER );
        ReportElement footer = new("Company", new byte[10], "Avans", 1, DateTime.Now, ReportElementType.FOOTER );
         
        //Act
        Report report = new("Report", sprint, ReportExtension.PDF);
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
        ISprintFactory<SprintReview> factory = new SprintReviewFactory();
            
        List<NotificationProvider> notificationProviders = new();
        notificationProviders.Add(NotificationProvider.MAIL);
        Developer scrumMaster = new("name", "email", "password", notificationProviders);
            
        ProductOwner productOwner = new("name", "email", "password", notificationProviders);
        Project project = new("Project", "Description", productOwner, new GitHub());

        // Act
        SprintReview sprint = factory.CreateSprint("Initial Sprint", DateTime.Now, DateTime.Now.AddDays(14), scrumMaster, project);
        
        ReportElement header = new("Company", new byte[10], "Avans", 1, DateTime.Now, ReportElementType.HEADER );
        ReportElement footer = new("Company", new byte[10], "Avans", 1, DateTime.Now, ReportElementType.FOOTER );
         
        //Act
        Report report = new("Report", sprint, ReportExtension.PDF);
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