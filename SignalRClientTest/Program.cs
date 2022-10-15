// See https://aka.ms/new-console-template for more information
using Microsoft.AspNetCore.SignalR.Client;
using System.Runtime.CompilerServices;

Console.WriteLine("Hello, World!");

string _myAccessToken = "eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCIsImN0eSI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6InNoaSIsIm5hbWVpZCI6InlveW8iLCJBZ2VudElkIjoiVFcwMDEiLCJMYW5ndWFuZ2UiOiJ6aC1UVyIsIm5iZiI6MTY2NTU5MDk2MiwiZXhwIjoxNjY2MTk1NzYyLCJpYXQiOjE2NjU1OTA5NjJ9.GFf_1OzQ8VeD0_fTh5CmHDr8iYMFn72mDhl72B4bynvKmWLzCAVmuvmcyLaX0m6cA7BmbawRIQ3is5IIYlJplg";

string url = "https://localhost:7141/notificationHub";
//string url = "https://localhost:7141/ChatHub";
//string url = "https://wepoker-uat.azurewebsites.net/notificationHub";
//string url = "https://wepoker-uat.azurewebsites.net/Service/notify";

// 建立Hub
HubConnection connection = new HubConnectionBuilder()
    .WithUrl(url, options =>
    {
        options.AccessTokenProvider = () => Task.FromResult(_myAccessToken);
    })
    .WithAutomaticReconnect()
    .Build();

// 事件監聽
connection.On<string>("UpdList", handler: message => { Console.WriteLine("UpdList : " + message); });

connection.On<string>("UpdSelfID", handler: message => { Console.WriteLine("UpdSelfID : " + message); });

connection.On<string>("UpdContent", handler: message => { Console.WriteLine("UpdContent : " + message); });

connection.On<string>("Notify", handler: message => { Console.WriteLine("Notify : " + message); });

connection.On<string>("NotificationHook", handler: message => { Console.WriteLine("NotificationHook : " + message); });

// 建立連線
connection.StartAsync().ContinueWith(
    task =>
    {
        if (task.IsCompletedSuccessfully)
        {
            Console.WriteLine("Connection Started");
        }
        else
        {
            Console.WriteLine("Connection Failed");
        }
    });


//維持程式執行迴圈，直止輸入 close 文字串
while (Console.ReadLine() != "close")
{
    Console.WriteLine("Connection...");
    System.Threading.Thread.Sleep(1000);
}

//結束與 SignalR Hub Server 之間的連線
connection.StopAsync();

Console.WriteLine("close...");
