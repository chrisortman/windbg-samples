using System;
using System.Threading.Tasks;

namespace SimpleApp.Wpf
{
    public class DateTimeService
    {
        public async Task<DateTimeOffset> WhatTimeIsIt(int delay = 5000)
        {
            await Task.Delay(delay);
            return DateTime.Now;
        }
    }
}