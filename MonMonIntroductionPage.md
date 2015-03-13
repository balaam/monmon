# Introduction #

I wrote MonMon because I wasn't happy with UltraEdit for developing Lua code. MonMon quickly became usable and I was happy to use it for Lua development but I've since discovered Sublime (http://www.sublimetext.com/). Sublime is an excellent editor and I've started using it in favour of MonMon.

The MonMon code is probably still quite useful if you're looking for reference about how to use Scintilla.NET (it's a little messy, I was hacky something together as fast as possible and then started to refactor it, which is the current state it's in.)

# Helping Out #

I'm not longer maintaining this project but if you want to compile the code you will need:

ScintillaNET(http://www.codeplex.com/ScintillaNET) DockPanel? Suite(http://sourceforge.net/projects/dockpanelsuite/) and NUnit(http://www.nunit.org/).

It uses the very latest version of DockPanel and requires the project to be included in the solution. (This was done so I could close tabs with the middle mouse button)

# Features #

  * Multiple tabs
  * Find All
  * Function listing
  * Full syntax highlighting for LUA
  * Partial implemented rule based autoformating.

## Coming features ##

  * More comphrehsive function listing
    * Ordering
    * Id broken function / malformed
    * Sorting function display
    * Allow code to have the functions sorted.
    * Close multiple files from the file tab
    * Change the numbers so they're not an awful light brown (same with braces)
    * Change awful 8 spaces? to a tab


# Done #
  * If something is already loaded just refresh it.
  * One instance of the program, everything opens in that.