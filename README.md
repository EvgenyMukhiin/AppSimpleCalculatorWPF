# AppSimpleCalculatorWPF
Простой калькулятор с возможностью добавления функционала через плагины. Плагин представляет из себя dll-сборку помещаемую в основную папку с проектом.
Структура решения состоить из 3х проектов:
- CalcWpfApp - Основной проект калькулятора
- Contract - Контракт для взаимодействия плагина и проекта
- Plugin - Плагин расширения (реализованно расширение программист)

Использованно: 
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
