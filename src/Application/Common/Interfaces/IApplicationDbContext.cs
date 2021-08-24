﻿using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<WeatherForecast> WeatherForecasts { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}