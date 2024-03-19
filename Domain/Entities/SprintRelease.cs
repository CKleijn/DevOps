﻿using System.Text;
using Domain.Helpers;
using Domain.States.Sprint;

namespace Domain.Entities;

public class SprintRelease : Sprint
{
    private IList<Release> _releases { get; init; }
    public IList<Release> Releases { get => _releases; init => _releases = value; }
    
    private ReleasePipeline _pipeline { get; set; }
    public ReleasePipeline Pipeline { get => _pipeline; set => _pipeline = value; }
    
    
    //TODO: implement functions

    public SprintRelease(string title, DateTime startDate, DateTime endDate, User scrumMaster) : base(title, startDate, endDate, scrumMaster)
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
        sb.AppendLine($"Start Date: {StartDate}");
        sb.AppendLine($"End Date: {EndDate}");
        sb.AppendLine($"Scrum Master: {ScrumMaster}");
        sb.AppendLine($"Status: {CurrentStatus.GetType()}");
        sb.AppendLine($"Releases: {Releases.Count}");
        
        return sb.ToString();
    }
}