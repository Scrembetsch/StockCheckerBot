# StockCheckerBot

This bot was a personal project to get a AMD graphics card for MSRP (It worked).

## Functionality
The items the bot should check can be configured int the `App.config` or the `StockCheckerBot.dll.config` if the release build is used. Just modify the Items if you want other ones.

The bot only checks if the item is in stock and opens the corresponding website. So you can buy it yourself. The value in `SiteOpenThreshold` is used to have a delay when the item is in stock to avoid spam-opening the website.

The `FallbackUrl` will be opened, when the bot had problems getting the stock (Adjust `RestartThreshold` to avoid spamming). On AMD store website the bot usually has problems when the buying queue is opened or they are adjusting something in the store (mostly on days a restock happens)