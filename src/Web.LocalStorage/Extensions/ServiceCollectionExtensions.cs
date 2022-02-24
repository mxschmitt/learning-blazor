﻿// Copyright (c) 2021 David Pine. All rights reserved.
// Licensed under the MIT License.

namespace Learning.Blazor.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddLocalStorage(
        this IServiceCollection services)
    {
        services.AddLogging();

        return services.AddSingleton<ILocalStorage, BrowserLocalStorage>()
                .AddSingleton<ISynchronousLocalStorage, SynchronousBrowserLocalStorage>();
    }
}
