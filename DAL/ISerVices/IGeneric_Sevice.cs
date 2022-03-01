namespace DAL.IServices;

public interface IGeneric_Sevice<T>
{
    public List<T> getList();
    public string Add(T sp);
    public string Edit(T sp);
    public string Delete(T sp);
    public string Save();
}