# GodHand
A tool for translating the japanese language to romaji and english language. This tool is meant for editing (3ds games) files.

## What does the program offer to you?
This tool lets you analyze and edit (3ds) files. To be specific, sort of .dat files.
It reads japanese characters like hiragana, katakana, kanjis and kana. I have added three inbuild translators which lets you translate it to romaji and english.
It shall help you to locate the japanese words / sentences and offer you a first rough translation. You don't have to rely on this feature, but it can help you.

Additionally, you can analyze common pictures formats and let an OCR do the translations.

For the mentioned translation projects, you can really create projects. So called project spaces allow you to manage your translations without touching the original fils.
If you think, you are finished with the translation, you can release your project folder to the community and let them apply your changes.

## How does it work?
The application is divided in several areas. Everything is managed via the title bar and the tab-control.
There are static tab-items like File and OCR. Others are dynamic and can be created on your will.

### Tab File
You can select a single file and edit it like you want. Everything you save here will be directly written to the file!

#### How to use
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
- Jisho Translation: Jisho will provide you with some additional information which the other two translators can't provide. Like english definitions, parts of speech, restriction ... .Translation is started when selected row changes.

### Tab OCR
You can select a single picture file and let run the OCR over your desired selection for a direct translation. The success depends on the quality of your chosen picture!

#### How to use
This is quite straight forwarded. You click on "Load Image" and select your japanese image file you want to translate.
If the image has been loaded, you can create a rectangle with your mouse (button down) on that image. If you have marked your text, let your mouse button go and wait a few seconds to load the Capture2Text result in the "Input" textbox. It will automaticall try to translate via google api into english. You might need to redo the steps for a good result.

### Encoder
You can now use a custom encoder. In the root subfolder *encoding*, you can place any .txt file which contains the format *{HexValue;Output Character}*.

Support is currently limited to max 3 bytes. 

For example
- E44E1E;丞  => 3 bytes
- E015;ガ  => 2 bytes
- 01;0  => 1 byte

It is still an alpha version. It needs to be tested for stable release. Please feel free to test it and giving feedback.
You can use the feature on "Tab File" and the project workspace.

### Settings
You can change some settings for the tool like enabling translation (romaji, english) at all or how you would like to have your romaji translation. Click on the title bar for activation.

## Projects
### Project Management
Project management allows you to create project spaces for your work. You can add, edit or delete projects.

- Add => Creates a project with a certain name and a root path to your files you want to edit
- Edit => Choose an existing project and change its name or rootpath
- Delete => Deletes a project

If you create a project, a folder will be created in the subfolder "projects".

### Project Workspaces
If you have created your project, you can select it from the titlebar and if you click on it, a tabitem will be added with the project name. In this workspace, you will have on the left side a navigator for the underlying folder and file structures of your selected rootpath.On the right side is the result table where you can act like in the Tab "File". You have to double click on the selected file to open the file.

You have an additional column on your result table which is called "PatchValue". If you edited the file before and saved your results, you can see your/the changes from before.
These changes are saved under the projects folder as a *.ghp* (GodHandPatch)

### Patching
Patching allows you your or from another user / team to use their translations.
You need to set the target folder (where your game is extracted) and the patch path (project path).

Be aware: The folder structure have to be the same in order for applying successful the patch.

## Pre-Requisites
You need Windows with .NET Framework 4.5.2. Then you are set to go.


## Possible Tasks
- [x] Custom encoding-tables (Alpha Version)
- [x] OCR-support?
- [ ] Adding other translations APIs
- [x] Creating a project folder structure
- [x] Adding a release patch management functionality

## Thanks
@linguanostra for providing the .NET wrapper for the KAKASI tool which is doing the romaji translation for you :+1:

## Links
Kasaki Wrapper -> https://github.com/linguanostra/Kakasi.NET

Capture2Text -> http://capture2text.sourceforge.net/
