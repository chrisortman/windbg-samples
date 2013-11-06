using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Threading;

namespace SimpleApp.Wpf
{
    public static class Locks
    {
        public static readonly object Lock1 = new object();
        public static readonly object Lock2 = new object();
    }
    public class DateTimeService
    {


        public async Task<DateTimeOffset> WhatTimeIsIt(int delay = 5000)
        {
            await Task.Delay(delay);
            return DateTime.Now;

        }
    }

    public class DeadlockService
    {
        private List<Thread> _threads = new List<Thread>();
        private List<Task> _tasks = new List<Task>();

        public void TaskExample()
        {
            var bothTasksCreated = new TaskCompletionSource<bool>();
            Task t2 = null;
            Task t1 = Task.Run(async delegate
            {
                await bothTasksCreated.Task;
                await t2;
                
            });

            t2 = Task.Run(async delegate
            {
                await bothTasksCreated.Task;
                await t1;
            });

            bothTasksCreated.SetResult(true);

        }

        public void ClassicExample()
        {
            var t1 = new Thread(Classic1);
            var t2 = new Thread(Classic2);

            t1.Start();
            t2.Start();

        }

        private void Classic1()
        {
            lock (typeof (int))
            {
                Thread.Sleep(1000);
                lock (typeof (float))
                {
                    Console.WriteLine("Classic 1 finished");
                }
            }
        }

        private void Classic2()
        {
            lock (typeof (float))
            {
                Thread.Sleep(1000);
                lock (typeof (int))
                {
                    Console.WriteLine("Classic 2 finished");
                }
            }
        }
    }
}