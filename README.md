# AppSimpleCalculatorWPF
Простой калькулятор с возможностью добавления функционала через плагины. Плагин представляет собой dll-сборку помещаемую в основную папку с проектом.
Структура решения состоит из 3-х проектов:
- CalcWpfApp - Основной проект калькулятора
- Contract - Контракт для взаимодействия плагина и проекта
- Plugin - Плагин расширения (реализованно расширение программист)

Использовано: 
- WPF,
- NET 6,
- MVVM,
- Microsoft.Extensions.Hosting,
- Microsoft.Extensions.DependencyInjection,
- Microsoft.Extensions.Logging,
- Managed Extensibility Framework.

Установленно из NuGet:
- System.ComponentModel.Composition
- Microsoft.Extensions.Hosting
- FontAwesome5

___Скриншоты:___

![AppSimpleCalculatorWPF](https://user-images.githubusercontent.com/59354723/187093847-9b4ec7e9-8d20-418d-923b-d45ada36b771.png)
