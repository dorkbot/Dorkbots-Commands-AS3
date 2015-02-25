Dorkbots-Commands
=================

Dorkbots Commands is a flexible framework for creating and running parallel and serial commands.

Example:

var serial:ICommands = new SerialCommands();

// you will want to extend Command to add concrete functionality.<br>
var commandOne:ICommand = new Command();<br>
var commandTwo:ICommand = new Command();

// these will run one after the other<br>
serial.addCommand(commandOne);<br>
serial.addCommand(commandTwo);

var parallel:ICommands = new ParallelCommands();<br>
var commandThree:ICommand = new Command();<br>
var commandFour:ICommand = new Command();

// these will run simultaneously<br>
parallel.addCommand(commandThree);<br>
parallel.addCommand(commandFour);

// you can also add Commands to Commands<br>
serial.addCommand(parallel);

serial.excute();

http://www.dorkbots.com/repositories/commands

Author: Dayvid jones<br>
http://www.dayvid.com<br>
Copyright (c) Disco Blimp 2015<br>
http://www.discoblimp.com