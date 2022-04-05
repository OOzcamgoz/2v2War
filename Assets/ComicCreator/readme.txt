Version 1.3

This is a Digital Comic Creator for Unity, it can be used to create linear or branching comics. The comic is all
done on the canvas so it is only 2d, I set it up in 16:9 resolution with a basic black bar system so the comics 
should fit to many different aspect ratios. I set it up for 1920x1080 resolution images, but you can try other sizes.

To make your comic you must create your own comic pages or images in whichever program you prefer and then import the
images into your unity project. Just drag them into your project folder. Prefabs are in the ComicCreator/Prefabs/ 
folder. Just drag one into the scene hierichy. Expand it to assign the cover image, and page images.

Comic Formats

Linear - make a straight up linear comic.
Branching - Make a comic with different paths/branches.
Diamond patterns - a format I came up with that is just a branching/merging system. Here are the specs for each:
	4x4 - 16 pages, story is 7 pages long with 20 possible paths.
	5x5 - 25 pages, story is 9 pages long with 70 possible paths. 
	6x6 - 36 pages, story is 11 pages long with 252 possible paths.
	7x7 - 49 pages, story is 13 pages long with 924 possible paths.
	8x8 - 64 pages, story is 15 pages long with 3432 possible paths.


Making a Linear Comic

1 import artwork
Create a folder in your project for your images. Drag and drop your artwork into your new folder.

2 drag in prefab
There are 3 diffeent comic formats, Go to the ComicCreator/Prefabs/Templates/ folder and drag the linear comic
prefab into the scene hierichy.

3 cover
Expand the LinearComic, then Canvas, then Backdrop. Click on cover and assign an image on the image componant.
Now to add a title, using the text componant, enter the text then assign an appropriate font and size, then place
appropriately.
The cover has a script on it so when you click it it will be disabled, opening the comic book.

4 pages
Comic pages are in the ComicCreator/Prefabs/ComicPages folder. The Comic prefab comes with a page already and you
can just duplicate it as many pages as you need. Add yor images to the comic page and then create the frames with
the horizontal/vertical frame prefabs from the ComicCreator/Prefabs/ComicFrames/ folder, just place and stretch to
size.

Speech Bubbles
Everything you need to create speech bubbles are in the ComicCreator/Prefabs/SpeechBubbles/ folder.

Text Boxes
There are white and yellow text boxes in the ComicCreator/Prefabs/TextBoxes/ folder.