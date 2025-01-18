<h1>OrionBot</h1>
A simple discord bot built in C# using the discord.net library.

<h2>Features</h2>
Profile:
Allows users to view their own and others profiles which displays all the information stored by the bot.

Timezones:
Allows users to save their timezone which allows other users to find out what time it is for them without having to ask.

Question Of The Day:
Allows server owners to set up a qotd to send to a specific channel at 3pm utc everyday, the qotd can optionally be set up to mention a role as well.

<h2>Usage Guide</h2>
!help to recieve general instructions on how to use the bot.
Add the feature's name after help for more information on that feature.

If the default ! command prefix conflicts with other bots in the server, you can use !prefix {new prefix} to change it to anything one character long.
You can also enable and disable commands if needed do: !enable or !disable followed by the command name.

<h3>Profile Commands</h3>
!profile add {name} allows a user to add themselves to the bot.
!profile allows the user to view their own profile, adding a name afterwards allows them to see another user's profile.
!profile change {name} allows the user to change the name stored.
!profile remove allows the user to remove their profile (This is permanent and irreversable).

<h3>Timezone Commands</h3>
!time add {Continent/City} allows the user to add their timezone to the bot. Some cities may be referenced under an unexpected continent name, if unsure check: https://en.wikipedia.org/wiki/List_of_tz_database_time_zones
!time allows the user to view the time in their own timezone.
!time for {name} allows the user to see what time it is for another user.
!time remove allows the user to remove their timezone from the bot (The profile will remain however).

<h3>Question Of The Day Commands</h3>
!qotd channel will set the channel the bot will send the qotd to, this must be used in the wanted channel.
!qotd role {role ID} will mention the role when the qotd is sent.
