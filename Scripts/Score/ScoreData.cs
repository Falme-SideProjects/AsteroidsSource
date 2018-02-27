public class ScoreData {

    //Datas to save togheter
    public string name;
    public int value;

    public ScoreData(string name, int value)
    {
        this.name = name;
        this.value = value;
    }

    public void setName(string name)
    {
        this.name = name;
    }


    public void setValue(int value)
    {
        this.value= value;
    }
}
