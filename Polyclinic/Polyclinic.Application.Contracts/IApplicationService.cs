namespace Polyclinic.Application.Contracts;

/// <summary>
/// Общий интерфейс сервиса приложения для выполнения CRUD-операций над DTO
/// </summary>
/// <typeparam name="TDto">Тип DTO для чтения данных</typeparam>
/// <typeparam name="TCreateUpdateDto">Тип DTO для создания и обновления данных</typeparam>
/// <typeparam name="TKey">Тип первичного ключа сущности</typeparam>
public interface IApplicationService<TDto, TCreateUpdateDto, TKey>
    where TDto : class
    where TCreateUpdateDto : class
    where TKey : struct
{
    /// <summary>
    /// Создаёт новую запись на основе DTO
    /// </summary>
    /// <param name="dto">DTO с данными для создания</param>
    /// <returns>
    /// Созданный объект DTO с заполненным идентификатором
    /// </returns>
    public Task<TDto> Create(TCreateUpdateDto dto);

    /// <summary>
    /// Получает запись по идентификатору
    /// </summary>
    /// <param name="dtoId">Идентификатор записи</param>
    /// <returns>
    /// DTO записи, если она найдена; иначе null
    /// </returns>
    public Task<TDto?> Get(TKey dtoId);

    /// <summary>
    /// Получает список всех записей
    /// </summary>
    /// <returns>
    /// Коллекция всех записей DTO
    /// </returns>
    public Task<IList<TDto>> GetAll();

    /// <summary>
    /// Обновляет существующую запись
    /// </summary>
    /// <param name="dto">DTO с обновлёнными данными</param>
    /// <param name="dtoId">Идентификатор обновляемой записи</param>
    /// <returns>
    /// Обновлённый объект DTO
    /// </returns>
    public Task<TDto> Update(TCreateUpdateDto dto, TKey dtoId);

    /// <summary>
    /// Удаляет запись по идентификатору
    /// </summary>
    /// <param name="dtoId">Идентификатор удаляемой записи</param>
    /// <returns>
    /// true, если запись была найдена и удалена; иначе false
    /// </returns>
    public Task<bool> Delete(TKey dtoId);
}