global using AutoMapper;

global using System.Net.Http.Json;
global using System.Collections;

global using Microsoft.Extensions.Logging;
global using Microsoft.Extensions.Options;
global using Microsoft.Extensions.Hosting;
global using Microsoft.Extensions.DependencyInjection;

global using FantasyHelper.Data;
global using FantasyHelper.Data.Models;

global using FantasyHelper.Services.Helpers;
global using FantasyHelper.Services.Interfaces;
global using FantasyHelper.Shared.Config;
global using FantasyHelper.Shared.Dtos;
global using FantasyHelper.Shared.Enums;
global using FantasyHelper.Shared.Dtos.External.Allsvenskan;
global using FantasyHelper.Shared.Dtos.External.FPL;

global using static FantasyHelper.Data.Setup;
global using static FantasyHelper.Services.Setup;