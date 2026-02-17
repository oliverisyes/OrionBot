# OrionBot
A simple discord bot built in C# using the [discord.net library](https://docs.discordnet.dev/).
**I do not recommend attempting to compile and run this bot yourself as it would require extensive modification to function correctly**

Info for developers [here!](https://github.com/oliverisyes/OrionBot/edit/main/README.md#for-developers)
Info for users [here!](https://github.com/oliverisyes/OrionBot/edit/main/README.md#for-users)

## For Developers

## For Users


<h2>Features</h2>
Profile:
Allows users to view their own and others profiles which displays all the information stored by the bot.

Timezones:
Allows users to save their timezone which allows other users to find out what time it is for them without having to ask.

Question Of The Day:
Allows server owners to set up a qotd to send to a specific channel at 3pm utc everyday, the qotd can optionally be set up to mention a role as well.

<h2>Usage Guide</h2>
!help to recieve general instructions on how to use the bot.<br>
Add the feature's name after help for more information on that feature.<br><br>

If the default ! command prefix conflicts with other bots in the server, you can use !prefix {new prefix} to change it to anything one character long.<br>
You can also enable and disable commands if needed do: !enable or !disable followed by the command name.

<h3>Profile Commands</h3>
!profile add {name} allows a user to add themselves to the bot.<br>
!profile allows the user to view their own profile, adding a name afterwards allows them to see another user's profile.<br>
!profile change {name} allows the user to change the name stored.<br>
!profile remove allows the user to remove their profile (This is permanent and irreversable).

<h3>Timezone Commands</h3>
!time add {Continent/City} allows the user to add their timezone to the bot. Some cities may be referenced under an unexpected continent name, if unsure check: https://en.wikipedia.org/wiki/List_of_tz_database_time_zones<br>
!time allows the user to view the time in their own timezone.<br>
!time for {name} allows the user to see what time it is for another user.<br>
!time remove allows the user to remove their timezone from the bot (The profile will remain however).

<h3>Question Of The Day Commands</h3>
!qotd channel will set the channel the bot will send the qotd to, this must be used in the wanted channel.<br>
!qotd role {role ID} will mention the role when the qotd is sent.
