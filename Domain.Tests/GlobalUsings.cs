global using Xunit;
global using Domain.Entities;
global using Domain.Interfaces.Factories;
global using Domain.Enums;
global using Domain.Interfaces.Strategies;
global using Infrastructure.Libraries.VersionControls;
global using Moq;
global using Domain.States.BacklogItem;

using System.Reflection;

Assembly.Load("Infrastructure");