using DAL.IServices;
namespace DAL.Service;

public class Generic_Sevice<T>: IGeneric_Sevice<T> where T : class
{
    private List<T> _lstOop;
    private DBContextWebAPI db;

    public Generic_Sevice()
    {
        db=new DBContextWebAPI();
        _lstOop = new List<T>();
    }

    public List<T> getList()
    {
        try
        {
            _lstOop = db.Set<T>().ToList();
            return _lstOop;
        }
        catch (Exception e)
        {
            return null;
        }
    }

    public string Add(T sp)
    {
        try
        {
            db.Set<T>().Add(sp);
            return "successful";
        }
        catch (Exception e)
        {
            return e.Message;
        }
    }

    public string Edit(T sp)
    {

        try
        {
            db.Set<T>().Update(sp);
            return "successful";
        }
        catch (Exception e)
        {
            return e.Message;
        }
    }

    public string Delete(T sp)
    {
        try
        {
            db.Set<T>().Update(sp);
            return "successful";
        }
        catch (Exception e)
        {
            return e.Message;
        }
    }

    public string Save()
    {

        try
        {
            db.SaveChanges();
            return "successful";
        }
        catch (Exception e)
        {
            return e.Message;
        }
    }
}