# GodHand
A tool for translating the japanese language to romaji and english language. This tool is meant for editing (3ds games) files.

## What does the program offer to you?
A tool for reading hiragana, katakana, kanjis and kana with inbuild romaji translation for (3ds game) files.
Additionally an english translator has been added for direct english translations.
It shall help you to locate the japanese words / sentences and offer you a a first inbuild translation to romaji / english. You don't have to rely on this feature, but it can help you, I guess. You can write down the new value and save it to the file directly.

You also can now create projects for your work. In your project, the functionality is not that much different like working with a single file. More in a few sentences.

## How does it work?
### Tab File
The usage is simple. You click "SelectFile" and select your desired file.
When a file has been selected, you click "Open File" and the tool loads all words / sentences it can find to the result table. Additionally, you can set up the starting offset in "Start Offset" and the length of the offset, "Offset Length". For really big files, this option does make senses. Otherwise stick to "-1", which tells the tool to read the whole file.
If the tool find just one japanese char, it will be added to the result table. In that case, some entries might look akward. You should ignore those.

You will find following columns:
- Current Value: It will show you the current word / sentence the tool has found
- Length: It tells you, how long the the byte-array is for the current value
- New Value: You can enter your own value here. It accepts only typical UTF-8 letters.
- New Length: This is an important column. It displays the length of the new value. If this length exceeds the "Current Length", then the file may break the target application / game. Be careful. Every entered UTF-8 letter in "New Value" is an additional char which adds up to the length. I have created a mechanism for not accepting values which are greater.
- Changes?: If you changed a row, you need to check the checkbox or the change won't be written to the file. Automatically set if current and new value are different.

If you click on a row, a *detailed row* will be shown with the following content
- Romaji Translation: Depending on the content of "Current Value", it tries to translate it to romaji. I am not sure about whole sentences. Translation is started when selected row changes.
- Google English Translation: It tries to translate it to english via the google api. Translation is started when selected row changes.

#### Encoder
You can now use a custom encoder. In the root folder, you can place any .txt file which contains the format *{HexValue;Output Character}*, in the folder *encoding*.
It is currently limited to max 3 bytes. 

For example
- E44E1E;丞  => 3 bytes
- E015;ガ  => 2 bytes
- 01;0  => 1 byte

It is still an alpha version. It needs to be tested for stable release. Please feel free to test it and giving feedback.

### Tab OCR
This is quite straight forwarded. You click on "Load Image" and select your japanese image file you want to translate.
If the image has been loaded, you can create a rectangle with your mouse (button down) on that image. If you have marked your text, let your mouse button go and wait a few seconds to load the Capture2Text result in the "Input" textbox. It will automaticall try to translate via google api into english. You might need to redo the steps for a good result.

## Settings
You can change some settings for the tool like enabling translation (romaji, english) at all or how you would like to have your romaji translation.

## Projects
### Project Management
Project management allows you to create project spaces for your work. You can add, edit or delete projects.

- Add => Creates a project with a certain name and a root path to your files you want to edit
- Edit => Choose an existing project and change its name or rootpath
- Delete => Deletes a project

If you create a project, a folder will be created in the subfolder "projects".

This feature is the first step to maintain translations and future releases with patches for the community.

### Project Workspace
If you have created your project, you can select it from the titlebar and if you click on it, a tabitem will be added with the project name. In this workspace, you will have on the left side a navigator into your given rootpath, for the underlying folder and file structures.On the right side is the result table where you can act like in the Tab "File". You have to double click on the selected file to open the file.

## Pre-Requisites
You need Windows with .NET Framework 4.5.2. Then you are set to go.


## Possible Tasks
- [x] Custom encoding-tables (Alpha Version)
- [x] OCR-support?
- [ ] Adding other translations APIs
- [x] Creating a project folder structure
- [ ] Adding a release patch management functionality

## Thanks
@linguanostra for providing the .NET wrapper for the KAKASI tool which is doing the romaji translation for you :+1:

## Links
Kasaki Wrapper -> https://github.com/linguanostra/Kakasi.NET

Capture2Text -> http://capture2text.sourceforge.net/
