# sbox-boilerplate
A boilerplate for kickstarting your S&amp;box addon

# Getting started

There are a few steps to take to turn this boilerplate into your own addon.

### .addon
This file contains information about the addon, it must be modified to suit your addon.

* name - your addon name
* version - your addon version
* sharedassets - ??
* depends - an array of other addons that your addon will load first and have access to.  For example, `base` includes a basic player and movement controller.
* gamemodes - I think this is used to tell S&box to load your gamemode scripts?
  * name - your addon name.  This should be an all lowercase alphanumeric string
  * title - your addon title.  This can be a user-friendly string with capitals and spaces
  * description - your addon description
  * icon - full url to an icon that will display in the main menu.

### BoilerplateGame.cs
This is the script that initializes your gamemode.
1. Rename the file, namespace, class, and constructor to suit your addon, i.e. `DeathrunGame.cs`
2. Edit this line: `[Library("boilerplate", Title = "Boilerplate Addon")]`

# Mapping

Create maps in `/content_src/maps/yourmap.vmap`, when compiled in Hammer it will be moved to `/content/maps/yourmap.vpk`
