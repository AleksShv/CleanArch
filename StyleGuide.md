# Clean Arch Style Guide
Самый верхний уровень решения состоит из 3х каталогов.

```
.
├── solution items/
├── src/
└── tests/
```

## 1 solution items:
В данном каталоге расположены файлы не относящиеся к исходному коду приложения, например: файлы развёртывания, файлы конфигурации докера и т.д.

## 2 src:
В данном каталоге расположено всё, что связано с исходным кодом приложения.

В решении представлены следущии уровни разделения кода по его функциональным обязаностям:
```
.
├── Utils/
├── Entities/
├── UseCases/
├── Infrastructure/
├── Controllers/
└── Hosts/
```

### *2.1 Utils*
Общая вспомогательная функциональность распределенная по всему приложению **НЕ ОТНОСЯЩАЯСЯ К БИЗНЕС ЛОГИКЕ!!!**.

Если необходима вспомогательная функциональность для работы со сторонними библиотеками, *стоит вынести её в отдельную сборку*.

Пример возможно имеющихся компонентов-утилит:
- `CleanArch.Utils`,
- `CleanArch.Utils.EntityFrameworkCore`,
- `CleanArch.Utils.DistributedCache`.

### 2.2 *Entities*
Сущности являются ядром приложения.

В идеале не должны зависеть от сторонних библиотек/фреймворков.

Компонент сущностей `CleanArch.Entities` можно декомпозировать на три компонента:

- `CleanArch.Entities`,
- `CleanArch.Entities.Base`,
- `CleanArch.Entities.TypeDefinitions`.

`CleanArch.Entities.Base` следует вынести для переиспользования базовыз механизмов в других своих проектах.

`CleanArch.Entities.TypeDefinitions` следует вынести в случае когда необходимо вынести все `enum` из основной сборки, дабы не тянуть зависиости на весь компонент целиком.

Но в целом можно всё это оставить в одном проекте.

Сущности могут быть богатыми либо анемичными, либо два варианта сразу.

Для анемичных моделей стоит выделить компонет доменных сервисов `CleanArch.DomainServices`, для хранения реализаций основных бизнес правил.

Доменные сервисы могут представлять из себя статические классы (хелперы/методы расширений) либо классическую реализацию сервисов (контракт/реализация).

Компоновать папки можно по [Bounded Context](https://martinfowler.com/bliki/BoundedContext.html), либо по их ответственности.

Пример компоновки по Bounded Context:

```
.
└── Catalog/
    ├── ProductConsts.cs
    └── ProductManager.cs
```
Пример компоновки по ответственности:
```
.
├── Services/
│   └── ProductManager.cs
└── Constants/
    └── ProductConstants.cs
```	
Так же данные два стиля можно комбинировать разбивая внутреннее содержимое Bounded Context'а по ответственности, либо наоборот:
```
.
└── Catalog/
    ├── Services/
    │   └── ProductManager.cs
    └── Constants/
        └── ProductConsts.cs
```
```
.
├── Services/
│   └── Catalog/
│       └── ProductManager.cs
└── Constants/
    └── Catalog/
        └── ProductConsts.cs
```

### *2.3 UseCases*

Слой юзкейсов состоит из трёх **основных** компонентов: 
- `CleanArch.UseCases`, 
- `CleanArch.DataAccess.Contracts`, 
- `CleanArch.Infrastructure.Contracts`.

Компонент `CleanArch.UseCases` содержит объекты запросов и их обработчики. Запросы и их обработчики реализуются с помощью библиотеки [MediatR](https://github.com/jbogard/MediatR/wiki).

Команды и запросы (Commannds/Queries) являются частными случаями запросов (Requests). В случае когда слой контроллеров имеет свой набор данных (Requests/Responses) лучше использовать натацию иенований *Commands/Queries* дабы не пересекались наименования моделей.
```
Controllers Layer:
    AddProductRequest.cs

Use Cases Layer: 
    AddProductCommand.cs
```
В случае когда контроллер использует модели из слоя юзкейсов, можно оставить именование с постфиксом Request. DTO можно оставлять с соответствующим постфиксом, либо постфиксом Response.
```
Use Cases Layer:
    GetProductDetailsRequest.cs
    ProductDetailsDto.cs / ProductDetailsResponse.cs
```

Структура может выглядеть следующим образом Bounded Context -> Entity -> Slice:
```
.
└── Catalog/
    └── Products/
        └── AddProduct/
            ├── AddProductCommand.cs
            ├── AddProductCommandHandler.cs
            ├── AddProductCommandValidator.cs
            ├── AddProductCommandProfile.cs
            └── ProductDto.cs
```

Каждый может содержать:
- Запрос,
- Обработчик запросов,
- Валидатор запроса,
- Конфигурация маппера,
- DTO
- Исключения.

Конфигурацию маппера можно вынести и на пару уровней выше, но лучше компоновать именно так, поскольку при удалении среза, конфигурация удалиться вместе с ним, и не придётся вручную удалять конфиг.

Так же `Bounded Context` может включать в себя необходимые для реализации исключения, утилиты и т.д:
```
.
└── Catalog/
    ├── Products/
    ├── Exceptions/
    └── Utils/
        ├── ValidationExtensions.cs
        ├── MappingExtensions.cs
        └── ProductAccessValidator.cs
```

Так же исключения и утилиты могут находиться:
- на уровне сущностей, если используются несколькими сущностями,
- на уровне контекстов, если используются несколькими контекстами.

```
.
└── Catalog/
    └── UpdateProductDetails/
        ├── UpdateUpdateProductDetailsCommand.cs
        ├── UpdateUpdateProductDetailsCommandHandler.cs
        └── ProductNotFoundException.cs
```
```
.
└── Catalog/
    ├── UpdateProductDetails/
    ├── UploadProductImage/
    └── Exceptions/
        └── ProductNotFoundException.cs
```
```
.
├── Catalog/
├── Warehouse/
└── Exceptions/
    └── ProductNotFoundException.cs
```

Либо же вынести это всё в отдельный `Internal` каталог:
```
.
└── Internal/
    ├── Exceptions/
    ├── Utils/
    └── Services/
```

Еще один сбособ компоновки файлов Bounded Context -> Entity -> Functional Responsibility:
```
.
└── Catalog/
    └── Products/
        ├── Commands/
        │   └── AddProductCommand.cs
        ├── Handlers/
        │   └── AddProductCommandHandler.cs
        ├── Validators/
        │   └── AddProductCommandValidator.cs
        ├── Profiles/
        │   └── AddProductCommandpROFILE.cs
        └── Dtos/
            └── ProductDto.cs
```
Так же из данной цепочки можно убрать как Bounded Context, так и Entity, либо изменить порядок, зависит от личных предпочтений разработчика.

Почему стоит строить структуру опираясь на `Bounded Context`:
- По данной структуре проекта можно получить поверхностные знания о бизнесе с котрым идёт работы, и о его функциональных требованиях. Разбиение по функциональным ответственностям не даст нам такого преимущества
- Если есть необходимость в том, чтобы удалить одну из функциональных возможностей системы, достаточно удалить одну папку
- Легко маштабировать на модули/микросервисы по границам контекстов

Компонент `CleanArch.DataAccess.Contracts` содержит необходимые абстракции для работы с хранилищем данных. Это могут быть интерфейсы репозиториев, абстракция над EF DbContext, интерфейсы объектов запросов.

Компонент `CleanArch.Infrastructure.Contracts` содержит необходимые для работы приложения абстракции над инфраструктурой. К инфраструктуре относятся сервисы отправки сообщений по SMTP/SMPP протоколам, хранилища двоичной информации, шины сообщений, сервисы аутентификации/авторизации, генераторы exel отчётов и всё что связано с работой сторонних сервисов.

Можно иметь один компонент инфраструктуры со всем необходимым, либо декомпозировать данный компонент на компоненты с конкретными ответственностями.
```
.
└── CleanArch.Infrastructure.Contracts/
    ├── Authentication
    ├── BlobStorage
    └── SmtpService
```
```
.
├── CleanArch.Infrastructure.Authentication.Contracts
├── CleanArch.Infrastructure.BlobStorage.Contracts
└── CleanArch.Infrastructure.SmtpService.Contracts
```

Помимо доступа к данным, в отдельный компонент так же можно вынести и аутентификацию:
```
.
├── CleanArch.Authentication.Contracts
├── CleanArch.DataAccess.Contracts
└── CleanArch.Infrastructure.Contracts
```

Например при использовании `ASP.NET Identity` данный подход позволит:
- абстрагироваться от готовой реализации,
- использовать аутентификацию как часть веб инфраструктуры сразу из контроллера, не нагружая прикладной код.

Из компонент `CleanArch.UseCases` можно извлечь следующие вспомогательные компоненты:
- `CleanArch.UseCases.Common`,
- `CleanArch.UseCases.Base`.

`CleanArch.UseCases.Common` содержит общий функционал используемый запросами/обработчиками.

`CleanArch.UseCases.Base` содержит базовый функционал, на котором строятся все запросы/обработчики.


### *2.4 Infrastructure*
Данный слой содержит реализации:
- Уровня доступа к данным,
- Внешних сервисов,
- Бэкгроунд воркеров.

В именах компонентов, реализующих контрвкты инфраструктуру, стоит указывать имена внешних сервисов на основе которого реализуются контракты, например:
- `CleanArch.DataAccess.SqlServer`,
- `CleanArch.Infrastructure.Smtp.SendGrid`,
- `CleanArch.Infrastructure.Smpp.Twilio`,
- `CleanArch.Infrastructure.ExcelGenerator.EPPlus`,
- `CleanArch.Infrastructure.BlobStorage.AwsS3`. 

### *2.5 Controllers*

Слой контроллеров содержит сборки с контроллерами, базовым и/или общим функционалом. Если для каждого клиента предусмотрен свой набор контроллеров, стоит вынестви их в отдельные компоненты.

Набор компонентов может выглядеть следующим образом:
- `CleanArch.Controllers.WebApi`,
- `CleanArch.Controllers.PublicApi`,
- `CleanArch.Controllers.MobileApi`,
- `CleanArch.Controllers.Common`,
- `CleanArch.Controllers.Base`.

Либо может быть только одна сборка `CleanArch.Controllers`, если есть только один набор контроллеров.

В компоненте контроллеров могут лежать файлы ресурсов `resx`, `json`, `yml` для локализации мультиязычных приложений.

Структура компонента контроллеров может выглядеть следующим образом:
```
.
└── Catalog/
    ├── Requests/
    │   └── AddProductRequest.cs
    ├── Responses/
    │   └── ProductDetailsResponse.cs
    ├── Resources/
    │   ├── ProductsController.resx
    │   └── ProductsController.en-US.resx
    ├── Utils/
    │   └── CatalogMappingProfile.cs
    └── Controllers/
        └── ProductsController.cs
```

Слой контроллеров может иметь свой набор моделей для работы с веб средой, и мапить эти модели на модели запросов из слоя юзкейсов, либо же можно их переиспользовать.

```
public async Task<IActionResult> GetProductPageAsync(
    [FromQuery] GetProductPageRequest request, // модель запроса из слоя юзкейсов
    CancellationToken cancellationToken = default)
{
    var query = Mapper.Map<GetProductPageQuery>(request);
    var productsPage = await Sender.Send(query, cancellationToken);
    var response = Mapper.Map<PaggingResponse<ProductPaggingItemResponse>>(productsPage);
    return Ok(response);
}
```
```
public async Task<IActionResult> GetProductPageAsync(
    [FromQuery] GetProductPageRequest request, // модель запроса из слоя юзкейсов
    CancellationToken cancellationToken = default)
    => Ok(await Sender.Send(request, cancellationToken));
```

Подход с использованием моделей из слоя юзкейсов можно использовать, если есть необходимость в быстрой реализации функционала.

Приоритетным подходом является исаользование для каждого слоя своего собсдвенного набора моделей.

### *2.6 Hosts*

Данный слой содержит набор приложений, явлюющихся корнем композиции и собирающих рабочее приложение воедино.

Пример компонентов:
- `CleanArch.PublicApi`,
- `CleanArch.WebApi`,
- `CleanArch.Webhooks`,
- `CleanArch.SignalR`.

Либо же всё можно объеденить в одном компоненте
- `CleanArch.Host`

## 3 tests:
В данном каталоге расположены компоненты с тестами.

Под каждый тип теста выделяется отдельная сборка.

В решении представлены 3 сборки:
- `CleanArch.UnitTests` (юнит-тестыы)
- `CleanArch.IntegrationTests` (интеграционные тесты)
- `CleanArch.ArchitectureTests` (архитектурные тесты)