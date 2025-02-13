﻿// Copyright (c) 2021 David Pine. All rights reserved.
// Licensed under the MIT License.

namespace Learning.Blazor.Api.Hubs;

public partial class NotificationHub
{
    public async Task JoinTweets()
    {
        await Groups.AddToGroupAsync(
            Context.ConnectionId,
            HubGroupNames.Tweets);

        if (_twitterService.CurrentStatus is StreamingStatus status)
        {
            await Clients.Caller.SendAsync(
                HubServerEventNames.StatusUpdated,
                Notification<StreamingStatus>.FromStatus(status));
        }

        if (_twitterService.LastFiftyTweets is { Count: > 0 })
        {
            await Clients.Caller.SendAsync(
                HubServerEventNames.InitialTweetsLoaded,
                Notification<List<TweetContents>>.FromTweets(
                    _twitterService.LastFiftyTweets.ToList()));
        }
    }

    public Task LeaveTweets() =>
        Groups.RemoveFromGroupAsync(
            Context.ConnectionId,
            HubGroupNames.Tweets);

    public Task StartTweetStream() =>
        _twitterService.StartTweetStreamAsync();
}
