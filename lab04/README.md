# Лабораторная работа 4

## Предметная область

Предметной областью финальной лабораторной работы стал **прототип программы для генерации самоподписанных и пользовательских цифровых сертификатов X509**.

\

**Проблема**: Для хранения сертификатов требуется подключение базы данных, а также механизм передачи сгенерированных сертификатов в БД. Обязательным требованиям в любом более-менее серьезном проекте является тестирование функционала.

\

**Решение**: Для проверки работоспособности обычно создают отдельные тесты при помощи фреймворков, либо же пишут без них. Так или иначе, для этого необходимы **фиктивные службы**.

## Реализация:

Перед тем, как проводить тестирование - необходимо написать логику, которую будем проверять. В моем случае было реализовано:

\

**Интерфейсы**:
- CaCertificateRepository - интерфейс с методами для работы с хранилищем самоподписанных сертификатов
- UserCertificateRepository - интерфейс с методами для работы с хранилищем
   пользовательских сертификатов
- CertificateService - интерфейс основного сервиса

**Классы**
- X509CaCertificateData - класс самоподписанного сертификата
- X509UserCertificateData - класс пользовательского сертификата
- X509CertificateGenerator - класс с методами для генерации
- PgCertificateService - сервис прослойка для создания и передачи в реальную БД 
- PgCertificateRepository - класс с логикой для работы с БД, включает в себя SQL запросы
- FakeCertificateRepository - класс с логикой для работы с хэш-таблицей для тестирования
- FakeCertificateService - фейк-сервис прослойка для работы с хэш-таблицей

\
**Пояснение**
\
В действительности, может показаться, что классы Service и классы Repository дублируют логику и все это можно было бы поместить в один класс, однако это необходимо для того, чтобы инкапсулировать логику программы. Пользователь будет работать только с сервисами без необходимости лезть в реализацию основной логики.

### Код Repository-классов

```
public class PgCertificateRepository implements CaCertificateRepository, 
                                                UserCertificateRepository 
{
  public PgCertificateRepository(String host, int port, String dbName,
                                     String user, String password) {}
                                     
  private Connection getConnection() throws SQLException 
  {
        return DriverManager.getConnection(url, user, password);
  }

  @Override
  public void saveCa(X509CaCertificateData ca) {}

  @Override
  public Optional<X509CaCertificateData> findCaById(int id) {}

  @Override
  public List<X509CaCertificateData> findAllCa() {}

  private X509CaCertificateData mapToCa(ResultSet rs) throws SQLException {}

  private PrivateKey loadPrivateKey(int caId) {}

  @Override
  public void saveUser(X509UserCertificateData userCert) {}

  @Override
  public Optional<X509UserCertificateData> findUserById(int id) {}

  @Override
  public List<X509UserCertificateData> findAllUsers() {}

  private X509UserCertificateData mapToUser(ResultSet rs) throws SQLException {}
  
}
```
\
Данный класс прекрасно реализует логику работы с **PostgreSQL**, однако очевидной проблемой является то, что перед отправкой созданных сертификатов в реальную БД необходимо все проверить на несколько раз. Мною был реализован фейк-класс, который позволяет это сделать.

```
public class FakeCertificateRepository implements CaCertificateRepository, 
                                                  UserCertificateRepository 
{
    //Таблицы и Id для хранения
    private final Map<Integer, X509CaCertificateData> caStorage = new LinkedHashMap<>();
    private final Map<Integer, X509UserCertificateData> userStorage = new LinkedHashMap<>();
    private int caNextId = 1;
    private int userNextId = 1;

    @Override
    public void saveCa(X509CaCertificateData certificate) {}

    @Override
    public Optional<X509CaCertificateData> findCaById(int id) {}

    @Override
    public List<X509CaCertificateData> findAllCa() {}

    @Override
    public void saveUser(X509UserCertificateData certificate) {}

    @Override
    public Optional<X509UserCertificateData> findUserById(int id) {}

    @Override
    public List<X509UserCertificateData> findAllUsers() {}

    public int caSize() {}

    public int userSize() {}

    public int totalSize() {}

    public void clear() {
        caStorage.clear();
        userStorage.clear();
        caNextId = 1;
        userNextId = 1;
        System.out.println("[FAKE DB] Хранилище очищено");
    }
}
```

## Вывод

В ходе выполнения лабораторной работы был изучен и применен паттерн **Фиктивная служба** на примере прототипа программы для генерации самоподписанных и пользовательских цифровых сертификатов X509.
\
Основным и очевидным преимуществом применения паттерна является **возможность тестирования** важных участков и частей кода перед применением в реальных системах в "боевых условиях".
