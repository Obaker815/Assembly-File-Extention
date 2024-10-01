# Machine Code File Extention

## Uses the file extention .sack (i just coded it that way)

## This program uses lists and Arrays to store data and instructions, don't worry about actual memory addresses

## You will need to compile it yourself and make it the default for the .sack extention


### Commands :

LDA | ADDRESS           - sets the ACC value to the data at the ADDRESS given

STA | ADDRESS           - opposite of LDA

ADD | ADDRESS           - adds the data at the ADDRESS to the data in the ACC

SUB | ADDRESS           - same as ADD but substract


INP                     - prompts the user to input, then stored in the ACC

OUT                     - outputs the data in the ACC to the console


BRA | LINE              - branches to a set line

BRP | LINE              - branches to a set line if the contents of the ACC is positive (zero inclusive)

BRZ | LINE              - branches to a set line if the contents of the ACC is zero


VAR | ADDRESS | DATA    - stores data at a given address

HLT                     - end of program
