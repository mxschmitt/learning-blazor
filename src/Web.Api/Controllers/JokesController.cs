﻿// Copyright (c) 2021 David Pine. All rights reserved.
// Licensed under the MIT License.

using System.Globalization;
using System.Net.Mime;
using Learning.Blazor.Extensions;
using Learning.Blazor.JokeServices;
using Learning.Blazor.Models;
using Microsoft.AspNetCore.Mvc;

namespace Learning.Blazor.Api.Controllers;

[
    ApiController,
    Route("api/jokes")
]
public class JokesController : ControllerBase
{
    private readonly IJokeFactory _jokeFactory;
    private readonly ILogger<JokesController> _logger;

    public JokesController(
        IJokeFactory jokeFactory,
        ILogger<JokesController> logger) =>
        (_jokeFactory, _logger) = (jokeFactory, logger);

    [
        HttpGet,
        Route("random"),
        Produces(MediaTypeNames.Application.Json)
    ]
    public async Task<IActionResult> Get()
    {
        var locale = CultureInfo.CurrentCulture.TwoLetterISOLanguageName;
        if (locale is not null)
        {

        }

        _logger.LogInformation("{DateTime}: Getting weather", DateTime.UtcNow);

        JokeResponse joke = await _jokeFactory.GetRandomJokeAsync();

        return new JsonResult(joke, DefaultJsonSerialization.Options);
    }
}
