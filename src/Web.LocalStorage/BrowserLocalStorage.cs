﻿// Copyright (c) 2021 David Pine. All rights reserved.
// Licensed under the MIT License.

using Learning.Blazor.Extensions;
using Microsoft.JSInterop;

namespace Learning.Blazor.LocalStorage
{
    /// <summary>
    /// See: https://developer.mozilla.org/en-US/docs/Web/API/Window/localStorage
    /// </summary>
    internal class BrowserLocalStorage : ILocalStorage
    {
        private readonly IJSRuntime _jSRuntime;

        public BrowserLocalStorage(IJSRuntime jSRuntime) => _jSRuntime = jSRuntime;

        ValueTask ILocalStorage.ClearAsync() =>
            _jSRuntime.InvokeVoidAsync("localStorage.clear");

        ValueTask ILocalStorage.RemoveAsync(string key) =>
            _jSRuntime.InvokeVoidAsync("localStorage.removeItem", key);

        async ValueTask<TItem?> ILocalStorage.GetAsync<TItem>(string key) where TItem : class
        {
            var json = await _jSRuntime.InvokeAsync<string?>("localStorage.getItem", key);
            return json is { Length: > 0 } ? json.FromJson<TItem>() : default!;
        }

        ValueTask ILocalStorage.SetAsync<TItem>(string key, TItem item)
        {
            var json = item.ToJson() ?? "";
            return _jSRuntime.InvokeVoidAsync("localStorage.setItem", key, json);
        }
    }
}
