Background:
I attended a Webinar about tips for engaging an online audience (Cindy Huggett et al). One of the tips was to play "Buzzword Bingo" during the webinar as a tool to keeping audience attention. It was mentioned that the game the presenters have used in the past was a single page and so all attendees would have the same board. This could be a limitation of the GoToWebinar app. For me, most presentations I do are in-person, so physical hand-outs would be possible -- even if I was to do a remote meeting, I could easily find ways to share a set of Bingo boards... if I didn't have to make them by pen and paper, or using a tool like MSWord (which would be awkward).

This led to a question: How does one go about creating *many* boards so not everyone scores Bingo at the same time?

Since I'm taking programming classes, I figured I should probably develop a software solution.

Idea:
Create a printable board of randomized buzzwords that are in the context of the presentation.

The presenter could launch the program and import or type-in at least 24 buzzwords. And maybe input the number of players (printouts). The program would generate a printable board (console, web, forms, ... or something else) that could be printed and handed out to attendees, and all buzzwords would be randomly placed on the boards.

Vision of App Operation:
User launches LingoBingoGen.exe and is prompted to input or import new keywords/mini-phrases. 
Depending on user selection, either a file or db prompt is displayed, or the program begins prompting the user for input. 
If prompted for input and the use has no more, the program will take the empty string as a means to stop asking for more input. 
The user can then review the data that was input and can accept the data or try again. 
After reviewing the data and accepting it, the program will prompt for the number of pages to publish, the publishing method (on-screen for snippets; printer for physical copies). 
The program will then prompt the user to Publish or Quit/Cancel.
When the user selects Publish, the output is shown/printed.
After Publishing the user is presented with options to save the list, make a new list, or exit the program.

Development Expectations:
Code will be written within the bounds of my experience, knowledge, and gleaned from my current training topics.
All code will be of my own creation, barring publicly available examples or common coding methodologies.
Ripping code from other projects or sources does not conform with the spirit of this project; learning and growing dev skills does.
Methods, components, etc will be built one-by-one, refactored, removed, etc, as part of the learning process.
Solution will be coded per my current skill level.
Code will be updated, refactored, and so-on as I have time and inspiration to do so.

Usage Expectations:
You are free to fork this repository. You will be on your own as no warranty is implied. Your use of this software is at your own risk, and you accept that risk and release all liability from me to any harm or type of harm this software might do.


Originally Created: 23-May-2019