Утилита перевода иврита на форме и в коде.

Для перевода hardcoded литералов в коде (вида if (MessageUtils.AskQuestion("האם למחוק שורה?"))):

В файле app.config проекта TranslateProgram указываем:

FileToRefactor - путь до файла с ивритом.
ResourcePath - путь до файла со строковыми ресурсами в проекте, обычно Strings.resx.
Mode - Code.
Запускаем, проверяем и переименовываем при необходимости сгенерированные названия ресурсов.
Для перевода форм:

Ставим Localizable в True: .
Меняем язык на English, переводим любую строчку, сохраняем. (Надо чтобы сгенерировался файл ИмяФормы.en.resx).
Меняем язык обратно на (Default).
В файле app.config ставим ResourcePath - путь до ИмяФормы.resx.
В файле app.config ставим Mode - Form.
Запускаем TranslateProgram.