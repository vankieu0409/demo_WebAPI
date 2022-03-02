namespace _2_BUS.Models;

public class ProductDetailTempplate
{
    private int id;
    private string name;
    private string skud;
    private int price;
    List<ThuocTinh> thuocTinhList;

    public ProductDetailTempplate()
    {
        
    }

    public ProductDetailTempplate(int id, string name, string skud, int price, List<ThuocTinh> thuocTinhList)
    {
        this.id = id;
        this.name = name;
        this.skud = skud;
        this.price = price;
        this.thuocTinhList = thuocTinhList;
    }

    public int Id
    {
        get => id;
        set => id = value;
    }

    public string Name
    {
        get => name;
        set => name = value;
    }

    public string Skud
    {
        get => skud;
        set => skud = value;
    }

    public int Price
    {
        get => price;
        set => price = value;
    }

    public List<ThuocTinh> ThuocTinhList
    {
        get => thuocTinhList;
        set => thuocTinhList = value;
    }
}