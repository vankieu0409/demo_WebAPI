namespace _2_BUS.Models;

public class ThuocTinh
{
    private string option;
    private string value;

    public ThuocTinh()
    {
        
    }

    public ThuocTinh(string option, string value)
    {
        this.option = option;
        this.value = value;
    }

    public string Option
    {
        get => option;
        set => option = value;
    }

    public string Value
    {
        get => value;
        set => this.value = value;
    }
}