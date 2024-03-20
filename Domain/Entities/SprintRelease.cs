﻿using System.Text;
using Domain.Helpers;
using Domain.States.Sprint;

namespace Domain.Entities;

public class SprintRelease : Sprint
{
    private IList<Release> _releases { get; init; }
    public IList<Release> Releases { get => _releases; init => _releases = value; }
    
    public SprintRelease(string title, DateTime startDate, DateTime endDate, Developer scrumMaster, Project project) : base(title, startDate, endDate, scrumMaster, project)
    {
        _releases = new List<Release>();
        Logger.DisplayCreatedAlert(nameof(SprintRelease), Title);
    }

    public void AddRelease(Release release)
    {
        _releases.Add(release);
        Logger.DisplayUpdatedAlert(nameof(Releases), $"Added release with an id of: {release.Id}");
    }
    
    public void RemoveRelease(Release release)
    {
        _releases.Remove(release);
        Logger.DisplayUpdatedAlert(nameof(Releases), $"Removed release with an id of: {release.Id}");
    }

    protected override bool ValidateChange()
    {
        //Don't allow mutation whenever state differs from the initial state
        if (CurrentStatus.GetType() != typeof(InitialState))
        {
            Logger.DisplayCustomAlert(nameof(Sprint), nameof(ValidateChange), $"Can't update sprint in current state ({CurrentStatus.GetType()}).");
            return false;
        }
        
        //Don't allow mutation whenever pipeline's state differs from the initial state
        if (Pipeline?.CurrentStatus.GetType() != typeof(States.Pipeline.InitialState))
        {
            Logger.DisplayCustomAlert(nameof(Sprint), nameof(ValidateChange), $"Can't update sprint when it's corresponding pipeline isn't in its initial state ({Pipeline?.CurrentStatus.GetType()}).");
            
            return false;
        }
        
        //Perform actions alteration is done on a sprint that has already ended
        if (EndDate < DateTime.Now)
        {
            //Set sprint to finished state if it isn't already
            if(CurrentStatus.GetType() != typeof(FinishedState))
                CurrentStatus = new FinishedState(this);
            
            Logger.DisplayCustomAlert(nameof(SprintRelease), nameof(ValidateChange), $"Can't update sprint after end date. Sprint will be set to close if it isn't already.");

            //Check if sprint input is valid
            CurrentStatus.ReleaseSprint();
            
            //Release sprint 
            if (CurrentStatus.GetType() == typeof(ReleaseState))
            {
                ReleaseSprint();
            }
            
            return false;
        }
        
        return true;
    }

    public override string ToString()
    {
        StringBuilder sb = new();
        
        sb.AppendLine($"Sprint Release: {Title}");
        sb.AppendLine($"Start Date: {StartDate.ToString("dd/mm/yyyy")}");
        sb.AppendLine($"End Date: {EndDate.ToString("dd/mm/yyyy")}");
        sb.AppendLine($"Scrum Master: {ScrumMaster.Name}");
        sb.AppendLine($"Amount of developers: {Developers.Count}");
        sb.AppendLine($"Amount of testers: {Testers.Count}");
        sb.AppendLine($"Status: {CurrentStatus.GetType()}");
        sb.AppendLine($"Amount of releases: {Releases.Count}");
        sb.AppendLine($"Amount of items: {SprintBacklog.Items.Count}");

        return sb.ToString();
    }
}