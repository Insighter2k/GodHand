# GodHand
A tool for translating the japanese language to romaji and english language. This tool is meant for editing (3ds games) files.

## What does the program offer to you?
A tool for reading hiragana, katakana, kanjis and kana with inbuild romaji translation for (3ds game) files.
Additionally an english translator has been added for direct english translations.
It shall help you to locate the japanese words / sentences and offer you a a first inbuild translation to romaji / english. You don't have to rely on this feature, but it can help you, I guess. You can write down the new value and save it to the file directly.

## How does it work?
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

## Settings
You can change some settings for the tool like enabling translation (romaji, english) at all or how you would like to have your romaji translation.

## Pre-Requisites
You need Windows with .NET Framework 4.5.2. Then you are set to go.

## Possible Tasks
- [ ] Custom encoding-tables
- [ ] OCR-support?
- [ ] Adding other translations APIs

## Thanks
@linguanostra for providing the .NET wrapper for the KAKASI tool which is doing the romaji translation for you :+1:
