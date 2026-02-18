# OrionBot
A simple Discord bot built in C# using the [Discord.Net library](https://docs.discordnet.dev).
> [!Caution]
> I do not recommend attempting to compile and run this bot yourself as it would require extensive modification to function correctly.

Invite the bot to your server using [this link!!](https://discord.com/oauth2/authorize?client_id=1325239790647640135&permissions=274877910016&integration_type=0&scope=bot)

Info for developers [here!](#for-developers)\
Info for users [here!](#for-users)

> [!Important]
> Before using any commands, ensure you've first created a [profile](#profile-anchor) with the bot.

Major features:
- !time - displays the time in the specified user's timezone
- !qotd - allows the bot to send a daily question in a specified channel

Other helpful commands:
- !help - gives assistance with using a specified command
- !prefix - changes the bot's command prefix to the specified symbol
- !enable/!disable - enables or disables specified feature
- !profile - displays the specified user's bot profile

See [for users](#for-users) for more details on how to use each command.

## For Developers
> [!Note]
> This project has been largely unmaintained for the past year.\
> I am planning to completely rewrite it with improved programming and structure in the near future.

### Project Details
As mentioned previously, this project is built in C# and uses the [Discord.Net library](https://docs.discordnet.dev) to connect to the Discord API, this allows me to use prewritten connections to the API so I can focus on the implementation of actual features rather than being concerned with interacting with the API directly.

For storage, I'm using a SQLite database utilising EF Core and LINQ to prevent possible issues with datatypes or mistyped SQL statements.

For timezone conversions, I've used [NodaTime](https://nodatime.org/) which similar to Discord.Net, allows me to focus on feature implementation rather than the complexities of timezones.

For question of the day scheduling, I've used [Quartz.Net](https://www.quartz-scheduler.net) which again like Discord.Net and NodaTime, allows me to focus on implementation rather than creating my own scheduling solution. 

### Deployment
I currently have this project deployed on a Raspberry Pi I own.\
This was to reduce hosting costs on a potentially indefinite deployment where a alternative cloud hosting solution would become unnecessarily expensive.

It uses a SQLite database locally stored on the Raspberry Pi for storage.\
This was due to SQLite's lightweight nature while still offering most of the benefits of any other relational database.\
SQLite working offline also avoids possible issues with bandwidth if it tries to read/write data while the Discord API is being used.

### Project Structure
The main OrionBot folder contains:
#### Program.cs
- Begins the program, begins the connection to the Discord API and checks the required database is present.
#### OrionContext.cs
- Uses EF Core to model the SQLite database by defining each of its tables as a set.
- Each of the tables then has a class, where their fields are defined.
- Also in these classes are methods using LINQ to query the database for data when needed.
#### IBot.cs
- Defines an interface for Bot.cs to implement.
#### EnvReader.cs
- Reads the .env file containing the bot API token.
#### Bot.cs
- Offers the choice to use either the debug or full versions of the bot.
- Connects to the Discord API via [Discord.Net](https://docs.discordnet.dev) and using the relevant API token.
- Schedules a [Quartz.Net](https://www.quartz-scheduler.net) job and trigger to be used for the qotd feature.
- Asynchronously handles "JoinedGuild" (joined a new Discord server) events.
  - When this is detected, the bot adds the server's details to the database.
- Asynchronously handles "MessageRecieved" (message sent somewhere the bot has access to) events.
  - Makes sure to ignore messages sent by other bots.
  - Checks for the command prefix. If found, executes the relevant command.
#### Commands (folder)
Contains folders for each of the commands available to users.\
All command classes are executed asynchronously and utilise the LINQ methods defined in OrionBot.context to complete database operations.
- EnableCommand
- HelpCommand
- HungerGamesCommand
  - Future feature
- PrefixCommand
- QotdCommand
  - Uses the [Quartz.Net](https://www.quartz-scheduler.net) schedule defined in Bot.cs to send a message at the same time everyday.
- TimezoneCommand
  - Uses [NodaTime](https://nodatime.org/) to get the current time in UTC and convert it to required timezone.

See [for users](#for-users) for more details about each command.

## For Users
> [!Important]
> Before using any commands, ensure you've first created a [profile](#profile-anchor) with the bot.

Info on how to use each of the bot's commands:

### Help
Provides help on how to use the bot commands.

Commands
- General help with the bot:\
  `!help`
- More specific help with a particular command:\
  `!help [profile/time/qotd]`

### Prefix
Allows changing the bot's command prefix.\
The default ! is a very command prefix for bots to use.\
If this conflicts with another bot, I recommend changing it to something else.

Commands
- Set command prefix:\
  *Although letters would work, I recommend using a symbol such as ? - $ _ or +*\
  `!prefix [chosen prefix]`

<a name="profile-anchor"></a>
### Profile
Allows the bot to store information about users and allows users to view that information.\
Only one profile can be stored per discord account.

Commands
- View your own profile:\
  `!profile`
- Add your profile to the bot:\
  *Your name doesn't have to match your discord username*\
  *I recommend using a nickname people know you by*\
  `!profile add [your name]`
- Change your profile name:\
  `!profile change [new name]`
- Permanently remove your profile:\
  `!profile remove`
- View another user's profile:\
  `!profile [their name]`

### Timezones
Allows users to view the time in other users' timezones.\
This can be particulary useful if you have multiple server members in different timezones.

Commands
- View your own time:\
  `!time`
- Add your timezone to the bot:\
  *If you're unsure what to enter for your timezone, you can find yours here: https://nodatime.org/TimeZones* \
  *Copy and paste the "Zone ID", some cities may be referenced under an unexpected continent name*\
  `!time add [continent/majorcity]`
- Permanently remove your timezone:\
  `!time remove`
  
### Question Of The Day
Sends a daily question for server members to answer from a premade list of questions.\
The question sends at 3pm UTC everyday.

Commands
- Set a channel to send questions in:\
  *Use this command in the channel you'd like to use*\
  `!qotd channel`  
- Set a role to be pinged alongside the question (optional)\
  `!qotd [role ID]`

### Enable/Disable
Allows server owners to enable or disable the timezone or question of the day commands.\
Both commands are enabled by default.

Commands
- Enable timezone/qotd commands:\
  `!enable [time/qotd]`
- Disable timezone/qotd commands:\
  `!disable [time/qotd]`
