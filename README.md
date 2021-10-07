# NoLinkBot
A very simple bot that allows Administrators of a Discord server to block certain users from sending links. No database setup. 

## Prefix: `nlb!`

# Commands

## `nlb!help` 
shows a help message

## `nlb!disallow <user>` 
disallows a user from sending messages that contain links

## `nlb!allow <user>`
allows a user to send messages that contain links

## `nlb!status <user>`
retrieves the allowed/disallowed status of a user

## `nlb!list` 
retrieves a list of all the users that are prohibited from sending links

## `nlb!addrole <roleid/role name>`
adds a role to the list of roles that, if assigned to a user, can execute commands

## `nlb!removerole <roleid/role name>`
removes a role from the list of roles that, if assigned to a user, can execute commands

## `nlb!listroles`
retrieves a list of all the roles that can execute commands