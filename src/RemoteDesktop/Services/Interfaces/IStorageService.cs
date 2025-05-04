namespace RemoteDesktop.Services.Interfaces;

internal interface IStorageService
{
    void SaveData<T>(T data);

    T GetData<T>();
}