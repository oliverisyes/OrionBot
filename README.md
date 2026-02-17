# OrionBot
A simple discord bot built in C# using the [discord.net library](https://docs.discordnet.dev/).
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

More detail about how to use these commands can be found in the [for users section](#for-users).

## For Developers

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
