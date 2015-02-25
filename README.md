Dorkbots-Commands
=================

Dorkbots Commands is a flexible framework for creating and running parallel and serial commands.

Example:

var serial:ICommands = new SerialCommands();

// you will want to extend Command to add concrete functionality.
var commandOne:ICommand = new Command();
var commandTwo:ICommand = new Command();

// these will run one after the other
serial.addCommand(commandOne);
serial.addCommand(commandTwo);

var parallel:ICommands = new ParallelCommands();
var commandThree:ICommand = new Command();
var commandFour:ICommand = new Command();

// these will run simultaneously
parallel.addCommand(commandThree);
parallel.addCommand(commandFour);

// you can also add Commands to Commands
serial.addCommand(parallel);

serial.excute();

http://www.dorkbots.com/repositories/commands

Author: Dayvid jones
http://www.dayvid.com
Copyright (c) Disco Blimp 2015
http://www.discoblimp.com