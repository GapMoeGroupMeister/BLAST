public class StartButton : ObjectUI
{
    
    private void Start()
    {
        OnClickEvent.AddListener(HandleClickEvent);
    }

    public void HandleClickEvent()
    {
        // 뭐 게임이 시작되게 만들어야함
        print("ClickTest");
    }

    public void HandleEnterEvent()
    {
        
    }
}