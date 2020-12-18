# ispSemester3
# Быстров Максим Евгеньевич, гр953505 ЛР4

![Solution](https://i.imgur.com/6rIFZ8w.png)

TxtManager относится к ЛР2-3, labalabalaba2 - консольное приложение для реализации возможностей из лр3 и лр4. Остальные проекты к ЛР4.

Добавлен класс DataManager, задача которого - считать данные из БД, сгенерировать файл XML, и отправить по назначению(SourceDir, где его потом оформит приложение из 3 лабы).
Важные поля класса:
```
        DataManagerConfigModel config;
        Person PersonToSend { get; set; }
        Store StoreToSend { get; set; }
        BusinessEntityContact BusinessEntityContact { get; set; }
        ToFileModel result = new ToFileModel();
```
*ToFileModel* - класс, содержащий нужные нам данные(Person и Store) для сериализации в XML, и дальнейшей отправки.

*DataManagerConfigModel*- содержит ID 

DataManager использует конфигурацию *dbconfig.json*:

![dbconfig.json](https://i.imgur.com/Fy7IneJ.png)

В конфигурации указана строка подключения, а так же названия используемых процедур. 

Какие данные конкретно считывает DataManager:

По названию магазина, находит.. 

![)))](https://i.imgur.com/QQ2buWv.png)
(BusinessEntity)
Имеется ввиду хозяин магазина. Нам нужно связаться с ним, но мы знаем только сам магазин. По запросу, DataManager находит нам информацию: Имя, Фамилия, номер телефона и email. 
Большее думаю нам не нужно.

![XML файл](https://i.imgur.com/S2J7ECh.png)

видео с примером: https://youtu.be/bBZ1h7G4XU0

Все.
